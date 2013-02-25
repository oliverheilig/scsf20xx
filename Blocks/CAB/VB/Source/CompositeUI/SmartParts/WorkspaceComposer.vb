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

Namespace SmartParts
	''' <summary>
	''' Composer class that allows workspaces that cannot inherit from 
	''' <see cref="Workspace{TSmartPart, TSmartPartInfo}"/> to reuse its logic by 
	''' implementing <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}"/> and 
	''' forwarding all its <see cref="IWorkspace"/> implementation methods to an instance 
	''' of the composer.
	''' </summary>
	''' <typeparam name="TSmartPart">Type of smart parts supported by the workspace being composed.</typeparam>
	''' <typeparam name="TSmartPartInfo">Type of the smart part information received by the workspace.</typeparam>
	''' <remarks>
	''' For an example on how to reuse the logic in <see cref="Workspace{TSmartPart, TSmartPartInfo}"/> on workspaces 
	''' that inherit from another class already, take a look at the TabWorkspace in the WinForms project.
	''' </remarks>
	Public Class WorkspaceComposer(Of TSmartPart, TSmartPartInfo As {ISmartPartInfo, New}) : Inherits Workspace(Of TSmartPart, TSmartPartInfo)
		Private composedWorkspace As IComposableWorkspace(Of TSmartPart, TSmartPartInfo)

		''' <summary>
		''' Initializes the composer with the composedWorkspace that will 
		''' be called when needed by the base <see cref="Workspace{TSmartPart, TSmartPartInfo}"/>.
		''' </summary>
		''' <param name="composedWorkspace">The workspace being composed with the behavior in this class.</param>
		Public Sub New(ByVal composedWorkspace As IComposableWorkspace(Of TSmartPart, TSmartPartInfo))
			Guard.ArgumentNotNull(composedWorkspace, "composedWorkspace")
			Me.composedWorkspace = composedWorkspace
		End Sub

		''' <summary>
		''' Sets the active smart part in the workspace.
		''' </summary>
		Public Overloads Sub SetActiveSmartPart(ByVal smartPart As TSmartPart)
			MyBase.SetActiveSmartPart(CObj(smartPart))
		End Sub

		''' <summary>
		''' Forcedly closes the smart part, without raising the <see cref="IWorkspace.SmartPartClosing"/> event.
		''' </summary>
		''' <param name="smartPart"></param>
		Public Sub ForceClose(ByVal smartPart As TSmartPart)
			MyBase.CloseInternal(smartPart)
		End Sub

		Private Sub OnSmartPartClosingEvent(ByVal sender As Object, ByVal e As WorkspaceCancelEventArgs) Handles MyBase.SmartPartClosing
			composedWorkspace.RaiseSmartPartClosing(e)
		End Sub

		Private Sub OnSmartPartActivatedEvent(ByVal sender As Object, ByVal e As WorkspaceEventArgs) Handles MyBase.SmartPartActivated
			composedWorkspace.RaiseSmartPartActivated(e)
		End Sub

		''' <summary>
		''' Calls <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.OnActivate"/> 
		''' on the composed workspace.
		''' </summary>
		Protected Overrides Sub OnActivate(ByVal smartPart As TSmartPart)
			composedWorkspace.OnActivate(smartPart)
		End Sub

		''' <summary>
		''' Calls <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.OnApplySmartPartInfo"/> 
		''' on the composed workspace.
		''' </summary>
		Protected Overrides Sub OnApplySmartPartInfo(ByVal smartPart As TSmartPart, ByVal smartPartInfo As TSmartPartInfo)
			composedWorkspace.OnApplySmartPartInfo(smartPart, smartPartInfo)
		End Sub

		''' <summary>
		''' Calls <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.OnShow"/> 
		''' on the composed workspace.
		''' </summary>
		Protected Overrides Sub OnShow(ByVal smartPart As TSmartPart, ByVal smartPartInfo As TSmartPartInfo)
			composedWorkspace.OnShow(smartPart, smartPartInfo)
		End Sub

		''' <summary>
		''' Calls <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.OnHide"/> 
		''' on the composed workspace.
		''' </summary>
		Protected Overrides Sub OnHide(ByVal smartPart As TSmartPart)
			composedWorkspace.OnHide(smartPart)
		End Sub

		''' <summary>
		''' Calls <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.OnClose"/> 
		''' on the composed workspace.
		''' </summary>
		Protected Overrides Sub OnClose(ByVal smartPart As TSmartPart)
			composedWorkspace.OnClose(smartPart)
		End Sub

		''' <summary>
		''' Calls <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.ConvertFrom"/> 
		''' on the composed workspace.
		''' </summary>
		Protected Overrides Function ConvertFrom(ByVal source As ISmartPartInfo) As TSmartPartInfo
			Return composedWorkspace.ConvertFrom(source)
		End Function
	End Class
End Namespace
