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
Imports Microsoft.Practices.CompositeUI.Collections
Imports Microsoft.Practices.CompositeUI.Utility
Imports Microsoft.Practices.ObjectBuilder

Namespace Collections
	<TestClass()> _
	Public Class ManagedObjectCollectionFixture
#Region "Add"

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub AddingNullObjectThrows()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()
			collection.Add(Nothing)
		End Sub

		<TestMethod()> _
		Public Sub AddedObjectStoredInProvidedContainer()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()
			Dim obj As Object = New Object()

			collection.Add(obj)

			Assert.IsTrue(collection.LifetimeContainer.Contains(obj))
		End Sub

#Region "Utilitary code for subprocedure AddedObjectIsAddedToLocator"

		Private Class AddedObjectIsAddedToLocatorUtilityProvider
			Private objField As Object
			Public Property Obj() As Object
				Get
					Return objField
				End Get
				Set(ByVal objArgument As Object)
					objField = objArgument
				End Set
			End Property

			Public Function CompareObj(ByVal pair As KeyValuePair(Of Object, Object)) As Boolean
				Return pair.Value Is objField
			End Function
		End Class

#End Region

		<TestMethod()> _
		Public Sub AddedObjectIsAddedToLocator()
			Dim obj As Object = New Object()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()

			collection.Add(obj)

			Dim utilityProvider As AddedObjectIsAddedToLocatorUtilityProvider = New AddedObjectIsAddedToLocatorUtilityProvider()
			utilityProvider.Obj = obj
			Dim locator As IReadableLocator = collection.Locator.FindBy(AddressOf utilityProvider.CompareObj)

			Assert.AreEqual(1, locator.Count)
		End Sub

		<TestMethod()> _
		Public Sub AddingObjectRunsTheBuilder()
			Dim obj As BuilderAwareObject = New BuilderAwareObject()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()

			collection.Add(obj)

			Assert.IsTrue(obj.BuilderWasRun)
		End Sub

		<TestMethod()> _
		Public Sub AddingUnnamedObjectTwiceYieldsSingleObjectInContainer()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()
			Dim obj As Object = New Object()

			collection.Add(obj)
			collection.Add(obj)

			Assert.AreEqual(1, collection.LifetimeContainer.Count)
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentException))> _
		Public Sub AddingSameObjectWithSameNameThrows()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()
			Dim obj As Object = New Object()

			collection.Add(obj, "Foo")
			collection.Add(obj, "Foo")
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentException))> _
		Public Sub AddingTwoDifferentObjectsWithSameNameThrows()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()

			collection.Add(New Object(), "Foo")
			collection.Add("Bar", "Foo")
		End Sub

		<TestMethod()> _
		Public Sub AddingObjectTwiceOnlyInjectsDependenciesOnce()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()

			Dim obj As MockDataObject = New MockDataObject()

			Dim policy1 As PropertySetterPolicy = New PropertySetterPolicy()
			policy1.Properties.Add("IntProperty", New PropertySetterInfo("IntProperty", New ValueParameter(Of Integer)(19)))
			collection.Builder.Policies.Set(Of IPropertySetterPolicy)(policy1, GetType(MockDataObject), "Foo")

			Dim policy2 As PropertySetterPolicy = New PropertySetterPolicy()
			policy2.Properties.Add("IntProperty", New PropertySetterInfo("IntProperty", New ValueParameter(Of Integer)(36)))
			collection.Builder.Policies.Set(Of IPropertySetterPolicy)(policy2, GetType(MockDataObject), "Bar")

			collection.Add(obj, "Foo")
			Assert.AreEqual(19, obj.IntProperty)

			collection.Add(obj, "Bar")
			Assert.AreEqual(19, obj.IntProperty)
		End Sub

		<TestMethod()> _
		Public Sub CanAddSameObjectWithManyNames()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()
			Dim obj As Object = New Object()

			collection.Add(obj, "Foo")
			collection.Add(obj, "Bar")

			Assert.AreEqual(1, collection.LifetimeContainer.Count)
			Assert.AreSame(obj, collection.Get("Foo"))
			Assert.AreSame(obj, collection.Get("Bar"))
		End Sub

#End Region

#Region "AddNew - Generic"

		<TestMethod()> _
		Public Sub AddNewWillCreateANewObjectAndGiveItToMe_Generic()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()

			Dim obj As Object = collection.AddNew(Of Object)()

			Assert.IsNotNull(obj)
		End Sub

		<TestMethod()> _
		Public Sub AddNewNamedWillCreateANewObjectAndGiveItToMe_Generic()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()

			Dim obj As Object = collection.AddNew(Of Object)("Foo")

			Assert.IsNotNull(obj)
		End Sub

		<TestMethod()> _
		Public Sub AddNewAddsToLocatorAndLifetimeContainer_Generic()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()

			Dim obj As Object = collection.AddNew(Of Object)("Foo")

			Assert.IsTrue(collection.LifetimeContainer.Contains(obj))
			Assert.AreEqual(obj, collection.Get("Foo"))
		End Sub

		<TestMethod()> _
		Public Sub AddNewOnlyCallsBuilderOnce_Generic()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()

			Dim obj As BuilderAwareObject = collection.AddNew(Of BuilderAwareObject)()

			Assert.AreEqual(1, obj.BuilderRunCount)
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentException))> _
		Public Sub CreatingTwoObjectsOfDifferentTypesButTheSameNameThrows_Generic()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()

			collection.AddNew(Of Object)("One")
			collection.AddNew(Of Integer)("One")
		End Sub

#End Region

#Region "AddNew - Non-Generic"

		<TestMethod()> _
		Public Sub AddNewWillCreateANewObjectAndGiveItToMe()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()

			Dim obj As Object = collection.AddNew(GetType(Object))

			Assert.IsNotNull(obj)
		End Sub

		<TestMethod()> _
		Public Sub AddNewNamedWillCreateANewObjectAndGiveItToMe()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()

			Dim obj As Object = collection.AddNew(GetType(Object), "Foo")

			Assert.IsNotNull(obj)
		End Sub

		<TestMethod()> _
		Public Sub AddNewAddsToLocatorAndLifetimeContainer()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()

			Dim obj As Object = collection.AddNew(GetType(Object), "Foo")

			Assert.IsTrue(collection.LifetimeContainer.Contains(obj))
			Assert.AreEqual(obj, collection.Get("Foo"))
		End Sub

		<TestMethod()> _
		Public Sub AddNewOnlyCallsBuilderOnce()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()

			Dim obj As BuilderAwareObject = CType(collection.AddNew(GetType(BuilderAwareObject)), BuilderAwareObject)

			Assert.AreEqual(1, obj.BuilderRunCount)
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentException))> _
		Public Sub CreatingTwoObjectsOfDifferentTypesButTheSameNameThrows()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()

			collection.AddNew(GetType(Object), "One")
			collection.AddNew(GetType(Integer), "One")
		End Sub

#End Region

#Region "Contains"

		<TestMethod()> _
		Public Sub CanFindOutIfCollectionContainsNamedObject()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()
			Dim obj As Object = New Object()

			collection.Add(obj, "Foo")

			Assert.IsTrue(collection.Contains("Foo"))
		End Sub

		<TestMethod()> _
		Public Sub ContainsDoesNotCheckParent()
			Dim parentCollection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()
			Dim childCollection As TestableManagedObjectCollection(Of Object) = New TestableManagedObjectCollection(Of Object)(parentCollection)
			Dim obj As Object = New Object()

			parentCollection.Add(obj, "Foo")

			Assert.IsFalse(childCollection.Contains("Foo"))
		End Sub

		<TestMethod()> _
		Public Sub CanFindOutIfCollectionContainsObject()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()
			Dim obj As Object = New Object()

			collection.Add(obj, "Foo")

			Assert.IsTrue(collection.ContainsObject(obj))
		End Sub

#End Region

#Region "Count"

		<TestMethod()> _
		Public Sub EmptyCollectionHasNoItemsInIt()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()

			Assert.AreEqual(0, collection.Count)
		End Sub

		<TestMethod()> _
		Public Sub CountReflectsNumberOfRegistrations()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()
			Dim obj As Object = New Object()

			collection.Add(obj, "Foo")
			collection.Add(obj, "Bar")

			Assert.AreEqual(2, collection.Count)
		End Sub

		<TestMethod()> _
		Public Sub CountReturnsLocalObjectCountOnly()
			Dim parentCollection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()
			Dim childCollection As TestableManagedObjectCollection(Of Object) = New TestableManagedObjectCollection(Of Object)(parentCollection)

			parentCollection.AddNew(Of Object)()

			Assert.AreEqual(1, parentCollection.Count)
			Assert.AreEqual(0, childCollection.Count)
		End Sub

		<TestMethod()> _
		Public Sub CouldIgnoresObjectsOfTheWrongType()
			Dim objectCollection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()
			Dim stringCollection As TestableManagedObjectCollection(Of String) = New TestableManagedObjectCollection(Of String)(objectCollection.LifetimeContainer, objectCollection.Locator, objectCollection.Builder, objectCollection.SearchMode, Nothing, Nothing, Nothing)

			Dim obj1 As Object = New Object()
			Dim obj2 As Object = New Object()
			Dim obj3 As String = "Hello there!"

			objectCollection.Add(obj1)
			objectCollection.Add(obj2)
			objectCollection.Add(obj3)

			Assert.AreEqual(3, objectCollection.Count)
			Assert.AreEqual(1, stringCollection.Count)
		End Sub

#End Region

#Region "FindByType - Generic"

		<TestMethod()> _
		Public Sub CanFindObjectsByAssignableType_Generic()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()

			collection.Add("Hello world")

			Assert.AreEqual(1, collection.FindByType(Of String)().Count)
			Assert.AreEqual(1, collection.FindByType(Of Object)().Count)
			Assert.AreEqual(0, collection.FindByType(Of Integer)().Count)
		End Sub

		<TestMethod()> _
		Public Sub FindByTypeSearchesParentContainerWhenConfiguredForSearchUp_Generic()
			Dim parentCollection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()
			Dim childCollection As ManagedObjectCollection(Of Object) = New ManagedObjectCollection(Of Object)(New LifetimeContainer(), New Locator(parentCollection.Locator), parentCollection.Builder, SearchMode.Up, Nothing, Nothing, parentCollection)

			parentCollection.AddNew(Of Object)()

			Assert.AreEqual(1, childCollection.FindByType(Of Object)().Count)
		End Sub

#End Region

#Region "FindByType - Non-Generic"

		<TestMethod()> _
		Public Sub CanFindObjectsByAssignableType()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()

			collection.Add("Hello world")

			Assert.AreEqual(1, collection.FindByType(GetType(String)).Count)
			Assert.AreEqual(1, collection.FindByType(GetType(Object)).Count)
			Assert.AreEqual(0, collection.FindByType(GetType(Integer)).Count)
		End Sub

		<TestMethod()> _
		Public Sub FindByTypeSearchesParentContainerWhenConfiguredForSearchUp()
			Dim parentCollection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()
			Dim childCollection As ManagedObjectCollection(Of Object) = _
				New ManagedObjectCollection(Of Object)(New LifetimeContainer(), _
					New Locator(parentCollection.Locator), parentCollection.Builder, _
					SearchMode.Up, Nothing, Nothing, parentCollection)

			parentCollection.AddNew(Of Object)()

			Assert.AreEqual(1, childCollection.FindByType(GetType(Object)).Count)
		End Sub

#End Region

#Region "Get - Generic"

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub GetWithNullIDThrows_Generic()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()

			collection.Get(Of Object)(Nothing)
		End Sub

		<TestMethod()> _
		Public Sub CanAddNamedObjectAndFindItByName_Generic()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()

			Dim obj As Object = collection.AddNew(Of Object)("foo")

			Assert.AreSame(obj, collection.Get(Of Object)("foo"))
		End Sub

		<TestMethod()> _
		Public Sub GetForObjectNotInCollectionReturnsNull_Generic()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()

			Assert.IsNull(collection.Get(Of Object)("bar"))
		End Sub

		<TestMethod()> _
		Public Sub GetDoesNotCheckParentCollection_Generic()
			Dim parentCollection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()
			Dim childCollection As TestableManagedObjectCollection(Of Object) = New TestableManagedObjectCollection(Of Object)(parentCollection)
			Dim obj As Object = New Object()

			parentCollection.Add(obj, "Foo")

			Assert.IsNull(childCollection.Get(Of Object)("Foo"))
		End Sub

#End Region

#Region "Get - Non-Generic"

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub GetWithNullIDThrows()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()

			collection.Get(Nothing)
		End Sub

		<TestMethod()> _
		Public Sub CanAddNamedObjectAndFindItByName()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()

			Dim obj As Object = collection.AddNew(Of Object)("foo")

			Assert.AreSame(obj, collection.Get("foo"))
		End Sub

		<TestMethod()> _
		Public Sub GetForObjectNotInCollectionReturnsNull()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()

			Assert.IsNull(collection.Get("bar"))
		End Sub

		<TestMethod()> _
		Public Sub GetDoesNotCheckParentCollection()
			Dim parentCollection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()
			Dim childCollection As TestableManagedObjectCollection(Of Object) = New TestableManagedObjectCollection(Of Object)(parentCollection)
			Dim obj As Object = New Object()

			parentCollection.Add(obj, "Foo")

			Assert.IsNull(childCollection.Get("Foo"))
		End Sub

#End Region

#Region "Indexer"

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub IndexerWithNullIDThrows()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()

			Dim unused As Object = collection(Nothing)
		End Sub

		<TestMethod()> _
		Public Sub CanAddNamedObjectAndIndexItByName()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()

			Dim obj As Object = collection.AddNew(Of Object)("foo")

			Assert.AreSame(obj, collection("foo"))
		End Sub

		<TestMethod()> _
		Public Sub IndexerForObjectNotInCollectionReturnsNull()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()

			Assert.IsNull(collection("bar"))
		End Sub

		<TestMethod()> _
		Public Sub IndexerDoesNotCheckParentCollection()
			Dim parentCollection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()
			Dim childCollection As TestableManagedObjectCollection(Of Object) = New TestableManagedObjectCollection(Of Object)(parentCollection)
			Dim obj As Object = New Object()

			parentCollection.Add(obj, "Foo")

			Assert.IsNull(childCollection("Foo"))
		End Sub

#End Region

#Region "Remove"

		<TestMethod()> _
		Public Sub RemovingNullDoesntThrow()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()

			collection.Remove(Nothing)
		End Sub

		<TestMethod()> _
		Public Sub RemoveRemovesFromLifetimeContainer()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()
			Dim obj As Object = New Object()

			collection.Add(obj)
			collection.Remove(obj)

			Assert.IsFalse(collection.LifetimeContainer.Contains(obj))
		End Sub

		<TestMethod()> _
		Public Sub RemoveRemovesFromLocator()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()
			Dim obj As Object = New Object()

			collection.Add(obj, "Foo")
			collection.Remove(obj)

			Assert.IsFalse(collection.Locator.Contains("Foo"))
		End Sub

		<TestMethod()> _
		Public Sub RemovingObjectNotInCollectionNoThrow()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()

			collection.Remove(New Object())
		End Sub

		<TestMethod()> _
		Public Sub RemovingNamedObjectCausesNameToBeAvailableAgain()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()

			Dim obj As Object = collection.AddNew(Of Object)("Foo")
			collection.Remove(obj)
			Dim obj2 As Object = collection.AddNew(Of Object)("Foo")

			Assert.IsNotNull(obj2)
			Assert.IsTrue(Not obj Is obj2)
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentException))> _
		Public Sub RemovingWorkItemThrows()
			Dim root As TestableRootWorkItem = New TestableRootWorkItem()
			Dim child As WorkItem = root.Items.AddNew(Of WorkItem)()

			root.Items.Remove(child)
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentException))> _
		Public Sub RemovingWorkItemDerivedClassThrows()
			Dim root As TestableRootWorkItem = New TestableRootWorkItem()
			Dim child As MockWorkItem = root.Items.AddNew(Of MockWorkItem)()

			root.Items.Remove(child)
		End Sub

		<TestMethod()> _
		Public Sub RemovingObjectCausesItToBeTornDown()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()
			Dim strategy As MockTearDownStrategy = New MockTearDownStrategy()
			collection.Builder.Strategies.Add(strategy, BuilderStage.PreCreation)

			Dim obj As Object = collection.AddNew(Of Object)()
			collection.Remove(obj)

			Assert.IsTrue(strategy.TearDownCalled)
		End Sub

#End Region

#Region "IEnumerable"

		<TestMethod()> _
		Public Sub CanEnumerateCollection()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()
			Dim obj1 As Object = New Object()
			Dim obj2 As Object = New Object()

			collection.Add(obj1)
			collection.Add(obj2)

			Dim o1Found As Boolean = False
			Dim o2Found As Boolean = False
			For Each pair As KeyValuePair(Of String, Object) In collection
				If pair.Value Is obj1 Then
					o1Found = True
				End If
				If pair.Value Is obj2 Then
					o2Found = True
				End If
			Next pair

			Assert.IsTrue(o1Found)
			Assert.IsTrue(o2Found)
		End Sub

		<TestMethod()> _
		Public Sub EnumeratorIgnoresItemsAddedDirectlyToLocator()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()
			Dim obj1 As Object = New Object()
			Dim obj2 As Object = New Object()
			Dim obj3 As Object = New Object()

			collection.Add(obj1)
			collection.Add(obj2)
			collection.Locator.Add("Foo", obj3)

			Dim o3Found As Boolean = False
			For Each pair As KeyValuePair(Of String, Object) In collection
				If pair.Value Is obj3 Then
					o3Found = True
				End If
			Next pair

			Assert.IsFalse(o3Found)
		End Sub

		<TestMethod()> _
		Public Sub EnumeratorIgnoresItemsOfTheWrongTypeInTheLocator()
			Dim objectCollection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()
			Dim stringCollection As TestableManagedObjectCollection(Of String) = New TestableManagedObjectCollection(Of String)(objectCollection.LifetimeContainer, objectCollection.Locator, objectCollection.Builder, objectCollection.SearchMode, Nothing, Nothing, Nothing)

			Dim obj1 As Object = New Object()
			Dim obj2 As Object = New Object()
			Dim obj3 As String = "Hello there!"

			objectCollection.Add(obj1)
			objectCollection.Add(obj2)
			objectCollection.Add(obj3)

			Dim o1Found As Boolean = False
			Dim o2Found As Boolean = False
			Dim o3Found As Boolean = False

			For Each pair As KeyValuePair(Of String, String) In stringCollection
				If Object.ReferenceEquals(pair.Value, obj1) Then
					o1Found = True
				End If
				If Object.ReferenceEquals(pair.Value, obj2) Then
					o2Found = True
				End If
				If Object.ReferenceEquals(pair.Value, obj3) Then
					o3Found = True
				End If
			Next pair

			Assert.IsFalse(o1Found)
			Assert.IsFalse(o2Found)
			Assert.IsTrue(o3Found)
		End Sub

#End Region

#Region "SearchMode Flags"

		<TestMethod()> _
		Public Sub GetCanSearchUpTheLocatorChain()
			Dim parentCollection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection(SearchMode.Up)
			Dim childCollection As TestableManagedObjectCollection(Of Object) = New TestableManagedObjectCollection(Of Object)(parentCollection)

			Dim obj As Object = parentCollection.AddNew(Of Object)("Foo")

			Assert.AreSame(obj, childCollection.Get("Foo"))
		End Sub

		<TestMethod()> _
		Public Sub EnumerationCanSearchUpTheLocatorChain()
			Dim parentCollection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection(SearchMode.Up)
			Dim childCollection As TestableManagedObjectCollection(Of Object) = New TestableManagedObjectCollection(Of Object)(parentCollection)

			Dim obj1 As Object = parentCollection.AddNew(Of Object)("Foo")
			Dim obj2 As Object = childCollection.AddNew(Of Object)("Bar")

			Dim o1Found As Boolean = False
			Dim o2Found As Boolean = False
			For Each pair As KeyValuePair(Of String, Object) In childCollection
				If pair.Value Is obj1 Then
					o1Found = True
				End If
				If pair.Value Is obj2 Then
					o2Found = True
				End If
			Next pair

			Assert.IsTrue(o1Found)
			Assert.IsTrue(o2Found)
		End Sub

		<TestMethod()> _
		Public Sub GetDoesNotReturnReplacedObject()
			Dim parentCollection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection(SearchMode.Up)
			Dim childCollection As TestableManagedObjectCollection(Of Object) = New TestableManagedObjectCollection(Of Object)(parentCollection)

			Dim obj1 As Object = parentCollection.AddNew(Of Object)("Foo")
			Dim obj2 As Object = childCollection.AddNew(Of Object)("Foo")

			Assert.AreSame(obj2, childCollection.Get("Foo"))
		End Sub

		<TestMethod()> _
		Public Sub EnumerationDoesNotReturnReplacedObjects()
			Dim parentCollection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection(SearchMode.Up)
			Dim childCollection As TestableManagedObjectCollection(Of Object) = New TestableManagedObjectCollection(Of Object)(parentCollection)

			Dim obj1 As Object = parentCollection.AddNew(Of Object)("Foo")
			Dim obj2 As Object = childCollection.AddNew(Of Object)("Foo")

			Dim o1Found As Boolean = False
			Dim o2Found As Boolean = False
			For Each pair As KeyValuePair(Of String, Object) In childCollection
				If pair.Value Is obj1 Then
					o1Found = True
				End If
				If pair.Value Is obj2 Then
					o2Found = True
				End If
			Next pair

			Assert.IsFalse(o1Found)
			Assert.IsTrue(o2Found)
		End Sub

#End Region

#Region "IndexerBehavior Flags"

#Region "Utilitary code for subprocedures IndexerCreatesWhenFlagSetToCreateAndItemDoesNotExist and CreatedIndexerItemIsRetrievedForSecondIndex"

		Private Function ObjectCreator(ByVal t As System.Type, ByVal id As String) As Object
			Return New Object()
		End Function

#End Region

		<TestMethod()> _
		Public Sub IndexerCreatesWhenFlagSetToCreateAndItemDoesNotExist()
			Dim collection As TestableManagedObjectCollection(Of Object) = _
				CreateManagedObjectCollection(SearchMode.Local, AddressOf ObjectCreator)

			Dim foo As Object = collection("Foo")

			Assert.IsNotNull(foo)
		End Sub

		<TestMethod()> _
		Public Sub CreatedIndexerItemIsRetrievedForSecondIndex()
			Dim collection As TestableManagedObjectCollection(Of Object) = _
				CreateManagedObjectCollection(SearchMode.Local, AddressOf ObjectCreator)

			Dim foo As Object = collection("Foo")
			Dim foo2 As Object = collection("Foo")

			Assert.AreSame(foo, foo2)
		End Sub

#End Region

#Region "Filters"

#Region "Utilitary code for subprocedures GetFiltersReturnedCollectionWithPredicate and IndexFiltersReturnedCollectionWithPredicate"

		Private Function CompareMockDataObjectType(ByVal obj As Object) As Boolean
			Return (TypeOf obj Is MockDataObject)
		End Function

#End Region

		<TestMethod()> _
		Public Sub GetFiltersReturnedCollectionWithPredicate()
			Dim filter As Predicate(Of Object) = AddressOf CompareMockDataObjectType

			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection(SearchMode.Local, Nothing, Filter)

			Dim o1 As Object = collection.AddNew(Of Object)("One")
			Dim o2 As MockDataObject = collection.AddNew(Of MockDataObject)("Two")

			Assert.IsNull(collection.Get("One"))
			Assert.AreSame(o2, collection.Get("Two"))
		End Sub

		<TestMethod()> _
		Public Sub IndexFiltersReturnedCollectionWithPredicate()
			Dim filter As Predicate(Of Object) = AddressOf CompareMockDataObjectType

			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection(SearchMode.Local, Nothing, Filter)

			Dim o1 As Object = collection.AddNew(Of Object)("One")
			Dim o2 As MockDataObject = collection.AddNew(Of MockDataObject)("Two")

			Assert.IsNull(collection("One"))
			Assert.AreSame(o2, collection("Two"))
		End Sub

		<TestMethod()> _
		Public Sub FilteredObjectWillNotBeReplacedByCreateNewIndexerBehavior()
			Dim filter As Predicate(Of Object) = AddressOf CompareMockDataObjectType

			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection(SearchMode.Local, AddressOf ObjectCreator, filter)

			Dim o1 As Object = Collection.AddNew(Of Object)("One")
			Assert.IsNull(Collection("One"))
		End Sub

		<TestMethod()> _
		Public Sub EnumerationFiltersReturnedCollectionWithPredicate()
			Dim filter As Predicate(Of Object) = AddressOf CompareMockDataObjectType

			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection(SearchMode.Local, Nothing, Filter)

			Dim o1 As Object = collection.AddNew(Of Object)("One")
			Dim o2 As MockDataObject = collection.AddNew(Of MockDataObject)("Two")

			Dim o1Found As Boolean = False
			Dim o2Found As Boolean = False

			For Each pair As KeyValuePair(Of String, Object) In collection
				If pair.Value.Equals(o1) Then
					o1Found = True
				End If
				If pair.Value.Equals(o2) Then
					o2Found = True
				End If
			Next pair

			Assert.IsFalse(o1Found)
			Assert.IsTrue(o2Found)
		End Sub

#End Region

#Region "Located For DI Resolution"

		<TestMethod()> _
		Public Sub AddedItemCanBeLocatedByTypeIDPair()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()

			Dim obj As Object = collection.AddNew(Of Object)("Foo")

			Assert.AreSame(obj, collection.Locator.Get(New DependencyResolutionLocatorKey(GetType(Object), "Foo")))
		End Sub

		<TestMethod()> _
		Public Sub RemovingItemRemovesTypeIdPairFromLocator()
			Dim collection As TestableManagedObjectCollection(Of Object) = CreateManagedObjectCollection()
			Dim obj As Object = collection.AddNew(Of Object)("Foo")

			collection.Remove(obj)

			Assert.IsNull(collection.Locator.Get(New DependencyResolutionLocatorKey(GetType(Object), "Foo")))
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

		Private Function CreateManagedObjectCollection() As TestableManagedObjectCollection(Of Object)
			Return CreateManagedObjectCollection(SearchMode.Local)
		End Function

		Private Function CreateManagedObjectCollection(ByVal searchMode As SearchMode) As TestableManagedObjectCollection(Of Object)
			Return CreateManagedObjectCollection(searchMode, Nothing)
		End Function

#Region "Utilitary code for function CreateManagedObjectCollection"

		Private Function ReturnTrue(ByVal obj As Object) As Boolean
			Return True
		End Function

#End Region

		Private Function CreateManagedObjectCollection(ByVal searchMode As SearchMode, ByVal indexerCreationDelegate As ManagedObjectCollection(Of Object).IndexerCreationDelegate) As TestableManagedObjectCollection(Of Object)
			Return CreateManagedObjectCollection(searchMode, indexerCreationDelegate, AddressOf ReturnTrue)
		End Function

		Private Function CreateManagedObjectCollection(ByVal searchMode As SearchMode, ByVal indexerCreationDelegate As ManagedObjectCollection(Of Object).IndexerCreationDelegate, ByVal filter As Predicate(Of Object)) As TestableManagedObjectCollection(Of Object)
			Dim container As LifetimeContainer = New LifetimeContainer()
			Dim locator As Locator = New Locator()
			locator.Add(GetType(ILifetimeContainer), container)

			Return New TestableManagedObjectCollection(Of Object)(container, locator, CreateBuilder(), searchMode, indexerCreationDelegate, filter, Nothing)
		End Function

		Private Function CreateBuilder() As Builder
			Dim result As Builder = New Builder()
			result.Policies.SetDefault(Of ISingletonPolicy)(New SingletonPolicy(True))
			Return result
		End Function

		Private Class TestableManagedObjectCollection(Of TItem)
			Inherits ManagedObjectCollection(Of TItem)

			Private innerBuilder As IBuilder(Of BuilderStage)
			Private container As ILifetimeContainer
			Private innerLocator As IReadWriteLocator
			Private innerSearchMode As SearchMode
			Private innerIndexerCreationDelegate As IndexerCreationDelegate
			Private predicate As Predicate(Of TItem)

			Public Sub New(ByVal container As ILifetimeContainer, ByVal locator As IReadWriteLocator, _
				ByVal builder As IBuilder(Of BuilderStage), ByVal searchMode As SearchMode, _
				ByVal indexerCreationDelegate As IndexerCreationDelegate, _
				ByVal predicate As Predicate(Of TItem), _
				ByVal parentCollection As TestableManagedObjectCollection(Of TItem))

				MyBase.New(container, locator, builder, searchMode, indexerCreationDelegate, predicate, parentCollection)
				Me.innerBuilder = builder
				Me.container = container
				Me.innerLocator = locator
				Me.innerSearchMode = searchMode
				Me.innerIndexerCreationDelegate = indexerCreationDelegate
				Me.predicate = predicate
			End Sub

			Public Sub New(ByVal parent As TestableManagedObjectCollection(Of TItem))
				Me.New(parent.container, New Locator(parent.Locator), parent.Builder, parent.SearchMode, parent.innerIndexerCreationDelegate, parent.predicate, parent)
				innerLocator.Add(GetType(ILifetimeContainer), parent.Locator.Get(Of ILifetimeContainer)())
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

			Public ReadOnly Property SearchMode() As SearchMode
				Get
					Return innerSearchMode
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

		Private Class MockDataObject
			Private innerIntProperty As Integer

			Public Property IntProperty() As Integer
				Get
					Return innerIntProperty
				End Get
				Set(ByVal value As Integer)
					innerIntProperty = Value
				End Set
			End Property
		End Class

		Private Class MockWorkItem
			Inherits WorkItem

		End Class

#End Region
	End Class
End Namespace
