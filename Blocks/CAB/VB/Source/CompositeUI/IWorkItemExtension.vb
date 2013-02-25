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
Imports System.ComponentModel


''' <summary>
''' Represents an extension that can monitor and act upon events in the 
''' lifecycle of a <see cref="WorkItem"/>.
''' </summary>
Public Interface IWorkItemExtension
	''' <summary>
	''' Initializes the extension with the work item being extended. 
	''' </summary>
	''' <param name="workItem">The work item being started.</param>
	''' <remarks>
	''' If the extension implements <see cref="IComponent"/>, this method 
	''' is called after it has been sited.
	''' </remarks>
	Sub Initialize(ByVal workItem As WorkItem)
End Interface
