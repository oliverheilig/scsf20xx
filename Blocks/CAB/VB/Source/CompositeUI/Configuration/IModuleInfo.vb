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
Imports System.Collections.Generic

Namespace Configuration
	''' <summary>
	''' Exposes metadata describing a module.
	''' </summary>
	Public Interface IModuleInfo
		''' <summary>
		''' Gets the assembly file path to the module.
		''' </summary>
		ReadOnly Property AssemblyFile() As String

		''' <summary>
		''' Gets the update location string for the module.
		''' </summary>
		ReadOnly Property UpdateLocation() As String

		''' <summary>
		''' Gets a list of required roles to use the module.
		''' </summary>
		ReadOnly Property AllowedRoles() As IList(Of String)
	End Interface
End Namespace