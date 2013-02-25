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
	Partial Public Class LaunchPadForm
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
			Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LaunchPadForm))
			Me.textBox2 = New System.Windows.Forms.TextBox()
			Me.textBox1 = New System.Windows.Forms.TextBox()
			Me.groupBox2 = New System.Windows.Forms.GroupBox()
			Me.btnNewCustomerList = New System.Windows.Forms.Button()
			Me.btnFireCustomerChange = New System.Windows.Forms.Button()
			Me.label2 = New System.Windows.Forms.Label()
			Me.groupBox1 = New System.Windows.Forms.GroupBox()
			Me.txtCustomerId = New System.Windows.Forms.TextBox()
			Me.lstGlobalCustomers = New System.Windows.Forms.ListBox()
			Me.label1 = New System.Windows.Forms.Label()
			Me.traceTextBox1 = New EventBrokerDemo.TraceTextBox()
			Me.label3 = New System.Windows.Forms.Label()
			Me.label4 = New System.Windows.Forms.Label()
			Me.groupBox2.SuspendLayout()
			Me.groupBox1.SuspendLayout()
			Me.SuspendLayout()
			' 
			' textBox2
			' 
			Me.textBox2.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.textBox2.BackColor = System.Drawing.SystemColors.Control
			Me.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None
			Me.textBox2.Location = New System.Drawing.Point(6, 19)
			Me.textBox2.Multiline = True
			Me.textBox2.Name = "textBox2"
			Me.textBox2.Size = New System.Drawing.Size(308, 60)
			Me.textBox2.TabIndex = 15
			Me.textBox2.Text = resources.GetString("textBox2.Text")
			' 
			' textBox1
			' 
			Me.textBox1.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.textBox1.BackColor = System.Drawing.SystemColors.Control
			Me.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
			Me.textBox1.Location = New System.Drawing.Point(11, 17)
			Me.textBox1.Multiline = True
			Me.textBox1.Name = "textBox1"
			Me.textBox1.Size = New System.Drawing.Size(308, 45)
			Me.textBox1.TabIndex = 14
			Me.textBox1.Text = "Enter a Customer ID here and click the Add Customer button. This will add a new c" & "ustomer object to the State and fire a global Event notifying subscribers who ar" & "e listening for that topic."
			' 
			' groupBox2
			' 
			Me.groupBox2.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.groupBox2.Controls.Add(Me.textBox2)
			Me.groupBox2.Controls.Add(Me.btnNewCustomerList)
			Me.groupBox2.Location = New System.Drawing.Point(12, 157)
			Me.groupBox2.Name = "groupBox2"
			Me.groupBox2.Size = New System.Drawing.Size(325, 122)
			Me.groupBox2.TabIndex = 19
			Me.groupBox2.TabStop = False
			' 
			' btnNewCustomerList
			' 
			Me.btnNewCustomerList.Anchor = (CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.btnNewCustomerList.Location = New System.Drawing.Point(200, 85)
			Me.btnNewCustomerList.Name = "btnNewCustomerList"
			Me.btnNewCustomerList.Size = New System.Drawing.Size(115, 23)
			Me.btnNewCustomerList.TabIndex = 11
			Me.btnNewCustomerList.Text = "Show List"
			' 
			' btnFireCustomerChange
			' 
			Me.btnFireCustomerChange.Anchor = (CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.btnFireCustomerChange.Location = New System.Drawing.Point(204, 71)
			Me.btnFireCustomerChange.Name = "btnFireCustomerChange"
			Me.btnFireCustomerChange.Size = New System.Drawing.Size(115, 23)
			Me.btnFireCustomerChange.TabIndex = 13
			Me.btnFireCustomerChange.Text = "Add Customer"
			' 
			' label2
			' 
			Me.label2.AutoSize = True
			Me.label2.Location = New System.Drawing.Point(9, 76)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(68, 13)
			Me.label2.TabIndex = 12
			Me.label2.Text = "Customer ID:"
			' 
			' groupBox1
			' 
			Me.groupBox1.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.groupBox1.Controls.Add(Me.textBox1)
			Me.groupBox1.Controls.Add(Me.btnFireCustomerChange)
			Me.groupBox1.Controls.Add(Me.txtCustomerId)
			Me.groupBox1.Controls.Add(Me.label2)
			Me.groupBox1.Location = New System.Drawing.Point(12, 28)
			Me.groupBox1.Name = "groupBox1"
			Me.groupBox1.Size = New System.Drawing.Size(325, 104)
			Me.groupBox1.TabIndex = 18
			Me.groupBox1.TabStop = False
			' 
			' txtCustomerId
			' 
			Me.txtCustomerId.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.txtCustomerId.Location = New System.Drawing.Point(80, 73)
			Me.txtCustomerId.Name = "txtCustomerId"
			Me.txtCustomerId.Size = New System.Drawing.Size(119, 20)
			Me.txtCustomerId.TabIndex = 10
			' 
			' lstGlobalCustomers
			' 
			Me.lstGlobalCustomers.Anchor = (CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.lstGlobalCustomers.FormattingEnabled = True
			Me.lstGlobalCustomers.Location = New System.Drawing.Point(343, 28)
			Me.lstGlobalCustomers.Name = "lstGlobalCustomers"
			Me.lstGlobalCustomers.Size = New System.Drawing.Size(178, 251)
			Me.lstGlobalCustomers.TabIndex = 21
			' 
			' label1
			' 
			Me.label1.Anchor = (CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.label1.AutoSize = True
			Me.label1.Location = New System.Drawing.Point(342, 9)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(70, 13)
			Me.label1.TabIndex = 22
			Me.label1.Text = "Customer List"
			' 
			' traceTextBox1
			' 
			Me.traceTextBox1.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.traceTextBox1.Location = New System.Drawing.Point(12, 294)
			Me.traceTextBox1.Name = "traceTextBox1"
			Me.traceTextBox1.Size = New System.Drawing.Size(509, 108)
			Me.traceTextBox1.TabIndex = 25
			' 
			' label3
			' 
			Me.label3.AutoSize = True
			Me.label3.Location = New System.Drawing.Point(9, 141)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(38, 13)
			Me.label3.TabIndex = 26
			Me.label3.Text = "Step 2"
			' 
			' label4
			' 
			Me.label4.AutoSize = True
			Me.label4.Location = New System.Drawing.Point(12, 9)
			Me.label4.Name = "label4"
			Me.label4.Size = New System.Drawing.Size(38, 13)
			Me.label4.TabIndex = 27
			Me.label4.Text = "Step 1"
			' 
			' LaunchPadForm
			' 
			Me.ClientSize = New System.Drawing.Size(533, 413)
			Me.Controls.Add(Me.label4)
			Me.Controls.Add(Me.label3)
			Me.Controls.Add(Me.traceTextBox1)
			Me.Controls.Add(Me.label1)
			Me.Controls.Add(Me.lstGlobalCustomers)
			Me.Controls.Add(Me.groupBox2)
			Me.Controls.Add(Me.groupBox1)
			Me.Name = "LaunchPadForm"
			Me.Text = "LaunchPadForm"
			'			Me.Load += New System.EventHandler(Me.LaunchPadForm_Load);
			Me.groupBox2.ResumeLayout(False)
			Me.groupBox2.PerformLayout()
			Me.groupBox1.ResumeLayout(False)
			Me.groupBox1.PerformLayout()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

#End Region

		Private textBox2 As TextBox
		Private textBox1 As TextBox
		Private groupBox2 As GroupBox
		Private WithEvents btnNewCustomerList As Button
		Private WithEvents btnFireCustomerChange As Button
		Private label2 As Label
		Private groupBox1 As GroupBox
		Private txtCustomerId As TextBox
		Private lstGlobalCustomers As ListBox
		Private label1 As Label
		Private traceTextBox1 As TraceTextBox
		Private label3 As Label
		Private label4 As Label
	End Class
End Namespace