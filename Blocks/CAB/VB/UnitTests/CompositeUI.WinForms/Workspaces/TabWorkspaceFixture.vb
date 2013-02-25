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
Imports System.ComponentModel
Imports System.Windows.Forms
Imports Microsoft.Practices.CompositeUI.SmartParts
Imports Microsoft.Practices.ObjectBuilder

<TestClass()> _
 Public Class TabWorkspaceFixture
#Region "Setup"

	Private Shared workspace As TabWorkspace
	Private Shared workItem As WorkItem
	Private Shared sp As MockSmartPart
	Private Shared owner As Form

	<TestInitialize()> _
	Public Sub SetUp()
		workItem = New TestableRootWorkItem()
		workspace = New TabWorkspace()
		workItem.Workspaces.Add(workspace)
		workItem.Services.Add(GetType(IWorkItemActivationService), New SimpleWorkItemActivationService())
		sp = New MockSmartPart()
		owner = New Form()
		owner.Controls.Add(workspace)
		owner.Show()
	End Sub

	<TestCleanup()> _
	Public Sub TearDown()
		owner.Dispose()
	End Sub

#End Region

#Region "ApplySPI"

	<TestMethod()> _
	Public Sub ApplyTabInfoDoesNotOverrideTitleIfNull()
		Dim info As TabSmartPartInfo = New TabSmartPartInfo()
		info.Title = "foo"
		workspace.Show(sp, info)

		info = New TabSmartPartInfo()
		workspace.ApplySmartPartInfo(sp, info)

		Assert.AreEqual("foo", workspace.TabPages(0).Text)
	End Sub

#End Region

#Region "Show"

	<TestMethod()> _
	Public Sub ParentChildWorkItemAndTabbedWorkspace()
		Dim rootWorkItem As WorkItem = New TestableRootWorkItem()
		'rootWorkItem.Services.AddNew<SimpleWorkItemActivationService, IWorkItemActivationService>();

		Dim workspace As TabWorkspace = rootWorkItem.Items.AddNew(Of TabWorkspace)()

		Dim childWorkItem As ChildWorkItem = rootWorkItem.Items.AddNew(Of ChildWorkItem)()
		childWorkItem.Run(workspace)

		Assert.IsTrue(childWorkItem.Items.ContainsObject(childWorkItem.ContainedSmartPart))
		Assert.IsTrue(childWorkItem.Items.ContainsObject(childWorkItem.ContainingSmartPart))
		Assert.AreSame(childWorkItem.ContainedSmartPart, childWorkItem.ContainingSmartPart.Placeholder.SmartPart, "Placeholder was not correctly replaced.")
	End Sub

	<TestMethod()> _
	Public Sub SelectingTabFiresSmartPartActivatedEvent()
		Dim wi As WorkItem = workItem.Items.AddNew(Of WorkItem)()
		Dim smartPart1 As MockSmartPart = New MockSmartPart()
		smartPart1.Name = "SP1"
		Dim smartPart2 As MockSmartPart = New MockSmartPart()
		smartPart1.Name = "SP2"
		wi.Items.Add(smartPart1)
		wi.Items.Add(smartPart2)

		Dim form As Form = New Form()
		form.Controls.Add(workspace)
		workspace.Dock = DockStyle.Fill
		form.Show()

		workspace.Show(smartPart1)
		workspace.Show(smartPart2)

		Dim utility As FiresCounterUtility(Of SmartParts.WorkspaceEventArgs) = _
			New FiresCounterUtility(Of SmartParts.WorkspaceEventArgs)(0)

		AddHandler workspace.SmartPartActivated, AddressOf utility.EventHandler
		workspace.SelectedIndex = 0

		Assert.AreEqual(1, utility.Activated)
	End Sub

	<TestMethod()> _
	Public Sub TestShowWithMorePages()
		Dim smartpartContainer As IContainer = New Container()
		Dim sampleSmartPart1 As MockSmartPart = New MockSmartPart()
		smartpartContainer.Add(sampleSmartPart1)
		workspace.Show(sampleSmartPart1)
		Dim sampleSmartPart2 As MockSmartPart = New MockSmartPart()
		smartpartContainer.Add(sampleSmartPart2)
		workspace.Show(sampleSmartPart2)

		Assert.AreEqual(2, workspace.TabPages.Count)
		Assert.AreEqual(1, workspace.SelectedIndex)

		workspace.Show(sampleSmartPart1)

		'Returns 3
		Assert.AreEqual(2, workspace.TabPages.Count)
		Assert.AreEqual(0, workspace.SelectedIndex)
	End Sub

	<TestMethod()> _
	Public Sub CreatingTabWithDefaultSPIFiresSmartPartActivedEvent()
		Dim smartPart As MockSmartPart = New MockSmartPart()
		smartPart.Name = "SP"
		workItem.Items.Add(smartPart)

		Dim utility As StateChangedUtility(Of SmartParts.WorkspaceEventArgs) = _
			New StateChangedUtility(Of SmartParts.WorkspaceEventArgs)(False)

		AddHandler workspace.SmartPartActivated, AddressOf utility.EventHandler

		workspace.Show(smartPart)

		Assert.IsTrue(utility.StateChanged)
	End Sub

	<TestMethod()> _
	Public Sub WorkspaceShowsCorrectTabFromWorkItem()
		Dim childWI As WorkItem = workItem.Items.AddNew(Of WorkItem)()
		Dim parentSP As MockSmartPart = workItem.Items.AddNew(Of MockSmartPart)("SP")
		Dim childSP As MockSmartPart = childWI.Items.AddNew(Of MockSmartPart)("SP")
		Dim tabWS As TabWorkspace = workItem.Items.AddNew(Of TabWorkspace)("TabWS")
		tabWS.Show(parentSP)
		tabWS.Show(childSP)

		tabWS.Show(parentSP)

		Assert.AreEqual(2, tabWS.TabPages.Count)
		Assert.AreSame(tabWS.TabPages(0), tabWS.SelectedTab)
	End Sub

	<TestMethod()> _
	Public Sub SmartPartActivatePassesCorrectEventArgs()
		Dim argsSmartPart As Object = Nothing
		Dim smartPart As MockSmartPart = New MockSmartPart()
		smartPart.Name = "SP"
		workItem.Items.Add(smartPart)

		Dim utility As FiresSmartPartUtility(Of SmartParts.WorkspaceEventArgs) = _
			New FiresSmartPartUtility(Of SmartParts.WorkspaceEventArgs)(Nothing)

		AddHandler workspace.SmartPartActivated, AddressOf utility.EventHandler

		workspace.Show(smartPart)

		Assert.AreEqual(smartPart, utility.SmartPart)
	End Sub

	<TestMethod()> _
	Public Sub ShowOnHiddenSPShowsTheTabPage()
		workspace.Show(sp)
		workspace.Hide(sp)
		workspace.Show(sp)

		Assert.AreEqual(1, workspace.TabPages.Count)
		Assert.IsTrue(sp.Visible)
	End Sub

	<TestMethod()> _
	Public Sub ShowTabPageAddedAtDesignTime()
		Dim sp1 As MockSmartPart = New MockSmartPart()
		workItem.Items.Add(sp1)

		Dim page As TabPage = New TabPage()
		page.Controls.Add(sp1)
		sp1.Dock = DockStyle.Fill
		page.Name = Guid.NewGuid().ToString()

		workspace.TabPages.Add(page)

		workspace.Show(sp)
		workspace.Show(sp1)

		Assert.AreEqual(2, workspace.TabPages.Count)
		Assert.IsTrue(sp1.Visible)
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentException))> _
	Public Sub ThrowsIfSmartPartNotControlShow()
		workspace.Show(New NonControlSmartPart())
	End Sub

	<TestMethod()> _
	Public Sub ShowAddsTabWithSPDockFill()
		workspace.Show(sp)

		Assert.AreEqual(1, workspace.TabPages.Count)
		Assert.AreEqual(DockStyle.Fill, sp.Dock)
	End Sub

	<TestMethod()> _
	Public Sub ShowAddsTabsSelectsTabAndSetsText()
		Dim spInfo As TabSmartPartInfo = New TabSmartPartInfo()
		spInfo.Title = "Title"
		spInfo.Description = "Description"
		workItem.RegisterSmartPartInfo(sp, spInfo)

		workspace.Show(sp)

		Assert.AreEqual(1, workspace.TabPages.Count)
		Assert.AreEqual("Title", workspace.SelectedTab.Text)
	End Sub

	<TestMethod()> _
	Public Sub ShowFocusesTabIfAlreadyContained()
		workspace.TabPages.Add("Foo")
		workspace.Show(sp)
		workspace.TabPages.Add("Bar")

		workspace.SelectedIndex = 0

		workspace.Show(sp)

		Assert.AreEqual(1, workspace.SelectedIndex)
	End Sub

	<TestMethod()> _
	Public Sub ShowTabWithNewInfo()
		Dim part As MockSmartPart = New MockSmartPart()
		workItem.Items.Add(part)
		Dim info As TabSmartPartInfo = New TabSmartPartInfo()
		info.Title = "Updated"

		workspace.Show(part, info)

		Assert.AreEqual("Updated", workspace.SelectedTab.Text)
	End Sub

	<TestMethod()> _
	Public Sub TabPositionWithInfoIsSetCorrectly()
		Dim page As TabPage = New TabPage()
		workspace.TabPages.Add(page)

		Dim info As TabSmartPartInfo = New TabSmartPartInfo()
		info.Position = TabPosition.Beginning
		workspace.Show(sp, info)

		Assert.AreEqual(2, workspace.TabPages.Count)
		Assert.AreSame(workspace.TabPages(0), workspace.SelectedTab)
	End Sub

	<TestMethod()> _
	Public Sub TabPositionWithRegisteredInfoIsSetCorrectly()
		Dim page As TabPage = New TabPage()
		workspace.TabPages.Add(page)

		Dim info As TabSmartPartInfo = New TabSmartPartInfo()
		info.Position = TabPosition.Beginning

		workItem.RegisterSmartPartInfo(sp, info)
		workspace.Show(sp)

		Assert.AreEqual(2, workspace.TabPages.Count)
		Assert.AreSame(workspace.TabPages(0), workspace.SelectedTab)
	End Sub

	<TestMethod()> _
	Public Sub CanShowTabPositionAtEnd()
		Dim page As TabPage = New TabPage()
		workspace.TabPages.Add(page)

		Dim info As TabSmartPartInfo = New TabSmartPartInfo()
		info.Position = TabPosition.End
		workspace.Show(sp, info)

		Assert.AreEqual(2, workspace.TabPages.Count)
		Assert.AreEqual(1, workspace.SelectedIndex)
	End Sub

	<TestMethod()> _
	Public Sub WorkspaceExposesCollectionOfPages()
		workspace.Show(sp)

		Assert.AreEqual(1, workspace.Pages.Count)
	End Sub

	<TestMethod()> _
	Public Sub CanShowSmartPartWithNullSite()
		Dim smartPartA As Control = New Control()

		workspace.Show(smartPartA)

		Assert.AreEqual(1, workspace.Pages.Count)
	End Sub

	<TestMethod()> _
	Public Sub CanUseNotTabSPI()
		Dim info As SmartPartInfo = New SmartPartInfo()
		info.Title = "Foo"

		workspace.Show(sp, info)
	End Sub

	<TestMethod()> _
	Public Sub UsesSPInfoIfNoTabSPInfoExists()
		Dim info As SmartPartInfo = New SmartPartInfo()
		info.Title = "Foo"
		workItem.RegisterSmartPartInfo(sp, info)

		workspace.Show(sp)

		Assert.AreEqual("Foo", workspace.Pages(sp).Text)
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
	Public Sub CanShowSmartPartWithNullInfo()
		workspace.Show(sp, Nothing)
	End Sub

	<TestMethod()> _
	Public Sub AddingSmartPartAtBeginningFiresOneActivatedEvent()
		Dim smartPartA As MockSmartPart = New MockSmartPart()
		Dim smartPartB As MockSmartPart = New MockSmartPart()

		Dim smartPartInfoA As TabSmartPartInfo = New TabSmartPartInfo()
		smartPartInfoA.Title = "Smart Part A"
		smartPartInfoA.Position = TabPosition.Beginning

		Dim smartPartInfoB As TabSmartPartInfo = New TabSmartPartInfo()
		smartPartInfoB.Title = "Smart Part B"

		workspace.Show(smartPartB, smartPartInfoB)

		Dim utility As FiresCounterWithSmartPartCheckUtility(Of SmartParts.WorkspaceEventArgs) = _
			New FiresCounterWithSmartPartCheckUtility(Of SmartParts.WorkspaceEventArgs)(0, smartPartA)

		AddHandler workspace.SmartPartActivated, AddressOf utility.EventHandler
		smartPartA = DirectCast(utility.SmartPart, MockSmartPart)

		workspace.Show(smartPartA, smartPartInfoA)

		Assert.AreEqual(1, utility.Activated)
	End Sub

	<TestMethod()> _
	Public Sub NotSettingActivateDefaultsToTrue()
		workspace.Show(sp)
		Dim smartPart As Control = New Control()
		workspace.Show(smartPart)

		Assert.AreEqual(1, workspace.SelectedIndex)
	End Sub

	<TestMethod()> _
	Public Sub CanActivateTabInSPI()
		Dim info As TabSmartPartInfo = New TabSmartPartInfo()
		info.ActivateTab = True
		workspace.Show(sp)
		Dim smartPart As Control = New Control()
		workspace.Show(smartPart, info)

		Assert.AreEqual(1, workspace.SelectedIndex)
	End Sub

	<TestMethod()> _
	Public Sub CanAvoidActivationTabInSPI()
		Dim info As TabSmartPartInfo = New TabSmartPartInfo()
		info.ActivateTab = False
		workspace.Show(sp)
		Dim smartPart As Control = New Control()
		workspace.Show(smartPart, info)

		Assert.AreEqual(0, workspace.SelectedIndex)
	End Sub

#End Region

#Region "Hide"

	<TestMethod(), ExpectedException(GetType(ArgumentException))> _
	Public Sub ThrowsIfSmartPartNotControlHide()
		workspace.Hide(New NonControlSmartPart())
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentException))> _
	Public Sub ThrowsIfSPNotInManagerHide()
		workspace.Hide(sp)
	End Sub

	<TestMethod()> _
	Public Sub CanHideTab()
		workspace.Show(sp)
		workspace.Hide(sp)

		Assert.IsFalse(sp.Visible)
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
	Public Sub ThrowsIfSmartPartNullHide()
		workspace.Hide(Nothing)
	End Sub

	<TestMethod()> _
	Public Sub HideActivatesPreviousSmartPartIfThereIsOne()
		Dim smartPartA As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()
		Dim smartPartB As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()
		workspace.Show(smartPartA)
		workspace.Show(smartPartB)

		workspace.Hide(smartPartB)

		Assert.AreSame(workspace.ActiveSmartPart, smartPartA)
	End Sub

	<TestMethod()> _
	Public Sub HideActivatesFollowingSiblingIfItIsFirstOne()
		Dim smartPartA As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()
		Dim smartPartB As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()
		workspace.Show(smartPartA)
		workspace.Show(smartPartB)
		workspace.Show(smartPartA)

		workspace.Hide(smartPartA)

		Assert.AreSame(workspace.ActiveSmartPart, smartPartB)
	End Sub

	<TestMethod()> _
	Public Sub HideDoesNotActivateAnythingIfNoOtherTabsExist()
		Dim smartPartA As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()
		Dim smartPartB As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()
		Dim smartPartC As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()
		workspace.Show(smartPartA)
		workspace.Show(smartPartB)
		workspace.Show(smartPartC)

		workspace.Close(smartPartC)

		workspace.Close(smartPartA)

		workspace.Activate(smartPartB)
		workspace.Hide(smartPartB)

		Assert.IsNull(workspace.ActiveSmartPart)
	End Sub

	<TestMethod()> _
	Public Sub HidindSmartPartDoesNothingAfterTheFirstHide()
		Dim smartPartA As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()
		Dim smartPartB As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()
		Dim smartPartC As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()
		workspace.Show(smartPartA)
		workspace.Show(smartPartB)
		workspace.Show(smartPartC)

		workspace.Hide(smartPartC)
		Assert.AreSame(smartPartB, workspace.ActiveSmartPart)

		workspace.Hide(smartPartC)
		Assert.AreSame(smartPartB, workspace.ActiveSmartPart)
	End Sub

#End Region

#Region "Close"

	<TestMethod()> _
	Public Sub WorkspaceFiresSmartPartClosing()
		Dim smartPart As MockSmartPart = New MockSmartPart()
		smartPart.Name = "SP"
		workItem.Items.Add(smartPart)
		workspace.Show(smartPart)

		Dim utility As StateChangedUtility(Of SmartParts.WorkspaceCancelEventArgs) = _
			New StateChangedUtility(Of SmartParts.WorkspaceCancelEventArgs)(False)

		AddHandler workspace.SmartPartClosing, AddressOf utility.EventHandler

		workspace.Close(smartPart)

		Assert.IsTrue(utility.StateChanged)
	End Sub

	<TestMethod()> _
	Public Sub CanCancelSmartPartClosing()
		Dim smartPart As MockSmartPart = New MockSmartPart()
		smartPart.Name = "SP"
		workItem.Items.Add(smartPart)
		workspace.Show(smartPart)

		Dim aUtility As Utility = New Utility()
		AddHandler workspace.SmartPartClosing, AddressOf aUtility.EventHandler

		workspace.Close(smartPart)

		Assert.IsFalse(smartPart.IsDisposed)
	End Sub

	<TestMethod()> _
	Public Sub ClosingByDisposingControlDoesNotFireClosingEvent()
		Dim smartPart As MockSmartPart = New MockSmartPart()
		smartPart.Name = "SP"
		workItem.Items.Add(smartPart)
		workspace.Show(smartPart)

		Dim utility As StateChangedUtility(Of SmartParts.WorkspaceCancelEventArgs) = _
			New StateChangedUtility(Of SmartParts.WorkspaceCancelEventArgs)(False)

		AddHandler workspace.SmartPartClosing, AddressOf utility.EventHandler

		smartPart.Dispose()

		Assert.IsFalse(utility.StateChanged)
		Assert.AreEqual(0, workspace.SmartParts.Count)
	End Sub

	<TestMethod()> _
	Public Sub CanRetrieveSmartPartFromEventArgs()
		Dim smartPartObject1 As Object = Nothing
		Dim smartPart As MockSmartPart = New MockSmartPart()
		smartPart.Name = "SP"
		workItem.Items.Add(smartPart)
		workspace.Show(smartPart)

		Dim utility As FiresSmartPartUtility(Of SmartParts.WorkspaceCancelEventArgs) = _
			New FiresSmartPartUtility(Of SmartParts.WorkspaceCancelEventArgs)(smartPartObject1)

		AddHandler workspace.SmartPartClosing, AddressOf utility.EventHandler

		workspace.Close(smartPart)
		smartPartObject1 = utility.SmartPart

		Assert.AreEqual(smartPart, smartPartObject1)
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentException))> _
	Public Sub ThrowsIfSmartPartNotControlClose()
		workspace.Close(New NonControlSmartPart())
	End Sub

	<TestMethod()> _
	Public Sub CloseRemovesTabButNotDisposesIt()
		workspace.Show(sp)
		workspace.Close(sp)

		Assert.AreEqual(0, workspace.TabPages.Count)
		Assert.IsFalse(sp.IsDisposed)
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentException))> _
	Public Sub ThrowsIfSPNotInManagerClose()
		workspace.Close(sp)
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
	Public Sub ThrowsIfSmartPartNullShow()
		workspace.Show(Nothing)
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
	Public Sub ThrowsIfSmartPartNullClose()
		workspace.Close(Nothing)
	End Sub

	<TestMethod()> _
	Public Sub CloseTabPage()
		Dim sampleSmartPart As MockSmartPart = New MockSmartPart()
		workItem.Items.Add(sampleSmartPart, "SampleSmartPart")
		workItem.Items.Add(workspace)
		workspace.Show(sampleSmartPart)

		workspace.Close(sampleSmartPart)

		Assert.AreEqual(0, workspace.TabPages.Count)
	End Sub

	<TestMethod()> _
	Public Sub RemovingSelectedTabFiresSelection()
		Dim cc As WorkItem = workItem.Items.AddNew(Of WorkItem)()
		Dim smartPart As MockSmartPart = New MockSmartPart()
		smartPart.Name = "SP"
		Dim smartPart2 As MockSmartPart = New MockSmartPart()
		smartPart.Name = "SP2"
		cc.Items.Add(smartPart)
		cc.Items.Add(smartPart2)

		Dim form As Form = New Form()
		form.Controls.Add(workspace)
		workspace.Dock = DockStyle.Fill
		form.Show()

		workspace.Show(smartPart)
		workspace.Show(smartPart2)

		Dim utility As FiresCounterUtility(Of SmartParts.WorkspaceEventArgs) = _
			New FiresCounterUtility(Of SmartParts.WorkspaceEventArgs)(0)

		AddHandler workspace.SmartPartActivated, AddressOf utility.EventHandler
		workspace.Close(smartPart2)

		Assert.AreEqual(1, utility.Activated)
	End Sub

	<TestMethod()> _
	Public Sub RemovingOneTabDoesNotFiresSelection()
		Dim smartPart As MockSmartPart = New MockSmartPart()
		smartPart.Name = "SP"
		workItem.Items.Add(smartPart)

		Dim form As Form = New Form()
		form.Controls.Add(workspace)
		workspace.Dock = DockStyle.Fill
		form.Show()

		Dim utility As FiresCounterUtility(Of SmartParts.WorkspaceEventArgs) = _
			New FiresCounterUtility(Of SmartParts.WorkspaceEventArgs)(0)

		AddHandler workspace.SmartPartActivated, AddressOf utility.EventHandler

		workspace.Show(smartPart)
		workspace.Close(smartPart)

		Assert.AreEqual(1, utility.Activated)
	End Sub

	<TestMethod()> _
	Public Sub CloseTabPageAddedAtDesignTime()
		' Create new instance to simulate design-time creation.
		workspace = New TabWorkspace()
		workItem.Items.Add(workspace)
		Dim sp1 As MockSmartPart = New MockSmartPart()
		workItem.Items.Add(sp1)

		Dim page As TabPage = New TabPage()
		page.Controls.Add(sp1)
		sp1.Dock = DockStyle.Fill
		page.Name = Guid.NewGuid().ToString()
		workspace.TabPages.Add(page)

		owner = New Form()
		owner.Controls.Add(workspace)
		owner.Show()

		workspace.Close(sp1)

		Assert.AreEqual(0, workspace.TabPages.Count)
	End Sub

#End Region

#Region "Misc"

	<TestMethod()> _
	Public Sub SelectTabWithNoSmartPartResetsActiveSmartPart()
		workspace.SelectedIndex = -1

		workspace.TabPages.Add("Foo")
		workspace.Show(sp)
		workspace.TabPages.Add("Bar")

		workspace.SelectedIndex = 0
		workspace.SelectedIndex = 1
		workspace.SelectedIndex = 0

		Assert.IsNull(workspace.ActiveSmartPart)
	End Sub

#End Region

#Region "Dispose"

	<TestMethod()> _
	Public Sub TabIsRemovedWhenWorkItemIsDisposed()
		Dim workspace As TabWorkspace = New TabWorkspace()
		workItem.Items.Add(workspace)

		workItem.Workspaces.Add(workspace, "TabWorkspace")

		Dim child As WorkItem = workItem.Items.AddNew(Of WorkItem)()
		Dim smartPart As Control = New Control()
		child.Items.Add(smartPart)

		Dim myWS As TabWorkspace = CType(child.Workspaces("TabWorkspace"), TabWorkspace)
		myWS.Show(smartPart)

		Assert.AreEqual(1, workspace.TabCount)
		Assert.IsTrue(workspace.Contains(smartPart))

		child.Dispose()

		Assert.IsFalse(workspace.Contains(smartPart))
		Assert.AreEqual(0, workspace.TabCount)

	End Sub

	<TestMethod()> _
	Public Sub TabIsRemovedWhenSmartPartIsDisposed()
		workspace.Show(sp)
		Assert.AreEqual(1, workspace.TabPages.Count)

		sp.Dispose()

		Assert.AreEqual(0, workspace.TabPages.Count)
	End Sub

	<TestMethod()> _
	Public Sub TabWorkspaceFiresDisposedEvent()
		Dim form As Form = New Form()
		form.Controls.Add(workspace)
		form.Show()

		Dim utility As StateChangedUtility(Of System.EventArgs) = _
			New StateChangedUtility(Of System.EventArgs)(False)

		AddHandler workspace.Disposed, AddressOf utility.EventHandler
		form.Close()

		Assert.IsTrue(utility.StateChanged)
	End Sub

	<TestMethod()> _
	Public Sub SPIsRemovedFromWorkspaceWhenDisposed1()
		Dim smartPartA As MockSmartPart = New MockSmartPart()

		Dim spInfoA As TabSmartPartInfo = New TabSmartPartInfo()
		spInfoA.Title = "Smart Part A"

		workItem.SmartParts.Add(smartPartA)

		workspace.Show(smartPartA, spInfoA)

		Assert.AreEqual(1, workspace.TabPages.Count)

		smartPartA.Dispose()

		Assert.AreEqual(0, workspace.TabPages.Count)

		' Returns 1
		Assert.AreEqual(0, workspace.SmartParts.Count)
	End Sub

#End Region

#Region "Activate"

	<TestMethod()> _
	Public Sub ActivatingMultipleTimesRaisesActivatedEventOnlyOnce()
		Dim info As TabSmartPartInfo = New TabSmartPartInfo()
		info.ActivateTab = True

		Dim utility As FiresCounterUtility(Of SmartParts.WorkspaceEventArgs) = _
			New FiresCounterUtility(Of SmartParts.WorkspaceEventArgs)(0)

		AddHandler workspace.SmartPartActivated, AddressOf utility.EventHandler

		' First call to event should happen here.
		workspace.Show(sp)
		Dim smartPart As Control = New Control()
		' Second call to event here.
		workspace.Show(smartPart, info)

		' No further calls should happen.
		workspace.Activate(smartPart)
		workspace.Activate(smartPart)

		Assert.AreEqual(2, utility.Activated)
	End Sub


#End Region

#Region "Supporting classes"

	<SmartPart()> _
	Private Class SmartPartWithPlaceholder : Inherits UserControl
		Private innerPlaceholder As SmartPartPlaceholder = New SmartPartPlaceholder()

		Public ReadOnly Property Placeholder() As SmartPartPlaceholder
			Get
				Return innerPlaceholder
			End Get
		End Property

		Public Sub New()
			innerPlaceholder.SmartPartName = "ChildControl"
			Me.Controls.Add(innerPlaceholder)
		End Sub
	End Class

	<SmartPart()> _
	Private Class SimpleUserControlSmartPart : Inherits UserControl
	End Class

	Private Class ChildWorkItem : Inherits WorkItem
		Public ContainedSmartPart As SimpleUserControlSmartPart
		Public ContainingSmartPart As SmartPartWithPlaceholder

		Public Overloads Sub Run(ByVal workspace As IWorkspace)
			ContainedSmartPart = Me.Items.AddNew(Of SimpleUserControlSmartPart)("ChildControl")
			ContainingSmartPart = Me.Items.AddNew(Of SmartPartWithPlaceholder)("ParentControl")

			workspace.Show(ContainingSmartPart)
		End Sub
	End Class

	<SmartPart()> _
	Private Class NonControlSmartPart : Inherits Object
	End Class

	<SmartPart()> _
	Private Class MockSmartPart : Inherits Control
		Private info As TabSmartPartInfo = New TabSmartPartInfo()

		Public Sub New()
			info.Title = "Title"
			info.Description = "Description"
		End Sub
	End Class

#End Region
End Class

