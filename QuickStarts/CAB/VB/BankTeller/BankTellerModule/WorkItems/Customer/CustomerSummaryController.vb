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
Imports Microsoft.Practices.CompositeUI
Imports Microsoft.Practices.CompositeUI.Commands
Imports System
Imports Microsoft.Practices.CompositeUI.SmartParts
Imports Microsoft.Practices.CompositeUI.WinForms
Imports BankTellerCommon

Namespace BankTellerModule
	Public Class CustomerSummaryController : Inherits Controller
		' The CustomerSummaryController is the controller used by the CustomerSummaryView.
		' The summary view contains the pieces of the other views to display a customer,
		' and includes the Save button for the user to save their changes. The save
		' request is forwarded up to the work item.

		Public Shadows ReadOnly Property WorkItem() As CustomerWorkItem
			Get
				Return TryCast(MyBase.WorkItem, CustomerWorkItem)
			End Get
		End Property

		Public Sub Save()
			WorkItem.Save()
		End Sub

		<CommandHandler(CommandConstants.EDIT_CUSTOMER)> _
		Public Sub OnCustomerEdit(ByVal sender As Object, ByVal args As EventArgs)
			If Me.WorkItem.Status = WorkItemStatus.Active Then
				Dim tabWS As TabWorkspace = TryCast(WorkItem.Workspaces(CustomerWorkItem.CUSTOMERDETAIL_TABWORKSPACE), TabWorkspace)

				If Not tabWS Is Nothing Then
					tabWS.SelectedIndex = 0
				End If
			End If
		End Sub

	End Class
End Namespace
