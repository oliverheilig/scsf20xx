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
Namespace BankShell
	Partial Public Class BankShellForm
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
			Me.innerMainMenuStrip = New System.Windows.Forms.MenuStrip()
			Me.File = New System.Windows.Forms.ToolStripMenuItem()
			Me.mainStatusStrip = New System.Windows.Forms.StatusStrip()
			Me.toolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
			Me.sideBarWorkspace = New Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace()
			Me.splitContainer1 = New System.Windows.Forms.SplitContainer()
			Me.contentWorkspace = New Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace()
			Me.innerMainMenuStrip.SuspendLayout()
			Me.mainStatusStrip.SuspendLayout()
			Me.splitContainer1.Panel1.SuspendLayout()
			Me.splitContainer1.Panel2.SuspendLayout()
			Me.splitContainer1.SuspendLayout()
			Me.SuspendLayout()
			' 
			' mainMenuStrip
			' 
			Me.innerMainMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.File})
			Me.innerMainMenuStrip.Location = New System.Drawing.Point(0, 0)
			Me.innerMainMenuStrip.Name = "mainMenuStrip"
			Me.innerMainMenuStrip.Size = New System.Drawing.Size(739, 24)
			Me.innerMainMenuStrip.TabIndex = 0
			Me.innerMainMenuStrip.Text = "mainmenu"
			' 
			' File
			' 
			Me.File.Name = "File"
			Me.File.Size = New System.Drawing.Size(35, 20)
			Me.File.Text = "&File"
			' 
			' mainStatusStrip
			' 
			Me.mainStatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripStatusLabel1})
			Me.mainStatusStrip.Location = New System.Drawing.Point(0, 513)
			Me.mainStatusStrip.Name = "mainStatusStrip"
			Me.mainStatusStrip.Size = New System.Drawing.Size(739, 22)
			Me.mainStatusStrip.TabIndex = 1
			Me.mainStatusStrip.Text = "statusStrip1"
			' 
			' toolStripStatusLabel1
			' 
			Me.toolStripStatusLabel1.Name = "toolStripStatusLabel1"
			Me.toolStripStatusLabel1.Size = New System.Drawing.Size(0, 17)
			' 
			' sideBarWorkspace
			' 
			Me.sideBarWorkspace.Dock = System.Windows.Forms.DockStyle.Fill
			Me.sideBarWorkspace.Location = New System.Drawing.Point(0, 0)
			Me.sideBarWorkspace.Name = "sideBarWorkspace"
			Me.sideBarWorkspace.Size = New System.Drawing.Size(222, 489)
			Me.sideBarWorkspace.TabIndex = 0
			Me.sideBarWorkspace.Text = "deckWorkspace1"
			' 
			' splitContainer1
			' 
			Me.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.splitContainer1.Location = New System.Drawing.Point(0, 24)
			Me.splitContainer1.Name = "splitContainer1"
			' 
			' splitContainer1.Panel1
			' 
			Me.splitContainer1.Panel1.Controls.Add(Me.sideBarWorkspace)
			' 
			' splitContainer1.Panel2
			' 
			Me.splitContainer1.Panel2.Controls.Add(Me.contentWorkspace)
			Me.splitContainer1.Size = New System.Drawing.Size(739, 489)
			Me.splitContainer1.SplitterDistance = 222
			Me.splitContainer1.TabIndex = 2
			Me.splitContainer1.Text = "splitContainer1"
			' 
			' contentWorkspace
			' 
			Me.contentWorkspace.Dock = System.Windows.Forms.DockStyle.Fill
			Me.contentWorkspace.Location = New System.Drawing.Point(0, 0)
			Me.contentWorkspace.Name = "contentWorkspace"
			Me.contentWorkspace.Size = New System.Drawing.Size(513, 489)
			Me.contentWorkspace.TabIndex = 0
			Me.contentWorkspace.Text = "deckedWorkspace1"
			' 
			' BankShellForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(739, 535)
			Me.Controls.Add(Me.splitContainer1)
			Me.Controls.Add(Me.mainStatusStrip)
			Me.Controls.Add(Me.innerMainMenuStrip)
			Me.MainMenuStrip = Me.innerMainMenuStrip
			Me.Name = "BankShellForm"
			Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
			Me.Text = "Bank Shell"
			Me.innerMainMenuStrip.ResumeLayout(False)
			Me.innerMainMenuStrip.PerformLayout()
			Me.mainStatusStrip.ResumeLayout(False)
			Me.mainStatusStrip.PerformLayout()
			Me.splitContainer1.Panel1.ResumeLayout(False)
			Me.splitContainer1.Panel2.ResumeLayout(False)
			Me.splitContainer1.ResumeLayout(False)
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

#End Region

		Private innerMainMenuStrip As System.Windows.Forms.MenuStrip
		Private toolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
		Private sideBarWorkspace As Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace
		Private splitContainer1 As System.Windows.Forms.SplitContainer
		Public contentWorkspace As Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace
		Private File As System.Windows.Forms.ToolStripMenuItem
		Friend mainStatusStrip As System.Windows.Forms.StatusStrip
	End Class
End Namespace

