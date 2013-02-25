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
Imports System.Text
Imports Microsoft.Practices.CompositeUI.Utility
Imports System.ComponentModel
Imports System.Collections.ObjectModel
Imports System.Diagnostics
Imports Microsoft.Practices.ObjectBuilder

Namespace EventBroker
	''' <summary>
	''' Represents a point of communication on a certain topic between the topic publishers and the topic subscribers.
	''' </summary>
	Public Class EventTopic : Implements IDisposable, IBuilderAware
		Private innerName As String
		Private innerEnabled As Boolean = True
		Private publications As List(Of Publication) = New List(Of Publication)()
		Private aWorkItemSubscriptionsList As List(Of WorkItemSubscriptions) = New List(Of WorkItemSubscriptions)()
		Private innerTraceSource As TraceSource = Nothing

		''' <summary>
		''' Sets the <see cref="TraceSource"/> to use for information tracing.
		''' </summary>
		<ClassNameTraceSource()> _
		Public Property TraceSource() As TraceSource
			Set(ByVal value As TraceSource)
				innerTraceSource = value
			End Set
			Protected Get
				Return innerTraceSource
			End Get
		End Property

		''' <summary>
		''' Gets the topic name.
		''' </summary>
		Public ReadOnly Property Name() As String
			Get
				Return innerName
			End Get
		End Property

		''' <summary>
		''' Gets or sets the enabled state of the topic. A disable topic will not fire events.
		''' </summary>
		Public Property Enabled() As Boolean
			Get
				Return innerEnabled
			End Get
			Set(ByVal value As Boolean)
				innerEnabled = value
			End Set
		End Property

		''' <summary>
		''' Adds a publication to the topic.
		''' </summary>
		''' <param name="publisher">The object that publishes the event that will fire the topic.</param>
		''' <param name="eventName">The name of the event.</param>
		''' <param name="workItem">The <see cref="WorkItem"/> where the publisher is.</param>
		''' <param name="scope">A <see cref="PublicationScope"/> value which states scope for the publication.</param>
		Public Overridable Sub AddPublication(ByVal publisher As Object, ByVal eventName As String, ByVal workItem As WorkItem, ByVal scope As PublicationScope)
			Guard.ArgumentNotNull(publisher, "publisher")
			Guard.ArgumentNotNullOrEmptyString(eventName, "eventName")
			Guard.ArgumentNotNull(workItem, "workItem")
			Guard.EnumValueIsDefined(GetType(PublicationScope), scope, "scope")
			Clean()
			ThrowIfRepeatedPublication(publisher, eventName)

			Dim publication As Publication = New Publication(Me, publisher, eventName, workItem, scope)

			publications.Add(publication)

			If Not innerTraceSource Is Nothing Then
				innerTraceSource.TraceInformation(My.Resources.EventTopicTracePublicationAdded, innerName, eventName, publisher.GetType().ToString())
			End If
		End Sub

		''' <summary>
		''' Checks if the specified publication is already added to the <see cref="EventTopic"/>.
		''' </summary>
		''' <param name="publisher">The object that contains the publication.</param>
		''' <param name="eventName">The name of event on the publisher that fires the topic.</param>
		''' <returns>True if the topic contains the requested publication; otherwise False.</returns>
		Public Overridable Function ContainsPublication(ByVal publisher As Object, ByVal eventName As String) As Boolean
			Guard.ArgumentNotNull(publisher, "publisher")
			Guard.ArgumentNotNull(eventName, "eventName")
			Clean()
			Return Not FindPublication(publisher, eventName) Is Nothing
		End Function

		''' <summary>
		''' Removes a publication from the topic.
		''' </summary>
		''' <param name="publisher">The object that contains the publication.</param>
		''' <param name="eventName">The name of event on the publisher that fires the topic.</param>
		Public Overridable Sub RemovePublication(ByVal publisher As Object, ByVal eventName As String)
			Guard.ArgumentNotNull(publisher, "publisher")
			Guard.ArgumentNotNull(eventName, "eventName")
			Clean()
			Dim publication As Publication = FindPublication(publisher, eventName)
			If Not publication Is Nothing Then
				publications.Remove(publication)
				publication.Dispose()
				If Not innerTraceSource Is Nothing Then
					innerTraceSource.TraceInformation(My.Resources.EventTopicTracePublicationRemoved, innerName, eventName, publisher.GetType().ToString())
				End If
			End If
		End Sub

		''' <summary>
		''' Gets the count of registered publications with this <see cref="EventTopic"/>.
		''' </summary>
		Public Overridable ReadOnly Property PublicationCount() As Integer
			Get
				Clean()
				Return publications.Count
			End Get
		End Property

		''' <summary>
		''' Fires the <see cref="EventTopic"/>.
		''' </summary>
		''' <param name="sender">The object that acts as the sender of the event to the subscribers.</param>
		''' <param name="e">An <see cref="EventArgs"/> instance to be passed to the subscribers.</param>
		''' <param name="workItem">The <see cref="WorkItem"/> where the object firing the event is.</param>
		''' <param name="scope">A <see cref="PublicationScope"/> value stating the scope of the firing behavior.</param>
		Public Overridable Sub Fire(ByVal sender As Object, ByVal e As EventArgs, ByVal workItem As WorkItem, ByVal scope As PublicationScope)
			Guard.EnumValueIsDefined(GetType(PublicationScope), scope, "scope")

			If innerEnabled Then
				If Not innerTraceSource Is Nothing Then
					innerTraceSource.TraceInformation(My.Resources.EventTopicTraceFireStarted, innerName)
				End If

				Clean()

				Select Case scope
					Case PublicationScope.WorkItem
						CallSubscriptionHandlers(sender, e, GetWorkItemHandlers(workItem))
					Case PublicationScope.Global
						CallSubscriptionHandlers(sender, e, GetAllHandlers())
					Case PublicationScope.Descendants
						CallSubscriptionHandlers(sender, e, GetDescendantsHandlers(workItem))
					Case Else
						Throw New ArgumentException(My.Resources.InvalidPublicationScope)
				End Select
				If Not innerTraceSource Is Nothing Then
					innerTraceSource.TraceInformation(My.Resources.EventTopicTraceFireCompleted, innerName)
				End If
			End If
		End Sub

		''' <summary>
		''' Adds a subcription to this <see cref="EventTopic"/>.
		''' </summary>
		''' <param name="subscriber">The object that contains the method that will handle the <see cref="EventTopic"/>.</param>
		''' <param name="handlerMethodName">The name of the method on the subscriber that will handle the <see cref="EventTopic"/>.</param>
		''' <param name="workItem">The <see cref="WorkItem"/> where the subscriber is.</param>
		''' <param name="threadOption">A <see cref="ThreadOption"/> value indicating how the handler method should be called.</param>
		Public Overridable Sub AddSubscription(ByVal subscriber As Object, ByVal handlerMethodName As String, ByVal workItem As WorkItem, ByVal threadOption As ThreadOption)
			AddSubscription(subscriber, handlerMethodName, Nothing, workItem, threadOption)
		End Sub

		''' <summary>
		''' Adds a subcription to this <see cref="EventTopic"/>.
		''' </summary>
		''' <param name="subscriber">The object that contains the method that will handle the <see cref="EventTopic"/>.</param>
		''' <param name="handlerMethodName">The name of the method on the subscriber that will handle the <see cref="EventTopic"/>.</param>
		''' <param name="workItem">The <see cref="WorkItem"/> where the subscriber is.</param>
		''' <param name="threadOption">A <see cref="ThreadOption"/> value indicating how the handler method should be called.</param>
		''' <param name="paramterTypes">Defines the types and order of the parameters for the subscriber. For none pass Nothing.
		''' Use this overload when there are several methods with the same name on the subscriber.</param>
		Public Overridable Sub AddSubscription(ByVal subscriber As Object, ByVal handlerMethodName As String, ByVal parameterTypes As Type(), ByVal workItem As WorkItem, ByVal threadOption As ThreadOption)
			Guard.ArgumentNotNull(subscriber, "subscriber")
			Guard.ArgumentNotNullOrEmptyString(handlerMethodName, "handlerMethodName")
			Guard.EnumValueIsDefined(GetType(ThreadOption), threadOption, "threadOption")
			Clean()
			Dim wis As WorkItemSubscriptions = FindWorkItemSubscription(workItem)
			If wis Is Nothing Then
				wis = New WorkItemSubscriptions(workItem)
				aWorkItemSubscriptionsList.Add(wis)
			End If
			wis.AddSubscription(subscriber, handlerMethodName, parameterTypes, threadOption)
			If Not innerTraceSource Is Nothing Then
				innerTraceSource.TraceInformation(My.Resources.EventTopicTraceSubscriptionAdded, innerName, handlerMethodName, subscriber.GetType().ToString())
			End If
		End Sub


		''' <summary>
		''' Removes a subscription from this <see cref="EventTopic"/>.
		''' </summary>
		''' <param name="subscriber">The object that contains the method that will handle the <see cref="EventTopic"/>.</param>
		''' <param name="handlerMethodName">The name of the method on the subscriber that will handle the <see cref="EventTopic"/>.</param>
		Public Overridable Sub RemoveSubscription(ByVal subscriber As Object, ByVal handlerMethodName As String)
			Guard.ArgumentNotNull(subscriber, "subscriber")
			Guard.ArgumentNotNull(handlerMethodName, "handlerMethodName")
			Clean()
			For Each wis As WorkItemSubscriptions In aWorkItemSubscriptionsList
				wis.RemoveSubscription(subscriber, handlerMethodName)
			Next wis

			If Not innerTraceSource Is Nothing Then
				innerTraceSource.TraceInformation(My.Resources.EventTopicTraceSubscriptionRemoved, innerName, handlerMethodName, subscriber.GetType().ToString())
			End If
		End Sub

		''' <summary>
		''' Checks if the specified subscription has been registered with this <see cref="EventTopic"/>.
		''' </summary>
		''' <param name="subscriber">The object that contains the method that will handle the <see cref="EventTopic"/>.</param>
		''' <param name="handlerMethodName">The name of the method on the subscriber that will handle the <see cref="EventTopic"/>.</param>
		''' <returns>True, if the topic contains the subscription; otherwise False.</returns>
		Public Overridable Function ContainsSubscription(ByVal subscriber As Object, ByVal handlerMethodName As String) As Boolean
			Guard.ArgumentNotNull(subscriber, "subscriber")
			Guard.ArgumentNotNull(handlerMethodName, "handlerMethodName")
			Clean()
			Return Not FindSubscription(subscriber, handlerMethodName) Is Nothing
		End Function

		''' <summary>
		''' Gets the count of registered subscriptions to this <see cref="EventTopic"/>.
		''' </summary>
		Public Overridable ReadOnly Property SubscriptionCount() As Integer
			Get
				Clean()
				Dim count As Integer = 0

				For Each wis As WorkItemSubscriptions In aWorkItemSubscriptionsList
					count += wis.SubscriptionCount
				Next wis

				Return count
			End Get
		End Property

		''' <summary>
		''' Perform a sanity cleaning of the dead references to publishers and subscribers
		''' </summary>
		''' <devdoc>As the topic maintains <see cref="WeakReference"/> to publishers and subscribers,
		''' those instances that are finished but hadn't been removed from the topic will leak. This method
		''' deals with that case.</devdoc>
		Private Sub Clean()
			For Each wis As WorkItemSubscriptions In aWorkItemSubscriptionsList.ToArray()
				wis.Clean()
				If wis.SubscriptionCount = 0 Then
					aWorkItemSubscriptionsList.Remove(wis)
				End If
			Next

            For Each publication As Publication In publications.ToArray()
                If publication.Publisher Is Nothing Or publication.WorkItem Is Nothing Or _
                (Not publication.WorkItem Is Nothing And publication.WorkItem.Status = WorkItemStatus.Terminated) Then
                    publications.Remove(publication)
                    publication.Dispose()
                End If
            Next publication
		End Sub

		''' <summary>
		''' Called to free resources.
		''' </summary>
		Public Sub Dispose() Implements IDisposable.Dispose
			Dispose(True)
			GC.SuppressFinalize(Me)
		End Sub

		''' <summary>
		''' Called to free resources.
		''' </summary>
		''' <param name="disposing">Should be True when calling from Dispose().</param>
		Protected Overridable Sub Dispose(ByVal disposing As Boolean)
			If disposing Then
				For Each publication As Publication In publications
					publication.Dispose()
				Next publication
				publications.Clear()
				aWorkItemSubscriptionsList.Clear()
			End If
		End Sub

		Private Sub CallSubscriptionHandlers(ByVal sender As Object, ByVal e As EventArgs, ByVal handlers As EventTopicFireDelegate())
			Dim exceptions As List(Of Exception) = New List(Of Exception)()

			For Each handler As EventTopicFireDelegate In handlers
				handler(sender, e, exceptions, innerTraceSource)
			Next handler

			If exceptions.Count > 0 Then
				TraceExceptions(exceptions)
				Throw New EventTopicException(Me, New ReadOnlyCollection(Of Exception)(exceptions))
			End If
		End Sub

		Private Sub TraceExceptions(ByVal exceptions As List(Of Exception))
			If Not innerTraceSource Is Nothing Then
				innerTraceSource.TraceInformation(My.Resources.EventTopicTraceFireExceptions, innerName)
				For Each ex As Exception In exceptions
					innerTraceSource.TraceInformation(ex.ToString())
				Next ex
			End If
		End Sub

		Private Function GetDescendantsHandlers(ByVal workItem As WorkItem) As EventTopicFireDelegate()
			Dim descendants As List(Of WorkItem) = New List(Of WorkItem)()
			Dim handlers As List(Of EventTopicFireDelegate) = New List(Of EventTopicFireDelegate)()
			descendants.Add(workItem)
			Dim i As Integer = 0
			Do While i < descendants.Count
				handlers.AddRange(GetWorkItemHandlers(descendants(i)))
				For Each pair As KeyValuePair(Of String, WorkItem) In descendants(i).WorkItems
					descendants.Add(pair.Value)
				Next pair
				i += 1
			Loop
			Return handlers.ToArray()
		End Function

		Private Function GetWorkItemHandlers(ByVal workItem As WorkItem) As EventTopicFireDelegate()
			Dim wis As WorkItemSubscriptions = FindWorkItemSubscription(workItem)
			If Not wis Is Nothing Then
				Return wis.GetHandlers()
			End If
			Return New EventTopicFireDelegate() {}
		End Function

		Private Function GetAllHandlers() As EventTopicFireDelegate()
			Dim handlers As List(Of EventTopicFireDelegate) = New List(Of EventTopicFireDelegate)()

			For Each wis As WorkItemSubscriptions In aWorkItemSubscriptionsList
				handlers.AddRange(wis.GetHandlers())
			Next wis
			Return handlers.ToArray()
		End Function

		Friend Sub Fire(ByVal publication As Publication, ByVal sender As Object, ByVal e As EventArgs)
			Dim aWorkItem As WorkItem = publication.WorkItem

			If aWorkItem Is Nothing OrElse (Not aWorkItem Is Nothing AndAlso aWorkItem.Status = WorkItemStatus.Terminated) Then
				publications.Remove(publication)
				publication.Dispose()
			Else
				Fire(sender, e, publication.WorkItem, publication.Scope)
			End If
		End Sub


		Private Sub ThrowIfRepeatedPublication(ByVal publisher As Object, ByVal eventName As String)
			If Not FindPublication(publisher, eventName) Is Nothing Then
				Throw New EventBrokerException(My.Resources.OnlyOnePublicationIsAllowed)
			End If
		End Sub

#Region "Utility code for the function FindPublication"
		Private Class PublicationHandler
			Private publisher As Object
			Private eventName As String
			Public Sub New(ByRef publisher As Object, ByRef eventName As String)
				Me.publisher = publisher
				Me.eventName = eventName
			End Sub
			Public Function Compare(ByVal match As Publication) As Boolean
				Return match.Publisher Is publisher AndAlso match.EventName = eventName
			End Function
		End Class
#End Region

		Private Function FindPublication(ByVal publisher As Object, ByVal eventName As String) As Publication
			Dim publicationHandler As PublicationHandler = New PublicationHandler(publisher, eventName)
			Return publications.Find(AddressOf publicationHandler.Compare)
		End Function


#Region "Utility code for the function FindWorkItemSubscription"
		Private Class WorkItemSubscriptionHandler
			Private workItem As WorkItem
			Public Sub New(ByRef workItem As WorkItem)
				Me.workItem = workItem
			End Sub
			Public Function Compare(ByVal match As WorkItemSubscriptions) As Boolean
				Return match.WorkItem Is workItem
			End Function
		End Class
#End Region

		Private Function FindWorkItemSubscription(ByVal workItem As WorkItem) As WorkItemSubscriptions
			Dim workItemSubscriptionHandler As WorkItemSubscriptionHandler = New WorkItemSubscriptionHandler(workItem)
			Return aWorkItemSubscriptionsList.Find(AddressOf workItemSubscriptionHandler.Compare)
		End Function

		Private Function FindSubscription(ByVal subscriber As Object, ByVal handlerMethodName As String) As Subscription
			For Each wis As WorkItemSubscriptions In aWorkItemSubscriptionsList
				Dim subscription As Subscription = wis.FindSubscription(subscriber, handlerMethodName)
				If Not subscription Is Nothing Then
					Return subscription
				End If
			Next wis
			Return Nothing
		End Function

		Private Class ClassNameTraceSourceAttribute : Inherits TraceSourceAttribute
			Public Sub New()
				MyBase.New(GetType(EventTopic).FullName)
			End Sub
		End Class

		''' <summary>
		''' See <see cref="IBuilderAware.OnBuiltUp"/> for more information.
		''' </summary>
		Public Overridable Sub OnBuiltUp(ByVal id As String) Implements IBuilderAware.OnBuiltUp
			Me.innerName = id
		End Sub

		''' <summary>
		''' See <see cref="IBuilderAware.OnTearingDown"/> for more information.
		''' </summary>
		Public Overridable Sub OnTearingDown() Implements IBuilderAware.OnTearingDown
		End Sub
	End Class
End Namespace
