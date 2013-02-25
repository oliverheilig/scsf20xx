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

Namespace SmartParts
	''' <summary>
	''' Interface implemented by workspaces that cannot inherit from 
	''' <see cref="Workspace{TSmartPart, TSmartPartInfo}"/> and want to aggregate the functionality from 
	''' a <see cref="WorkspaceComposer{TSmartPart, TSmartPartInfo}"/>
	''' </summary>
	''' <typeparam name="TSmartPart">Type of smart parts supported by the workspace implementing this interface.</typeparam>
	''' <typeparam name="TSmartPartInfo">Type of the smart part information received by the workspace implementing this interface.</typeparam>
	Public Interface IComposableWorkspace(Of TSmartPart, TSmartPartInfo As {ISmartPartInfo, New}) : Inherits IWorkspace
		''' <summary>
		''' When overridden in a derived class, activates the smartPart
		''' in the workspace.
		''' </summary>
		''' <param name="smartPart">The smart part to activate.</param>
		Sub OnActivate(ByVal smartPart As TSmartPart)

		''' <summary>
		''' When overridden in a derived class, applies the smartPartInfo
		''' to the smartPart that lives in the workspace.
		''' </summary>
		Sub OnApplySmartPartInfo(ByVal smartPart As TSmartPart, ByVal smartPartInfo As TSmartPartInfo)

		''' <summary>
		''' When overridden in a derived class, shows the smartPart
		''' on the workspace.
		''' </summary>
		''' <param name="smartPart">The smart part to show.</param>
		''' <param name="smartPartInfo">The information to apply to the smart part.</param>
		Sub OnShow(ByVal smartPart As TSmartPart, ByVal smartPartInfo As TSmartPartInfo)

		''' <summary>
		''' When overridden in a derived class, hides the smartPart
		''' on the workspace.
		''' </summary>
		''' <param name="smartPart">The smart part to hide.</param>
		Sub OnHide(ByVal smartPart As TSmartPart)

		''' <summary>
		''' When overridden in a derived class, closes and removes the smartPart
		''' from the workspace.
		''' </summary>
		''' <param name="smartPart">The smart part to close.</param>
		Sub OnClose(ByVal smartPart As TSmartPart)

		''' <summary>
		''' Raises the <see cref="IWorkspace.SmartPartActivated"/> event on the composed workspace.
		''' </summary>
		''' <param name="e">The <see cref="EventArgs"/> to use for the event.</param>
		Sub RaiseSmartPartActivated(ByVal e As WorkspaceEventArgs)

		''' <summary>
		''' Raises the <see cref="IWorkspace.SmartPartClosing"/> event on the composed workspace.
		''' </summary>
		''' <param name="e">The <see cref="EventArgs"/> to use for the event.</param>
		Sub RaiseSmartPartClosing(ByVal e As WorkspaceCancelEventArgs)

		''' <summary>
		''' Converts from the given source into a new instance of the TSmartPartInfo information.
		''' </summary>
		Function ConvertFrom(ByVal source As ISmartPartInfo) As TSmartPartInfo
	End Interface
End Namespace
