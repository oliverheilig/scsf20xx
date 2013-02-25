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
Imports Microsoft.Practices.CompositeUI.Commands

Namespace Tests.Commands
	<TestClass()> _
	Public Class EventTopicCommandFixture
		Private Shared workItem As WorkItem
		Private Shared topic As MockTopic

		<TestInitialize()> _
		Public Sub Setup()
			workItem = New TestableRootWorkItem()
			topic = New MockTopic()
			workItem.EventTopics.Add(topic, topic.Name)
		End Sub


		<TestMethod()> _
		Public Sub CommandExecutionFiresEventTopic()
			topic = workItem.EventTopics.AddNew(Of MockTopic)("topic://EventTopicCommand/Test")
			Dim cmd As EventTopicCommand = workItem.Commands.AddNew(Of EventTopicCommand)("Test")
			cmd.Execute()

			Assert.IsTrue(topic.FireCalled)
		End Sub


		Private Class MockTopic : Inherits EventTopic
			Public FireCalled As Boolean = False

			Public Sub New()
				MyBase.New()
			End Sub

			Public Overrides Sub Fire(ByVal sender As Object, ByVal e As EventArgs, ByVal workItem As WorkItem, ByVal scope As PublicationScope)
				FireCalled = True
			End Sub
		End Class
	End Class
End Namespace
