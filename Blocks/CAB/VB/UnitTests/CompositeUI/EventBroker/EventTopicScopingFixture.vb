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

Namespace Tests.EventBroker
	<TestClass()> _
	Public Class EventTopicScopingFixture
		Private Shared item1 As WorkItem
		Private Shared item2 As WorkItem
		Private Shared item3 As WorkItem
		Private Shared item4 As WorkItem

		Private Shared topic As EventTopic
		Private Shared publisher1 As TestPublisher
		Private Shared subscriber1 As TestSubscriber
		Private Shared subscriber2 As TestSubscriber
		Private Shared subscriber3 As TestSubscriber
		Private Shared subscriber4 As TestSubscriber

		Shared Sub New()
			item1 = New TestableRootWorkItem()
			item2 = item1.WorkItems.AddNew(Of WorkItem)()
			item3 = item1.WorkItems.AddNew(Of WorkItem)()
			item4 = item2.WorkItems.AddNew(Of WorkItem)()
		End Sub

		<TestInitialize()> _
		Public Sub Setup()
			topic = New EventTopic()

			publisher1 = New TestPublisher()
			subscriber1 = New TestSubscriber()
			subscriber2 = New TestSubscriber()
			subscriber3 = New TestSubscriber()
			subscriber4 = New TestSubscriber()

			topic.AddSubscription(subscriber1, "TestEventHandler", item1, ThreadOption.Publisher)
			topic.AddSubscription(subscriber2, "TestEventHandler", item2, ThreadOption.Publisher)
			topic.AddSubscription(subscriber3, "TestEventHandler", item3, ThreadOption.Publisher)
			topic.AddSubscription(subscriber4, "TestEventHandler", item4, ThreadOption.Publisher)
		End Sub


		<TestMethod()> _
		Public Sub LocalScopePublicationAreHandledLocally()
			topic.AddPublication(publisher1, "TestEvent", item2, PublicationScope.WorkItem)
			publisher1.FireTestEvent()

			Assert.IsFalse(subscriber1.TestEventHandlerCalled)
			Assert.IsTrue(subscriber2.TestEventHandlerCalled)
			Assert.IsFalse(subscriber3.TestEventHandlerCalled)
			Assert.IsFalse(subscriber4.TestEventHandlerCalled)
		End Sub


		<TestMethod()> _
		Public Sub GlobalScopePublicationIsHandledInAllSubscribers()
			topic.AddPublication(publisher1, "TestEvent", item2, PublicationScope.Global)
			publisher1.FireTestEvent()

			Assert.IsTrue(subscriber1.TestEventHandlerCalled)
			Assert.IsTrue(subscriber2.TestEventHandlerCalled)
			Assert.IsTrue(subscriber3.TestEventHandlerCalled)
			Assert.IsTrue(subscriber4.TestEventHandlerCalled)
		End Sub


		<TestMethod()> _
		Public Sub DescendantScopedFormRootIsHandledByAllSubscribers()
			topic.AddPublication(publisher1, "TestEvent", item1, PublicationScope.Descendants)
			publisher1.FireTestEvent()

			Assert.IsTrue(subscriber1.TestEventHandlerCalled)
			Assert.IsTrue(subscriber2.TestEventHandlerCalled)
			Assert.IsTrue(subscriber3.TestEventHandlerCalled)
			Assert.IsTrue(subscriber4.TestEventHandlerCalled)
		End Sub

		<TestMethod()> _
		Public Sub DescendatScopedIsHandledOnWorkItemAndChildren()
			topic.AddPublication(publisher1, "TestEvent", item2, PublicationScope.Descendants)
			publisher1.FireTestEvent()

			Assert.IsFalse(subscriber1.TestEventHandlerCalled)
			Assert.IsTrue(subscriber2.TestEventHandlerCalled)
			Assert.IsFalse(subscriber3.TestEventHandlerCalled)
			Assert.IsTrue(subscriber4.TestEventHandlerCalled)
		End Sub

		<TestMethod()> _
		Public Sub AddedMiddleSubscriberGetCalled()
			Dim added As TestSubscriber = New TestSubscriber()
			topic.AddPublication(publisher1, "TestEvent", item1, PublicationScope.Descendants)
			topic.AddSubscription(added, "TestEventHandler", item3, ThreadOption.Publisher)
			publisher1.FireTestEvent()

			Assert.IsTrue(added.TestEventHandlerCalled)
		End Sub

		Private Class TestPublisher
			Public Event TestEvent As EventHandler

			Public Sub FireTestEvent()
				If Not TestEventEvent Is Nothing Then
					RaiseEvent TestEvent(Me, EventArgs.Empty)
				End If

			End Sub
		End Class

		Private Class TestSubscriber
			Public TestEventHandlerCalled As Boolean = False

			Public Sub TestEventHandler(ByVal sender As Object, ByVal e As EventArgs)
				TestEventHandlerCalled = True
			End Sub
		End Class

	End Class
End Namespace
