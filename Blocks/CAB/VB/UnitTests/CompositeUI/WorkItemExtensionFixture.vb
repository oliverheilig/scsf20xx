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
Imports Microsoft.Practices.CompositeUI.Services


<TestClass()> _
Public Class WorkItemExtensionFixture
	<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
	Public Sub ThrowsIfWorkItemNull()
		Dim extension As MockExtension = New MockExtension()

		extension.Initialize(Nothing)
	End Sub

	<TestMethod()> _
	Public Sub VirtualMethodsAreCalled()
		Dim extensions As IWorkItemExtensionService = New WorkItemExtensionService()
		Dim parentWorkItem As WorkItem = New TestableRootWorkItem()
		parentWorkItem.Services.Add(GetType(IWorkItemActivationService), New SimpleWorkItemActivationService())
		parentWorkItem.Services.Add(GetType(IWorkItemExtensionService), extensions)
		extensions.RegisterExtension(GetType(WorkItem), GetType(MockExtension))

		Dim childWorkItem As WorkItem = parentWorkItem.WorkItems.AddNew(Of WorkItem)()
		childWorkItem.Activate()
		childWorkItem.Run()
		childWorkItem.Deactivate()
		childWorkItem.Terminate()

		Assert.IsTrue(MockExtension.InitializedCalled)
		Assert.IsTrue(MockExtension.ActivatedCalled)
		Assert.IsTrue(MockExtension.DeactivatedCalled)
		Assert.IsTrue(MockExtension.RunStartedCalled)
		Assert.IsTrue(MockExtension.TerminatedCalled)
	End Sub


	<TestMethod()> _
	Public Sub ExtensionMarkedForRootWorkItemAreAppliedOnlyToRoot()
		Dim rootWorkItem As WorkItem = New MyTestableRootWorkItem()

		Assert.IsTrue(MockExtension.InitializedCalled)
		MockExtension.InitializedCalled = False

		Dim child As WorkItem = rootWorkItem.WorkItems.AddNew(Of WorkItem)()

		Assert.AreEqual(1, RootWorkItemExtension.OnInializedCount)
		Assert.AreSame(rootWorkItem, RootWorkItemExtension.InitializedWorkItem)
		Assert.IsTrue(MockExtension.InitializedCalled)
	End Sub

#Region "Helper classes"

	Private Class MyTestableRootWorkItem : Inherits TestableRootWorkItem
		Protected Overrides Sub TestableAddServices()
			Dim extensions As IWorkItemExtensionService = Services.AddNew(Of WorkItemExtensionService, IWorkItemExtensionService)()
			extensions.RegisterRootExtension(GetType(RootWorkItemExtension))
			extensions.RegisterExtension(GetType(WorkItem), GetType(MockExtension))
			FinishInitialization()
		End Sub
	End Class

	Private Class MockExtension : Inherits WorkItemExtension
		Public Shared ActivatedCalled As Boolean = False
		Public Shared DeactivatedCalled As Boolean = False
		Public Shared InitializedCalled As Boolean = False
		Public Shared RunStartedCalled As Boolean = False
		Public Shared TerminatedCalled As Boolean = False

		Protected Overrides Sub OnActivated()
			ActivatedCalled = True
		End Sub

		Protected Overrides Sub OnDeactivated()
			DeactivatedCalled = True
		End Sub

		Protected Overrides Sub OnInitialized()
			InitializedCalled = True
		End Sub

		Protected Overrides Sub OnRunStarted()
			RunStartedCalled = True
		End Sub

		Protected Overrides Sub OnTerminated()
			TerminatedCalled = True
		End Sub
	End Class

	<RootWorkItemExtension()> _
	Private Class RootWorkItemExtension : Inherits WorkItemExtension
		Public Shared OnInializedCount As Integer = 0
		Public Shared InitializedWorkItem As WorkItem

		Protected Overrides Sub OnInitialized()
			OnInializedCount += 1
			InitializedWorkItem = MyBase.WorkItem
			MyBase.OnInitialized()
		End Sub
	End Class

#End Region
End Class
