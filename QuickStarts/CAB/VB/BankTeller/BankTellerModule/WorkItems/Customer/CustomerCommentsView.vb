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

Namespace BankTellerModule
	' This SmartPart implements ISmartPartInfoProvider because it is displayed
	' dynamically in its tabbed workspace. The SmartPartInfo lets us tell the
	' tabbed workspace what the name of our tab should be.

	<SmartPart()> _
	Partial Public Class CustomerCommentsView : Inherits UserControl
		' The Customer state is stored in our parent work item

		Private innerCustomer As Customer = Nothing

		<State()> _
		Public WriteOnly Property Customer() As Customer
			Set(ByVal value As Customer)
				innerCustomer = value
			End Set
		End Property

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub CustomerCommentsView_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
			If (Not DesignMode) Then
				customerBindingSource.Add(innerCustomer)
			End If
		End Sub
	End Class
End Namespace
