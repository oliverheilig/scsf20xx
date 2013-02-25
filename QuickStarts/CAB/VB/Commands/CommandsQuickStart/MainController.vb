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
Imports System.Windows.Forms
Imports Microsoft.Practices.CompositeUI
Imports Microsoft.Practices.CompositeUI.Commands

' This is the controller for the main work item
' and holds all commands handlers for this sample quickstart.
Public Class MainController : Inherits Controller
	<CommandHandler("ShowCustomer")> _
	Public Sub ShowCustomerHandler(ByVal sender As Object, ByVal e As EventArgs)
		MessageBox.Show("Show Customer")
	End Sub

	<CommandHandler("EnableShowCustomer")> _
	Public Sub EnableShowCustomerHandler(ByVal sender As Object, ByVal e As EventArgs)
		WorkItem.Commands("ShowCustomer").Status = CommandStatus.Enabled
	End Sub

	<CommandHandler("DisableShowCustomer")> _
	Public Sub DisableShowCustomer(ByVal sender As Object, ByVal e As EventArgs)
		WorkItem.Commands("ShowCustomer").Status = CommandStatus.Disabled
	End Sub

	<CommandHandler("HideShowCustomer")> _
	Public Sub HideShowCustomer(ByVal sender As Object, ByVal e As EventArgs)
		WorkItem.Commands("ShowCustomer").Status = CommandStatus.Unavailable
	End Sub
End Class
