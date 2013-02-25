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
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports Microsoft.Practices.CompositeUI.SmartParts
Imports Microsoft.Practices.CompositeUI.Utility


''' <summary>
''' A <see cref="Control"/> that acts as a placeholder for a smartpart.
''' </summary>
<DesignerCategory("Code"), ToolboxBitmap(GetType(SmartPartPlaceholder), "SmartPartPlaceholder")> _
Public Class SmartPartPlaceholder : Inherits Control : Implements ISmartPartPlaceholder
	Private innerSmartPartName As String
	Private innerSmartPart As Object

	''' <summary>
	''' Fires when a smartpart is shown in the placeholder.
	''' </summary>
	Public Event SmartPartShown As EventHandler(Of SmartPartPlaceHolderEventArgs)

#Region "Constructors"

	''' <summary>
	''' Initializes a new instance of the <see cref="SmartPartPlaceholder"/> class.
	''' </summary>
	Public Sub New()
		SetStyle(ControlStyles.UserPaint Or ControlStyles.SupportsTransparentBackColor, True)
		Me.BackColor = Color.Transparent
	End Sub

#End Region

#Region "Properties"

	''' <summary>
	''' Gets or sets the name of SmartPart that will be placed in the placeholder.
	''' </summary>
	Public Property SmartPartName() As String Implements ISmartPartPlaceholder.SmartPartName
		Get
			Return innerSmartPartName
		End Get
		Set(ByVal value As String)
			innerSmartPartName = value
			Me.Refresh()
		End Set
	End Property

	''' <summary>
	''' Gets or sets a reference to the smartpart after it has been added.
	''' </summary>
	<Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
	Public Property SmartPart() As Object Implements ISmartPartPlaceholder.SmartPart
		Get
			Return innerSmartPart
		End Get
		Set(ByVal value As Object)
			Guard.ArgumentNotNull(value, "value")
			If TypeOf value Is Control = False Then
				Throw New ArgumentException(My.Resources.SmartPartNotControl)
			End If

			innerSmartPart = value
			Dim spcontrol As Control = CType(innerSmartPart, Control)
			spcontrol.Dock = DockStyle.Fill
			spcontrol.Show()
			Me.Controls.Clear()
			Me.Controls.Add(spcontrol)
			OnSmartPartShown(spcontrol)
		End Set
	End Property

#End Region

	Private Sub OnSmartPartShown(ByVal smartPartShown As Object)
		If Not Me.SmartPartShownEvent Is Nothing Then
			RaiseEvent SmartPartShown(Me, New SmartPartPlaceHolderEventArgs(smartPartShown))
		End If
	End Sub

#Region "Protected"

	''' <summary>
	''' Paints a border at design time.
	''' </summary>
	''' <param name="e">A <see cref="PaintEventArgs"/> that contains the event data</param>
	Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
		If (Not Me.DesignMode) Then
			Return
		End If

		Using p As Pen = New Pen(Me.ForeColor)
			Using b As Brush = New SolidBrush(Me.ForeColor)
				Dim r As Rectangle = Me.ClientRectangle
				r.Height -= 1
				r.Width -= 1

				p.DashStyle = DashStyle.Dash
				e.Graphics.DrawRectangle(p, r)
				e.Graphics.DrawString(Me.SmartPartName, Me.Font, b, r.X + 2, r.Y + 2)
			End Using
		End Using
	End Sub

#End Region
End Class
