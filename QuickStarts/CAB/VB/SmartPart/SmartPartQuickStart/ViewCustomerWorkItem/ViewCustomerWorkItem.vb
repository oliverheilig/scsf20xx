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
Imports Microsoft.Practices.CompositeUI
Imports Microsoft.Practices.CompositeUI.SmartParts
Imports Microsoft.Practices.CompositeUI.WinForms

Namespace SmartPartQuickStart.ViewCustomerWorkItem
	''' <summary>
	''' A WorkItem to handle the viewing of one customer.
	''' </summary>
	Public Class ViewCustomerWorkItem
		Inherits WorkItem

		Private tabView As CustomerTabView
		Private commentsView As CustomerCommentsView
		Private customerSummary As CustomerSummaryView

		''' <summary>
		''' Starts the workitem.
		''' </summary>
		''' <param name="workspace"></param>
		Public Overloads Sub Run(ByVal workspace As IWorkspace)
			'Create views to be used by workitem
			CreateSummaryView()
			CreateTabView()

			'Make the tabview visible in the workspace.
			workspace.Show(tabView)
		End Sub

		''' <summary>
		''' Shows the custoemr comments in a new tab.
		''' </summary>
		Public Sub ShowCustomerComments()
			'retrieve the tab workspace from the workitem.
			'"tabWorkspace1" is the name of the control.
			Dim tabbedSpace As IWorkspace = Workspaces("tabWorkspace1")

			If Not tabbedSpace Is Nothing Then
				If commentsView Is Nothing Then
					commentsView = Me.Items.AddNew(Of CustomerCommentsView)("CustomerCommentsView")
					Dim info As ISmartPartInfo = New SmartPartInfo()
					info.Title = "Comments"
					Me.RegisterSmartPartInfo(commentsView, info)
				End If
				'The "Show" of the tabworkspace creates 
				'a new tab and show the comments view.
				tabbedSpace.Show(commentsView)
			End If
		End Sub

		''' <summary>
		''' State that is inject in the workitem.
		''' The State is set this way so child items
		''' can get inject with the state.
		''' </summary>
		Public Property Customer() As Customer
			Get
				Return CType(State("Customer"), Customer)
			End Get
			Set(ByVal value As Customer)
				State("Customer") = value
			End Set
		End Property

		Private Sub CreateSummaryView()
			If customerSummary Is Nothing Then
				customerSummary = Me.Items.AddNew(Of CustomerSummaryView)("CustomerSummary")
			End If
		End Sub

		Private Sub CreateTabView()
			If tabView Is Nothing Then
				tabView = Me.Items.AddNew(Of CustomerTabView)("CustomerView")
			End If
		End Sub


	End Class
End Namespace
