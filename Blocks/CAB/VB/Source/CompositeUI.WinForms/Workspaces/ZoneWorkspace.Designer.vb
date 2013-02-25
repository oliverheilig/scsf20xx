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
Imports System.Windows.Forms
Imports System
Imports System.Drawing

<ProvideProperty("ZoneName", GetType(ScrollableControl)), ProvideProperty("IsDefaultZone", GetType(ScrollableControl)), ToolboxBitmap(GetType(ZoneWorkspace), "ZoneWorkspace")> _
 Partial Public Class ZoneWorkspace
	Implements IExtenderProvider

	''' <summary>
	''' Zone name assigned to a given control. Can only be assigned to controls that are 
	''' contained in the <see cref="ZoneWorkspace"/>.
	''' </summary>
	''' <param name="target">Control to retrieve the zone name for.</param>
	''' <returns>The name of the zone assigned to the control, or null.</returns>
	<Category("Layout"), DefaultValue(CType(Nothing, String)), DisplayName("ZoneName"), Description("Zone name assigned to the control so that smart parts can be shown on it programmatically.")> _
	Public Function GetZoneName(ByVal target As Control) As String
		If (Not zonesByControl.ContainsKey(target)) Then
			Return Nothing
		Else
			Return zonesByControl(target)
		End If
	End Function

	''' <summary>
	''' Zone name assigned to a given control. Can only be assigned to controls that are 
	''' contained in the <see cref="ZoneWorkspace"/>.
	''' </summary>
	''' <param name="target">Control to set the zone name to.</param>
	''' <param name="name">The name of the zone to assign to the control.</param>
	''' <returns>The name of the zone to assign to the control.</returns>
	<DisplayName("ZoneName")> _
	Public Sub SetZoneName(ByVal target As Control, ByVal name As String)
		If IsDescendant(target) Then
			If zonesByControl.ContainsKey(target) Then
				Dim oldname As String = zonesByControl(target)
				zonesByName.Remove(oldname)
				zonesByControl.Remove(target)
			ElseIf String.IsNullOrEmpty(name) = False Then
				AddHandler target.ParentChanged, AddressOf OnZoneParentChanged
			End If

			If String.IsNullOrEmpty(name) = False Then
				zonesByControl(target) = name
				zonesByName(name) = target
			End If
		End If
	End Sub

	''' <summary>
	''' Determines whether the zone is the default one. Only one zone can be the default and 
	''' only controls that are contained in the <see cref="ZoneWorkspace"/> can be set as default zones.
	''' </summary>
	''' <param name="target">Control to specify as the default one.</param>
	''' <returns>true if the zone is the default one, false otherwise.</returns>
	<Category("Layout"), DefaultValue(False), DisplayName("IsDefaultZone"), Description("Specifies whether the zone is the default one for showing smart parts.")> _
	Public Function GetIsDefaultZone(ByVal target As Control) As Boolean
		Return defaultZone Is target
	End Function

	''' <summary>
	''' Determines whether the zone is the default one. Only one zone can be the default and 
	''' only controls that are contained in the <see cref="ZoneWorkspace"/> can be set as default zones.
	''' </summary>
	''' <param name="target">Control to specify as the default one.</param>
	''' <param name="isDefault">true if the zone is the default one, false otherwise.</param>
	<DisplayName("IsDefaultZone")> _
	Public Sub SetIsDefaultZone(ByVal target As Control, ByVal isDefault As Boolean)
		If isDefault Then
			defaultZone = target
		Else
			defaultZone = Nothing
		End If
	End Sub

	Private Function CanExtend(ByVal extendee As Object) As Boolean Implements IExtenderProvider.CanExtend
		Return TypeOf extendee Is ScrollableControl AndAlso IsDescendant(CType(extendee, Control))
	End Function

	' Fix design-time.
	''' <summary>
	''' Refreshes the values on the control at design-time.
	''' </summary>
	''' <param name="e">The control being modified.</param>
	<EditorBrowsable(EditorBrowsableState.Advanced)> _
	Protected Overrides Sub OnControlAdded(ByVal e As ControlEventArgs)
		MyBase.OnControlAdded(e)
		RefreshRecursive(e.Control)
	End Sub

	''' <summary>
	''' Refreshes the values on the control at design-time.
	''' </summary>
	''' <param name="e">The control being modified.</param>
	<EditorBrowsable(EditorBrowsableState.Advanced)> _
	Protected Overrides Sub OnControlRemoved(ByVal e As ControlEventArgs)
		MyBase.OnControlRemoved(e)

		If DesignMode = True Then
			RefreshRecursive(e.Control)
		Else
			RemoveControlFromDictionaries(e.Control)
		End If
	End Sub

	Private Sub RefreshRecursive(ByVal control As Control)
		If DesignMode Then
			' If the refresh operations are not called explicitly, 
			' controls will never be properly parented.
			TypeDescriptor.Refresh(control)
			For Each child As Control In control.Controls
				RefreshRecursive(child)
			Next child
		End If
	End Sub
End Class

