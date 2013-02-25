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
Imports System.Reflection

Namespace Services
	''' <summary>
	''' Represents the information about a loaded module
	''' </summary>
	Public Class LoadedModuleInfo
		Private innerAssembly As System.Reflection.Assembly
		Private innerDependencies As List(Of String)
		Private innerName As String
		Private innerRoles As List(Of String)

		''' <summary>
		''' Initializes a new instance of the <see cref="LoadedModuleInfo"/> class with the given
		''' assembly, name, roles and dependencies.
		''' </summary>
		Public Sub New(ByVal anAssembly As System.Reflection.Assembly, ByVal aName As String, _
		 ByVal roles As IEnumerable(Of String), ByVal dependencies As IEnumerable(Of String))

			Me.innerAssembly = anAssembly
			Me.innerName = aName

			Me.innerRoles = New List(Of String)()
			If Not roles Is Nothing Then
				Me.innerRoles.AddRange(roles)
			End If

			Me.innerDependencies = New List(Of String)()
			If Not dependencies Is Nothing Then
				Me.innerDependencies.AddRange(dependencies)
			End If
		End Sub

		''' <summary>
		''' Initializes a new instance of the <see cref="LoadedModuleInfo"/> class, copying the existing object.
		''' </summary>
		Public Sub New(ByVal toCopy As LoadedModuleInfo)
			Me.innerAssembly = toCopy.Assembly
			Me.innerName = toCopy.Name
			Me.innerRoles = New List(Of String)(toCopy.Roles)
			Me.innerDependencies = New List(Of String)(toCopy.Dependencies)
		End Sub

		''' <summary>
		''' The assembly the module was loaded from.
		''' </summary>
		Public ReadOnly Property [Assembly]() As System.Reflection.Assembly
			Get
				Return innerAssembly
			End Get
		End Property

		''' <summary>
		''' The name of the assembly, if provided.
		''' </summary>
		Public ReadOnly Property Name() As String
			Get
				Return innerName
			End Get
		End Property

		''' <summary>
		''' The dependencies this module has, if any.
		''' </summary>
		Public ReadOnly Property Dependencies() As IList(Of String)
			Get
				Return innerDependencies.AsReadOnly()
			End Get
		End Property

		''' <summary>
		''' The allowed roles this module has, if any. If empty, then all roles are allowed.
		''' </summary>
		Public ReadOnly Property Roles() As IList(Of String)
			Get
				Return innerRoles.AsReadOnly()
			End Get
		End Property
	End Class
End Namespace
