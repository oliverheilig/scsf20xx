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
Imports System.Collections.ObjectModel
Imports Microsoft.Practices.CompositeUI.Utility
Imports System.Globalization
Imports System.Reflection
Imports System.Diagnostics
Imports Microsoft.Practices.ObjectBuilder

Namespace Commands
	''' <summary>
	''' Represents a command that can be fired by several UI Elements.
	''' </summary>
	Public Class Command : Implements IDisposable, IBuilderAware
		Private innerTraceSource As TraceSource
		Private innerStatus As CommandStatus = CommandStatus.Enabled
		Private innerAdapters As List(Of CommandAdapter) = New List(Of CommandAdapter)()
		Private ownedAdapters As List(Of CommandAdapter) = New List(Of CommandAdapter)()
		Private innerName As String
		Private innerMapService As ICommandAdapterMapService

		''' <summary>
		''' This service is used by <see cref="Command"/> to create adapters for its invokers.
		''' </summary>
		<ServiceDependencyAttribute()> _
		Public WriteOnly Property MapService() As ICommandAdapterMapService
			Set(ByVal value As ICommandAdapterMapService)
				innerMapService = value
			End Set
		End Property

		''' <summary>
		''' Used by injection to set the <see cref="TraceSource"/> to use for tracing.
		''' </summary>
		<ClassNameTraceSourceAttribute()> _
		Public Property TraceSource() As TraceSource
			Set(ByVal value As TraceSource)
				innerTraceSource = value
			End Set
			Protected Get
				Return innerTraceSource
			End Get
		End Property

		''' <summary>
		''' This event signals that some of the command's properties have changed.
		''' </summary>
		Public Event Changed As EventHandler

		''' <summary>
		''' This event signals that this <see cref="Command"/> is executed. Handle this event to implement the
		''' actions to be executed when the command is fired.
		''' </summary>
		Public Event ExecuteAction As EventHandler

		''' <summary>
		''' Initializes a new instance of the <see cref="Command"/> class.
		''' </summary>
		Public Sub New()

		End Sub

		''' <summary>
		''' Gets the name of the command.
		''' </summary>
		Public ReadOnly Property Name() As String
			Get
				Return innerName
			End Get
		End Property

		''' <summary>
		''' Gets or sets the status of the <see cref="Command"/>, which controls the 
		''' visible/enabled behavior of both the <see cref="Command"/> and 
		''' its associated UI elements.
		''' </summary>
		Public Property Status() As CommandStatus
			Get
				Return Me.innerStatus
			End Get
			Set(ByVal value As CommandStatus)
				Dim oldValue As CommandStatus = Me.innerStatus
				Me.innerStatus = Value
				If oldValue <> Value Then
					OnChanged()
					If Not innerTraceSource Is Nothing Then
						innerTraceSource.TraceInformation(My.Resources.TraceCommandStatusChanged, innerName, innerStatus)
					End If
				End If
			End Set
		End Property

		''' <summary>
		''' Registers a <see cref="CommandAdapter"/> with this command.
		''' </summary>
		''' <param name="adapter">The <see cref="CommandAdapter"/> to register with this <see cref="Command"/>.</param>
		Public Overridable Sub AddCommandAdapter(ByVal adapter As CommandAdapter)
			AddHandler adapter.ExecuteCommand, AddressOf Me.OnExecuteAction
			adapter.BindCommand(Me)

			innerAdapters.Add(adapter)
		End Sub

		''' <summary>
		''' Removes the <see cref="CommandAdapter"/> from this command.
		''' </summary>
		''' <param name="adapter">The <see cref="CommandAdapter"/> to remove from this <see cref="Command"/>.</param>
		Public Overridable Sub RemoveCommandAdapter(ByVal adapter As CommandAdapter)
			RemoveHandler adapter.ExecuteCommand, AddressOf Me.OnExecuteAction
			adapter.UnbindCommand(Me)

			innerAdapters.Remove(adapter)
		End Sub

		''' <summary>
		''' Executes this <see cref="Command"/> by firing the <see cref="ExecuteAction"/> event.
		''' </summary>
		''' <remarks>The event will only be fired when the command's status is enabled.</remarks>
		Public Overridable Sub Execute()
			If innerStatus = CommandStatus.Enabled Then
				OnExecuteAction(Me, EventArgs.Empty)
				If Not innerTraceSource Is Nothing Then
					innerTraceSource.TraceInformation(My.Resources.TraceCommandExecuted, innerName)
				End If
			End If
		End Sub

		''' <summary>
		''' Returns a <see cref="ReadOnlyCollection{T}"/> with the registered <see cref="CommandAdapter"/>.
		''' </summary>
		Public Overridable ReadOnly Property Adapters() As ReadOnlyCollection(Of CommandAdapter)
			Get
				Return New ReadOnlyCollection(Of CommandAdapter)(innerAdapters)
			End Get
		End Property

#Region "Utility class for FindAdapters()"
		Private Class AdapterHandler(Of TAdapter As CommandAdapter)
			Private adapters As List(Of TAdapter)
			Public Sub New(ByRef anAdaptersList As List(Of TAdapter))
				adapters = anAdaptersList
			End Sub
			Public Sub Handle(ByVal anAdapter As CommandAdapter)
				If TypeOf anAdapter Is TAdapter Then
					adapters.Add(DirectCast(anAdapter, TAdapter))
				End If
			End Sub
		End Class
#End Region

		''' <summary>
		''' Returns a <see cref="ReadOnlyCollection{T}"/> with the registered <see cref="CommandAdapter"/>
		''' which are of the type specified by TAdapter.
		''' </summary>
		''' <typeparam name="TAdapter">The type of adapter to search for.</typeparam>
		''' <returns>A new instance of a <see cref="ReadOnlyCollection{T}"/>.</returns>
		Public Overridable Function FindAdapters(Of TAdapter As CommandAdapter)() As ReadOnlyCollection(Of TAdapter)
			Dim found As List(Of TAdapter) = New List(Of TAdapter)()


			Dim handler As AdapterHandler(Of TAdapter) = New AdapterHandler(Of TAdapter)(found)
			innerAdapters.ForEach(AddressOf handler.Handle)
			Return New ReadOnlyCollection(Of TAdapter)(found)
		End Function

		''' <summary>
		''' Adds a new invoker for this <see cref="Command"/>.
		''' </summary>
		''' <remarks>The command will use the <see cref="ICommandAdapterMapService"/> to create the 
		''' corresponding <see cref="CommandAdapter"/> for the specified invoker.</remarks>
		Public Overridable Sub AddInvoker(ByVal invoker As Object, ByVal eventName As String)
			If innerMapService Is Nothing Then
				Throw New CommandException(String.Format(CultureInfo.CurrentCulture, My.Resources.CannotGetMapService, Name))
			End If

			Dim adapter As CommandAdapter = innerMapService.CreateAdapter(invoker.GetType())
			If adapter Is Nothing Then
				Throw New CommandException(String.Format(CultureInfo.CurrentCulture, My.Resources.CannotGetCommandAdapter, invoker.GetType()))
			End If
			adapter.AddInvoker(invoker, eventName)
			AddCommandAdapter(adapter)
			ownedAdapters.Add(adapter)
		End Sub

		''' <summary>
		''' Removes the invoker from the <see cref="Command"/>.
		''' </summary>
		''' <param name="invoker">An invoker object for the command.</param>
		''' <param name="eventName">The name of the event on the invoker object that fires 
		''' this <see cref="Command"/>.</param>
		''' <remarks>This method removes the invoker from all the <see cref="CommandAdapter"/> 
		''' registered with this <see cref="Command"/>.</remarks>
		Public Overridable Sub RemoveInvoker(ByVal invoker As Object, ByVal eventName As String)
			For Each adapter As CommandAdapter In innerAdapters.ToArray()
				If adapter.ContainsInvoker(invoker) = True Then
					adapter.RemoveInvoker(invoker, eventName)
					If adapter.InvokerCount = 0 Then
						RemoveCommandAdapter(adapter)
						If (ownedAdapters.Contains(adapter)) Then
							ownedAdapters.Remove(adapter)
							adapter.Dispose()
						End If
					End If
				End If
			Next adapter
		End Sub

		''' <summary>
		''' Handles the <see cref="CommandAdapter.ExecuteCommand"/> event and fires the <see cref="ExecuteAction"/>
		''' event accordingly.
		''' </summary>
		''' <param name="sender">The sender of the event.</param>
		''' <param name="e">The arguments of the event.</param>
		Protected Overridable Sub OnExecuteAction(ByVal sender As Object, ByVal e As EventArgs)
			If Status = CommandStatus.Enabled AndAlso Not ExecuteActionEvent Is Nothing Then
				RaiseEvent ExecuteAction(Me, e)
			End If
		End Sub

		''' <summary>
		''' Fires the <see cref="Changed"/> event for this command.
		''' </summary>
		Protected Overridable Sub OnChanged()
			If Not ChangedEvent Is Nothing Then
				RaiseEvent Changed(Me, EventArgs.Empty)
			End If
		End Sub

		Friend Function IsHandlerRegistered(ByVal target As Object, ByVal methodInfo As MethodInfo) As Boolean
			If Not ExecuteActionEvent Is Nothing Then
				For Each dlg As System.Delegate In ExecuteActionEvent.GetInvocationList()
					If dlg.Target Is target AndAlso dlg.Method Is methodInfo Then
						Return True
					End If
				Next dlg
			End If
			Return False
		End Function

		''' <summary>
		''' Removes all the adapters from the command before disposing it.
		''' </summary>
		Public Sub Dispose() Implements IDisposable.Dispose
			Dispose(True)
			GC.SuppressFinalize(Me)
		End Sub

		''' <summary>
		''' Called to free resources.
		''' </summary>
		''' <param name="disposing">Should be <see langword="true"/> when calling from Dispose().</param>
		Protected Overridable Sub Dispose(ByVal disposing As Boolean)
			If disposing Then
				RemoveCommandAdapters()
			End If
		End Sub

		Private Sub RemoveCommandAdapters()
			For Each adapter As CommandAdapter In innerAdapters.ToArray()
				RemoveCommandAdapter(adapter)
			Next
			For Each adapter As CommandAdapter In ownedAdapters.ToArray()
				adapter.Dispose()
			Next
		End Sub

		''' <summary>
		''' See <see cref="IBuilderAware.OnBuiltUp"/> for more information.
		''' </summary>
		Public Overridable Sub OnBuiltUp(ByVal id As String) Implements IBuilderAware.OnBuiltUp
			Me.innerName = id
		End Sub

		''' <summary>
		''' See <see cref="IBuilderAware.OnTearingDown"/> for more information.
		''' </summary>
		Public Overridable Sub OnTearingDown() Implements IBuilderAware.OnTearingDown
		End Sub

		Private Class ClassNameTraceSourceAttribute : Inherits TraceSourceAttribute
			Public Sub New()
				MyBase.New(GetType(Command).FullName)
			End Sub
		End Class

	End Class
End Namespace