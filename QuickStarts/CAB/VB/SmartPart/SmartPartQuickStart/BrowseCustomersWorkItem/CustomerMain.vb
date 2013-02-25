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
Imports System.Windows.Forms
Imports Microsoft.Practices.CompositeUI
Imports Microsoft.Practices.CompositeUI.SmartParts
Imports System.Collections.ObjectModel

Namespace SmartPartQuickStart.BrowseCustomersWorkItem
	''' <summary>
	''' The SmartPart attribute tells the SmartPartMonitor to add this view 
	''' to the WorkItem.
	''' </summary>
	<SmartPart()> _
	Partial Public Class CustomerMain
		Inherits UserControl
		Implements IWorkspace

		''' <summary>
		''' Constructor
		''' </summary>
		Public Sub New()
			InitializeComponent()
		End Sub

		' You can make anything a workspace by implementing the
		' IWorkspace interface.
#Region "IWorkspace Members"

		''' <summary>
		''' Fired when a smartpart is closing.
		''' </summary>
		Public Event SmartPartClosing As EventHandler(Of WorkspaceCancelEventArgs) Implements IWorkspace.SmartPartClosing

		''' <summary>
		''' Fires when the smartpart is activated.
		''' </summary>
		Public Custom Event SmartPartActivated As EventHandler(Of WorkspaceEventArgs) Implements IWorkspace.SmartPartActivated
			AddHandler(ByVal value As EventHandler(Of WorkspaceEventArgs))
				Throw New Exception("The method or operation is not implemented.")
			End AddHandler
			RemoveHandler(ByVal value As EventHandler(Of WorkspaceEventArgs))
				Throw New Exception("The method or operation is not implemented.")
			End RemoveHandler
			RaiseEvent(ByVal sender As Object, ByVal e As WorkspaceEventArgs)
			End RaiseEvent
		End Event

		''' <summary>
		''' Shows the given smartpart in a workspace with the 
		''' given smartpartinfo which provides additional information.
		''' </summary>
		''' <param name="smartPart"></param>
		''' <param name="smartPartInfo"></param>
		Public Overloads Sub Show(ByVal smartPart As Object, ByVal smartPartInfo As ISmartPartInfo)
			Me.customersDeckedWorkspace.Show(smartPart, smartPartInfo)
		End Sub

		''' <summary>
		''' Shows the given smartpart in a workspace.
		''' </summary>
		''' <param name="smartPart"></param>
		Public Overloads Sub Show(ByVal smartPart As Object)
			Me.customersDeckedWorkspace.Show(smartPart)
		End Sub

		''' <summary>
		''' Hides the given smartpart.
		''' </summary>
		''' <param name="smartPart"></param>
		Public Overloads Sub Hide(ByVal smartPart As Object)
			Me.customersDeckedWorkspace.Hide(smartPart)
		End Sub

		''' <summary>
		''' Closes the given smartpart.
		''' </summary>
		''' <param name="smartPart"></param>
		Public Sub Close(ByVal smartPart As Object)
			RaiseEvent SmartPartClosing(Me, New WorkspaceCancelEventArgs(smartPart))

			Me.customersDeckedWorkspace.Close(smartPart)
		End Sub

#End Region


#Region "IWorkspace Members"


		Public ReadOnly Property SmartParts() As ReadOnlyCollection(Of Object) Implements IWorkspace.SmartParts
			Get
				Throw New Exception("The method or operation is not implemented.")
			End Get
		End Property

		Public ReadOnly Property ActiveSmartPart() As Object Implements IWorkspace.ActiveSmartPart
			Get
				Throw New Exception("The method or operation is not implemented.")
			End Get
		End Property

		Public Sub Activate(ByVal smartPart As Object)
			Throw New Exception("The method or operation is not implemented.")
		End Sub

		Public Sub ApplySmartPartInfo(ByVal smartPart As Object, ByVal smartPartInfo As ISmartPartInfo)
			Throw New Exception("The method or operation is not implemented.")
		End Sub

		Public Overloads Function Contains(ByVal smartPart As Object) As Boolean
			Throw New Exception("The method or operation is not implemented.")
		End Function

		Public Function IsActive(ByVal smartPart As Object) As Boolean
			Throw New Exception("The method or operation is not implemented.")
		End Function

#End Region

		Public Sub Activate1(ByVal smartPart As Object) Implements Microsoft.Practices.CompositeUI.SmartParts.IWorkspace.Activate

		End Sub


		Public Sub ApplySmartPartInfo1(ByVal smartPart As Object, ByVal smartPartInfo As Microsoft.Practices.CompositeUI.SmartParts.ISmartPartInfo) Implements Microsoft.Practices.CompositeUI.SmartParts.IWorkspace.ApplySmartPartInfo

		End Sub

		Public Sub Close1(ByVal smartPart As Object) Implements Microsoft.Practices.CompositeUI.SmartParts.IWorkspace.Close

		End Sub

		Public Sub Hide1(ByVal smartPart As Object) Implements Microsoft.Practices.CompositeUI.SmartParts.IWorkspace.Hide

		End Sub

		Public Sub Show1(ByVal smartPart As Object) Implements Microsoft.Practices.CompositeUI.SmartParts.IWorkspace.Show

		End Sub

		Public Sub Show1(ByVal smartPart As Object, ByVal smartPartInfo As Microsoft.Practices.CompositeUI.SmartParts.ISmartPartInfo) Implements Microsoft.Practices.CompositeUI.SmartParts.IWorkspace.Show

		End Sub

	End Class
End Namespace
