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
Imports Microsoft.Practices.CompositeUI.Utility
Imports System.Globalization

Namespace Commands
	''' <summary>
	''' A <see cref="CommandAdapter"/> that fires a .NET event when the command is fired.
	''' </summary>
	''' <typeparam name="TInvoker">Type of the invoker(s)</typeparam>
	''' <remarks>This adapter allows a set of invokers of a given type TIvoker
	''' to fire a single <see cref="Command"/>.</remarks>
	Public Class EventCommandAdapter(Of TInvoker)
		Inherits CommandAdapter

		Private innerInvokers As ListDictionary(Of TInvoker, String) = New ListDictionary(Of TInvoker, String)()

		''' <summary>
		''' Initializes a new instance of the <see cref="EventCommandAdapter{TInvoker}"/> class
		''' </summary>
		Public Sub New()
		End Sub

		''' <summary>
		''' Initializes a new instance and wires the specified invoker event to this adapter.
		''' </summary>
		''' <param name="invoker">The invoker that is to be adapted.</param>
		''' <param name="eventName">The event the adapter will listen on the invoker.</param>
		Public Sub New(ByVal invoker As TInvoker, ByVal eventName As String)
			WireUpInvoker(invoker, eventName)
		End Sub

		''' <summary>
		''' Gets the list of invokers in the adapter.
		''' </summary>
		Public ReadOnly Property Invokers() As ReadOnlyDictionary(Of TInvoker, List(Of String))
			Get
				Return New ReadOnlyDictionary(Of TInvoker, List(Of String))(innerInvokers)
			End Get
		End Property

		''' <summary>
		''' The handler for the invokers events.
		''' </summary>
		Public Sub InvokerEventHandler(ByVal sender As Object, ByVal e As EventArgs)
			FireCommand()
		End Sub

		''' <summary>
		''' Adds an invoker with the specified eventName.
		''' </summary>
		''' <param name="invoker">The invoker object to add.</param>
		''' <param name="eventName">The event on the invoker the adapter will listen to.</param>
		Public Overrides Sub AddInvoker(ByVal invoker As Object, ByVal eventName As String)
			Guard.ArgumentNotNull(invoker, "invoker")
			ThrowIfWrongType(invoker)
			WireUpInvoker(CType(invoker, TInvoker), eventName)
		End Sub

		''' <summary>
		''' Removes the invoker from the adapter.
		''' </summary>
		''' <param name="invoker">The invoker object to remove.</param>
		''' <param name="eventName">The name of the event on the invoker the adapter is listening to.</param>
		Public Overrides Sub RemoveInvoker(ByVal invoker As Object, ByVal eventName As String)
			UnwireInvoker(invoker, eventName)
			RemoveInvokerIfZeroEvents(invoker)
		End Sub

		''' <summary>
		''' Overriden to unhook and dispose all the invokers.
		''' </summary>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing Then
				For Each pair As KeyValuePair(Of TInvoker, List(Of String)) In innerInvokers
					Dim events As List(Of String) = pair.Value
					For i As Integer = events.Count - 1 To 0 Step -1
						UnwireInvoker(pair.Key, events(i))
					Next i
				Next pair

				innerInvokers.Clear()
			End If
			'MyBase.Dispose(disposing)
		End Sub

		''' <summary>
		''' Checks if the adapter contains the invoker.
		''' </summary>
		''' <param name="invoker">The invoker to check for</param>
		''' <returns>True if the adapter contains the invoker; otherwise False.</returns>
		Public Overrides Function ContainsInvoker(ByVal invoker As Object) As Boolean
			If GetType(TInvoker).IsAssignableFrom(invoker.GetType()) = False Then
				Return False
			Else
				Return innerInvokers.ContainsKey(CType(invoker, TInvoker))
			End If
		End Function

		''' <summary>
		''' Gets the count of invokers in the adapter
		''' </summary>
		Public Overrides ReadOnly Property InvokerCount() As Integer
			Get
				Return innerInvokers.Count
			End Get
		End Property

		Private Sub WireUpInvoker(ByVal invoker As TInvoker, ByVal eventName As String)
			Guard.ArgumentNotNull(invoker, "invoker")
			Guard.ArgumentNotNull(eventName, "evenName")
			ThrowIfWrongType(invoker)

			If innerInvokers.ContainsKey(invoker) = False Then
				HookInvokerEvent(invoker, eventName)
				innerInvokers.Add(invoker, eventName)
			ElseIf innerInvokers(invoker).Contains(eventName) = False Then
				innerInvokers(invoker).Add(eventName)
				HookInvokerEvent(invoker, eventName)
			End If
		End Sub

		Private Sub UnwireInvoker(ByVal invoker As Object, ByVal eventName As String)
			Guard.ArgumentNotNull(invoker, "invoker")
			Guard.ArgumentNotNullOrEmptyString(eventName, "eventName")
			ThrowIfWrongType(invoker)

			Dim typedInvoker As TInvoker = CType(invoker, TInvoker)
			If innerInvokers.ContainsKey(typedInvoker) = True Then
				UnhookInvokerEvent(invoker, eventName)
				innerInvokers(typedInvoker).Remove(eventName)
			End If
		End Sub

		Private Sub RemoveInvokerIfZeroEvents(ByVal invoker As Object)
			Guard.ArgumentNotNull(invoker, "invoker")
			ThrowIfWrongType(invoker)

			Dim typedInvoker As TInvoker = CType(invoker, TInvoker)
			If innerInvokers.ContainsKey(typedInvoker) AndAlso innerInvokers(typedInvoker).Count = 0 Then
				innerInvokers.Remove(typedInvoker)
			End If
		End Sub

		Private Sub HookInvokerEvent(ByVal invoker As Object, ByVal eventName As String)
			Dim eventInfo As EventInfo = GetEventInfo(invoker, eventName)
			Dim handlerDelegate As System.Delegate = System.Delegate.CreateDelegate(eventInfo.EventHandlerType, Me, InvokerEventHandle)
			eventInfo.AddEventHandler(invoker, handlerDelegate)
		End Sub

		Private Sub UnhookInvokerEvent(ByVal invoker As Object, ByVal eventName As String)
			Dim eventInfo As EventInfo = GetEventInfo(invoker, eventName)
			Dim handlerDelegate As System.Delegate = System.Delegate.CreateDelegate(eventInfo.EventHandlerType, Me, InvokerEventHandle)
			eventInfo.RemoveEventHandler(invoker, handlerDelegate)
		End Sub


		Private Function GetEventInfo(ByVal invoker As Object, ByVal eventName As String) As EventInfo
			Dim invokerType As Type = invoker.GetType()
			Dim eventInfo As EventInfo = invokerType.GetEvent(eventName, BindingFlags.Instance Or BindingFlags.Static Or BindingFlags.Public)

			If eventInfo Is Nothing Then
				Throw New CommandException(String.Format(CultureInfo.CurrentCulture, My.Resources.EventNotPresentOnInvoker, eventName, invokerType.FullName))
			End If
			If eventInfo.GetAddMethod().IsStatic OrElse eventInfo.GetRemoveMethod().IsStatic Then
				Throw New CommandException(String.Format(CultureInfo.CurrentCulture, My.Resources.StaticCommandPublisherNotSupported, eventInfo.Name))
			End If
			Return eventInfo
		End Function


		Private ReadOnly Property InvokerEventHandle() As MethodInfo
			Get
				Return Me.GetType().GetMethod("InvokerEventHandler", BindingFlags.Instance Or BindingFlags.Public)
			End Get
		End Property

		Private Sub ThrowIfWrongType(ByVal invoker As Object)
			If GetType(TInvoker).IsAssignableFrom(invoker.GetType()) = False Then
				Throw New ArgumentException(String.Format(CultureInfo.CurrentCulture, My.Resources.EventCommandAdapterInvokerNotType, GetType(TInvoker)))
			End If
		End Sub
	End Class
End Namespace
