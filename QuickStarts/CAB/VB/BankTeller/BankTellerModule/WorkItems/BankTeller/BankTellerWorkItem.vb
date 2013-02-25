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
Imports Microsoft.Practices.CompositeUI.Services
Imports BankTellerCommon
Imports Microsoft.Practices.ObjectBuilder
Imports Microsoft.Practices.CompositeUI.UIElements
Imports System.Windows.Forms
Imports Microsoft.Practices.CompositeUI.WinForms
Imports Microsoft.Practices.CompositeUI.Utility

Namespace BankTellerModule
	' The BankTellerWorkItem is the core work item of the module. Rather than
	' representing a single use case, it is the container of all the other
	' smaller work items in the system.
	Public Class BankTellerWorkItem : Inherits WorkItem : Implements IShowInShell
		Private queueItem As ToolStripMenuItem
		Private contentWorkspace As IWorkspace

		' The work item uses the state persistence service that's been registered
		' in the shell initialization
		Public ReadOnly Property PersistenceService() As IStatePersistenceService
			Get
				Return Services.Get(Of IStatePersistenceService)()
			End Get
		End Property

		' Here we populate the work item with some of our views and start showing
		' ourselves. The BankTellerMainView has smart part placeholders named
		' UserInfo and CustomerList; these are filled in at runtime with the
		' smart parts that are registered with those names. We chose to put a
		' UserInfoView in the "UserInfo" placeholder, and a CustomerQueueView
		' in the "CustomerList" placeholder.
		'
		' Note that order is important here. When we create the BankTellerMainView,
		' it is going to assume that the smart parts that it needs already exist
		' in the work item. Therefore, we create the smart parts first, and then
		' create the main view that contains them.
		Public Sub Show(ByVal sideBar As IWorkspace, ByVal content As IWorkspace) Implements IShowInShell.Show
			contentWorkspace = content

			'Needs to be named because it will be used in a placeholder
			Me.Items.AddNew(Of UserInfoView)("UserInfo")
			Dim sideBarView As SideBarView = Me.Items.AddNew(Of SideBarView)()

			AddMenuItems()

			sideBar.Show(sideBarView)
			Me.Activate()
		End Sub

		Private Sub AddMenuItems()
			If queueItem Is Nothing Then
				queueItem = New ToolStripMenuItem("Queue")
				UIExtensionSites(UIExtensionConstants.FILE).Add(queueItem)
				UIExtensionSites.RegisterSite(UIExtensionConstants.QUEUE, queueItem.DropDownItems)

				Dim acceptCustomer As ToolStripMenuItem = New ToolStripMenuItem("Accept Customer")
				acceptCustomer.ShortcutKeys = Keys.Control Or Keys.A
				UIExtensionSites(UIExtensionConstants.QUEUE).Add(acceptCustomer)

				Commands(CommandConstants.ACCEPT_CUSTOMER).AddInvoker(acceptCustomer, "Click")
			End If
		End Sub

		Private WriteOnly Property ShowQueueMenu() As Boolean
			Set(ByVal value As Boolean)
				If Not queueItem Is Nothing AndAlso queueItem.Visible <> Value Then
					queueItem.Visible = Value
				End If
			End Set
		End Property

		Protected Overrides Sub OnActivated()
			MyBase.OnActivated()

			ShowQueueMenu = True
		End Sub

		' When the user clicks on a customer in their customer queue, the
		' CustomerQueueController calls us to tell us to start working with
		' the customer.
		'
		' Editing a customer is self-contained in a work item (the CustomerWorkItem)
		' so we end up with one CustomerWorkItem contained in ourselves for
		' each customer that is being edited.
		Public Sub WorkWithCustomer(ByVal customer As Customer)
			' Construct a key to register the work item in ourselves
			Dim key As String = String.Format("Customer#{0}", customer.ID)

			' Have we already made the work item for this customer?
			' If so, return the existing one.
			Dim workItem As CustomerWorkItem = Me.Items.Get(Of CustomerWorkItem)(key)

			If workItem Is Nothing Then
				workItem = WorkItems.AddNew(Of CustomerWorkItem)(key)
				'Set ID before setting state.  State will be cleared if a new id is set.
				workItem.ID = key
				workItem.State(StateConstants.CUSTOMER) = customer

				' Ask the persistence service if we have a saved version of
				' this work item. If so, load it from persistence.
				If Not PersistenceService Is Nothing AndAlso PersistenceService.Contains(workItem.ID) Then
					workItem.Load()
				End If
			End If

			workItem.Show(contentWorkspace)
		End Sub

	End Class
End Namespace
