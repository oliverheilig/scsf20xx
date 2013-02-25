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
Imports System.Drawing
Imports System.Windows.Forms

Namespace EventBrokerDemo
	Partial Public Class ProgressView
		Inherits Form

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

#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.progressWork = New ProgressBar()
			Me.SuspendLayout()
			' 
			' progressWork
			' 
			Me.progressWork.Anchor = (CType(((AnchorStyles.Top Or AnchorStyles.Left) Or AnchorStyles.Right), AnchorStyles))
			Me.progressWork.Location = New Point(12, 12)
			Me.progressWork.Name = "progressWork"
			Me.progressWork.Size = New Size(444, 23)
			Me.progressWork.TabIndex = 0
			' 
			' ProgressView
			' 
			Me.AutoScaleDimensions = New SizeF(6.0F, 13.0F)
			Me.ClientSize = New Size(468, 47)
			Me.Controls.Add(Me.progressWork)
			Me.Name = "ProgressView"
			Me.Text = "ProgressView"
			Me.ResumeLayout(False)

		End Sub

#End Region

		Private progressWork As ProgressBar
	End Class
End Namespace