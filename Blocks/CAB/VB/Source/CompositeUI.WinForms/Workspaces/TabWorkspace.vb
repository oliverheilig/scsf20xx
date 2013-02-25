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
Imports System.Drawing
Imports System.Windows.Forms
Imports Microsoft.Practices.CompositeUI.SmartParts
Imports Microsoft.Practices.CompositeUI.Utility
Imports System.Collections.ObjectModel

''' <summary>
''' Implements a Workspace that shows smartparts in a <see cref="TabControl"/>.
''' </summary>
Public Class TabWorkspace
	Inherits TabControl
	Implements IComposableWorkspace(Of Control, TabSmartPartInfo)

	Private innerPages As Dictionary(Of Control, TabPage) = New Dictionary(Of Control, TabPage)()
	Private composer As WorkspaceComposer(Of Control, TabSmartPartInfo)
	Private callComposerActivateOnIndexChange As Boolean = True
	Private populatingPages As Boolean = False

	''' <summary>
	''' Initializes a new instance of the <see cref="TabWorkspace"/> class
	''' </summary>
	Public Sub New()
		composer = New WorkspaceComposer(Of Control, TabSmartPartInfo)(Me)
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

#Region "Properties"

	''' <summary>
	''' Gets the collection of pages that the tab workspace uses.
	''' </summary>
	Public ReadOnly Property Pages() As ReadOnlyDictionary(Of Control, TabPage)
		Get
			Return New ReadOnlyDictionary(Of Control, TabPage)(innerPages)
		End Get
	End Property

#End Region

#Region "Private"

	Private Sub SetTabProperties(ByVal page As TabPage, ByVal smartPartInfo As TabSmartPartInfo)
		If String.IsNullOrEmpty(smartPartInfo.Title) Then
			page.Text = page.Text
		Else
			page.Text = smartPartInfo.Title
		End If

		Try
			Dim currentSelection As TabPage = Me.SelectedTab
			callComposerActivateOnIndexChange = False
			If smartPartInfo.Position = TabPosition.Beginning Then
				Dim pages As TabPage() = GetTabPages()
				Me.TabPages.Clear()

				Me.TabPages.Add(page)
				Me.TabPages.AddRange(pages)
			ElseIf Me.TabPages.Contains(page) = False Then
				Me.TabPages.Add(page)
			End If

			' Preserve selection through the operation.
			Me.SelectedTab = currentSelection
		Finally
			callComposerActivateOnIndexChange = True
		End Try
	End Sub

	Private Function GetTabPages() As TabPage()
		Dim pages As TabPage() = New TabPage(Me.TabPages.Count - 1) {}
		Dim i As Integer = 0
		Do While i < pages.Length
			pages(i) = Me.TabPages(i)
			i += 1
		Loop

		Return pages
	End Function

	Private Sub ShowExistingTab(ByVal smartPart As Control)
		Dim key As String = innerPages(smartPart).Name
		Me.TabPages(key).Show()
	End Sub

	Private Function GetOrCreateTabPage(ByVal smartPart As Control) As TabPage
		Dim page As TabPage = Nothing

		' If the tab was added with the control at design-time, it will have a parent control, 
		' and somewhere up its containment chain we'll find one of our tabs.
		Dim current As Control = smartPart
		Do While Not current Is Nothing AndAlso page Is Nothing
			current = current.Parent
			page = TryCast(current, TabPage)
		Loop

		If page Is Nothing Then
			page = New TabPage()
			page.Controls.Add(smartPart)
			smartPart.Dock = DockStyle.Fill
			page.Name = Guid.NewGuid().ToString()

			innerPages.Add(smartPart, page)
		ElseIf innerPages.ContainsKey(smartPart) = False Then
			innerPages.Add(smartPart, page)
		End If

		Return page
	End Function

	Private Sub PopulatePages()
		' If the page count matches don't waste the 
		' time repopulating the pages collection
		If (Not populatingPages) AndAlso innerPages.Count <> Me.TabPages.Count Then
			For Each page As TabPage In Me.TabPages
				If Me.innerPages.ContainsValue(page) = False Then
					Dim control As Control = GetControlFromPage(page)
					If Not control Is Nothing AndAlso composer.SmartParts.Contains(control) = False Then
						Dim tabinfo As TabSmartPartInfo = New TabSmartPartInfo()
						tabinfo.ActivateTab = False
						' Avoid circular calls to this method.
						populatingPages = True
						Try
							Show(control, tabinfo)

						Finally
							populatingPages = False
						End Try
					End If
				End If
			Next page
		End If
	End Sub

	Private Sub ControlDisposed(ByVal sender As Object, ByVal e As EventArgs)
		Dim control As Control = TryCast(sender, Control)
		If Not control Is Nothing AndAlso Me.innerPages.ContainsKey(control) Then
			composer.ForceClose(control)
		End If
	End Sub

	Private Function GetControlFromSelectedPage() As Control
		Return GetControlFromPage(Me.SelectedTab)
	End Function

	Private Function GetControlFromPage(ByVal page As TabPage) As Control
		Dim control As Control = Nothing
		If page.Controls.Count > 0 Then
			control = page.Controls(0)
		End If

		Return control
	End Function

#End Region

#Region "Internal implementation"

	''' <summary>
	''' Fires the <see cref="SmartPartActivated"/> event whenever 
	''' the selected tab index changes.
	''' </summary>
	''' <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
	Protected Overrides Sub OnSelectedIndexChanged(ByVal e As EventArgs)
		MyBase.OnSelectedIndexChanged(e)
		If callComposerActivateOnIndexChange AndAlso TabPages.Count <> 0 Then
			' Locate the smart part corresponding to the page.
			For Each pair As KeyValuePair(Of Control, TabPage) In innerPages
				If pair.Value Is Me.SelectedTab Then
					DirectCast(Me, IComposableWorkspace(Of Control, TabSmartPartInfo)).Activate(pair.Key)
					Return
				End If
			Next pair

			' If we got here, we couldn't find a corresponding smart part for the 
			' currently active tab, hence we reset the ActiveSmartPart value.
			composer.SetActiveSmartPart(Nothing)
		End If
	End Sub

	''' <summary>
	''' Hooks up tab pages added at design-time.
	''' </summary>
	Protected Overrides Sub OnCreateControl()
		MyBase.OnCreateControl()
		PopulatePages()
	End Sub

	Private Sub ActivateSiblingTab()
		If Me.SelectedIndex > 0 Then
			Me.SelectedIndex = Me.SelectedIndex - 1
		ElseIf Me.SelectedIndex < Me.TabPages.Count - 1 Then
			Me.SelectedIndex = Me.SelectedIndex + 1
		Else
			composer.SetActiveSmartPart(Nothing)
		End If
	End Sub

	Private Sub ResetSelectedIndexIfNoTabs()
		' First control to come in is special. We need to 
		' set the selected index to a non-zero index so we 
		' get the appropriate behavior for activation.
		If Me.TabPages.Count = 0 Then
			Try
				callComposerActivateOnIndexChange = False
				Me.SelectedIndex = -1
			Finally
				callComposerActivateOnIndexChange = True
			End Try
		End If
	End Sub

#End Region

#Region "Protected virtual implementations"

	''' <summary>
	''' Activates the smart part.
	''' </summary>
	Protected Overridable Sub OnActivate(ByVal smartPart As Control)
		PopulatePages()

		Dim key As String = innerPages(smartPart).Name

		Try
			callComposerActivateOnIndexChange = False
			SelectedTab = TabPages(key)
			TabPages(key).Show()
		Finally
			callComposerActivateOnIndexChange = True
		End Try
	End Sub

	''' <summary>
	''' Applies the smart part display information to the smart part.
	''' </summary>
	Protected Overridable Sub OnApplySmartPartInfo(ByVal smartPart As Control, ByVal smartPartInfo As TabSmartPartInfo)
		PopulatePages()
		Dim key As String = innerPages(smartPart).Name
		SetTabProperties(TabPages(key), smartPartInfo)
		If smartPartInfo.ActivateTab Then
			Activate(smartPart)
		End If
	End Sub

	''' <summary>
	''' Closes the smart part.
	''' </summary>
	Protected Overridable Sub OnClose(ByVal smartPart As Control)
		PopulatePages()
		TabPages.Remove(innerPages(smartPart))
		innerPages.Remove(smartPart)

		RemoveHandler smartPart.Disposed, AddressOf ControlDisposed
		'smartPart.Dispose();
	End Sub

	''' <summary>
	''' Hides the smart part.
	''' </summary>
	Protected Overridable Sub OnHide(ByVal smartPart As Control)
		If smartPart.Visible Then
			PopulatePages()
			Dim key As String = innerPages(smartPart).Name
			TabPages(key).Hide()
			ActivateSiblingTab()
		End If
	End Sub

	''' <summary>
	''' Shows the control.
	''' </summary>
	Protected Overridable Sub OnShow(ByVal smartPart As Control, ByVal smartPartInfo As TabSmartPartInfo)
		PopulatePages()
		ResetSelectedIndexIfNoTabs()

		Dim page As TabPage = GetOrCreateTabPage(smartPart)
		SetTabProperties(page, smartPartInfo)

		If smartPartInfo.ActivateTab Then
			Activate(smartPart)
		End If

		AddHandler smartPart.Disposed, AddressOf ControlDisposed
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
	Protected Overridable Function ConvertFrom(ByVal source As ISmartPartInfo) As TabSmartPartInfo
		Return SmartPartInfo.ConvertTo(Of TabSmartPartInfo)(source)
	End Function

#End Region

#Region "IComposableWorkspace<Control,TabSmartPartInfo> Members"

	''' <summary>
	''' See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.OnActivate"/> for more information.
	''' </summary>
	Private Sub OnActivateBase(ByVal smartPart As Control) Implements IComposableWorkspace(Of Control, TabSmartPartInfo).OnActivate
		OnActivate(smartPart)
	End Sub

	''' <summary>
	''' See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.OnApplySmartPartInfo"/> for more information.
	''' </summary>
	Private Sub OnApplySmartPartInfoBase(ByVal smartPart As Control, ByVal smartPartInfo As TabSmartPartInfo) Implements IComposableWorkspace(Of Control, TabSmartPartInfo).OnApplySmartPartInfo
		OnApplySmartPartInfo(smartPart, smartPartInfo)
	End Sub

	''' <summary>
	''' See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.OnShow"/> for more information.
	''' </summary>
	Private Sub OnShowBase(ByVal smartPart As Control, ByVal smartPartInfo As TabSmartPartInfo) Implements IComposableWorkspace(Of Control, TabSmartPartInfo).OnShow
		OnShow(smartPart, smartPartInfo)
	End Sub

	''' <summary>
	''' See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.OnHide"/> for more information.
	''' </summary>
	Private Sub OnHideBase(ByVal smartPart As Control) Implements IComposableWorkspace(Of Control, TabSmartPartInfo).OnHide
		OnHide(smartPart)
	End Sub

	''' <summary>
	''' See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.OnClose"/> for more information.
	''' </summary>
	Private Sub OnCloseBase(ByVal smartPart As Control) Implements IComposableWorkspace(Of Control, TabSmartPartInfo).OnClose
		OnClose(smartPart)
	End Sub

	''' <summary>
	''' See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.RaiseSmartPartActivated"/> for more information.
	''' </summary>
	Private Sub RaiseSmartPartActivated(ByVal e As WorkspaceEventArgs) Implements IComposableWorkspace(Of Control, TabSmartPartInfo).RaiseSmartPartActivated
		OnSmartPartActivated(e)
	End Sub

	''' <summary>
	''' See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.RaiseSmartPartClosing"/> for more information.
	''' </summary>
	Private Sub RaiseSmartPartClosing(ByVal e As WorkspaceCancelEventArgs) Implements IComposableWorkspace(Of Control, TabSmartPartInfo).RaiseSmartPartClosing
		OnSmartPartClosing(e)
	End Sub

	''' <summary>
	''' See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.ConvertFrom"/> for more information.
	''' </summary>
	Private Function ConvertFromBase(ByVal source As ISmartPartInfo) As TabSmartPartInfo Implements IComposableWorkspace(Of Control, TabSmartPartInfo).ConvertFrom
		Return SmartPartInfo.ConvertTo(Of TabSmartPartInfo)(source)
	End Function

#End Region

#Region "IWorkspace Members"

	''' <summary>
	''' See <see cref="IWorkspace.SmartPartClosing"/> for more information.
	''' </summary>
	Public Event SmartPartClosing As EventHandler(Of WorkspaceCancelEventArgs) Implements IComposableWorkspace(Of Control, TabSmartPartInfo).SmartPartClosing

	''' <summary>
	''' See <see cref="IWorkspace.SmartPartActivated"/> for more information.
	''' </summary>
	'Public Event SmartPartActivated As EventHandler(Of WorkspaceEventArgs) Implements IComposableWorkspace(Of Control, TabSmartPartInfo).SmartPartActivated
	Public Event SmartPartActivated As EventHandler(Of WorkspaceEventArgs) Implements IComposableWorkspace(Of Control, TabSmartPartInfo).SmartPartActivated

	''' <summary>
	''' See <see cref="IWorkspace.SmartParts"/> for more information.
	''' </summary>
	<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
	Public ReadOnly Property SmartParts() As ReadOnlyCollection(Of Object) Implements IComposableWorkspace(Of Control, TabSmartPartInfo).SmartParts
		Get
			Return composer.SmartParts
		End Get
	End Property

	''' <summary>
	''' See <see cref="IWorkspace.ActiveSmartPart"/> for more information.
	''' </summary>
	<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
	Public ReadOnly Property ActiveSmartPart() As Object Implements IComposableWorkspace(Of Control, TabSmartPartInfo).ActiveSmartPart
		Get
			Return composer.ActiveSmartPart
		End Get
	End Property

	''' <summary>
	''' Shows the smart part in a new tab with the given information.
	''' </summary>
	Public Overloads Sub Show(ByVal smartPart As Object, ByVal smartPartInfo As ISmartPartInfo) Implements IComposableWorkspace(Of Control, TabSmartPartInfo).Show
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
	Public Sub Close(ByVal smartPart As Object) Implements IComposableWorkspace(Of Control, TabSmartPartInfo).Close
		composer.Close(smartPart)
	End Sub

	''' <summary>
	''' Activates the tab the smart part is contained in.
	''' </summary>
	''' <param name="smartPart"></param>
	Public Sub Activate(ByVal smartPart As Object) Implements IComposableWorkspace(Of Control, TabSmartPartInfo).Activate
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
