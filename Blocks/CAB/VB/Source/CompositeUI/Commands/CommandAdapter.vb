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
Imports Microsoft.Practices.ObjectBuilder
Imports Microsoft.Practices.CompositeUI.Utility

Namespace Commands
	''' <summary>
	''' Defines a command adapter class, which provides the logic needed for an object to be able 
	''' to invoke a <see cref="Command"/>.
	''' </summary>
	Public MustInherit Class CommandAdapter : Implements IDisposable
		Private boundCommand As Command

		''' <summary>
		''' Event to be fired by the <see cref="CommandAdapter"/> implementations
		''' to signal that the <see cref="Command"/> should execute.
		''' </summary>
		Public Event ExecuteCommand As EventHandler

		''' <summary>
		''' Called when the <see cref="CommandAdapter"/> is registered with a <see cref="Command"/>.
		''' </summary>
		''' <param name="command">The <see cref="Command"/> the adapter is being added to.</param>
		''' <remarks>The default implementation of this method adds the CommandChangedHandler method
		''' handler to the <see cref="Command.Changed"/> event.
		''' </remarks>
		Public Overridable Sub BindCommand(ByVal command As Command)
			Guard.ArgumentNotNull(command, "command")
			If Not boundCommand Is Nothing Then
				Throw New InvalidOperationException(My.Resources.AdapterAlreadyBoundToACommand)
			End If
			AddHandler command.Changed, AddressOf CommandChangedHandler
			boundCommand = command
		End Sub

		''' <summary>
		''' Called when <see cref="CommandAdapter"/> is removed from a <see cref="Command"/>.
		''' </summary>
		''' <param name="command">The <see cref="Command"/> the adapter is being removed from.</param>
		''' <remarks>The default implementation of this method removes the CommandChangedHandler method handler 
		''' from the <see cref="Command.Changed"/> event.
		''' </remarks>
		Public Overridable Sub UnbindCommand(ByVal command As Command)
			Guard.ArgumentNotNull(command, "command")
			If Not boundCommand Is command Then
				Throw New InvalidOperationException(My.Resources.AdapterNotBoundToGivenCommand)
			End If
			RemoveHandler command.Changed, AddressOf CommandChangedHandler
			boundCommand = Nothing
		End Sub

		''' <summary>
		''' Adds an invoker to the <see cref="CommandAdapter"/>.
		''' </summary>
		Public MustOverride Sub AddInvoker(ByVal invoker As Object, ByVal eventName As String)

		''' <summary>
		''' Removes the invoker from the <see cref="CommandAdapter"/>.
		''' </summary>
		Public MustOverride Sub RemoveInvoker(ByVal invoker As Object, ByVal eventName As String)

		''' <summary>
		''' Gets the count of invokers in the <see cref="CommandAdapter"/>.
		''' </summary>
		Public MustOverride ReadOnly Property InvokerCount() As Integer

		''' <summary>
		''' Used to unhook and dispose all the invokers.
		''' </summary>
		Public Sub Dispose() Implements IDisposable.Dispose
			Dispose(True)
			GC.SuppressFinalize(Me)
		End Sub

		''' <summary>
		''' Called to free resources.
		''' </summary>
		''' <param name="disposing">Should be true when calling from Dispose().</param>
		Protected Overridable Sub Dispose(ByVal disposing As Boolean)
		End Sub

		''' <summary>
		''' Checks if the <see cref="CommandAdapter"/> contains the invoker.
		''' </summary>
		''' <param name="invoker">The invoker to check for.</param>
		''' <returns>True if the invoker is in the adapter; otherwise False.</returns>
		Public MustOverride Function ContainsInvoker(ByVal invoker As Object) As Boolean

		''' <summary>
		''' Handles the <see cref="Command.Changed"/> event.
		''' </summary>
		''' <param name="command">The <see cref="Command"/> that fired the Changed event.</param>
		Protected Overridable Sub OnCommandChanged(ByVal command As Command)
		End Sub

		''' <summary>
		''' Causes the execution of the <see cref="Command"/>.
		''' </summary>
		Protected Overridable Sub FireCommand()
			If Not ExecuteCommandEvent Is Nothing Then
				RaiseEvent ExecuteCommand(Me, EventArgs.Empty)
			End If
		End Sub

		Private Sub CommandChangedHandler(ByVal sender As Object, ByVal e As EventArgs)
			OnCommandChanged(CType(sender, Command))
		End Sub

	End Class
End Namespace