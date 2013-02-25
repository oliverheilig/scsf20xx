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
Imports Microsoft.Practices.CompositeUI.Utility

Namespace Configuration
	''' <summary>
	''' This is the default implementation of <see cref="IModuleInfo"/>.
	''' </summary>
	Public Class ModuleInfo
		Implements IModuleInfo

		Private innerAssembly As String
		Private innerUpdateLocation As String
		Private innerAllowedRoles As List(Of String) = New List(Of String)()

		''' <summary>
		''' Initializes an instance of the <see cref="ModuleInfo"/> class.
		''' </summary>
		Public Sub New()
		End Sub

		''' <summary>
		''' Initializes an instance of the <see cref="ModuleInfo"/> class.
		''' </summary>
		''' <param name="assemblyFile">The filename of an assembly.</param>
		Public Sub New(ByVal assemblyFile As String)
			Guard.ArgumentNotNullOrEmptyString(assemblyFile, "assemblyFile")

			Me.innerAssembly = assemblyFile
		End Sub

		''' <summary>
		''' Initializes an instance of the <see cref="ModuleInfo"/> class.
		''' </summary>
		''' <param name="assembly">The assembly.</param>
		Public Sub New(ByVal anAssembly As System.Reflection.Assembly)
			Guard.ArgumentNotNull(anAssembly, "assembly")

			Me.innerAssembly = anAssembly.CodeBase.Replace("file:///", "").Replace("/"c, "\"c)
		End Sub

		''' <summary>
		''' Gets the assembly file path to the module.
		''' </summary>
		Public ReadOnly Property AssemblyFile() As String Implements IModuleInfo.AssemblyFile
			Get
				Return innerAssembly
			End Get
		End Property

		''' <summary>
		''' Sets the assembly file path to the module.
		''' </summary>
		Public Sub SetAssemblyFile(ByVal value As String)
			innerAssembly = value
		End Sub

		''' <summary>
		''' Gets the update location for the module.
		''' </summary>
		Public ReadOnly Property UpdateLocation() As String Implements IModuleInfo.UpdateLocation
			Get
				Return innerUpdateLocation
			End Get
		End Property

		''' <summary>
		''' Sets the update location for the module.
		''' </summary>
		Public Sub SetUpdateLocation(ByVal value As String)
			innerUpdateLocation = value
		End Sub

		''' <summary>
		''' Gets the list of required roles to use the module.
		''' </summary>
		Public ReadOnly Property AllowedRoles() As IList(Of String) Implements IModuleInfo.AllowedRoles
			Get
				Return innerAllowedRoles.AsReadOnly()
			End Get
		End Property

		''' <summary>
		''' Adds roles to the allowed roles for the module.
		''' </summary>
		''' <param name="roles">A string list of roles to be added.</param>
		Public Sub AddRoles(ByVal ParamArray roles As String())
			Guard.ArgumentNotNull(roles, "roles")

			For Each role As String In roles
				If Not role Is Nothing AndAlso role.Length > 0 Then
					innerAllowedRoles.Add(role)
				End If
			Next role
		End Sub

		''' <summary>
		''' Clears the list of allowed roles.
		''' </summary>
		Public Sub ClearRoles()
			innerAllowedRoles.Clear()
		End Sub
	End Class
End Namespace