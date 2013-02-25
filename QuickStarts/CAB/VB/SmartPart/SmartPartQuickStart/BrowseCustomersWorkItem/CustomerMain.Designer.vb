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
Namespace SmartPartQuickStart.BrowseCustomersWorkItem
	Partial Public Class CustomerMain
		Inherits System.Windows.Forms.UserControl
		Implements Microsoft.Practices.CompositeUI.SmartParts.IWorkspace

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
			Me.splitContainer1 = New System.Windows.Forms.SplitContainer()
			Me.customerListView1 = New SmartPartQuickStart.BrowseCustomersWorkItem.CustomerListView()
			Me.customersDeckedWorkspace = New Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace()
			Me.splitContainer1.Panel1.SuspendLayout()
			Me.splitContainer1.Panel2.SuspendLayout()
			Me.splitContainer1.SuspendLayout()
			Me.SuspendLayout()
			' 
			' splitContainer1
			' 
			Me.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.splitContainer1.Location = New System.Drawing.Point(0, 0)
			Me.splitContainer1.Name = "splitContainer1"
			' 
			' splitContainer1.Panel1
			' 
			Me.splitContainer1.Panel1.Controls.Add(Me.customerListView1)
			' 
			' splitContainer1.Panel2
			' 
			Me.splitContainer1.Panel2.Controls.Add(Me.customersDeckedWorkspace)
			Me.splitContainer1.Size = New System.Drawing.Size(655, 529)
			Me.splitContainer1.SplitterDistance = 219
			Me.splitContainer1.TabIndex = 1
			Me.splitContainer1.Text = "splitContainer1"
			' 
			' customerListView1
			' 
			Me.customerListView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
			Me.customerListView1.Description = ""
			Me.customerListView1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.customerListView1.Location = New System.Drawing.Point(0, 0)
			Me.customerListView1.Name = "customerListView1"
			Me.customerListView1.Size = New System.Drawing.Size(219, 529)
			Me.customerListView1.TabIndex = 0
			Me.customerListView1.Title = "Customers"
			' 
			' customersDeckedWorkspace
			' 
			Me.customersDeckedWorkspace.Dock = System.Windows.Forms.DockStyle.Fill
			Me.customersDeckedWorkspace.Location = New System.Drawing.Point(0, 0)
			Me.customersDeckedWorkspace.Name = "customersDeckedWorkspace"
			Me.customersDeckedWorkspace.Size = New System.Drawing.Size(432, 529)
			Me.customersDeckedWorkspace.TabIndex = 0
			' 
			' CustomerMain
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.Controls.Add(Me.splitContainer1)
			Me.Name = "CustomerMain"
			Me.Size = New System.Drawing.Size(655, 529)
			Me.splitContainer1.Panel1.ResumeLayout(False)
			Me.splitContainer1.Panel2.ResumeLayout(False)
			Me.splitContainer1.ResumeLayout(False)
			Me.ResumeLayout(False)

		End Sub

#End Region

		Private splitContainer1 As System.Windows.Forms.SplitContainer
		Private customerListView1 As CustomerListView
		Friend customersDeckedWorkspace As Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace
	End Class
End Namespace
