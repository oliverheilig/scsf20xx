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
Imports System.Reflection
Imports Microsoft.Practices.CompositeUI.Utility

Namespace EventBroker
	''' <summary>
	''' Inspects objects for event publications and subscriptions,
	''' registering and unregistering the publishers and subscribers with the corresponding <see cref="EventTopic"/>.
	''' </summary>
	Public Class EventInspector

		Private Sub New()
		End Sub

		''' <summary>
		''' Processes an object by scanning its members and registering publications 
		''' and subscriptions.
		''' </summary>
		''' <param name="item">The object to scan.</param>
		''' <param name="workItem">The <see cref="WorkItem"/> where the object is.</param>
		Public Shared Sub Register(ByVal item As Object, ByVal workItem As WorkItem)
			Guard.ArgumentNotNull(item, "item")
			Guard.ArgumentNotNull(workItem, "workItem")

			ProcessPublishers(item, item.GetType(), workItem, True)
			ProcessSubscribers(item, item.GetType(), workItem, True)
		End Sub

		''' <summary>
		''' Processes an object by scanning its members and unregistering publications 
		''' and subscriptions.
		''' </summary>
		''' <param name="item">The object to scan.</param>
		''' <param name="workItem">The <see cref="WorkItem"/> where the object is.</param>
		Public Shared Sub Unregister(ByVal item As Object, ByVal workItem As WorkItem)
			Guard.ArgumentNotNull(item, "item")
			Guard.ArgumentNotNull(workItem, "workItem")

			ProcessPublishers(item, item.GetType(), workItem, False)
			ProcessSubscribers(item, item.GetType(), workItem, False)
		End Sub

		Private Shared Sub ProcessPublishers(ByVal item As Object, ByVal itemType As Type, ByVal workItem As WorkItem, ByVal registerPublisher As Boolean)
			For Each info As EventInfo In itemType.GetEvents()
				For Each attr As EventPublicationAttribute In info.GetCustomAttributes(GetType(EventPublicationAttribute), True)
					HandlePublisher(item, registerPublisher, info, attr, workItem)
				Next attr
			Next info
		End Sub

		Private Shared Sub HandlePublisher(ByVal item As Object, ByVal registerPublisher As Boolean, ByVal info As EventInfo, ByVal attr As EventPublicationAttribute, ByVal workItem As WorkItem)
			Dim topic As EventTopic = workItem.EventTopics(attr.Topic)

			If registerPublisher Then
				topic.AddPublication(item, info.Name, workItem, attr.Scope)
			Else
				topic.RemovePublication(item, info.Name)
			End If
		End Sub

		Private Shared Sub ProcessSubscribers(ByVal item As Object, ByVal itemType As Type, ByVal workItem As WorkItem, ByVal registerSubscriber As Boolean)
			For Each info As MethodInfo In itemType.GetMethods()
				For Each attr As EventSubscriptionAttribute In info.GetCustomAttributes(GetType(EventSubscriptionAttribute), True)
					HandleSubscriber(item, registerSubscriber, info, attr, workItem)
				Next attr
			Next info
		End Sub

		Private Shared Sub HandleSubscriber(ByVal item As Object, ByVal registerSubscriber As Boolean, ByVal info As MethodInfo, ByVal attr As EventSubscriptionAttribute, ByVal workItem As WorkItem)
			Dim topic As EventTopic = workItem.EventTopics(attr.Topic)

			If registerSubscriber = True Then
				Dim paramTypes As Type() = GetParamTypes(info)

				topic.AddSubscription(item, info.Name, paramTypes, workItem, attr.Thread)
			Else
				topic.RemoveSubscription(item, info.Name)
			End If
		End Sub

		Private Shared Function GetParamTypes(ByVal info As MethodInfo) As Type()
			Dim paramInfos As ParameterInfo() = info.GetParameters()
			Dim paramTypes As Type() = New Type(paramInfos.Length - 1) {}
			Dim i As Integer = 0
			Do While i < paramTypes.Length
				paramTypes(i) = paramInfos(i).ParameterType
				i += 1
			Loop
			Return paramTypes
		End Function

	End Class
End Namespace