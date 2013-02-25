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
Imports System.Collections.ObjectModel

Namespace SmartParts
	''' <summary>
	''' Workspaces are used to provide a surface onto which display SmartParts.
	''' </summary>
	Public Interface IWorkspace
		''' <summary>
		''' Fires when smart part is closing.
		''' </summary>
		Event SmartPartClosing As EventHandler(Of WorkspaceCancelEventArgs)

		''' <summary>
		''' Fires when the smartpart is activated.
		''' </summary>
		Event SmartPartActivated As EventHandler(Of WorkspaceEventArgs)

		''' <summary>
		''' A snapshot of the smart parts currently contained in the workspace.
		''' </summary>
		ReadOnly Property SmartParts() As ReadOnlyCollection(Of Object)

		''' <summary>
		''' The currently active smart part.
		''' </summary>
		ReadOnly Property ActiveSmartPart() As Object

		''' <summary>
		''' Activates the smartPart on the workspace.
		''' </summary>
		''' <param name="smartPart">The smart part to activate.</param>
		Sub Activate(ByVal smartPart As Object)

		''' <summary>
		''' Applies the smartPartInfo to the smartPart.
		''' </summary>
		Sub ApplySmartPartInfo(ByVal smartPart As Object, ByVal smartPartInfo As ISmartPartInfo)

		''' <summary>
		''' Closes a smart part. Disposing the smart part is the responsibility of the caller.
		''' </summary>
		''' <param name="smartPart">Smart part to close.</param>
		Sub Close(ByVal smartPart As Object)

		''' <summary>
		''' Hides a smart part from the UI.
		''' </summary>
		''' <param name="smartPart">Smart part to hide.</param>
		Sub Hide(ByVal smartPart As Object)

		''' <summary>
		''' Shows SmartPart using the given SmartPartInfo
		''' </summary>
		''' <param name="smartPart">Smart part to show.</param>
		''' <param name="smartPartInfo"></param>
		Sub Show(ByVal smartPart As Object, ByVal smartPartInfo As ISmartPartInfo)

		''' <summary>
		''' Shows a smart part in the UI.
		''' </summary>
		''' <param name="smartPart">Smart part to show.</param>
		Sub Show(ByVal smartPart As Object)
	End Interface
End Namespace