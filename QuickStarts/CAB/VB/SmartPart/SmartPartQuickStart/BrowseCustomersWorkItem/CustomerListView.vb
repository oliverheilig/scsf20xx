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

Namespace SmartPartQuickStart.BrowseCustomersWorkItem
	''' <summary>
	''' Provides a list fo customers.
	''' </summary>
	Partial Public Class CustomerListView
		Inherits TitledSmartPart

		''' <summary>
		''' The controller will get injected into the smartpart
		''' when it is added to the workitem.
		''' </summary>
		Private innerController As CustomersController = Nothing

		<CreateNew()> _
		Public WriteOnly Property Controller() As CustomersController
			Set(ByVal value As CustomersController)
				innerController = value
			End Set
		End Property

		''' <summary>
		''' The customer list State will be injected into the view.
		''' </summary>
		Private innerCustomer As List(Of Customer) = Nothing

		<State()> _
		Public WriteOnly Property Customers() As List(Of Customer)
			Set(ByVal value As List(Of Customer))
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
		''' Sets the datasource for the listbox.
		''' Wires up the SelectedIndexChanged event.
		''' </summary>
		''' <param name="e"></param>
		Protected Overrides Sub OnLoad(ByVal e As EventArgs)
			If DesignMode = False Then
				innerController.PopulateCustomersData()

				customerListBox.DataSource = innerCustomer
				customerListBox.DisplayMember = "FullName"

				AddHandler customerListBox.SelectedIndexChanged, AddressOf customerListBox_SelectedIndexChanged
			End If
		End Sub

		Private Sub customerListBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
			'Call the controller to show customer details.
			innerController.ShowCustomerDetails(CType(Me.customerListBox.SelectedValue, Customer))
		End Sub

	End Class
End Namespace
