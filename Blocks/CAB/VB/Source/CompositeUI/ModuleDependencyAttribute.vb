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
''' Indicates that this assembly has a dependency on another named module.
''' The other named module will be loaded before this module. Can be used
''' multiple times to indicate multiple dependencies.
''' </summary>
<AttributeUsage(AttributeTargets.Assembly, AllowMultiple:=True)> _
Public NotInheritable Class ModuleDependencyAttribute
	Inherits Attribute

	Private innerName As String

	''' <summary>
	''' Creates a new instance of the <see cref="ModuleDependencyAttribute"/> class
	''' using the provided module name as a dependency.
	''' </summary>
	''' <param name="name">The name of the module which this module depends on.</param>
	Public Sub New(ByVal aName As String)
		Me.innerName = aName
	End Sub

	''' <summary>
	''' The name of the module which this module depends on.
	''' </summary>
	Public Property Name() As String
		Get
			Return innerName
		End Get
		Set(ByVal value As String)
			innerName = value
		End Set
	End Property
End Class
