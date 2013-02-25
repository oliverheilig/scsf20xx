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

Namespace Services
	''' <summary>
	''' The implemented service should register <see cref="WorkItem"/> types
	''' and create <see cref="WorkItem"/>s.
	''' </summary>
	Public Interface IWorkItemTypeCatalogService
		''' <summary>
		''' Returns the list of registered <see cref="WorkItem"/> types.
		''' </summary>
		ReadOnly Property RegisteredWorkItemTypes() As ICollection(Of Type)

		''' <summary>
		''' Creates <see cref="WorkItem"/>s for the registered types that match the provided type.
		''' </summary>
		''' <typeparam name="TWorkItem">The type of <see cref="WorkItem"/>s to create.</typeparam>
		''' <param name="parentWorkItem">The parent <see cref="WorkItem"/> to create them in.</param>
		''' <param name="action">A callback for each created <see cref="WorkItem"/>.</param>
		Sub CreateEachWorkItem(Of TWorkItem)(ByVal parentWorkItem As WorkItem, ByVal action As Action(Of TWorkItem))

		''' <summary>
		''' Creates <see cref="WorkItem"/>s for the registered types that match the provided type.
		''' </summary>
		''' <param name="workItemType">The type of <see cref="WorkItem"/>s to create.</param>
		''' <param name="parentWorkItem">The parent <see cref="WorkItem"/> to create them in.</param>
		''' <param name="action">A callback for each created <see cref="WorkItem"/>.</param>
		Sub CreateEachWorkItem(ByVal workItemType As Type, ByVal parentWorkItem As WorkItem, ByVal action As Action(Of WorkItem))

		''' <summary>
		''' Registers a <see cref="WorkItem"/> type with the catalog.
		''' </summary>
		''' <typeparam name="TWorkItem">The type of WorkItem to be registered.</typeparam>
		Sub RegisterWorkItem(Of TWorkItem As WorkItem)()

		''' <summary>
		''' Registers a <see cref="WorkItem"/> type with the catalog.
		''' </summary>
		''' <param name="workItemType">The type of WorkItem to be registered.</param>
		Sub RegisterWorkItem(ByVal workItemType As Type)
	End Interface
End Namespace