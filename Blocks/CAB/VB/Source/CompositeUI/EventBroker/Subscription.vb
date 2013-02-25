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
Imports System.Reflection
Imports System.ComponentModel
Imports System.Threading
Imports Microsoft.Practices.CompositeUI.Utility
Imports System.Globalization
Imports System.Diagnostics

Namespace EventBroker
	''' <summary>
	''' Represents a topic subscription.
	''' </summary>
	Friend Class Subscription
		Private wrSubscriber As WeakReference
		Private innerHandlerMethodName As String
		Private methodHandle As RuntimeMethodHandle
		Private innerThreadOption As ThreadOption
		Private syncContext As SynchronizationContext
		Private workItemSubscriptions As WorkItemSubscriptions
		Private handlerEventArgsType As Type = Nothing
		Private typeHandle As RuntimeTypeHandle

		''' <summary>
		''' Initializes a new instance of the <see cref="Subscription"/> class.
		''' </summary>
		Friend Sub New(ByVal workItemSubscriptions As WorkItemSubscriptions, ByVal subscriber As Object, ByVal handlerMethodName As String, ByVal threadOption As ThreadOption)
			Me.New(workItemSubscriptions, subscriber, handlerMethodName, Nothing, threadOption)
		End Sub

		''' <summary>
		''' Initializes a new instance of the <see cref="Subscription"/> class
		''' </summary>
		Friend Sub New(ByVal aWorkItemSubscriptions As WorkItemSubscriptions, ByVal aSubscriber As Object, ByVal aHandlerMethodName As String, ByVal parameterTypes As Type(), ByVal aThreadOption As ThreadOption)
			Me.wrSubscriber = New WeakReference(aSubscriber)
			Me.innerHandlerMethodName = aHandlerMethodName
			Me.innerThreadOption = aThreadOption
			Me.workItemSubscriptions = aWorkItemSubscriptions

			Dim miHandler As MethodInfo = GetMethodInfo(aSubscriber, aHandlerMethodName, parameterTypes)
			If miHandler Is Nothing Then
				Throw New EventBrokerException(String.Format(CultureInfo.CurrentCulture, My.Resources.SubscriberHandlerNotFound, aHandlerMethodName, aSubscriber.GetType().ToString()))
			ElseIf miHandler.IsStatic Then
				Throw New EventBrokerException(String.Format(CultureInfo.CurrentCulture, My.Resources.CannotRegisterStaticSubscriptionMethods, miHandler.DeclaringType.FullName, miHandler.Name))

			End If

			Me.typeHandle = aSubscriber.GetType().TypeHandle
			Me.methodHandle = miHandler.MethodHandle
			Dim parameters As ParameterInfo() = miHandler.GetParameters()
			If IsValidEventHandler(parameters) Then
				Dim pInfo As ParameterInfo = miHandler.GetParameters()(1)
				Dim pType As Type = pInfo.ParameterType
				handlerEventArgsType = GetType(EventHandler(Of )).MakeGenericType(pType)
			Else
				Throw New EventBrokerException(String.Format(CultureInfo.CurrentCulture, My.Resources.InvalidSubscriptionSignature, miHandler.DeclaringType.FullName, miHandler.Name))
			End If

			If aThreadOption = ThreadOption.UserInterface Then
				' If there's a syncronization context (i.e. the WindowsFormsSynchronizationContext 
				' created to marshal back to the thread where a control was initially created 
				' in a particular thread), capture it to marshal back to it through the 
				' context, that basically goes through a Post/Send.
				If Not SynchronizationContext.Current Is Nothing Then
					syncContext = SynchronizationContext.Current
				End If
			End If
		End Sub

		Private Shared Function IsValidEventHandler(ByVal parameters As ParameterInfo()) As Boolean
			Return parameters.Length = 2 AndAlso GetType(EventArgs).IsAssignableFrom(parameters(1).ParameterType)
		End Function

		Private Function GetMethodInfo(ByVal aSubscriber As Object, ByVal aHandlerMethodName As String, ByVal parameterTypes As Type()) As MethodInfo
			If Not parameterTypes Is Nothing Then
				Return aSubscriber.GetType().GetMethod(aHandlerMethodName, parameterTypes)
			Else
				Return aSubscriber.GetType().GetMethod(aHandlerMethodName)
			End If
		End Function

		''' <summary>
		''' The subscriber of the event.
		''' </summary>
		Public ReadOnly Property Subscriber() As Object
			Get
				Return wrSubscriber.Target
			End Get
		End Property

		''' <summary>
		''' The handler method name that's subscribed to the event.
		''' </summary>
		Public ReadOnly Property HandlerMethodName() As String
			Get
				Return innerHandlerMethodName
			End Get
		End Property

		''' <summary>
		''' The callback thread option for the event.
		''' </summary>
		Public ReadOnly Property ThreadOption() As ThreadOption
			Get
				Return innerThreadOption
			End Get
		End Property

		Friend Function GetHandler() As EventTopicFireDelegate
			Return AddressOf EventTopicFireHandler
		End Function

		Private Sub EventTopicFireHandler(ByVal sender As Object, ByVal e As EventArgs, ByVal exceptions As List(Of Exception), ByVal traceSource As TraceSource)
			Dim targetSubscriber As Object = wrSubscriber.Target
			If targetSubscriber Is Nothing Then
				Return
			End If
			Select Case innerThreadOption
				Case ThreadOption.Publisher
					CallOnPublisher(sender, e, exceptions)
				Case ThreadOption.Background
					CallOnBackgroundWorker(sender, e, traceSource)
				Case ThreadOption.UserInterface
					CallOnUserInterface(sender, e, exceptions)
				Case Else
			End Select
		End Sub

		Private Sub CallOnPublisher(ByVal sender As Object, ByVal e As EventArgs, ByVal exceptions As List(Of Exception))
			Try
				Dim handler As System.Delegate = CreateSubscriptionDelegate()
				If Not handler Is Nothing Then
					handler.DynamicInvoke(sender, e)
				End If
			Catch ex As TargetInvocationException
				exceptions.Add(ex.InnerException)
			End Try
		End Sub

#Region "Utility code for the subprocedure CallOnBackgroundWorker"
		Private Class WorkItemWorker
			Public Sub DoWork(ByVal state As Object)
				Dim args As CallInBackgroundArguments = CType(state, CallInBackgroundArguments)
				Try
					args.Handler.DynamicInvoke(args.Sender, args.EventArgs)
				Catch ex As Exception
					args.TraceSource.TraceInformation(My.Resources.BackgroundSubscriberException, ex.ToString())
					Throw
				End Try
			End Sub
		End Class
#End Region

		Private Sub CallOnBackgroundWorker(ByVal sender As Object, ByVal e As EventArgs, ByVal traceSource As TraceSource)
			Dim handler As System.Delegate = CreateSubscriptionDelegate()
			If Not handler Is Nothing Then
				Dim worker As WorkItemWorker = New WorkItemWorker
				ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf worker.DoWork), New CallInBackgroundArguments(sender, e, handler, traceSource))
			End If
		End Sub

#Region "Utility code for the subprocedure CallOnUserInterface"
		Private Class SubscriptionCallback
			Private sender As Object
			Private e As EventArgs
			Private exceptions As List(Of Exception)

			Public Sub New(ByRef sender As Object, ByRef e As EventArgs, ByRef exceptions As List(Of Exception))
				Me.sender = sender
				Me.e = e
				Me.exceptions = exceptions
			End Sub
			Public Sub Callback(ByVal data As Object)
				Try
					CType(data, [Delegate]).DynamicInvoke(sender, e)
				Catch ex As TargetInvocationException
					exceptions.Add(ex.InnerException)
				End Try
			End Sub
		End Class
#End Region

		Private Sub CallOnUserInterface(ByVal sender As Object, ByVal e As EventArgs, ByVal exceptions As List(Of Exception))
			Dim handler As System.Delegate = CreateSubscriptionDelegate()
			If Not handler Is Nothing Then
				If Not syncContext Is Nothing Then
					Dim subscriptionCallback As SubscriptionCallback = New SubscriptionCallback(sender, e, exceptions)
					syncContext.Send(AddressOf subscriptionCallback.Callback, handler)
				Else
					Try
						handler.DynamicInvoke(sender, e)
					Catch ex As TargetInvocationException
						exceptions.Add(ex.InnerException)
					End Try
				End If
			End If
		End Sub

		Private Function CreateSubscriptionDelegate() As System.Delegate
			Dim aSubscriber As Object = wrSubscriber.Target
			If Not aSubscriber Is Nothing Then
				Return System.Delegate.CreateDelegate(Me.handlerEventArgsType, aSubscriber, CType(MethodInfo.GetMethodFromHandle(methodHandle, typeHandle), MethodInfo))
			End If
			Return Nothing
		End Function

		Private Structure CallInBackgroundArguments
			Public Handler As System.Delegate
			Public Sender As Object
			Public EventArgs As EventArgs
			Public TraceSource As TraceSource

			Public Sub New(ByVal aSender As Object, ByVal anEventArgs As EventArgs, ByVal aHandler As System.Delegate, ByVal aTraceSource As TraceSource)
				Sender = aSender
				EventArgs = anEventArgs
				Handler = aHandler
				TraceSource = aTraceSource
			End Sub
		End Structure
	End Class
End Namespace
