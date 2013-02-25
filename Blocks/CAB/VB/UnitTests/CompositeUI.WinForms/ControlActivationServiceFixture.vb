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


<TestClass()> _
Public Class ControlActivationServiceFixture

	<TestMethod()> _
	Public Sub ServiceSuccessfullyHooksEventWhenAdded()
		Dim workItem As WorkItem = New TestableRootWorkItem()
		workItem.Services.Add(GetType(IWorkItemActivationService), New SimpleWorkItemActivationService())
		Dim service As IControlActivationService = workItem.Services.Get(Of IControlActivationService)()
		Dim utilityInstance As StateChangedUtility(Of EventArgs) = _
			New StateChangedUtility(Of EventArgs)(False)
		AddHandler workItem.Activated, AddressOf utilityInstance.EventHandler
		Dim view As UserControl = New UserControl()
		workItem.Items.Add(view)
		service.ControlAdded(view)

		Dim f As Form = New Form()
		f.Controls.Add(view)

		f.Show()

		Assert.IsTrue(utilityInstance.StateChanged, "Control.Enter didn't cause WorkItem.Activate to be called")
	End Sub

	<TestMethod()> _
	Public Sub ContainerWorkItemIsActivated()
		Dim activation As ActivationServiceMock = New ActivationServiceMock()
		Dim item As WorkItem = New TestableRootWorkItem()
		item.Services.Add(GetType(IWorkItemActivationService), activation)
		Dim ctrl As MyControl = New MyControl()

		Dim svc As IControlActivationService = item.Services.Get(Of IControlActivationService)()
		item.Items.Add(ctrl)
		svc.ControlAdded(ctrl)

		ctrl.FireEnter()

		Assert.AreEqual(WorkItemStatus.Active, item.Status)
	End Sub


	<TestMethod()> _
	Public Sub DisposedControlDoesNotActivateTheWorkItem()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim testControl As MyControl = New MyControl()
		workItem.Items.Add(testControl)

		testControl.Dispose()

		testControl.FireEnter()
		Assert.IsFalse(workItem.Status = WorkItemStatus.Active)
	End Sub


	<TestMethod()> _
	Public Sub RemovedControlDoesNotActiveteTheWorkItem()
		Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
		Dim testControl As MyControl = New MyControl()
		workItem.Items.Add(testControl)

		workItem.Items.Remove(testControl)

		testControl.FireEnter()
		Assert.IsFalse(workItem.Status = WorkItemStatus.Active)
	End Sub

	Private Class MyControl : Inherits Control
		Public Sub FireEnter()
			OnEnter(EventArgs.Empty)
		End Sub

		Protected Overrides Sub OnEnter(ByVal e As EventArgs)
			MyBase.OnEnter(e)
		End Sub
	End Class

	Private Class ActivationServiceMock
		Implements IWorkItemActivationService

		Public Sub ChangeStatus(ByVal item As WorkItem) Implements IWorkItemActivationService.ChangeStatus
		End Sub
	End Class
End Class
