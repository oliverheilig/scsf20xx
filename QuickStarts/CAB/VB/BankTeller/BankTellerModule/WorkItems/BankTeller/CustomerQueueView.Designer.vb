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
	Partial Public Class CustomerQueueView
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
			Me.panel1 = New System.Windows.Forms.Panel()
			Me.btnNextCustomer = New System.Windows.Forms.Button()
			Me.listCustomers = New System.Windows.Forms.ListBox()
			Me.label1 = New System.Windows.Forms.Label()
			Me.panel1.SuspendLayout()
			Me.SuspendLayout()
			' 
			' panel1
			' 
			Me.panel1.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.panel1.Controls.Add(Me.btnNextCustomer)
			Me.panel1.Controls.Add(Me.listCustomers)
			Me.panel1.Controls.Add(Me.label1)
			Me.panel1.Location = New System.Drawing.Point(4, 4)
			Me.panel1.Name = "panel1"
			Me.panel1.Padding = New System.Windows.Forms.Padding(5)
			Me.panel1.Size = New System.Drawing.Size(182, 394)
			Me.panel1.TabIndex = 0
			' 
			' btnNextCustomer
			' 
			Me.btnNextCustomer.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.btnNextCustomer.Location = New System.Drawing.Point(8, 28)
			Me.btnNextCustomer.Name = "btnNextCustomer"
			Me.btnNextCustomer.Size = New System.Drawing.Size(166, 26)
			Me.btnNextCustomer.TabIndex = 5
			Me.btnNextCustomer.Text = "Accept Customer"
			'			Me.btnNextCustomer.Click += New System.EventHandler(Me.OnAcceptCustomer);
			' 
			' listCustomers
			' 
			Me.listCustomers.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.listCustomers.FormattingEnabled = True
			Me.listCustomers.IntegralHeight = False
			Me.listCustomers.Location = New System.Drawing.Point(8, 61)
			Me.listCustomers.Name = "listCustomers"
			Me.listCustomers.Size = New System.Drawing.Size(166, 321)
			Me.listCustomers.TabIndex = 4
			Me.listCustomers.ValueMember = "Count"
			'			Me.listCustomers.SelectedIndexChanged += New System.EventHandler(Me.OnCustomerSelectionChanged);
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CByte(0)))
			Me.label1.Location = New System.Drawing.Point(8, 5)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(122, 20)
			Me.label1.TabIndex = 3
			Me.label1.Text = "My Customers"
			' 
			' CustomerQueueView
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.Controls.Add(Me.panel1)
			Me.Name = "CustomerQueueView"
			Me.Size = New System.Drawing.Size(189, 401)
			Me.panel1.ResumeLayout(False)
			Me.panel1.PerformLayout()
			Me.ResumeLayout(False)

		End Sub

#End Region

		Private panel1 As System.Windows.Forms.Panel
		Private WithEvents btnNextCustomer As System.Windows.Forms.Button
		Private WithEvents listCustomers As System.Windows.Forms.ListBox
		Private label1 As System.Windows.Forms.Label

	End Class
End Namespace
