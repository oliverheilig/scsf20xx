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
Imports Microsoft.Practices.CompositeUI.Utility

''' <summary>
''' Base implementation with virtual methods for handling the 
''' <see cref="WorkItem"/> events.
''' </summary>
Public MustInherit Class WorkItemExtension
	Implements IWorkItemExtension

	Private innerWorkItem As WorkItem

	''' <summary>
	''' Hooks to all events in the <see cref="WorkItem"/> to 
	''' the virtual event handlers.
	''' </summary>
	''' <param name="workItem">The <see cref="WorkItem"/> being extended.</param>
	Public Sub Initialize(ByVal workItem As WorkItem) Implements IWorkItemExtension.Initialize
		Guard.ArgumentNotNull(workItem, "workItem")

		Me.innerWorkItem = workItem

		AddHandler workItem.Activated, AddressOf OnActivated
		AddHandler workItem.Deactivated, AddressOf OnDeactivated
		AddHandler workItem.Initialized, AddressOf OnInitialized
		AddHandler workItem.RunStarted, AddressOf OnRunStarted
		AddHandler workItem.Terminated, AddressOf OnTerminated
	End Sub

	Private Sub OnActivated(ByVal sender As Object, ByVal e As EventArgs)
		OnActivated()
	End Sub

	Private Sub OnDeactivated(ByVal sender As Object, ByVal e As EventArgs)
		OnDeactivated()
	End Sub

	Private Sub OnInitialized(ByVal sender As Object, ByVal e As EventArgs)
		OnInitialized()
	End Sub

	Private Sub OnRunStarted(ByVal sender As Object, ByVal e As EventArgs)
		OnRunStarted()
	End Sub

	Private Sub OnTerminated(ByVal sender As Object, ByVal e As EventArgs)
		OnTerminated()
	End Sub

	''' <summary>
	''' The <see cref="WorkItem"/> being extended.
	''' </summary>
	Public ReadOnly Property WorkItem() As WorkItem
		Get
			Return innerWorkItem
		End Get
	End Property

	''' <summary>
	''' Method that handles the <see cref="Microsoft.Practices.CompositeUI.WorkItem.Activated"/> event on the 
	''' extended <see cref="WorkItem"/>.
	''' </summary>
	Protected Overridable Sub OnActivated()
	End Sub

	''' <summary>
	''' Method that handles the <see cref="Microsoft.Practices.CompositeUI.WorkItem.Deactivated"/> event on the 
	''' extended <see cref="WorkItem"/>.
	''' </summary>
	Protected Overridable Sub OnDeactivated()
	End Sub

	''' <summary>
	''' Method that handles the <see cref="Microsoft.Practices.CompositeUI.WorkItem.Initialized"/> event on the 
	''' extended <see cref="WorkItem"/>.
	''' </summary>
	Protected Overridable Sub OnInitialized()
	End Sub

	''' <summary>
	''' Method that handles the <see cref="Microsoft.Practices.CompositeUI.WorkItem.RunStarted"/> event on the 
	''' extended <see cref="WorkItem"/>.
	''' </summary>
	Protected Overridable Sub OnRunStarted()
	End Sub

	''' <summary>
	''' Method that handles the <see cref="Microsoft.Practices.CompositeUI.WorkItem.OnTerminated"/> event on the 
	''' extended <see cref="WorkItem"/>.
	''' </summary>
	Protected Overridable Sub OnTerminated()
	End Sub
End Class