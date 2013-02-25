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
Namespace DesignTime
	Partial Friend Class DeckWorkspaceForm
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

#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.deckWorkspace1 = New Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace()
			Me.SuspendLayout()
			' 
			' deckWorkspace1
			' 
			Me.deckWorkspace1.Location = New System.Drawing.Point(12, 43)
			Me.deckWorkspace1.Name = "deckWorkspace1"
			Me.deckWorkspace1.Size = New System.Drawing.Size(270, 201)
			Me.deckWorkspace1.TabIndex = 0
			Me.deckWorkspace1.Text = "deckWorkspace1"
			' 
			' DeckWorkspaceForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(292, 273)
			Me.Controls.Add(Me.deckWorkspace1)
			Me.Name = "DeckWorkspaceForm"
			Me.Text = "DeckWorkspaceForm"
			Me.ResumeLayout(False)

		End Sub

#End Region

		Private deckWorkspace1 As DeckWorkspace







	End Class
End Namespace