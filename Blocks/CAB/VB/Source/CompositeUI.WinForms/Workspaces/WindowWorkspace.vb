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

''' <summary>
''' Implements a Workspace that shows smartparts in windows.
''' </summary>
Public Class WindowWorkspace
	Inherits Workspace(Of Control, WindowSmartPartInfo)

	Private windowDictionary As Dictionary(Of Control, Form) = New Dictionary(Of Control, Form)()
	Private fireActivatedFromForm As Boolean = True
	Private ownerForm As IWin32Window

	''' <summary>
	''' Initializes the workspace with no owner form to use to show new 
	''' windows.
	''' </summary>
	Public Sub New()
	End Sub

	''' <summary>
	''' Initializes the workspace with the form to use as the owner of 
	''' all windows shown through the workspace.
	''' </summary>
	''' <param name="ownerForm">The owner of windows shown through the workspace</param>
	Public Sub New(ByVal ownerForm As IWin32Window)
		Me.ownerForm = ownerForm
	End Sub

	''' <summary>
	''' Read-only view of WindowDictionary.
	''' </summary>
	<Browsable(False)> _
	Public ReadOnly Property Windows() As ReadOnlyDictionary(Of Control, Form)
		Get
			Return New ReadOnlyDictionary(Of Control, Form)(windowDictionary)
		End Get
	End Property

#Region "Protected"

	''' <summary>
	''' Creates a form if it does not already exist and adds the given control.
	''' </summary>
	''' <param name="control"></param>
	''' <returns></returns>
	Protected Function GetOrCreateForm(ByVal control As Control) As Form
		Dim form As WindowForm
		If Me.windowDictionary.ContainsKey(control) Then
			form = CType(Me.windowDictionary(control), WindowForm)
		Else
			form = New WindowForm()
			Me.windowDictionary.Add(control, form)
			form.Controls.Add(control)
			CalculateSize(control, form)
			AddHandler control.Disposed, AddressOf ControlDisposed
			WireUpForm(form)
		End If

		Return form
	End Function

	''' <summary>
	''' Sets specific properties for the given form.
	''' </summary>
	Protected Sub SetWindowProperties(ByVal form As Form, ByVal info As WindowSmartPartInfo)
		form.Text = info.Title
		If info.Width <> 0 Then
			form.Width = info.Width
		Else
			form.Width = form.Width
		End If
		If info.Height <> 0 Then
			form.Height = info.Height
		Else
			form.Height = form.Height
		End If
		form.ControlBox = info.ControlBox
		form.MaximizeBox = info.MaximizeBox
		form.MinimizeBox = info.MinimizeBox
		form.Icon = info.Icon
		form.Location = info.Location
	End Sub

	''' <summary>
	''' Sets the location information for the given form.
	''' </summary>
	Protected Sub SetWindowLocation(ByVal form As Form, ByVal info As WindowSmartPartInfo)
		form.Location = info.Location
	End Sub

#End Region

#Region "Private"

	Private Sub ControlDisposed(ByVal sender As Object, ByVal e As EventArgs)
		Dim control As Control = TryCast(sender, Control)
		If Not control Is Nothing AndAlso MyBase.SmartParts.Contains(sender) Then
			CloseInternal(control)
		End If
	End Sub

	Private Sub WireUpForm(ByVal form As WindowForm)
		AddHandler form.WindowFormClosing, New EventHandler(Of WorkspaceCancelEventArgs)(AddressOf WindowFormClosing)
		AddHandler form.WindowFormClosed, New EventHandler(Of WorkspaceEventArgs)(AddressOf WindowFormClosed)
		AddHandler form.WindowFormActivated, New EventHandler(Of WorkspaceEventArgs)(AddressOf WindowFormActivated)
	End Sub

	Private Sub WindowFormActivated(ByVal sender As Object, ByVal e As WorkspaceEventArgs)
		If fireActivatedFromForm Then
			RaiseSmartPartActivated(e.SmartPart)
			MyBase.SetActiveSmartPart(e.SmartPart)
		End If
	End Sub

	Private Sub WindowFormClosed(ByVal sender As Object, ByVal e As WorkspaceEventArgs)
		RemoveEntry(CType(e.SmartPart, Control))
		MyBase.InnerSmartParts.Remove(CType(e.SmartPart, Control))
	End Sub

	Private Sub WindowFormClosing(ByVal sender As Object, ByVal e As WorkspaceCancelEventArgs)
		MyBase.RaiseSmartPartClosing(e)
	End Sub

	Private Sub CalculateSize(ByVal smartPart As Control, ByVal form As Form)
		form.Size = New Size(smartPart.Size.Width, smartPart.Size.Height + 20)
	End Sub

	Private Sub RemoveEntry(ByVal spcontrol As Control)
		Me.windowDictionary.Remove(spcontrol)
	End Sub

	Private Sub ShowForm(ByVal form As Form, ByVal smartPartInfo As WindowSmartPartInfo)
		SetWindowProperties(form, smartPartInfo)

		If smartPartInfo.Modal = True Then
			SetWindowLocation(form, smartPartInfo)
			' Argument can be null. It's the default for the other overload.
			form.ShowDialog(ownerForm)
		Else
			' Call changes if no owner is specified.
			If Not ownerForm Is Nothing Then
				form.Show(ownerForm)
			Else
				form.Show()
			End If
			SetWindowLocation(form, smartPartInfo)
			form.BringToFront()
		End If
	End Sub

#End Region

#Region "Private Form Class"

	''' <summary>
	''' WindowForm class
	''' </summary>
	Private Class WindowForm
		Inherits Form
		''' <summary>
		''' Fires when form is closing
		''' </summary>
		Public Event WindowFormClosing As EventHandler(Of WorkspaceCancelEventArgs)

		''' <summary>
		''' Fires when form is closed
		''' </summary>
		Public Event WindowFormClosed As EventHandler(Of WorkspaceEventArgs)

		''' <summary>
		''' Fires when form is activated
		''' </summary>
		Public Event WindowFormActivated As EventHandler(Of WorkspaceEventArgs)

		''' <summary>
		''' Handles Activated Event.
		''' </summary>
		''' <param name="e"></param>
		Protected Overrides Sub OnActivated(ByVal e As EventArgs)
			If Me.Controls.Count > 0 Then
				RaiseEvent WindowFormActivated(Me, New WorkspaceEventArgs(Me.Controls(0)))
			End If

			MyBase.OnActivated(e)
		End Sub


		''' <summary>
		''' Handles the Closing Event
		''' </summary>
		''' <param name="e"></param>
		Protected Overrides Sub OnClosing(ByVal e As CancelEventArgs)
			If Me.Controls.Count > 0 Then
				Dim cancelArgs As WorkspaceCancelEventArgs = FireWindowFormClosing(Me.Controls(0))
				e.Cancel = cancelArgs.Cancel

				If cancelArgs.Cancel = False Then
					Me.Controls(0).Hide()
				End If
			End If

			MyBase.OnClosing(e)
		End Sub

		''' <summary>
		''' Handles the Closed Event
		''' </summary>
		''' <param name="e"></param>
		Protected Overrides Sub OnClosed(ByVal e As EventArgs)
			If (Not Me.WindowFormClosedEvent Is Nothing) AndAlso (Me.Controls.Count > 0) Then
				RaiseEvent WindowFormClosed(Me, New WorkspaceEventArgs(Me.Controls(0)))
			End If

			MyBase.OnClosed(e)
		End Sub

		Private Function FireWindowFormClosing(ByVal smartPart As Object) As WorkspaceCancelEventArgs
			Dim cancelArgs As WorkspaceCancelEventArgs = New WorkspaceCancelEventArgs(smartPart)

			If Not Me.WindowFormClosingEvent Is Nothing Then
				RaiseEvent WindowFormClosing(Me, cancelArgs)
			End If

			Return cancelArgs
		End Function
	End Class

#End Region

#Region "Behavior overrides"

	''' <summary>
	''' Shows the form for the smart part and brings it to the front.
	''' </summary>
	Protected Overrides Sub OnActivate(ByVal smartPart As Control)
		' Prevent double firing from composer Workspace class and from form.
		Try
			fireActivatedFromForm = False
			Dim form As Form = windowDictionary(smartPart)
			form.BringToFront()
			form.Show()
		Finally
			fireActivatedFromForm = True
		End Try
	End Sub

	''' <summary>
	''' Sets the properties on the window based on the information.
	''' </summary>
	Protected Overrides Sub OnApplySmartPartInfo(ByVal smartPart As Control, ByVal smartPartInfo As WindowSmartPartInfo)
		Dim form As Form = windowDictionary(smartPart)
		SetWindowProperties(form, smartPartInfo)
		SetWindowLocation(form, smartPartInfo)
	End Sub

	''' <summary>
	''' Shows a form for the smart part and sets its properties.
	''' </summary>
	Protected Overrides Sub OnShow(ByVal smartPart As Control, ByVal smartPartInfo As WindowSmartPartInfo)
		Dim form As Form = GetOrCreateForm(smartPart)
		smartPart.Show()
		ShowForm(form, smartPartInfo)
	End Sub

	''' <summary>
	''' Hides the form where the smart part is being shown.
	''' </summary>
	Protected Overrides Sub OnHide(ByVal smartPart As Control)
		Dim form As Form = windowDictionary(smartPart)
		form.Hide()
	End Sub

	''' <summary>
	''' Closes the form where the smart part is being shown.
	''' </summary>
	Protected Overrides Sub OnClose(ByVal smartPart As Control)
		Dim form As Form = windowDictionary(smartPart)
		RemoveHandler smartPart.Disposed, AddressOf ControlDisposed

		' Remove the smartPart from the form to avoid disposing it.
		form.Controls.Remove(smartPart)

		form.Close()
		windowDictionary.Remove(smartPart)
	End Sub

#End Region
End Class
