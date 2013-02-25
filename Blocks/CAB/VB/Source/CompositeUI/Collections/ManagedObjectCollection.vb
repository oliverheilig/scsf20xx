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
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Globalization
Imports Microsoft.Practices.CompositeUI.Utility
Imports Microsoft.Practices.ObjectBuilder
Imports Microsoft.Practices.CompositeUI.Commands
Imports Microsoft.Practices.CompositeUI.EventBroker

Namespace Collections
	''' <summary>
	''' Represents a filtered collection of objects, based on the locator used by the <see cref="WorkItem"/>.
	''' </summary>
	Public Class ManagedObjectCollection(Of TItem)
		Implements ICollection, IEnumerable(Of KeyValuePair(Of String, TItem))

		Private container As ILifetimeContainer
		Private locator As IReadWriteLocator
		Private builder As IBuilder(Of BuilderStage)
		Private aSearchMode As SearchMode
		Private innerIndexerCreationDelegate As IndexerCreationDelegate
		Private filter As Predicate(Of TItem)
		Private parentCollection As ManagedObjectCollection(Of TItem)
		Private workItem As WorkItem

		''' <summary>
		''' The delegate used for auto-creation of an item when not present, but indexed.
		''' </summary>
		''' <param name="t">The type being asked for.</param>
		''' <param name="id">The ID being asked for.</param>
		''' <returns></returns>
		Public Delegate Function IndexerCreationDelegate(ByVal t As Type, ByVal id As String) As TItem

		''' <summary>
		''' Initializes an instance of the <see cref="ManagedObjectCollection{TItem}"/> class.
		''' </summary>
		Public Sub New(ByVal Container As ILifetimeContainer, _
		 ByVal Locator As IReadWriteLocator, ByVal Builder As IBuilder(Of BuilderStage), _
		 ByVal aSearchMode As SearchMode, ByVal IndexerCreationDelegate As IndexerCreationDelegate, _
		 ByVal Filter As Predicate(Of TItem))

			Me.container = Container
			Me.locator = Locator
			Me.builder = Builder
			Me.aSearchMode = aSearchMode
			Me.innerIndexerCreationDelegate = IndexerCreationDelegate
			Me.filter = Filter
			Me.parentCollection = Nothing
		End Sub

		''' <summary>
		''' Initializes an instance of the <see cref="ManagedObjectCollection{TItem}"/> class.
		''' </summary>
		Public Sub New(ByVal container As ILifetimeContainer, _
		 ByVal locator As IReadWriteLocator, ByVal builder As IBuilder(Of BuilderStage), _
		 ByVal aSearchMode As SearchMode, ByVal indexerCreationDelegate As IndexerCreationDelegate, _
		 ByVal filter As Predicate(Of TItem), ByVal parentCollection As ManagedObjectCollection(Of TItem))

			Me.container = container
			Me.locator = locator
			Me.builder = builder
			Me.aSearchMode = aSearchMode
			Me.innerIndexerCreationDelegate = indexerCreationDelegate
			Me.filter = filter
			Me.parentCollection = parentCollection
			Me.workItem = locator.Get(Of WorkItem)(New DependencyResolutionLocatorKey(GetType(WorkItem), Nothing))

			If Not Me.workItem Is Nothing Then
				AddHandler workItem.ObjectAdded, AddressOf WorkItem_ItemAdded
				AddHandler workItem.ObjectRemoved, AddressOf WorkItem_ItemRemoved
			End If
		End Sub

		''' <summary>
		''' Event that's fired when an object is added to the collection.
		''' </summary>
		Public Event Added As EventHandler(Of DataEventArgs(Of TItem))

		''' <summary>
		''' Event that's fired when an object is removed from the collection, or disposed when the
		''' collection is cleaning up.
		''' </summary>
		Public Event Removed As EventHandler(Of DataEventArgs(Of TItem))

		''' <summary>
		''' Gets an item by ID.
		''' </summary>
		''' <param name="id">The ID of the item to get.</param>
		''' <returns>The item, if present; null otherwise. If the collection has been given an
		''' <see cref="IndexerCreationDelegate"/> and the item is not found, it will be created
		''' using the delegate.</returns>
		Default Public ReadOnly Property Item(ByVal id As String) As TItem
			Get
				Dim result As TItem = [Get](id)

				If CObj(result) Is Nothing AndAlso Not innerIndexerCreationDelegate Is Nothing _
				 AndAlso (Not Contains(id, aSearchMode, False)) Then

					result = innerIndexerCreationDelegate(GetType(TItem), id)
					Add(result, id)
				End If

				Return result
			End Get
		End Property

		''' <summary>
		''' Gets the number of items in the collection.
		''' </summary>
		Public ReadOnly Property Count() As Integer
			Get
				Dim result As Integer = 0

				For Each pair As KeyValuePair(Of Object, Object) In locator
					Dim key As DependencyResolutionLocatorKey = TryCast(pair.Key, DependencyResolutionLocatorKey)

					If Not key Is Nothing AndAlso Not key.ID Is Nothing AndAlso TypeOf pair.Value Is TItem _
					 AndAlso PassesFilter(pair.Value) Then

						result += 1
					End If
				Next pair

				If aSearchMode = SearchMode.Up AndAlso Not parentCollection Is Nothing Then
					result += parentCollection.Count
				End If

				Return result
			End Get
		End Property

		''' <summary>
		''' Adds an item to the collection, without an ID.
		''' </summary>
		''' <param name="item">The item to be added.</param>
		Public Sub Add(ByVal item As TItem)
			Add(item, Nothing)
		End Sub

		''' <summary>
		''' Adds an item to the collection, with an ID.
		''' </summary>
		''' <param name="item">The item to be added.</param>
		''' <param name="id">The ID of the item.</param>
		Public Sub Add(ByVal item As TItem, ByVal id As String)
			Guard.ArgumentNotNull(item, "item")

			Build(item.GetType(), id, item)
		End Sub

		''' <summary>
		''' Creates a new item and adds it to the collection, without an ID.
		''' </summary>
		''' <param name="typeToBuild">The type of item to be built.</param>
		''' <returns>The newly created item.</returns>
		Public Function AddNew(ByVal typeToBuild As Type) As TItem
			Return AddNew(typeToBuild, Nothing)
		End Function

		''' <summary>
		''' Creates a new item and adds it to the collection, with an ID.
		''' </summary>
		''' <param name="typeToBuild">The type of item to be built.</param>
		''' <param name="id">The ID of the item.</param>
		''' <returns>The newly created item.</returns>
		Public Function AddNew(ByVal typeToBuild As Type, ByVal id As String) As TItem
			Return Build(typeToBuild, id, Nothing)
		End Function

		''' <summary>
		''' Creates a new item and adds it to the collection, without an ID.
		''' </summary>
		''' <typeparam name="TTypeToBuild">The type of item to be built.</typeparam>
		''' <returns>The newly created item.</returns>
		Public Function AddNew(Of TTypeToBuild As TItem)() As TTypeToBuild
			Return CType(AddNew(GetType(TTypeToBuild), Nothing), TTypeToBuild)
		End Function

		''' <summary>
		''' Creates a new item and adds it to the collection, with an ID.
		''' </summary>
		''' <typeparam name="TTypeToBuild">The type of item to be built.</typeparam>
		''' <param name="id">The ID of the item.</param>
		''' <returns>The newly created item.</returns>
		Public Function AddNew(Of TTypeToBuild As TItem)(ByVal id As String) As TTypeToBuild
			Return CType(AddNew(GetType(TTypeToBuild), id), TTypeToBuild)
		End Function

		''' <summary>
		''' Determines if the collection contains an object with the given ID.
		''' </summary>
		''' <param name="id">The ID of the object.</param>
		''' <returns>Returns true if the collection contains the object; false otherwise.</returns>
		Public Function Contains(ByVal id As String) As Boolean
			Return Not [Get](id) Is Nothing
		End Function

		Private Function Contains(ByVal id As String, ByVal aSearchMode As SearchMode, ByVal filtered As Boolean) As Boolean
			Return Not [Get](id, aSearchMode, filtered) Is Nothing
		End Function

		''' <summary>
		''' Determines if the collection contains an object.
		''' </summary>
		''' <param name="item">The object.</param>
		''' <returns>Returns true if the collection contains the object; false otherwise.</returns>
		Public Function ContainsObject(ByVal item As TItem) As Boolean
			Return container.Contains(item)
		End Function

		''' <summary>
		''' Finds all objects that are type-compatible with the given type.
		''' </summary>
		''' <typeparam name="TSearchType">The type of item to find.</typeparam>
		''' <returns>A collection of the found items.</returns>
		Public Function FindByType(Of TSearchType As TItem)() As ICollection(Of TSearchType)
			Dim result As List(Of TSearchType) = New List(Of TSearchType)()

			For Each obj As Object In container
				If GetType(TSearchType).IsAssignableFrom(obj.GetType()) Then
					result.Add(CType(obj, TSearchType))
				End If
			Next obj

			If aSearchMode = SearchMode.Up AndAlso Not parentCollection Is Nothing Then
				result.AddRange(parentCollection.FindByType(Of TSearchType)())
			End If

			Return result
		End Function

		''' <summary>
		''' Finds all objects that are type-compatible with the given type.
		''' </summary>
		''' <param name="searchType">The type of item to find.</param>
		''' <returns>A collection of the found items.</returns>
		Public Function FindByType(ByVal searchType As Type) As ICollection(Of TItem)
			Dim result As List(Of TItem) = New List(Of TItem)()

			For Each obj As Object In container
				If searchType.IsAssignableFrom(obj.GetType()) Then
					result.Add(CType(obj, TItem))
				End If
			Next obj

			If aSearchMode = SearchMode.Up AndAlso Not parentCollection Is Nothing Then
				result.AddRange(parentCollection.FindByType(searchType))
			End If

			Return result
		End Function

		''' <summary>
		''' Gets the object with the given ID.
		''' </summary>
		''' <param name="id">The ID to get.</param>
		''' <returns>The object, if present; null otherwise.</returns>
		Public Function [Get](ByVal id As String) As TItem
			Return [Get](id, aSearchMode, True)
		End Function

		Private Function [Get](ByVal id As String, ByVal aSearchMode As SearchMode, ByVal filtered As Boolean) As TItem
			Guard.ArgumentNotNull(id, "id")

			For Each pair As KeyValuePair(Of Object, Object) In locator
				If TypeOf pair.Key Is DependencyResolutionLocatorKey Then
					Dim depKey As DependencyResolutionLocatorKey = CType(pair.Key, DependencyResolutionLocatorKey)

					If Object.Equals(depKey.ID, id) Then
						Dim result As TItem = CType(pair.Value, TItem)
						If (Not filtered) OrElse filter Is Nothing OrElse filter(result) Then
							Return result
						End If
					End If
				End If
			Next pair

			If aSearchMode = SearchMode.Up AndAlso Not parentCollection Is Nothing Then
				Return parentCollection.Get(id)
			End If

			Return Nothing
		End Function

		''' <summary>
		''' Gets the object with the given ID.
		''' </summary>
		''' <typeparam name="TTypeToGet">The type of the object to get.</typeparam>
		''' <param name="id">The ID to get.</param>
		''' <returns>The object, if present; null otherwise.</returns>
		Public Function [Get](Of TTypeToGet As TItem)(ByVal id As String) As TTypeToGet
			Return CType([Get](id), TTypeToGet)
		End Function

		''' <summary>
		''' Removes an object from the container.
		''' </summary>
		''' <param name="item">The item to be removed.</param>
		Public Sub Remove(ByVal item As TItem)
			If container.Contains(item) Then
				ThrowIfItemRemovalIsNotPermitted(item)

				builder.TearDown(locator, item)
				container.Remove(item)

				Dim keysToRemove As List(Of Object) = New List(Of Object)()

				For Each pair As KeyValuePair(Of Object, Object) In locator
					If pair.Value.Equals(item) Then
						keysToRemove.Add(pair.Key)
					End If
				Next pair

				For Each key As Object In keysToRemove
					locator.Remove(key)
				Next key
			End If
		End Sub

		Private Shared Sub ThrowIfItemRemovalIsNotPermitted(ByVal item As TItem)

			If (TypeOf item Is WorkItem) Then
				Throw New ArgumentException(My.Resources.NoRemoveWorkItemFromManagedObjectCollection, "item")
			End If

			If (TypeOf item Is Command) Then

				Dim cmd As Command = TryCast(item, Command)
				Throw New ArgumentException(String.Format(CultureInfo.CurrentCulture, _
				 My.Resources.RemoveCommandFromWorkItemIsNotPermitted, cmd.Name), "item")
			End If

			If (TypeOf item Is EventTopic) Then
				Dim topic As EventTopic = TryCast(item, EventTopic)
				Throw New ArgumentException(String.Format(CultureInfo.CurrentCulture, _
				 My.Resources.RemoveEventTopicFromWorkItemIsNotPermitted, topic.Name), "item")
			End If
		End Sub

		Private Function Build(ByVal typeToBuild As Type, ByVal idToBuild As String, ByVal item As Object) As TItem
			If Not idToBuild Is Nothing AndAlso Contains(idToBuild, SearchMode.Local, True) Then
				Throw New ArgumentException(String.Format(CultureInfo.CurrentCulture, My.Resources.DuplicateID, idToBuild))
			End If

			If Not item Is Nothing _
			 AndAlso Object.ReferenceEquals(item, _
			  locator.Get(New DependencyResolutionLocatorKey(GetType(WorkItem), Nothing))) Then

				Throw New ArgumentException(My.Resources.CannotAddWorkItemToItself, "item")
			End If

			If item Is Nothing Then
				item = BuildFirstTimeItem(typeToBuild, idToBuild, Nothing)
			ElseIf (Not container.Contains(item)) Then
				item = BuildFirstTimeItem(typeToBuild, idToBuild, item)
			Else
				BuildRepeatedItem(typeToBuild, idToBuild, item)
			End If

			Return CType(item, TItem)
		End Function

		''' <summary>
		''' Used to build a first time item (either an existing one or a new one). The Builder will
		''' end up locating it, and we add it to the container.
		''' </summary>
		Private Function BuildFirstTimeItem(ByVal typeToBuild As Type, ByVal idToBuild As String, ByVal item As Object) As Object
			Return builder.BuildUp(locator, typeToBuild, NormalizeID(idToBuild), item)
		End Function

		''' <summary>
		''' Used to "build" an item we've already seen once. We don't use the builder, because that
		''' would do double-injection. Since it's already in the lifetime container, all we need to
		''' do is add a second locator registration for it for the right name.
		''' </summary>
		Private Sub BuildRepeatedItem(ByVal typeToBuild As Type, ByVal idToBuild As String, ByVal item As Object)
			locator.Add(New DependencyResolutionLocatorKey(typeToBuild, NormalizeID(idToBuild)), item)
		End Sub

		''' <summary>
		''' Gets an enumerator which returns all the objects in the container, along with their
		''' names.
		''' </summary>
		''' <returns>The enumerator.</returns>
		Public Function GetEnumerator() As IEnumerator(Of KeyValuePair(Of String, TItem)) Implements IEnumerable(Of KeyValuePair(Of String, TItem)).GetEnumerator
			Dim currentLocator As IReadableLocator = locator
			Dim seenNames As Dictionary(Of String, Object) = New Dictionary(Of String, Object)()
			Dim baseList As List(Of KeyValuePair(Of String, TItem)) = New List(Of KeyValuePair(Of String, TItem))

			Do
				For Each pair As KeyValuePair(Of Object, Object) In currentLocator
					If TypeOf pair.Key Is DependencyResolutionLocatorKey Then
						Dim depKey As DependencyResolutionLocatorKey = CType(pair.Key, DependencyResolutionLocatorKey)

						If Not depKey.ID Is Nothing AndAlso TypeOf pair.Value Is TItem Then
							Dim id As String = depKey.ID
							Dim value As TItem = CType(pair.Value, TItem)

							If (Not seenNames.ContainsKey(id)) AndAlso (filter Is Nothing OrElse filter(value)) Then
								seenNames(id) = String.Empty
								baseList.Add(New KeyValuePair(Of String, TItem)(id, value))
							End If
						End If
					End If
				Next pair
				currentLocator = currentLocator.ParentLocator
			Loop While aSearchMode = SearchMode.Up AndAlso Not currentLocator Is Nothing
			Return baseList.GetEnumerator()
		End Function

		Private Function GetEnumeratorBase() As IEnumerator Implements IEnumerable.GetEnumerator
			Return GetEnumerator()
		End Function

		Private Function PassesFilter(ByVal item As Object) As Boolean
			If Not filter Is Nothing Then
				Return filter(CType(item, TItem))
			Else
				Return True
			End If
		End Function

		Private Function NormalizeID(ByVal idToBuild As String) As String
			If Not idToBuild Is Nothing Then
				Return idToBuild
			Else
				Return Guid.NewGuid().ToString()
			End If
		End Function

		Private Sub WorkItem_ItemAdded(ByVal sender As Object, ByVal e As DataEventArgs(Of Object))
			If (Not AddedEvent Is Nothing) AndAlso TypeOf e.Data Is TItem Then
				Dim value As TItem = CType(e.Data, TItem)
				Dim key As DependencyResolutionLocatorKey = FindObjectInLocator(value)

				If Not key Is Nothing AndAlso Not key.ID Is Nothing AndAlso PassesFilter(value) Then
					RaiseEvent Added(Me, New DataEventArgs(Of TItem)(value))
				End If
			End If
		End Sub

		Private Sub WorkItem_ItemRemoved(ByVal sender As Object, ByVal e As DataEventArgs(Of Object))
			If (Not RemovedEvent Is Nothing) AndAlso TypeOf e.Data Is TItem Then
				Dim value As TItem = CType(e.Data, TItem)
				Dim key As DependencyResolutionLocatorKey = FindObjectInLocator(value)

				If Not key Is Nothing AndAlso Not key.ID Is Nothing AndAlso PassesFilter(value) Then
					RaiseEvent Removed(Me, New DataEventArgs(Of TItem)(value))
				End If
			End If
		End Sub

		Private Function FindObjectInLocator(ByVal value As Object) As DependencyResolutionLocatorKey
			For Each pair As KeyValuePair(Of Object, Object) In locator
				If TypeOf pair.Key Is DependencyResolutionLocatorKey AndAlso pair.Value Is value Then
					Return (CType(pair.Key, DependencyResolutionLocatorKey))
				End If
			Next pair

			Return Nothing
		End Function

		Private Sub CopyTo(ByVal array As Array, ByVal index As Integer) Implements ICollection.CopyTo
			Throw New NotImplementedException()
		End Sub

		Private ReadOnly Property CountBase() As Integer Implements ICollection.Count
			Get
				Return Count
			End Get
		End Property

		Private ReadOnly Property IsSynchronized() As Boolean Implements ICollection.IsSynchronized
			Get
				Return False
			End Get
		End Property

		Private ReadOnly Property SyncRoot() As Object Implements ICollection.SyncRoot
			Get
				Throw New NotImplementedException()
			End Get
		End Property
	End Class
End Namespace
