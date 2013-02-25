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
Imports System.Windows.Forms

Namespace EventBrokerDemo
	Partial Public Class TraceTextBox
		Inherits UserControl

		''' <summary> 
		''' Required designer variable.
		''' </summary>
		Private components As IContainer = Nothing

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
			Me.logBox = New TextBox()
			Me.panel1 = New Panel()
			Me.enableLogging = New CheckBox()
			Me.panel1.SuspendLayout()
			Me.SuspendLayout()
			' 
			' logBox
			' 
			Me.logBox.Dock = DockStyle.Fill
			Me.logBox.Location = New Point(0, 25)
			Me.logBox.Multiline = True
			Me.logBox.Name = "logBox"
			Me.logBox.ScrollBars = ScrollBars.Both
			Me.logBox.Size = New Size(513, 96)
			Me.logBox.TabIndex = 0
			Me.logBox.WordWrap = False
			' 
			' panel1
			' 
			Me.panel1.Controls.Add(Me.enableLogging)
			Me.panel1.Dock = DockStyle.Top
			Me.panel1.Location = New Point(0, 0)
			Me.panel1.Name = "panel1"
			Me.panel1.Size = New Size(513, 25)
			Me.panel1.TabIndex = 1
			' 
			' enableLogging
			' 
			Me.enableLogging.AutoSize = True
			Me.enableLogging.Checked = True
			Me.enableLogging.CheckState = CheckState.Checked
			Me.enableLogging.Location = New Point(4, 4)
			Me.enableLogging.Name = "enableLogging"
			Me.enableLogging.Size = New Size(90, 17)
			Me.enableLogging.TabIndex = 0
			Me.enableLogging.Text = "&Enable tracing"
			'			Me.enableLogging.CheckedChanged += New EventHandler(Me.enableLogging_CheckedChanged);
			' 
			' TraceTextBox
			' 
			Me.AutoScaleDimensions = New SizeF(6.0F, 13.0F)
			Me.Controls.Add(Me.logBox)
			Me.Controls.Add(Me.panel1)
			Me.Name = "TraceTextBox"
			Me.Size = New Size(513, 121)
			Me.panel1.ResumeLayout(False)
			Me.panel1.PerformLayout()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

#End Region

		Private logBox As TextBox
		Private panel1 As Panel
		Private WithEvents enableLogging As CheckBox
	End Class
End Namespace