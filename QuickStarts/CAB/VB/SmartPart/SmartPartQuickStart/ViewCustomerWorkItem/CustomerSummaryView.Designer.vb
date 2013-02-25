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
	Partial Public Class CustomerSummaryView
		Inherits SmartPartQuickStart.TitledSmartPart

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
			Me.summaryTextBox = New System.Windows.Forms.TextBox()
			Me.SuspendLayout()
			' 
			' summaryTextBox
			' 
			Me.summaryTextBox.Dock = System.Windows.Forms.DockStyle.Fill
			Me.summaryTextBox.Location = New System.Drawing.Point(0, 23)
			Me.summaryTextBox.Multiline = True
			Me.summaryTextBox.Name = "summaryTextBox"
			Me.summaryTextBox.Size = New System.Drawing.Size(588, 316)
			Me.summaryTextBox.TabIndex = 1
			' 
			' CustomerSummaryView
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.Controls.Add(Me.summaryTextBox)
			Me.Name = "CustomerSummaryView"
			Me.Title = "Summary"
			Me.Controls.SetChildIndex(Me.summaryTextBox, 0)
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

#End Region

		Private summaryTextBox As System.Windows.Forms.TextBox
	End Class
End Namespace
