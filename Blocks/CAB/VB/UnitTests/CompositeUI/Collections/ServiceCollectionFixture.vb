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
Imports Microsoft.Practices.CompositeUI.Collections
Imports Microsoft.Practices.CompositeUI.Services
Imports Microsoft.Practices.CompositeUI.Utility
Imports Microsoft.Practices.ObjectBuilder

Namespace Collections
	<TestClass()> _
	Public Class ServiceCollectionFixture
#Region "Add - Generic"

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub AddingNullServiceThrows_Generic()
			Dim services As TestableServiceCollection = CreateServiceCollection()

			services.Add(Of Object)(Nothing)
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentException))> _
		Public Sub CannotAddSameServiceTypeTwiceSinceServicesAreSingletons_Generic()
			Dim services As TestableServiceCollection = CreateServiceCollection()

			services.Add(Of MockDataObject)(New MockDataObject())
			services.Add(Of MockDataObject)(New MockDataObject())
		End Sub

#End Region

#Region "Add - Non-Generic"

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub AddingNullServiceThrows()
			Dim services As TestableServiceCollection = CreateServiceCollection()

			services.Add(GetType(Object), Nothing)
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub AddingNullServiceTypeThrows()
			Dim services As TestableServiceCollection = CreateServiceCollection()

			services.Add(Nothing, New Object())
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentException))> _
		Public Sub CannotAddSameServiceTypeTwiceSinceServicesAreSingletons()
			Dim services As TestableServiceCollection = CreateServiceCollection()

			services.Add(GetType(MockDataObject), New MockDataObject())
			services.Add(GetType(MockDataObject), New MockDataObject())
		End Sub

		<TestMethod()> _
		Public Sub AddingServiceRunsTheBuilder()
			Dim obj As BuilderAwareObject = New BuilderAwareObject()
			Dim services As TestableServiceCollection = CreateServiceCollection()

			services.Add(GetType(BuilderAwareObject), obj)

			Assert.IsTrue(obj.BuilderWasRun)
		End Sub

		<TestMethod()> _
		<ExpectedException(GetType(ArgumentException))> _
		Public Sub RegistrationWithIncompatibleTypeThrows()
			Dim srv As Service = New Service()
			Dim services As TestableServiceCollection = CreateServiceCollection()

			services.Add(GetType(IStartable), srv)
		End Sub

		Public Interface IStartable
		End Interface

		Public Interface IService
		End Interface

		Public Class Service
			Implements IService
		End Class

#End Region

#Region "AddOnDemand - Generic"

		<TestMethod()> _
		Public Sub CanAddDemandAddServiceAndRetrieveService_Generic()
			Dim services As TestableServiceCollection = CreateServiceCollection()

			services.AddOnDemand(Of MockDemandService)()

			Assert.IsTrue(services.Contains(Of MockDemandService)())
			Assert.IsNotNull(services.Get(Of MockDemandService)())
		End Sub

		<TestMethod()> _
		Public Sub CanAddDemandAddServiceOfOneTypeAndRegisterAsOtherType_Generic()
			Dim services As TestableServiceCollection = CreateServiceCollection()

			services.AddOnDemand(Of MockDemandService, IMockDemandService)()

			Assert.IsFalse(services.Contains(Of MockDemandService)())
			Assert.IsTrue(services.Contains(Of IMockDemandService)())
			Assert.IsNull(services.Get(Of MockDemandService)())
			Assert.IsNotNull(services.Get(Of IMockDemandService)())
		End Sub

		<TestMethod()> _
		Public Sub CanAddDemandAddServiceAndItWontBeCreatedUntilAskedFor_Generic()
			Dim services As TestableServiceCollection = CreateServiceCollection()
			MockDemandService.WasCreated = False

			services.AddOnDemand(Of MockDemandService)()
			Assert.IsFalse(MockDemandService.WasCreated)
			Assert.IsTrue(services.Contains(Of MockDemandService)())
			Assert.IsFalse(MockDemandService.WasCreated)

			Dim svc As MockDemandService = services.Get(Of MockDemandService)()
			Assert.IsTrue(MockDemandService.WasCreated)
		End Sub

		<TestMethod()> _
		Public Sub DemandAddedServiceFromParentGetsReplacedInParentEvenWhenAskedForFromChild_Generic()
			Dim parent As TestableServiceCollection = CreateServiceCollection()
			Dim child As TestableServiceCollection = New TestableServiceCollection(parent)

			parent.AddOnDemand(Of MockDemandService)()
			Dim svc As MockDemandService = child.Get(Of MockDemandService)()

			Assert.AreSame(svc, parent.Locator.Get(New DependencyResolutionLocatorKey(GetType(MockDemandService), Nothing)))
		End Sub

#End Region

#Region "AddOnDemand - Non-Generic"

		<TestMethod()> _
		Public Sub CanAddDemandAddServiceAndRetrieveService()
			Dim services As TestableServiceCollection = CreateServiceCollection()

			services.AddOnDemand(GetType(MockDemandService))

			Assert.IsTrue(services.Contains(Of MockDemandService)())
			Assert.IsNotNull(services.Get(Of MockDemandService)())
		End Sub

		<TestMethod()> _
		Public Sub CanAddDemandAddServiceOfOneTypeAndRegisterAsOtherType()
			Dim services As TestableServiceCollection = CreateServiceCollection()

			services.AddOnDemand(GetType(MockDemandService), GetType(IMockDemandService))

			Assert.IsFalse(services.Contains(Of MockDemandService)())
			Assert.IsTrue(services.Contains(Of IMockDemandService)())
			Assert.IsNull(services.Get(Of MockDemandService)())
			Assert.IsNotNull(services.Get(Of IMockDemandService)())
		End Sub

		<TestMethod()> _
		Public Sub CanAddDemandAddServiceAndItWontBeCreatedUntilAskedFor()
			Dim services As TestableServiceCollection = CreateServiceCollection()
			MockDemandService.WasCreated = False

			services.AddOnDemand(GetType(MockDemandService))
			Assert.IsFalse(MockDemandService.WasCreated)
			Assert.IsTrue(services.Contains(Of MockDemandService)())
			Assert.IsFalse(MockDemandService.WasCreated)

			Dim svc As MockDemandService = services.Get(Of MockDemandService)()
			Assert.IsTrue(MockDemandService.WasCreated)
		End Sub

		<TestMethod()> _
		Public Sub NestedWorkItemIntegrationTest()
			Dim parentWorkItem As WorkItem = New TestableRootWorkItem()
			Dim childWorkItem As WorkItem = parentWorkItem.WorkItems.AddNew(Of WorkItem)()

			Dim a As MockA = New MockA()
			Dim b As MockA = New MockA()

			parentWorkItem.Services.Add(GetType(MockA), a)
			childWorkItem.Services.Add(GetType(MockA), b)

			Assert.AreSame(a, parentWorkItem.Services.Get(Of MockA)())
			Assert.AreSame(b, childWorkItem.Services.Get(Of MockA)())

			Dim c As MockB = New MockB()

			parentWorkItem.Services.Add(GetType(MockB), c)

			' Throws ArgumentException
			childWorkItem.Services.AddOnDemand(GetType(MockB))
		End Sub

		Private Class MockA
		End Class
		Private Class MockB
		End Class

#End Region

#Region "AddNew - Generic"

		<TestMethod()> _
		Public Sub CanCreateService_Generic()
			Dim services As TestableServiceCollection = CreateServiceCollection()

			Dim svc As MockDataObject = services.AddNew(Of MockDataObject)()

			Assert.IsNotNull(svc)
			Assert.AreSame(svc, services.Get(Of MockDataObject)())
		End Sub

		<TestMethod()> _
		Public Sub CanCreateServiceRegisteredAsOtherType_Generic()
			Dim services As TestableServiceCollection = CreateServiceCollection()

			Dim svc As MockDataObject = services.AddNew(Of MockDataObject, IMockDataObject)()

			Assert.IsNotNull(svc)
			Assert.IsNull(services.Get(Of MockDataObject)())
			Assert.AreSame(svc, services.Get(Of IMockDataObject)())
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentException))> _
		Public Sub CannotCreateSameServiceTypeTwiceSinceServicesAreSingletons_Generic()
			Dim services As TestableServiceCollection = CreateServiceCollection()

			services.AddNew(Of MockDataObject)()
			services.AddNew(Of MockDataObject)()
		End Sub

		<TestMethod()> _
		Public Sub CanCreateServiceInChildWhenServiceExistsInParent_Generic()
			Dim services As TestableServiceCollection = CreateServiceCollection()
			Dim childServices As TestableServiceCollection = New TestableServiceCollection(services)

			Dim parentService As MockDataObject = services.AddNew(Of MockDataObject)()
			Dim childService As MockDataObject = childServices.AddNew(Of MockDataObject)()

			Assert.AreSame(parentService, services.Get(Of MockDataObject)())
			Assert.AreSame(childService, childServices.Get(Of MockDataObject)())
		End Sub

#End Region

#Region "AddNew - Non-Generic"

		<TestMethod()> _
		Public Sub CanCreateService()
			Dim services As TestableServiceCollection = CreateServiceCollection()

			Dim svc As Object = services.AddNew(GetType(MockDataObject))

			Assert.IsNotNull(svc)
			Assert.AreSame(svc, services.Get(GetType(MockDataObject)))
		End Sub

		<TestMethod()> _
		Public Sub CanCreateServiceRegisteredAsOtherType()
			Dim services As TestableServiceCollection = CreateServiceCollection()

			Dim svc As Object = services.AddNew(GetType(MockDataObject), GetType(IMockDataObject))

			Assert.IsNotNull(svc)
			Assert.IsNull(services.Get(GetType(MockDataObject)))
			Assert.AreSame(svc, services.Get(GetType(IMockDataObject)))
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentException))> _
		Public Sub CannotCreateSameServiceTypeTwiceSinceServicesAreSingletons()
			Dim services As TestableServiceCollection = CreateServiceCollection()

			services.AddNew(GetType(MockDataObject))
			services.AddNew(GetType(MockDataObject))
		End Sub

		<TestMethod()> _
		Public Sub CanCreateServiceInChildWhenServiceExistsInParent()
			Dim services As TestableServiceCollection = CreateServiceCollection()
			Dim childServices As TestableServiceCollection = New TestableServiceCollection(services)

			Dim parentService As Object = services.AddNew(GetType(MockDataObject))
			Dim childService As Object = childServices.AddNew(GetType(MockDataObject))

			Assert.AreSame(parentService, services.Get(GetType(MockDataObject)))
			Assert.AreSame(childService, childServices.Get(GetType(MockDataObject)))
		End Sub

#End Region

#Region "Get - Generic"

		<TestMethod()> _
		Public Sub AddingServiceToWorkItemAllowsRetrievalOfService_Generic()
			Dim services As TestableServiceCollection = CreateServiceCollection()
			Dim svc As MockDataObject = New MockDataObject()

			services.Add(GetType(MockDataObject), svc)
			Dim result As MockDataObject = services.Get(Of MockDataObject)()

			Assert.AreSame(svc, result)
		End Sub

		<TestMethod()> _
		Public Sub GetServiceCanReturnNullWhenServiceDoesntExist_Generic()
			Dim services As TestableServiceCollection = CreateServiceCollection()

			Assert.IsNull(services.Get(Of MockDataObject)())
		End Sub

		<TestMethod()> _
		Public Sub CanAskForBuilderToEnsureServiceExists_Generic()
			Dim services As TestableServiceCollection = CreateServiceCollection()
			Dim svc As MockDataObject = New MockDataObject()

			services.Add(GetType(MockDataObject), svc)
			Dim result As MockDataObject = services.Get(Of MockDataObject)(True)

			Assert.AreSame(svc, result)
		End Sub

		<TestMethod(), ExpectedException(GetType(ServiceMissingException))> _
		Public Sub GetServiceCanThrowOnMissingService_Generic()
			Dim services As TestableServiceCollection = CreateServiceCollection()

			services.Get(Of MockDataObject)(True)
		End Sub

#End Region

#Region "Get - Non-Generic"

		<TestMethod()> _
		Public Sub AddingServiceToWorkItemAllowsRetrievalOfService()
			Dim services As TestableServiceCollection = CreateServiceCollection()
			Dim svc As MockDataObject = New MockDataObject()

			services.Add(GetType(MockDataObject), svc)
			Dim result As MockDataObject = CType(services.Get(GetType(MockDataObject)), MockDataObject)

			Assert.AreSame(svc, result)
		End Sub

		<TestMethod()> _
		Public Sub GetServiceCanReturnNullWhenServiceDoesntExist()
			Dim services As TestableServiceCollection = CreateServiceCollection()

			Assert.IsNull(services.Get(GetType(MockDataObject)))
		End Sub

		<TestMethod()> _
		Public Sub CanAskForBuilderToEnsureServiceExists()
			Dim services As TestableServiceCollection = CreateServiceCollection()
			Dim svc As MockDataObject = New MockDataObject()

			services.Add(GetType(MockDataObject), svc)
			Dim result As MockDataObject = CType(services.Get(GetType(MockDataObject), True), MockDataObject)

			Assert.AreSame(svc, result)
		End Sub

		<TestMethod(), ExpectedException(GetType(ServiceMissingException))> _
		Public Sub GetServiceCanThrowOnMissingService()
			Dim services As TestableServiceCollection = CreateServiceCollection()

			services.Get(GetType(MockDataObject), True)
		End Sub

#End Region

#Region "Remove - Generic"

		<TestMethod()> _
		Public Sub CanRemoveService_Generic()
			Dim services As TestableServiceCollection = CreateServiceCollection()

			services.Add(Of MockDataObject)(New MockDataObject())
			services.Remove(Of MockDataObject)()

			Assert.IsNull(services.Get(Of MockDataObject)())
		End Sub

		<TestMethod()> _
		Public Sub RemovingServiceAllowsNewServiceToBeRegisteredForGivenServiceType_Generic()
			Dim services As TestableServiceCollection = CreateServiceCollection()

			services.AddNew(Of Object)()
			services.Remove(Of Object)()
			services.AddNew(Of Object)()
		End Sub

		<TestMethod()> _
		Public Sub RemovingServiceRemovesStrongReferenceToService_Generic()
			Dim services As TestableServiceCollection = CreateServiceCollection()
			Dim wr As WeakReference = New WeakReference(services.AddNew(Of Object)())

			services.Remove(Of Object)()
			GC.Collect()

			Assert.IsNull(wr.Target)
		End Sub

		<TestMethod()> _
		Public Sub RemovingMultipleRegisteredServiceOnlyRemovesStrongReferenceWhenLastInstanceIsGone_Generic()
			Dim services As TestableServiceCollection = CreateServiceCollection()
			Dim mdo As MockDataObject = New MockDataObject()
			Dim wr As WeakReference = New WeakReference(mdo)
			services.Add(Of IMockDataObject)(mdo)
			services.Add(Of IMockDataObject2)(mdo)
			mdo = Nothing

			services.Remove(Of IMockDataObject)()
			GC.Collect()
			Assert.IsNotNull(wr.Target)

			services.Remove(Of IMockDataObject2)()
			GC.Collect()
			Assert.IsNull(wr.Target)
		End Sub

		<TestMethod()> _
		Public Sub RemovingServiceCausesItToBeTornDown_Generic()
			Dim services As TestableServiceCollection = CreateServiceCollection()
			Dim strategy As MockTearDownStrategy = New MockTearDownStrategy()
			services.Builder.Strategies.Add(strategy, BuilderStage.PreCreation)

			services.AddNew(Of Object)()
			services.Remove(Of Object)()

			Assert.IsTrue(strategy.TearDownCalled)
		End Sub

#End Region

#Region "Remove - Non-Generic"

		<TestMethod()> _
		Public Sub CanRemoveService()
			Dim services As TestableServiceCollection = CreateServiceCollection()

			services.Add(GetType(MockDataObject), New MockDataObject())
			services.Remove(GetType(MockDataObject))

			Assert.IsNull(services.Get(GetType(MockDataObject)))
		End Sub

		<TestMethod()> _
		Public Sub RemovingServiceAllowsNewServiceToBeRegisteredForGivenServiceType()
			Dim services As TestableServiceCollection = CreateServiceCollection()

			services.AddNew(GetType(Object))
			services.Remove(GetType(Object))
			services.AddNew(GetType(Object))
		End Sub

		<TestMethod()> _
		Public Sub RemovingServiceRemovesStrongReferenceToService()
			Dim services As TestableServiceCollection = CreateServiceCollection()
			Dim wr As WeakReference = New WeakReference(services.AddNew(Of Object)())

			services.Remove(GetType(Object))
			GC.Collect()

			Assert.IsNull(wr.Target)
		End Sub

#End Region

#Region "IEnumerable"

		<TestMethod()> _
		Public Sub CanEnumerateCollection()
			Dim collection As TestableServiceCollection = CreateServiceCollection()
			Dim obj1 As Object = New Object()
			Dim obj2 As String = "Hello world"

			collection.Add(obj1)
			collection.Add(obj2)

			Dim o1Found As Boolean = False
			Dim o2Found As Boolean = False
			For Each pair As KeyValuePair(Of Type, Object) In collection
				If pair.Value.Equals(obj1) Then
					o1Found = True
				End If
				If pair.Value.Equals(obj2) Then
					o2Found = True
				End If
			Next pair

			Assert.IsTrue(o1Found)
			Assert.IsTrue(o2Found)
		End Sub

		<TestMethod()> _
		Public Sub EnumeratorIgnoresItemsAddedDirectlyToLocator()
			Dim collection As TestableServiceCollection = CreateServiceCollection()
			Dim obj1 As Object = New Object()
			Dim obj2 As String = "Hello world"
			Dim obj3 As Object = New Object()

			collection.Add(obj1)
			collection.Add(obj2)
			collection.Locator.Add(GetType(Object), obj3)

			Dim o3Found As Boolean = False
			For Each pair As KeyValuePair(Of Type, Object) In collection
				If pair.Value Is obj3 Then
					o3Found = True
				End If
			Next pair

			Assert.IsFalse(o3Found)
		End Sub

#End Region

#Region "Added/Removed Events"

#Region "Utilitary code for the following subprocedures"

		Private Class EventHandlerUtilityProvider
			Private eventFiredField As Boolean
			Private serviceInEventField As Object
			Private eventFireCountField As Integer
			Public Property EventFired() As Boolean
				Get
					Return eventFiredField
				End Get
				Set(ByVal eventFiredArgument As Boolean)
					eventFiredField = eventFiredArgument
				End Set
			End Property

			Public Property ServiceInEvent() As Object
				Get
					Return serviceInEventField
				End Get
				Set(ByVal ServiceInEventArgument As Object)
					serviceInEventField = ServiceInEventArgument
				End Set
			End Property

			Public Property EventFireCount() As Integer
				Get
					Return eventFireCountField
				End Get
				Set(ByVal eventFireCountArgument As Integer)
					eventFireCountField = eventFireCountArgument
				End Set
			End Property

			Public Sub GeneralEventHandlerTest(ByVal sender As Object, ByVal e As DataEventArgs(Of Object))
				EventFired = True
				ServiceInEvent = e.Data
			End Sub

			Public Sub EventFireCounterHandler(ByVal sender As Object, ByVal e As DataEventArgs(Of Object))
				EventFireCount += 1
			End Sub

			Public Sub EventFireHandler(ByVal sender As Object, ByVal e As DataEventArgs(Of Object))
				EventFired = True
			End Sub
		End Class

#End Region

		<TestMethod()> _
		Public Sub CreatingAServiceFiresEvent()
			Dim services As TestableServiceCollection = CreateServiceCollection()

			Dim utilityProvider As EventHandlerUtilityProvider = New EventHandlerUtilityProvider
			utilityProvider.EventFired = False
			utilityProvider.ServiceInEvent = Nothing
			AddHandler services.Added, AddressOf utilityProvider.GeneralEventHandlerTest

			Dim obj As Object = services.AddNew(Of Object)()

			Assert.IsTrue(utilityProvider.EventFired)
			Assert.AreSame(obj, utilityProvider.ServiceInEvent)
		End Sub

		<TestMethod()> _
		Public Sub AddingAServiceFiresEvent()
			Dim obj As Object = New Object()
			Dim services As TestableServiceCollection = CreateServiceCollection()

			Dim utilityProvider As EventHandlerUtilityProvider = New EventHandlerUtilityProvider
			utilityProvider.EventFired = False
			utilityProvider.ServiceInEvent = Nothing
			AddHandler services.Added, AddressOf utilityProvider.GeneralEventHandlerTest
			services.Add(obj)

			Assert.IsTrue(utilityProvider.EventFired)
			Assert.AreSame(obj, utilityProvider.ServiceInEvent)
		End Sub

		<TestMethod()> _
		Public Sub AddingAServiceTwiceFiresEventOnce()
			Dim str As String = "Hello world"
			Dim services As TestableServiceCollection = CreateServiceCollection()

			Dim utilityProvider As EventHandlerUtilityProvider = New EventHandlerUtilityProvider
			utilityProvider.EventFireCount = 0
			AddHandler services.Added, AddressOf utilityProvider.EventFireCounterHandler
			services.Add(Of String)(str)
			services.Add(Of Object)(str)

			Assert.AreEqual(1, utilityProvider.EventFireCount)
		End Sub

		<TestMethod()> _
		Public Sub RemovingServiceFiresEvent()
			Dim services As TestableServiceCollection = CreateServiceCollection()

			Dim utilityProvider As EventHandlerUtilityProvider = New EventHandlerUtilityProvider
			utilityProvider.EventFired = False
			utilityProvider.ServiceInEvent = Nothing
			AddHandler services.Removed, AddressOf utilityProvider.GeneralEventHandlerTest
			Dim obj As Object = services.AddNew(Of Object)()
			services.Remove(GetType(Object))

			Assert.IsTrue(utilityProvider.EventFired)
			Assert.AreSame(obj, utilityProvider.ServiceInEvent)
		End Sub

		<TestMethod()> _
		Public Sub RemovingServiceTwiceFiresEventOnce()
			Dim services As TestableServiceCollection = CreateServiceCollection()

			Dim utilityProvider As EventHandlerUtilityProvider = New EventHandlerUtilityProvider
			utilityProvider.EventFireCount = 0
			AddHandler services.Removed, AddressOf utilityProvider.EventFireCounterHandler
			services.AddNew(Of Object)()
			services.Remove(GetType(Object))
			services.Remove(GetType(Object))

			Assert.AreEqual(1, utilityProvider.EventFireCount)
		End Sub

		<TestMethod()> _
		Public Sub RemovingServiceNotInWorkItemDoesntFireEvent()
			Dim services As TestableServiceCollection = CreateServiceCollection()

			Dim utilityProvider As EventHandlerUtilityProvider = New EventHandlerUtilityProvider
			utilityProvider.EventFired = False
			AddHandler services.Removed, AddressOf utilityProvider.EventFireHandler
			services.Remove(GetType(Object))

			Assert.IsFalse(utilityProvider.EventFired)
		End Sub

		<TestMethod()> _
		Public Sub RemovingMultiplyRegisteredServiceInstanceDoesntFireEventUntilLastInstanceIsRemoved()
			Dim str As String = "Hello world"
			Dim services As TestableServiceCollection = CreateServiceCollection()

			Dim utilityProvider As EventHandlerUtilityProvider = New EventHandlerUtilityProvider
			utilityProvider.EventFired = False
			AddHandler services.Removed, AddressOf utilityProvider.EventFireHandler

			services.Add(Of String)(str)
			services.Add(Of Object)(str)

			services.Remove(Of Object)()
			Assert.IsFalse(utilityProvider.EventFired)
			services.Remove(Of String)()
			Assert.IsTrue(utilityProvider.EventFired)
		End Sub

		<TestMethod()> _
		Public Sub DemandAddedServiceEventsAreFiredAtTheRightTime()
			Dim services As TestableServiceCollection = CreateServiceCollection()

			Dim addedEventTestUtilityProvider As EventHandlerUtilityProvider = New EventHandlerUtilityProvider
			addedEventTestUtilityProvider.EventFired = False
			Dim removedEventTestUtilityProvider As EventHandlerUtilityProvider = New EventHandlerUtilityProvider
			removedEventTestUtilityProvider.EventFired = False

			AddHandler services.Added, AddressOf addedEventTestUtilityProvider.EventFireHandler
			AddHandler services.Removed, AddressOf removedEventTestUtilityProvider.EventFireHandler

			services.AddOnDemand(Of Object)()
			Assert.IsFalse(addedEventTestUtilityProvider.EventFired)
			Assert.IsFalse(removedEventTestUtilityProvider.EventFired)

			services.Get(Of Object)()
			Assert.IsTrue(addedEventTestUtilityProvider.EventFired)
			Assert.IsFalse(removedEventTestUtilityProvider.EventFired)

			services.Remove(Of Object)()
			Assert.IsTrue(removedEventTestUtilityProvider.EventFired)
		End Sub

#End Region

#Region "Located For DI Resolution"

		<TestMethod()> _
		Public Sub AddedServiceCanBeLocatedByTypeIDPair()
			Dim collection As TestableServiceCollection = CreateServiceCollection()

			Dim obj As Object = collection.AddNew(Of Object)()

			Assert.AreSame(obj, collection.Locator.Get(New DependencyResolutionLocatorKey(GetType(Object), Nothing)))
		End Sub

		<TestMethod()> _
		Public Sub RemovingItemRemovesTypeIdPairFromLocator()
			Dim collection As TestableServiceCollection = CreateServiceCollection()
			Dim obj As Object = collection.AddNew(Of Object)()

			collection.Remove(GetType(Object))

			Assert.IsNull(collection.Locator.Get(New DependencyResolutionLocatorKey(GetType(Object), Nothing)))
		End Sub

		<TestMethod()> _
		Public Sub RemovingSpecificServiceTypeRegistrationsRemovesOnlyThoseDependencyKeys()
			Dim collection As TestableServiceCollection = CreateServiceCollection()
			Dim str As String = "Hello world"

			collection.Add(Of String)(str)
			collection.Add(Of Object)(str)

			collection.Remove(GetType(Object))
			Assert.IsNull(collection.Locator.Get(New DependencyResolutionLocatorKey(GetType(Object), Nothing)))
			Assert.IsNotNull(collection.Locator.Get(New DependencyResolutionLocatorKey(GetType(String), Nothing)))

			collection.Remove(GetType(String))
			Assert.IsNull(collection.Locator.Get(New DependencyResolutionLocatorKey(GetType(String), Nothing)))
		End Sub

#End Region

#Region "Helpers"

		Private Class MockTearDownStrategy
			Inherits BuilderStrategy

			Public TearDownCalled As Boolean = False

			Public Overrides Function TearDown(ByVal context As IBuilderContext, ByVal item As Object) As Object
				TearDownCalled = True
				Return MyBase.TearDown(context, item)
			End Function
		End Class

		Private Function CreateServiceCollection() As TestableServiceCollection
			Dim container As LifetimeContainer = New LifetimeContainer()
			Dim locator As Locator = New Locator()
			locator.Add(GetType(ILifetimeContainer), container)

			Return New TestableServiceCollection(container, locator, CreateBuilder())
		End Function

		Private Function CreateBuilder() As Builder
			Dim result As Builder = New Builder()
			result.Policies.SetDefault(Of ISingletonPolicy)(New SingletonPolicy(True))
			Return result
		End Function

		Private Class TestableServiceCollection
			Inherits ServiceCollection

			Private innerBuilder As IBuilder(Of BuilderStage)
			Private container As ILifetimeContainer
			Private innerLocator As IReadWriteLocator

			Public Sub New(ByVal container As ILifetimeContainer, ByVal locator As IReadWriteLocator, ByVal builder As IBuilder(Of BuilderStage))
				Me.New(container, locator, builder, Nothing)
			End Sub

			Public Sub New(ByVal parent As TestableServiceCollection)
				Me.New(parent.container, New Locator(parent.Locator), parent.Builder, parent)
				innerLocator.Add(GetType(ILifetimeContainer), parent.Locator.Get(Of ILifetimeContainer)())
			End Sub

			Private Sub New(ByVal container As ILifetimeContainer, ByVal locator As IReadWriteLocator, ByVal builder As IBuilder(Of BuilderStage), ByVal parent As TestableServiceCollection)
				MyBase.New(container, locator, builder, parent)
				Me.innerBuilder = builder
				Me.container = container
				Me.innerLocator = locator
			End Sub

			Public ReadOnly Property LifetimeContainer() As ILifetimeContainer
				Get
					Return container
				End Get
			End Property

			Public ReadOnly Property Locator() As IReadWriteLocator
				Get
					Return innerLocator
				End Get
			End Property

			Public ReadOnly Property Builder() As IBuilder(Of BuilderStage)
				Get
					Return innerBuilder
				End Get
			End Property
		End Class

		Public Class BuilderAwareObject
			Implements IBuilderAware

			Public BuilderWasRun As Boolean = False
			Public BuilderRunCount As Integer = 0

			Public Sub OnBuiltUp(ByVal id As String) Implements IBuilderAware.OnBuiltUp
				BuilderWasRun = True
				BuilderRunCount += 1
			End Sub

			Public Sub OnTearingDown() Implements IBuilderAware.OnTearingDown
				BuilderWasRun = True
				BuilderRunCount += 1
			End Sub
		End Class

		Private Interface IMockDataObject
		End Interface

		Private Interface IMockDataObject2
		End Interface

		Private Class MockDataObject
			Implements IMockDataObject, IMockDataObject2

			Private intProp As Integer

			Public Property IntProperty() As Integer
				Get
					Return intProp
				End Get
				Set(ByVal value As Integer)
					intProp = value
				End Set
			End Property
		End Class

		Private Class MockDependencyService
		End Class

		Private Interface IMockDependingService
		End Interface

		Private Class MockDependingService
			Implements IMockDependingService

			Private innerInjectedService As MockDependencyService

			<ServiceDependency()> _
			Public Property InjectedService() As MockDependencyService
				Get
					Return innerInjectedService
				End Get
				Set(ByVal value As MockDependencyService)
					innerInjectedService = value
				End Set
			End Property
		End Class

		Private Class SomeService
			Private innerMyDependency As Object

			<ServiceDependency()> _
			Public Property MyDependency() As Object
				Get
					Return innerMyDependency
				End Get
				Set(ByVal value As Object)
					innerMyDependency = value
				End Set
			End Property
		End Class

		Private Interface IMockDemandService
		End Interface

		Private Class MockDemandService
			Implements IMockDemandService

			Public Shared WasCreated As Boolean

			Public Sub New()
				WasCreated = True
			End Sub
		End Class

#End Region
	End Class
End Namespace
