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
Imports System.Windows.Forms
Imports Microsoft.Practices.CompositeUI
Imports Microsoft.Practices.ObjectBuilder

Namespace EventBrokerDemo
	Partial Public Class LaunchPadForm
		Inherits Form

		' Specify we need the LaunchPadController
		Private innerController As LaunchPadController

		<CreateNew()> _
		Public WriteOnly Property Controller() As LaunchPadController
			Set(ByVal value As LaunchPadController)
				innerController = value
			End Set
		End Property

		' Specify we need the state object under the "customers" key
		Private innerCustomers As List(Of String)

		<State("customers")> _
		Public WriteOnly Property Customers() As List(Of String)
			Set(ByVal value As List(Of String))
				innerCustomers = value
			End Set
		End Property

		Public Sub New()
			InitializeComponent()
		End Sub

		' At this point, the form is sited into the work item, and our controller and view
		' are already injected by CAB. So we can hook the State changed event.
		Protected Overrides Sub OnLoad(ByVal e As EventArgs)
			AddHandler innerController.State.StateChanged, AddressOf State_StateChanged
		End Sub

		' Update the view accordingly to the changes on the model (the state)
		Private Sub State_StateChanged(ByVal sender As Object, ByVal e As StateChangedEventArgs)
			Me.lstGlobalCustomers.DataSource = Nothing
			Me.lstGlobalCustomers.DataSource = innerCustomers
		End Sub

		' Here we handle view events, forwarding the needed calls to our controller.

		Private Sub btnFireCustomerChange_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFireCustomerChange.Click
			If txtCustomerId.Text.Length > 0 Then
				innerController.AddCustomer(txtCustomerId.Text)
				txtCustomerId.Text = ""
			End If
			txtCustomerId.Focus()
		End Sub

		Private Sub btnNewCustomerList_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNewCustomerList.Click
			innerController.ShowCustomerList()
		End Sub

		Private Sub LaunchPadForm_Load(ByVal sender As Object, ByVal e As EventArgs)

		End Sub
	End Class
End Namespace