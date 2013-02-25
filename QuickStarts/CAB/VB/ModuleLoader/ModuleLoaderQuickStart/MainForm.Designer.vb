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
Namespace ModuleLoaderQuickStart
	Partial Public Class MainForm
		Inherits System.Windows.Forms.Form

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

#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.MainWorkspace = New Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace()
			Me.SuspendLayout()
			' 
			' MainWorkspace
			' 
			Me.MainWorkspace.Dock = System.Windows.Forms.DockStyle.Fill
			Me.MainWorkspace.Location = New System.Drawing.Point(0, 0)
			Me.MainWorkspace.Name = "MainWorkspace"
			Me.MainWorkspace.Size = New System.Drawing.Size(267, 84)
			Me.MainWorkspace.TabIndex = 0
			Me.MainWorkspace.Text = "MainWorkSpace"
			' 
			' MainForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(267, 84)
			Me.Controls.Add(Me.MainWorkspace)
			Me.Name = "MainForm"
			Me.Text = "ModuleLoader MainForm"
			Me.ResumeLayout(False)

		End Sub

#End Region

		Private MainWorkspace As Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace



	End Class
End Namespace

