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
Imports Microsoft.Practices.CompositeUI.SmartParts
Imports Microsoft.Practices.CompositeUI
Imports Microsoft.Practices.CompositeUI.Commands
Imports Microsoft.Practices.CompositeUI.UIElements
Imports BankTellerCommon
Imports Microsoft.Practices.ObjectBuilder

Namespace BankTellerModule
	<SmartPart()> _
	Partial Public Class CustomerSummaryView : Inherits UserControl
		Private innerController As CustomerSummaryController

		Public Sub New()
			InitializeComponent()
		End Sub

		<CreateNew()> _
		Public WriteOnly Property Controller() As CustomerSummaryController
			Set(ByVal value As CustomerSummaryController)
				innerController = value
			End Set
		End Property

		Private Sub OnSave(ByVal sender As Object, ByVal e As EventArgs) Handles SaveButton.Click
			innerController.Save()
		End Sub

		Protected Overrides Sub OnLoad(ByVal e As EventArgs)
			MyBase.OnLoad(e)

			innerController.WorkItem.UIExtensionSites.RegisterSite(UIExtensionConstants.CUSTOMERCONTEXT, Me.customerContextMenu)
		End Sub

		Friend Sub FocusFirstTab()
			Me.tabbedWorkspace1.SelectedTab = Me.tabbedWorkspace1.TabPages(0)
		End Sub
	End Class
End Namespace
