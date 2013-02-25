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
Imports Microsoft.Practices.CompositeUI.UIElements
Imports System.Collections

<TestClass()> _
 Public Class UIExtensionSiteCollectionFixture
#Region "Contains"

	<TestMethod()> _
	Public Sub CanFindOutIfSiteIsContainedInCollection()
		Dim collection As UIExtensionSiteCollection = New UIExtensionSiteCollection()

		collection.RegisterSite("Foo", New MockAdapter())

		Assert.IsTrue(collection.Contains("Foo"))
		Assert.IsFalse(collection.Contains("Foo2"))
	End Sub

	<TestMethod()> _
	Public Sub ContainsChecksParent()
		Dim parent As UIExtensionSiteCollection = New UIExtensionSiteCollection()
		Dim child As UIExtensionSiteCollection = New UIExtensionSiteCollection(parent)

		parent.RegisterSite("Foo", New MockAdapter())

		Assert.IsTrue(child.Contains("Foo"))
	End Sub

#End Region

#Region "Count"

	<TestMethod()> _
	Public Sub CanCountSitesInCollection()
		Dim collection As UIExtensionSiteCollection = New UIExtensionSiteCollection()

		collection.RegisterSite("Foo", New MockAdapter())

		Assert.AreEqual(1, collection.Count)
	End Sub

	<TestMethod()> _
	Public Sub CountIncludesParentSites()
		Dim parent As UIExtensionSiteCollection = New UIExtensionSiteCollection()
		Dim child As UIExtensionSiteCollection = New UIExtensionSiteCollection(parent)

		parent.RegisterSite("Foo", New MockAdapter())
		child.RegisterSite("Bar", New MockAdapter())

		Assert.AreEqual(1, parent.Count)
		Assert.AreEqual(2, child.Count)
	End Sub

	<TestMethod()> _
	Public Sub CountDoesNotIncludeDuplicates()
		Dim parent As UIExtensionSiteCollection = New UIExtensionSiteCollection()
		Dim child As UIExtensionSiteCollection = New UIExtensionSiteCollection(parent)

		parent.RegisterSite("Foo", New MockAdapter())
		Dim childSite As UIExtensionSite = child("Foo")

		Assert.AreEqual(1, child.Count)
	End Sub

#End Region

#Region "Indexer"

	<TestMethod(), ExpectedException(GetType(ArgumentException))> _
	Public Sub InvalidSiteNameThrows()
		Dim collection As UIExtensionSiteCollection = New UIExtensionSiteCollection()

		Dim unused As UIExtensionSite = collection("Foo")
	End Sub

	<TestMethod()> _
	Public Sub CanRetrieveSiteViaIndexer()
		Dim collection As UIExtensionSiteCollection = New UIExtensionSiteCollection()

		collection.RegisterSite("Foo", New MockAdapter())

		Assert.IsNotNull(collection("Foo"))
	End Sub

	<TestMethod()> _
	Public Sub CanRetrieveParentSiteViaIndexer()
		Dim parent As UIExtensionSiteCollection = New UIExtensionSiteCollection()
		Dim child As UIExtensionSiteCollection = New UIExtensionSiteCollection(parent)

		parent.RegisterSite("Foo", New MockAdapter())
		child.RegisterSite("Bar", New MockAdapter())

		Assert.IsNotNull(child("Foo"))
		Assert.IsNotNull(child("Bar"))
	End Sub

	<TestMethod()> _
	Public Sub ItemsAddedToLocalSiteDoNotAffectItemsInParentSite()
		Dim parent As UIExtensionSiteCollection = New UIExtensionSiteCollection()
		Dim child As UIExtensionSiteCollection = New UIExtensionSiteCollection(parent)
		parent.RegisterSite("Foo", New MockAdapter())

		Dim parentSite As UIExtensionSite = parent("Foo")
		Dim childSite As UIExtensionSite = child("Foo")
		parentSite.Add(New Object())
		childSite.Add(New Object())

		Assert.AreEqual(1, parentSite.Count)
		Assert.AreEqual(1, childSite.Count)
	End Sub

#End Region

#Region "RegisterSite (with object)"

	<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
	Public Sub NullObjectThrows()
		Dim collection As UIExtensionSiteCollection = New UIExtensionSiteCollection()

		collection.RegisterSite("Foo", CObj(Nothing))
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
	Public Sub NullSiteNameWithObjectThrows()
		Dim collection As UIExtensionSiteCollection = New UIExtensionSiteCollection()

		collection.RegisterSite(Nothing, New Object())
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentException))> _
	Public Sub EmptySiteNameWithObjectThrows()
		Dim collection As UIExtensionSiteCollection = New UIExtensionSiteCollection()

		collection.RegisterSite("", New Object())
	End Sub

	<TestMethod()> _
	Public Sub CanRegisterSiteFromObjectAndFactoryCatalogIsUsed()
		Dim wi As TestableRootWorkItem = New TestableRootWorkItem()
		wi.Services.Remove(Of IUIElementAdapterFactoryCatalog)()
		Dim factoryCatalog As MockFactoryCatalog = wi.Services.AddNew(Of MockFactoryCatalog, IUIElementAdapterFactoryCatalog)()
		Dim collection As UIExtensionSiteCollection = New UIExtensionSiteCollection(wi)
		Dim obj As Object = New Object()

		collection.RegisterSite("Foo", New Object())
		collection("Foo").Add(obj)

		Assert.AreSame(obj, factoryCatalog.Adapter.AddedElement)
	End Sub

#End Region

#Region "RegisterSite (with adapter)"

	<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
	Public Sub NullAdapterThrows()
		Dim collection As UIExtensionSiteCollection = New UIExtensionSiteCollection()

		collection.RegisterSite("Foo", CType(Nothing, IUIElementAdapter))
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
	Public Sub NullSiteNameWithAdapterThrows()
		Dim collection As UIExtensionSiteCollection = New UIExtensionSiteCollection()

		collection.RegisterSite(Nothing, New MockAdapter())
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentException))> _
	Public Sub EmptySiteNameWithAdapterThrows()
		Dim collection As UIExtensionSiteCollection = New UIExtensionSiteCollection()

		collection.RegisterSite("", New MockAdapter())
	End Sub

	<TestMethod()> _
	Public Sub CanRegisterAdapterForSiteAndItIsUsedWithinTheSite()
		Dim adapter As MockAdapter = New MockAdapter()
		Dim collection As UIExtensionSiteCollection = New UIExtensionSiteCollection()
		Dim obj As Object = New Object()

		collection.RegisterSite("Foo", adapter)
		collection("Foo").Add(obj)

		Assert.AreSame(obj, adapter.AddedElement)
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentException))> _
	Public Sub RegisterSiteAdapterTwiceThrows()
		Dim collection As UIExtensionSiteCollection = New UIExtensionSiteCollection()

		collection.RegisterSite("Foo", New MockAdapter())
		collection.RegisterSite("Foo", New MockAdapter())
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentException))> _
	Public Sub TryingToOverrideParentSiteThrows()
		Dim parent As UIExtensionSiteCollection = New UIExtensionSiteCollection()
		Dim child As UIExtensionSiteCollection = New UIExtensionSiteCollection(parent)

		parent.RegisterSite("Foo", New MockAdapter())
		child.RegisterSite("Foo", New MockAdapter())
	End Sub

#End Region

#Region "UnregisterSite"

	<TestMethod()> _
	Public Sub CanUnregisterSiteCreatedWithObject()
		Dim wi As TestableRootWorkItem = New TestableRootWorkItem()
		wi.Services.Remove(Of IUIElementAdapterFactoryCatalog)()
		wi.Services.AddNew(Of MockFactoryCatalog, IUIElementAdapterFactoryCatalog)()
		Dim collection As UIExtensionSiteCollection = New UIExtensionSiteCollection(wi)

		collection.RegisterSite("foo", New Object())
		collection.UnregisterSite("foo")

		Assert.IsFalse(collection.Contains("foo"))
	End Sub

	<TestMethod()> _
	Public Sub CanUnregisterSiteCreatedWithAdapter()
		Dim collection As UIExtensionSiteCollection = New UIExtensionSiteCollection()

		collection.RegisterSite("foo", New MockAdapter())
		collection.UnregisterSite("foo")

		Assert.IsFalse(collection.Contains("foo"))
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentException))> _
	Public Sub UnregisterSiteCreatedInParentThrows()
		Dim parent As UIExtensionSiteCollection = New UIExtensionSiteCollection()
		Dim child As UIExtensionSiteCollection = New UIExtensionSiteCollection(parent)

		parent.RegisterSite("Foo", New MockAdapter())
		child.UnregisterSite("Foo")
	End Sub

#End Region

	<TestMethod()> _
	Public Sub DisposingWorkItemClearsUIExtensionSites()

		Dim parent As WorkItem = New TestableRootWorkItem()
		Dim uiAdapter As MockUIAdapter = New MockUIAdapter()

		parent.UIExtensionSites.RegisterSite("Foo", uiAdapter)
		parent.UIExtensionSites("Foo").Add(New Object())
		parent.Dispose()

		Assert.AreEqual(0, uiAdapter.Items.Count)
	End Sub

	Public Class MockUIAdapter
		Implements IUIElementAdapter

		Public Items As List(Of Object) = New List(Of Object)()

		Public Function Add(ByVal uiElement As Object) As Object Implements IUIElementAdapter.Add
			Items.Add(uiElement)
			Return uiElement
		End Function

		Public Sub Remove(ByVal uiElement As Object) Implements IUIElementAdapter.Remove
			Items.Remove(uiElement)
		End Sub

	End Class


#Region "Helpers"

	Private Class MockAdapter
		Implements IUIElementAdapter

		Public AddedElement As Object = Nothing
		Public RemovedElement As Object = Nothing

		Public Function Add(ByVal uiElement As Object) As Object Implements IUIElementAdapter.Add
			AddedElement = uiElement
			Return uiElement
		End Function

		Public Sub Remove(ByVal uiElement As Object) Implements IUIElementAdapter.Remove
			RemovedElement = uiElement
		End Sub
	End Class

	Private Class MockFactory
		Implements IUIElementAdapterFactory

		Private adapter As MockAdapter

		Public Sub New(ByVal adapter As MockAdapter)
			Me.adapter = adapter
		End Sub

		Public Function GetAdapter(ByVal uiElement As Object) As IUIElementAdapter Implements IUIElementAdapterFactory.GetAdapter
			Return adapter
		End Function

		Public Function Supports(ByVal uiElement As Object) As Boolean Implements IUIElementAdapterFactory.Supports
			Return True
		End Function
	End Class

	Private Class MockFactoryCatalog
		Implements IUIElementAdapterFactoryCatalog

		Public Adapter As MockAdapter = New MockAdapter()
		Private innerFactories As List(Of IUIElementAdapterFactory) = New List(Of IUIElementAdapterFactory)()

		Public Sub New()
			innerFactories.Add(New MockFactory(Adapter))
		End Sub

		Public ReadOnly Property Factories() As IList(Of IUIElementAdapterFactory) Implements IUIElementAdapterFactoryCatalog.Factories
			Get
				Return innerFactories.AsReadOnly()
			End Get
		End Property

		Public Function GetFactory(ByVal element As Object) As IUIElementAdapterFactory Implements IUIElementAdapterFactoryCatalog.GetFactory
			Return innerFactories(0)
		End Function

		Public Sub RegisterFactory(ByVal factory As IUIElementAdapterFactory) Implements IUIElementAdapterFactoryCatalog.RegisterFactory
			Throw New NotImplementedException()
		End Sub
	End Class

#End Region
End Class

