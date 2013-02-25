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
Imports Microsoft.Practices.ObjectBuilder


''' <summary>
''' The interface to be implemented by a visualizer in CAB.
''' </summary>
Public Interface IVisualizer : Inherits IDisposable
	''' <summary>
	''' Returns the <see cref="Builder"/> used by the CAB application.
	''' </summary>
	ReadOnly Property CabBuilder() As Builder

	''' <summary>
	''' Returns the <see cref="WorkItem"/> that is the root of the CAB application hierarchy.
	''' </summary>
	ReadOnly Property CabRootWorkItem() As WorkItem

	''' <summary>
	''' Initializes the visualizer.
	''' </summary>
	''' <param name="cabRootWorkItem">The root <see cref="WorkItem"/> of the <see cref="CabApplication{TWorkItem}"/>.</param>
	''' <param name="cabBuilder">The <see cref="Builder"/> used by the <see cref="CabApplication{TWorkItem}"/>.</param>
	Sub Initialize(ByVal cabRootWorkItem As WorkItem, ByVal cabBuilder As Builder)
End Interface
