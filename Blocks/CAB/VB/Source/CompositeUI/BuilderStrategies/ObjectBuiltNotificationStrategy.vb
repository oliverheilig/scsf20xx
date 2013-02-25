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
Imports Microsoft.Practices.ObjectBuilder

Namespace BuilderStrategies
	''' <summary>
	''' An ObjectBuilder strategy which notifies CAB when an object is fully built so that the
	''' <see cref="WorkItem"/> can be notified.
	''' </summary>
	Public Class ObjectBuiltNotificationStrategy : Inherits BuilderStrategy
		Private policy As ObjectBuiltNotificationPolicy

		''' <summary>
		''' See <see cref="IBuilderStrategy.BuildUp"/> for more information.
		''' </summary>
		Public Overrides Function BuildUp(ByVal context As IBuilderContext, ByVal typeToBuild As Type, ByVal existing As Object, ByVal idToBuild As String) As Object
			Dim workItem As WorkItem = context.Locator.Get(Of WorkItem)(New DependencyResolutionLocatorKey(GetType(WorkItem), Nothing))
			Dim notification As ObjectBuiltNotificationPolicy.ItemNotification

			If policy Is Nothing Then
				policy = context.Policies.Get(Of ObjectBuiltNotificationPolicy)(Nothing, Nothing)
			End If

			notification = Nothing
			If Not workItem Is Nothing AndAlso (Not Object.ReferenceEquals(workItem, existing)) AndAlso policy.AddedDelegates.TryGetValue(workItem, notification) Then
				notification(existing)
			End If

			Return MyBase.BuildUp(context, typeToBuild, existing, idToBuild)
		End Function

		''' <summary>
		''' See <see cref="IBuilderStrategy.TearDown"/> for more information.
		''' </summary>
		Public Overrides Function TearDown(ByVal context As IBuilderContext, ByVal item As Object) As Object
			Dim workItem As WorkItem = context.Locator.Get(Of WorkItem)(New DependencyResolutionLocatorKey(GetType(WorkItem), Nothing))
			Dim notification As ObjectBuiltNotificationPolicy.ItemNotification

			notification = Nothing
			If Not workItem Is Nothing AndAlso policy.RemovedDelegates.TryGetValue(workItem, notification) Then
				notification(item)
			End If

			Return MyBase.TearDown(context, item)
		End Function
	End Class
End Namespace
