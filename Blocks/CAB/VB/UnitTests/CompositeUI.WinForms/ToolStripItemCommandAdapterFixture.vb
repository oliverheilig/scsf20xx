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
Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Forms
Imports Microsoft.Practices.CompositeUI.Commands


<TestClass()> _
Public Class ToolStripItemCommandAdapterFixture
	Private Shared mainForm As Form
	Private Shared item As ToolStripItem

	<TestInitialize()> _
	Public Sub SetUp()
		mainForm = New Form()
		item = New ToolStripMenuItem()
		Dim strip As ToolStrip = New ToolStrip()
		strip.Items.Add(item)

		mainForm.Controls.Add(strip)
		mainForm.Show()
	End Sub

	<TestCleanup()> _
	Public Sub TearDown()
		mainForm.Close()
	End Sub

	<TestMethod()> _
	Public Sub DisabledCommandDisablesButShowsItem()
		Dim command As Command = New Command()
		Dim adapter As ToolStripItemCommandAdapter = New ToolStripItemCommandAdapter(item, "Click")
		command.AddCommandAdapter(adapter)

		command.Status = CommandStatus.Disabled

		Assert.IsFalse(item.Enabled)
		Assert.IsTrue(item.Visible)
	End Sub

	<TestMethod()> _
	Public Sub EnabledCommandEnablesAndShowsItem()
		Dim command As Command = New Command()
		Dim adapter As ToolStripItemCommandAdapter = New ToolStripItemCommandAdapter(item, "Click")
		command.AddCommandAdapter(adapter)
		command.Status = CommandStatus.Disabled

		command.Status = CommandStatus.Enabled

		Assert.IsTrue(item.Enabled)
		Assert.IsTrue(item.Visible)
	End Sub

	<TestMethod()> _
	Public Sub UnavailableCommandDisablesAndHidesItem()
		Dim command As Command = New Command()
		Dim adapter As ToolStripItemCommandAdapter = New ToolStripItemCommandAdapter(item, "Click")
		command.AddCommandAdapter(adapter)

		command.Status = CommandStatus.Unavailable

		Assert.IsFalse(item.Enabled)
		Assert.IsFalse(item.Visible)
	End Sub
End Class

