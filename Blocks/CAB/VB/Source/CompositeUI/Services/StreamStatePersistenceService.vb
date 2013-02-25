'===============================================================================
' Microsoft patterns & practices
' CompositeUI Application Block
'===============================================================================
' Copyright © Microsoft Corporation.  All rights reserved.
' THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
' OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
' LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
' FITNESS FOR A PARTICULAR PURPOSE.
'===============================================================================


Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Specialized
Imports System.Globalization
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

Namespace Services
	''' <summary>
	''' Implements simple file based <see cref="IStatePersistenceService"/> that stores
	''' <see cref="State"/> data in binary files using the <see cref="BinaryFormatter"/> serializer.
	''' </summary>
	Public MustInherit Class StreamStatePersistenceService
		Implements IStatePersistenceService, IConfigurable

		Private useCryptography As Boolean = False

		Private cryptoSvc As ICryptographyService = Nothing

		''' <summary>
		''' The <see cref="ICryptographyService"/> used to protect sensitive data.
		''' </summary>
		<ServiceDependency(Required:=False)> _
		Public Property CryptographyService() As ICryptographyService
			Get
				If cryptoSvc Is Nothing Then
					Throw New StatePersistenceException(String.Format(CultureInfo.CurrentCulture, _
					   My.Resources.ServiceMissingExceptionMessage, GetType(ICryptographyService), Me.GetType()))
				End If

				Return cryptoSvc
			End Get
			Set(ByVal value As ICryptographyService)
				cryptoSvc = Value
			End Set
		End Property

		''' <summary>
		''' Configuration attribute that can be passed to the <see cref="IConfigurable.Configure"/> implementation 
		''' on the service, to determine whether cryptography must be used for persistence. It must be a 
		''' key in the dictionary with the name <c>UseCryptography</c>.
		''' </summary>
		Public Const UseCryptographyAttribute As String = "UseCryptography"

#Region "Utility code for subprocedure Save and function Load"

		Private Class SaveLoadUtilityProvider
			Private dataField As Byte()
			Private stateField As State
			Private innerFormatter As BinaryFormatter

			Public Property Data() As Byte()
				Get
					Return dataField
				End Get
				Set(ByVal value As Byte())
					dataField = value
				End Set
			End Property

			Public Property State() As State
				Get
					Return stateField
				End Get
				Set(ByVal value As State)
					stateField = value
				End Set
			End Property

			Public WriteOnly Property Formatter() As BinaryFormatter
				Set(ByVal value As BinaryFormatter)
					innerFormatter = value
				End Set
			End Property

			Public Sub WriteStream(ByVal fs As Stream)
				fs.Write(dataField, 0, dataField.Length)
			End Sub

			Public Sub SerializeStream(ByVal stream As Stream)
				innerFormatter.Serialize(stream, stateField)
			End Sub

			Public Sub ReadStream(ByVal stream As Stream)
				dataField = New Byte(CInt(stream.Length) - 1) {}
				stream.Read(dataField, 0, CInt(stream.Length))
			End Sub

			Public Sub DeserializeStream(ByVal stream As Stream)
				stateField = CType(innerFormatter.Deserialize(stream), State)
			End Sub
		End Class

#End Region

		''' <summary>
		''' Saves the <see cref="State"/> to the persistence storage.
		''' </summary>
		''' <param name="state">The <see cref="State"/> to store.</param>
		Public Sub Save(ByVal state As State) Implements IStatePersistenceService.Save
			If state Is Nothing Then
				Throw New ArgumentNullException("state")
			End If
			Try
				' Always overwrite state when saving.
				If Contains(state.ID) Then
					Remove(state.ID)
				End If

				Dim utilityProvider As SaveLoadUtilityProvider = New SaveLoadUtilityProvider()
				If useCryptography Then
					Dim fmt As BinaryFormatter = New BinaryFormatter()

					Using stream As MemoryStream = New MemoryStream()
						fmt.Serialize(stream, state)
						Dim data As Byte() = CryptographyService.EncryptSymmetric(stream.GetBuffer())

						utilityProvider.Data = data
						OpenStream(state.ID, AddressOf utilityProvider.WriteStream)
					End Using
				Else
					Dim fmt As BinaryFormatter = New BinaryFormatter()
					utilityProvider.Formatter = fmt
					utilityProvider.State = state
					OpenStream(state.ID, AddressOf utilityProvider.SerializeStream)
				End If
			Catch ex As Exception
				Throw New StatePersistenceException(String.Format(CultureInfo.CurrentCulture, My.Resources.CannotSaveState, state.ID), ex)
			End Try
		End Sub

		''' <summary>
		''' Retrieves the saved <see cref="State"/> from the persistence storage.
		''' </summary>
		''' <param name="id">The id of the <see cref="State"/> to retrieve.</param>
		''' <returns>The <see cref="State"/> instance created from the store.</returns>
		Public Function Load(ByVal id As String) As State Implements IStatePersistenceService.Load
			If Contains(id) = False Then
				Throw New StatePersistenceException(String.Format(CultureInfo.CurrentCulture, My.Resources.StateDoesNotExist, id))
			End If
			Try
				Dim utilityProvider As SaveLoadUtilityProvider = New SaveLoadUtilityProvider()
				If useCryptography Then
					Dim cipherData As Byte() = Nothing
					OpenStream(id, AddressOf utilityProvider.ReadStream)
					cipherData = utilityProvider.Data

					Using ms As MemoryStream = New MemoryStream(CryptographyService.DecryptSymmetric(cipherData))
						Dim fmt As BinaryFormatter = New BinaryFormatter()
						Return CType(fmt.Deserialize(ms), State)
					End Using
				Else
					Dim fmt As BinaryFormatter = New BinaryFormatter()
					Dim state As State = Nothing
					utilityProvider.Formatter = fmt
					OpenStream(id, AddressOf utilityProvider.DeserializeStream)
					state = utilityProvider.State

					Return state
				End If
			Catch ex As Exception
				Throw New StatePersistenceException(String.Format(CultureInfo.CurrentCulture, My.Resources.CannotLoadState, id), ex)
			End Try
		End Function

		''' <summary>
		''' Removes the <see cref="State"/> from the persistence storage.
		''' </summary>
		''' <param name="id">The id of the <see cref="State"/> to remove.</param>
		Public Sub Remove(ByVal id As String) Implements IStatePersistenceService.Remove
			Try
				RemoveStream(id)
			Catch ex As Exception
				Throw New StatePersistenceException(String.Format(CultureInfo.CurrentCulture, My.Resources.CannotLoadState, id), ex)
			End Try
		End Sub

		''' <summary>
		''' Checks if the persistence services has the <see cref="State"/> with the specified
		''' id in its storage.
		''' </summary>
		''' <param name="id">The id of the <see cref="State"/> to look for.</param>
		''' <returns>true if the <see cref="State"/> is persisted in the storage; otherwise false.</returns>
		Public MustOverride Function Contains(ByVal id As String) As Boolean Implements IStatePersistenceService.Contains

		Private Shared Sub ThrowIfInvalidStream(ByVal stm As Stream)
			If stm Is Nothing OrElse stm.CanRead = False Then
				Throw New StatePersistenceException(My.Resources.InvalidStateStream)
			End If
		End Sub

		''' <summary>
		''' Removes the <see cref="State"/> from the persistence storage.
		''' </summary>
		''' <param name="id">The id of the <see cref="State"/> to remove.</param>
		Public MustOverride Sub RemoveStream(ByVal id As String)

		''' <summary>
		''' Retrieves the <see cref="Stream"/> to use as the storage for the <see cref="State"/>.
		''' </summary>
		''' <param name="id">The identifier of the <see cref="State"/> to retrieve.</param>
		''' <returns>The storage stream.</returns>
		Protected MustOverride Function GetStream(ByVal id As String) As Stream

		''' <summary>
		''' Retrieves the <see cref="Stream"/> to use as the storage for the <see cref="State"/>, specifying whether the 
		''' stream should be disposed after usage.
		''' </summary>
		''' <param name="id">The identifier of the associated <see cref="State"/>.</param>
		''' <param name="shouldDispose">A <see labgword="bool"/> value indicating if the stream will be 
		''' disposed after usage.</param>
		''' <returns>The storage stream.</returns>
		Protected Overridable Function GetStream(ByVal id As String, <System.Runtime.InteropServices.Out()> ByRef shouldDispose As Boolean) As Stream
			shouldDispose = True
			Return GetStream(id)
		End Function

		''' <summary>
		''' Configures the <see cref="StreamStatePersistenceService"/> using the settings provided
		''' by the provided settings collection.
		''' </summary>
		''' <param name="settings"></param>
		Public Overridable Sub Configure(ByVal settings As NameValueCollection) Implements IConfigurable.Configure
			If settings Is Nothing Then
				Throw New ArgumentNullException("settings")
			End If
			If (Not String.IsNullOrEmpty(settings(UseCryptographyAttribute))) Then
				If Boolean.TryParse(settings(UseCryptographyAttribute), useCryptography) = False Then
					Throw New StatePersistenceException(My.Resources.InvalidCryptographyValue)
				End If
			End If
		End Sub

		Private Delegate Sub OpenStreamDelegate(ByVal openedStream As Stream)

		Private Sub OpenStream(ByVal stateId As String, ByVal operation As OpenStreamDelegate)
			Dim dispose As Boolean
			Dim stream As Stream = GetStream(stateId, dispose)

			Try
				ThrowIfInvalidStream(stream)
				operation(stream)
			Finally
				If dispose AndAlso Not stream Is Nothing Then
					CType(stream, IDisposable).Dispose()
				End If
			End Try
		End Sub
	End Class
End Namespace