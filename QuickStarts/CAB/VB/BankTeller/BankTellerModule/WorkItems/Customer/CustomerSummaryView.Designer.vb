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
Namespace BankTellerModule
	Partial Public Class CustomerSummaryView
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
			Me.tabbedWorkspace1 = New Microsoft.Practices.CompositeUI.WinForms.TabWorkspace()
			Me.tabSummary = New System.Windows.Forms.TabPage()
			Me.tabAccounts = New System.Windows.Forms.TabPage()
			Me.SaveButton = New System.Windows.Forms.Button()
			Me.customerContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
			Me.customerDetailView1 = New BankTellerModule.CustomerDetailView()
			Me.customerAccountsView1 = New BankTellerModule.CustomerAccountsView()
			Me.customerHeaderView1 = New BankTellerModule.CustomerHeaderView()
			Me.tabbedWorkspace1.SuspendLayout()
			Me.tabSummary.SuspendLayout()
			Me.tabAccounts.SuspendLayout()
			Me.SuspendLayout()
			' 
			' tabbedWorkspace1
			' 
			Me.tabbedWorkspace1.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.tabbedWorkspace1.Controls.Add(Me.tabSummary)
			Me.tabbedWorkspace1.Controls.Add(Me.tabAccounts)
			Me.tabbedWorkspace1.Location = New System.Drawing.Point(4, 96)
			Me.tabbedWorkspace1.Name = "tabbedWorkspace1"
			Me.tabbedWorkspace1.SelectedIndex = 0
			Me.tabbedWorkspace1.Size = New System.Drawing.Size(469, 282)
			Me.tabbedWorkspace1.TabIndex = 1
			' 
			' tabSummary
			' 
			Me.tabSummary.AutoScroll = True
			Me.tabSummary.BackColor = System.Drawing.Color.White
			Me.tabSummary.Controls.Add(Me.customerDetailView1)
			Me.tabSummary.Location = New System.Drawing.Point(4, 22)
			Me.tabSummary.Name = "tabSummary"
			Me.tabSummary.Padding = New System.Windows.Forms.Padding(5)
			Me.tabSummary.Size = New System.Drawing.Size(461, 256)
			Me.tabSummary.TabIndex = 0
			Me.tabSummary.Text = "Summary"
			' 
			' tabAccounts
			' 
			Me.tabAccounts.BackColor = System.Drawing.Color.White
			Me.tabAccounts.Controls.Add(Me.customerAccountsView1)
			Me.tabAccounts.Location = New System.Drawing.Point(4, 22)
			Me.tabAccounts.Name = "tabAccounts"
			Me.tabAccounts.Padding = New System.Windows.Forms.Padding(5)
			Me.tabAccounts.Size = New System.Drawing.Size(461, 256)
			Me.tabAccounts.TabIndex = 1
			Me.tabAccounts.Text = "Accounts"
			' 
			' SaveButton
			' 
			Me.SaveButton.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.SaveButton.Location = New System.Drawing.Point(396, 383)
			Me.SaveButton.Name = "SaveButton"
			Me.SaveButton.Size = New System.Drawing.Size(75, 23)
			Me.SaveButton.TabIndex = 2
			Me.SaveButton.Text = "Save"
			'			Me.SaveButton.Click += New System.EventHandler(Me.OnSave);
			' 
			' customerContextMenu
			' 
			Me.customerContextMenu.Name = "customerContextMenu"
			Me.customerContextMenu.Size = New System.Drawing.Size(61, 4)
			' 
			' customerDetailView1
			' 
			Me.customerDetailView1.AutoScroll = True
			Me.customerDetailView1.Location = New System.Drawing.Point(8, 8)
			Me.customerDetailView1.MinimumSize = New System.Drawing.Size(445, 271)
			Me.customerDetailView1.Name = "customerDetailView1"
			Me.customerDetailView1.Size = New System.Drawing.Size(445, 271)
			Me.customerDetailView1.TabIndex = 0
			' 
			' customerAccountsView1
			' 
			Me.customerAccountsView1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.customerAccountsView1.Location = New System.Drawing.Point(5, 5)
			Me.customerAccountsView1.Name = "customerAccountsView1"
			Me.customerAccountsView1.Size = New System.Drawing.Size(451, 246)
			Me.customerAccountsView1.TabIndex = 0
			' 
			' customerHeaderView1
			' 
			Me.customerHeaderView1.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.customerHeaderView1.Location = New System.Drawing.Point(4, 4)
			Me.customerHeaderView1.Name = "customerHeaderView1"
			Me.customerHeaderView1.Size = New System.Drawing.Size(465, 85)
			Me.customerHeaderView1.TabIndex = 0
			' 
			' CustomerSummaryView
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ContextMenuStrip = Me.customerContextMenu
			Me.Controls.Add(Me.SaveButton)
			Me.Controls.Add(Me.tabbedWorkspace1)
			Me.Controls.Add(Me.customerHeaderView1)
			Me.Name = "CustomerSummaryView"
			Me.Size = New System.Drawing.Size(476, 412)
			Me.tabbedWorkspace1.ResumeLayout(False)
			Me.tabSummary.ResumeLayout(False)
			Me.tabAccounts.ResumeLayout(False)
			Me.ResumeLayout(False)

		End Sub

#End Region

		Private tabbedWorkspace1 As Microsoft.Practices.CompositeUI.WinForms.TabWorkspace
		Private tabSummary As System.Windows.Forms.TabPage
		Private customerDetailView1 As CustomerDetailView
		Private tabAccounts As System.Windows.Forms.TabPage
		Private customerAccountsView1 As CustomerAccountsView
		Private customerHeaderView1 As CustomerHeaderView
		Private WithEvents SaveButton As System.Windows.Forms.Button
		Private customerContextMenu As System.Windows.Forms.ContextMenuStrip
	End Class
End Namespace
