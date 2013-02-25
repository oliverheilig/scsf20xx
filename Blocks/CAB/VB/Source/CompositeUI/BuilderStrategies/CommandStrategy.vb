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
Imports Microsoft.Practices.CompositeUI.Commands
Imports Microsoft.Practices.ObjectBuilder
Imports System.Globalization

Namespace BuilderStrategies
	''' <summary>
	''' A <see cref="BuilderStrategy"/> that processes commands associated 
	''' with component members through the <see cref="CommandHandlerAttribute"/>.
	''' </summary>
	Public Class CommandStrategy : Inherits BuilderStrategy
		''' <summary>
		''' Inspects the object for command handler declarations registering them with the 
		''' corresponding <see cref="Command"/>.
		''' </summary>
		Public Overrides Function BuildUp(ByVal context As IBuilderContext, ByVal t As Type, ByVal existing As Object, ByVal id As String) As Object
			Dim workItem As WorkItem = GetWorkItem(context, existing)

			If Not workItem Is Nothing Then
				Dim targetType As Type = existing.GetType()

				For Each methodInfo As MethodInfo In targetType.GetMethods()
					RegisterCommandHandlers(context, workItem, existing, id, methodInfo)
				Next methodInfo
			End If

			Return MyBase.BuildUp(context, t, existing, id)
		End Function

		''' <summary>
		''' Inspects the object for command handler declarations unregistering them from the 
		''' corresponding command.
		''' </summary>
		Public Overrides Function TearDown(ByVal context As IBuilderContext, ByVal item As Object) As Object
			Dim workItem As WorkItem = GetWorkItem(context, item)

			If Not workItem Is Nothing Then
				Dim targetType As Type = item.GetType()

				For Each methodInfo As MethodInfo In targetType.GetMethods()
					UnregisterCommandHandlers(context, workItem, item, methodInfo)
				Next methodInfo
			End If

			Return MyBase.TearDown(context, item)
		End Function

		Private Sub RegisterCommandHandlers(ByVal context As IBuilderContext, ByVal workItem As WorkItem, ByVal target As Object, ByVal targetID As String, ByVal methodInfo As MethodInfo)
			For Each attr As CommandHandlerAttribute In methodInfo.GetCustomAttributes(GetType(CommandHandlerAttribute), True)
				Dim cmd As Command = workItem.Commands(attr.CommandName)

				If (Not cmd.IsHandlerRegistered(target, methodInfo)) Then
					AddHandler cmd.ExecuteAction, CreateCommandHandler(target, methodInfo)
					TraceBuildUp(context, target.GetType(), targetID, My.Resources.CommandInjectionBuildUp, methodInfo.Name, attr.CommandName)
				End If
			Next attr
		End Sub

		Private Sub UnregisterCommandHandlers(ByVal context As IBuilderContext, ByVal workItem As WorkItem, ByVal target As Object, ByVal methodInfo As MethodInfo)
			For Each attr As CommandHandlerAttribute In methodInfo.GetCustomAttributes(GetType(CommandHandlerAttribute), True)
				If workItem.Commands.Contains(attr.CommandName) Then
					RemoveHandler workItem.Commands(attr.CommandName).ExecuteAction, CreateCommandHandler(target, methodInfo)
					TraceTearDown(context, target, My.Resources.CommandInjectionTearDown, methodInfo.Name, attr.CommandName)
				End If
			Next attr
		End Sub

		Private Function CreateCommandHandler(ByVal target As Object, ByVal methodInfo As MethodInfo) As EventHandler
			Dim handler As System.Delegate = Nothing

			If methodInfo.IsStatic = False Then
				handler = System.Delegate.CreateDelegate(GetType(EventHandler), target, CType(methodInfo, MethodInfo))
			Else
				Throw New CommandException(String.Format(CultureInfo.CurrentCulture, My.Resources.StaticCommandHandlerNotSupported, methodInfo.Name))
			End If

			Return CType(handler, EventHandler)
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