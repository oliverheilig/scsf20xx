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
Imports System.Configuration
Imports System.Windows.Forms
Imports Microsoft.Practices.CompositeUI
Imports Microsoft.Practices.CompositeUI.UIElements
Imports Microsoft.Practices.CompositeUI.SmartParts
Imports Microsoft.Practices.CompositeUI.Services
Imports Microsoft.Practices.ObjectBuilder
Imports BankTellerCommon

Namespace BankTellerModule
	' This is the initialization class for the Module. Any classes in the assembly that
	' derive from Microsoft.Practices.CompositeUI.ModuleInit will automatically
	' be created and called for initialization.

	Public Class BankTellerModuleInit : Inherits ModuleInit
		Private workItem As WorkItem

		<InjectionConstructor()> _
		Public Sub New(<ServiceDependency()> ByVal workItem As WorkItem)
			Me.workItem = workItem
		End Sub

		Public Overrides Sub Load()
			AddCustomerMenuItem()

			'Retrieve well known workspaces
			Dim sideBarWorkspace As IWorkspace = workItem.Workspaces(WorkspacesConstants.SHELL_SIDEBAR)
			Dim contentWorkspace As IWorkspace = workItem.Workspaces(WorkspacesConstants.SHELL_CONTENT)

			Dim bankTellerWorkItem As BankTellerWorkItem = workItem.WorkItems.AddNew(Of BankTellerWorkItem)()
			bankTellerWorkItem.Show(sideBarWorkspace, contentWorkspace)
		End Sub

		Private Sub AddCustomerMenuItem()
			Dim customerItem As ToolStripMenuItem = New ToolStripMenuItem("Customer")
			workItem.UIExtensionSites(UIExtensionConstants.FILE).Add(customerItem)
			workItem.UIExtensionSites.RegisterSite(My.Resources.CustomerMenuExtensionSite, customerItem.DropDownItems)
		End Sub
	End Class
End Namespace
