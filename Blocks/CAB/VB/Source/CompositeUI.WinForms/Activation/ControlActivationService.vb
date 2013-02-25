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
Imports System.Windows.Forms


''' <summary>
''' Monitors a <see cref="Control"/> and when it is entered activates the
''' <see cref="WorkItem"/> the <see cref="Control"/> is contained in.
''' </summary>
Public Class ControlActivationService : Implements IControlActivationService
	Private innerWorkItem As WorkItem

	''' <summary>
	''' The <see cref="WorkItem"/> where this service lives.
	''' </summary>
	<ServiceDependency()> _
	Public WriteOnly Property WorkItem() As WorkItem
		Set(ByVal value As WorkItem)
			innerWorkItem = value
		End Set
	End Property

	''' <summary>
	''' Notifies that a <see cref="Control"/> has been added to the container.
	''' </summary>
	''' <param name="control">The <see cref="Control"/> in which to monitor the OnEnter event.</param>
	Public Sub ControlAdded(ByVal control As Control) Implements IControlActivationService.ControlAdded
		AddHandler control.Enter, AddressOf OnControlEntered
		AddHandler control.Disposed, AddressOf OnControlDisposed
	End Sub

	''' <summary>
	''' Notifies that a <see cref="Control"/> has been removed from the container.
	''' </summary>
	''' <param name="control">The <see cref="Control"/> being monitored.</param>
	Public Sub ControlRemoved(ByVal control As Control) Implements IControlActivationService.ControlRemoved
		RemoveHandler control.Enter, AddressOf OnControlEntered
		RemoveHandler control.Disposed, AddressOf OnControlDisposed
	End Sub

	Private Sub OnControlEntered(ByVal sender As Object, ByVal e As EventArgs)
		If Not innerWorkItem Is Nothing Then
			innerWorkItem.Activate()
		End If
	End Sub

	Private Sub OnControlDisposed(ByVal sender As Object, ByVal e As EventArgs)
		Dim control As Control = CType(sender, Control)
		RemoveHandler control.Enter, AddressOf OnControlEntered
		RemoveHandler control.Disposed, AddressOf OnControlDisposed
	End Sub
End Class
