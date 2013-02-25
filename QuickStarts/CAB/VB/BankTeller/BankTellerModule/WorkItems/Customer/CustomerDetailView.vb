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
Imports Microsoft.Practices.CompositeUI.SmartParts
Imports BankTellerCommon
Imports Microsoft.Practices.CompositeUI.UIElements
Imports Microsoft.Practices.ObjectBuilder

Namespace BankTellerModule
	<SmartPart()> _
	Partial Public Class CustomerDetailView : Inherits UserControl
		Private innerCustomer As Customer
		Private innerParentWorkItem As WorkItem
		Private innerController As CustomerDetailController

		Public Sub New()
			InitializeComponent()
		End Sub

		<ServiceDependency()> _
		Public WriteOnly Property ParentWorkItem() As WorkItem
			Set(ByVal value As WorkItem)
				innerParentWorkItem = value
			End Set
		End Property

		' The Customer state is stored in our parent work item
		<State()> _
		Public WriteOnly Property Customer() As Customer
			Set(ByVal value As Customer)
				innerCustomer = value
			End Set
		End Property

		' We use our controller so we can show the comments page
		<CreateNew()> _
		Public WriteOnly Property Controller() As CustomerDetailController
			Set(ByVal value As CustomerDetailController)
				innerController = value
			End Set
		End Property

		Protected Overrides Sub OnLoad(ByVal e As EventArgs)
			MyBase.OnLoad(e)

			If Not innerCustomer Is Nothing Then
				Me.customerBindingSource.Add(innerCustomer)
			End If
		End Sub

		Private Sub OnShowComments(ByVal sender As Object, ByVal e As EventArgs) Handles showCommentsButton.Click
			innerController.ShowCustomerComments()
		End Sub
	End Class
End Namespace
