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
Imports Microsoft.Practices.CompositeUI
Imports Microsoft.Practices.CompositeUI.SmartParts

Namespace SmartPartQuickStart.ViewCustomerWorkItem
	''' <summary>
	''' Displays the comments associated with the customer.
	''' </summary>
	Partial Public Class CustomerCommentsView
		Inherits TitledSmartPart

		''' <summary>
		''' The customer State will be injected into the view.
		''' </summary>
		Private innerCustomer As Customer = Nothing

		<State()> _
		Public WriteOnly Property Customer() As Customer
			Set(ByVal value As Customer)
				innerCustomer = value
			End Set
		End Property

		''' <summary>
		''' Constructor
		''' </summary>
		Public Sub New()
			InitializeComponent()
		End Sub

		''' <summary>
		''' Adds the customer to the bindings source.
		''' </summary>
		''' <param name="e"></param>
		Protected Overrides Sub OnLoad(ByVal e As EventArgs)
			MyBase.OnLoad(e)

			If Not innerCustomer Is Nothing Then
				Me.customerBindingSource.Add(innerCustomer)
			End If
		End Sub
	End Class
End Namespace
