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


''' <summary>
''' Indicates that the assembly should be considered a named module using the
''' provided name.
''' </summary>
<AttributeUsage(AttributeTargets.Assembly, AllowMultiple:=False)> _
Public NotInheritable Class ModuleAttribute
	Inherits Attribute

	Private innerName As String

	''' <summary>
	''' Creates a new instance of the <see cref="ModuleAttribute"/> class using the
	''' provided module name.
	''' </summary>
	''' <param name="name">The name of the module.</param>
	Public Sub New(ByVal aName As String)
		Me.innerName = aName
	End Sub

	''' <summary>
	''' The name of the module.
	''' </summary>
	Public ReadOnly Property Name() As String
		Get
			Return innerName
		End Get
	End Property
End Class
