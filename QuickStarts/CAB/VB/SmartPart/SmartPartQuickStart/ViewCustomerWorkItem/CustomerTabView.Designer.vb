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
Namespace SmartPartQuickStart.ViewCustomerWorkItem
	Partial Public Class CustomerTabView
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
			Me.tabWorkspace1 = New Microsoft.Practices.CompositeUI.WinForms.TabWorkspace()
			Me.tabPage1 = New System.Windows.Forms.TabPage()
			Me.customerDetailView1 = New SmartPartQuickStart.ViewCustomerWorkItem.CustomerDetailView()
			Me.tabPage2 = New System.Windows.Forms.TabPage()
			Me.smartPartPlaceholder1 = New Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder()
			Me.tabWorkspace1.SuspendLayout()
			Me.tabPage1.SuspendLayout()
			Me.tabPage2.SuspendLayout()
			Me.SuspendLayout()
			' 
			' tabWorkspace1
			' 
			Me.tabWorkspace1.Controls.Add(Me.tabPage1)
			Me.tabWorkspace1.Controls.Add(Me.tabPage2)
			Me.tabWorkspace1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.tabWorkspace1.Location = New System.Drawing.Point(0, 0)
			Me.tabWorkspace1.Name = "tabWorkspace1"
			Me.tabWorkspace1.SelectedIndex = 0
			Me.tabWorkspace1.Size = New System.Drawing.Size(464, 316)
			Me.tabWorkspace1.TabIndex = 0
			' 
			' tabPage1
			' 
			Me.tabPage1.Controls.Add(Me.customerDetailView1)
			Me.tabPage1.Location = New System.Drawing.Point(4, 22)
			Me.tabPage1.Name = "tabPage1"
			Me.tabPage1.Padding = New System.Windows.Forms.Padding(3)
			Me.tabPage1.Size = New System.Drawing.Size(456, 290)
			Me.tabPage1.TabIndex = 1
			Me.tabPage1.Text = "Customer Details"
			' 
			' customerDetailView1
			' 
			Me.customerDetailView1.Description = ""
			Me.customerDetailView1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.customerDetailView1.Location = New System.Drawing.Point(3, 3)
			Me.customerDetailView1.Name = "customerDetailView1"
			Me.customerDetailView1.Size = New System.Drawing.Size(450, 284)
			Me.customerDetailView1.TabIndex = 0
			Me.customerDetailView1.Title = "Customer Details"
			' 
			' tabPage2
			' 
			Me.tabPage2.Controls.Add(Me.smartPartPlaceholder1)
			Me.tabPage2.Location = New System.Drawing.Point(4, 22)
			Me.tabPage2.Name = "tabPage2"
			Me.tabPage2.Padding = New System.Windows.Forms.Padding(3)
			Me.tabPage2.Size = New System.Drawing.Size(456, 290)
			Me.tabPage2.TabIndex = 2
			Me.tabPage2.Text = "Summary"
			' 
			' smartPartPlaceholder1
			' 
			Me.smartPartPlaceholder1.BackColor = System.Drawing.SystemColors.ControlDark
			Me.smartPartPlaceholder1.SmartPartName = "CustomerSummary"
			Me.smartPartPlaceholder1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.smartPartPlaceholder1.Location = New System.Drawing.Point(3, 3)
			Me.smartPartPlaceholder1.Name = "smartPartPlaceholder1"
			Me.smartPartPlaceholder1.Size = New System.Drawing.Size(450, 284)
			Me.smartPartPlaceholder1.TabIndex = 0
			Me.smartPartPlaceholder1.Text = "smartPartPlaceholder1"
			' 
			' CustomerTabView
			' 
			Me.Controls.Add(Me.tabWorkspace1)
			Me.Name = "CustomerTabView"
			Me.Size = New System.Drawing.Size(464, 316)
			Me.tabWorkspace1.ResumeLayout(False)
			Me.tabPage1.ResumeLayout(False)
			Me.tabPage2.ResumeLayout(False)
			Me.ResumeLayout(False)

		End Sub

#End Region

		Private tabWorkspace1 As Microsoft.Practices.CompositeUI.WinForms.TabWorkspace
		Private tabPage1 As System.Windows.Forms.TabPage
		Private customerDetailView1 As CustomerDetailView
		Private tabPage2 As System.Windows.Forms.TabPage
		Private smartPartPlaceholder1 As Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder


	End Class
End Namespace
