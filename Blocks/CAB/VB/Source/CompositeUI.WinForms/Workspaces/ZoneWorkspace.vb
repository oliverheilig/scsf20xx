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
Imports System.Globalization
Imports System.Windows.Forms
Imports Microsoft.Practices.CompositeUI.SmartParts
Imports Microsoft.Practices.CompositeUI.Utility
Imports System.Collections.ObjectModel

''' <summary>
''' Implements a Workspace that can contain named zones where 
''' controls can be shown.
''' </summary>
''' <remarks>
''' This workspace is intended to be used in a designer. If programmatic manipulation of zone definition 
''' is required, instead of directly adding zones to the <see cref="Zones"/> property, the following 
''' operations should be performed in order to get proper behavior:
''' <para>
''' 1 - The control must be added as a child control of the workspace, probably inside a splitter container.
''' </para>
''' <para>
''' 2 - The <see cref="SetZoneName"/> must be called passing the control and the desired zone name.
''' </para>
''' </remarks>
Partial Public Class ZoneWorkspace
	Inherits Panel
	Implements IComposableWorkspace(Of Control, ZoneSmartPartInfo), ISupportInitialize

	Private zonesByControl As Dictionary(Of Control, String) = New Dictionary(Of Control, String)()
	Private zonesByName As Dictionary(Of String, Control) = New Dictionary(Of String, Control)()
	Private defaultZone As Control = Nothing
	Private composer As WorkspaceComposer(Of Control, ZoneSmartPartInfo)

	''' <summary>
	''' Initializes a new instance of the <see cref="ZoneWorkspace"/> class
	''' </summary>
	Public Sub New()
		composer = New WorkspaceComposer(Of Control, ZoneSmartPartInfo)(Me)
	End Sub

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
	''' Dictionary of zones that have been given names. 
	''' </summary>
	<Browsable(False)> _
	Public ReadOnly Property Zones() As ReadOnlyDictionary(Of String, Control)
		Get
			Return New ReadOnlyDictionary(Of String, Control)(zonesByName)
		End Get
	End Property

#Region "Private Methods"

	Private Sub RemoveControlFromDictionaries(ByVal control As Control)
		If Me.zonesByControl.ContainsKey(control) Then
			Me.zonesByName.Remove(GetZoneName(control))
			Me.zonesByControl.Remove(control)
		Else
			Dim keys As Control() = New Control(zonesByControl.Keys.Count - 1) {}
			zonesByControl.Keys.CopyTo(keys, 0)

			For Each key As Control In keys
				If IsDescendant(control, key) = True Then
					Me.zonesByName.Remove(GetZoneName(key))
					Me.zonesByControl.Remove(key)
					RemoveHandler key.ParentChanged, AddressOf OnZoneParentChanged

					For Each sp As Control In key.Controls
						Close(sp)
					Next sp
				End If
			Next key

		End If
	End Sub

	Private Sub SmartPartEntered(ByVal sender As Object, ByVal e As EventArgs)
		Dim control As Control = TryCast(sender, Control)
		If Not control Is Nothing Then
			Dim workspace As IComposableWorkspace(Of Control, ZoneSmartPartInfo) = Me
			workspace.Activate(control)
		Else
			composer.SetActiveSmartPart(Nothing)
		End If
	End Sub

	Private Sub OnZoneParentChanged(ByVal sender As Object, ByVal e As EventArgs)
		Dim control As Control = TryCast(sender, Control)
		If Not control Is Nothing Then
			If IsDescendant(control) = False Then
				If zonesByControl.ContainsKey(control) Then
					Dim name As String = zonesByControl(control)
					zonesByName.Remove(name)
					zonesByControl.Remove(control)
					For Each sp As Control In control.Controls
						Close(sp)
					Next sp
					RemoveHandler control.ParentChanged, AddressOf OnZoneParentChanged
				End If
			End If
		End If
	End Sub

	Private Function IsDescendant(ByVal target As Control) As Boolean
		Return IsDescendant(Me, target)
	End Function

	Private Function IsDescendant(ByVal parent As Control, ByVal child As Control) As Boolean
		Dim childParent As Control = child.Parent
		Do While Not childParent Is Nothing AndAlso Not childParent Is parent
			childParent = childParent.Parent
		Loop

		Return childParent Is parent
	End Function

	Private Function GetTargetZone(ByVal info As ZoneSmartPartInfo) As Control
		Dim zone As Control = defaultZone
		If Not info Is Nothing AndAlso Not info.ZoneName Is Nothing Then
			If zonesByName.ContainsKey(info.ZoneName) = False Then
				Throw New ArgumentOutOfRangeException("ZoneName", String.Format(CultureInfo.CurrentCulture, My.Resources.NoZoneWithName, info.ZoneName))
			End If

			zone = zonesByName(info.ZoneName)
		End If

		If zone Is Nothing Then
			For Each pair As KeyValuePair(Of String, Control) In zonesByName
				zone = pair.Value
				Exit For
			Next pair
		End If
		Return zone
	End Function

	Private Sub AddControlToZone(ByVal control As Control, ByVal info As ZoneSmartPartInfo)
		Dim zone As Control = GetTargetZone(info)
		If info.Dock.HasValue Then
			control.Dock = info.Dock.Value
		End If

		zone.Controls.Add(control)
	End Sub

	Private Sub ControlDisposed(ByVal sender As Object, ByVal e As EventArgs)
		Dim control As Control = TryCast(sender, Control)
		If Not control Is Nothing Then
			composer.ForceClose(control)
		End If
	End Sub

#End Region

#Region "Virtual implementation members"

	''' <summary>
	''' Activates the smart part.
	''' </summary>
	Protected Overridable Sub OnActivate(ByVal smartPart As Control)
		smartPart.Show()
		RemoveHandler smartPart.Enter, AddressOf SmartPartEntered
		Try
			smartPart.Focus()
		Finally
			AddHandler smartPart.Enter, AddressOf SmartPartEntered
		End Try
	End Sub

	''' <summary>
	''' Applies the smart part display information to the smart part.
	''' </summary>
	Protected Overridable Sub OnApplySmartPartInfo(ByVal smartPart As Control, ByVal smartPartInfo As ZoneSmartPartInfo)
		AddControlToZone(smartPart, smartPartInfo)
	End Sub

	''' <summary>
	''' Closes the smart part.
	''' </summary>
	Protected Overridable Sub OnClose(ByVal smartPart As Control)
		RemoveHandler smartPart.Disposed, AddressOf ControlDisposed
		RemoveHandler smartPart.Enter, AddressOf SmartPartEntered

		If Not smartPart.Parent Is Nothing Then
			smartPart.Parent.Controls.Remove(smartPart)
		End If
	End Sub

	''' <summary>
	''' Hides the smart part.
	''' </summary>
	Protected Overridable Sub OnHide(ByVal smartPart As Control)
		smartPart.Hide()
	End Sub

	''' <summary>
	''' Shows the control.
	''' </summary>
	Protected Overridable Sub OnShow(ByVal smartPart As Control, ByVal smartPartInfo As ZoneSmartPartInfo)
		If zonesByName.Count = 0 Then
			Throw New InvalidOperationException(My.Resources.NoZonesInZoneWorkspace)
		End If
		AddHandler smartPart.Disposed, AddressOf ControlDisposed
		AddControlToZone(smartPart, smartPartInfo)
		Activate(smartPart)

	End Sub

	''' <summary>
	''' Raises the <see cref="SmartPartActivated"/> event.
	''' </summary>
	Protected Overridable Sub OnSmartPartActivated(ByVal e As WorkspaceEventArgs)
		If Not SmartPartActivatedEvent Is Nothing Then
			RaiseEvent SmartPartActivated(Me, e)
		End If
	End Sub

	''' <summary>
	''' Raises the <see cref="SmartPartClosing"/> event.
	''' </summary>
	Protected Overridable Sub OnSmartPartClosing(ByVal e As WorkspaceCancelEventArgs)
		If Not SmartPartClosingEvent Is Nothing Then
			RaiseEvent SmartPartClosing(Me, e)
		End If
	End Sub

	''' <summary>
	''' Converts a smart part information to a compatible one for the workspace.
	''' </summary>
	Protected Overridable Function OnConvertFrom(ByVal source As ISmartPartInfo) As ZoneSmartPartInfo
		Return SmartPartInfo.ConvertTo(Of ZoneSmartPartInfo)(source)
	End Function

#End Region

#Region "IComposableWorkspace<Control,ZoneSmartPartInfo> Members"

	''' <summary>
	''' See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.OnActivate"/> for more information.
	''' </summary>
	Private Sub OnActivateBase(ByVal smartPart As Control) Implements IComposableWorkspace(Of Control, ZoneSmartPartInfo).OnActivate
		OnActivate(smartPart)
	End Sub

	''' <summary>
	''' See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.OnApplySmartPartInfo"/> for more information.
	''' </summary>
	Private Sub OnApplySmartPartInfoBase(ByVal smartPart As Control, ByVal smartPartInfo As ZoneSmartPartInfo) Implements IComposableWorkspace(Of Control, ZoneSmartPartInfo).OnApplySmartPartInfo
		OnApplySmartPartInfo(smartPart, smartPartInfo)
	End Sub

	''' <summary>
	''' See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.OnShow"/> for more information.
	''' </summary>
	Private Sub OnShowBase(ByVal smartPart As Control, ByVal smartPartInfo As ZoneSmartPartInfo) Implements IComposableWorkspace(Of Control, ZoneSmartPartInfo).OnShow
		OnShow(smartPart, smartPartInfo)
	End Sub

	''' <summary>
	''' See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.OnHide"/> for more information.
	''' </summary>
	Private Sub OnHideBase(ByVal smartPart As Control) Implements IComposableWorkspace(Of Control, ZoneSmartPartInfo).OnHide
		OnHide(smartPart)
	End Sub

	''' <summary>
	''' See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.OnClose"/> for more information.
	''' </summary>
	Private Sub OnCloseBase(ByVal smartPart As Control) Implements IComposableWorkspace(Of Control, ZoneSmartPartInfo).OnClose
		OnClose(smartPart)
	End Sub

	''' <summary>
	''' See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.RaiseSmartPartActivated"/> for more information.
	''' </summary>
	Private Sub RaiseSmartPartActivated(ByVal e As WorkspaceEventArgs) Implements IComposableWorkspace(Of Control, ZoneSmartPartInfo).RaiseSmartPartActivated
		OnSmartPartActivated(e)
	End Sub

	''' <summary>
	''' See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.RaiseSmartPartClosing"/> for more information.
	''' </summary>
	Private Sub RaiseSmartPartClosing(ByVal e As WorkspaceCancelEventArgs) Implements IComposableWorkspace(Of Control, ZoneSmartPartInfo).RaiseSmartPartClosing
		OnSmartPartClosing(e)
	End Sub

	''' <summary>
	''' See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.ConvertFrom"/> for more information.
	''' </summary>
	Private Function ConvertFrom(ByVal source As ISmartPartInfo) As ZoneSmartPartInfo Implements IComposableWorkspace(Of Control, ZoneSmartPartInfo).ConvertFrom
		Return OnConvertFrom(source)
	End Function

#End Region

#Region "IWorkspace Members"

	''' <summary>
	''' See <see cref="IWorkspace.SmartPartClosing"/> for more information.
	''' </summary>
	Public Event SmartPartClosing As EventHandler(Of WorkspaceCancelEventArgs) Implements IWorkspace.SmartPartClosing

	''' <summary>
	''' See <see cref="IWorkspace.SmartPartActivated"/> for more information.
	''' </summary>
	Public Event SmartPartActivated As EventHandler(Of WorkspaceEventArgs) Implements IWorkspace.SmartPartActivated

	''' <summary>
	''' See <see cref="IWorkspace.SmartParts"/> for more information.
	''' </summary>
	Public ReadOnly Property SmartParts() As ReadOnlyCollection(Of Object) Implements IWorkspace.SmartParts
		Get
			Return composer.SmartParts
		End Get
	End Property

	''' <summary>
	''' See <see cref="IWorkspace.ActiveSmartPart"/> for more information.
	''' </summary>
	Public ReadOnly Property ActiveSmartPart() As Object Implements IWorkspace.ActiveSmartPart
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

#Region "ISupportInitialize Members"

	Private Sub BeginInit() Implements ISupportInitialize.BeginInit
		OnBeginInit()
	End Sub

	Private Sub EndInit() Implements ISupportInitialize.EndInit
		OnEndInit()
	End Sub

	''' <summary>
	''' Begins the initialization of the workspace in a designer-generated 
	''' block of code (typically InitializeComponent method).
	''' </summary>
	Protected Overridable Sub OnBeginInit()
	End Sub

	''' <summary>
	''' Ends the initialization of the workspace in a designer-generated 
	''' block of code (typically InitializeComponent method).
	''' </summary>
	Protected Overridable Sub OnEndInit()
		' Treat top-level controls inside named zones as smart parts too.
		For Each zone As KeyValuePair(Of String, Control) In zonesByName
			For Each child As Control In zone.Value.Controls
				' Only set the zone name, so that the remaining information stays the same
				' as specified at design-time. ZoneName is required so that the Show is 
				' performed on the proper zone (should be a no-op).
				Dim zoneInfo As ZoneSmartPartInfo = New ZoneSmartPartInfo(zone.Key)
				Show(child, zoneInfo)
			Next child
		Next zone
	End Sub

#End Region
End Class
