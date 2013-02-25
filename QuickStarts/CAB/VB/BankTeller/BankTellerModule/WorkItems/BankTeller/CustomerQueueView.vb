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
Imports Microsoft.Practices.CompositeUI.SmartParts
Imports BankTellerCommon
Imports System.Collections.Generic
Imports Microsoft.Practices.CompositeUI.Utility
Imports Microsoft.Practices.ObjectBuilder

Namespace BankTellerModule
	<SmartPart()> _
	Partial Public Class CustomerQueueView : Inherits UserControl
		Private innerMyController As CustomerQueueController

		' We need our controller, so that we can work with a customer when
		' the user clicks on one
		<CreateNew()> _
		Public WriteOnly Property MyController() As CustomerQueueController
			Set(ByVal value As CustomerQueueController)
				innerMyController = value
			End Set
		End Property

		Public Sub New()
			InitializeComponent()
		End Sub

		' In addition to offering a button to get the next customer from the
		' queue, we also support a menu item to do the same thing. Because
		' the signature for both methods (button click handle, command handler)
		' we use this single method to do both.

		<CommandHandler(CommandConstants.ACCEPT_CUSTOMER)> _
		Public Sub OnAcceptCustomer(ByVal sender As Object, ByVal e As EventArgs) Handles btnNextCustomer.Click
			Dim customer As Customer = innerMyController.GetNextCustomerInQueue()

			If customer Is Nothing Then
				MessageBox.Show(Me, "There are no more customers in the queue.", "Bank Teller", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Return
			End If

			listCustomers.Items.Add(customer)
		End Sub

		Private Sub OnCustomerSelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles listCustomers.SelectedIndexChanged
			Dim customer As Customer = TryCast(listCustomers.SelectedItem, Customer)

			If Not customer Is Nothing Then
				innerMyController.WorkWithCustomer(customer)
			End If
		End Sub
	End Class
End Namespace
