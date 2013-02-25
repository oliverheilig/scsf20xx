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
	Partial Public Class CustomerCommentsView
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
			Me.customerBindingSource = New System.Windows.Forms.BindingSource(Me.components)
			Me.commentsTextBox = New System.Windows.Forms.TextBox()
			CType(Me.customerBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' customerBindingSource
			' 
			Me.customerBindingSource.DataSource = GetType(BankTellerCommon.Customer)
			' 
			' commentsTextBox
			' 
			Me.commentsTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.customerBindingSource, "Comments", True))
			Me.commentsTextBox.Dock = System.Windows.Forms.DockStyle.Fill
			Me.commentsTextBox.Location = New System.Drawing.Point(3, 3)
			Me.commentsTextBox.Multiline = True
			Me.commentsTextBox.Name = "commentsTextBox"
			Me.commentsTextBox.Size = New System.Drawing.Size(368, 314)
			Me.commentsTextBox.TabIndex = 1
			' 
			' CustomerCommentsView
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.Controls.Add(Me.commentsTextBox)
			Me.Name = "CustomerCommentsView"
			Me.Padding = New System.Windows.Forms.Padding(3)
			Me.Size = New System.Drawing.Size(374, 320)
			'			Me.Load += New System.EventHandler(Me.CustomerCommentsView_Load);
			CType(Me.customerBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

#End Region

		Private customerBindingSource As System.Windows.Forms.BindingSource
		Private commentsTextBox As System.Windows.Forms.TextBox
	End Class
End Namespace
