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
Imports System.Diagnostics
Imports System.IO
Imports System.Reflection
Imports Microsoft.Practices.CompositeUI.BuilderStrategies
Imports Microsoft.Practices.CompositeUI.Collections
Imports Microsoft.Practices.CompositeUI.Commands
Imports Microsoft.Practices.CompositeUI.EventBroker
Imports Microsoft.Practices.CompositeUI.Services
Imports Microsoft.Practices.CompositeUI.SmartParts
Imports Microsoft.Practices.CompositeUI.UIElements
Imports Microsoft.Practices.CompositeUI.Utility
Imports Microsoft.Practices.ObjectBuilder
Imports System.ComponentModel
Imports System.Globalization
Imports System.Threading

''' <summary>
''' Defines the work item into which smart parts run.
''' </summary>
Public Class WorkItem
	Implements IBuilderAware, IDisposable

#Region "Fields"

	Private builder As Builder
	Private buildUpFinished As Boolean = False
	Private lifetime As ILifetimeContainer = New LifetimeContainer()
	Private locator As IReadWriteLocator
	Private parentItem As WorkItem
	Private smartPartInfos As ListDictionary(Of Object, ISmartPartInfo) = New ListDictionary(Of Object, ISmartPartInfo)()
	Private itemState As State
	Private itemStatus As WorkItemStatus

	Private commandCollection As ManagedObjectCollection(Of Command)
	Private smartPartCollection As ManagedObjectCollection(Of Object)
	Private workItemCollection As ManagedObjectCollection(Of WorkItem)
	Private workspaceCollection As ManagedObjectCollection(Of IWorkspace)
	Private itemsCollection As ManagedObjectCollection(Of Object)
	Private eventTopicCollection As ManagedObjectCollection(Of EventTopic)
	Private itemServiceCollection As ServiceCollection
	Private itemUIExtensionSiteCollection As UIExtensionSiteCollection

	Private itemTraceSource As TraceSource = Nothing

#End Region

#Region "WorkItem Lifetime (ctor, Dispose, Initialize, etc.)"

	''' <summary>
	''' Initializes a new instance of the <see cref="WorkItem"/> class.
	''' </summary>
	Public Sub New()

	End Sub

	''' <summary>
	''' Disposes the <see cref="WorkItem"/>.
	''' </summary>
	Public Sub Dispose() Implements IDisposable.Dispose
		Dispose(True)
		GC.SuppressFinalize(Me)
	End Sub

	''' <summary>
	''' Runs the work item by calling the <see cref="OnRunStarted"/> method, 
	''' which will raise the <see cref="RunStarted"/> event and eventually 
	''' the custom starting business logic a derived class placed on it.
	''' </summary>
	Public Sub Run()
		OnRunStarted()
	End Sub

	''' <summary>
	''' Initializes a root <see cref="WorkItem"/>. Intended to be called by the user who creates the
	''' first WorkItem using new. Subsequent child WorkItem classes are automatically initialized
	''' through dependency injection from their parent.
	''' </summary>
	''' <param name="builder">The <see cref="Builder"/> used to build objects.</param>
	Protected Friend Sub InitializeRootWorkItem(ByVal builder As Builder)
		Me.builder = builder
		Me.locator = New Locator()

		InitializeFields()
		InitializeCollectionFacades()
		InitializeState()
	End Sub

	''' <summary>
	''' Initializes the <see cref="WorkItem"/> after construction.
	''' This method is typically called by the parent <see cref="WorkItem"/> 
	''' during the process of building a new child <see cref="WorkItem"/>.
	''' </summary>
	<InjectionMethod()> _
	Public Sub InitializeWorkItem()
		If buildUpFinished Then
			Throw New InvalidOperationException()
		End If

		InitializeFields()
		InitializeCollectionFacades()
		InitializeState()
		InitializeServices()
	End Sub

	Private Sub InitializeFields()
		If builder Is Nothing Then
			builder = parentItem.builder
		End If

		If locator Is Nothing Then
			locator = New Locator(parentItem.locator)
		End If

		If (Not locator.Contains(GetType(ILifetimeContainer), SearchMode.Local)) Then
			locator.Add(GetType(ILifetimeContainer), lifetime)
		End If

		Dim policy As ObjectBuiltNotificationPolicy = builder.Policies.Get(Of ObjectBuiltNotificationPolicy)(Nothing, Nothing)

		If Not policy Is Nothing Then
			policy.AddedDelegates(Me) = New ObjectBuiltNotificationPolicy.ItemNotification(AddressOf OnObjectAdded)
			policy.RemovedDelegates(Me) = New ObjectBuiltNotificationPolicy.ItemNotification(AddressOf OnObjectRemoved)
		End If

		LocateWorkItem(GetType(WorkItem))
		LocateWorkItem(Me.GetType())

		itemStatus = WorkItemStatus.Inactive
	End Sub

	Private Sub LocateWorkItem(ByVal workItemType As Type)
		Dim key As DependencyResolutionLocatorKey = New DependencyResolutionLocatorKey(workItemType, Nothing)

		If (Not locator.Contains(key, SearchMode.Local)) Then
			locator.Add(key, Me)
		End If
	End Sub

#Region "Utility code for the subprocedure InitializeCollectionFacades"

	Private Function GetCustomAttributes(ByVal obj As Object) As Boolean
		Return obj.GetType().GetCustomAttributes(GetType(SmartPartAttribute), True).Length > 0
	End Function

#End Region

	Private Sub InitializeCollectionFacades()
		If itemServiceCollection Is Nothing Then
			If parentItem Is Nothing Then
				itemServiceCollection = New ServiceCollection(lifetime, locator, builder, Nothing)
			Else
				itemServiceCollection = New ServiceCollection(lifetime, locator, builder, _
				 parentItem.itemServiceCollection)
			End If
		End If

		If commandCollection Is Nothing Then
			If parentItem Is Nothing Then
				commandCollection = New ManagedObjectCollection(Of Command)(lifetime, locator, builder, _
				 SearchMode.Up, AddressOf CreateCommand, Nothing, Nothing)
			Else
				commandCollection = New ManagedObjectCollection(Of Command)(lifetime, locator, builder, _
				 SearchMode.Up, AddressOf CreateCommand, Nothing, parentItem.commandCollection)
			End If
		End If

		If workItemCollection Is Nothing Then
			If parentItem Is Nothing Then
				workItemCollection = New ManagedObjectCollection(Of WorkItem)(lifetime, locator, builder, _
				 SearchMode.Local, Nothing, Nothing, Nothing)
			Else
				workItemCollection = New ManagedObjectCollection(Of WorkItem)(lifetime, locator, builder, _
				 SearchMode.Local, Nothing, Nothing, parentItem.workItemCollection)
			End If
		End If

		If workspaceCollection Is Nothing Then
			If parentItem Is Nothing Then
				workspaceCollection = New ManagedObjectCollection(Of IWorkspace)(lifetime, locator, builder, _
				 SearchMode.Up, Nothing, Nothing, Nothing)
			Else
				workspaceCollection = New ManagedObjectCollection(Of IWorkspace)(lifetime, locator, builder, _
				 SearchMode.Up, Nothing, Nothing, parentItem.workspaceCollection)
			End If
		End If

		If itemsCollection Is Nothing Then
			If parentItem Is Nothing Then
				itemsCollection = New ManagedObjectCollection(Of Object)(lifetime, locator, builder, _
				 SearchMode.Local, Nothing, Nothing, Nothing)
			Else
				itemsCollection = New ManagedObjectCollection(Of Object)(lifetime, locator, builder, _
				 SearchMode.Local, Nothing, Nothing, parentItem.itemsCollection)
			End If
		End If

		Dim parentItemSmartParts As ManagedObjectCollection(Of Object) = Nothing
		If Not parentItem Is Nothing Then
			parentItemSmartParts = parentItem.smartPartCollection
		End If

		If smartPartCollection Is Nothing Then
			smartPartCollection = New ManagedObjectCollection(Of Object)(lifetime, locator, builder, _
			 SearchMode.Local, Nothing, AddressOf GetCustomAttributes, _
			 parentItemSmartParts)
		End If

		If eventTopicCollection Is Nothing Then
			If (Parent Is Nothing) Then
				eventTopicCollection = New ManagedObjectCollection(Of EventTopic)(lifetime, locator, builder, _
				 SearchMode.Local, AddressOf CreateEventTopic, Nothing, Nothing)
			Else
				eventTopicCollection = RootWorkItem.eventTopicCollection
			End If
		End If

		If itemUIExtensionSiteCollection Is Nothing Then
			If parentItem Is Nothing Then
				itemUIExtensionSiteCollection = New UIExtensionSiteCollection(Me)
			Else
				itemUIExtensionSiteCollection = New UIExtensionSiteCollection(parentItem.itemUIExtensionSiteCollection)
			End If
		End If

	End Sub

	Private Sub InitializeState()
		ID = Guid.NewGuid().ToString()
	End Sub

	''' <summary>
	''' Called to create a new <see cref="Command"/> instance to add to the <see cref="WorkItem.Commands"/> collection.
	''' </summary>
	''' <param name="t">The type of command to create.</param>
	''' <param name="name">The name of the command.</param>
	''' <returns>A new <see cref="Command"/> instance.</returns>
	''' <remarks>Override this method to create another type of command.</remarks>
	Protected Overridable Function CreateCommand(ByVal t As Type, ByVal name As String) As Command
		Return New Command()
	End Function

	''' <summary>
	''' Called to create a new <see cref="EventTopic"/> instance to add to the <see cref="WorkItem.EventTopics"/> collection.
	''' </summary>
	''' <param name="t">The type of event topic to create.</param>
	''' <param name="topicName">The name of the event topic.</param>
	''' <returns>A new <see cref="EventTopic"/> instance.</returns>
	''' <remarks>Override this method to create another type of event topic.</remarks>
	Protected Overridable Function CreateEventTopic(ByVal t As Type, ByVal topicName As String) As EventTopic
		Return New EventTopic()
	End Function

	''' <summary>
	''' Initializes the built-in services exposed by the <see cref="WorkItem"/>.
	''' </summary>
	Protected Overridable Sub InitializeServices()
	End Sub

#End Region

#Region "Events"

	''' <summary>
	''' Occurs when the <see cref="WorkItem"/> is activated.
	''' </summary>
	Public Event Activated As EventHandler

	''' <summary>
	''' Occurs when the <see cref="WorkItem"/> is activating.
	''' </summary>
	Public Event Activating As CancelEventHandler

	''' <summary>
	''' Occurs when the <see cref="WorkItem"/> is deactivated.
	''' </summary>
	Public Event Deactivated As EventHandler

	''' <summary>
	''' Occurs when the <see cref="WorkItem"/> is deactivated.
	''' </summary>
	Public Event Deactivating As CancelEventHandler

	''' <summary>
	''' Occurs when the <see cref="WorkItem"/> is disposed.
	''' </summary>
	Public Event Disposed As EventHandler

	''' <summary>
	''' Occurs when the <see cref="ID"/> is changed.
	''' </summary>
	Public Event IdChanged As EventHandler(Of DataEventArgs(Of String))

	''' <summary>
	''' Occurs when the <see cref="WorkItem"/> is initialized.
	''' </summary>
	Public Event Initialized As EventHandler

	''' <summary>
	''' Occurs when the <see cref="Run"/> method is executed.
	''' </summary>
	Public Event RunStarted As EventHandler

	''' <summary>
	''' Occurs when the <see cref="WorkItem"/> is terminated.
	''' </summary>
	Public Event Terminated As EventHandler

	''' <summary>
	''' Occurs before a <see cref="WorkItem"/> is terminated.
	''' </summary>
	Public Event Terminating As EventHandler

	Friend Event ObjectAdded As EventHandler(Of DataEventArgs(Of Object))
	Friend Event ObjectRemoved As EventHandler(Of DataEventArgs(Of Object))

#End Region

#Region "Properties"

	''' <summary>
	''' List of commands registered with the <see cref="WorkItem"/>.
	''' </summary>
	Public ReadOnly Property Commands() As ManagedObjectCollection(Of Command)
		Get
			Return commandCollection
		End Get
	End Property

	''' <summary>
	''' Gets/sets the ID of this <see cref="WorkItem"/>. The ID is used for persistence of the
	''' WorkItem. By default, the ID will be a GUID. If you set a new ID, the old state data
	''' will be lost and replaced with new, empty state.
	''' </summary>
	Public Property ID() As String
		Get
			If itemState Is Nothing Then
				Return Nothing
			End If
			Return itemState.ID
		End Get
		Set(ByVal value As String)
			If Not itemState Is Nothing Then
				ReleaseState()
			End If

			AttachState(New State(value))
			OnIdChanged()
		End Set
	End Property

	''' <summary>
	''' Gets a list of all the objects and services contained in this <see cref="WorkItem"/>.
	''' </summary>
	Public ReadOnly Property Items() As ManagedObjectCollection(Of Object)
		Get
			Return itemsCollection
		End Get
	End Property

	''' <summary>
	''' Gets the parent <see cref="WorkItem"/>.
	''' </summary>
	<Browsable(False), Dependency(NotPresentBehavior:=NotPresentBehavior.ReturnNull)> _
	Public Property Parent() As WorkItem
		Get
			Return parentItem
		End Get
		Set(ByVal value As WorkItem)
			If buildUpFinished Then
				Throw New InvalidOperationException()
			End If
			parentItem = value
		End Set
	End Property

	''' <summary>
	''' Gets the root <see cref="WorkItem"/> (the one at the top of the hierarchy).
	''' </summary>
	<Browsable(False)> _
	Public ReadOnly Property RootWorkItem() As WorkItem
		Get
			Dim result As WorkItem = Me

			Do While Not result.Parent Is Nothing
				result = result.Parent
			Loop

			Return result
		End Get
	End Property

	''' <summary>
	''' Returns the collection of services associated with this WorkItem.
	''' </summary>
	Public ReadOnly Property Services() As ServiceCollection
		Get
			Return itemServiceCollection
		End Get
	End Property

	''' <summary>
	''' Returns a collection describing the child smart parts (objects
	''' with the <see cref="SmartPartAttribute"/> applied to them) in this WorkItem.
	''' </summary>
	Public ReadOnly Property SmartParts() As ManagedObjectCollection(Of Object)
		Get
			Return smartPartCollection
		End Get
	End Property

	''' <summary>
	''' Gets the <see cref="State"/> associated with this <see cref="WorkItem"/>.
	''' </summary>
	Public ReadOnly Property State() As State
		Get
			Return itemState
		End Get
	End Property

	''' <summary>
	''' Gets the current <see cref="WorkItemStatus"/> of the <see cref="WorkItem"/>.
	''' </summary>
	Public ReadOnly Property Status() As WorkItemStatus
		Get
			Return itemStatus
		End Get
	End Property

	''' <summary>
	''' Sets the <see cref="System.Diagnostics.TraceSource"/> used by the <see cref="WorkItem"/> to log messages. 
	''' </summary>
	<ClassNameTraceSourceAttribute()> _
	Public WriteOnly Property TraceSource() As TraceSource
		Set(ByVal value As TraceSource)
			itemTraceSource = value
		End Set
	End Property

	''' <summary>
	''' Returns a collection of <see cref="UIExtensionSite"/>s in the WorkItem.
	''' </summary>
	Public ReadOnly Property UIExtensionSites() As UIExtensionSiteCollection
		Get
			Return itemUIExtensionSiteCollection
		End Get
	End Property

	''' <summary>
	''' Returns a collection describing the child <see cref="WorkItem"/> objects in this WorkItem.
	''' </summary>
	Public ReadOnly Property WorkItems() As ManagedObjectCollection(Of WorkItem)
		Get
			Return workItemCollection
		End Get
	End Property

	''' <summary>
	''' Returns a collection describing the <see cref="IWorkspace"/> objects in this WorkItem.
	''' </summary>
	Public ReadOnly Property Workspaces() As ManagedObjectCollection(Of IWorkspace)
		Get
			Return workspaceCollection
		End Get
	End Property

	''' <summary>
	''' Returns a collection describing the <see cref="EventTopic"/> objects in this WorkItem.
	''' </summary>
	Public ReadOnly Property EventTopics() As ManagedObjectCollection(Of EventTopic)
		Get
			Return eventTopicCollection
		End Get
	End Property

#End Region

#Region "Work Item Status"

	''' <summary>
	''' Activates the <see cref="WorkItem"/>.
	''' </summary>
	Public Sub Activate()
		ChangeStatus(WorkItemStatus.Active)
	End Sub

	''' <summary>
	''' Deactivates the <see cref="WorkItem"/>.
	''' </summary>
	Public Sub Deactivate()
		ChangeStatus(WorkItemStatus.Inactive)
	End Sub

#End Region

#Region "State Management"

	''' <summary>
	''' Deletes the saved state of the <see cref="WorkItem"/>. The local copy of the state is not changed.
	''' </summary>
	''' <exception cref="ServiceMissingException">Thrown if the IStatePersistenceService is not
	''' registered in the WorkItem.</exception>
	Public Sub DeleteState()
		Dim service As IStatePersistenceService = Services.Get(Of IStatePersistenceService)()

		If service Is Nothing Then
			Throw New ServiceMissingException(GetType(IStatePersistenceService), Me)
		End If

		service.Remove(ID)
	End Sub

	''' <summary>
	''' Loads the work item.
	''' </summary>
	Public Sub Load()
		Dim service As IStatePersistenceService = Services.Get(Of IStatePersistenceService)()

		If service Is Nothing Then
			Throw New ServiceMissingException(GetType(IStatePersistenceService), Me)
		End If

		Using New WriterLock(State.syncRoot)
			Dim newState As State = service.Load(ID)
			newState.syncRoot = ReleaseState()
			AttachState(newState)
		End Using
	End Sub

#End Region

#Region "Other Methods"

	''' <summary>
	''' Returns smart part information for a control.
	''' </summary>
	''' <typeparam name="TSmartPartInfo">The type of the <see cref="ISmartPartInfo"/> instance to retrieve.</typeparam>
	''' <param name="smartPart">The smartPart which to retreive the smart part info for.</param>
	''' <returns>The <see cref="ISmartPartInfo"/> for the control and type requested, or null if no one is registered.</returns>
	Public Function GetSmartPartInfo(Of TSmartPartInfo As ISmartPartInfo)(ByVal smartPart As Object) As TSmartPartInfo
		Guard.ArgumentNotNull(smartPart, "smartPart")

		Return FindSmartPartInfo(Of TSmartPartInfo)(smartPart)
	End Function

	''' <summary>
	''' See <see cref="IBuilderAware.OnBuiltUp"/> for more information.
	''' </summary>
	Public Overridable Sub OnBuiltUp(ByVal idArg As String) Implements ObjectBuilder.IBuilderAware.OnBuiltUp
		' Prevent double build up notifications
		If buildUpFinished Then
			Throw New InvalidOperationException()
		End If

		buildUpFinished = True

		If Not parentItem Is Nothing Then
			FinishInitialization()
		End If
	End Sub

	''' <summary>
	''' Finishes the initialization of <see cref="WorkItem"/> classes by calling the
	''' <see cref="IWorkItemExtensionService"/> and <see cref="OnInitialized()"/>. For
	''' root WorkItems, this will be called by the <see cref="CabApplication{TWorkItem}"/> after
	''' the modules are loaded (so root WorkItem extensions work). For child WorkItems,
	''' this will be called during <see cref="OnBuiltUp"/> automatically.
	''' </summary>
	Protected Friend Sub FinishInitialization()
		Dim extensionsService As IWorkItemExtensionService = Services.Get(Of IWorkItemExtensionService)()
		If Not extensionsService Is Nothing Then
			extensionsService.InitializeExtensions(Me)
		End If

		OnInitialized()
	End Sub

	''' <summary>
	''' See <see cref="IBuilderAware.OnTearingDown"/> for more information.
	''' </summary>
	Public Overridable Sub OnTearingDown() Implements ObjectBuilder.IBuilderAware.OnTearingDown
		Dim policy As ObjectBuiltNotificationPolicy = builder.Policies.Get(Of ObjectBuiltNotificationPolicy)(Nothing, Nothing)

		If Not policy Is Nothing Then
			policy.AddedDelegates.Remove(Me)
			policy.RemovedDelegates.Remove(Me)
		End If
	End Sub

#Region "Utility code for the subprocedure RegisterSmartPartInfo"

	Private Class RegisterSmartPartInfoUtilityProvider
		Private infoField As ISmartPartInfo
		Public Property Info() As ISmartPartInfo
			Get
				Return infoField
			End Get
			Set(ByVal infoArgument As ISmartPartInfo)
				infoField = infoArgument
			End Set
		End Property

		Public Function CompareType(ByVal match As ISmartPartInfo) As Boolean
			Return CObj(match).GetType() Is CObj(Info).GetType()
		End Function
	End Class

	Private registerSmartPartInfoUtility As RegisterSmartPartInfoUtilityProvider = New RegisterSmartPartInfoUtilityProvider

#End Region

	''' <summary>
	''' Registers a <see cref="ISmartPartInfo"/> view data for a given control
	''' </summary>
	''' <param name="smartPart">The smartPart to which provide additional presentation information.</param>
	''' <param name="info">The additional presentation information for the control.</param>
	Public Sub RegisterSmartPartInfo(ByVal smartPart As Object, ByVal info As ISmartPartInfo)
		Guard.ArgumentNotNull(smartPart, "smartPart")
		Guard.ArgumentNotNull(info, "info")

		registerSmartPartInfoUtility.Info = info
		Dim findSPI As Predicate(Of ISmartPartInfo) = AddressOf registerSmartPartInfoUtility.CompareType

		If Not Me Is RootWorkItem Then
			RegisterSmartPartInfoInWorkItem(smartPart, info, findSPI, RootWorkItem)
		End If
		RegisterSmartPartInfoInWorkItem(smartPart, info, findSPI, Me)
	End Sub

	Private Shared Sub RegisterSmartPartInfoInWorkItem(ByVal smartPart As Object, ByVal info As ISmartPartInfo, ByVal findSPI As Predicate(Of ISmartPartInfo), ByVal aWorkItem As WorkItem)
		If aWorkItem.smartPartInfos.ContainsKey(smartPart) = False Then
			aWorkItem.smartPartInfos.Add(smartPart)
			aWorkItem.smartPartInfos(smartPart).Add(info)
		Else
			Dim registered As ISmartPartInfo = aWorkItem.smartPartInfos(smartPart).Find(findSPI)
			If Not registered Is Nothing Then
				aWorkItem.smartPartInfos(smartPart).Remove(registered)
			End If
			aWorkItem.smartPartInfos(smartPart).Add(info)
		End If
	End Sub

	''' <summary>
	''' Saves the state of this work item.
	''' </summary>
	Public Sub Save()
		Dim service As IStatePersistenceService = Services.Get(Of IStatePersistenceService)()

		If service Is Nothing Then
			Throw New ServiceMissingException(GetType(IStatePersistenceService), Me)
		End If

		Using New WriterLock(State.syncRoot)
			service.Save(State)
			State.AcceptChanges()
		End Using
	End Sub

	''' <summary>
	''' Terminates the work item.
	''' </summary>
	''' <remarks>
	''' Terminating the work item renders the work item invalid. Any calls to a work item
	''' after it has been terminated will result in undefined behavior.
	''' </remarks>
	Public Sub Terminate()
		ThrowIfWorkItemTerminated()
		Dispose()
	End Sub

#End Region

#Region "Protected Members"

	''' <summary>
	''' Used when the creator of a root <see cref="WorkItem"/> needs to ensure that the WorkItem
	''' has properly built itself (so its dependencies get injected properly).
	''' </summary>
	Protected Friend Sub BuildUp()
		' We use Guid.NewGuid() to generate a dummy ID, so that the WorkItem buildup sequence can
		' run (the WorkItem is already located with the null ID, which marks it as a service, so
		' the SingletonStrategy would short circuit and not do the build-up).

		Dim aType As Type = Me.GetType()
		Dim temporaryID As String = Guid.NewGuid().ToString()
		Dim propPolicy As PropertySetterPolicy = New PropertySetterPolicy()
		propPolicy.Properties.Add("Parent", New PropertySetterInfo("Parent", New ValueParameter(GetType(WorkItem), Nothing)))

		Dim policies As PolicyList = New PolicyList()
		policies.Set(Of ISingletonPolicy)(New SingletonPolicy(False), aType, temporaryID)
		policies.Set(Of IPropertySetterPolicy)(propPolicy, aType, temporaryID)

		builder.BuildUp(locator, aType, temporaryID, Me, policies)
	End Sub

	''' <summary>
	''' Provides protected access to the builder contained within the workitem.
	''' </summary>
	Protected ReadOnly Property InnerBuilder() As Builder
		Get
			Return builder
		End Get
	End Property

	''' <summary>
	''' Provides protected access to the locator contained within the workitem.
	''' </summary>
	Protected ReadOnly Property InnerLocator() As IReadWriteLocator
		Get
			Return locator
		End Get
	End Property

	''' <summary>
	''' Internal dispose method.
	''' </summary>
	''' <param name="disposing">Set to true if called from Dispose;
	''' set to false if called from the finalizer.</param>
	Protected Overridable Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If itemStatus = WorkItemStatus.Terminated Then
				Return
			End If

			ChangeStatus(WorkItemStatus.Terminated)
			builder.TearDown(locator, Me)
			Me.itemUIExtensionSiteCollection.Dispose()

			Dim policy As ObjectBuiltNotificationPolicy = builder.Policies.Get(Of ObjectBuiltNotificationPolicy)(Nothing, Nothing)

			If Not policy Is Nothing Then
				policy.AddedDelegates.Remove(Me)
				policy.RemovedDelegates.Remove(Me)
			End If

			If Not State Is Nothing Then
				ReleaseState()
			End If

			If Not parentItem Is Nothing Then
				Dim ids As List(Of Object) = New List(Of Object)()

				For Each pair As KeyValuePair(Of Object, Object) In parentItem.locator
					If pair.Value Is Me Then
						ids.Add(pair.Key)
					End If
				Next pair

				For Each id As Object In ids
					parentItem.locator.Remove(id)
				Next id

				parentItem.lifetime.Remove(Me)
				For Each smartpart As Object In smartPartInfos.Keys
					RootWorkItem.smartPartInfos.Remove(smartpart)
				Next smartpart
			End If

			lifetime.Remove(Me)

			Dim lifetimeObjects As List(Of Object) = New List(Of Object)()
			lifetimeObjects.AddRange(lifetime)

			For Each obj As Object In lifetimeObjects
				If lifetime.Contains(obj) Then
					builder.TearDown(locator, obj)
				End If
			Next obj

			lifetime.Dispose()
			OnDisposed()
		End If
	End Sub

	Private Sub AttachState(ByVal state As State)
		itemState = state
		AddHandler Me.State.StateChanged, New EventHandler(Of StateChangedEventArgs)(AddressOf stateDataStateChanged)
		Items.Add(Me.State)
	End Sub

	Private Function ReleaseState() As ReaderWriterLock
		Dim rwl As ReaderWriterLock = itemState.syncRoot
		RemoveHandler itemState.StateChanged, New EventHandler(Of StateChangedEventArgs)(AddressOf stateDataStateChanged)

		Items.Remove(itemState)
		itemState.Dispose()
		itemState = Nothing

		Return rwl
	End Function

	''' <summary>
	''' Fires the <see cref="Activated"/> event.
	''' </summary>
	Protected Overridable Sub OnActivated()
		If Not Me.ActivatedEvent Is Nothing Then
			RaiseEvent Activated(Me, EventArgs.Empty)
		End If

		If Not itemTraceSource Is Nothing Then
			itemTraceSource.TraceInformation(String.Format(CultureInfo.CurrentCulture, My.Resources.TraceWorkItemStatusChangedToActivated, ID))
		End If
	End Sub

	''' <summary>
	''' Fires the <see cref="Activating"/> event.
	''' </summary>
	Protected Overridable Sub OnActivating(ByVal args As CancelEventArgs)
		If Not itemTraceSource Is Nothing Then
			itemTraceSource.TraceInformation(String.Format(CultureInfo.CurrentCulture, My.Resources.TraceWorkItemStatusActivating, ID))
		End If

		If Not Me.ActivatingEvent Is Nothing Then
			RaiseEvent Activating(Me, args)
		End If
	End Sub

	''' <summary>
	''' Fires the <see cref="Deactivated"/> event.
	''' </summary>
	Protected Overridable Sub OnDeactivated()
		If Not Me.DeactivatedEvent Is Nothing Then
			RaiseEvent Deactivated(Me, EventArgs.Empty)
		End If

		If Not itemTraceSource Is Nothing Then
			itemTraceSource.TraceInformation(String.Format(CultureInfo.CurrentCulture, My.Resources.TraceWorkItemStatusChangedToDeactivated, ID))
		End If
	End Sub

	''' <summary>
	''' Fires the <see cref="Deactivating"/> event.
	''' </summary>
	Protected Overridable Sub OnDeactivating(ByVal args As CancelEventArgs)
		If Not itemTraceSource Is Nothing Then
			itemTraceSource.TraceInformation(String.Format(CultureInfo.CurrentCulture, My.Resources.TraceWorkItemStatusDeactivating, ID))
		End If

		If Not Me.DeactivatingEvent Is Nothing Then
			RaiseEvent Deactivating(Me, args)
		End If
	End Sub

	''' <summary>
	''' Fires the <see cref="Disposed"/> event.
	''' </summary>
	Protected Overridable Sub OnDisposed()
		If Not DisposedEvent Is Nothing Then
			RaiseEvent Disposed(Me, EventArgs.Empty)
		End If

		If Not itemTraceSource Is Nothing Then
			itemTraceSource.TraceInformation(String.Format(CultureInfo.CurrentCulture, My.Resources.TraceWorkItemStatusChangedToDisposed, ID))
		End If
	End Sub

	''' <summary>
	''' Fires the <see cref="IdChanged"/> event.
	''' </summary>
	Protected Overridable Sub OnIdChanged()
		If Not IdChangedEvent Is Nothing Then
			RaiseEvent IdChanged(Me, New DataEventArgs(Of String)(ID))
		End If
	End Sub

	''' <summary>
	''' Fires the <see cref="Initialized"/> event.
	''' </summary>
	Protected Overridable Sub OnInitialized()
		If Not Me.InitializedEvent Is Nothing Then
			RaiseEvent Initialized(Me, EventArgs.Empty)
		End If

		If Not itemTraceSource Is Nothing Then
			itemTraceSource.TraceInformation(String.Format(CultureInfo.CurrentCulture, My.Resources.TraceWorkItemStatusChangedToInitialized, ID))
		End If
	End Sub

	''' <summary>
	''' Fires the <see cref="ObjectRemoved"/> event.
	''' </summary>
	Protected Overridable Sub OnObjectAdded(ByVal item As Object)
		If Not ObjectAddedEvent Is Nothing Then
			RaiseEvent ObjectAdded(Me, New DataEventArgs(Of Object)(item))
		End If
	End Sub

	''' <summary>
	''' Fires the <see cref="ObjectRemoved"/> event.
	''' </summary>
	Protected Overridable Sub OnObjectRemoved(ByVal item As Object)
		If Not ObjectRemovedEvent Is Nothing Then
			RaiseEvent ObjectRemoved(Me, New DataEventArgs(Of Object)(item))
		End If
	End Sub

	''' <summary>
	''' Fires the <see cref="RunStarted"/> event. Derived classes can override this 
	''' method to place custom business logic to execute when the <see cref="Run"/> 
	''' method is called on the <see cref="WorkItem"/>.
	''' </summary>
	Protected Overridable Sub OnRunStarted()
		If Not Me.RunStartedEvent Is Nothing Then
			RaiseEvent RunStarted(Me, EventArgs.Empty)
		End If

		itemTraceSource.TraceInformation(String.Format(CultureInfo.CurrentCulture, My.Resources.TraceWorkItemStatusChangedToInitialized, ID))
	End Sub

	''' <summary>
	''' Fires the <see cref="Terminated"/> event.
	''' </summary>
	Protected Overridable Sub OnTerminated()
		If Not Me.TerminatedEvent Is Nothing Then
			RaiseEvent Terminated(Me, EventArgs.Empty)
		End If

		If Not itemTraceSource Is Nothing Then
			itemTraceSource.TraceInformation(String.Format(CultureInfo.CurrentCulture, My.Resources.TraceWorkItemStatusChangedToTerminated, ID))
		End If
	End Sub

	''' <summary>
	''' Fires the <see cref="Terminating"/> event.
	''' </summary>
	Protected Overridable Sub OnTerminating()
		If Not itemTraceSource Is Nothing Then
			itemTraceSource.TraceInformation(String.Format(CultureInfo.CurrentCulture, My.Resources.TraceWorkItemStatusTerminating, ID))
		End If

		If Not Me.TerminatingEvent Is Nothing Then
			RaiseEvent Terminating(Me, EventArgs.Empty)
		End If
	End Sub

#End Region

#Region "Private Members"

#Region "Utility code for the function FindSmartPartInfo"

	Private Function CompareTSmartPartInfo(Of TSmartPartInfo As ISmartPartInfo)(ByVal info As ISmartPartInfo) As Boolean
		Return CObj(info).GetType() Is GetType(TSmartPartInfo)
	End Function

#End Region

	Private Function FindSmartPartInfo(Of TSmartPartInfo As ISmartPartInfo)(ByVal smartPart As Object) As TSmartPartInfo
		Dim result As TSmartPartInfo = Nothing
		If RootWorkItem.smartPartInfos.ContainsKey(smartPart) Then
			result = CType(RootWorkItem.smartPartInfos(smartPart).Find(AddressOf CompareTSmartPartInfo(Of TSmartPartInfo)), TSmartPartInfo)
		End If

		Return result
	End Function

	Private ReadOnly Property ActivationService() As IWorkItemActivationService
		Get
			Return Services.Get(Of IWorkItemActivationService)()
		End Get
	End Property

	Private Sub stateDataStateChanged(ByVal sender As Object, ByVal e As StateChangedEventArgs)
		Dim topicKey As String = StateChangedTopic.BuildStateChangedTopicString(e.Key)
		Dim topic As EventTopic = EventTopics.Get(topicKey)
		If Not topic Is Nothing Then
			topic.Fire(Me, e, Me, PublicationScope.WorkItem)
		End If
	End Sub

	Private Delegate Sub ExtensionCallback(ByVal workItem As WorkItem)

	Private Delegate Sub ExtensionCallbackCancellable(ByVal workItem As WorkItem, ByVal args As CancelEventArgs)

	Private Sub ChangeStatus(ByVal newStatus As WorkItemStatus)
		ThrowIfWorkItemTerminated()

		Dim oldStatus As WorkItemStatus = itemStatus

		If oldStatus <> newStatus Then
			Dim args As CancelEventArgs = New CancelEventArgs()
			FireIfActivating(newStatus, args)
			FireIfDeactivating(newStatus, args)
			FireIfTerminating(newStatus)
			If args.Cancel = False Then
				itemStatus = newStatus
				If Not ActivationService Is Nothing Then
					ActivationService.ChangeStatus(Me)
				End If
				FireStatusEvents()
			End If
		End If
	End Sub

	Private Sub FireIfActivating(ByVal newStatus As WorkItemStatus, ByVal args As CancelEventArgs)
		If newStatus = WorkItemStatus.Active Then
			OnActivating(args)
		End If
	End Sub

	Private Sub FireIfDeactivating(ByVal newStatus As WorkItemStatus, ByVal args As CancelEventArgs)
		If newStatus = WorkItemStatus.Inactive Then
			OnDeactivating(args)
		End If
	End Sub

	Private Sub FireIfTerminating(ByVal newStatus As WorkItemStatus)
		If newStatus = WorkItemStatus.Terminated Then
			OnTerminating()
		End If
	End Sub

	Private Sub FireStatusEvents()
		If Me.itemStatus = WorkItemStatus.Active Then
			OnActivated()
		ElseIf Me.itemStatus = WorkItemStatus.Inactive Then
			OnDeactivated()
		ElseIf Me.itemStatus = WorkItemStatus.Terminated Then
			OnTerminated()
		End If
	End Sub

	Private Sub ThrowIfActivationServiceNotPresent()
		If ActivationService Is Nothing Then
			Throw New ServiceMissingException(GetType(IWorkItemActivationService), Me)
		End If
	End Sub

	Private Sub ThrowIfWorkItemTerminated()
		If Me.Status = WorkItemStatus.Terminated Then
			Throw New InvalidOperationException(My.Resources.WorkItemTerminated)
		End If
	End Sub

#End Region

#Region "Inner Classes"

	Private Class ClassNameTraceSourceAttribute : Inherits TraceSourceAttribute
		Public Sub New()
			MyBase.New(GetType(WorkItem).FullName)
		End Sub
	End Class

#End Region

End Class
