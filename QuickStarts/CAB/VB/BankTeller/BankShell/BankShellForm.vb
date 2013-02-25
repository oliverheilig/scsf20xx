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
Imports Microsoft.Practices.CompositeUI.Utility
Imports Microsoft.Practices.CompositeUI.Services
Imports BankTellerCommon
Imports Microsoft.Practices.CompositeUI.EventBroker
Imports Microsoft.Practices.ObjectBuilder

Namespace BankShell
	' The shell represents the main window of the application. The shell
	' provides a menu, status bar, and a single workspace for the rest of
	' the window. Modules will use the workspace to display their role-
	' specific user interface.
	'
	' Core menu items (like Exit and About) are handled in the shell. We
	' ask for them to be dispatched on the user interface thread so that
	' we can directly call Form methods without using Invoke.
	'
	' We listen for status update events. Modules can fire status update
	' events to tell us to change the status bar.
	Partial Public Class BankShellForm : Inherits Form
		Private workItem As WorkItem
		Private workItemTypeCatalog As IWorkItemTypeCatalogService

		Public Sub New()
			InitializeComponent()
		End Sub

		''' <summary>
		''' This constructor will be called by ObjectBuilder when the Form is created
		''' by calling WorkItem.Items.AddNew.
		''' </summary>
		<InjectionConstructor()> _
		Public Sub New(ByVal workItem As WorkItem, ByVal workItemTypeCatalog As IWorkItemTypeCatalogService)
			Me.New()
			Me.workItem = workItem
			Me.workItemTypeCatalog = workItemTypeCatalog
		End Sub

		<CommandHandler("FileExit")> _
		Public Sub OnFileExit(ByVal sender As Object, ByVal e As EventArgs)
			Close()
		End Sub

		<CommandHandler("HelpAbout")> _
		Public Sub OnHelpAbout(ByVal sender As Object, ByVal e As EventArgs)
			MessageBox.Show(Me, "Bank Teller QuickStart Version 1.0", "About", MessageBoxButtons.OK, MessageBoxIcon.Information)
		End Sub

		<EventSubscription("topic://BankShell/statusupdate", Thread:=ThreadOption.UserInterface)> _
		Public Sub OnStatusUpdate(ByVal sender As Object, ByVal e As DataEventArgs(Of String))
			toolStripStatusLabel1.Text = e.Data
		End Sub

	End Class
End Namespace