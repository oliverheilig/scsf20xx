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
	Partial Public Class CustomerAccountsView
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
			Me.dataGridView1 = New System.Windows.Forms.DataGridView()
			Me.accountNumberDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
			Me.accountTypeDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
			Me.currentBalanceDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
			Me.CustomerAccountBindingSource = New System.Windows.Forms.BindingSource(Me.components)
			Me.toolCommands = New System.Windows.Forms.ToolStrip()
			CType(Me.dataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.CustomerAccountBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' dataGridView1
			' 
			Me.dataGridView1.AllowUserToAddRows = False
			Me.dataGridView1.AllowUserToDeleteRows = False
			Me.dataGridView1.AutoGenerateColumns = False
			Me.dataGridView1.Columns.Add(Me.accountNumberDataGridViewTextBoxColumn)
			Me.dataGridView1.Columns.Add(Me.accountTypeDataGridViewTextBoxColumn)
			Me.dataGridView1.Columns.Add(Me.currentBalanceDataGridViewTextBoxColumn)
			Me.dataGridView1.DataSource = Me.CustomerAccountBindingSource
			Me.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.dataGridView1.Location = New System.Drawing.Point(0, 25)
			Me.dataGridView1.Name = "dataGridView1"
			Me.dataGridView1.ReadOnly = True
			Me.dataGridView1.Size = New System.Drawing.Size(445, 246)
			Me.dataGridView1.TabIndex = 0
			Me.dataGridView1.Text = "dataGridView1"
			' 
			' accountNumberDataGridViewTextBoxColumn
			' 
			Me.accountNumberDataGridViewTextBoxColumn.DataPropertyName = "AccountNumber"
			Me.accountNumberDataGridViewTextBoxColumn.HeaderText = "Account Number"
			Me.accountNumberDataGridViewTextBoxColumn.Name = "AccountNumber"
			Me.accountNumberDataGridViewTextBoxColumn.ReadOnly = True
			' 
			' accountTypeDataGridViewTextBoxColumn
			' 
			Me.accountTypeDataGridViewTextBoxColumn.DataPropertyName = "AccountType"
			Me.accountTypeDataGridViewTextBoxColumn.HeaderText = "Account Type"
			Me.accountTypeDataGridViewTextBoxColumn.Name = "AccountType"
			Me.accountTypeDataGridViewTextBoxColumn.ReadOnly = True
			' 
			' currentBalanceDataGridViewTextBoxColumn
			' 
			Me.currentBalanceDataGridViewTextBoxColumn.DataPropertyName = "CurrentBalance"
			Me.currentBalanceDataGridViewTextBoxColumn.HeaderText = "Current Balance"
			Me.currentBalanceDataGridViewTextBoxColumn.Name = "CurrentBalance"
			Me.currentBalanceDataGridViewTextBoxColumn.ReadOnly = True
			' 
			' CustomerAccountBindingSource
			' 
			Me.CustomerAccountBindingSource.DataSource = GetType(BankTellerCommon.CustomerAccount)
			' 
			' toolCommands
			' 
			Me.toolCommands.Location = New System.Drawing.Point(0, 0)
			Me.toolCommands.Name = "toolCommands"
			Me.toolCommands.Size = New System.Drawing.Size(445, 25)
			Me.toolCommands.TabIndex = 1
			' 
			' CustomerAccountsView
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.Controls.Add(Me.dataGridView1)
			Me.Controls.Add(Me.toolCommands)
			Me.Name = "CustomerAccountsView"
			Me.Size = New System.Drawing.Size(445, 271)
			'			Me.Load += New System.EventHandler(Me.CustomerAccountsView_Load);
			CType(Me.dataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.CustomerAccountBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

#End Region

		Private dataGridView1 As System.Windows.Forms.DataGridView
		Private accountNumberDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
		Private accountTypeDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
		Private currentBalanceDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
		Private CustomerAccountBindingSource As System.Windows.Forms.BindingSource
		Private toolCommands As System.Windows.Forms.ToolStrip



	End Class
End Namespace
