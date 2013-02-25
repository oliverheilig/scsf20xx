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
Imports Microsoft.Practices.CompositeUI.EventBroker
Imports Microsoft.Practices.ObjectBuilder

Namespace BuilderStrategies
	''' <summary>
	''' A <see cref="BuilderStrategy"/>processes <see cref="EventTopic"/> publications and subscriptions
	''' declared in a component inspecting the <see cref="EventPublicationAttribute"/> and 
	''' <see cref="EventSubscriptionAttribute"/> attributes.
	''' </summary>
	Public Class EventBrokerStrategy : Inherits BuilderStrategy
		''' <summary>
		''' Forwards the <see cref="EventTopic"/> related attributes processing to the <see cref="EventInspector"/>
		''' for registering publishers and/or subscribers.
		''' </summary>
		Public Overrides Function BuildUp(ByVal context As IBuilderContext, ByVal t As Type, ByVal existing As Object, ByVal id As String) As Object
			Dim workItem As WorkItem = GetWorkItem(context, existing)

			If Not workItem Is Nothing Then
				EventInspector.Register(existing, workItem)
			End If

			Return MyBase.BuildUp(context, t, existing, id)
		End Function

		''' <summary>
		''' Forwards the <see cref="EventTopic"/> related attributes processing to the <see cref="EventInspector"/>
		''' for unregistering publishers and/or subscribers.
		''' </summary>
		Public Overrides Function TearDown(ByVal context As IBuilderContext, ByVal item As Object) As Object
			Dim workItem As WorkItem = GetWorkItem(context, item)

			If Not workItem Is Nothing Then
				EventInspector.Unregister(item, workItem)
			End If

			Return MyBase.TearDown(context, item)
		End Function

		Private Function GetWorkItem(ByVal context As IBuilderContext, ByVal item As Object) As WorkItem
			Dim wi As WorkItem = TryCast(item, WorkItem)

			If Not wi Is Nothing Then
				Return wi
			End If

			Return context.Locator.Get(Of WorkItem)(New DependencyResolutionLocatorKey(GetType(WorkItem), Nothing))
		End Function
	End Class
End Namespace