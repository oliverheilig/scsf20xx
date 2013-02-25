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
Imports System.ComponentModel
Imports System.Reflection


<TestClass()> _
Public Class DeckWorkspaceFixture
	Private Shared workspace As DeckWorkspace
	Private Shared smartPart As MockSmartPart

	<TestInitialize()> _
	Public Sub Setup()
		workspace = New DeckWorkspace()
		smartPart = New MockSmartPart()
	End Sub

#Region "Show"

	<TestMethod()> _
	Public Sub ShowMakesControlVisible()
		smartPart.Visible = False

		workspace.Show(smartPart)

		Assert.IsTrue(smartPart.Visible)
	End Sub

	<TestMethod()> _
	Public Sub ShowHidesPreviouslyVisibleControl()
		Dim c2 As MockSmartPart = New MockSmartPart()

		workspace.Show(smartPart)
		workspace.Show(c2)

		Assert.AreSame(smartPart, workspace.Controls(1), "Hiden control didn't go to the bottom of the deck")
		Assert.AreSame(c2, workspace.ActiveSmartPart)
	End Sub

	<TestMethod()> _
	Public Sub ShowSetsDockFill()
		workspace.Show(smartPart)

		Assert.AreEqual(DockStyle.Fill, smartPart.Dock)
	End Sub

	<TestMethod()> _
	Public Sub ShowWhenControlAlreadyExistsShowsSameControl()
		Dim c2 As MockSmartPart = New MockSmartPart()
		Dim c3 As MockSmartPart = New MockSmartPart()

		workspace.Show(smartPart)
		workspace.Show(c2)
		workspace.Show(c3)
		workspace.Show(c2)
		workspace.Show(smartPart)

		Assert.AreEqual(3, workspace.SmartParts.Count)
	End Sub

	<TestMethod()> _
	Public Sub CallingShowTwiceStillShowsControl()
		workspace.Show(smartPart)
		workspace.Show(smartPart)

		Assert.AreSame(smartPart, workspace.ActiveSmartPart)
	End Sub

	<TestMethod()> _
	Public Sub FiresSmartPartActivateWhenShown()
		Dim utilityInstance As FiresSmartPartUtility(Of WorkspaceEventArgs) = _
			New FiresSmartPartUtility(Of WorkspaceEventArgs)(Nothing)

		AddHandler workspace.SmartPartActivated, AddressOf utilityInstance.EventHandler

		workspace.Show(smartPart)

		Assert.AreEqual(smartPart, utilityInstance.SmartPart)
	End Sub

#End Region

#Region "Hide"

	<TestMethod()> _
	Public Sub HideDoesNotHideControl()
		smartPart.Visible = False
		workspace.Show(smartPart)

		Dim utilityInstance As StateChangedUtility(Of EventArgs) = _
			New StateChangedUtility(Of EventArgs)(False)
		AddHandler smartPart.VisibleChanged, AddressOf utilityInstance.EventHandler

		workspace.Hide(smartPart)

		' The reasoning is that in a deck, the factor that causes 
		' a smart part to be hiden is that another one is shown on top.
		' There's no actual hiding of the previous control.
		Assert.IsTrue(smartPart.Visible)
		Assert.IsFalse(utilityInstance.StateChanged)
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentException))> _
	Public Sub HideNonExistSmartPartThrows()
		workspace.Hide(smartPart)
	End Sub

	<TestMethod()> _
	Public Sub HidingShowsPreviousFireActivatedEvent()
		Dim sp1 As MockSmartPart = New MockSmartPart()
		workspace.Show(sp1)
		workspace.Show(smartPart)
		Dim utilityInstance As FiresSmartPartUtility(Of WorkspaceEventArgs) = _
			New FiresSmartPartUtility(Of WorkspaceEventArgs)(Nothing)
		AddHandler workspace.SmartPartActivated, AddressOf utilityInstance.EventHandler

		workspace.Hide(smartPart)

		Assert.AreEqual(sp1, utilityInstance.SmartPart)
	End Sub

	<TestMethod()> _
	Public Sub HideNonActiveSmartPartDoesNotChangeCurrentOne()
		Dim smartPartA As ControlSmartPart = New ControlSmartPart()
		Dim smartPartB As ControlSmartPart = New ControlSmartPart()
		Dim smartPartC As ControlSmartPart = New ControlSmartPart()

		workspace.Show(smartPartA)
		workspace.Show(smartPartB)
		workspace.Show(smartPartC)

		workspace.Hide(smartPartB)

		Assert.AreSame(smartPartC, workspace.ActiveSmartPart)
	End Sub

	<TestMethod()> _
	Public Sub ShowHideKeepsOrder()
		Dim c1 As ControlSmartPart = New ControlSmartPart()
		Dim c2 As ControlSmartPart = New ControlSmartPart()
		Dim c3 As ControlSmartPart = New ControlSmartPart()

		workspace.Show(c1)
		workspace.Show(c2)
		workspace.Show(c3)
		workspace.Show(c2)
		workspace.Hide(c2)

		Assert.AreSame(c3, workspace.ActiveSmartPart)
	End Sub

#End Region

#Region "Close"

	<TestMethod()> _
	Public Sub CloseRemovesSmartPartButDoesNotDispose()
		workspace.Show(smartPart)

		workspace.Close(smartPart)

		Assert.IsFalse(workspace.Controls.Contains(smartPart))
		Assert.AreEqual(0, workspace.SmartParts.Count)
		Assert.IsFalse(smartPart.IsDisposed)
	End Sub

	<TestMethod()> _
	Public Sub WorkspaceFiresSmartPartClosing()
		workspace.Show(smartPart)
		Dim utilityInstance As StateChangedUtility(Of WorkspaceCancelEventArgs) = _
			New StateChangedUtility(Of WorkspaceCancelEventArgs)(False)
		AddHandler workspace.SmartPartClosing, AddressOf utilityInstance.EventHandler

		workspace.Close(smartPart)

		Assert.IsTrue(utilityInstance.StateChanged)
	End Sub

	<TestMethod()> _
	Public Sub CanCancelSmartPartClosing()
		workspace.Show(smartPart)

		Dim utilityInstance As Utility = New Utility()
		AddHandler workspace.SmartPartClosing, AddressOf utilityInstance.EventHandler

		workspace.Close(smartPart)

		Assert.IsFalse(smartPart.IsDisposed)
	End Sub

	<TestMethod()> _
	Public Sub ClosingByDisposingControlDoesNotFireClosingEvent()
		Dim closing As Boolean = False
		workspace.Show(smartPart)

		Dim utility As StateChangedUtility(Of SmartParts.WorkspaceCancelEventArgs) = _
			New StateChangedUtility(Of SmartParts.WorkspaceCancelEventArgs)(False)

		AddHandler workspace.SmartPartClosing, AddressOf utility.EventHandler

		smartPart.Dispose()

		Assert.IsFalse(utility.StateChanged)
		Assert.AreEqual(0, workspace.SmartParts.Count)
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentException))> _
	Public Sub ClosingNonExistSSmartPartThrows()
		workspace.Close(smartPart)
	End Sub

	<TestMethod()> _
	Public Sub CloseShowsPreviouslyVisibleControl()
		workspace = New DeckWorkspace()

		Dim c1 As MockSmartPart = New MockSmartPart()
		Dim c2 As MockSmartPart = New MockSmartPart()

		workspace.Show(c1)
		workspace.Show(c2)
		workspace.Close(c2)

		Assert.AreSame(c1, workspace.ActiveSmartPart)
	End Sub

#End Region

#Region "Misc"

	<TestMethod()> _
	Public Sub DeckIsOderedCorrectly()
		smartPart.Visible = False
		Dim c2 As MockSmartPart = New MockSmartPart()
		c2.Visible = False

		workspace.Show(smartPart)
		workspace.Show(c2)
		workspace.Show(smartPart)
		workspace.Hide(smartPart)

		Assert.AreSame(c2, workspace.ActiveSmartPart)
		Assert.AreSame(c2, workspace.SmartParts(1))
	End Sub

#End Region

#Region "Disposing"

	<TestMethod()> _
	Public Sub ControlIsRemovedWhenSmartPartIsDisposed()
		workspace.Show(smartPart)
		Assert.AreEqual(1, workspace.SmartParts.Count)

		smartPart.Dispose()

		Assert.AreEqual(0, workspace.SmartParts.Count)
	End Sub

	<TestMethod()> _
	Public Sub PreviousSmartPartActivatedWhenActiveSmartPartDisposed()
		Dim smartPartA As MockSmartPart = New MockSmartPart()
		Dim smartPartB As MockSmartPart = New MockSmartPart()
		workspace.Show(smartPartA)
		workspace.Show(smartPartB)

		smartPartB.Dispose()
		Assert.IsFalse(workspace.Contains(smartPartB))
		Assert.AreSame(smartPartA, workspace.ActiveSmartPart)
	End Sub

	<TestMethod()> _
	Public Sub WorkspaceFiresDisposedEvent()
		Dim workspace As DeckWorkspace = New DeckWorkspace()
		Dim form As Form = New Form()
		form.Controls.Add(workspace)
		form.Show()

		Dim utilityInstance As StateChangedUtility(Of EventArgs) = _
			New StateChangedUtility(Of EventArgs)(False)
		AddHandler workspace.Disposed, AddressOf utilityInstance.EventHandler
		form.Close()

		Assert.IsTrue(utilityInstance.StateChanged)
	End Sub

	<TestMethod()> _
	Public Sub DisposeNonActiveSmartPartDoesNotChangeActiveOne()
		Dim smartPartA As ControlSmartPart = New ControlSmartPart()
		Dim smartPartB As ControlSmartPart = New ControlSmartPart()
		Dim smartPartC As ControlSmartPart = New ControlSmartPart()

		workspace.Show(smartPartA)
		workspace.Show(smartPartB)
		workspace.Show(smartPartC)

		smartPartB.Dispose()

		Assert.AreSame(smartPartC, workspace.ActiveSmartPart)
	End Sub

#End Region

	<TestMethod()> _
	Public Sub CanCloseWorkspaceWithTwoSmartparts()
		Dim parent As Control = New Control()
		parent.Controls.Add(workspace)
		Dim sp1 As MockSmartPart = New MockSmartPart()
		Dim sp2 As MockSmartPart = New MockSmartPart()
		workspace.Show(sp1)
		workspace.Show(sp2)


		parent.Dispose()
	End Sub

	<TestMethod()> _
	Public Sub ShowHidingFiresCorrectNumberOfTimes()

		Dim sp1 As MockSmartPart = New MockSmartPart()
		Dim sp2 As MockSmartPart = New MockSmartPart()

		Dim utilityInstance As FiresCounterUtility(Of WorkspaceEventArgs) = _
			New FiresCounterUtility(Of WorkspaceEventArgs)(0)
		AddHandler workspace.SmartPartActivated, AddressOf utilityInstance.EventHandler

		workspace.Show(sp1)
		workspace.Show(sp2)
		workspace.Show(smartPart)

		workspace.Hide(smartPart)

		Assert.AreEqual(4, utilityInstance.Activated)
	End Sub

	<TestMethod()> _
	Public Sub ShowingHidingMultipleTimesKeepsProperDeckOrdering()
		Dim smartPartA As ControlSmartPart = New ControlSmartPart()
		Dim smartPartB As ControlSmartPart = New ControlSmartPart()
		Dim smartPartC As ControlSmartPart = New ControlSmartPart()

		workspace.Show(smartPartA)
		workspace.Show(smartPartB)
		workspace.Show(smartPartC)

		workspace.Hide(smartPartC)
		Assert.AreSame(smartPartB, workspace.ActiveSmartPart)

		workspace.Hide(smartPartB)
		Assert.AreSame(smartPartA, workspace.ActiveSmartPart)

		workspace.Close(smartPartA)
		Assert.AreSame(smartPartC, workspace.ActiveSmartPart)

		workspace.Hide(smartPartC)
		Assert.AreSame(smartPartB, workspace.ActiveSmartPart)

		workspace.Hide(smartPartB)
		Assert.AreSame(smartPartC, workspace.ActiveSmartPart)
	End Sub

#Region "Supporting classes"

	<SmartPart()> _
	Private Class NonMockSmartPartSmartPart : Inherits Object
	End Class

	<SmartPart()> _
	Private Class MockSmartPart : Inherits Control
	End Class

	<SmartPart()> _
	Private Class ControlSmartPart : Inherits Control
	End Class

#End Region
End Class
