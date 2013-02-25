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
	<SmartPart()> _
	Partial Public Class CustomerHeaderView : Inherits UserControl
		' The Customer state is stored in our parent work item

		<State()> _
		Public WriteOnly Property Customer() As Customer
			Set(ByVal value As Customer)
				innerCustomer = Value
			End Set
		End Property

		Private innerCustomer As Customer = Nothing

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub CustomerHeaderView_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
			If (Not DesignMode) Then
				customerBindingSource.Add(innerCustomer)
			End If
		End Sub
	End Class
End Namespace
