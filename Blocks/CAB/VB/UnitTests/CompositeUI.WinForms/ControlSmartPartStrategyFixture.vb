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


Imports Microsoft.VisualStudio.TestTools.UnitTesting








Imports System
Imports System.Windows.Forms
Imports Microsoft.Practices.CompositeUI.SmartParts
Imports System.Collections.Generic
Imports Microsoft.Practices.ObjectBuilder


<TestClass()> _
Public Class ControlSmartPartStrategyFixture
	Private Shared control As Control
	Private Shared workItem As WorkItem
	Private Shared strat As ControlSmartPartStrategy
	Private Shared context As MockBuilderContext

	<TestInitialize()> _
	Public Sub Setup()
		control = New Control()
		workItem = New TestableRootWorkItem()
		strat = New ControlSmartPartStrategy()
		context = New MockBuilderContext(strat)
		context.Locator.Add(New DependencyResolutionLocatorKey(GetType(WorkItem), Nothing), workItem)
	End Sub

	<TestMethod()> _
	Public Sub AddingControlWithPlaceholderReplacesWSP()
		Dim placeholder As SmartPartPlaceholder = New SmartPartPlaceholder()
		placeholder.SmartPartName = "SP1"
		control.Controls.Add(placeholder)
		Dim smartPart1 As MockSmartPart = New MockSmartPart()

		workItem.Items.Add(smartPart1, "SP1")
		workItem.Items.Add(control)

		Assert.AreSame(smartPart1, placeholder.SmartPart)
	End Sub

	<TestMethod()> _
	Public Sub TestSmartPartHolderHavingNoSmartPartInContainerNoOp()
		Dim smartpartHolder As SmartPartPlaceholder = New SmartPartPlaceholder()
		smartpartHolder.SmartPartName = "SampleSmartPart"
		control.Controls.Add(smartpartHolder)

		workItem.Items.Add(control)

		Assert.IsNull(smartpartHolder.SmartPart)
	End Sub

	<TestMethod()> _
	Public Sub RemovingControlRemovesSmartParts()
		Dim originalCount As Integer = workItem.Items.Count
		Dim smartPart1 As MockSmartPart = New MockSmartPart()
		smartPart1.Name = "SmartPart1"
		Dim smartPart2 As MockSmartPart = New MockSmartPart()
		smartPart2.Name = "SmartPart2"
		smartPart1.Controls.Add(smartPart2)
		control.Controls.Add(smartPart1)
		workItem.Items.Add(control)

		Assert.AreEqual(3, workItem.Items.Count - originalCount)

		workItem.Items.Remove(control)

		Assert.AreEqual(0, workItem.Items.Count - originalCount)
	End Sub

	<TestMethod()> _
	Public Sub MonitorCallsRegisterWorkspace()
		Dim control As MockControlWithWorkspace = New MockControlWithWorkspace()

		workItem.Items.Add(control)

		Assert.AreEqual(control.Workspace, workItem.Workspaces(control.Workspace.Name))
	End Sub

	<TestMethod()> _
	Public Sub WorkspacesAreRegisteredWithName()
		Dim mockControl As MockControlWithWorkspace = New MockControlWithWorkspace()

		workItem.Items.Add(mockControl)

		Assert.AreEqual(mockControl.Workspace, workItem.Workspaces(mockControl.Workspace.Name))
	End Sub

	<TestMethod()> _
	Public Sub EmptyStringNameIsReplaceWhenAdded()
		Dim control As Control = New Control()
		Dim workspace As TabWorkspace = New TabWorkspace()
		control.Controls.Add(workspace)

		workItem.Items.Add(control)

		Dim tabWorkSpaces As ICollection(Of TabWorkspace) = workItem.Workspaces.FindByType(Of TabWorkspace)()

		Assert.IsNull(workItem.Workspaces(workspace.Name))
		Assert.IsTrue(tabWorkSpaces.Contains(workspace))
	End Sub


#Region "Supporting Classes"

	<SmartPart()> _
	Private Class MockSmartPart : Inherits UserControl
	End Class

	Private Class MockControlWithWorkspace : Inherits UserControl
		Private innerWorkspace As TabWorkspace
		Private button As Button

		Public ReadOnly Property Workspace() As TabWorkspace
			Get
				Return innerWorkspace
			End Get
		End Property

		Public Sub New()
			innerWorkspace = New TabWorkspace()
			innerWorkspace.Name = "TestName"

			button = New Button()

			Me.Controls.Add(innerWorkspace)
			Me.Controls.Add(button)
		End Sub
	End Class
#End Region
End Class

