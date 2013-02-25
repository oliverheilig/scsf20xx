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
Imports Microsoft.Practices.CompositeUI.EventBroker

Namespace EventBrokerDemo
	Partial Public Class CustomerListView
		Inherits Form

		Private innerCustomers As List(Of String)

		<State("customers")> _
		Public WriteOnly Property Customers() As List(Of String)
			Set(ByVal value As List(Of String))
				innerCustomers = value
			End Set
		End Property

		Private innerController As CustomerListController

		<CreateNew()> _
		Public WriteOnly Property Controller() As CustomerListController
			Set(ByVal value As CustomerListController)
				innerController = value
			End Set
		End Property

		Public Sub New()
			InitializeComponent()
		End Sub

		' Once sited, we can access the injected dependencies
		' so we can add our handlers to the state change events
		Protected Overrides Sub OnLoad(ByVal e As EventArgs)
			AddHandler innerController.ParentState.StateChanged, AddressOf ParentState_StateChanged
			AddHandler innerController.State.StateChanged, AddressOf State_StateChanged
			lstGlobalCustomers.DataSource = Me.innerController.ParentState("customers")
		End Sub

		' These two EventSubscriptions handle the background process start and stop, updating the
		' UI accordingly by enabling or disabling the Cancel button.

		<EventSubscription("topic://EventBrokerQuickStart/ProcessCompleted", Thread:=ThreadOption.UserInterface)> _
		Public Sub OnControllerProcessCompleted(ByVal sender As Object, ByVal args As EventArgs)
			btnCancelProcess.Enabled = False
		End Sub

		<EventSubscription("topic://EventBrokerQuickStart/StartProcess", Thread:=ThreadOption.UserInterface)> _
		Public Sub OnControllerStartProcess(ByVal sender As Object, ByVal args As DictionaryEventArgs)
			btnCancelProcess.Enabled = True
		End Sub

		' Here we handle the UI elements events and call the appropiate
		' controller methods.

		Private Sub btnAddLocalCustomer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddLocalCustomer.Click
			If txtCustomerId.Text.Length > 0 Then
				Me.innerController.AddLocalCustomer(txtCustomerId.Text)
				txtCustomerId.Text = ""
			End If
			txtCustomerId.Focus()
		End Sub

		Private Sub btnAddGlobalCustomer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddGlobalCustomer.Click
			If txtCustomerId.Text.Length > 0 Then
				Me.innerController.AddGlobalCustomer(txtCustomerId.Text)
				txtCustomerId.Text = ""
			End If
			txtCustomerId.Focus()
		End Sub

		Private Sub btnProcessLocal_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnProcessLocal.Click
			Me.innerController.ProcessLocalCustomers()
		End Sub

		Private Sub btnCancelProcess_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelProcess.Click
			Me.innerController.CancelProcess()
		End Sub

		' These are our handlers to the state change events
		' where we update the view accordingly.

		Private Sub ParentState_StateChanged(ByVal sender As Object, ByVal e As StateChangedEventArgs)
			lstGlobalCustomers.DataSource = Nothing
			lstGlobalCustomers.DataSource = Me.innerController.ParentState("customers")
		End Sub

		Private Sub State_StateChanged(ByVal sender As Object, ByVal e As StateChangedEventArgs)
			lstLocalCustomers.DataSource = Nothing
			lstLocalCustomers.DataSource = Me.innerCustomers
		End Sub
	End Class
End Namespace