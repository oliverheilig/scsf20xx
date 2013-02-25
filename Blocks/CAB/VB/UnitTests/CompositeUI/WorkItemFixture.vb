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
Imports System.ComponentModel
Imports Microsoft.Practices.CompositeUI.EventBroker
Imports Microsoft.Practices.CompositeUI.Services
Imports Microsoft.Practices.CompositeUI.SmartParts
Imports Microsoft.Practices.CompositeUI.Utility
Imports Microsoft.Practices.ObjectBuilder
Imports System.Windows.Forms


<TestClass()> _
Public Class WorkItemFixture
	' Construction

#Region "Constructors"

	<TestMethod()> _
	Public Sub EmptyConstructorCreatesABuilderAndLocator()
		Dim wi As TestableRootWorkItem = New TestableRootWorkItem()

		Assert.IsNotNull(wi.Builder)
		Assert.IsNotNull(wi.Locator)
	End Sub

#End Region

	' WorkItem hierarchy

#Region "WorkItem hierarchy"

	<TestMethod()> _
	Public Sub GetParentOnChildWorkItemReturnsParentWorkItem()
		Dim parentWorkItem As WorkItem = New TestableRootWorkItem()
		Dim childWorkItem As WorkItem = parentWorkItem.WorkItems.AddNew(Of WorkItem)()

		Assert.AreSame(parentWorkItem, childWorkItem.Parent)
	End Sub

	<TestMethod()> _
	Public Sub GetParentOnRootWorkItemReturnNull()
		Dim wi As WorkItem = New TestableRootWorkItem()

		Assert.IsNull(wi.Parent)
	End Sub

	<TestMethod()> _
	Public Sub CanAddAWorkItemToAWorkItem()
		Dim wi1 As WorkItem = New TestableRootWorkItem()
		Dim wi2 As WorkItem = New WorkItem()

		wi1.WorkItems.Add(wi2)

		Assert.IsTrue(wi1.WorkItems.ContainsObject(wi2))
		Assert.AreSame(wi1, wi2.Parent)
	End Sub

	<TestMethod()> _
	Public Sub UsingCreateToMakeWorkItemProperlySetsParentRelationship()
		Dim parentWorkItem As WorkItem = New TestableRootWorkItem()
		Dim childWorkItem As WorkItem = parentWorkItem.WorkItems.AddNew(Of WorkItem)()

		Assert.AreSame(parentWorkItem, childWorkItem.Parent)
	End Sub

	<TestMethod()> _
	Public Sub UsingCreateToMakeWorkItemDerivedClassProperlySetsParentRelationship()
		Dim parentWorkItem As WorkItem = New TestableRootWorkItem()
		Dim childWorkItem As WorkItem = parentWorkItem.WorkItems.AddNew(Of ChildWorkItem)()

		Assert.AreSame(parentWorkItem, childWorkItem.Parent)
	End Sub

	<TestMethod()> _
	Public Sub CanDiscoverRootWorkItem()
		Dim grandparent As WorkItem = New TestableRootWorkItem()
		Dim parent As WorkItem = grandparent.WorkItems.AddNew(Of WorkItem)()
		Dim child As WorkItem = parent.WorkItems.AddNew(Of WorkItem)()

		Assert.AreSame(grandparent, grandparent.RootWorkItem)
		Assert.AreSame(grandparent, parent.RootWorkItem)
		Assert.AreSame(grandparent, child.RootWorkItem)
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentException))> _
	Public Sub CannotAddWorkItemToItself()
		Dim wi As WorkItem = New TestableRootWorkItem()

		wi.WorkItems.Add(wi)
	End Sub

#End Region

#Region "Disposal"

	<TestMethod()> _
	Public Sub DisposingWorkItemCausesContainedObjectsToBeDisposed()
		Dim wi As WorkItem = New TestableRootWorkItem()
		Dim obj As MockDisposableObject = wi.Items.AddNew(Of MockDisposableObject)()

		wi.Dispose()

		Assert.IsTrue(obj.WasDisposed)
	End Sub

	<TestMethod()> _
	Public Sub TerminatingWorkItemCausesContainedObjectsToBeDisposed()
		Dim wi As WorkItem = New TestableRootWorkItem()
		Dim obj As MockDisposableObject = wi.Items.AddNew(Of MockDisposableObject)()

		wi.Terminate()

		Assert.IsTrue(obj.WasDisposed)
	End Sub

	<TestMethod()> _
	Public Sub DisposingContainerCausesContainedObjectsToBeTornDown()
		Dim wi As TestableRootWorkItem = New TestableRootWorkItem()
		Dim strategy As MockTearDownStrategy = New MockTearDownStrategy()
		wi.Builder.Strategies.Add(strategy, BuilderStage.PreCreation)

		wi.Items.AddNew(Of Object)()
		wi.Dispose()

		Assert.IsTrue(strategy.TearDownCalled)
	End Sub

	<TestMethod()> _
	Public Sub TerminatingWorkItemCausesItToBeRemovedFromParent()
		Dim parent As TestableRootWorkItem = New TestableRootWorkItem()
		Dim child As WorkItem = parent.WorkItems.AddNew(Of WorkItem)()

		Assert.AreEqual(1, parent.WorkItems.Count)
		child.Terminate()
		Assert.AreEqual(0, parent.WorkItems.Count)
	End Sub

#End Region

	' Collections

#Region "Collection Events"

#Region "Utility code for the AddingItemToWorkItemDoesNotFireServicesAddedEvent function"

	Private Class AddingItemToWorkItemDoesNotFireServicesAddedEventUtility

		Private innerAddedCalled As Boolean

		Public Property AddedCalled() As Boolean
			Get
				Return innerAddedCalled
			End Get
			Set(ByVal value As Boolean)
				innerAddedCalled = value
			End Set
		End Property

		Public Sub New(ByVal addedCalled As Boolean)
			innerAddedCalled = addedCalled
		End Sub

		Public Sub EventHandler(ByVal sender As Object, ByVal args As DataEventArgs(Of Object))
			innerAddedCalled = True
		End Sub

	End Class

#End Region

	<TestMethod()> _
	Public Sub AddingItemToWorkItemDoesNotFireServicesAddedEvent()
		Dim wi As TestableRootWorkItem = New TestableRootWorkItem()
		Dim utilityInstance As AddingItemToWorkItemDoesNotFireServicesAddedEventUtility = _
			New AddingItemToWorkItemDoesNotFireServicesAddedEventUtility(False)
		AddHandler wi.Services.Added, AddressOf utilityInstance.EventHandler

		Dim obj As Object = wi.Items.AddNew(Of Object)()

		Assert.IsFalse(utilityInstance.AddedCalled)
	End Sub

	<TestMethod()> _
	Public Sub AdddingServiceToWorkItemDoesNotFireItemsAddedEvent()
		Dim wi As TestableRootWorkItem = New TestableRootWorkItem()
		Dim utilityInstance As AddingItemToWorkItemDoesNotFireServicesAddedEventUtility = _
			New AddingItemToWorkItemDoesNotFireServicesAddedEventUtility(False)
		AddHandler wi.Items.Added, AddressOf utilityInstance.EventHandler

		Dim obj As Object = wi.Services.AddNew(Of Object)()

		Assert.IsFalse(utilityInstance.AddedCalled)
	End Sub

#Region "Utility for RemovingItemFromWorkItemDoesNotFireServicesRemovedEvent"

	Private Class RemovingItemFromWorkItemDoesNotFireServicesRemovedEventUtility

		Private innerRemoveCalled As Boolean

		Public Property RemoveCalled() As Boolean
			Get
				Return innerRemoveCalled
			End Get
			Set(ByVal value As Boolean)
				innerRemoveCalled = value
			End Set
		End Property

		Public Sub New(ByVal removeCalled As Boolean)
			innerRemoveCalled = removeCalled
		End Sub

		Public Sub EventHandler(ByVal sender As Object, ByVal args As DataEventArgs(Of Object))
			innerRemoveCalled = True
		End Sub

	End Class

#End Region

	<TestMethod()> _
	Public Sub RemovingItemFromWorkItemDoesNotFireServicesRemovedEvent()
		Dim wi As TestableRootWorkItem = New TestableRootWorkItem()
		Dim utilityInstance As RemovingItemFromWorkItemDoesNotFireServicesRemovedEventUtility = _
			New RemovingItemFromWorkItemDoesNotFireServicesRemovedEventUtility(False)
		AddHandler wi.Services.Removed, AddressOf utilityInstance.EventHandler

		Dim obj As Object = wi.Items.AddNew(Of Object)()
		wi.Items.Remove(obj)

		Assert.IsFalse(utilityInstance.RemoveCalled)
	End Sub

	<TestMethod()> _
	Public Sub RemovingServiceFromWorkItemDoesNotFireItemsRemovedEvent()
		Dim wi As TestableRootWorkItem = New TestableRootWorkItem()
		Dim utilityInstance As RemovingItemFromWorkItemDoesNotFireServicesRemovedEventUtility = _
			New RemovingItemFromWorkItemDoesNotFireServicesRemovedEventUtility(False)
		AddHandler wi.Items.Removed, AddressOf utilityInstance.EventHandler

		Dim obj As Object = wi.Services.AddNew(Of Object)()
		wi.Services.Remove(Of Object)()

		Assert.IsFalse(utilityInstance.RemoveCalled)
	End Sub

#End Region

	' Features

#Region "Activation"

	<TestMethod()> _
	Public Sub StatusIsInactiveWhenCreated()
		Dim wi As WorkItem = New TestableRootWorkItem()

		Assert.AreEqual(WorkItemStatus.Inactive, wi.Status)
	End Sub

	<TestMethod()> _
	Public Sub StatusIsActiveWhenActivateCalled()
		Dim wi As WorkItem = New TestableRootWorkItem()

		wi.Activate()

		Assert.AreEqual(WorkItemStatus.Active, wi.Status)
	End Sub

	<TestMethod()> _
	Public Sub StatusIsInactiveAfterDeactivateCalled()
		Dim wi As WorkItem = New TestableRootWorkItem()

		wi.Activate()
		wi.Deactivate()

		Assert.AreEqual(WorkItemStatus.Inactive, wi.Status)
	End Sub

	<TestMethod()> _
	Public Sub StatusIsTerminatedAfterTerminateCalled()
		Dim wi As WorkItem = New TestableRootWorkItem()

		wi.Terminate()

		Assert.AreEqual(WorkItemStatus.Terminated, wi.Status)
	End Sub

	<TestMethod()> _
	Public Sub WorkItemCallsActivationServiceWhenActivated()
		Dim wi As WorkItem = New TestableRootWorkItem()
		Dim svc As MockWorkItemActivationService = wi.Services.AddNew(Of MockWorkItemActivationService, IWorkItemActivationService)()

		wi.Activate()

		Assert.IsTrue(svc.ChangeStatusCalled)
	End Sub

	<TestMethod()> _
	Public Sub WorkItemCallsActivationServiceWhenDeactivated()
		Dim wi As WorkItem = New TestableRootWorkItem()
		Dim svc As MockWorkItemActivationService = wi.Services.AddNew(Of MockWorkItemActivationService, IWorkItemActivationService)()

		wi.Activate()
		wi.Deactivate()

		Assert.IsTrue(svc.ChangeStatusCalled)
	End Sub

	<TestMethod()> _
	Public Sub WorkItemCallsActivationServiceWhenTerminated()
		Dim wi As WorkItem = New TestableRootWorkItem()
		Dim svc As MockWorkItemActivationService = wi.Services.AddNew(Of MockWorkItemActivationService, IWorkItemActivationService)()

		wi.Terminate()

		Assert.IsTrue(svc.ChangeStatusCalled)
	End Sub

	<TestMethod()> _
	Public Sub WorkItemPassesItselfToActivationServiceWhenActivated()
		Dim wi As WorkItem = New TestableRootWorkItem()
		Dim svc As MockWorkItemActivationService = wi.Services.AddNew(Of MockWorkItemActivationService, IWorkItemActivationService)()

		wi.Activate()

		Assert.AreEqual(wi, svc.LastChangedItem)
	End Sub

	<TestMethod()> _
	Public Sub WorkItemPassesItselfToActivationServiceWhenDeactivated()
		Dim wi As WorkItem = New TestableRootWorkItem()
		Dim svc As MockWorkItemActivationService = wi.Services.AddNew(Of MockWorkItemActivationService, IWorkItemActivationService)()

		wi.Activate()
		wi.Deactivate()

		Assert.AreEqual(wi, svc.LastChangedItem)
	End Sub

	<TestMethod()> _
	Public Sub WorkItemPassesItselfToActivationServiceWhenTerminated()
		Dim wi As WorkItem = New TestableRootWorkItem()
		Dim svc As MockWorkItemActivationService = wi.Services.AddNew(Of MockWorkItemActivationService, IWorkItemActivationService)()

		wi.Terminate()

		Assert.AreEqual(wi, svc.LastChangedItem)
	End Sub

	<TestMethod(), ExpectedException(GetType(InvalidOperationException))> _
	Public Sub ActivateOnTerminatedWorkItemThrows()
		Dim wi As WorkItem = New TestableRootWorkItem()

		wi.Terminate()
		wi.Activate()
	End Sub

#Region "Utility for FiresActivatingEventWhenActivated"

	Private Class FiresActivatingEventWhenActivatedUtility

		Private innerActivating As Boolean

		Public Property Activating() As Boolean
			Get
				Return innerActivating
			End Get
			Set(ByVal value As Boolean)
				innerActivating = value
			End Set
		End Property

		Public Sub New(ByVal activating As Boolean)
			innerActivating = activating
		End Sub

		Public Sub EventHandler(ByVal sender As Object, ByVal args As CancelEventArgs)
			innerActivating = True
		End Sub

	End Class

#End Region

	<TestMethod()> _
	Public Sub FiresActivatingEventWhenActivated()
		Dim wi As WorkItem = New TestableRootWorkItem()

		Dim utilityInstance As FiresActivatingEventWhenActivatedUtility = _
			New FiresActivatingEventWhenActivatedUtility(False)
		AddHandler wi.Activating, AddressOf utilityInstance.EventHandler
		wi.Activate()

		Assert.IsTrue(utilityInstance.Activating)
	End Sub

#Region "Utility for CanCancelActivation"

	Private Class CanCancelActivationUtility

		Private innerActivated As Boolean

		Public Property Activated() As Boolean
			Get
				Return innerActivated
			End Get
			Set(ByVal value As Boolean)
				innerActivated = value
			End Set
		End Property

		Public Sub New(ByVal activated As Boolean)
			innerActivated = activated
		End Sub

		Public Sub EventHandler(ByVal sender As Object, ByVal args As EventArgs)
			innerActivated = True
		End Sub

	End Class

	Public Sub ActivatingEventHandler(ByVal sender As Object, ByVal args As CancelEventArgs)
		args.Cancel = True
	End Sub

#End Region

	<TestMethod()> _
	Public Sub CanCancelActivation()
		Dim wi As WorkItem = New TestableRootWorkItem()

		Dim utilityInstance As CanCancelActivationUtility = New CanCancelActivationUtility(False)
		AddHandler wi.Activating, AddressOf ActivatingEventHandler
		AddHandler wi.Activated, AddressOf utilityInstance.EventHandler
		wi.Activate()

		Assert.IsFalse(utilityInstance.Activated)
	End Sub

#Region "Utility code for FiresActivatedEventWhenActivated"

	Private Class FiresActivatedEventWhenActivatedUtility

		Private innerActivated As Boolean

		Public Property Activated() As Boolean
			Get
				Return innerActivated
			End Get
			Set(ByVal value As Boolean)
				innerActivated = value
			End Set
		End Property

		Public Sub New(ByVal activated As Boolean)
			innerActivated = activated
		End Sub

		Public Sub EventHandler(ByVal sender As Object, ByVal args As EventArgs)
			innerActivated = True
		End Sub

	End Class

#End Region

	<TestMethod()> _
	Public Sub FiresActivatedEventWhenAcivated()
		Dim wi As WorkItem = New TestableRootWorkItem()

		Dim utilityInstance As FiresActivatedEventWhenActivatedUtility = _
			New FiresActivatedEventWhenActivatedUtility(False)
		AddHandler wi.Activated, AddressOf utilityInstance.EventHandler
		wi.Activate()

		Assert.IsTrue(utilityInstance.Activated)
	End Sub

#Region "Utility code for FiresDeactivatedEventWhenDeactivated"

	Private Class FiresDeactivatedEventWhenDeactivatedUtility

		Private innerDeactivated As Boolean

		Public Property Deactivated() As Boolean
			Get
				Return innerDeactivated
			End Get
			Set(ByVal value As Boolean)
				innerDeactivated = value
			End Set
		End Property

		Public Sub New(ByVal deactivated As Boolean)
			innerDeactivated = deactivated
		End Sub

		Public Sub EventHandler(ByVal sender As Object, ByVal args As EventArgs)
			innerDeactivated = True
		End Sub

	End Class

#End Region

	<TestMethod()> _
	Public Sub FiresDeactivatedEventWhenDeactivated()
		Dim wi As WorkItem = New TestableRootWorkItem()

		Dim utilityInstance As FiresDeactivatedEventWhenDeactivatedUtility = _
			New FiresDeactivatedEventWhenDeactivatedUtility(False)
		AddHandler wi.Deactivated, AddressOf utilityInstance.EventHandler
		wi.Activate()
		wi.Deactivate()

		Assert.IsTrue(utilityInstance.Deactivated)
	End Sub

#Region "Utility code for FiresDeactivatingEventWhenDeactivated"

	Private Class FiresDeactivatingEventWhenDeactivatedUtility

		Private innerDeactivating As Boolean

		Public Property Deactivating() As Boolean
			Get
				Return innerDeactivating
			End Get
			Set(ByVal value As Boolean)
				innerDeactivating = value
			End Set
		End Property

		Public Sub New(ByVal deactivating As Boolean)
			innerDeactivating = deactivating
		End Sub

		Public Sub EventHandler(ByVal sender As Object, ByVal args As CancelEventArgs)
			innerDeactivating = True
		End Sub

	End Class

#End Region

	<TestMethod()> _
	Public Sub FiresDeactivatingEventWhenDeactivated()
		Dim wi As WorkItem = New TestableRootWorkItem()

		Dim utilityInstance As FiresDeactivatingEventWhenDeactivatedUtility = _
			New FiresDeactivatingEventWhenDeactivatedUtility(False)
		AddHandler wi.Deactivating, AddressOf utilityInstance.EventHandler
		wi.Activate()
		wi.Deactivate()

		Assert.IsTrue(utilityInstance.Deactivating)
	End Sub

#Region "Utility code for CanCancelDeactivation"

	Private Sub DeactivatingEventHandler(ByVal sender As Object, ByVal args As CancelEventArgs)
		args.Cancel = True
	End Sub

#End Region

	<TestMethod()> _
	Public Sub CanCancelDeactivation()
		Dim wi As WorkItem = New TestableRootWorkItem()

		Dim utilityInstance As FiresDeactivatedEventWhenDeactivatedUtility = _
			New FiresDeactivatedEventWhenDeactivatedUtility(False)
		AddHandler wi.Deactivated, AddressOf utilityInstance.EventHandler
		AddHandler wi.Deactivating, AddressOf DeactivatingEventHandler
		wi.Activate()
		wi.Deactivate()

		Assert.IsFalse(utilityInstance.Deactivated)
	End Sub

#Region "Utility code for FiresTerminatedEventWhenTerminated"

	Private Class FiresTerminatedEventWhenTerminatedUtility

		Private innerTerminated As Boolean

		Public Property Terminated() As Boolean
			Get
				Return innerTerminated
			End Get
			Set(ByVal value As Boolean)
				innerTerminated = value
			End Set
		End Property

		Public Sub New(ByVal terminated As Boolean)
			innerTerminated = terminated
		End Sub

		Public Sub EventHandler(ByVal sender As Object, ByVal args As EventArgs)
			innerTerminated = True
		End Sub

	End Class

#End Region

	<TestMethod()> _
	Public Sub FiresTerminatedEventWhenTerminated()
		Dim wi As WorkItem = New TestableRootWorkItem()

		Dim utilityInstance As FiresTerminatedEventWhenTerminatedUtility = _
			New FiresTerminatedEventWhenTerminatedUtility(False)
		AddHandler wi.Terminated, AddressOf utilityInstance.EventHandler
		wi.Terminate()

		Assert.IsTrue(utilityInstance.Terminated)
	End Sub

#Region "Utility code for FiresTerminatingBeforeTerminated"

	Private Class FiresTerminatingBeforeTerminatedUtility

		Private innerCalledFirst As Integer

		Public Property CalledFirst() As Integer
			Get
				Return innerCalledFirst
			End Get
			Set(ByVal value As Integer)
				innerCalledFirst = value
			End Set
		End Property

		Public Sub New(ByVal calledFirst As Integer)
			innerCalledFirst = calledFirst
		End Sub

		Public Sub TerminatingEventHandler(ByVal sender As Object, ByVal args As EventArgs)
			innerCalledFirst = 1
		End Sub

		Public Sub TerminatedEventHandler(ByVal sender As Object, ByVal args As EventArgs)
			If (innerCalledFirst <> 1) Then
				innerCalledFirst = 2
			End If
		End Sub

	End Class

#End Region

	<TestMethod()> _
	Public Sub FiresTerminatingBeforeTerminated()
		Dim wi As WorkItem = New TestableRootWorkItem()

		Dim utilityInstance As FiresTerminatingBeforeTerminatedUtility = _
			 New FiresTerminatingBeforeTerminatedUtility(0)
		AddHandler wi.Terminating, AddressOf utilityInstance.TerminatingEventHandler
		AddHandler wi.Terminated, AddressOf utilityInstance.TerminatedEventHandler
		wi.Terminate()

		Assert.AreEqual(1, utilityInstance.CalledFirst)
	End Sub

#End Region

#Region "Dependency injection"

	<TestMethod()> _
	Public Sub CreatedDependenciesAreInItemsCollection()
		Dim wi As WorkItem = New TestableRootWorkItem()
		Dim originalCount As Integer = wi.Items.Count
		wi.Items.AddNew(Of MockDependingObject)()

		Assert.AreEqual(2, wi.Items.Count - originalCount)
	End Sub

#End Region

#Region "Simple properties"

	<TestMethod()> _
	Public Sub NewWorkItemHasUniqueId()
		Dim item1 As WorkItem = New TestableRootWorkItem()
		Dim item2 As WorkItem = New TestableRootWorkItem()

		Assert.IsTrue(item1.ID <> item2.ID)
	End Sub

	<TestMethod()> _
	Public Sub WorkItemStateHasSameIdAsWorkItem()
		Dim wi As WorkItem = New TestableRootWorkItem()

		Assert.AreEqual(wi.ID, wi.State.ID)
	End Sub

	<TestMethod()> _
	Public Sub CanGetCorrectNumberOfSmartParts()
		Dim wi As TestableRootWorkItem = New TestableRootWorkItem()

		wi.SmartParts.Add(New Mock1())
		wi.SmartParts.Add(New Mock2())
		wi.SmartParts.Add(New Mock3())

		' Returns 3
		Assert.AreEqual(2, wi.SmartParts.Count)
	End Sub

	<SmartPart()> _
	Public Class Mock1
	End Class

	<SmartPart()> _
	Public Class Mock2
	End Class

	Public Class Mock3
	End Class

#End Region

#Region "SmartPartInfo"

	<TestMethod()> _
	Public Sub GettingForNotRegisteredReturnsNull()
		Dim wi As WorkItem = New TestableRootWorkItem()
		Dim ctrl As Control = New Control()
		wi.RegisterSmartPartInfo(ctrl, New SmartPartInfo("foo", "bar"))

		Dim info As MySmartPartInfo = wi.GetSmartPartInfo(Of MySmartPartInfo)(ctrl)

		Assert.IsNull(info)
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
	Public Sub CanGetSmartPartInfoThrowsForNull()
		Dim wi As WorkItem = New TestableRootWorkItem()

		wi.GetSmartPartInfo(Of MySmartPartInfo)(Nothing)
	End Sub

	<TestMethod()> _
	Public Sub CanRegisterASmartPartInfoForAControl()
		Dim wi As WorkItem = New TestableRootWorkItem()
		Dim ctrl As Control = New Control()
		Dim info As ISmartPartInfo = New MySmartPartInfo()
		info.Title = "Title"
		info.Description = "Description"

		wi.RegisterSmartPartInfo(ctrl, info)

		Assert.AreSame(info, wi.GetSmartPartInfo(Of MySmartPartInfo)(ctrl))
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
	Public Sub RegisterSmartPartInfoIsGuardedForNullControl()
		Dim wi As WorkItem = New TestableRootWorkItem()

		wi.RegisterSmartPartInfo(Nothing, Nothing)
	End Sub

	<TestMethod()> _
	Public Sub CanRegisterSeveralTypesOfSmartPartInfos()
		Dim wi As WorkItem = New TestableRootWorkItem()
		Dim info1 As MySmartPartInfo = New MySmartPartInfo()
		Dim info2 As MyOtherSmartPartInfo = New MyOtherSmartPartInfo()
		Dim ctrl As Control = New Control()

		wi.RegisterSmartPartInfo(ctrl, info1)
		wi.RegisterSmartPartInfo(ctrl, info2)

		Assert.AreSame(info1, wi.GetSmartPartInfo(Of MySmartPartInfo)(ctrl))
		Assert.AreSame(info2, wi.GetSmartPartInfo(Of MyOtherSmartPartInfo)(ctrl))
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
	Public Sub RegisterSmartPartInfoIsGuardedForNullSmartPartInfo()
		Dim wi As WorkItem = New TestableRootWorkItem()

		wi.RegisterSmartPartInfo(New Control(), Nothing)
	End Sub

	<TestMethod()> _
	Public Sub RegisteringSameTypeSmartPartInfoKeepsTheLastOne()
		Dim wi As WorkItem = New TestableRootWorkItem()
		Dim info1 As MySmartPartInfo = New MySmartPartInfo()
		Dim info2 As MySmartPartInfo = New MySmartPartInfo()
		Dim ctrl As Control = New Control()

		wi.RegisterSmartPartInfo(ctrl, info1)
		wi.RegisterSmartPartInfo(ctrl, info2)

		Assert.AreSame(info2, wi.GetSmartPartInfo(Of MySmartPartInfo)(ctrl))
	End Sub

#End Region

#Region "State and persistence"

	<TestMethod(), ExpectedException(GetType(ServiceMissingException))> _
	Public Sub LoadMethodThrowsWhenNoServicePresent()
		Dim wi As WorkItem = New TestableRootWorkItem()

		wi.Load()
	End Sub

	<TestMethod(), ExpectedException(GetType(ServiceMissingException))> _
	Public Sub SaveMethodThrowsIfNoPersistenceService()
		Dim wi As WorkItem = New TestableRootWorkItem()

		wi.Save()
	End Sub

	<TestMethod()> _
	Public Sub LoadMethodCallsPersistenceService()
		Dim wi As WorkItem = New TestableRootWorkItem()
		Dim svc As MockPersistenceService = wi.Services.AddNew(Of MockPersistenceService, IStatePersistenceService)()

		wi.Load()

		Assert.IsTrue(svc.LoadCalled)
	End Sub

	<TestMethod()> _
	Public Sub LoadMethodSetsWorkItemState()
		Dim wi As WorkItem = New TestableRootWorkItem()
		Dim svc As MockPersistenceService = wi.Services.AddNew(Of MockPersistenceService, IStatePersistenceService)()

		wi.Load()

		Assert.AreSame(svc.LoadedState, wi.State)
	End Sub

	<TestMethod()> _
	Public Sub NewWorkItemHasState()
		Dim wi As WorkItem = New TestableRootWorkItem()

		Assert.IsNotNull(wi.State)
	End Sub

	<TestMethod()> _
	Public Sub CanRemoveWorkItemState()
		Dim wi As WorkItem = New TestableRootWorkItem()
		Dim svc As MockPersistenceService = wi.Services.AddNew(Of MockPersistenceService, IStatePersistenceService)()

		wi.DeleteState()

		Assert.IsTrue(svc.RemoveCalled)
	End Sub

	<TestMethod()> _
	Public Sub SaveMethodCallsPersistenceServiceWithWorkItemState()
		Dim wi As WorkItem = New TestableRootWorkItem()
		Dim svc As MockPersistenceService = wi.Services.AddNew(Of MockPersistenceService, IStatePersistenceService)()

		wi.Save()

		Assert.IsTrue(svc.SaveCalled)
		Assert.AreSame(wi.State, svc.SavedState)
	End Sub

	<TestMethod()> _
	Public Sub EventBrokerEventArgsHasNewData()
		Dim item As MockStateWorkItem = New MockStateWorkItem()
		Dim obj1 As Object = New Object()
		item.State("Test") = obj1

		Assert.IsNotNull(item.StateEventArgs)
		Assert.AreSame(item.StateEventArgs.NewValue, obj1)
	End Sub

	<TestMethod()> _
	Public Sub ChangingStateMoreThanOnceFiresEventMultipleTimes()
		Dim item As MockStateWorkItem = New MockStateWorkItem()
		item.State("Test") = New Object()
		item.State("Test") = New Object()
		item.State("Test2") = New Object()
		item.State("Test") = New Object()

		Assert.AreEqual(3, item.StateChangeCalled)
	End Sub

	<TestMethod()> _
	Public Sub EventBrokerEventArgsHasNewDataAfterMultipleChanges()
		Dim item As MockStateWorkItem = New MockStateWorkItem()
		Dim obj1 As Object = New Object()
		item.State("Test") = obj1
		Dim obj2 As Object = New Object()
		item.State("Test") = obj2
		Dim obj3 As Object = New Object()
		item.State("Test") = obj3

		Assert.IsNotNull(item.StateEventArgs)
		Assert.AreEqual(3, item.StateChangeCalled)
		Assert.AreSame(item.StateEventArgs.NewValue, obj3)
		Assert.AreSame(item.StateEventArgs.OldValue, obj2)
	End Sub

	<TestMethod()> _
	Public Sub SaveMethodResetsHasChangesFlag()
		Dim wi As WorkItem = New TestableRootWorkItem()
		Dim svc As MockPersistenceService = wi.Services.AddNew(Of MockPersistenceService, IStatePersistenceService)()
		wi.State("foo") = "foo"

		Assert.IsTrue(wi.State.HasChanges)

		wi.Save()

		Assert.IsFalse(wi.State.HasChanges)
	End Sub

#End Region

	' Test support

#Region "Helper classes"

	Private Class MockTearDownStrategy : Inherits BuilderStrategy
		Public TearDownCalled As Boolean = False

		Public Overrides Function TearDown(ByVal context As IBuilderContext, ByVal item As Object) As Object
			TearDownCalled = True
			Return MyBase.TearDown(context, item)
		End Function
	End Class

	Private Class ChildWorkItem : Inherits WorkItem
	End Class

	Private Class MockStateWorkItem : Inherits TestableRootWorkItem
		Public StateChangeCalled As Integer = 0
		Public StateEventArgs As StateChangedEventArgs = Nothing

		<StateChanged("Test", ThreadOption.Publisher)> _
		Public Sub TestStateChanged(ByVal sender As Object, ByVal args As StateChangedEventArgs)
			StateChangeCalled += 1
			StateEventArgs = args
		End Sub
	End Class

	Private Interface IMockDataObject
		Property IntProperty() As Integer
	End Interface

	Private Interface IMockDataObject2
	End Interface

	Private Class MockDataObject : Implements IMockDataObject, IMockDataObject2
		Private innerIntProperty As Integer

		Public Property IntProperty() As Integer Implements IMockDataObject.IntProperty
			Get
				Return innerIntProperty
			End Get
			Set(ByVal value As Integer)
				innerIntProperty = value
			End Set
		End Property
	End Class

	Private Class MockWorkItemActivationService
		Implements IWorkItemActivationService

		Public ChangeStatusCalled As Boolean = False
		Public LastChangedItem As WorkItem

		Public Sub ChangeStatus(ByVal item As WorkItem) Implements IWorkItemActivationService.ChangeStatus
			LastChangedItem = item
			ChangeStatusCalled = True
		End Sub
	End Class

	Private Class MockDisposableObject : Implements IDisposable
		Public WasDisposed As Boolean = False

		Public Sub Dispose() Implements IDisposable.Dispose
			WasDisposed = True
		End Sub
	End Class

	Private Class MySmartPartInfo
		Implements ISmartPartInfo

		Private innerDescription As String = "Default"
		Private innerTitle As String = "Default"

		Public Property Description() As String Implements ISmartPartInfo.Description
			Get
				Return innerDescription
			End Get
			Set(ByVal value As String)
				innerDescription = value
			End Set
		End Property

		Public Property Title() As String Implements ISmartPartInfo.Title
			Get
				Return innerTitle
			End Get
			Set(ByVal value As String)
				innerTitle = value
			End Set
		End Property
	End Class

	Private Class MyOtherSmartPartInfo : Inherits MySmartPartInfo
	End Class

	Private Class MockPersistenceService
		Implements IStatePersistenceService

		Public SaveCalled As Boolean = False
		Public SavedState As State = Nothing

		Public LoadCalled As Boolean = False
		Public LoadedState As State = Nothing

		Public RemoveCalled As Boolean = False

		Public Sub Save(ByVal state As State) Implements IStatePersistenceService.Save
			SaveCalled = True
			SavedState = state
		End Sub

		Public Function Load(ByVal id As String) As State Implements IStatePersistenceService.Load
			LoadCalled = True
			LoadedState = New State(id)
			Return LoadedState
		End Function

		Public Sub Remove(ByVal id As String) Implements IStatePersistenceService.Remove
			RemoveCalled = True
		End Sub

		Public Function Contains(ByVal id As String) As Boolean Implements IStatePersistenceService.Contains
			Throw New NotImplementedException()
		End Function
	End Class

	Public Class MockDependingObject
		<ComponentDependency("Foo", CreateIfNotFound:=True)> _
		Public WriteOnly Property DependentObject() As MockDependentObject
			Set(ByVal value As MockDependentObject)
			End Set
		End Property
	End Class

	Public Class MockDependentObject
	End Class

#End Region
End Class
