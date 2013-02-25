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
Imports Microsoft.Practices.CompositeUI.EventBroker
Imports Microsoft.Practices.CompositeUI.Tests.Mocks
Imports Microsoft.Practices.CompositeUI.BuilderStrategies
Imports Microsoft.Practices.ObjectBuilder

Namespace BuilderStrategies
	<TestClass()> _
	Public Class EventBrokerStrategyFixture
		<TestMethod()> _
		Public Sub EventStrategyAcceptsEventBrokerServices()
			Dim workItem As WorkItem = New TestableRootWorkItem()
			Dim strat As EventBrokerStrategy = New EventBrokerStrategy()
			Dim context As MockBuilderContext = New MockBuilderContext(strat)
			Dim thing As MockEventObject = New MockEventObject()
			context.Locator.Add(New DependencyResolutionLocatorKey(GetType(WorkItem), Nothing), workItem)

			strat.BuildUp(context, GetType(MockEventObject), thing, Nothing)

			Assert.IsTrue(workItem.EventTopics.Contains("topic1"))
			Assert.IsTrue(workItem.EventTopics.Get("topic1").ContainsPublication(thing, "SomeEvent"))

			Assert.IsTrue(workItem.EventTopics.Contains("globalTopic"))
			Assert.IsTrue(workItem.EventTopics.Get("globalTopic").ContainsPublication(thing, "SomeEvent2"))

			Assert.IsTrue(workItem.EventTopics.Contains("localSubscriptionTopic"))
			Assert.IsTrue(workItem.EventTopics.Get("localSubscriptionTopic").ContainsSubscription(thing, "SomeHandler"))

			Assert.IsTrue(workItem.EventTopics.Contains("globalSubscriptionTopic"))
			Assert.IsTrue(workItem.EventTopics.Get("globalSubscriptionTopic").ContainsSubscription(thing, "SomeHandle2"))
		End Sub

		<TestMethod()> _
		Public Sub RemovingItemRemovesSubscribersAndPublishers()
			Dim workItem As WorkItem = New TestableRootWorkItem()
			Dim strat As EventBrokerStrategy = New EventBrokerStrategy()
			Dim context As MockBuilderContext = New MockBuilderContext(strat)
			Dim thing As MockEventObject = New MockEventObject()
			Dim subscriber As MockEventSubscriber = New MockEventSubscriber()
			context.Locator.Add(New DependencyResolutionLocatorKey(GetType(WorkItem), Nothing), workItem)

			strat.BuildUp(context, GetType(MockEventObject), thing, Nothing)
			strat.BuildUp(context, GetType(MockEventSubscriber), subscriber, Nothing)

			Assert.IsFalse(thing.SomeEventIsNull())
			Assert.IsFalse(thing.SomeEvent2IsNull())

			strat.TearDown(context, thing)

			Assert.IsTrue(thing.SomeEventIsNull())
			Assert.IsTrue(thing.SomeEvent2IsNull())
		End Sub

		<TestMethod()> _
		Public Sub AddedServicesGetInspectedAndRegistered()
			Dim workItem As WorkItem = New TestableRootWorkItem()
			Dim strat As EventBrokerStrategy = New EventBrokerStrategy()
			Dim context As MockBuilderContext = New MockBuilderContext(strat)
			context.Locator.Add(New DependencyResolutionLocatorKey(GetType(WorkItem), Nothing), workItem)

			Dim handle As EventTopic = workItem.EventTopics("GlobalEvent")

			Dim mock As MockService = New MockService()
			context.HeadOfChain.BuildUp(context, GetType(MockService), mock, Nothing)

			Assert.AreEqual(1, handle.SubscriptionCount, "The subscription was not registered.")
			Assert.AreEqual(1, handle.PublicationCount, "The publication was not registered.")
		End Sub

		<TestMethod()> _
		Public Sub ChildContainerGetsEventsRegistered()
			Dim handle As EventTopic = New EventTopic()

			Dim workItem As WorkItem = CreateWorkItem(handle)
			workItem.WorkItems.AddNew(Of MockWorkItem)()

			Assert.AreEqual(1, handle.SubscriptionCount, "The subscription was not registered.")
			Assert.AreEqual(1, handle.PublicationCount, "The publication was not registered.")
		End Sub

		Private Function CreateWorkItem(ByVal handle As EventTopic) As WorkItem
			Dim result As WorkItem = New TestableRootWorkItem()
			result.EventTopics.Add(handle, "GlobalEvent")
			Return result
		End Function

		Private Class MockWorkItem : Inherits WorkItem
			<EventPublication("GlobalEvent")> _
			Public Event [Event] As EventHandler

			<EventSubscription("GlobalEvent")> _
			Public Sub OnEvent(ByVal sender As Object, ByVal e As EventArgs)
			End Sub

			Public Sub FireEvent()
				If Not EventEvent Is Nothing Then
					RaiseEvent [Event](Me, EventArgs.Empty)
				End If
			End Sub
		End Class

		Public Class MockService
			<EventPublication("GlobalEvent")> _
			Public Event [Event] As EventHandler

			<EventSubscription("GlobalEvent")> _
			Public Sub OnEvent(ByVal sender As Object, ByVal e As EventArgs)
			End Sub

			Public Sub FireEvent()
				If Not EventEvent Is Nothing Then
					RaiseEvent [Event](Me, EventArgs.Empty)
				End If
			End Sub
		End Class

		Private Class MockEventSubscriber
			<EventSubscription("topic1")> _
			Public Sub topic1Handler(ByVal sender As Object, ByVal e As EventArgs)
			End Sub

			<EventSubscription("globalTopic")> _
			Public Sub globalTopicHandler(ByVal sender As Object, ByVal e As EventArgs)
			End Sub
		End Class

		Private Class MockEventObject
			<EventPublication("topic1")> _
			Public Event SomeEvent As EventHandler

			<EventPublication("globalTopic")> _
			Public Event SomeEvent2 As EventHandler

			<EventSubscription("localSubscriptionTopic")> _
			Public Sub SomeHandler(ByVal sender As Object, ByVal eh As EventArgs)
			End Sub

			<EventSubscription("globalSubscriptionTopic", Thread:=ThreadOption.Background)> _
			Public Sub SomeHandle2(ByVal sender As Object, ByVal eh As EventArgs)
			End Sub

			Public Function SomeEventIsNull() As Boolean
				Return (SomeEventEvent Is Nothing)
			End Function

			Public Function SomeEvent2IsNull() As Boolean
				Return (SomeEvent2Event Is Nothing)
			End Function

			Private Sub CompilerWarningEradicator()
				If Not SomeEventEvent Is Nothing Then
					RaiseEvent SomeEvent(Nothing, Nothing)
				End If
				If Not SomeEvent2Event Is Nothing Then
					RaiseEvent SomeEvent2(Nothing, Nothing)
				End If
			End Sub
		End Class
	End Class
End Namespace
