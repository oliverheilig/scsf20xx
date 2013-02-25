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
Imports System.Runtime.InteropServices
Imports System.Threading
Imports System.Windows.Forms
Imports Microsoft.Practices.CompositeUI.SmartParts
Imports System.ComponentModel
Imports System.Drawing
Imports System.Reflection
Imports System.IO


<TestClass()> _
Public Class WindowWorkspaceFixture

	Private Const WM_SYSCOMMAND As Integer = &H112
	Private Const SC_CLOSE As Integer = &HF060

	<CLSCompliant(False)> _
	<DllImport("user32.dll")> _
	Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
	End Function

#Region "Show"

	<TestMethod()> _
	Public Sub ShowShowsNewFormWithControl()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		workspace.Show(smartPart)

		Dim form As Form = workspace.Windows(smartPart)
		Assert.AreSame(smartPart, form.Controls(0))
		Assert.IsTrue(workspace.Windows(smartPart).Visible)
		Assert.IsTrue(smartPart.Visible)
	End Sub

	<TestMethod()> _
	Public Sub ShowShowsNewFormWithOwnerAndControl()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		Dim owner As Form = New Form()
		Dim ws As WindowWorkspace = New WindowWorkspace(owner)

		ws.Show(smartPart)

		Dim form As Form = ws.Windows(smartPart)
		Assert.AreSame(owner, form.Owner)
	End Sub

	<TestMethod()> _
	Public Sub ShowingSetFormTextFromWindowSmartPartInfo()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		Dim info As WindowSmartPartInfo = New WindowSmartPartInfo()
		info.Title = "Mock Smart Part"
		workItem.RegisterSmartPartInfo(smartPart, info)
		workspace.Show(smartPart, info)

		Assert.AreEqual("Mock Smart Part", workspace.Windows(smartPart).Text)
	End Sub

	<TestMethod()> _
	Public Sub ShowingSmartPartISPInfoProviderSetFormText()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		Dim info As ISmartPartInfo = New WindowSmartPartInfo()
		info.Title = "Smart Part"
		workItem.RegisterSmartPartInfo(smartPart, info)

		workspace.Show(smartPart)

		Assert.AreEqual("Smart Part", workspace.Windows(smartPart).Text)
	End Sub

#Region "Utility code for CanShowModal"

	Private Class CanShowModalUtility
		Public Workspace As WindowWorkspace
		Public Info As WindowSmartPartInfo
		Public SmartPart As MockSmartPart

		Public Sub New(ByVal workspace As WindowWorkspace, ByVal info As WindowSmartPartInfo, ByVal smartPart As MockSmartPart)
			Me.Workspace = workspace
			Me.Info = info
			Me.SmartPart = smartPart
		End Sub

		Public Sub ThreadMethod()
			Workspace.Show(SmartPart, Info)
		End Sub
	End Class

#End Region

	<TestMethod()> _
	Public Sub CanShowModal()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		Dim info As WindowSmartPartInfo = New WindowSmartPartInfo()
		info.Title = "Mock Smart Part"
		info.Modal = True

		Dim utilityInstance As CanShowModalUtility = _
			New CanShowModalUtility(workspace, info, smartPart)
		Dim execThread As Thread = New Thread(New ThreadStart(AddressOf utilityInstance.ThreadMethod))

		Try
			execThread.Start()
			Thread.Sleep(1000)

			Assert.IsTrue(workspace.Windows(smartPart).Visible)
		Finally
			SendMessage(workspace.Windows(smartPart).Handle, WM_SYSCOMMAND, SC_CLOSE, 0)
			execThread.Join()
		End Try
	End Sub

	<TestMethod()> _
	Public Sub CanShowModalWithOwner()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim owner As Form = New Form()
		Dim workspace As WindowWorkspace = New WindowWorkspace(owner)
		workItem.Workspaces.Add(workspace)
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		Dim info As WindowSmartPartInfo = New WindowSmartPartInfo()
		info.Title = "Mock Smart Part"
		info.Modal = True

		Dim utilityInstance As CanShowModalUtility = _
			New CanShowModalUtility(workspace, info, smartPart)
		Dim execThread As Thread = New Thread(New ThreadStart(AddressOf utilityInstance.ThreadMethod))

		Try
			execThread.Start()
			Thread.Sleep(1000)

			Assert.IsTrue(workspace.Windows(smartPart).Visible)
			Assert.AreSame(owner, workspace.Windows(smartPart).Owner)
		Finally
			SendMessage(workspace.Windows(smartPart).Handle, WM_SYSCOMMAND, SC_CLOSE, 0)
			execThread.Join()
		End Try
	End Sub

	<TestMethod()> _
	Public Sub CanShowNonModal()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		Dim info As WindowSmartPartInfo = New WindowSmartPartInfo()
		info.Title = "Mock Smart Part"
		info.Modal = False

		workspace.Show(smartPart, info)

		Assert.IsTrue(workspace.Windows(smartPart).Visible)
	End Sub

	<TestMethod()> _
	Public Sub FormSizeIsCorrectSize()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		smartPart.Size = New System.Drawing.Size(150, 125)

		workspace.Show(smartPart)

		Assert.AreEqual(150, workspace.Windows(smartPart).Size.Width)
		Assert.AreEqual(145, workspace.Windows(smartPart).Size.Height)
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentException))> _
	Public Sub ShowingNonControlThrows()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()

		workspace.Show(New Object())
	End Sub

	<TestMethod()> _
	Public Sub ShowingFiresActivatedEvent()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		Dim utilityInstance As StateChangedUtility(Of WorkspaceEventArgs) = _
			New StateChangedUtility(Of WorkspaceEventArgs)(False)

		AddHandler workspace.SmartPartActivated, AddressOf utilityInstance.EventHandler
		workspace.Show(smartPart)

		Assert.IsTrue(utilityInstance.StateChanged)
	End Sub

	<TestMethod()> _
	Public Sub ShowingFiresActivatedWithSmartPart()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		Dim utilityInstance As FiresSmartPartUtility(Of WorkspaceEventArgs) = _
			New FiresSmartPartUtility(Of WorkspaceEventArgs)(Nothing)

		AddHandler workspace.SmartPartActivated, AddressOf utilityInstance.EventHandler
		workspace.Show(smartPart)

		Assert.AreEqual(smartPart, utilityInstance.SmartPart)
	End Sub

	<TestMethod()> _
	Public Sub SettingFocusOnWindowFiresActivated()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		Dim utilityInstance As FiresSmartPartUtility(Of WorkspaceEventArgs) = _
			New FiresSmartPartUtility(Of WorkspaceEventArgs)(Nothing)
		Dim smartPart2 As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()
		workspace.Show(smartPart2)
		workspace.Show(smartPart)

		AddHandler workspace.SmartPartActivated, AddressOf utilityInstance.EventHandler

		workspace.Windows(smartPart2).Focus()

		Assert.AreEqual(smartPart2, utilityInstance.SmartPart)
	End Sub

	<TestMethod()> _
	Public Sub WindowActivatedFiresCorrectNumberOfTimes()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		Dim utilityInstance As FiresCounterUtility(Of WorkspaceEventArgs) = _
			New FiresCounterUtility(Of WorkspaceEventArgs)(0)
		Dim smartPart2 As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()
		AddHandler workspace.SmartPartActivated, AddressOf utilityInstance.EventHandler

		workspace.Show(smartPart2)
		workspace.Show(smartPart)
		workspace.Windows(smartPart2).Focus()

		Assert.AreEqual(3, utilityInstance.Activated)
	End Sub

	<TestMethod()> _
	Public Sub ShowExistingFormBringsToFront()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()
		Dim smartPart2 As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		workspace.Show(smartPart)
		workspace.Show(smartPart2)
		workspace.Show(smartPart)

		Assert.IsTrue(smartPart.Focused)
	End Sub

	<TestMethod()> _
	Public Sub CanSpecifySize()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
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
	Public Sub CanSpecifyLocation()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		Dim info As WindowSmartPartInfo = New WindowSmartPartInfo()
		info.Title = "Mock Smart Part"
		info.Location = New Point(10, 50)

		workspace.Show(smartPart, info)

		Assert.AreEqual(10, workspace.Windows(smartPart).Location.X)
		Assert.AreEqual(50, workspace.Windows(smartPart).Location.Y)
	End Sub

	<TestMethod()> _
	Public Sub CanSetWindowOptions()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
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
	Public Sub CanApplyWindowOptions()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		Dim info As WindowSmartPartInfo = New WindowSmartPartInfo()
		info.Title = "Mock Smart Part"
		info.Width = 400

		workspace.Show(smartPart, info)

		Assert.AreEqual(400, workspace.Windows(smartPart).Width)

		info.Width = 500
		workspace.ApplySmartPartInfo(smartPart, info)

		Assert.AreEqual(500, workspace.Windows(smartPart).Width)
	End Sub

	<TestMethod()> _
	Public Sub CanShowIfSPINotWindowSPI()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		Dim info As SmartPartInfo = New SmartPartInfo()
		info.Title = "Foo"

		workspace.Show(smartPart, info)
	End Sub

	<TestMethod()> _
	Public Sub UsesSPInfoIfNoWindowSPInfoExists()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		Dim info As SmartPartInfo = New SmartPartInfo()
		info.Title = "Foo"
		workItem.RegisterSmartPartInfo(smartPart, info)

		workspace.Show(smartPart)

		Assert.AreEqual("Foo", workspace.Windows(smartPart).Text)
	End Sub

	<TestMethod()> _
	Public Sub FiresOneEventOnlyIfSmartPartIsShownMultipleTimes()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPartA As MockSmartPart = New MockSmartPart()
		Dim smartPartB As MockSmartPart = New MockSmartPart()

		workspace.Show(smartPartA)
		workspace.Show(smartPartB)

		Dim utilityInstance As FiresCounterWithSmartPartCheckUtility(Of WorkspaceEventArgs) = _
			New FiresCounterWithSmartPartCheckUtility(Of WorkspaceEventArgs)(0, smartPartA)

		AddHandler workspace.SmartPartActivated, AddressOf utilityInstance.EventHandler

		workspace.Show(smartPartA)

		Assert.AreEqual(1, utilityInstance.Activated)
	End Sub

	<TestMethod()> _
	Public Sub ShowTwiceReusesForm()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPart As MockSmartPart = New MockSmartPart()

		workspace.Show(smartPart)
		workspace.Show(smartPart)

		Assert.AreEqual(1, workspace.Windows.Count)
	End Sub

	<TestMethod()> _
	Public Sub ShowSetsVisible()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()

		Dim smartPart As MockSmartPart = New MockSmartPart()
		smartPart.Visible = False

		workspace.Show(smartPart)

		Assert.IsTrue(smartPart.Visible)
	End Sub

#End Region

#Region "Hide"

	<TestMethod()> _
	Public Sub CanHideWithSmartPart()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		workspace.Show(smartPart)

		workspace.Hide(smartPart)

		Assert.IsFalse(workspace.Windows(smartPart).Visible)
		Assert.IsFalse(smartPart.Visible)
	End Sub

	<TestMethod()> _
	Public Sub CanShowSameWindowAfterHidden()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		workspace.Show(smartPart)
		workspace.Hide(smartPart)

		workspace.Show(smartPart)

		Assert.IsNotNull(workspace.Windows(smartPart))
		Assert.IsTrue(smartPart.Visible)
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentException))> _
	Public Sub HideNonExistControlThrows()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		workspace.Hide(smartPart)
	End Sub

	<TestMethod()> _
	Public Sub HidingSmartPartDoesNotAutomaticallyShowPreviousForm()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPartA As MockSmartPart = New MockSmartPart()
		smartPartA.Visible = False
		Dim smartPartB As MockSmartPart = New MockSmartPart()
		smartPartB.Visible = False

		Dim smartPartInfoB As WindowSmartPartInfo = New WindowSmartPartInfo()
		smartPartInfoB.Title = "Window SmartPart B"

		Dim smartPartInfoA As WindowSmartPartInfo = New WindowSmartPartInfo()
		smartPartInfoA.Title = "Window SmartPart A"

		workspace.Show(smartPartA, smartPartInfoA)
		Assert.IsTrue(smartPartA.Visible)

		' Force the form to non-visible so it doesn't fire
		' his own Activated event after we hide the following 
		' smart part, therefore making the condition impossible 
		' to test.

		workspace.Windows(smartPartA).Hide()

		workspace.Show(smartPartB, smartPartInfoB)
		Assert.IsTrue(smartPartB.Visible)

		workspace.Hide(smartPartB)

		Assert.IsNull(workspace.ActiveSmartPart)
	End Sub

#End Region

#Region "Close"

	<TestMethod()> _
	Public Sub CloseDisposesAndClosesWindow()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		workspace.Show(smartPart)

		Dim form As Form = workspace.Windows(smartPart)
		workspace.Close(smartPart)

		Assert.IsTrue(form.IsDisposed, "Form not disposed")
		Assert.IsFalse(form.Visible, "Form is visible")
		Assert.IsFalse(smartPart.IsDisposed)
	End Sub

	<TestMethod()> _
	Public Sub CloseRemovesEntriesInWindowsAndSmartParts()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		workspace.Show(smartPart)

		workspace.Close(smartPart)

		Assert.AreEqual(0, workspace.Windows.Count)
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentException))> _
	Public Sub CloseNonExistControlThrows()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		workspace.Close(smartPart)
	End Sub

	<TestMethod()> _
	Public Sub WorkspaceFiresSmartPartClosing()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		Dim utilityInstance As StateChangedUtility(Of WorkspaceCancelEventArgs) = _
			New StateChangedUtility(Of WorkspaceCancelEventArgs)(False)
		workspace.Show(smartPart)
		AddHandler workspace.SmartPartClosing, AddressOf utilityInstance.EventHandler

		workspace.Close(smartPart)

		Assert.IsTrue(utilityInstance.StateChanged)
	End Sub

	<TestMethod()> _
	Public Sub CanCancelSmartPartClosing()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		workspace.Show(smartPart)
		Dim utilityInstance As Utility = New Utility
		AddHandler workspace.SmartPartClosing, AddressOf utilityInstance.EventHandler

		workspace.Close(smartPart)

		Assert.IsFalse(smartPart.IsDisposed, "Smart Part Was Disposed")
	End Sub

	<TestMethod()> _
	Public Sub ClosingIsCalledWhenClosed()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		Dim utilityInstance As StateChangedUtility(Of WorkspaceCancelEventArgs) = _
			New StateChangedUtility(Of WorkspaceCancelEventArgs)(False)
		workspace.Show(smartPart)
		AddHandler workspace.SmartPartClosing, AddressOf utilityInstance.EventHandler

		workspace.Windows(smartPart).Close()

		Assert.IsTrue(utilityInstance.StateChanged)
	End Sub

	<TestMethod()> _
	Public Sub ClosingDoesNotFireIfNoControlsOnForm()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		Dim utilityInstance As StateChangedUtility(Of WorkspaceCancelEventArgs) = _
			New StateChangedUtility(Of WorkspaceCancelEventArgs)(False)
		workspace.Show(smartPart)
		AddHandler workspace.SmartPartClosing, AddressOf utilityInstance.EventHandler

		workspace.Windows(smartPart).Controls.Clear()

		Assert.IsFalse(utilityInstance.StateChanged)
	End Sub

	<TestMethod()> _
	Public Sub ClosedIsCalledWhenClosed()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		Dim utilityInstance As StateChangedUtility(Of WorkspaceCancelEventArgs) = _
			New StateChangedUtility(Of WorkspaceCancelEventArgs)(False)
		workspace.Show(smartPart)
		AddHandler workspace.SmartPartClosing, AddressOf utilityInstance.EventHandler

		workspace.Windows(smartPart).Close()

		Assert.IsTrue(utilityInstance.StateChanged)
	End Sub

	<TestMethod()> _
	Public Sub CloseRemovesWindow()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		workspace.Show(smartPart)
		workspace.Close(smartPart)

		Assert.IsFalse(workspace.Windows.ContainsKey(smartPart))
	End Sub

	<TestMethod()> _
	Public Sub ClosedDoesNotFireIfNoControlsOnForm()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		Dim utilityInstance As StateChangedUtility(Of WorkspaceCancelEventArgs) = _
			New StateChangedUtility(Of WorkspaceCancelEventArgs)(False)
		workspace.Show(smartPart)
		AddHandler workspace.SmartPartClosing, AddressOf utilityInstance.EventHandler

		workspace.Windows(smartPart).Controls.Clear()

		Assert.IsFalse(utilityInstance.StateChanged)
	End Sub

	<TestMethod()> _
	Public Sub CanCancelCloseWhenFormClose()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		workspace.Show(smartPart)
		Dim utilityInstance As Utility = New Utility
		AddHandler workspace.SmartPartClosing, AddressOf utilityInstance.EventHandler

		workspace.Windows(smartPart).Close()

		Assert.IsFalse(smartPart.IsDisposed)
	End Sub

	<TestMethod()> _
	Public Sub CloseSmartPartDoesNotDisposeIt()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		workspace.Show(smartPart)
		workspace.Close(smartPart)

		Assert.IsFalse(smartPart.IsDisposed)
	End Sub

#End Region

	<TestMethod()> _
	Public Sub ControlIsRemovedWhenSmartPartIsDisposed()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPart As MockSmartPart = workItem.SmartParts.AddNew(Of MockSmartPart)()

		workspace.Show(smartPart)
		Assert.AreEqual(1, workspace.Windows.Count)

		smartPart.Dispose()

		Assert.AreEqual(0, workspace.Windows.Count)
		Assert.AreEqual(0, workspace.SmartParts.Count)
	End Sub

	<TestMethod()> _
	Public Sub SPIsRemovedFromWorkspaceWhenDisposed()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim workspace As WindowWorkspace = workItem.Workspaces.AddNew(Of WindowWorkspace)()
		Dim smartPartA As MockSmartPart = New MockSmartPart()
		workspace.Show(smartPartA)
		Dim utilityInstance As StateChangedUtility(Of workspacecanceleventargs) = _
			New StateChangedUtility(Of workspacecanceleventargs)(False)
		smartPartA.Dispose()
		Assert.IsFalse(utilityInstance.StateChanged)
		Assert.AreEqual(0, workspace.Windows.Count)
		Assert.AreEqual(0, workspace.SmartParts.Count)
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

