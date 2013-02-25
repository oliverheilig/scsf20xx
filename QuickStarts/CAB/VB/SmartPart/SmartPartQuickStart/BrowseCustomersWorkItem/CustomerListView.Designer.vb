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
	Partial Public Class CustomerListView
		Inherits TitledSmartPart

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
			Me.customerListBox = New System.Windows.Forms.ListBox()
			Me.SuspendLayout()
			' 
			' customerListBox
			' 
			Me.customerListBox.BorderStyle = System.Windows.Forms.BorderStyle.None
			Me.customerListBox.Dock = System.Windows.Forms.DockStyle.Fill
			Me.customerListBox.FormattingEnabled = True
			Me.customerListBox.Location = New System.Drawing.Point(0, 23)
			Me.customerListBox.Name = "customerListBox"
			Me.customerListBox.Size = New System.Drawing.Size(240, 403)
			Me.customerListBox.TabIndex = 1
			' 
			' CustomerListView
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
			Me.Controls.Add(Me.customerListBox)
			Me.Name = "CustomerListView"
			Me.Size = New System.Drawing.Size(240, 433)
			Me.Title = "Customers"
			Me.Controls.SetChildIndex(Me.customerListBox, 0)
			Me.ResumeLayout(False)

		End Sub

#End Region

		Private customerListBox As System.Windows.Forms.ListBox
	End Class
End Namespace
