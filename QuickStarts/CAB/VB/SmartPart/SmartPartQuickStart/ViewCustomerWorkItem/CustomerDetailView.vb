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
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data
Imports System.Text
Imports System.Windows.Forms
Imports Microsoft.Practices.CompositeUI
Imports Microsoft.Practices.ObjectBuilder

Namespace SmartPartQuickStart.ViewCustomerWorkItem
	''' <summary>
	''' Displays the details of the customer.
	''' </summary>
	Partial Public Class CustomerDetailView
		Inherits TitledSmartPart

		''' <summary>
		''' The customer State will be injected into the view.
		''' </summary>
		Private innerCustomer As Customer = Nothing

		<State("Customer")> _
		Public WriteOnly Property Customer() As Customer
			Set(ByVal value As Customer)
				innerCustomer = value
			End Set
		End Property

		''' <summary>
		''' The controller will be injected into the view.
		''' </summary>
		Private innerController As CustomerController = Nothing

		<CreateNew()> _
		Public WriteOnly Property Controller() As CustomerController
			Set(ByVal value As CustomerController)
				innerController = value
			End Set
		End Property

		''' <summary>
		''' Constructor
		''' </summary>
		Public Sub New()
			InitializeComponent()
		End Sub

		''' <summary>
		''' Adds the customer to the binding source.
		''' </summary>
		''' <param name="e"></param>
		Protected Overrides Sub OnLoad(ByVal e As EventArgs)
			MyBase.OnLoad(e)

			If Not Me.innerCustomer Is Nothing Then
				Me.customerBindingSource.Add(Me.innerCustomer)
			End If
		End Sub

		Private Sub commentsButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles commentsButton.Click
			'Calls to the controller to show the comments associated with
			'the customer.
			innerController.ShowCustomerComments()
		End Sub
	End Class
End Namespace
