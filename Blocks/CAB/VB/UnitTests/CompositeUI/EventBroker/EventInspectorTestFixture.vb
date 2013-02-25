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
Imports Microsoft.Practices.CompositeUI.Services
Imports Microsoft.Practices.CompositeUI.EventBroker
Imports Microsoft.Practices.CompositeUI.UIElements
Imports Microsoft.Practices.CompositeUI.Tests.Mocks
Imports System.ComponentModel

Namespace Tests.EventBroker
	<TestClass()> _
	Public Class EventInspectorFixture
		'static EventCatalogServiceMock catalog;
		Private Shared workItem As WorkItem

		<TestInitialize()> _
		Public Sub SetUp()
			workItem = New TestableRootWorkItem()
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub CannotRegisterNullObject()
			EventInspector.Register(Nothing, workItem)
		End Sub

		<TestMethod()> _
		Public Sub CanRegisterObject()
			EventInspector.Register(New Object(), workItem)

			Assert.AreEqual(0, workItem.EventTopics.Count)
		End Sub

		<TestMethod()> _
		Public Sub CanRegisterLocalEventPublisher()
			EventInspector.Register(New Mocks.LocalEventPublisher(), workItem)

			Assert.AreEqual(1, workItem.EventTopics.Count)
			Assert.AreEqual("LocalEvent", workItem.EventTopics.Get("LocalEvent").Name)
			Assert.AreEqual(1, workItem.EventTopics.Get("LocalEvent").PublicationCount)
		End Sub

		<TestMethod()> _
		Public Sub CanRegisterGlobalEventPublisher()
			Dim handle As EventTopic = New EventTopic()

			EventInspector.Register(New Mocks.GlobalEventPublisher(), workItem)

			Assert.AreEqual(1, workItem.EventTopics.Count)
			Assert.AreEqual("GlobalEvent", workItem.EventTopics.Get("GlobalEvent").Name)
			Assert.AreEqual(1, workItem.EventTopics.Get("GlobalEvent").PublicationCount)
		End Sub

		<TestMethod()> _
		Public Sub EmptyTopicsGetsUnregistered()
			Dim topic As EventTopic = New EventTopic()
			workItem.EventTopics.Add(topic, "GlobalEvent")
			Dim publisher As Mocks.GlobalEventPublisher = New Mocks.GlobalEventPublisher()
			Dim subscriber As Mocks.GlobalEventHandler = New Mocks.GlobalEventHandler()

			EventInspector.Register(publisher, workItem)
			Assert.AreEqual(1, topic.PublicationCount)

			EventInspector.Register(subscriber, workItem)
			Assert.AreEqual(1, topic.SubscriptionCount)

			EventInspector.Unregister(publisher, workItem)
			Assert.AreEqual(0, topic.PublicationCount)
			Assert.AreEqual(1, topic.SubscriptionCount)

			EventInspector.Unregister(subscriber, workItem)
			Assert.AreEqual(0, topic.PublicationCount)
			Assert.AreEqual(0, topic.SubscriptionCount)
		End Sub

		<TestMethod()> _
		Public Sub CanRegisterObjectWithOverloadedMethodes()
			EventInspector.Register(New subA(), workItem)

			Assert.AreEqual(1, workItem.EventTopics.Count)
		End Sub

		Public Class subA
			<EventSubscription("testA")> _
			Public Sub HandlerA(ByVal sender As Object, ByVal e As EventArgs)
			End Sub

			Public Sub HandlerA()
			End Sub
		End Class


		Private Class MockInspectorTarget
			<EventPublication("LocalEvent", PublicationScope.WorkItem)> _
			Public Event LocalEvent As EventHandler

			<EventPublication("GlobalEvent", PublicationScope.Global)> _
			Public Event GlobalEvent As EventHandler

			<EventSubscription("TestTopic")> _
			Public Sub TestTopicHandler(ByVal sender As Object, ByVal e As EventArgs)
			End Sub

			Private Sub ResolveCompilerWarnings()
				RaiseEvent LocalEvent(Nothing, Nothing)
				RaiseEvent GlobalEvent(Nothing, Nothing)
			End Sub

		End Class
	End Class
End Namespace
