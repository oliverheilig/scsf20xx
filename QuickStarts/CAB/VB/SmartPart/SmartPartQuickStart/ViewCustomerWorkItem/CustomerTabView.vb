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
Imports System.Windows.Forms
Imports Microsoft.Practices.CompositeUI.SmartParts

Namespace SmartPartQuickStart.ViewCustomerWorkItem
	''' <summary>
	''' The SmartPart attribute tells the SmartPartMonitor to add this view 
	''' to the WorkItem.
	''' </summary>
	<SmartPart()> _
	Partial Public Class CustomerTabView
		Inherits UserControl

		''' <summary>
		''' Constructor
		''' </summary>
		Public Sub New()
			InitializeComponent()
		End Sub

	End Class
End Namespace
