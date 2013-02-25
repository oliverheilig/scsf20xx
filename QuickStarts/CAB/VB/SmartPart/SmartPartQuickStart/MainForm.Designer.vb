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
			Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
			Me.menuStrip1 = New System.Windows.Forms.MenuStrip()
			Me.fileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.newToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.openToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
			Me.saveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.saveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
			Me.printToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.printPreviewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
			Me.exitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.editToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.undoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.redoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
			Me.cutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.copyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.pasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
			Me.selectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.customizeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.optionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.helpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.contentsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.indexToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.searchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
			Me.aboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.statusStrip1 = New System.Windows.Forms.StatusStrip()
			Me.MainFormWorkspace = New Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace()
			Me.menuStrip1.SuspendLayout()
			Me.SuspendLayout()
			' 
			' menuStrip1
			' 
			Me.menuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.fileToolStripMenuItem, Me.editToolStripMenuItem, Me.toolsToolStripMenuItem, Me.helpToolStripMenuItem})
			Me.menuStrip1.Location = New System.Drawing.Point(0, 0)
			Me.menuStrip1.Name = "menuStrip1"
			Me.menuStrip1.Size = New System.Drawing.Size(592, 24)
			Me.menuStrip1.TabIndex = 1
			Me.menuStrip1.Text = "menuStrip1"
			' 
			' fileToolStripMenuItem
			' 
			Me.fileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.newToolStripMenuItem, Me.openToolStripMenuItem, Me.toolStripSeparator, Me.saveToolStripMenuItem, Me.saveAsToolStripMenuItem, Me.toolStripSeparator1, Me.printToolStripMenuItem, Me.printPreviewToolStripMenuItem, Me.toolStripSeparator2, Me.exitToolStripMenuItem})
			Me.fileToolStripMenuItem.Name = "fileToolStripMenuItem"
			Me.fileToolStripMenuItem.Size = New System.Drawing.Size(35, 20)
			Me.fileToolStripMenuItem.Text = "&File"
			' 
			' newToolStripMenuItem
			' 
			Me.newToolStripMenuItem.Image = (CType(resources.GetObject("newToolStripMenuItem.Image"), System.Drawing.Image))
			Me.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.newToolStripMenuItem.Name = "newToolStripMenuItem"
			Me.newToolStripMenuItem.ShortcutKeys = (CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys))
			Me.newToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
			Me.newToolStripMenuItem.Text = "&New"
			' 
			' openToolStripMenuItem
			' 
			Me.openToolStripMenuItem.Image = (CType(resources.GetObject("openToolStripMenuItem.Image"), System.Drawing.Image))
			Me.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.openToolStripMenuItem.Name = "openToolStripMenuItem"
			Me.openToolStripMenuItem.ShortcutKeys = (CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys))
			Me.openToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
			Me.openToolStripMenuItem.Text = "&Open"
			' 
			' toolStripSeparator
			' 
			Me.toolStripSeparator.Name = "toolStripSeparator"
			Me.toolStripSeparator.Size = New System.Drawing.Size(148, 6)
			' 
			' saveToolStripMenuItem
			' 
			Me.saveToolStripMenuItem.Image = (CType(resources.GetObject("saveToolStripMenuItem.Image"), System.Drawing.Image))
			Me.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.saveToolStripMenuItem.Name = "saveToolStripMenuItem"
			Me.saveToolStripMenuItem.ShortcutKeys = (CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys))
			Me.saveToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
			Me.saveToolStripMenuItem.Text = "&Save"
			' 
			' saveAsToolStripMenuItem
			' 
			Me.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem"
			Me.saveAsToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
			Me.saveAsToolStripMenuItem.Text = "Save &As"
			' 
			' toolStripSeparator1
			' 
			Me.toolStripSeparator1.Name = "toolStripSeparator1"
			Me.toolStripSeparator1.Size = New System.Drawing.Size(148, 6)
			' 
			' printToolStripMenuItem
			' 
			Me.printToolStripMenuItem.Image = (CType(resources.GetObject("printToolStripMenuItem.Image"), System.Drawing.Image))
			Me.printToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.printToolStripMenuItem.Name = "printToolStripMenuItem"
			Me.printToolStripMenuItem.ShortcutKeys = (CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys))
			Me.printToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
			Me.printToolStripMenuItem.Text = "&Print"
			' 
			' printPreviewToolStripMenuItem
			' 
			Me.printPreviewToolStripMenuItem.Image = (CType(resources.GetObject("printPreviewToolStripMenuItem.Image"), System.Drawing.Image))
			Me.printPreviewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem"
			Me.printPreviewToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
			Me.printPreviewToolStripMenuItem.Text = "Print Pre&view"
			' 
			' toolStripSeparator2
			' 
			Me.toolStripSeparator2.Name = "toolStripSeparator2"
			Me.toolStripSeparator2.Size = New System.Drawing.Size(148, 6)
			' 
			' exitToolStripMenuItem
			' 
			Me.exitToolStripMenuItem.Name = "exitToolStripMenuItem"
			Me.exitToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
			Me.exitToolStripMenuItem.Text = "E&xit"
			' 
			' editToolStripMenuItem
			' 
			Me.editToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.undoToolStripMenuItem, Me.redoToolStripMenuItem, Me.toolStripSeparator3, Me.cutToolStripMenuItem, Me.copyToolStripMenuItem, Me.pasteToolStripMenuItem, Me.toolStripSeparator4, Me.selectAllToolStripMenuItem})
			Me.editToolStripMenuItem.Name = "editToolStripMenuItem"
			Me.editToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
			Me.editToolStripMenuItem.Text = "&Edit"
			' 
			' undoToolStripMenuItem
			' 
			Me.undoToolStripMenuItem.Name = "undoToolStripMenuItem"
			Me.undoToolStripMenuItem.ShortcutKeys = (CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Z), System.Windows.Forms.Keys))
			Me.undoToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
			Me.undoToolStripMenuItem.Text = "&Undo"
			' 
			' redoToolStripMenuItem
			' 
			Me.redoToolStripMenuItem.Name = "redoToolStripMenuItem"
			Me.redoToolStripMenuItem.ShortcutKeys = (CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Y), System.Windows.Forms.Keys))
			Me.redoToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
			Me.redoToolStripMenuItem.Text = "&Redo"
			' 
			' toolStripSeparator3
			' 
			Me.toolStripSeparator3.Name = "toolStripSeparator3"
			Me.toolStripSeparator3.Size = New System.Drawing.Size(147, 6)
			' 
			' cutToolStripMenuItem
			' 
			Me.cutToolStripMenuItem.Image = (CType(resources.GetObject("cutToolStripMenuItem.Image"), System.Drawing.Image))
			Me.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.cutToolStripMenuItem.Name = "cutToolStripMenuItem"
			Me.cutToolStripMenuItem.ShortcutKeys = (CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys))
			Me.cutToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
			Me.cutToolStripMenuItem.Text = "Cu&t"
			' 
			' copyToolStripMenuItem
			' 
			Me.copyToolStripMenuItem.Image = (CType(resources.GetObject("copyToolStripMenuItem.Image"), System.Drawing.Image))
			Me.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.copyToolStripMenuItem.Name = "copyToolStripMenuItem"
			Me.copyToolStripMenuItem.ShortcutKeys = (CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys))
			Me.copyToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
			Me.copyToolStripMenuItem.Text = "&Copy"
			' 
			' pasteToolStripMenuItem
			' 
			Me.pasteToolStripMenuItem.Image = (CType(resources.GetObject("pasteToolStripMenuItem.Image"), System.Drawing.Image))
			Me.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem"
			Me.pasteToolStripMenuItem.ShortcutKeys = (CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys))
			Me.pasteToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
			Me.pasteToolStripMenuItem.Text = "&Paste"
			' 
			' toolStripSeparator4
			' 
			Me.toolStripSeparator4.Name = "toolStripSeparator4"
			Me.toolStripSeparator4.Size = New System.Drawing.Size(147, 6)
			' 
			' selectAllToolStripMenuItem
			' 
			Me.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem"
			Me.selectAllToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
			Me.selectAllToolStripMenuItem.Text = "Select &All"
			' 
			' toolsToolStripMenuItem
			' 
			Me.toolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.customizeToolStripMenuItem, Me.optionsToolStripMenuItem})
			Me.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem"
			Me.toolsToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
			Me.toolsToolStripMenuItem.Text = "&Tools"
			' 
			' customizeToolStripMenuItem
			' 
			Me.customizeToolStripMenuItem.Name = "customizeToolStripMenuItem"
			Me.customizeToolStripMenuItem.Size = New System.Drawing.Size(134, 22)
			Me.customizeToolStripMenuItem.Text = "&Customize"
			' 
			' optionsToolStripMenuItem
			' 
			Me.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem"
			Me.optionsToolStripMenuItem.Size = New System.Drawing.Size(134, 22)
			Me.optionsToolStripMenuItem.Text = "&Options"
			' 
			' helpToolStripMenuItem
			' 
			Me.helpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.contentsToolStripMenuItem, Me.indexToolStripMenuItem, Me.searchToolStripMenuItem, Me.toolStripSeparator5, Me.aboutToolStripMenuItem})
			Me.helpToolStripMenuItem.Name = "helpToolStripMenuItem"
			Me.helpToolStripMenuItem.Size = New System.Drawing.Size(40, 20)
			Me.helpToolStripMenuItem.Text = "&Help"
			' 
			' contentsToolStripMenuItem
			' 
			Me.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem"
			Me.contentsToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
			Me.contentsToolStripMenuItem.Text = "&Contents"
			' 
			' indexToolStripMenuItem
			' 
			Me.indexToolStripMenuItem.Name = "indexToolStripMenuItem"
			Me.indexToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
			Me.indexToolStripMenuItem.Text = "&Index"
			' 
			' searchToolStripMenuItem
			' 
			Me.searchToolStripMenuItem.Name = "searchToolStripMenuItem"
			Me.searchToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
			Me.searchToolStripMenuItem.Text = "&Search"
			' 
			' toolStripSeparator5
			' 
			Me.toolStripSeparator5.Name = "toolStripSeparator5"
			Me.toolStripSeparator5.Size = New System.Drawing.Size(126, 6)
			' 
			' aboutToolStripMenuItem
			' 
			Me.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem"
			Me.aboutToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
			Me.aboutToolStripMenuItem.Text = "&About..."
			' 
			' statusStrip1
			' 
			Me.statusStrip1.Location = New System.Drawing.Point(0, 397)
			Me.statusStrip1.Name = "statusStrip1"
			Me.statusStrip1.Size = New System.Drawing.Size(592, 22)
			Me.statusStrip1.TabIndex = 3
			Me.statusStrip1.Text = "statusStrip1"
			' 
			' MainFormWorkspace
			' 
			Me.MainFormWorkspace.Dock = System.Windows.Forms.DockStyle.Fill
			Me.MainFormWorkspace.Location = New System.Drawing.Point(0, 24)
			Me.MainFormWorkspace.Name = "MainFormWorkspace"
			Me.MainFormWorkspace.Size = New System.Drawing.Size(592, 373)
			Me.MainFormWorkspace.TabIndex = 4
			' 
			' MainForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(592, 419)
			Me.Controls.Add(Me.MainFormWorkspace)
			Me.Controls.Add(Me.statusStrip1)
			Me.Controls.Add(Me.menuStrip1)
			Me.MainMenuStrip = Me.menuStrip1
			Me.Name = "MainForm"
			Me.Text = "SmartPart Quick Start"
			Me.menuStrip1.ResumeLayout(False)
			Me.menuStrip1.PerformLayout()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

#End Region

		Private menuStrip1 As System.Windows.Forms.MenuStrip
		Private fileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private newToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private openToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private toolStripSeparator As System.Windows.Forms.ToolStripSeparator
		Private saveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private saveAsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
		Private printToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private printPreviewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private toolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
		Private exitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private editToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private undoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private redoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private toolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
		Private cutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private copyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private pasteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private toolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
		Private selectAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private toolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private customizeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private optionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private helpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private contentsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private indexToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private searchToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private toolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
		Private aboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private statusStrip1 As System.Windows.Forms.StatusStrip
		Private MainFormWorkspace As Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace
	End Class
End Namespace

