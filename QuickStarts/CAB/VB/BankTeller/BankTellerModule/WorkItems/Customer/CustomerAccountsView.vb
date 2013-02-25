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
Imports Microsoft.Practices.CompositeUI.SmartParts
Imports Microsoft.Practices.CompositeUI
Imports BankTellerCommon

Namespace BankTellerModule
	<SmartPart()> _
	Partial Public Class CustomerAccountsView : Inherits UserControl
		Private innerCustomer As Customer
		Private innerAccountService As CustomerAccountService

		' The Customer state is stored in our parent work item
		<State()> _
		Public WriteOnly Property Customer() As Customer
			Set(ByVal value As Customer)
				innerCustomer = value
			End Set
		End Property

		' Make sure our required CustomerAccountService is available
		<ServiceDependency()> _
		Public WriteOnly Property AccountService() As CustomerAccountService
			Set(ByVal value As CustomerAccountService)
				innerAccountService = value
			End Set
		End Property

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub CustomerAccountsView_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
			If Not innerCustomer Is Nothing Then
				dataGridView1.DataSource = innerAccountService.GetByCustomerID(innerCustomer.ID)
			End If
		End Sub
	End Class
End Namespace
