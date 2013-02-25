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
Namespace CommandsQuickStart
	Partial Public Class ShellForm
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
		Public Sub InitializeComponent()
			Me.innerMainMenuStrip = New System.Windows.Forms.MenuStrip()
			Me.File = New System.Windows.Forms.ToolStripMenuItem()
			Me.ExitMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.innerMainMenuStrip.SuspendLayout()
			Me.SuspendLayout()
			' 
			' innerMainMenuStrip
			' 
			Me.innerMainMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.File})
			Me.innerMainMenuStrip.Location = New System.Drawing.Point(0, 0)
			Me.innerMainMenuStrip.Name = "innerMainMenuStrip"
			Me.innerMainMenuStrip.Size = New System.Drawing.Size(292, 24)
			Me.innerMainMenuStrip.TabIndex = 0
			Me.innerMainMenuStrip.Text = "menuStrip1"
			' 
			' File
			' 
			Me.File.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExitMenuItem})
			Me.File.Name = "File"
			Me.File.Size = New System.Drawing.Size(35, 20)
			Me.File.Text = "&File"
			' 
			' Exit
			' 
			Me.ExitMenuItem.Name = "Exit"
			Me.ExitMenuItem.Size = New System.Drawing.Size(152, 22)
			Me.ExitMenuItem.Text = "E&xit"
			' 
			' ShellForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(292, 273)
			Me.Controls.Add(Me.innerMainMenuStrip)
			Me.MainMenuStrip = Me.innerMainMenuStrip
			Me.Name = "ShellForm"
			Me.Text = "CommandsQuickStart"
			Me.innerMainMenuStrip.ResumeLayout(False)
			Me.innerMainMenuStrip.PerformLayout()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

#End Region

		Private innerMainMenuStrip As System.Windows.Forms.MenuStrip
		Private File As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents ExitMenuItem As System.Windows.Forms.ToolStripMenuItem
	End Class
End Namespace

