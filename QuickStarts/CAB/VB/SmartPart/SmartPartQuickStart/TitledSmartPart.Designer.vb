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
Namespace SmartPartQuickStart
	Partial Public Class TitledSmartPart
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
			Me.components = New System.ComponentModel.Container()
			Me.titleLabel = New System.Windows.Forms.Label()
			Me.toolTip = New System.Windows.Forms.ToolTip(Me.components)
			Me.SuspendLayout()
			' 
			' titleLabel
			' 
			Me.titleLabel.BackColor = System.Drawing.SystemColors.ControlDark
			Me.titleLabel.Dock = System.Windows.Forms.DockStyle.Top
			Me.titleLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CByte(0)))
			Me.titleLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
			Me.titleLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
			Me.titleLabel.Location = New System.Drawing.Point(0, 0)
			Me.titleLabel.Name = "titleLabel"
			Me.titleLabel.Padding = New System.Windows.Forms.Padding(3)
			Me.titleLabel.Size = New System.Drawing.Size(588, 23)
			Me.titleLabel.TabIndex = 0
			' 
			' TitledSmartPart
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.Controls.Add(Me.titleLabel)
			Me.Name = "TitledSmartPart"
			Me.Size = New System.Drawing.Size(588, 339)
			Me.ResumeLayout(False)

		End Sub

#End Region

		Private titleLabel As System.Windows.Forms.Label
		Private toolTip As System.Windows.Forms.ToolTip
	End Class
End Namespace
