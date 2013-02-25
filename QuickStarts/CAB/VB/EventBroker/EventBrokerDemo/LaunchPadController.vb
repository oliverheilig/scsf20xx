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
Imports System.ComponentModel
Imports Microsoft.Practices.CompositeUI
Imports Microsoft.Practices.CompositeUI.EventBroker
Imports Microsoft.Practices.CompositeUI.Utility

Namespace EventBrokerDemo
	Public Class LaunchPadController : Inherits Controller
		Private innerCustomers As List(Of String)

		<State("customers")> _
		Public WriteOnly Property Customers() As List(Of String)
			Set(ByVal value As List(Of String))
				innerCustomers = value
			End Set
		End Property

		' A sync key. As this QS shows some multi-threading behavior 
		' we need to synchronize some object access.
		Private syncRoot As Object = New Object()

		' Adds a customer to the state and fire the state changed event.
		Public Sub AddCustomer(ByVal customerId As String)
			SyncLock syncRoot
				innerCustomers.Add(customerId)
			End SyncLock
			State.RaiseStateChanged("customers", customerId)
		End Sub

		''' <summary>
		''' This is the subscription for the CustomerAdded event
		''' We're using the default scope, which is Global
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		<EventSubscription("topic://EventBrokerQuickStart/CustomerAdded")> _
		Public Sub OnCustomerAdded(ByVal sender As Object, ByVal e As DataEventArgs(Of String))
			Me.AddCustomer(e.Data)
		End Sub

		' Creates a new (generic) work item and sets up the customer list view and controller
		Public Sub ShowCustomerList()
			' Create a new WorkItem and add it as a child WorkItem
			Dim child As WorkItem = Me.WorkItem.WorkItems.AddNew(Of WorkItem)()

			' Forward the state to the child work item
			child.State("customers") = New List(Of String)()

			' Create view for the customer list and add them to the work item
			child.Items.AddNew(Of CustomerListView)().Show()
		End Sub
	End Class
End Namespace