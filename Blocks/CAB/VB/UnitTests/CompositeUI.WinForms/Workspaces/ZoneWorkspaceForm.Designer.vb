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

Partial Friend Class ZoneWorkspaceForm
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
		Me.pnlButtons = New System.Windows.Forms.Panel()
		Me.button1 = New System.Windows.Forms.Button()
		Me.Workspace = New Microsoft.Practices.CompositeUI.WinForms.ZoneWorkspace()
		Me.splitContainer1 = New System.Windows.Forms.SplitContainer()
		Me.flowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
		Me.pnlButtons.SuspendLayout()
		Me.Workspace.SuspendLayout()
		Me.splitContainer1.Panel2.SuspendLayout()
		Me.splitContainer1.SuspendLayout()
		Me.SuspendLayout()
		' 
		' pnlButtons
		' 
		Me.pnlButtons.Controls.Add(Me.button1)
		Me.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.pnlButtons.Location = New System.Drawing.Point(0, 411)
		Me.pnlButtons.Name = "pnlButtons"
		Me.pnlButtons.Size = New System.Drawing.Size(488, 41)
		Me.pnlButtons.TabIndex = 1
		' 
		' button1
		' 
		Me.button1.Location = New System.Drawing.Point(12, 6)
		Me.button1.Name = "button1"
		Me.button1.Size = New System.Drawing.Size(75, 23)
		Me.button1.TabIndex = 0
		Me.button1.Text = "button1"
		'			Me.button1.Click += New System.EventHandler(Me.button1_Click);
		' 
		' Workspace
		' 
		Me.Workspace.Controls.Add(Me.splitContainer1)
		Me.Workspace.Location = New System.Drawing.Point(13, 13)
		Me.Workspace.Name = "Workspace"
		Me.Workspace.Size = New System.Drawing.Size(463, 382)
		Me.Workspace.TabIndex = 2
		' 
		' splitContainer1
		' 
		Me.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.splitContainer1.Location = New System.Drawing.Point(0, 0)
		Me.splitContainer1.Name = "splitContainer1"
		' 
		' splitContainer1.Panel1
		' 
		Me.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption
		Me.Workspace.SetZoneName(Me.splitContainer1.Panel1, "LeftZone")
		' 
		' splitContainer1.Panel2
		' 
		Me.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Desktop
		Me.splitContainer1.Panel2.Controls.Add(Me.flowLayoutPanel1)
		Me.splitContainer1.Size = New System.Drawing.Size(463, 382)
		Me.splitContainer1.SplitterDistance = 155
		Me.splitContainer1.TabIndex = 0
		Me.splitContainer1.Text = "splitContainer1"
		' 
		' flowLayoutPanel1
		' 
		Me.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Workspace.SetIsDefaultZone(Me.flowLayoutPanel1, True)
		Me.flowLayoutPanel1.Location = New System.Drawing.Point(0, 0)
		Me.flowLayoutPanel1.Name = "flowLayoutPanel1"
		Me.flowLayoutPanel1.Size = New System.Drawing.Size(304, 382)
		Me.flowLayoutPanel1.TabIndex = 0
		Me.Workspace.SetZoneName(Me.flowLayoutPanel1, "ContentZone")
		' 
		' TiledWorkspaceForm
		' 
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(488, 452)
		Me.Controls.Add(Me.Workspace)
		Me.Controls.Add(Me.pnlButtons)
		Me.Name = "TiledWorkspaceForm"
		Me.Text = "TiledWorkspaceForm"
		Me.pnlButtons.ResumeLayout(False)
		Me.Workspace.ResumeLayout(False)
		Me.splitContainer1.Panel2.ResumeLayout(False)
		Me.splitContainer1.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private pnlButtons As System.Windows.Forms.Panel
	Private WithEvents button1 As System.Windows.Forms.Button
	Friend Workspace As ZoneWorkspace
	Private splitContainer1 As System.Windows.Forms.SplitContainer
	Private flowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
End Class
