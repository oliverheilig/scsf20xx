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
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.Globalization
Imports System.IO
Imports System.IO.IsolatedStorage

Namespace Services
	''' <summary>
	''' Service that uses isolated storage for state persistence.
	''' </summary>
	Public Class IsolatedStorageStatePersistenceService
		Inherits StreamStatePersistenceService

		''' <summary>
		''' Configuration attribute that can be passed to the <see cref="IConfigurable.Configure"/> implementation 
		''' on the service, to determine the scope of the isolated storage to use for persistence. It must be a 
		''' key in the dictionary with the name <c>Scope</c>.
		''' </summary>
		''' <example>
		''' Supported syntax is the same one as C#, except that the <see cref="System.IO.IsolatedStorage.IsolatedStorageScope"/> enum type 
		''' must be omited:
		''' <code>Assembly | User</code>
		''' or:
		''' <code>Roaming | User | Assembly | Domain</code>
		''' </example>
		Public Const ScopeAttribute As String = "Scope"

		Private innerScope As IsolatedStorageScope

		''' <summary>
		''' Initializes the service with the default isolated store, which is the combination 
		''' of the flags <see cref="IsolatedStorageScope.Roaming"/>, <see cref="IsolatedStorageScope.User"/>,
		''' <see cref="IsolatedStorageScope.Assembly"/> and <see cref="IsolatedStorageScope.Domain"/>.
		''' </summary>
		Public Sub New()
			Me.New(IsolatedStorageScope.Roaming Or IsolatedStorageScope.User Or IsolatedStorageScope.Assembly Or IsolatedStorageScope.Domain)
		End Sub

		''' <summary>
		''' Initializes the service with a specific isolated storage scope.
		''' </summary>
		Public Sub New(ByVal scope As IsolatedStorageScope)
			Me.innerScope = scope
		End Sub

		''' <summary>
		''' Checks if the persistence service has the state with the specified
		''' id in its storage.
		''' </summary>
		''' <param name="id">The id of the state to look for.</param>
		''' <returns>true if the state is persisted in the storage; otherwise false.</returns>
		Public Overrides Function Contains(ByVal id As String) As Boolean
			Dim store As IsolatedStorageFile = IsolatedStorageFile.GetStore(innerScope, Nothing, Nothing)
			Dim files As String() = store.GetFileNames(GetFileName(id))

			Return files.Length = 1
		End Function

		''' <summary>
		''' Removes the state from the persistence storage.
		''' </summary>
		''' <param name="id">The id of the state to remove.</param>
		Public Overrides Sub RemoveStream(ByVal id As String)
			If Contains(id) Then
				Dim store As IsolatedStorageFile = IsolatedStorageFile.GetStore(innerScope, Nothing, Nothing)
				store.DeleteFile(GetFileName(id))
			End If
		End Sub

		''' <summary>
		''' Retrieves the <see cref="Stream"/> to use as the storage for the state.
		''' </summary>
		''' <param name="id">The identifier of the state.</param>
		''' <returns>The storage stream.</returns>
		Protected Overrides Function GetStream(ByVal id As String) As Stream
			Dim store As IsolatedStorageFile = IsolatedStorageFile.GetStore(innerScope, Nothing, Nothing)
			Dim fileName As String = GetFileName(id)

			If Contains(id) = False Then
				Return New IsolatedStorageFileStream(fileName, FileMode.CreateNew, store)
			Else
				Return New IsolatedStorageFileStream(fileName, FileMode.Open, store)
			End If
		End Function

		''' <summary>
		''' Configures the service with the <see cref="ScopeAttribute"/> value, to customize the 
		''' store location.
		''' </summary>
		''' <param name="settings">Values to configure the service with.</param>
		Public Overrides Sub Configure(ByVal settings As NameValueCollection)
			MyBase.Configure(settings)
			If (Not String.IsNullOrEmpty(settings(ScopeAttribute))) Then
				Dim values As String() = settings(ScopeAttribute).Split(New Char() {"|"c}, StringSplitOptions.RemoveEmptyEntries)
				Dim scopes As List(Of IsolatedStorageScope) = New List(Of IsolatedStorageScope)(values.Length)
				For Each value As String In values
					Dim trimmed As String = value.Trim()
					If trimmed.Length > 0 Then
						If System.Enum.IsDefined(GetType(IsolatedStorageScope), trimmed) = False Then
							Throw New StatePersistenceException(String.Format(CultureInfo.CurrentCulture, My.Resources.InvalidIsolatedStorageStatePersistanceScope, settings(ScopeAttribute), trimmed))
						End If
						scopes.Add(CType(System.Enum.Parse(GetType(IsolatedStorageScope), trimmed), IsolatedStorageScope))
					End If
				Next value

				innerScope = IsolatedStorageScope.None
				For Each tmpscope As IsolatedStorageScope In scopes
					innerScope = innerScope Or tmpscope
				Next tmpscope

				' Try to initialize a store with the given scope.
				Try
					Dim store As IsolatedStorageFile = IsolatedStorageFile.GetStore(innerScope, Nothing, Nothing)
				Catch aex As ArgumentException
					Throw New StatePersistenceException(String.Format(CultureInfo.CurrentCulture, My.Resources.InvalidIsolatedStorageStatePersistanceParsedScope, settings(ScopeAttribute)), aex)
				End Try
			End If
		End Sub

		''' <summary>
		''' Removes all state from the store.
		''' </summary>
		Public Sub Clear()
			Dim store As IsolatedStorageFile = IsolatedStorageFile.GetStore(innerScope, Nothing, Nothing)
			Dim files As String() = store.GetFileNames("*")
			For Each file As String In files
				store.DeleteFile(file)
			Next file
		End Sub

		''' <summary>
		''' Specifies the scope for the storage.
		''' </summary>
		Public Property Scope() As IsolatedStorageScope
			Get
				Return innerScope
			End Get
			Set(ByVal value As IsolatedStorageScope)
				innerScope = Value
			End Set
		End Property

		Private Function GetFileName(ByVal id As String) As String
			Return String.Format(CultureInfo.InvariantCulture, "{0}.state", id)
		End Function
	End Class
End Namespace