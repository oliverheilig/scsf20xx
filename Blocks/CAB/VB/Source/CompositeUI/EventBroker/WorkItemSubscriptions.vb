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
Imports System.Collections

Namespace EventBroker
	''' <summary>
	''' Maintains the subscriptions registered from a <see cref="WorkItem"/>
	''' </summary>
	Friend Class WorkItemSubscriptions : Implements IEnumerable(Of Subscription)
		Private wrWorkItem As WeakReference
		Private subscriptions As List(Of Subscription) = New List(Of Subscription)()
		Private lockObject As Object = New Object()

		Public Sub New(ByVal aWorkItem As WorkItem)
			Me.wrWorkItem = New WeakReference(aWorkItem)
		End Sub

		Public Sub AddSubscription(ByVal subscriber As Object, ByVal handlerMethodName As String, ByVal threadOption As ThreadOption)
			AddSubscription(subscriber, handlerMethodName, Nothing, threadOption)
		End Sub

		Public Sub AddSubscription(ByVal subscriber As Object, ByVal handlerMethodName As String, ByVal parameterTypes As Type(), ByVal threadOption As ThreadOption)
			SyncLock lockObject
				If FindSubscription(subscriber, handlerMethodName) Is Nothing Then
					subscriptions.Add(New Subscription(Me, subscriber, handlerMethodName, parameterTypes, threadOption))
				End If
			End SyncLock
		End Sub

		Public Sub RemoveSubscription(ByVal subscriber As Object, ByVal handlerMethodName As String)
			SyncLock lockObject
				Dim subscription As Subscription = FindSubscription(subscriber, handlerMethodName)
				If Not subscription Is Nothing Then
					RemoveSubscription(subscription)
				End If
			End SyncLock
		End Sub

		Public Sub RemoveSubscription(ByVal subscription As Subscription)
			SyncLock lockObject
				subscriptions.Remove(subscription)
			End SyncLock
		End Sub

		Public ReadOnly Property WorkItem() As WorkItem
			Get
				Return CType(wrWorkItem.Target, WorkItem)
			End Get
		End Property

		Public Function GetHandlers() As EventTopicFireDelegate()
			Dim result As List(Of EventTopicFireDelegate) = New List(Of EventTopicFireDelegate)()

			SyncLock lockObject
				For Each subscription As Subscription In subscriptions.ToArray()
					Dim handler As EventTopicFireDelegate = subscription.GetHandler()
					If Not handler Is Nothing Then
						result.Add(handler)
					End If
				Next subscription
				Return result.ToArray()
			End SyncLock
		End Function

		Public ReadOnly Property SubscriptionCount() As Integer
			Get
				Return subscriptions.Count
			End Get
		End Property

#Region "Utility code for the function FindSubscription"
		Private Class SubscriptionHandler
			Private subscriber As Object
			Private handlerMethodName As String
			Public Sub New(ByRef subscriber As Object, ByRef handlerMethodName As String)
				Me.subscriber = subscriber
				Me.handlerMethodName = handlerMethodName
			End Sub
			Public Function Compare(ByVal match As Subscription) As Boolean
				Return match.Subscriber Is subscriber AndAlso match.HandlerMethodName = handlerMethodName
			End Function
		End Class
#End Region

		Public Function FindSubscription(ByVal subscriber As Object, ByVal handlerMethodName As String) As Subscription
			SyncLock lockObject
				Clean()
				Dim subscriptionHandler As SubscriptionHandler = New SubscriptionHandler(subscriber, handlerMethodName)
				Return subscriptions.Find(AddressOf subscriptionHandler.Compare)
			End SyncLock
		End Function

		Public Sub Clean()
			SyncLock lockObject
				If WorkItem Is Nothing Or (Not WorkItem Is Nothing And WorkItem.Status = WorkItemStatus.Terminated) Then
					subscriptions.Clear()
				Else

					For Each subscription As Subscription In subscriptions.ToArray()
						If subscription.Subscriber Is Nothing Then
							RemoveSubscription(subscription)
						End If
					Next subscription
				End If
			End SyncLock
		End Sub

		Public Function GetEnumerator() As IEnumerator(Of Subscription) Implements IEnumerable(Of Subscription).GetEnumerator
			Return subscriptions.GetEnumerator()
		End Function

		Private Function GetEnumeratorBase() As IEnumerator Implements IEnumerable.GetEnumerator
			Return GetEnumerator()
		End Function
	End Class
End Namespace
