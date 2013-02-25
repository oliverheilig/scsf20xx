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
Imports System.Collections.Generic

Namespace Services
	''' <summary>
	''' Keeps a catlog of <see cref="WorkItem"/>s that are registered at module initialization time.
	''' </summary>
	Public Class WorkItemTypeCatalogService
		Implements IWorkItemTypeCatalogService

		Private registeredTypes As Utility.Set(Of Type) = New Utility.Set(Of Type)()

		''' <summary>
		''' See <see cref="IWorkItemTypeCatalogService.RegisteredWorkItemTypes"/> for more information.
		''' </summary>
		Public ReadOnly Property RegisteredWorkItemTypes() As ICollection(Of Type) Implements IWorkItemTypeCatalogService.RegisteredWorkItemTypes
			Get
				Return registeredTypes.AsReadOnly()
			End Get
		End Property

		''' <summary>
		''' See <see cref="IWorkItemTypeCatalogService.CreateEachWorkItem{TWorkItem}"/> for more information.
		''' </summary>
		Public Sub CreateEachWorkItem(Of TWorkItem)(ByVal parentWorkItem As WorkItem, ByVal action As Action(Of TWorkItem)) Implements IWorkItemTypeCatalogService.CreateEachWorkItem
			Guard.ArgumentNotNull(parentWorkItem, "parentWorkItem")
			Guard.ArgumentNotNull(action, "action")

			For Each type As Type In registeredTypes
				If GetType(TWorkItem).IsAssignableFrom(type) Then
					action(CType(parentWorkItem.Items.AddNew(type), TWorkItem))
				End If
			Next type
		End Sub

		''' <summary>
		''' See <see cref="IWorkItemTypeCatalogService.CreateEachWorkItem"/> for more information.
		''' </summary>
		Public Sub CreateEachWorkItem(ByVal workItemType As Type, ByVal parentWorkItem As WorkItem, _
		 ByVal action As Action(Of WorkItem)) Implements IWorkItemTypeCatalogService.CreateEachWorkItem

			Guard.ArgumentNotNull(parentWorkItem, "parentWorkItem")
			Guard.ArgumentNotNull(action, "action")

			For Each type As Type In registeredTypes
				If workItemType.IsAssignableFrom(type) Then
					action(CType(parentWorkItem.Items.AddNew(type), WorkItem))
				End If
			Next type
		End Sub

		''' <summary>
		''' See <see cref="IWorkItemTypeCatalogService.RegisterWorkItem{TWorkItem}"/> for more information.
		''' </summary>
		Public Sub RegisterWorkItem(Of TWorkItem As WorkItem)() Implements IWorkItemTypeCatalogService.RegisterWorkItem
			RegisterWorkItem(GetType(TWorkItem))
		End Sub

		''' <summary>
		''' See <see cref="IWorkItemTypeCatalogService.RegisterWorkItem"/> for more information.
		''' </summary>
		Public Sub RegisterWorkItem(ByVal workItemType As Type) Implements IWorkItemTypeCatalogService.RegisterWorkItem
			Guard.TypeIsAssignableFromType(workItemType, GetType(WorkItem), "workItemType")

			registeredTypes.Add(workItemType)
		End Sub
	End Class
End Namespace
