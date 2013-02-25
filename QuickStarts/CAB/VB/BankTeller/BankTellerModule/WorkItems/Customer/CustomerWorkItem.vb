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
Imports Microsoft.Practices.CompositeUI.UIElements
Imports Microsoft.Practices.CompositeUI.Utility
Imports Microsoft.Practices.CompositeUI.WinForms
Imports BankTellerCommon
Imports Microsoft.Practices.CompositeUI.EventBroker

Namespace BankTellerModule
	' The CustomerWorkItem represents the use case of editing a customer's data.
	' It contains the views necessary to edit a single customer. When it's time
	' to edit a customer, the work item's parent calls Run and passes the
	' workspace where the editing will take place.

	Public Class CustomerWorkItem : Inherits WorkItem
		Public Shared ReadOnly CUSTOMERDETAIL_TABWORKSPACE As String = "tabbedWorkspace1"

		Private editCustomerMenuItem As ToolStripMenuItem
		Private customerSummaryView As CustomerSummaryView
		Private commentsView As CustomerCommentsView
		Private addressLabel As ToolStripStatusLabel

		' This event is published to indicate that the module would like to
		' "update status". The only subscriber to this event today is the shell
		' which updates the status bar. These two components don't know anything
		' about one another, because they communicate indirectly via the
		' EventBroker. In reality, you can have any number of publishers and
		' any number of subscribers; in fact, other modules will undoubtedly
		' also publish status update events.
		<EventPublication("topic://BankShell/statusupdate", PublicationScope.Global)> _
		Public Event UpdateStatusTextEvent As EventHandler(Of DataEventArgs(Of String))

		Public Sub Show(ByVal parentWorkspace As IWorkspace)
			If customerSummaryView Is Nothing Then
				customerSummaryView = Items.AddNew(Of CustomerSummaryView)()
			End If
			parentWorkspace.Show(customerSummaryView)

			AddMenuItems()

			Dim customer As Customer = CType(State(StateConstants.CUSTOMER), Customer)
			OnStatusTextUpdate(String.Format("Editing {0}, {1}", customer.LastName, customer.FirstName))

			UpdateUserAddressLabel(customer)

			Me.Activate()

			' When activating, force focus on the first tab in the view.
			' Extensions may have added stuff at the end of the tab.
			customerSummaryView.FocusFirstTab()
		End Sub

		Private Sub UpdateUserAddressLabel(ByVal customer As Customer)
			If addressLabel Is Nothing Then
				addressLabel = New ToolStripStatusLabel()
				UIExtensionSites(UIExtensionConstants.MAINSTATUS).Add(addressLabel)
				addressLabel.Text = customer.Address1
			End If
		End Sub

		Private Sub AddMenuItems()
			If editCustomerMenuItem Is Nothing Then
				editCustomerMenuItem = New ToolStripMenuItem("Edit")
				UIExtensionSites(My.Resources.CustomerMenuExtensionSite).Add(editCustomerMenuItem)

				Commands(CommandConstants.EDIT_CUSTOMER).AddInvoker(editCustomerMenuItem, "Click")
				Commands(CommandConstants.CUSTOMER_MOUSEOVER).AddInvoker(customerSummaryView, "MouseHover")
			End If
		End Sub

		Private Sub SetUIElementVisibility(ByVal visible As Boolean)
			If Not editCustomerMenuItem Is Nothing Then
				editCustomerMenuItem.Visible = visible
			End If

			If Not addressLabel Is Nothing Then
				addressLabel.Visible = visible
			End If
		End Sub

		' We watch for when we are activated (i.e., shown to
		' be worked on), we want to fire a status update event and show ourselves
		' in the provided workspace.
		Protected Overrides Sub OnActivated()
			MyBase.OnActivated()

			SetUIElementVisibility(True)
		End Sub

		Protected Overrides Sub OnDeactivated()
			MyBase.OnDeactivated()

			SetUIElementVisibility(False)
		End Sub

		Protected Overridable Sub OnStatusTextUpdate(ByVal newText As String)
			If Not UpdateStatusTextEventEvent Is Nothing Then
				RaiseEvent UpdateStatusTextEvent(Me, New DataEventArgs(Of String)(newText))
			End If
		End Sub

		' This is called by CustomerDetailController when the user has indicated
		' that they want to show the comments. We dynamically create the comments
		' view and add it to our tab workspace.
		Public Sub ShowCustomerComments()
			CreateCommentsView()

			Dim ws As IWorkspace = Workspaces(CUSTOMERDETAIL_TABWORKSPACE)
			If Not ws Is Nothing Then
				ws.Show(commentsView)
			End If
		End Sub

		Private Sub CreateCommentsView()
			If commentsView Is Nothing Then
				commentsView = Items.AddNew(Of CustomerCommentsView)()
			End If
			Dim info As ISmartPartInfo = New TabSmartPartInfo()
			info.Title = "Comments"
			RegisterSmartPartInfo(commentsView, info)
		End Sub

		<CommandHandler(CommandConstants.CUSTOMER_MOUSEOVER)> _
		Public Sub OnCustomerEdit(ByVal sender As Object, ByVal args As EventArgs)
			If Status = WorkItemStatus.Active Then
				Dim form As Form = customerSummaryView.ParentForm

				Dim tooltipText As String = "This is customer work item " & Me.ID
				Dim toolTip As ToolTip = New ToolTip()
				toolTip.IsBalloon = True
				toolTip.Show(tooltipText, form, form.Size.Width - 30, 30, 3000)
			End If
		End Sub

	End Class
End Namespace