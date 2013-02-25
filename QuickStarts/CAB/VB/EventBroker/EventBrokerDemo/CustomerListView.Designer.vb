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
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Namespace EventBrokerDemo
	Partial Public Class CustomerListView
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
			Me.lstGlobalCustomers = New System.Windows.Forms.ListBox()
			Me.btnAddGlobalCustomer = New System.Windows.Forms.Button()
			Me.txtCustomerId = New System.Windows.Forms.TextBox()
			Me.btnAddLocalCustomer = New System.Windows.Forms.Button()
			Me.lstLocalCustomers = New System.Windows.Forms.ListBox()
			Me.btnProcessLocal = New System.Windows.Forms.Button()
			Me.textBox3 = New System.Windows.Forms.TextBox()
			Me.label1 = New System.Windows.Forms.Label()
			Me.label2 = New System.Windows.Forms.Label()
			Me.btnCancelProcess = New System.Windows.Forms.Button()
			Me.SuspendLayout()
			' 
			' lstGlobalCustomers
			' 
			Me.lstGlobalCustomers.FormattingEnabled = True
			Me.lstGlobalCustomers.Location = New System.Drawing.Point(12, 29)
			Me.lstGlobalCustomers.Name = "lstGlobalCustomers"
			Me.lstGlobalCustomers.Size = New System.Drawing.Size(268, 108)
			Me.lstGlobalCustomers.TabIndex = 0
			' 
			' btnAddGlobalCustomer
			' 
			Me.btnAddGlobalCustomer.Location = New System.Drawing.Point(205, 258)
			Me.btnAddGlobalCustomer.Name = "btnAddGlobalCustomer"
			Me.btnAddGlobalCustomer.Size = New System.Drawing.Size(75, 23)
			Me.btnAddGlobalCustomer.TabIndex = 1
			Me.btnAddGlobalCustomer.Text = "Add Global"
			' 
			' txtCustomerId
			' 
			Me.txtCustomerId.Location = New System.Drawing.Point(12, 260)
			Me.txtCustomerId.Name = "txtCustomerId"
			Me.txtCustomerId.Size = New System.Drawing.Size(101, 20)
			Me.txtCustomerId.TabIndex = 2
			' 
			' btnAddLocalCustomer
			' 
			Me.btnAddLocalCustomer.Location = New System.Drawing.Point(124, 258)
			Me.btnAddLocalCustomer.Name = "btnAddLocalCustomer"
			Me.btnAddLocalCustomer.Size = New System.Drawing.Size(75, 23)
			Me.btnAddLocalCustomer.TabIndex = 3
			Me.btnAddLocalCustomer.Text = "Add Local"
			' 
			' lstLocalCustomers
			' 
			Me.lstLocalCustomers.FormattingEnabled = True
			Me.lstLocalCustomers.Location = New System.Drawing.Point(12, 170)
			Me.lstLocalCustomers.Name = "lstLocalCustomers"
			Me.lstLocalCustomers.Size = New System.Drawing.Size(268, 82)
			Me.lstLocalCustomers.TabIndex = 4
			' 
			' btnProcessLocal
			' 
			Me.btnProcessLocal.Location = New System.Drawing.Point(13, 329)
			Me.btnProcessLocal.Name = "btnProcessLocal"
			Me.btnProcessLocal.Size = New System.Drawing.Size(156, 23)
			Me.btnProcessLocal.TabIndex = 5
			Me.btnProcessLocal.Text = "Process Local"
			' 
			' textBox3
			' 
			Me.textBox3.BackColor = System.Drawing.SystemColors.Menu
			Me.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None
			Me.textBox3.Location = New System.Drawing.Point(12, 290)
			Me.textBox3.Multiline = True
			Me.textBox3.Name = "textBox3"
			Me.textBox3.Size = New System.Drawing.Size(268, 33)
			Me.textBox3.TabIndex = 17
			Me.textBox3.Text = "This button starts a background worker that moves the local customers to the glob" & "al customer list."
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Location = New System.Drawing.Point(12, 9)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(88, 13)
			Me.label1.TabIndex = 18
			Me.label1.Text = "Global customers"
			' 
			' label2
			' 
			Me.label2.AutoSize = True
			Me.label2.Location = New System.Drawing.Point(12, 154)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(84, 13)
			Me.label2.TabIndex = 19
			Me.label2.Text = "Local customers"
			' 
			' btnCancelProcess
			' 
			Me.btnCancelProcess.Enabled = False
			Me.btnCancelProcess.Location = New System.Drawing.Point(205, 329)
			Me.btnCancelProcess.Name = "btnCancelProcess"
			Me.btnCancelProcess.Size = New System.Drawing.Size(75, 23)
			Me.btnCancelProcess.TabIndex = 20
			Me.btnCancelProcess.Text = "Cancel"
			' 
			' CustomerListView
			' 
			Me.ClientSize = New System.Drawing.Size(292, 364)
			Me.Controls.Add(Me.btnCancelProcess)
			Me.Controls.Add(Me.label2)
			Me.Controls.Add(Me.label1)
			Me.Controls.Add(Me.textBox3)
			Me.Controls.Add(Me.btnProcessLocal)
			Me.Controls.Add(Me.lstLocalCustomers)
			Me.Controls.Add(Me.btnAddLocalCustomer)
			Me.Controls.Add(Me.txtCustomerId)
			Me.Controls.Add(Me.btnAddGlobalCustomer)
			Me.Controls.Add(Me.lstGlobalCustomers)
			Me.Name = "CustomerListView"
			Me.Text = "CustomerListView"
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

#End Region

		Private lstGlobalCustomers As ListBox
		Private WithEvents btnAddGlobalCustomer As Button
		Private txtCustomerId As TextBox
		Private WithEvents btnAddLocalCustomer As Button
		Private lstLocalCustomers As ListBox
		Private WithEvents btnProcessLocal As Button
		Private textBox3 As TextBox
		Private label1 As Label
		Private label2 As Label
		Private WithEvents btnCancelProcess As Button
	End Class
End Namespace