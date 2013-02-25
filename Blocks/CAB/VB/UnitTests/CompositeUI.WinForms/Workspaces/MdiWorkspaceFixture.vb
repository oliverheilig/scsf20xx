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
Imports System.Drawing
Imports System.Reflection
Imports System.IO


<TestClass()> _
Public Class MdiWorkspaceFixture
	<TestMethod()> _
	Public Sub MDIWorkspaceIsMDIContainer()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim parentForm As Form = workItem.Items.AddNew(Of Form)()
		Dim workspace As MdiWorkspace = New MdiWorkspace(parentForm)
		workItem.Workspaces.Add(workspace)
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		Assert.IsTrue(workspace.ParentMdiForm.IsMdiContainer)
	End Sub

#Region "Show"

	<TestMethod()> _
	Public Sub ShowShowsNewMDIChildWithSmartPart()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim parentForm As Form = workItem.Items.AddNew(Of Form)()
		Dim workspace As MdiWorkspace = New MdiWorkspace(parentForm)
		workItem.Workspaces.Add(workspace)
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		workspace.Show(smartPart)

		Assert.AreEqual(1, workspace.ParentMdiForm.MdiChildren.Length)
		Assert.IsTrue(workspace.Windows(smartPart).IsMdiChild)
		Assert.AreEqual(smartPart, workspace.ParentMdiForm.MdiChildren(0).Controls(0))
	End Sub

	<TestMethod()> _
	Public Sub ShowSizesFormCorrectly()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim parentForm As Form = workItem.Items.AddNew(Of Form)()
		Dim workspace As MdiWorkspace = New MdiWorkspace(parentForm)
		workItem.Workspaces.Add(workspace)
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		smartPart.Size = New System.Drawing.Size(300, 200)
		workspace.Show(smartPart)

		Assert.AreEqual(300, workspace.ParentMdiForm.MdiChildren(0).Size.Width)
		Assert.AreEqual(220, workspace.ParentMdiForm.MdiChildren(0).Size.Height)
	End Sub

	<TestMethod()> _
	Public Sub ShowSetTextOnFormFromSPInfo()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim parentForm As Form = workItem.Items.AddNew(Of Form)()
		Dim workspace As MdiWorkspace = New MdiWorkspace(parentForm)
		workItem.Workspaces.Add(workspace)
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		Dim info As ISmartPartInfo = New WindowSmartPartInfo()
		info.Title = "Smart Part"
		workItem.RegisterSmartPartInfo(smartPart, info)

		workspace.Show(smartPart)

		Assert.AreEqual("Smart Part", workspace.ParentMdiForm.MdiChildren(0).Text)
	End Sub

	<TestMethod()> _
	Public Sub ShowSetTextFromWindowSPInfo()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim parentForm As Form = workItem.Items.AddNew(Of Form)()
		Dim workspace As MdiWorkspace = New MdiWorkspace(parentForm)
		workItem.Workspaces.Add(workspace)
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		Dim info As WindowSmartPartInfo = New WindowSmartPartInfo()
		info.Title = "SP"

		workspace.Show(smartPart, info)

		Assert.AreEqual("SP", workspace.ParentMdiForm.MdiChildren(0).Text)
	End Sub

	<TestMethod()> _
	Public Sub ShowSmartpartTwice()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim parentForm As Form = workItem.Items.AddNew(Of Form)()
		Dim workspace As MdiWorkspace = New MdiWorkspace(parentForm)
		workItem.Workspaces.Add(workspace)
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		parentForm.Show()
		workspace.Show(smartPart)
		workspace.Show(smartPart)

		Assert.IsTrue(smartPart.Visible)
	End Sub

	<TestMethod()> _
	Public Sub ShowingFiresActivatedEvent()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim parentForm As Form = workItem.Items.AddNew(Of Form)()
		Dim workspace As MdiWorkspace = New MdiWorkspace(parentForm)
		workItem.Workspaces.Add(workspace)
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		Dim utilityInstance As StateChangedUtility(Of WorkspaceEventArgs) = _
			New StateChangedUtility(Of WorkspaceEventArgs)(False)
		parentForm.Show()
		AddHandler workspace.SmartPartActivated, AddressOf utilityInstance.EventHandler

		workspace.Show(smartPart)

		Assert.IsTrue(utilityInstance.StateChanged)
	End Sub

	<TestMethod()> _
	Public Sub ShowingFiresActivatedWithSmartPart()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim parentForm As Form = workItem.Items.AddNew(Of Form)()
		Dim workspace As MdiWorkspace = New MdiWorkspace(parentForm)
		workItem.Workspaces.Add(workspace)
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		Dim utilityInstance As FiresSmartPartUtility(Of WorkspaceEventArgs) = _
			New FiresSmartPartUtility(Of WorkspaceEventArgs)(Nothing)
		parentForm.Show()
		AddHandler workspace.SmartPartActivated, AddressOf utilityInstance.EventHandler

		workspace.Show(smartPart)

		Assert.AreEqual(smartPart, utilityInstance.SmartPart)
	End Sub

	<TestMethod()> _
	Public Sub ShowExistingFormShouldBringToFront()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim parentForm As Form = workItem.Items.AddNew(Of Form)()
		Dim workspace As MdiWorkspace = New MdiWorkspace(parentForm)
		workItem.Workspaces.Add(workspace)

		parentForm.Show()
		Dim smartPart As MockSmartPart = New MockSmartPart()
		Dim smartPart2 As MockSmartPart = New MockSmartPart()

		workspace.Show(smartPart)
		workspace.Show(smartPart2)

		workspace.Show(smartPart)

		Assert.IsTrue(smartPart.Focused)
	End Sub

	<TestMethod()> _
	Public Sub CanSpecifySizeOnShow()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim parentForm As Form = workItem.Items.AddNew(Of Form)()
		Dim workspace As MdiWorkspace = New MdiWorkspace(parentForm)
		workItem.Workspaces.Add(workspace)
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		Dim info As WindowSmartPartInfo = New WindowSmartPartInfo()
		info.Title = "Mock Smart Part"
		info.Width = 300
		info.Height = 400

		workspace.Show(smartPart, info)

		Assert.AreEqual(300, workspace.Windows(smartPart).Width)
		Assert.AreEqual(400, workspace.Windows(smartPart).Height)
	End Sub

	<TestMethod()> _
	Public Sub CanSpecifyLocationOnShow()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim parentForm As Form = workItem.Items.AddNew(Of Form)()
		Dim workspace As MdiWorkspace = New MdiWorkspace(parentForm)
		workItem.Workspaces.Add(workspace)
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		Dim info As WindowSmartPartInfo = New WindowSmartPartInfo()
		info.Title = "Mock Smart Part"
		info.Location = New Point(10, 50)

		workspace.Show(smartPart, info)

		Assert.AreEqual(10, workspace.Windows(smartPart).Location.X)
		Assert.AreEqual(50, workspace.Windows(smartPart).Location.Y)
	End Sub

	<TestMethod()> _
	Public Sub CanSetWindowOptionsOnShow()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim parentForm As Form = workItem.Items.AddNew(Of Form)()
		Dim workspace As MdiWorkspace = New MdiWorkspace(parentForm)
		workItem.Workspaces.Add(workspace)
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		Dim info As WindowSmartPartInfo = New WindowSmartPartInfo()
		info.Title = "Mock Smart Part"
		info.ControlBox = False
		info.MinimizeBox = False
		info.MaximizeBox = False
		Dim icon As Icon = Nothing
		Dim asm As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()

		Using imgStream As Stream = asm.GetManifestResourceStream("Microsoft.Practices.CompositeUI.WinForms.Tests.test.ico")
			icon = New Icon(imgStream)
		End Using
		info.Icon = icon

		workspace.Show(smartPart, info)

		Assert.IsFalse(workspace.Windows(smartPart).ControlBox)
		Assert.IsFalse(workspace.Windows(smartPart).MinimizeBox)
		Assert.IsFalse(workspace.Windows(smartPart).MaximizeBox)
		Assert.AreSame(icon, workspace.Windows(smartPart).Icon)
	End Sub

	<TestMethod()> _
	Public Sub CanShowWithNonWindowSPI()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim parentForm As Form = workItem.Items.AddNew(Of Form)()
		Dim workspace As MdiWorkspace = New MdiWorkspace(parentForm)
		workItem.Workspaces.Add(workspace)
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		Dim info As SmartPartInfo = New SmartPartInfo()
		info.Title = "Foo"

		workspace.Show(smartPart, info)
	End Sub

	<TestMethod()> _
	Public Sub UsesSPInfoIfNoWindowSPInfoExists()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim parentForm As Form = workItem.Items.AddNew(Of Form)()
		Dim workspace As MdiWorkspace = New MdiWorkspace(parentForm)
		workItem.Workspaces.Add(workspace)
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		Dim info As SmartPartInfo = New SmartPartInfo()
		info.Title = "Foo"
		workItem.RegisterSmartPartInfo(smartPart, info)

		workspace.Show(smartPart)

		Assert.AreEqual("Foo", workspace.Windows(smartPart).Text)
	End Sub

	<TestMethod()> _
	Public Sub CloseSmartPartDoesNotDisposeIt()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As MdiWorkspace = workItem.Workspaces.AddNew(Of MdiWorkspace)()
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		workspace.Show(smartPart)
		workspace.Close(smartPart)

		Assert.IsFalse(smartPart.IsDisposed)
	End Sub

#End Region

#Region "Hide"

	<TestMethod()> _
	Public Sub CanHideFormWithSmartPart()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim parentForm As Form = workItem.Items.AddNew(Of Form)()
		Dim workspace As MdiWorkspace = New MdiWorkspace(parentForm)
		workItem.Workspaces.Add(workspace)
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		workspace.Show(smartPart)
		workspace.Hide(smartPart)

		Assert.IsFalse(smartPart.Visible)
		Assert.IsFalse(workspace.Windows(smartPart).Visible)
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentException))> _
	Public Sub HideNonExistSmartPartThrows()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim parentForm As Form = workItem.Items.AddNew(Of Form)()
		Dim workspace As MdiWorkspace = New MdiWorkspace(parentForm)
		workItem.Workspaces.Add(workspace)
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		workspace.Hide(smartPart)
	End Sub

#End Region

#Region "Close"

	<TestMethod()> _
	Public Sub CanCloseMdiChild()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim parentForm As Form = workItem.Items.AddNew(Of Form)()
		Dim workspace As MdiWorkspace = New MdiWorkspace(parentForm)
		workItem.Workspaces.Add(workspace)
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		workspace.Show(smartPart)
		Dim form As Form = workspace.Windows(smartPart)

		workspace.Close(smartPart)

		Assert.IsTrue(form.IsDisposed)
		Assert.IsFalse(smartPart.IsDisposed)
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentException))> _
	Public Sub CloseNonExistSmartPartThrows()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim parentForm As Form = workItem.Items.AddNew(Of Form)()
		Dim workspace As MdiWorkspace = New MdiWorkspace(parentForm)
		workItem.Workspaces.Add(workspace)
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		workspace.Close(smartPart)
	End Sub

	<TestMethod()> _
	Public Sub WorkspaceFiresSmartPartClosing()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim parentForm As Form = workItem.Items.AddNew(Of Form)()
		Dim workspace As MdiWorkspace = New MdiWorkspace(parentForm)
		workItem.Workspaces.Add(workspace)
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		Dim utilityInstance As StateChangedUtility(Of WorkspaceCancelEventArgs) = _
			New StateChangedUtility(Of WorkspaceCancelEventArgs)(False)
		parentForm.Show()
		workspace.Show(smartPart)
		AddHandler workspace.SmartPartClosing, AddressOf utilityInstance.EventHandler

		workspace.Close(smartPart)

		Assert.IsTrue(utilityInstance.StateChanged)
	End Sub

	<TestMethod()> _
	Public Sub CanCancelSmartPartClosing()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim parentForm As Form = workItem.Items.AddNew(Of Form)()
		Dim workspace As MdiWorkspace = New MdiWorkspace(parentForm)
		workItem.Workspaces.Add(workspace)
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		parentForm.Show()
		workspace.Show(smartPart)
		Dim utilityInstance As Utility = New Utility()
		AddHandler workspace.SmartPartClosing, AddressOf utilityInstance.EventHandler

		workspace.Close(smartPart)

		Assert.IsFalse(smartPart.IsDisposed, "Smart Part Was Disposed")
	End Sub

#End Region

	<TestMethod()> _
	Public Sub SettingFocusOnWindowFiresActivated()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim parentForm As Form = workItem.Items.AddNew(Of Form)()
		Dim workspace As MdiWorkspace = New MdiWorkspace(parentForm)
		workItem.Workspaces.Add(workspace)
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		Dim utilityInstance As FiresSmartPartUtility(Of WorkspaceEventArgs) = _
			New FiresSmartPartUtility(Of WorkspaceEventArgs)(Nothing)
		parentForm.Show()
		Dim sp1 As MockSmartPart = New MockSmartPart()
		workspace.Show(sp1)
		workspace.Show(smartPart)

		AddHandler workspace.SmartPartActivated, AddressOf utilityInstance.EventHandler

		workspace.Windows(sp1).Focus()

		Assert.AreEqual(sp1, utilityInstance.SmartPart)
	End Sub

	<TestMethod()> _
	Public Sub WindowActivatedFiresCorrectNumberOfTimes()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim parentForm As Form = workItem.Items.AddNew(Of Form)()
		Dim workspace As MdiWorkspace = New MdiWorkspace(parentForm)
		workItem.Workspaces.Add(workspace)
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		Dim utilityInstance As FiresCounterUtility(Of WorkspaceEventArgs) = _
			New FiresCounterUtility(Of WorkspaceEventArgs)(0)
		parentForm.Show()
		Dim sp1 As MockSmartPart = New MockSmartPart()
		AddHandler workspace.SmartPartActivated, AddressOf utilityInstance.EventHandler

		workspace.Show(sp1)
		workspace.Show(smartPart)
		workspace.Windows(sp1).Focus()

		Assert.AreEqual(3, utilityInstance.Activated)
	End Sub

#Region "Helper classes"

	<SmartPart()> _
	Private Class MockSmartPart : Inherits Control
		Private info As SmartPartInfo = New SmartPartInfo()

		Public Sub New()
			info.Title = "Smart Part"
		End Sub
	End Class

#End Region
End Class

