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

''' <summary>
''' An implementation of <see cref="IWorkItemActivationService"/> that ensures that only
''' one <see cref="WorkItem"/> is active at one time.
''' </summary>
Public Class SimpleWorkItemActivationService
	Implements IWorkItemActivationService

	Private syncroot As Object = New Object()
	Private activeWorkItem As WorkItem

	''' <summary>
	''' Initializes a new instance of the <see cref="SimpleWorkItemActivationService"/> class.
	''' </summary>
	Public Sub New()
	End Sub

	''' <summary>
	''' See <see cref="IWorkItemActivationService.ChangeStatus"/> for more information.
	''' </summary>
	Public Sub ChangeStatus(ByVal item As WorkItem) Implements IWorkItemActivationService.ChangeStatus
		SyncLock syncroot
			If Not item Is activeWorkItem AndAlso item.Status = WorkItemStatus.Active Then
				If Not activeWorkItem Is Nothing AndAlso activeWorkItem.Status <> WorkItemStatus.Terminated Then
					activeWorkItem.Deactivate()
				End If

				activeWorkItem = item
			End If
		End SyncLock
	End Sub
End Class
