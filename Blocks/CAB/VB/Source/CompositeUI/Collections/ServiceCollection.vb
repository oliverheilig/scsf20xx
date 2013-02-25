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
Imports System.Collections.Generic
Imports Microsoft.Practices.CompositeUI.Services
Imports Microsoft.Practices.CompositeUI.Utility
Imports Microsoft.Practices.ObjectBuilder
Imports System.Collections
Imports System.Globalization

Namespace Collections
	''' <summary>
	''' Collection of services that are contained in a <see cref="WorkItem"/>.
	''' </summary>
	Public Class ServiceCollection
		Implements ICollection, IEnumerable(Of KeyValuePair(Of Type, Object))

		Private builder As IBuilder(Of BuilderStage)
		Private locator As IReadWriteLocator
		Private container As ILifetimeContainer
		Private parent As ServiceCollection

		''' <summary>
		''' Initializes an instance of the <see cref="ServiceCollection"/> class.
		''' </summary>
		''' <param name="container">The lifetime container the collection will use</param>
		''' <param name="locator">The locator the collection will use</param>
		''' <param name="builder">The builder the collection will use</param>
		''' <param name="parent">The parent collection</param>
		Public Sub New(ByVal container As ILifetimeContainer, ByVal locator As IReadWriteLocator, ByVal builder As IBuilder(Of BuilderStage), ByVal parent As ServiceCollection)
			Me.builder = builder
			Me.container = container
			Me.locator = locator
			Me.parent = parent
		End Sub

		''' <summary>
		''' Fired whenever a new service is added to the container. For demand-add services, the event is
		''' fired when the service is eventually created.
		''' </summary>
		Public Event Added As EventHandler(Of DataEventArgs(Of Object))

		''' <summary>
		''' Fired whenever a service is removed from the container. When the services collection is disposed,
		''' the Removed event is not fired for the services in the container.
		''' </summary>
		Public Event Removed As EventHandler(Of DataEventArgs(Of Object))

		''' <summary>
		''' Adds a service.
		''' </summary>
		''' <typeparam name="TService">The type under which to register the service.</typeparam>
		''' <param name="serviceInstance">The service to register.</param>
		''' <exception cref="ArgumentNullException">serviceInstance is null.</exception>
		''' <exception cref="ArgumentException">A service of the given type is already registered.</exception>
		Public Sub Add(Of TService)(ByVal serviceInstance As TService)
			Add(GetType(TService), serviceInstance)
		End Sub

		''' <summary>
		''' Adds a service.
		''' </summary>
		''' <param name="serviceType">The type under which to register the service.</param>
		''' <param name="serviceInstance">The service to register.</param>
		''' <exception cref="ArgumentNullException">serviceInstance is null.</exception>
		''' <exception cref="ArgumentException">A service of the given type is already registered.</exception>
		Public Sub Add(ByVal serviceType As Type, ByVal serviceInstance As Object)
			Guard.ArgumentNotNull(serviceType, "serviceType")
			Guard.ArgumentNotNull(serviceInstance, "serviceInstance")

			Build(serviceInstance.GetType(), serviceType, serviceInstance)
		End Sub

		''' <summary>
		''' Adds a service that will not be created until the first time it is requested.
		''' </summary>
		''' <typeparam name="TService">The type of service</typeparam>
		Public Sub AddOnDemand(Of TService)()
			AddOnDemand(GetType(TService), Nothing)
		End Sub

		''' <summary>
		''' Adds a service that will not be created until the first time it is requested.
		''' </summary>
		''' <typeparam name="TService">The type of service</typeparam>
		''' <typeparam name="TRegisterAs">The type to register the service as</typeparam>
		Public Sub AddOnDemand(Of TService, TRegisterAs)()
			AddOnDemand(GetType(TService), GetType(TRegisterAs))
		End Sub

		''' <summary>
		''' Adds a service that will not be created until the first time it is requested.
		''' </summary>
		''' <param name="serviceType">The type of service</param>
		Public Sub AddOnDemand(ByVal serviceType As Type)
			AddOnDemand(serviceType, Nothing)
		End Sub

		''' <summary>
		''' Adds a service that will not be created until the first time it is requested.
		''' </summary>
		''' <param name="serviceType">The type of service</param>
		''' <param name="registerAs">The type to register the service as</param>
		Public Sub AddOnDemand(ByVal serviceType As Type, ByVal registerAs As Type)
			Guard.ArgumentNotNull(serviceType, "serviceType")

			If registerAs Is Nothing Then
				registerAs = serviceType
			End If

			Dim key As DependencyResolutionLocatorKey = New DependencyResolutionLocatorKey(registerAs, Nothing)
			If locator.Contains(key, SearchMode.Local) Then
				Throw New ArgumentException(String.Format(CultureInfo.CurrentCulture, My.Resources.DuplicateService, registerAs.FullName))
			End If

			Dim placeholder As DemandAddPlaceholder = New DemandAddPlaceholder(serviceType)
			locator.Add(key, placeholder)
			container.Add(placeholder)
		End Sub

		''' <summary>
		''' Creates and adds a service.
		''' </summary>
		''' <typeparam name="TService">The type of the service to create. This type is also
		''' the type the service is registered under.</typeparam>
		''' <returns>The new service instance.</returns>
		''' <exception cref="ArgumentException">Object builder cannot find an appropriate
		''' constructor on the object.</exception>
		Public Function AddNew(Of TService)() As TService
			Return AddNew(Of TService, TService)()
		End Function

		''' <summary>
		''' Creates a service.
		''' </summary>
		''' <typeparam name="TService">The type of the service to create.</typeparam>
		''' <typeparam name="TRegisterAs">The type the service is registered under.</typeparam>
		''' <returns>The new service instance.</returns>
		''' <exception cref="ArgumentException">Object builder cannot find an appropriate
		''' constructor on the object.</exception>
		Public Function AddNew(Of TService As TRegisterAs, TRegisterAs)() As TService
			Return CType(AddNew(GetType(TService), GetType(TRegisterAs)), TService)
		End Function

		''' <summary>
		''' Creates a service.
		''' </summary>
		''' <param name="serviceType">The type of the service to create. This type is also
		''' the type the service is registered under.</param>
		''' <returns>The new service instance.</returns>
		''' <exception cref="ArgumentException">Object builder cannot find an appropriate
		''' constructor on the object.</exception>
		Public Function AddNew(ByVal serviceType As Type) As Object
			Return AddNew(serviceType, serviceType)
		End Function

		''' <summary>
		''' Creates a service.
		''' </summary>
		''' <param name="serviceType">The type of the service to create.</param>
		''' <param name="registerAs">The type the service is registered under.</param>
		''' <returns>The new service instance.</returns>
		''' <exception cref="ArgumentException">Object builder cannot find an appropriate
		''' constructor on the object.</exception>
		Public Function AddNew(ByVal serviceType As Type, ByVal registerAs As Type) As Object
			Guard.ArgumentNotNull(serviceType, "serviceType")
			Guard.ArgumentNotNull(registerAs, "registerAs")

			Return Build(serviceType, registerAs, Nothing)
		End Function

		''' <summary>
		''' Determines whether the given service type exists in the collection.
		''' </summary>
		''' <typeparam name="TService">Type of service to search for.</typeparam>
		''' <returns>
		''' true if the TService exists; 
		''' false otherwise.
		''' </returns>
		Public Function Contains(Of TService)() As Boolean
			Return Contains(GetType(TService))
		End Function

		''' <summary>
		''' Determines whether the given service type exists in the collection.
		''' </summary>
		''' <param name="serviceType">Type of service to search for.</param>
		''' <returns>
		''' true if the serviceType exists; 
		''' false otherwise.
		''' </returns>
		Public Function Contains(ByVal serviceType As Type) As Boolean
			Return Contains(serviceType, SearchMode.Up)
		End Function

		Private Function Contains(ByVal serviceType As Type, ByVal searchMode As SearchMode) As Boolean
			Guard.ArgumentNotNull(serviceType, "serviceType")

			Return locator.Contains(New DependencyResolutionLocatorKey(serviceType, Nothing), searchMode)
		End Function

		''' <summary>
		''' Determines whether the given service type exists directly in the local collection. The parent
		''' collections are not consulted.
		''' </summary>
		''' <param name="serviceType">Type of service to search for.</param>
		''' <returns>
		''' true if the serviceType exists; 
		''' false otherwise.
		''' </returns>
		Public Function ContainsLocal(ByVal serviceType As Type) As Boolean
			Return Contains(serviceType, SearchMode.Local)
		End Function

		''' <summary>
		''' Gets a service.
		''' </summary>
		''' <typeparam name="TService">The type of the service to be found.</typeparam>
		''' <returns>The service instance, if present; null otherwise.</returns>
		Public Function [Get](Of TService)() As TService
			Return CType([Get](GetType(TService), False), TService)
		End Function

		''' <summary>
		''' Gets a service.
		''' </summary>
		''' <typeparam name="TService">The type of the service to be found.</typeparam>
		''' <param name="ensureExists">If true, will throw an exception if the service is not found.</param>
		''' <returns>The service instance, if present; null if not (and ensureExists is false).</returns>
		''' <exception cref="ServiceMissingException">Thrown if ensureExists is true and the service is not found.</exception>
		Public Function [Get](Of TService)(ByVal ensureExists As Boolean) As TService
			Return CType([Get](GetType(TService), ensureExists), TService)
		End Function

		''' <summary>
		''' Gets a service.
		''' </summary>
		''' <param name="serviceType">The type of the service to be found.</param>
		''' <returns>The service instance, if present; null otherwise.</returns>
		Public Function [Get](ByVal serviceType As Type) As Object
			Return [Get](serviceType, False)
		End Function

		''' <summary>
		''' Gets a service.
		''' </summary>
		''' <param name="serviceType">The type of the service to be found.</param>
		''' <param name="ensureExists">If true, will throw an exception if the service is not found.</param>
		''' <returns>The service instance, if present; null if not (and ensureExists is false).</returns>
		''' <exception cref="ServiceMissingException">Thrown if ensureExists is true and the service is not found.</exception>
		Public Function [Get](ByVal serviceType As Type, ByVal ensureExists As Boolean) As Object
			Guard.ArgumentNotNull(serviceType, "serviceType")

			If Contains(serviceType, SearchMode.Local) Then
				Dim result As Object = locator.Get(New DependencyResolutionLocatorKey(serviceType, Nothing))

				Dim placeholder As DemandAddPlaceholder = TryCast(result, DemandAddPlaceholder)

				If Not placeholder Is Nothing Then
					Remove(serviceType)
					result = Build(placeholder.TypeToCreate, serviceType, Nothing)
				End If

				Return result
			End If

			If Not parent Is Nothing Then
				Return parent.Get(serviceType, ensureExists)
			End If

			If ensureExists Then
				Throw New ServiceMissingException(serviceType)
			End If

			Return Nothing
		End Function

		''' <summary>
		''' Removes a service registration from the <see cref="WorkItem"/>.
		''' </summary>
		''' <typeparam name="TService">The service type to remove.</typeparam>
		Public Sub Remove(Of TService)()
			Remove(GetType(TService))
		End Sub

		''' <summary>
		''' Removes a service registration from the <see cref="WorkItem"/>.
		''' </summary>
		''' <param name="serviceType">The service type to remove.</param>
		Public Sub Remove(ByVal serviceType As Type)
			Guard.ArgumentNotNull(serviceType, "serviceType")
			Dim key As DependencyResolutionLocatorKey = New DependencyResolutionLocatorKey(serviceType, Nothing)

			If locator.Contains(key, SearchMode.Local) Then
				Dim serviceInstance As Object = locator.Get(key, SearchMode.Local)
				Dim isLastInstance As Boolean = True

				locator.Remove(New DependencyResolutionLocatorKey(serviceType, Nothing))

				For Each kvp As KeyValuePair(Of Object, Object) In locator
					If ReferenceEquals(kvp.Value, serviceInstance) Then
						isLastInstance = False
						Exit For
					End If
				Next kvp

				If isLastInstance Then
					builder.TearDown(locator, serviceInstance)
					container.Remove(serviceInstance)

					If Not (TypeOf serviceInstance Is DemandAddPlaceholder) Then
						OnRemoved(serviceInstance)
					End If
				End If
			End If
		End Sub

		''' <summary>
		''' Called when a new service is added to the collection.
		''' </summary>
		''' <param name="service">The service that was added.</param>
		Protected Overridable Sub OnAdded(ByVal service As Object)
			If Not AddedEvent Is Nothing Then
				RaiseEvent Added(Me, New DataEventArgs(Of Object)(service))
			End If
		End Sub

		''' <summary>
		''' Called when a service is removed from the collection.
		''' </summary>
		''' <param name="service">The service that was removed.</param>
		Protected Overridable Sub OnRemoved(ByVal service As Object)
			If Not RemovedEvent Is Nothing Then
				RaiseEvent Removed(Me, New DataEventArgs(Of Object)(service))
			End If
		End Sub

		Private Function Build(ByVal typeToBuild As Type, ByVal typeToRegisterAs As Type, ByVal serviceInstance As Object) As Object
			Guard.TypeIsAssignableFromType(typeToBuild, typeToRegisterAs, "typeToBuild")

			If locator.Contains(New DependencyResolutionLocatorKey(typeToRegisterAs, Nothing), SearchMode.Local) Then
				Throw New ArgumentException(String.Format(CultureInfo.CurrentCulture, My.Resources.DuplicateService, typeToRegisterAs.FullName))
			End If

			If serviceInstance Is Nothing Then
				serviceInstance = BuildFirstTimeItem(typeToBuild, typeToRegisterAs, Nothing)
			ElseIf (Not container.Contains(serviceInstance)) Then
				serviceInstance = BuildFirstTimeItem(typeToBuild, typeToRegisterAs, serviceInstance)
			Else
				BuildRepeatedItem(typeToRegisterAs, serviceInstance)
			End If

			Return serviceInstance
		End Function

		''' <summary>
		''' Used to build a first time item (either an existing one or a new one). The Builder will
		''' end up locating it, and we add it to the container.
		''' </summary>
		Private Function BuildFirstTimeItem(ByVal typeToBuild As Type, ByVal typeToRegisterAs As Type, ByVal item As Object) As Object
			item = builder.BuildUp(locator, typeToBuild, Nothing, item)

			If Not typeToRegisterAs Is typeToBuild Then
				locator.Add(New DependencyResolutionLocatorKey(typeToRegisterAs, Nothing), item)
				locator.Remove(New DependencyResolutionLocatorKey(typeToBuild, Nothing))
			End If

			OnAdded(item)
			Return item
		End Function

		''' <summary>
		''' Used to "build" an item we've already seen once. We don't use the builder, because that
		''' would do double-injection. Since it's already in the lifetime container, all we need to
		''' do is add a second locator registration for it for the right name.
		''' </summary>
		Private Sub BuildRepeatedItem(ByVal typeToRegisterAs As Type, ByVal item As Object)
			locator.Add(New DependencyResolutionLocatorKey(typeToRegisterAs, Nothing), item)
		End Sub

		Private Class DemandAddPlaceholder
			Private innerTypeToCreate As Type

			Public Sub New(ByVal aTypeToCreate As Type)
				Me.innerTypeToCreate = aTypeToCreate
			End Sub

			Public ReadOnly Property TypeToCreate() As Type
				Get
					Return innerTypeToCreate
				End Get
			End Property
		End Class

		''' <summary>
		''' Enumerates through all seen types and retrieves a KeyValuePair for each. 
		''' </summary>
		''' <returns></returns>
		Public Function GetEnumerator() As IEnumerator(Of KeyValuePair(Of Type, Object)) Implements IEnumerable(Of KeyValuePair(Of Type, Object)).GetEnumerator
			Dim currentLocator As IReadableLocator = locator
			Dim seenTypes As Dictionary(Of Type, Object) = New Dictionary(Of Type, Object)()
			Dim baseList As List(Of KeyValuePair(Of Type, Object)) = New List(Of KeyValuePair(Of Type, Object))

			Do
				For Each pair As KeyValuePair(Of Object, Object) In currentLocator
					If TypeOf pair.Key Is DependencyResolutionLocatorKey Then
						Dim depKey As DependencyResolutionLocatorKey = CType(pair.Key, DependencyResolutionLocatorKey)

						If depKey.ID Is Nothing Then
							Dim type As Type = depKey.Type

							If (Not seenTypes.ContainsKey(type)) Then
								seenTypes(type) = String.Empty
								baseList.Add(New KeyValuePair(Of Type, Object)(type, pair.Value))
							End If
						End If
					End If
				Next pair
				currentLocator = currentLocator.ParentLocator
			Loop While Not currentLocator Is Nothing
			Return baseList.GetEnumerator()
		End Function

		Private Function GetEnumeratorBase() As IEnumerator Implements IEnumerable.GetEnumerator
			Return GetEnumerator()
		End Function

		Private Sub CopyTo(ByVal array As Array, ByVal index As Integer) Implements ICollection.CopyTo
			Throw New NotImplementedException()
		End Sub

		Private ReadOnly Property Count() As Integer Implements ICollection.Count
			Get
				Throw New NotImplementedException()
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
