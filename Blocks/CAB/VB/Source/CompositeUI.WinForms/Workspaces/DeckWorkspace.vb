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
Imports System.ComponentModel
Imports System.Windows.Forms
Imports Microsoft.Practices.CompositeUI.SmartParts
Imports System.Collections.ObjectModel


''' <summary>
''' Implements a workspace which shows <see cref="Control"/> layered as in a deck.
''' </summary>
Partial Public Class DeckWorkspace

	Inherits Control
	Implements IComposableWorkspace(Of Control, SmartPartInfo)

	Private composer As WorkspaceComposer(Of Control, SmartPartInfo)
	Private isDisposing As Boolean = False

	''' <summary>
	''' Initializes a new instance of the <see cref="DeckWorkspace"/> class.
	''' </summary>
	Public Sub New()
		composer = New WorkspaceComposer(Of Control, SmartPartInfo)(Me)
	End Sub

	''' <summary>
	''' The controls that the deck currently contains.
	''' </summary>
	<Browsable(False)> _
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
	Public ReadOnly Property SmartParts() As ReadOnlyCollection(Of Control)
		Get
			Dim controls As Control() = New Control(composer.SmartParts.Count - 1) {}
			composer.SmartParts.CopyTo(controls, 0)
			Return New ReadOnlyCollection(Of Control)(controls)
		End Get
	End Property

	''' <summary>
	''' The currently active smart part.
	''' </summary>
	<Browsable(False)> _
	<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
	Public ReadOnly Property ActiveSmartPart() As Control
		Get
			Return CType(composer.ActiveSmartPart, Control)
		End Get
	End Property

	''' <summary>
	''' Dependency injection setter property to get the <see cref="WorkItem"/> where the 
	''' object is contained.
	''' </summary>
	<ServiceDependency()> _
	Public WriteOnly Property WorkItem() As WorkItem
		Set(ByVal value As WorkItem)
			composer.WorkItem = value
		End Set
	End Property

	''' <summary>
	''' Overriden to control when the workspace is being disposed to disable the control activation logic.
	''' </summary>
	''' <param name="disposing">A flag that indicates if <see cref="IDisposable.Dispose"/> was called.</param>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		isDisposing = disposing
		MyBase.Dispose(disposing)
	End Sub


#Region "Private"

	Private Sub ControlDisposed(ByVal sender As Object, ByVal e As EventArgs)
		Dim control As Control = TryCast(sender, Control)
		If Me.isDisposing = False AndAlso (Not control Is Nothing) Then
			composer.ForceClose(control)
		End If
	End Sub

	Private Sub FireSmartPartActivated(ByVal smartPart As Object)
		RaiseEvent SmartPartActivated(Me, New WorkspaceEventArgs(smartPart))
	End Sub

	Private Sub ActivateTopmost()
		If Me.Controls.Count <> 0 Then
			Activate(Me.Controls(0))
		End If
	End Sub

#End Region

#Region "Protected virtual implementation"

	''' <summary>
	''' Activates the smart part.
	''' </summary>
	Protected Overridable Sub OnActivate(ByVal smartPart As Control)
		'this.Controls.SetChildIndex(smartPart, this.Controls.Count - 1);
		smartPart.BringToFront()
		smartPart.Show()
	End Sub

	''' <summary>
	''' Applies the smart part display information to the smart part.
	''' </summary>
	Protected Overridable Sub OnApplySmartPartInfo(ByVal smartPart As Control, ByVal aSmartPartInfo As SmartPartInfo)
		' No op. We do not use the SPI for anything actually.
	End Sub

	''' <summary>
	''' Closes the smart part.
	''' </summary>
	Protected Overridable Sub OnClose(ByVal smartPart As Control)
		Me.Controls.Remove(smartPart)

		AddHandler smartPart.Disposed, AddressOf ControlDisposed

		ActivateTopmost()
	End Sub

	''' <summary>
	''' Hides the smart part.
	''' </summary>
	Protected Overridable Sub OnHide(ByVal smartPart As Control)
		smartPart.SendToBack()

		ActivateTopmost()
	End Sub

	''' <summary>
	''' Shows the control.
	''' </summary>
	Protected Overridable Sub OnShow(ByVal smartPart As Control, ByVal smartPartInfo As SmartPartInfo)
		smartPart.Dock = DockStyle.Fill

		Me.Controls.Add(smartPart)

		AddHandler smartPart.Disposed, AddressOf ControlDisposed
		Activate(smartPart)
	End Sub

	''' <summary>
	''' Raises the <see cref="SmartPartActivated"/> event.
	''' </summary>
	Protected Overridable Sub OnSmartPartActivated(ByVal e As WorkspaceEventArgs)
		RaiseEvent SmartPartActivated(Me, e)
	End Sub


	''' <summary>
	''' Raises the <see cref="SmartPartClosing"/> event.
	''' </summary>
	Protected Sub OnSmartPartClosing(ByVal e As WorkspaceCancelEventArgs)
		RaiseEvent SmartPartClosing(Me, e)
	End Sub

	''' <summary>
	''' Converts a smart part information to a compatible one for the workspace.
	''' </summary>
	Protected Overridable Function OnConvertFrom(ByVal source As ISmartPartInfo) As SmartPartInfo
		Return SmartPartInfo.ConvertTo(Of SmartPartInfo)(source)
	End Function

#End Region

#Region "IComposableWorkspace(Of Control, SmartPartInfo) Members"

	Sub OnActivateBase(ByVal smartPart As Control) Implements IComposableWorkspace(Of Control, SmartPartInfo).OnActivate
		OnActivate(smartPart)
	End Sub

	Sub OnApplySmartPartInfoBase(ByVal smartPart As Control, ByVal smartPartInfo As SmartPartInfo) Implements IComposableWorkspace(Of Control, SmartPartInfo).OnApplySmartPartInfo
		OnApplySmartPartInfo(smartPart, smartPartInfo)
	End Sub

	Sub OnCloseBase(ByVal smartPart As Control) Implements IComposableWorkspace(Of Control, SmartPartInfo).OnClose
		OnClose(smartPart)
	End Sub

	Sub OnHideBase(ByVal smartPart As Control) Implements IComposableWorkspace(Of Control, SmartPartInfo).OnHide
		OnHide(smartPart)
	End Sub

	Sub OnShowBase(ByVal smartPart As Control, ByVal smartPartInfo As SmartPartInfo) Implements IComposableWorkspace(Of Control, SmartPartInfo).OnShow
		OnShow(smartPart, SmartPartInfo)
	End Sub

	Sub RaiseSmartPartActivated(ByVal e As WorkspaceEventArgs) Implements IComposableWorkspace(Of Control, SmartPartInfo).RaiseSmartPartActivated
		OnSmartPartActivated(e)
	End Sub

	Sub RaiseSmartPartClosing(ByVal e As WorkspaceCancelEventArgs) Implements IComposableWorkspace(Of Control, SmartPartInfo).RaiseSmartPartClosing
		OnSmartPartClosing(e)
	End Sub

	Function ConvertFrom(ByVal source As ISmartPartInfo) As SmartPartInfo Implements IComposableWorkspace(Of Control, SmartPartInfo).ConvertFrom
		Return OnConvertFrom(source)
	End Function

#End Region

#Region "IWorkspace Members"

	''' <summary>
	''' See <see cref="IWorkspace.SmartPartClosing"/>.
	''' </summary>
	Public Event SmartPartClosing As EventHandler(Of WorkspaceCancelEventArgs) Implements IWorkspace.SmartPartClosing

	''' <summary>
	''' See <see cref="IWorkspace.SmartPartActivated"/>.
	''' </summary>
	Public Event SmartPartActivated As EventHandler(Of WorkspaceEventArgs) Implements IWorkspace.SmartPartActivated

	''' <summary>
	''' See <see cref="IWorkspace.SmartParts"/>.
	''' </summary>
	Private ReadOnly Property SmartPartsBase() As ReadOnlyCollection(Of Object) Implements IWorkspace.SmartParts
		Get
			Return composer.SmartParts
		End Get
	End Property

	''' <summary>
	''' See <see cref="IWorkspace.ActiveSmartPart"/>.
	''' </summary>
	Private ReadOnly Property ActiveSmartPartBase() As Object Implements IWorkspace.ActiveSmartPart
		Get
			Return composer.ActiveSmartPart
		End Get
	End Property

	''' <summary>
	''' Shows the smart part in a new tab with the given information.
	''' </summary>
	Public Overloads Sub Show(ByVal smartPart As Object, ByVal smartPartInfo As ISmartPartInfo) Implements IWorkspace.Show
		composer.Show(smartPart, smartPartInfo)
	End Sub

	''' <summary>
	''' Shows the smart part in a new tab.
	''' </summary>
	Public Overloads Sub Show(ByVal smartPart As Object) Implements IWorkspace.Show
		composer.Show(smartPart)
	End Sub

	''' <summary>
	''' Hides the smart part and its tab.
	''' </summary>
	Public Overloads Sub Hide(ByVal smartPart As Object) Implements IWorkspace.Hide
		composer.Hide(smartPart)
	End Sub

	''' <summary>
	''' Closes the smart part and removes its tab.
	''' </summary>
	Public Sub Close(ByVal smartPart As Object) Implements IWorkspace.Close
		composer.Close(smartPart)
	End Sub

	''' <summary>
	''' Activates the tab the smart part is contained in.
	''' </summary>
	''' <param name="smartPart"></param>
	Public Sub Activate(ByVal smartPart As Object) Implements IWorkspace.Activate
		composer.Activate(smartPart)
	End Sub

	''' <summary>
	''' Applies new layout information on the tab of the smart part.
	''' </summary>
	Public Sub ApplySmartPartInfo(ByVal smartPart As Object, ByVal smartPartInfo As ISmartPartInfo) Implements IWorkspace.ApplySmartPartInfo
		composer.ApplySmartPartInfo(smartPart, smartPartInfo)
	End Sub

#End Region

End Class
