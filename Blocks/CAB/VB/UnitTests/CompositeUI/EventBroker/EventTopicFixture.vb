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
Imports Microsoft.Practices.CompositeUI.EventBroker
Imports System.Diagnostics

Namespace Tests.EventBroker
	<TestClass()> _
	Public Class EventTopicFixture
		Private Shared topic As TestEventTopic
		Private workItem As WorkItem

		<TestInitialize()> _
		Public Sub Setup()
			topic = New TestEventTopic()
			workItem = New TestableRootWorkItem()
		End Sub

		<TestMethod()> _
		Public Sub CanAddPublication()
			Dim publisher As TestPublisher = New TestPublisher()
			Assert.IsFalse(topic.ContainsPublication(publisher, "TestEvent"))
			topic.AddPublication(publisher, "TestEvent", workItem, PublicationScope.WorkItem)

			Assert.IsTrue(topic.ContainsPublication(publisher, "TestEvent"))
		End Sub


		<TestMethod(), ExpectedException(GetType(EventBrokerException))> _
		Public Sub ThrowIfEventNotFound()
			topic.AddPublication(New TestPublisher(), "InexistentEvent", workItem, PublicationScope.WorkItem)
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentException))> _
		Public Sub ThrowIfEmptyEventName()
			topic.AddPublication(New TestPublisher(), String.Empty, workItem, PublicationScope.WorkItem)
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub ThrowIfNullEventName()
			topic.AddPublication(New TestPublisher(), Nothing, workItem, PublicationScope.WorkItem)
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub ThrowsIfNotPublisherSpecified()
			topic.AddPublication(Nothing, "TestEvent", workItem, PublicationScope.WorkItem)
		End Sub


		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub ThrowIfNoWorkItemSpecified()
			topic.AddPublication(New TestPublisher(), "TestEvent", Nothing, PublicationScope.WorkItem)
		End Sub

		<TestMethod(), ExpectedException(GetType(EventBrokerException))> _
		Public Sub ThrowIfAddingSamePublication()
			Dim publisher As TestPublisher = New TestPublisher()
			topic.AddPublication(publisher, "TestEvent", workItem, PublicationScope.WorkItem)
			topic.AddPublication(publisher, "TestEvent", workItem, PublicationScope.WorkItem)
		End Sub


		<TestMethod()> _
		Public Sub AcceptSeveralPublicationFromDifferentEventsOnSamePublisher()
			Dim publisher As TestPublisher = New TestPublisher()
			topic.AddPublication(publisher, "TestEvent", workItem, PublicationScope.WorkItem)
			topic.AddPublication(publisher, "AnotherTestEvent", workItem, PublicationScope.WorkItem)

			Assert.IsTrue(topic.ContainsPublication(publisher, "TestEvent"))
			Assert.IsTrue(topic.ContainsPublication(publisher, "AnotherTestEvent"))
		End Sub

		<TestMethod()> _
		Public Sub CanRemoveOnePublicationFromSamePublisher()
			Dim publisher As TestPublisher = New TestPublisher()
			topic.AddPublication(publisher, "TestEvent", workItem, PublicationScope.WorkItem)
			topic.AddPublication(publisher, "AnotherTestEvent", workItem, PublicationScope.WorkItem)

			topic.RemovePublication(publisher, "TestEvent")

			Assert.IsTrue(topic.ContainsPublication(publisher, "AnotherTestEvent"))
			Assert.IsFalse(topic.ContainsPublication(publisher, "TestEvent"))
		End Sub

		<TestMethod()> _
		Public Sub AddedPublisheEventIsHookedUp()
			Dim publisher As TestPublisher = New TestPublisher()
			topic.AddPublication(publisher, "TestEvent", workItem, PublicationScope.WorkItem)

			Assert.AreEqual(1, publisher.InvocationListLength)
		End Sub

		<TestMethod()> _
		Public Sub AddTwoPublishers()
			Dim publisher1 As TestPublisher = New TestPublisher()
			Dim publisher2 As TestPublisher = New TestPublisher()
			topic.AddPublication(publisher1, "TestEvent", workItem, PublicationScope.WorkItem)
			topic.AddPublication(publisher2, "TestEvent", workItem, PublicationScope.WorkItem)

			Assert.AreEqual(1, publisher1.InvocationListLength)
			Assert.AreEqual(1, publisher2.InvocationListLength)
		End Sub

		<TestMethod()> _
		Public Sub RemoveOnePublisher()
			Dim publisher1 As TestPublisher = New TestPublisher()
			Dim publisher2 As TestPublisher = New TestPublisher()
			topic.AddPublication(publisher1, "TestEvent", workItem, PublicationScope.WorkItem)
			topic.AddPublication(publisher2, "TestEvent", workItem, PublicationScope.WorkItem)
			topic.RemovePublication(publisher1, "TestEvent")

			Assert.AreEqual(0, publisher1.InvocationListLength)
			Assert.AreEqual(1, publisher2.InvocationListLength)
		End Sub

		<TestMethod()> _
		Public Sub FiringThePublisherFiresTheTopic()
			Dim publisher As TestPublisher = New TestPublisher()
			topic.AddPublication(publisher, "TestEvent", workItem, PublicationScope.WorkItem)

			publisher.FireTestEvent()

			Assert.IsTrue(topic.FireCalled)
		End Sub


		<TestMethod()> _
		Public Sub TopicCanBeFiredManually()
			topic.Fire(Me, EventArgs.Empty, workItem, PublicationScope.WorkItem)

			Assert.IsTrue(topic.FireCalled)
		End Sub



		<TestMethod()> _
		Public Sub CanAddSubscription()
			Dim subscriber As TestSubscriber = New TestSubscriber()
			topic.AddSubscription(subscriber, "TestEventHandler", workItem, ThreadOption.Publisher)

			Assert.IsTrue(topic.ContainsSubscription(subscriber, "TestEventHandler"))
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub ThrowIfAddingNullSubscriber()
			topic.AddSubscription(Nothing, "TestEventHandler", workItem, ThreadOption.Publisher)
		End Sub


		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub ThrowIfNullMethodHandler()
			topic.AddSubscription(New TestPublisher(), Nothing, workItem, ThreadOption.Publisher)
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentException))> _
		Public Sub ThrowIfEmptyMethodHandler()
			topic.AddSubscription(New TestPublisher(), String.Empty, workItem, ThreadOption.Publisher)
		End Sub

		<TestMethod(), ExpectedException(GetType(EventBrokerException))> _
		Public Sub ThrowIfInvalidMethodHandlerName()
			topic.AddSubscription(New TestPublisher(), "NonExistingHandler", workItem, ThreadOption.Publisher)
		End Sub

		<TestMethod()> _
		Public Sub CannAddTwoSubscribers()
			Dim subscriber1 As TestSubscriber = New TestSubscriber()
			Dim subscriber2 As TestSubscriber = New TestSubscriber()
			topic.AddSubscription(subscriber1, "TestEventHandler", workItem, ThreadOption.Publisher)
			topic.AddSubscription(subscriber2, "AnotherTestEventHandler", workItem, ThreadOption.Publisher)

			Assert.IsTrue(topic.ContainsSubscription(subscriber1, "TestEventHandler"))
			Assert.IsTrue(topic.ContainsSubscription(subscriber2, "AnotherTestEventHandler"))
		End Sub

		<TestMethod()> _
		Public Sub CanRemoveOneSubscriber()
			Dim subscriber1 As TestSubscriber = New TestSubscriber()
			Dim subscriber2 As TestSubscriber = New TestSubscriber()
			topic.AddSubscription(subscriber1, "TestEventHandler", workItem, ThreadOption.Publisher)
			topic.AddSubscription(subscriber2, "TestEventHandler", workItem, ThreadOption.Publisher)

			topic.RemoveSubscription(subscriber2, "TestEventHandler")

			Assert.IsTrue(topic.ContainsSubscription(subscriber1, "TestEventHandler"))
			Assert.IsFalse(topic.ContainsSubscription(subscriber2, "TestEventHandler"))
		End Sub

		<TestMethod()> _
		Public Sub FiringTopicCallsSubscriber()
			Dim subscriber As TestSubscriber = New TestSubscriber()
			topic.AddSubscription(subscriber, "TestEventHandler", workItem, ThreadOption.Publisher)

			topic.Fire(Me, EventArgs.Empty, workItem, PublicationScope.WorkItem)

			Assert.IsTrue(subscriber.TestEventHandlerCalled)
		End Sub


		<TestMethod()> _
		Public Sub TopicIsEnabledByDefault()
			Assert.IsTrue(topic.Enabled)
		End Sub

		<TestMethod()> _
		Public Sub FiringDisableTopicNotCallSubscriber()
			Dim subscriber As TestSubscriber = New TestSubscriber()
			topic.AddSubscription(subscriber, "TestEventHandler", workItem, ThreadOption.Publisher)

			topic.Enabled = False
			topic.Fire(Me, EventArgs.Empty, workItem, PublicationScope.WorkItem)

			Assert.IsFalse(subscriber.TestEventHandlerCalled)
		End Sub

		<TestMethod()> _
		Public Sub FiringAndEnabledTopicCallsHandler()
			topic.Enabled = False
			Dim subscriber As TestSubscriber = New TestSubscriber()
			topic.AddSubscription(subscriber, "TestEventHandler", workItem, ThreadOption.Publisher)

			topic.Enabled = True
			topic.Fire(Me, EventArgs.Empty, workItem, PublicationScope.WorkItem)

			Assert.IsTrue(subscriber.TestEventHandlerCalled)
		End Sub

		<TestMethod()> _
		Public Sub FinalizedSubscriberIsRemovedOnClean()
			Dim subscriber As TestSubscriber = New TestSubscriber()
			topic.AddSubscription(subscriber, "TestEventHandler", workItem, ThreadOption.Publisher)
			Assert.AreEqual(1, topic.SubscriptionCount)

			subscriber = Nothing
			GC.Collect()
			GC.WaitForPendingFinalizers()

			Assert.AreEqual(0, topic.SubscriptionCount)
		End Sub


		<TestMethod()> _
		Public Sub FinalizedPublisherIsRemovedOnClean()
			Dim publisher As TestPublisher = New TestPublisher()
			topic.AddPublication(publisher, "TestEvent", workItem, PublicationScope.WorkItem)
			Assert.AreEqual(1, topic.PublicationCount)

			publisher = Nothing
			GC.Collect()
			GC.WaitForPendingFinalizers()

			Assert.AreEqual(0, topic.PublicationCount)
		End Sub

		<TestMethod()> _
		Public Sub EventTopicExposesPublicationCount()
			Assert.AreEqual(0, topic.PublicationCount)
			topic.AddPublication(New TestPublisher(), "TestEvent", workItem, PublicationScope.WorkItem)
			Assert.AreEqual(1, topic.PublicationCount)
		End Sub


		<TestMethod(), ExpectedException(GetType(ArgumentException))> _
		Public Sub ThrowIfFiringWithInvalidScope()
			topic.Fire(Me, EventArgs.Empty, workItem, CType(10, PublicationScope))
		End Sub


		<TestMethod()> _
		Public Sub SubscriberCanTakeSpecializedEventArgs()
			Dim subscriber As TestSubscriber = New TestSubscriber()
			Dim args As InheritedEventArgs = New InheritedEventArgs()
			topic.AddSubscription(subscriber, "InheritedEventArgsHandler", workItem, ThreadOption.Publisher)

			topic.Fire(Me, args, workItem, PublicationScope.WorkItem)

			Assert.IsTrue(subscriber.InheritedEventArgsHandlerCalled)
		End Sub


		<TestMethod()> _
		Public Sub EventTopicThrowsWithExceptionsOccurredInSubscribers()
			Dim subscriber1 As TestSubscriber = New TestSubscriber()
			Dim subscriber2 As TestSubscriber = New TestSubscriber()

			topic.AddSubscription(subscriber1, "FailingHandler", workItem, ThreadOption.Publisher)
			topic.AddSubscription(subscriber2, "FailingHandler", workItem, ThreadOption.Publisher)

			Try
				topic.Fire(Me, EventArgs.Empty, workItem, PublicationScope.WorkItem)
			Catch ex As EventTopicException
				Assert.AreEqual(2, ex.Exceptions.Count)
				Assert.AreEqual("FailingHandler", ex.Exceptions(0).Message)
				Assert.AreEqual("FailingHandler", ex.Exceptions(1).Message)
			End Try
		End Sub


		<TestMethod()> _
		Public Sub CanRegisterGenericEventHandlerSignatures()
			Dim publisher As TestPublisher = New TestPublisher()
			topic.AddPublication(publisher, "GenericEvent", workItem, PublicationScope.WorkItem)
			Assert.IsTrue(topic.ContainsPublication(publisher, "GenericEvent"))
		End Sub


		<TestMethod()> _
		Public Sub SubscriberHandlesGenericEvent()
			Dim publisher As TestPublisher = New TestPublisher()
			Dim subscriber As TestSubscriber = New TestSubscriber()
			topic.AddPublication(publisher, "GenericEvent", workItem, PublicationScope.WorkItem)
			topic.AddSubscription(subscriber, "InheritedEventArgsHandler", workItem, ThreadOption.Publisher)

			publisher.FireGenericEvent()
			Assert.IsTrue(subscriber.InheritedEventArgsHandlerCalled)
		End Sub


		<TestMethod()> _
		Public Sub NotFailWhenSubscriberHasBeenFinalized()
			Dim subscriber As TestSubscriber = New TestSubscriber()
			topic.AddSubscription(subscriber, "TestEventHandler", workItem, ThreadOption.Publisher)
			subscriber = Nothing

			GC.Collect()
			GC.WaitForPendingFinalizers()

			topic.Fire(Me, EventArgs.Empty, workItem, PublicationScope.WorkItem)
		End Sub


		<TestMethod()> _
		Public Sub PublishersAreReleasedWhenDisposed()
			Dim publisher As TestPublisher = New TestPublisher()
			topic.AddPublication(publisher, "TestEvent", workItem, PublicationScope.WorkItem)

			topic.Dispose()
			Assert.AreEqual(0, publisher.InvocationListLength)
			Assert.AreEqual(0, topic.PublicationCount)
		End Sub


		<TestMethod()> _
		Public Sub SubscribersAreReleasedWhenDisposed()
			Dim subscriber As TestSubscriber = New TestSubscriber()
			topic.AddSubscription(subscriber, "TestEventHandler", workItem, ThreadOption.Publisher)

			topic.Dispose()
			Assert.AreEqual(0, topic.SubscriptionCount)
		End Sub


		<TestMethod(), ExpectedException(GetType(EventBrokerException))> _
		Public Sub CannotRegisterStaticPublisher()
			Dim publisher As StaticPublisher = New StaticPublisher()
			topic.AddPublication(publisher, "StaticEvent", workItem, PublicationScope.WorkItem)
		End Sub

		<TestMethod(), ExpectedException(GetType(EventBrokerException))> _
		Public Sub CannotRegisterStaticSubscriber()
			Dim subscriber As StaticSubscriber = New StaticSubscriber()
			topic.AddSubscription(subscriber, "StaticEventHandler", workItem, ThreadOption.Publisher)
		End Sub

		<TestMethod()> _
		Public Sub DoesNotFailsWIthMultipleOverloads()
			Dim wi As WorkItem = New TestableRootWorkItem()

			Dim pa As pubA = New pubA()
			Dim sa As subA = New subA()

			wi.Items.Add(pa)
			wi.Items.Add(sa)

			pa.Fire()
		End Sub

		<TestMethod(), ExpectedException(GetType(EventBrokerException))> _
		Public Sub InvalidPublicationSignatureThrows()
			Dim wi As WorkItem = New TestableRootWorkItem()
			Dim topic As EventTopic = wi.EventTopics.AddNew(Of EventTopic)("Foo")
			topic.AddPublication(Me, "InvalidPublicationSignature", wi, PublicationScope.WorkItem)
		End Sub

		Public Delegate Sub MyDelegate(ByVal count As Integer)

		<TestMethod(), ExpectedException(GetType(EventBrokerException))> _
		Public Sub InvalidSubscriptionSignatureThrows()
			Dim wi As WorkItem = New TestableRootWorkItem()
			Dim topic As EventTopic = wi.EventTopics.AddNew(Of EventTopic)("Foo")
			topic.AddSubscription(Me, "InvalidSubscriptionSignature", wi, ThreadOption.Publisher)
		End Sub

		Public Sub InvalidSubscriptionSignature(ByVal count As Integer)
		End Sub

		<TestMethod()> _
		Public Sub AddingRepeatedSubscriptionNoops()
			Dim wi As WorkItem = New TestableRootWorkItem()
			Dim topic As EventTopic = New EventTopic()
			Dim subscriber As TestSubscriber = New TestSubscriber()
			topic.AddSubscription(subscriber, "TestEventHandler", wi, ThreadOption.Publisher)
			topic.AddSubscription(subscriber, "TestEventHandler", wi, ThreadOption.Publisher)

			Assert.AreEqual(1, topic.SubscriptionCount)
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub RemovePublisherNullPublisherThrows()
			Dim topic As EventTopic = New EventTopic()
			topic.RemovePublication(Nothing, "Test")
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub RemovePublisherNullEventThrows()
			Dim topic As EventTopic = New EventTopic()
			Dim pub As TestPublisher = New TestPublisher()
			topic.RemovePublication(pub, Nothing)
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub RemoveSubscriberNullSubscriberThrows()
			Dim topic As EventTopic = New EventTopic()
			topic.RemoveSubscription(Nothing, "Test")
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub RemoveSubscriberNullEventThrows()
			Dim topic As EventTopic = New EventTopic()
			Dim subscriber As TestSubscriber = New TestSubscriber()
			topic.RemoveSubscription(subscriber, Nothing)
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub ContainsSubscriberNullSubscriberThrows()
			Dim topic As EventTopic = New EventTopic()
			topic.ContainsSubscription(Nothing, "Test")
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub ContainsSubscriberNullEventThrows()
			Dim topic As EventTopic = New EventTopic()
			Dim subscriber As TestSubscriber = New TestSubscriber()
			topic.ContainsSubscription(subscriber, Nothing)
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub ContainsPublisherNullSubscriberThrows()
			Dim topic As EventTopic = New EventTopic()
			topic.ContainsPublication(Nothing, "Test")
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub ContainsPublisherNullEventThrows()
			Dim topic As EventTopic = New EventTopic()
			Dim pub As TestPublisher = New TestPublisher()
			topic.ContainsPublication(pub, Nothing)
		End Sub

		<TestMethod(), ExpectedException(GetType(EventBrokerException))> _
		Public Sub AddPublisherWithThreeParamDelegate()
			Dim wi As TestableRootWorkItem = New TestableRootWorkItem()

			Dim a As pubC = New pubC()

			wi.Items.Add(a)
		End Sub

		<TestMethod()> _
		Public Sub TraceSourceIsInjected()
			Dim container As WorkItem = New TestableRootWorkItem()
			Dim ts As ITraceSourceCatalogService = container.Services.Get(Of ITraceSourceCatalogService)()
			Dim topic As TestEventTopic = New TestEventTopic()
			workItem.EventTopics.Add(topic)

			Assert.IsFalse(String.IsNullOrEmpty(topic.TraceSourceName))
		End Sub

		<TestMethod(), ExpectedException(GetType(EventBrokerException))> _
		Public Sub StaticPublisherThrows()
			Dim workItem As WorkItem = New TestableRootWorkItem()
			Dim topic As EventTopic = New EventTopic()
			Dim pub As StaticPublisher = New StaticPublisher()

			topic.AddPublication(pub, "StaticEvent", workItem, PublicationScope.WorkItem)
		End Sub


		<TestMethod()> _
		Public Sub EventTopicCreatedThroughIndexerGetsItsName()
			Dim workItem As WorkItem = New TestableRootWorkItem()
			Dim topic As EventTopic = workItem.EventTopics("Foo")

			Assert.AreEqual("Foo", topic.Name)
		End Sub


		<TestMethod()> _
		Public Sub EventTopicCreatedThroughAddNewGetItsName()
			Dim workItem As WorkItem = New TestableRootWorkItem()
			Dim topic As EventTopic = workItem.EventTopics.AddNew(Of EventTopic)("Foo")

			Assert.AreEqual("Foo", topic.Name)
		End Sub

		<TestMethod()> _
		<ExpectedException(GetType(ArgumentException))> _
		Public Sub ThrowsWhenRemovingEventTopicFromEventTopicCollection()
			Dim workItem As WorkItem = New TestableRootWorkItem()
			Dim topic As EventTopic = workItem.EventTopics.AddNew(Of EventTopic)("Foo")
			workItem.EventTopics.Remove(topic)
		End Sub

		<TestMethod()> _
		Public Sub AddedTopicIsCreatedWithGivenName()
			Dim workItem As WorkItem = New TestableRootWorkItem()

			Dim topic As EventTopic = workItem.EventTopics.AddNew(Of EventTopic)("Foo1")
			Assert.AreEqual("Foo1", topic.Name)

			topic = workItem.EventTopics("Foo2")
			Assert.AreEqual("Foo2", topic.Name)

		End Sub

		<TestMethod()> _
		Public Sub GenericSubscribersCanBeUsed()
			Dim workItem As WorkItem = New TestableRootWorkItem()
			Dim aPub As pubA = workItem.Items.AddNew(Of pubA)()
			Dim aSub As TSubA(Of Object) = workItem.Items.AddNew(Of TSubA(Of Object))()

			aPub.Fire()
			Assert.IsTrue(aSub.HandlerACalled)
		End Sub

#Region "Supporting Classes"

		Public Delegate Sub Temp(ByVal sender As Object, ByVal e As EventArgs, ByVal i As Integer)
		Public Class pubC
			<EventPublication("testA")> _
			Public Event A As Temp

			Public Sub Fire()
				RaiseEvent A(Nothing, Nothing, 0)
			End Sub
		End Class

		Public Class pubA
			<EventPublication("testA")> _
			Public Event A As EventHandler

			Public Sub Fire()
				RaiseEvent A(Nothing, Nothing)
			End Sub
		End Class

		Public Class subA
			<EventSubscription("testA")> _
			Public Sub HandlerA(ByVal sender As Object, ByVal e As EventArgs)
			End Sub

			Public Sub HandlerA()
			End Sub
		End Class

		Public Class TSubA(Of T)
			Public dummy As T
			Public HandlerACalled As Boolean

			<EventSubscription("testA")> _
			Public Sub HandlerA(ByVal sender As Object, ByVal e As EventArgs)
				HandlerACalled = True
			End Sub

			Public Sub HandlerA()
			End Sub
		End Class

		Public Class TSubC
			Public HandlerACalled As Boolean

			<EventSubscription("testA")> _
			Public Sub HandlerA(ByVal sender As Object, ByVal e As EventArgs)
				HandlerACalled = True
			End Sub
		End Class

		Private Class TestEventTopic : Inherits EventTopic
			Public FireCalled As Boolean = False

			Public Overrides Sub Fire(ByVal sender As Object, ByVal e As EventArgs, ByVal workItem As WorkItem, ByVal scope As PublicationScope)
				FireCalled = True
				MyBase.Fire(sender, e, workItem, scope)
			End Sub

			Public ReadOnly Property TraceSourceName() As String
				Get
					Return MyBase.TraceSource.Name
				End Get
			End Property
		End Class

		Private Class TestPublisher
			Public Event TestEvent As EventHandler

			Public Event AnotherTestEvent As EventHandler

			Public Event GenericEvent As EventHandler(Of InheritedEventArgs)

			Public Sub FireTestEvent()
				If Not TestEventEvent Is Nothing Then
					RaiseEvent TestEvent(Me, EventArgs.Empty)
				End If
			End Sub

			Public Sub FireGenericEvent()
				If Not GenericEventEvent Is Nothing Then
					RaiseEvent GenericEvent(Me, New InheritedEventArgs())
				End If
			End Sub

			Public ReadOnly Property InvocationListLength() As Integer
				Get
					If Not TestEventEvent Is Nothing Then
						Return TestEventEvent.GetInvocationList().Length
					End If
					Return 0
				End Get
			End Property

			Private Sub ResolveCompilerWarnings()
				RaiseEvent AnotherTestEvent(Nothing, Nothing)
			End Sub
		End Class

		Private Class InheritedEventArgs : Inherits EventArgs
		End Class

		Private Class TestSubscriber
			Public TestEventHandlerCalled As Boolean = False

			Public Sub TestEventHandler(ByVal sender As Object, ByVal e As EventArgs)
				TestEventHandlerCalled = True
			End Sub

			Public Sub AnotherTestEventHandler(ByVal sender As Object, ByVal e As EventArgs)
			End Sub

			Public InheritedEventArgsHandlerCalled As Boolean = False

			Public Sub InheritedEventArgsHandler(ByVal sender As Object, ByVal e As InheritedEventArgs)
				InheritedEventArgsHandlerCalled = True
			End Sub

			Public Sub FailingHandler(ByVal sender As Object, ByVal e As EventArgs)
				Throw New Exception("FailingHandler")
			End Sub
		End Class

		Private Class StaticPublisher
			Public Shared Event StaticEvent As EventHandler

			Private Shared Sub CompilerWarnings()
				RaiseEvent StaticEvent(Nothing, Nothing)
			End Sub
		End Class

		Private Class StaticSubscriber
			Public Shared Sub StaticEventHandler(ByVal sender As Object, ByVal e As EventArgs)

			End Sub
		End Class

#End Region
	End Class
End Namespace
