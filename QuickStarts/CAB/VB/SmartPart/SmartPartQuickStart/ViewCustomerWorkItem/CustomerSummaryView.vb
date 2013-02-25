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
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data
Imports System.Text
Imports System.Windows.Forms
Imports Microsoft.Practices.CompositeUI.WinForms

Namespace SmartPartQuickStart.ViewCustomerWorkItem
	''' <summary>
	''' Summary view for customer information.
	''' </summary>
	Partial Public Class CustomerSummaryView
		Inherits TitledSmartPart

		''' <summary>
		''' Constructor
		''' </summary>
		Public Sub New()
			InitializeComponent()
		End Sub
	End Class
End Namespace
