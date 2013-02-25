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
''' Provides functionality for monitoring <see cref="WorkItem"/> activation.
''' </summary>
Public Interface IWorkItemActivationService
	''' <summary>
	''' Called by the <see cref="WorkItem"/> when its status has changed.
	''' </summary>
	''' <param name="item">The <see cref="WorkItem"/> that has changed.</param>
	Sub ChangeStatus(ByVal item As WorkItem)
End Interface

