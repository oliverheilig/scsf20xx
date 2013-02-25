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
	Partial Public Class CustomerHeaderView
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
			Me.lblFirstName = New System.Windows.Forms.Label()
			Me.lblLastName = New System.Windows.Forms.Label()
			Me.lblCustomerID = New System.Windows.Forms.Label()
			Me.txtCustomerID = New System.Windows.Forms.TextBox()
			Me.txtFirstName = New System.Windows.Forms.TextBox()
			Me.txtLastName = New System.Windows.Forms.TextBox()
			Me.customerBindingSource = New System.Windows.Forms.BindingSource(Me.components)
			CType(Me.customerBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' lblFirstName
			' 
			Me.lblFirstName.AutoSize = True
			Me.lblFirstName.Location = New System.Drawing.Point(3, 35)
			Me.lblFirstName.Name = "lblFirstName"
			Me.lblFirstName.Size = New System.Drawing.Size(56, 13)
			Me.lblFirstName.TabIndex = 0
			Me.lblFirstName.Text = "First Name:"
			' 
			' lblLastName
			' 
			Me.lblLastName.AutoSize = True
			Me.lblLastName.Location = New System.Drawing.Point(3, 62)
			Me.lblLastName.Name = "lblLastName"
			Me.lblLastName.Size = New System.Drawing.Size(57, 13)
			Me.lblLastName.TabIndex = 1
			Me.lblLastName.Text = "Last Name:"
			' 
			' lblCustomerID
			' 
			Me.lblCustomerID.AutoSize = True
			Me.lblCustomerID.Location = New System.Drawing.Point(3, 9)
			Me.lblCustomerID.Name = "lblCustomerID"
			Me.lblCustomerID.Size = New System.Drawing.Size(64, 13)
			Me.lblCustomerID.TabIndex = 2
			Me.lblCustomerID.Text = "Customer ID:"
			' 
			' txtCustomerID
			' 
			Me.txtCustomerID.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.txtCustomerID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.customerBindingSource, "ID", True))
			Me.txtCustomerID.Location = New System.Drawing.Point(73, 6)
			Me.txtCustomerID.Name = "txtCustomerID"
			Me.txtCustomerID.ReadOnly = True
			Me.txtCustomerID.Size = New System.Drawing.Size(176, 20)
			Me.txtCustomerID.TabIndex = 3
			' 
			' txtFirstName
			' 
			Me.txtFirstName.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.txtFirstName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.customerBindingSource, "FirstName", True))
			Me.txtFirstName.Location = New System.Drawing.Point(73, 32)
			Me.txtFirstName.Name = "txtFirstName"
			Me.txtFirstName.ReadOnly = True
			Me.txtFirstName.Size = New System.Drawing.Size(176, 20)
			Me.txtFirstName.TabIndex = 4
			' 
			' txtLastName
			' 
			Me.txtLastName.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.txtLastName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.customerBindingSource, "LastName", True))
			Me.txtLastName.Location = New System.Drawing.Point(73, 59)
			Me.txtLastName.Name = "txtLastName"
			Me.txtLastName.ReadOnly = True
			Me.txtLastName.Size = New System.Drawing.Size(176, 20)
			Me.txtLastName.TabIndex = 5
			' 
			' customerBindingSource
			' 
			Me.customerBindingSource.DataSource = GetType(BankTellerCommon.Customer)
			' 
			' CustomerHeaderView
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.Controls.Add(Me.txtLastName)
			Me.Controls.Add(Me.txtFirstName)
			Me.Controls.Add(Me.txtCustomerID)
			Me.Controls.Add(Me.lblCustomerID)
			Me.Controls.Add(Me.lblLastName)
			Me.Controls.Add(Me.lblFirstName)
			Me.Name = "CustomerHeaderView"
			Me.Size = New System.Drawing.Size(256, 85)
			'			Me.Load += New System.EventHandler(Me.CustomerHeaderView_Load);
			CType(Me.customerBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

#End Region

		Private lblCustomerID As System.Windows.Forms.Label
		Private lblFirstName As System.Windows.Forms.Label
		Private lblLastName As System.Windows.Forms.Label
		Private txtCustomerID As System.Windows.Forms.TextBox
		Private txtFirstName As System.Windows.Forms.TextBox
		Private txtLastName As System.Windows.Forms.TextBox
		Private customerBindingSource As System.Windows.Forms.BindingSource
	End Class
End Namespace
