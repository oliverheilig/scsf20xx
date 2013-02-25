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
	Partial Public Class UserInfoView
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
			Me.label1 = New System.Windows.Forms.Label()
			Me.Principal = New System.Windows.Forms.Label()
			Me.SuspendLayout()
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Location = New System.Drawing.Point(10, 9)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(28, 13)
			Me.label1.TabIndex = 0
			Me.label1.Text = "User:"
			' 
			' Principal
			' 
			Me.Principal.AutoSize = True
			Me.Principal.Location = New System.Drawing.Point(44, 9)
			Me.Principal.Name = "Principal"
			Me.Principal.Size = New System.Drawing.Size(84, 13)
			Me.Principal.TabIndex = 1
			Me.Principal.Text = "(current principal)"
			' 
			' UserInfoView
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.Controls.Add(Me.Principal)
			Me.Controls.Add(Me.label1)
			Me.Name = "UserInfoView"
			Me.Size = New System.Drawing.Size(269, 66)
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

#End Region

		Private label1 As System.Windows.Forms.Label
		Private Principal As System.Windows.Forms.Label
	End Class
End Namespace
