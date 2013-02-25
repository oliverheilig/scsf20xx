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
Namespace GPSModule
	Partial Public Class GPSView
		Inherits System.Windows.Forms.UserControl

		''' <summary> 
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary> 
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (Not components Is Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

#Region "Component Designer generated code"

		''' <summary> 
		''' Required method for Designer support - do not modify 
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.txtDistance = New System.Windows.Forms.TextBox()
			Me.cmdGetDistance = New System.Windows.Forms.Button()
			Me.txtLatitude = New System.Windows.Forms.TextBox()
			Me.cmdGetLatitude = New System.Windows.Forms.Button()
			Me.SuspendLayout()
			' 
			' txtDistance
			' 
			Me.txtDistance.Location = New System.Drawing.Point(94, 50)
			Me.txtDistance.Name = "txtDistance"
			Me.txtDistance.Size = New System.Drawing.Size(163, 20)
			Me.txtDistance.TabIndex = 11
			' 
			' cmdGetDistance
			' 
			Me.cmdGetDistance.Location = New System.Drawing.Point(13, 48)
			Me.cmdGetDistance.Name = "cmdGetDistance"
			Me.cmdGetDistance.Size = New System.Drawing.Size(75, 23)
			Me.cmdGetDistance.TabIndex = 10
			Me.cmdGetDistance.Text = "GetDistance"
			' 
			' txtLatitude
			' 
			Me.txtLatitude.Location = New System.Drawing.Point(94, 21)
			Me.txtLatitude.Name = "txtLatitude"
			Me.txtLatitude.Size = New System.Drawing.Size(163, 20)
			Me.txtLatitude.TabIndex = 9
			' 
			' cmdGetLatitude
			' 
			Me.cmdGetLatitude.Location = New System.Drawing.Point(13, 19)
			Me.cmdGetLatitude.Name = "cmdGetLatitude"
			Me.cmdGetLatitude.Size = New System.Drawing.Size(75, 23)
			Me.cmdGetLatitude.TabIndex = 8
			Me.cmdGetLatitude.Text = "GetLatitude"
			' 
			' GPSView
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.Controls.Add(Me.txtDistance)
			Me.Controls.Add(Me.cmdGetDistance)
			Me.Controls.Add(Me.txtLatitude)
			Me.Controls.Add(Me.cmdGetLatitude)
			Me.Name = "GPSView"
			Me.Size = New System.Drawing.Size(272, 86)
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

#End Region

		Private txtDistance As System.Windows.Forms.TextBox
		Private WithEvents cmdGetDistance As System.Windows.Forms.Button
		Private txtLatitude As System.Windows.Forms.TextBox
		Private WithEvents cmdGetLatitude As System.Windows.Forms.Button
	End Class
End Namespace
