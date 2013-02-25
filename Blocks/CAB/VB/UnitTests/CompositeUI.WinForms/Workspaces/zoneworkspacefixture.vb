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
Imports Microsoft.Practices.CompositeUI
Imports Microsoft.Practices.CompositeUI.WinForms


<TestClass()> _
Public Class ZoneWorkspaceFixture
	Private Shared workspace As ZoneWorkspace
	Private Shared control As Control
	Private Shared workItem As WorkItem

	<TestInitialize()> _
	Public Sub SetUp()
		workItem = New TestableRootWorkItem()
		workspace = New ZoneWorkspace()
		control = New Control()

		workItem.Services.Add(GetType(IWorkItemActivationService), New SimpleWorkItemActivationService())
		workItem.Workspaces.Add(workspace)
		Dim info As ISmartPartInfo = New ZoneSmartPartInfo("Main")
		workItem.RegisterSmartPartInfo(control, info)
	End Sub

	<TestMethod()> _
	Public Sub CanSetControlZoneName()
		workspace.Controls.Add(control)

		workspace.SetZoneName(control, "Main")

		Assert.AreEqual(1, workspace.Zones.Count)
		Assert.AreSame(control, workspace.Zones("Main"))
		Assert.AreEqual("Main", workspace.GetZoneName(control))
	End Sub

	<TestMethod()> _
	Public Sub CanExtendScrollableControl()
		Dim cc As ContainerControl = New ContainerControl()
		workspace.Controls.Add(cc)
		Dim sc As SplitContainer = New SplitContainer()
		workspace.Controls.Add(sc)
		Dim fp As FlowLayoutPanel = New FlowLayoutPanel()
		workspace.Controls.Add(fp)

		Assert.IsTrue((CType(workspace, IExtenderProvider)).CanExtend(cc))
		Assert.IsTrue((CType(workspace, IExtenderProvider)).CanExtend(sc.Panel1))
		Assert.IsTrue((CType(workspace, IExtenderProvider)).CanExtend(sc))
		Assert.IsTrue((CType(workspace, IExtenderProvider)).CanExtend(fp))
	End Sub

	<TestMethod()> _
	Public Sub SetControlZoneNameNotDescendentNoOp()
		workspace.SetZoneName(control, "Main")

		Assert.AreEqual(0, workspace.Zones.Count)
	End Sub

	<TestMethod()> _
	Public Sub RemoveControlRemovesZoneName()
		workspace.Controls.Add(control)
		workspace.SetZoneName(control, "Main")

		workspace.Controls.Remove(control)

		Assert.AreEqual(0, workspace.Zones.Count)
	End Sub

	<TestMethod()> _
	Public Sub DisposeControlRemovesZoneName()
		workspace.Controls.Add(control)
		workspace.SetZoneName(control, "Main")

		control.Dispose()

		Assert.AreEqual(0, workspace.Zones.Count)
	End Sub

	<TestMethod(), ExpectedException(GetType(InvalidOperationException))> _
	Public Sub ShowNoZonesThrows()
		workspace.Show(New Control())
	End Sub


	<TestMethod(), ExpectedException(GetType(ArgumentOutOfRangeException))> _
	Public Sub ShowWithNonExistingZoneNameThrows()
		Dim zone As Control = New Control()
		workspace.Controls.Add(zone)
		workspace.SetZoneName(zone, "Main")
		workspace.Show(control, New ZoneSmartPartInfo("Blah"))
	End Sub

	<TestMethod()> _
	Public Sub ShowOnZoneAddsControlAndSetsVisible()
		Dim zone As Control = New Control()
		workspace.Controls.Add(zone)
		workspace.SetZoneName(zone, "Main")

		workspace.Show(control, New ZoneSmartPartInfo("Main"))

		Assert.AreEqual(1, zone.Controls.Count)
		Assert.IsTrue(control.Visible)
	End Sub

	<TestMethod()> _
	Public Sub HideSmartPartHidesControl()
		Dim zone As Control = New Control()
		workspace.Controls.Add(zone)
		workspace.SetZoneName(zone, "Main")

		workspace.Show(control, New ZoneSmartPartInfo("Main"))
		workspace.Hide(control)

		Assert.AreEqual(1, zone.Controls.Count)
		Assert.IsFalse(control.Visible)
	End Sub

	<TestMethod()> _
	Public Sub CloseSmartPartRemovesControl()
		Dim zone As Control = New Control()
		workspace.Controls.Add(zone)
		workspace.SetZoneName(zone, "Main")

		workspace.Show(control, New ZoneSmartPartInfo("Main"))
		workspace.Close(control)

		Assert.AreEqual(0, zone.Controls.Count)
		Assert.IsFalse(control.IsDisposed)
	End Sub

	<TestMethod()> _
	Public Sub CloseSmartPartFiresClosing()
		Dim zone As Control = New Control()
		workspace.Controls.Add(zone)
		workspace.SetZoneName(zone, "Main")
		Dim utilityInstance As StateChangedUtility(Of WorkspaceCancelEventArgs) = _
			New StateChangedUtility(Of WorkspaceCancelEventArgs)(False)
		AddHandler workspace.SmartPartClosing, AddressOf utilityInstance.EventHandler

		workspace.Show(control, New ZoneSmartPartInfo("Main"))
		workspace.Close(control)

		Assert.IsTrue(utilityInstance.StateChanged)
	End Sub

	<TestMethod()> _
	Public Sub CancelClosingDoesNotRemoveControl()
		Dim zone As Control = New Control()
		workspace.Controls.Add(zone)
		workspace.SetZoneName(zone, "Main")
		Dim utilityInstance As Utility = New Utility
		AddHandler workspace.SmartPartClosing, AddressOf utilityInstance.EventHandler

		workspace.Show(control, New ZoneSmartPartInfo("Main"))
		workspace.Close(control)

		Assert.AreEqual(1, zone.Controls.Count)
	End Sub

	<TestMethod()> _
	Public Sub ClosingByDisposingControlDoesNotFireClosingEvent()
		Dim zone As Control = New Control()
		workspace.Controls.Add(zone)
		workspace.SetZoneName(zone, "Main")
		Dim utilityInstance As StateChangedUtility(Of workspacecanceleventargs) = _
			New StateChangedUtility(Of workspacecanceleventargs)(False)

		AddHandler workspace.SmartPartClosing, AddressOf utilityInstance.eventhandler

		workspace.Show(control, New ZoneSmartPartInfo("Main"))
		control.Dispose()

		Assert.IsFalse(utilityInstance.StateChanged)
		Assert.AreEqual(0, workspace.SmartParts.Count)
	End Sub

	<TestMethod()> _
	Public Sub ShowNoParamsShowsInDefaultZoneDesigner()
		Dim form As ZoneWorkspaceForm = New ZoneWorkspaceForm()
		workItem.Items.Add(form.Workspace)
		Dim calendar As MonthCalendar = New MonthCalendar()
		workItem.RegisterSmartPartInfo(calendar, New ZoneSmartPartInfo("ContentZone"))

		form.Workspace.Show(calendar)

		Assert.AreEqual(1, form.Workspace.Zones("ContentZone").Controls.Count)
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentException))> _
	Public Sub ThrowsIfHideNotShownControl()
		Dim sampleControl As Control = New Control()

		workspace.Hide(sampleControl)
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentException))> _
	Public Sub ThrowsIfCloseNotShownControl()
		Dim sampleControl As Control = New Control()

		workspace.Close(sampleControl)
	End Sub

	<TestMethod()> _
	Public Sub FocusingSmartPartFiresActivated()
		Dim form As ZoneWorkspaceForm = CreateFormAddWorkspace()
		Dim zone As Control = New Control()

		workspace.Controls.Add(zone)
		workspace.SetZoneName(zone, "Main")

		Dim utilityInstance As StateChangedUtility(Of WorkspaceEventArgs) = _
			New StateChangedUtility(Of WorkspaceEventArgs)(False)
		AddHandler workspace.SmartPartActivated, AddressOf utilityInstance.EventHandler
		form.Show()

		workspace.Show(control, New ZoneSmartPartInfo("Main"))
		workspace.Zones("Main").Focus()

		Assert.IsTrue(utilityInstance.StateChanged)
	End Sub

	<TestMethod()> _
	Public Sub FocusingSmartPartFiresActivatedWithSmartPart()
		Dim form As ZoneWorkspaceForm = CreateFormAddWorkspace()
		Dim zone As Control = New Control()

		workspace.Controls.Add(zone)
		workspace.SetZoneName(zone, "Main")
		Dim utilityInstance As FiresSmartPartUtility(Of WorkspaceEventArgs) = _
			New FiresSmartPartUtility(Of WorkspaceEventArgs)(Nothing)
		AddHandler workspace.SmartPartActivated, AddressOf utilityInstance.EventHandler
		form.Show()

		workspace.Show(control, New ZoneSmartPartInfo("Main"))
		workspace.Zones("Main").Focus()

		Assert.AreEqual(control, utilityInstance.SmartPart)
	End Sub

	<TestMethod()> _
	Public Sub ActivatedFiresCorrectNumberOfTimes()
		Dim form As ZoneWorkspaceForm = CreateFormAddWorkspace()
		Dim zone As Control = New Control()
		Dim zone1 As Control = New Control()
		Dim control2 As Control = New Control()

		AddZones(zone, zone1)

		Dim utilityInstance As FiresCounterUtility(Of WorkspaceEventArgs) = _
			New FiresCounterUtility(Of WorkspaceEventArgs)(0)
		AddHandler workspace.SmartPartActivated, AddressOf utilityInstance.EventHandler
		form.Show()

		workspace.Show(control, New ZoneSmartPartInfo("Main"))
		workspace.Show(control2, New ZoneSmartPartInfo("Main1"))
		control.Select()
		control2.Select()
		control.Select()

		'Will fire five times because it fires when show is called.
		Assert.AreEqual(5, utilityInstance.Activated)
	End Sub

	<TestMethod()> _
	Public Sub ControlIsRemovedWhenSmartPartIsDisposed()
		Dim zone As Control = New Control()
		workspace.Controls.Add(zone)
		workspace.SetZoneName(zone, "Main")
		workspace.Show(control)
		Assert.IsTrue(workspace.Zones("Main").Contains(control))

		control.Dispose()

		Assert.IsFalse(workspace.Zones("Main").Contains(control))
	End Sub

	<TestMethod()> _
	Public Sub ZoneDocksControlCorrectly()
		Dim zone As Control = New Control()
		workspace.Controls.Add(zone)
		workspace.SetZoneName(zone, "Main")
		Dim info As ZoneSmartPartInfo = New ZoneSmartPartInfo()
		info.Dock = DockStyle.Fill
		workspace.Show(control, info)

		Assert.AreEqual(DockStyle.Fill, workspace.Zones("Main").Controls(0).Dock)
	End Sub

	<TestMethod()> _
	Public Sub WorkspaceGetsRegisteredSPI()
		Dim zone As Control = New Control()
		workspace.Controls.Add(zone)
		workspace.SetZoneName(zone, "Main")
		Dim info As ZoneSmartPartInfo = New ZoneSmartPartInfo()
		info.Dock = DockStyle.Fill

		workItem.RegisterSmartPartInfo(control, info)
		workspace.Show(control, info)

		Assert.AreEqual(DockStyle.Fill, workspace.Zones("Main").Controls(0).Dock)
	End Sub

	<TestMethod()> _
	Public Sub ZoneDoesNotFireSPActivatedEventIfNoSPPresent()
		Dim zone1 As Control = New Control()
		workspace.Controls.Add(zone1)

		workspace.SetZoneName(zone1, "TestZone")

		Assert.AreEqual("TestZone", workspace.GetZoneName(zone1))

		Dim utilityInstance As StateChangedUtility(Of WorkspaceEventArgs) = _
			New StateChangedUtility(Of WorkspaceEventArgs)(False)

		AddHandler workspace.SmartPartActivated, AddressOf utilityInstance.EventHandler
		zone1.Focus()

		Assert.IsFalse(utilityInstance.StateChanged)
	End Sub

	<TestMethod()> _
	Public Sub CanShowInDefaultZone()
		Dim zone As Control = New Control()
		workspace.Controls.Add(zone)
		workspace.SetZoneName(zone, "TestZone")
		workspace.SetIsDefaultZone(zone, True)

		Dim smartPartA As Control = New Control()
		workspace.Show(smartPartA)

		Assert.IsTrue(workspace.GetIsDefaultZone(zone))
		Assert.AreSame(smartPartA, workspace.Zones("TestZone").Controls(0))
	End Sub

	<TestMethod()> _
	Public Sub CanShowInDefaultZoneWithInfoNoZoneName()
		Dim zone As Control = New Control()
		workspace.Controls.Add(zone)
		workspace.SetZoneName(zone, "TestZone")
		workspace.SetIsDefaultZone(zone, True)

		Dim smartPartA As Control = New Control()
		Dim info As ZoneSmartPartInfo = New ZoneSmartPartInfo()
		info.Dock = DockStyle.Fill
		info.Title = "Test"
		workspace.Show(smartPartA, info)

		Assert.IsTrue(workspace.GetIsDefaultZone(zone))
		Assert.AreSame(smartPartA, workspace.Zones("TestZone").Controls(0))
	End Sub

#Region "Utility code for RemovingZoneUnregistersGotFocusEvent"

	Private Class RemovingZoneUnregistersGotFocusEventUtility

		Private innerActivatedCalled As Boolean

		Public Property ActivatedCalled() As Boolean
			Get
				Return innerActivatedCalled
			End Get
			Set(ByVal value As Boolean)
				innerActivatedCalled = value
			End Set
		End Property

		Public Sub New(ByVal activatedCalled As Boolean)
			innerActivatedCalled = activatedCalled
		End Sub

		Public Sub EventHandler(ByVal sender As Object, ByVal args As WorkspaceEventArgs)
			innerActivatedCalled = True
		End Sub

	End Class

#End Region

	<TestMethod()> _
	Public Sub RemovingZoneUnregistersGotFocusEvent()
		Dim zone1 As Control = New Control()
		workspace.Controls.Add(zone1)

		workspace.SetZoneName(zone1, "TestZone")

		Assert.AreEqual("TestZone", workspace.GetZoneName(zone1))

		workspace.Controls.Remove(zone1)

		Assert.IsNull(workspace.GetZoneName(zone1))
		Assert.AreEqual(0, workspace.Zones.Count)

		Dim utilityInstance As RemovingZoneUnregistersGotFocusEventUtility = _
			New RemovingZoneUnregistersGotFocusEventUtility(False)
		AddHandler workspace.SmartPartActivated, AddressOf utilityInstance.EventHandler

		Dim sp As ControlSmartPart = New ControlSmartPart()
		zone1.Controls.Add(sp)
		Dim form1 As Form = New Form()
		form1.Controls.Add(zone1)
		form1.Show()

		Assert.IsFalse(utilityInstance.ActivatedCalled)
	End Sub

#Region "Utility code for ActivatingZoneRaisesSmartPartActivatedForContainingSP"

	Private Class ActivatingZoneRaisesSmartPartActivatedForContainingUtility

		Private innerReceived As Control

		Public Property Received() As Control
			Get
				Return innerReceived
			End Get
			Set(ByVal value As Control)
				innerReceived = value
			End Set
		End Property

		Public Sub New(ByVal received As Control)
			innerReceived = received
		End Sub

		Public Sub EventHandler(ByVal sender As Object, ByVal args As WorkspaceEventArgs)
			innerReceived = CType(args.SmartPart, Control)
		End Sub

	End Class

#End Region

	<TestMethod()> _
	Public Sub ActivatingZoneRaisesSmartPartActivatedForContainingSP()
		Dim zoneForm As ZoneWorkspaceForm = New ZoneWorkspaceForm()
		zoneForm.Show()
		Dim sp As Control = New Control()
		sp.Size = New System.Drawing.Size(50, 50)
		sp.Text = "Foo"
		Dim tb As TextBox = New TextBox()
		sp.Controls.Add(tb)
		zoneForm.Workspace.Show(sp)
		zoneForm.Workspace.Show(New Control())

		Dim utilityInstance As StateChangedWithSmartPartCheckUtility(Of WorkspaceEventArgs) = _
			New StateChangedWithSmartPartCheckUtility(Of WorkspaceEventArgs)(Nothing)

		AddHandler zoneForm.Workspace.SmartPartActivated, AddressOf utilityInstance.EventHandler
		tb.Select()
		Assert.AreSame(sp, utilityInstance.SmartPart)
	End Sub

	<TestMethod()> _
	Public Sub RenamingZoneDoesNotRegisterEventsMultipleTimes()

		Dim zoneForm As ZoneWorkspaceForm = New ZoneWorkspaceForm()
		zoneForm.Show()
		Dim zone1 As Control = New Control()

		zoneForm.Workspace.Controls.Add(zone1)
		zoneForm.Workspace.SetZoneName(zone1, "TestZone")

		'rename
		zoneForm.Workspace.SetZoneName(zone1, "NewZone")

		Dim utilityInstance As FiresCounterUtility(Of WorkspaceEventArgs) = _
			New FiresCounterUtility(Of WorkspaceEventArgs)(0)

		AddHandler zoneForm.Workspace.SmartPartActivated, AddressOf utilityInstance.EventHandler
		zoneForm.Workspace.Show(New Control(), New ZoneSmartPartInfo("NewZone"))

		Assert.AreEqual("NewZone", zoneForm.Workspace.GetZoneName(zone1))
		Assert.AreEqual(1, utilityInstance.Activated)
	End Sub

	<TestMethod()> _
	Public Sub ShowFiresActivatedEventWithSPAsParameter()
		Dim smartPartA As ControlSmartPart = New ControlSmartPart()
		Dim smartPartInfoA As ZoneSmartPartInfo = New ZoneSmartPartInfo()
		smartPartInfoA.ZoneName = "Zone"

		Dim zone As Control = New Control()
		workspace.Controls.Add(zone)
		workspace.SetZoneName(zone, "Zone")

		Dim utilityInstance As StateChangedWithSmartPartCheckUtility(Of WorkspaceEventArgs) = _
			New StateChangedWithSmartPartCheckUtility(Of WorkspaceEventArgs)(False)
		AddHandler workspace.SmartPartActivated, AddressOf utilityInstance.EventHandler
		workspace.Show(smartPartA, smartPartInfoA)
		Dim containedSmartPart As ControlSmartPart = TryCast(utilityInstance.SmartPart, ControlSmartPart)
		Assert.IsTrue(containedSmartPart.Visible)
		Assert.IsTrue(utilityInstance.StateChanged)
	End Sub

	<TestMethod()> _
	Public Sub RemovingZoneFromWorkspaceRemovesContainedSmartPart()

		Dim zone As Control = New Control()
		workspace.Controls.Add(zone)

		workspace.SetZoneName(zone, "TestZone3")

		Dim smartPartA As ControlSmartPart = New ControlSmartPart()
		Dim spInfoA As ZoneSmartPartInfo = New ZoneSmartPartInfo()
		spInfoA.ZoneName = "TestZone3"

		workspace.Show(smartPartA, spInfoA)
		zone.Focus()
		Dim f As Form = New Form()
		f.Controls.Add(zone)

		'Fails
		Assert.IsFalse(workspace.SmartParts.Contains(smartPartA))
	End Sub

	<TestMethod()> _
	Public Sub RemovingControlChainUnregistersZone()
		Dim parent As Control = New Control()
		Dim zone1 As Control = New Control()
		parent.Controls.Add(zone1)
		workspace.Controls.Add(parent)
		workspace.SetZoneName(zone1, "TestZone")

		workspace.Controls.Remove(parent)

		Assert.IsNull(workspace.GetZoneName(zone1))
		Assert.AreEqual(0, workspace.Zones.Count)
	End Sub

	<TestMethod()> _
	Public Sub SetZoneWithMultipleNames()
		Dim zone As Control = New Control()
		workspace.Controls.Add(zone)

		workspace.SetZoneName(zone, "TestZone")
		workspace.SetZoneName(zone, "TestZone1")
		workspace.SetZoneName(zone, "TestZone2")
		workspace.SetZoneName(zone, "TestZone3")

		Assert.AreEqual("TestZone3", workspace.GetZoneName(zone))
		Assert.AreEqual(1, workspace.Zones.Count)
	End Sub

	<TestMethod()> _
	Public Sub RemovingControlChainUnregistersZones()
		Dim parent As Control = New Control()
		Dim zone1 As Control = New Control()
		Dim zone2 As Control = New Control()

		parent.Controls.Add(zone1)
		parent.Controls.Add(zone2)

		workspace.Controls.Add(parent)
		workspace.SetZoneName(zone1, "TestZone")
		workspace.SetZoneName(zone2, "TestZone2")

		workspace.Controls.Remove(parent)

		Assert.IsNull(workspace.GetZoneName(zone1))
		Assert.AreEqual(0, workspace.Zones.Count)
	End Sub

	<TestMethod()> _
	Public Sub ShowGetsSmartPartInfoRegisteredWithWorkItem1()
		Dim smartPartA As ControlSmartPart = New ControlSmartPart()

		Dim smartPartInfoA As ZoneSmartPartInfo = New ZoneSmartPartInfo()
		smartPartInfoA.ZoneName = "ZoneA"
		smartPartInfoA.Dock = DockStyle.Left

		workItem.RegisterSmartPartInfo(smartPartA, smartPartInfoA)

		Dim zoneA As Control = New Control()
		workspace.Controls.Add(zoneA)
		workspace.SetZoneName(zoneA, "ZoneA")

		workspace.Show(smartPartA)

		Assert.IsTrue(workspace.Zones("ZoneA").Controls.Contains(smartPartA))
		Assert.AreEqual(DockStyle.Left, smartPartA.Dock)
	End Sub

	<TestMethod()> _
	Public Sub FocusOnInnerControlActivatesContainingSmartPart()
		Dim form As ZoneWorkspaceForm = New ZoneWorkspaceForm()
		form.Show()

		Dim sp1 As ControlSmartPart = New ControlSmartPart()
		sp1.Size = New System.Drawing.Size(50, 50)
		Dim tb1 As TextBox = New TextBox()
		sp1.Controls.Add(tb1)

		Dim sp2 As ControlSmartPart = New ControlSmartPart()
		sp2.Size = New System.Drawing.Size(50, 50)
		Dim tb2 As TextBox = New TextBox()
		sp2.Controls.Add(tb2)

		form.Workspace.Show(sp1, New ZoneSmartPartInfo("LeftZone"))
		form.Workspace.Show(sp2, New ZoneSmartPartInfo("ContentZone"))

		Assert.AreSame(sp2, form.Workspace.ActiveSmartPart)
		tb1.Select()
		Assert.AreSame(sp1, form.Workspace.ActiveSmartPart)
		tb2.Select()
		Assert.AreSame(sp2, form.Workspace.ActiveSmartPart)
	End Sub

	<TestMethod()> _
	Public Sub ShowWithNoSPIDoesNotOverrideDockInformation()
		Dim sp As ControlSmartPart = New ControlSmartPart()
		sp.Dock = DockStyle.Fill

		AddZones(New Control(), New Control())
		workspace.Show(sp)

		Assert.AreEqual(DockStyle.Fill, sp.Dock)
	End Sub

	<TestMethod()> _
	Public Sub FiresOneEventOnlyIfSmartPartIsShownMultipleTimes()
		' Show First SmartPart
		Dim smartPartA As ControlSmartPart = New ControlSmartPart()
		Dim smartPartInfoA As ZoneSmartPartInfo = New ZoneSmartPartInfo()
		smartPartInfoA.ZoneName = "Zone"

		Dim zone As Control = New Control()
		workspace.Controls.Add(zone)
		workspace.SetZoneName(zone, "Zone")

		workspace.Show(smartPartA, smartPartInfoA)
		Assert.IsTrue(smartPartA.Visible)

		' Show Second SmartPart
		Dim smartPartB As ControlSmartPart = New ControlSmartPart()
		Dim smartPartInfoB As ZoneSmartPartInfo = New ZoneSmartPartInfo()
		smartPartInfoB.ZoneName = "Zone1"

		Dim zone1 As Control = New Control()
		workspace.Controls.Add(zone1)
		workspace.SetZoneName(zone1, "Zone1")

		workspace.Show(smartPartB, smartPartInfoB)
		Assert.IsTrue(smartPartB.Visible)

		' Show first SmartPart again
		Dim utilityInstance As FiresCounterUtility(Of WorkspaceEventArgs) = _
			New FiresCounterUtility(Of WorkspaceEventArgs)(0)
		AddHandler workspace.SmartPartActivated, AddressOf utilityInstance.EventHandler

		workspace.Show(smartPartA, smartPartInfoA)
		Assert.AreEqual(1, utilityInstance.Activated)
	End Sub

	<TestMethod()> _
Public Sub DesignTimeControlsProcessedAtEndInit()
		Dim utilityInstance As StateChangedUtility(Of WorkspaceEventArgs) = _
			New StateChangedUtility(Of WorkspaceEventArgs)(False)
		AddHandler workspace.SmartPartActivated, AddressOf utilityInstance.EventHandler

		Dim space As ISupportInitialize = DirectCast(workspace, ISupportInitialize)
		space.BeginInit()

		Dim zone As Control = New Control()
		workspace.Controls.Add(zone)
		workspace.SetZoneName(zone, "Zone")

		zone.Controls.Add(New MonthCalendar())


		space.EndInit()

		Assert.AreEqual(1, workspace.SmartParts.Count)
		Assert.IsTrue(utilityInstance.StateChanged)
	End Sub

	<TestMethod()> _
		Public Sub DesignTimeControlsOnlyFirstLevelProcessed()
		Dim space As ISupportInitialize = DirectCast(workspace, ISupportInitialize)
		space.BeginInit()

		Dim zone As Control = New Control()
		workspace.Controls.Add(zone)
		workspace.SetZoneName(zone, "Zone")

		Dim sp As Control = New Control()
		Dim inner As Control = New Control()
		sp.Controls.Add(inner)

		zone.Controls.Add(sp)

		space.EndInit()

		Assert.AreEqual(1, workspace.SmartParts.Count)

		inner.Dispose()
		Assert.AreEqual(1, workspace.SmartParts.Count)
	End Sub

	<TestMethod()> _
	Public Sub RemovingControlChainUnregistersZone1()
		Dim parent As Control = New Control()
		Dim zone1 As Control = New Control()

		parent.Controls.Add(zone1)

		workspace.Controls.Add(parent)
		workspace.SetZoneName(zone1, "TestZone")

		workspace.Controls.Remove(parent)

		' Debug here. It goes in the OnZoneParentChanged method of ZoneWorkspace
		parent.Controls.Remove(zone1)

		Assert.IsNull(workspace.GetZoneName(zone1))
		Assert.AreEqual(0, workspace.Zones.Count)
	End Sub

	Private Shared Sub AddZones(ByVal zone As Control, ByVal zone1 As Control)
		workspace.Controls.Add(zone)
		workspace.Controls.Add(zone1)
		workspace.SetZoneName(zone, "Main")
		workspace.SetZoneName(zone1, "Main1")
	End Sub

	Private Shared Function CreateFormAddWorkspace() As ZoneWorkspaceForm
		Dim form As ZoneWorkspaceForm = New ZoneWorkspaceForm()
		form.Controls.Add(workspace)
		Return form
	End Function


	<SmartPart()> _
	Public Class ControlSmartPart : Inherits Control
	End Class
End Class
