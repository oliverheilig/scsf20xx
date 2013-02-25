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
	Public Class WorkItemEventTopicCollectionFixture
		Private Shared root As WorkItem

		<TestInitialize()> _
		Public Sub Setup()
			root = New TestableRootWorkItem()
		End Sub


		<TestMethod()> _
		Public Sub CollectionIsEmpty()
			Assert.AreEqual(0, root.EventTopics.Count)
		End Sub


		<TestMethod()> _
		Public Sub CanAddEventTopic()
			Dim topic As EventTopic = root.EventTopics.AddNew(Of EventTopic)("test")

			Assert.AreEqual(1, root.EventTopics.Count)
			Assert.AreSame(topic, root.EventTopics.Get("test"))
		End Sub

		<TestMethod()> _
		Public Sub TopicAddedToChildIsAccessibleInParent()
			Dim child As WorkItem = root.WorkItems.AddNew(Of WorkItem)()

			Dim topic As EventTopic = child.EventTopics.AddNew(Of EventTopic)("test")

			Assert.AreEqual(1, root.EventTopics.Count)
			Assert.AreSame(topic, root.EventTopics.Get("test"))
		End Sub

	End Class
End Namespace
