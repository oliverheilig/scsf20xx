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
Imports Microsoft.Practices.ObjectBuilder


''' <summary>
''' A base class for controllers and presenters that provides access to the <see cref="WorkItem"/>
''' and <see cref="State"/>.
''' </summary>
Public Class Controller
	Private innerWorkItem As WorkItem

	''' <summary>
	''' Initializes a new instance of the <see cref="Controller"/> class.
	''' </summary>
	Public Sub New()
	End Sub

	''' <summary>
	''' Gets the current work item where the controller lives.
	''' </summary>
	<Dependency(NotPresentBehavior:=NotPresentBehavior.ReturnNull)> _
	Public Property WorkItem() As WorkItem
		Get
			Return innerWorkItem
		End Get
		Set(ByVal value As WorkItem)
			innerWorkItem = value
		End Set
	End Property

	''' <summary>
	''' Gets the state associated with the current <see cref="WorkItem"/>.
	''' </summary>
	Public ReadOnly Property State() As State
		Get
			If WorkItem Is Nothing Then
				Return Nothing
			Else
				Return WorkItem.State
			End If
		End Get
	End Property
End Class
