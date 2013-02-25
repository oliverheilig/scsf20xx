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
Imports System.ComponentModel
Imports Microsoft.Practices.CompositeUI.EventBroker
Imports Microsoft.Practices.CompositeUI.Utility
Imports System.Threading

Namespace Tests.EventBroker

    <TestClass()> _
    Public Class RunInBackgroundWorkerFixture
        Private Shared topic As EventTopic
        Private workItem As WorkItem

#If RELEASE Then
		Private Shared MillisecondsToWait As Integer = 1000
#Else
        Private Shared MillisecondsToWait As Integer = 60000
#End If

        <TestInitialize()> _
        Public Sub Setup()
            topic = New EventTopic()
            workItem = New TestableRootWorkItem()
        End Sub

        <TestMethod()> _
        Public Sub SubscriberRequiringAsyncEventArgsReceivesWorkerAndEventArgs()
            Dim subscriber As BackgroundSubscriber = New BackgroundSubscriber()
            Dim e As EventArgs = New EventArgs()
            topic.AddSubscription(subscriber, "AsyncEventHandler", workItem, ThreadOption.Background)

            topic.Fire(Me, e, workItem, PublicationScope.WorkItem)

            subscriber.StartAsyncProcessSignal.Set()
            Dim signaled As Boolean = subscriber.FinishedSignal.WaitOne(MillisecondsToWait, True)

            Assert.IsTrue(signaled)
            Assert.IsNotNull(subscriber.ReceiverEventArgs)
            Assert.AreSame(e, subscriber.ReceiverEventArgs)
        End Sub

        Private Class BackgroundSubscriber
            Public FinishedSignal As AutoResetEvent = New AutoResetEvent(False)


            Public Sub TestEventHandler(ByVal sender As Object, ByVal e As EventArgs)
            End Sub

            Public Sub TestEventHandlerAsync(ByVal sender As Object, ByVal e As EventArgs)
                StartAsyncProcessSignal.WaitOne(MillisecondsToWait, True)
            End Sub

            Public ReceiverEventArgs As EventArgs

            Public Sub AsyncEventHandler(ByVal sender As Object, ByVal e As EventArgs)
                StartAsyncProcessSignal.WaitOne(MillisecondsToWait, True)
                ReceiverEventArgs = e
                FinishedSignal.Set()
            End Sub

            Public ResultingException As Exception = Nothing
            Public StartAsyncProcessSignal As AutoResetEvent = New AutoResetEvent(False)
            Public Sub FailingAsyncEventHandler(ByVal sender As Object, ByVal e As EventArgs)
                StartAsyncProcessSignal.WaitOne(MillisecondsToWait, True)
                ResultingException = New Exception("FailingAsyncEventHandler")
                Throw ResultingException
            End Sub
        End Class
    End Class
End Namespace
