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
Imports System.Reflection
Imports System.Globalization

Namespace EventBroker
	''' <summary>
	''' Represents a publication for a topic and handles the publication event.
	''' </summary>
	Friend Class Publication : Implements IDisposable
		Private topic As EventTopic
		Private wrPublisher As WeakReference
		Private wrWorkItem As WeakReference
		Private innerScope As PublicationScope
		Private innerEventName As String

		''' <summary>
		''' Initializes a new instance of the <see cref="Publication"/> class
		''' </summary>
		Public Sub New(ByVal topic As EventTopic, ByVal publisher As Object, ByVal eventName As String, ByVal workItem As WorkItem, ByVal scope As PublicationScope)
			Me.topic = topic
			Me.wrPublisher = New WeakReference(publisher)
			Me.innerScope = scope
			Me.innerEventName = eventName
			Me.wrWorkItem = New WeakReference(workItem)

			Dim publishedEvent As EventInfo = publisher.GetType().GetEvent(eventName, BindingFlags.Public Or BindingFlags.Instance Or BindingFlags.Static)

			If publishedEvent Is Nothing Then
				Throw New EventBrokerException(String.Format(CultureInfo.CurrentCulture, My.Resources.CannotFindPublishedEvent, eventName))
			End If

			ThrowIfInvalidEventHandler(publishedEvent)
			ThrowIfEventIsStatic(publishedEvent)

			Dim handler As System.Delegate = System.Delegate.CreateDelegate(publishedEvent.EventHandlerType, Me, Me.GetType().GetMethod("PublicationHandler"))
			publishedEvent.AddEventHandler(publisher, handler)
		End Sub

		Private Sub ThrowIfEventIsStatic(ByVal publishedEvent As EventInfo)
			If publishedEvent.GetAddMethod().IsStatic OrElse publishedEvent.GetRemoveMethod().IsStatic Then
				Throw New EventBrokerException(String.Format(CultureInfo.CurrentCulture, My.Resources.StaticPublisherNotAllowed, EventName))
			End If
		End Sub

		Private Sub ThrowIfInvalidEventHandler(ByVal info As EventInfo)
			If GetType(EventHandler).IsAssignableFrom(info.EventHandlerType) OrElse (info.EventHandlerType.IsGenericType AndAlso GetType(EventHandler(Of )).IsAssignableFrom(info.EventHandlerType.GetGenericTypeDefinition())) Then
				Return
			End If

			Throw New EventBrokerException(String.Format(CultureInfo.CurrentCulture, My.Resources.InvalidPublicationSignature, info.DeclaringType.FullName, info.Name))
		End Sub

		''' <summary>
		''' Fires the event publication.
		''' </summary>
		Public Sub PublicationHandler(ByVal sender As Object, ByVal e As EventArgs)
			topic.Fire(Me, sender, e)
		End Sub

		''' <summary>
		''' The publisher of the event.
		''' </summary>
		Public ReadOnly Property Publisher() As Object
			Get
				Return wrPublisher.Target
			End Get
		End Property

		''' <summary>
		''' The <see cref="WorkItem"/> this Publication lives in.
		''' </summary>
		Public ReadOnly Property WorkItem() As WorkItem
			Get
				Return CType(wrWorkItem.Target, WorkItem)
			End Get
		End Property

		''' <summary>
		''' The name of the event on the <see cref="Publication.Publisher"/>.
		''' </summary>
		Public ReadOnly Property EventName() As String
			Get
				Return innerEventName
			End Get
		End Property

		''' <summary>
		''' The <see cref="PublicationScope"/> of the event.
		''' </summary>
		Public ReadOnly Property Scope() As PublicationScope
			Get
				Return innerScope
			End Get
		End Property

		''' <summary>
		''' See <see cref="IDisposable.Dispose"/> for more information.
		''' </summary>
		Public Sub Dispose() Implements IDisposable.Dispose
			Dispose(True)
			GC.SuppressFinalize(Me)
		End Sub

		''' <summary>
		''' Implementation of the disposable pattern.
		''' </summary>
		Protected Overridable Sub Dispose(ByVal disposing As Boolean)
			If disposing Then
				Dim aPublisher As Object = Publisher
				If Not aPublisher Is Nothing Then
					Dim publishedEvent As EventInfo = aPublisher.GetType().GetEvent(innerEventName)
					publishedEvent.RemoveEventHandler(aPublisher, System.Delegate.CreateDelegate(publishedEvent.EventHandlerType, Me, Me.GetType().GetMethod("PublicationHandler")))
				End If
			End If
		End Sub
	End Class
End Namespace
