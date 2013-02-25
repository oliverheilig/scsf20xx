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
Imports System.Threading
Imports Microsoft.Practices.CompositeUI.EventBroker
Imports System.Windows.Forms
Imports System.Reflection

Namespace Tests.EventBroker
	<TestClass()> _
	Public Class EventTopicSubscriptionModesFixture
		Private Shared item As WorkItem
		Private Shared topic As EventTopic

		<TestInitialize()> _
		Public Sub Setup()
			item = New TestableRootWorkItem()
			topic = New EventTopic()
		End Sub


		<TestMethod()> _
		Public Sub RunInPublisherThreadCallsRunInCallerThread()
			Dim subscriber As TestSubscriber = New TestSubscriber()
			topic.AddSubscription(subscriber, "TestEventHandler", item, ThreadOption.Publisher)

			topic.Fire(Me, EventArgs.Empty, item, PublicationScope.WorkItem)

			Assert.AreEqual(Thread.CurrentThread.ManagedThreadId, subscriber.ManagedThreadId)
		End Sub

		<TestMethod()> _
		Public Sub RunInPublisherThreadCallsRunInPublisherThread()
			Dim subscriber As TestSubscriber = New TestSubscriber()
			Dim publisher As TestPublisher = New TestPublisher()
			topic.AddPublication(publisher, "TestEvent", item, PublicationScope.WorkItem)
			topic.AddSubscription(subscriber, "TestEventHandler", item, ThreadOption.Publisher)

			publisher.FireTestEvent()

			Assert.AreEqual(publisher.ManagedThreadId, subscriber.ManagedThreadId)
		End Sub


		<TestMethod()> _
		Public Sub RunInBackgroundWorkerRunInBackgroundWorker()
			Dim subscriber As AsyncTestSubscriber = New AsyncTestSubscriber()
			topic.AddSubscription(subscriber, "TestEventHandler", item, ThreadOption.Background)

			topic.Fire(Me, EventArgs.Empty, item, PublicationScope.WorkItem)

			Dim result As Boolean = AsyncTestSubscriber.Wait.WaitOne(5000, True)

			Assert.IsTrue(result AndAlso (Thread.CurrentThread.ManagedThreadId <> AsyncTestSubscriber.ManagedThreadId))
		End Sub

		<TestMethod()> _
		Public Sub RunInUserInterfaceThread()
			Dim publisher As BackgroundThreadPublisher = New BackgroundThreadPublisher()

			Dim subscriber As UISubscriber = New UISubscriber()
			topic.AddSubscription(subscriber, "TestEventHandler", item, ThreadOption.UserInterface)

			publisher.CanFireTopic.Set()
			Application.Run()

			Assert.AreEqual(Thread.CurrentThread.ManagedThreadId, subscriber.ManagedThreadId)
		End Sub


		<TestMethod()> _
		Public Sub RunInUserInterfaceThreadExceptionsAreReported()
			Dim publisher As BackgroundThreadPublisher = New BackgroundThreadPublisher()

			Dim subscriber As UISubscriber = New UISubscriber()
			topic.AddSubscription(subscriber, "FailingHandler", item, ThreadOption.UserInterface)

			publisher.CanFireTopic.Set()
			Application.Run()

			Thread.Sleep(500)
			Assert.IsNotNull(publisher.ThrownException)
		End Sub

		<TestMethod()> _
		Public Sub RunInUserInterfaceThreadWithNoSyncronizationContextCallsSubscriber()
			Dim publisher As BackgroundThreadPublisher = New BackgroundThreadPublisher()

			Dim context As SynchronizationContext = SynchronizationContext.Current
			Dim subscriber As TestSubscriber = New TestSubscriber()
			topic.AddSubscription(subscriber, "TestEventHandler", item, ThreadOption.UserInterface)

			publisher.CanFireTopic.Set()

			subscriber.HandlerCalledSignal.WaitOne()
			Assert.IsTrue(subscriber.TestEventHandlerCalled)
		End Sub

		Private Class UISubscriber : Inherits Control
			Public ManagedThreadId As Integer = -1

			Public Sub TestEventHandler(ByVal sender As Object, ByVal e As EventArgs)
				ManagedThreadId = Thread.CurrentThread.ManagedThreadId
				Application.ExitThread()
			End Sub

			Public Sub FailingHandler(ByVal sender As Object, ByVal e As EventArgs)
				Try
					ManagedThreadId = Thread.CurrentThread.ManagedThreadId
					Throw New Exception("FailingHandler")
				Finally
					Application.ExitThread()
				End Try
			End Sub
		End Class

		Private Class BackgroundThreadPublisher
			Public CanFireTopic As AutoResetEvent = New AutoResetEvent(False)
			Private worker As Thread
			Public ThrownException As EventTopicException = Nothing

			Public Sub New()
				worker = New Thread(AddressOf Work)
				worker.Start()
			End Sub

			Public Sub Work()
				CanFireTopic.WaitOne()
				Try
					topic.Fire(Me, EventArgs.Empty, item, PublicationScope.WorkItem)
				Catch ex As EventTopicException
					ThrownException = ex
					Application.ExitThread()
				End Try
			End Sub
		End Class

		Public Class TestPublisher
			Public Event TestEvent As EventHandler

			Public ManagedThreadId As Integer = -1

			Public Sub FireTestEvent()
				ManagedThreadId = Thread.CurrentThread.ManagedThreadId
				If Not TestEventEvent Is Nothing Then
					RaiseEvent TestEvent(Me, EventArgs.Empty)
				End If

			End Sub
		End Class

		Private Class TestSubscriber
			Public TestEventHandlerCalled As Boolean = False
			Public ManagedThreadId As Integer = -1
			Public HandlerCalledSignal As AutoResetEvent = New AutoResetEvent(False)

			Public Sub TestEventHandler(ByVal sender As Object, ByVal e As EventArgs)
				ManagedThreadId = Thread.CurrentThread.ManagedThreadId
				TestEventHandlerCalled = True
				HandlerCalledSignal.Set()
			End Sub
		End Class

		Private Class AsyncTestSubscriber
			Public Shared TestEventHandlerCalled As Boolean = False
			Public Shared ManagedThreadId As Integer = -1
			Public Shared Wait As AutoResetEvent = New AutoResetEvent(False)

			Public Sub TestEventHandler(ByVal sender As Object, ByVal e As EventArgs)
				ManagedThreadId = Thread.CurrentThread.ManagedThreadId
				TestEventHandlerCalled = True
				Wait.Set()
			End Sub
		End Class

		Private Class SeparatedThreadEventPublisher
			Private FireSignal As AutoResetEvent = New AutoResetEvent(False)

			Public Sub New()
				Dim thread As Thread = New Thread(AddressOf FiringTopicThread)
				thread.Start()
			End Sub

			Private Sub FiringTopicThread()
				Do While FireSignal.WaitOne()
					topic.Fire(Me, EventArgs.Empty, item, PublicationScope.WorkItem)
				Loop
			End Sub


			Public Sub Fire()
				FireSignal.Set()
			End Sub
		End Class
	End Class
End Namespace
