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
Imports System.IO
Imports System.Reflection
Imports Microsoft.Practices.CompositeUI.Configuration

Namespace Services
	''' <summary>
	''' This implementation of <see cref="IModuleEnumerator"/> processes the assemblies located in a specified
	''' folder and enumerates the modules locating them using reflection.
	''' </summary>
	Public Class ReflectionModuleEnumerator
		Implements IModuleEnumerator

		Private innerBasePath As String

		''' <summary>
		''' Initializes a new <see cref="ReflectionModuleEnumerator"/> instance.
		''' </summary>
		Public Sub New()
			innerBasePath = AppDomain.CurrentDomain.BaseDirectory
		End Sub

		''' <summary>
		''' Gets or sets the path where the assemblies to be reflected are located.
		''' </summary>
		Public Property BasePath() As String
			Get
				Return innerBasePath
			End Get
			Set(ByVal value As String)
				innerBasePath = Value
			End Set
		End Property

		''' <summary>
		''' Returns an array with the modules found by using reflection.
		''' </summary>
		''' <returns>An array of <see cref="IModuleInfo"/>. An empty array is returned if no module is located.</returns>
		Public Function EnumerateModules() As IModuleInfo() Implements IModuleEnumerator.EnumerateModules
			Dim modules As List(Of ModuleInfo) = New List(Of ModuleInfo)()
			Dim directories As List(Of String) = New List(Of String)()

			directories.Add(BasePath)
			Do While directories.Count > 0
				Dim directory As String = directories(0)
				directories.Remove(directory)
				For Each filename As String In System.IO.Directory.GetFiles(directory, "*.dll")
					Dim assm As System.Reflection.Assembly = System.Reflection.Assembly.LoadFile(filename)
					If AssemblyHasModuleAttribute(assm) Then
						Dim mi As ModuleInfo = New ModuleInfo()
						mi.SetAssemblyFile(filename)
						modules.Add(mi)
					End If
				Next filename
				directories.AddRange(System.IO.Directory.GetDirectories(directory))
			Loop

			Return modules.ToArray()
		End Function

		Private Shared Function AssemblyHasModuleAttribute(ByVal assm As System.Reflection.Assembly) As Boolean
			Return assm.GetCustomAttributes(GetType(ModuleAttribute), False).Length > 0
		End Function
	End Class
End Namespace