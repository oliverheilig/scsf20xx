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
Imports System.ComponentModel
Imports System.Globalization
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

Namespace Services
	''' <summary>
	''' Implements simple file based <see cref="IStatePersistenceService"/> that stores
	''' the state in binary files using the <see cref="BinaryFormatter"/> serializer.
	''' </summary>
	Public Class FileStatePersistenceService
		Inherits StreamStatePersistenceService

		Private innerBasePath As String

		Private ReadOnly fileNameFormat As String = "{0}.state"

		''' <summary>
		''' Initializes the service with the default folder location being the 
		''' current <see cref="System.AppDomain.BaseDirectory"/>.
		''' </summary>
		Public Sub New()
			Me.New(AppDomain.CurrentDomain.BaseDirectory)
		End Sub

		''' <summary>
		''' Initializes the service specifying the root folder to use for the persisted state.
		''' </summary>
		''' <param name="basePath">Root folder for the persisted state.</param>
		''' <remarks>
		''' If the basePath is a relative path, it will be resolved relative to 
		''' the <see cref="AppDomain.BaseDirectory"/>.
		''' </remarks>
		Public Sub New(ByVal basePath As String)
			Me.innerBasePath = basePath
		End Sub

		''' <summary>
		''' Specifies the root folder for the persisted state.
		''' </summary>
		''' <remarks>
		''' If the value is a relative path, it will be resolved relative to 
		''' the <see cref="AppDomain.BaseDirectory"/>.
		''' </remarks>
		Public Property BasePath() As String
			Get
				Return innerBasePath
			End Get
			Set(ByVal value As String)
				innerBasePath = New DirectoryInfo(Value).FullName
			End Set
		End Property

		''' <summary>
		''' Determines whether the file associated with the given state id exists.
		''' </summary>
		''' <param name="id">Identifier of the state.</param>
		''' <returns>true if the file exists; otherwise false.</returns>
		Public Overrides Function Contains(ByVal id As String) As Boolean
			Return File.Exists(GetFileName(id))
		End Function

		''' <summary>
		''' Removes the file associated with the given state id.
		''' </summary>
		''' <param name="id">Identifier of the state to remove.</param>
		Public Overrides Sub RemoveStream(ByVal id As String)
			Dim filename As String = GetFileName(id)
			If File.Exists(filename) Then
				File.Delete(filename)
			End If
		End Sub

		''' <summary>
		''' Retrieves the storage file <see cref="Stream"/>associated with the given state id.
		''' </summary>
		''' <param name="id">Identitier of the state.</param>
		Protected Overrides Function GetStream(ByVal id As String) As Stream
			Dim filename As String = GetFileName(id)
			If File.Exists(filename) Then
				Return New FileStream(filename, FileMode.Open, FileAccess.ReadWrite)
			Else
				Return New FileStream(filename, FileMode.Create, FileAccess.ReadWrite)
			End If
		End Function

		Private Function GetFileName(ByVal id As String) As String
			Return Path.Combine(innerBasePath, String.Format(CultureInfo.InvariantCulture, fileNameFormat, id))
		End Function
	End Class
End Namespace