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
Public Class ControlCommandAdapterFixture
	<TestMethod()> _
	Public Sub DisabledCommandDisablesButShowsItem()
		Dim command As Command = New Command()
		Dim item As MockInvoker = New MockInvoker()
		Dim adapter As ControlCommandAdapter = New ControlCommandAdapter(item, "Event")
		command.AddCommandAdapter(adapter)

		command.Status = CommandStatus.Disabled

		Assert.IsFalse(item.Enabled)
		Assert.IsTrue(item.Visible)
	End Sub

	<TestMethod()> _
	Public Sub EnabledCommandEnablesAndShowsItem()
		Dim command As Command = New Command()
		Dim item As MockInvoker = New MockInvoker()
		Dim adapter As ControlCommandAdapter = New ControlCommandAdapter(item, "Event")
		command.AddCommandAdapter(adapter)
		command.Status = CommandStatus.Disabled

		command.Status = CommandStatus.Enabled

		Assert.IsTrue(item.Enabled)
		Assert.IsTrue(item.Visible)
	End Sub

	<TestMethod()> _
	Public Sub UnavailableCommandDisablesAndHidesItem()
		Dim command As Command = New Command()
		Dim item As MockInvoker = New MockInvoker()
		Dim adapter As ControlCommandAdapter = New ControlCommandAdapter(item, "Event")
		command.AddCommandAdapter(adapter)

		command.Status = CommandStatus.Unavailable

		Assert.IsFalse(item.Enabled)
		Assert.IsFalse(item.Visible)
	End Sub

	Private Class MockInvoker
		Inherits Control

		Public Event [Event] As EventHandler

		Public Sub DoInvoke()
			If Not EventEvent Is Nothing Then
				RaiseEvent [Event](Me, EventArgs.Empty)
			End If
		End Sub
	End Class
End Class

