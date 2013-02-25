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
Imports Microsoft.Practices.CompositeUI
Imports Microsoft.Practices.CompositeUI.SmartParts
Imports Microsoft.Practices.ObjectBuilder

Namespace SmartPartQuickStart.BrowseCustomersWorkItem
	''' <summary>
	''' Browse Cusotmer WorkItem.
	''' </summary>
	Public Class BrowseCustomersWorkItem
		Inherits WorkItem

		Private customerMain As CustomerMain

		''' <summary>
		''' Starts the workitem.  You can put any logic 
		''' here that makes the workitem work as desired.
		''' </summary>
		Protected Overrides Sub OnRunStarted()
			MyBase.OnRunStarted()

			'Create the main view that will be shown on the main workspace
			'of the MainForm.
			Dim workspace As IWorkspace = Me.Workspaces("MainFormWorkspace")
			customerMain = Me.Items.AddNew(Of CustomerMain)("CustomerMain")
			workspace.Show(customerMain)
		End Sub

		''' <summary>
		''' Shows the details for a customer.
		''' </summary>
		''' <param name="customer"></param>
		Public Sub ShowCustomerDetails(ByVal customer As Customer)
			'Set the state so the child workitem gets injected with it.
			State("Customer") = customer

			'Create a key for the workitem so we can check
			'later if the workitem has already been created.
			Dim key As String = customer.Id & "Details"

			Dim customerWorkItem As ViewCustomerWorkItem.ViewCustomerWorkItem = Items.Get(Of ViewCustomerWorkItem.ViewCustomerWorkItem)(key)

			If customerWorkItem Is Nothing Then
				customerWorkItem = Me.Items.AddNew(Of ViewCustomerWorkItem.ViewCustomerWorkItem)(key)
				customerWorkItem.Customer = CType(State("Customer"), Customer)
			End If

			'Run the child workitem.
			customerWorkItem.Run(customerMain.customersDeckedWorkspace)
		End Sub
	End Class
End Namespace
