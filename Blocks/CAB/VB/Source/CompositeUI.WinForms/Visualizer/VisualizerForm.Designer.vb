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

Namespace Visualizer
	Partial Friend Class VisualizerForm
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
			Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(VisualizerForm))
			Me.MainWorkspace = New Microsoft.Practices.CompositeUI.WinForms.TabWorkspace()
			Me.SuspendLayout()
			' 
			' MainWorkspace
			' 
			resources.ApplyResources(Me.MainWorkspace, "MainWorkspace")
			Me.MainWorkspace.Multiline = True
			Me.MainWorkspace.Name = "MainWorkspace"
			Me.MainWorkspace.SelectedIndex = 0
			' 
			' VisualizerForm
			' 
			resources.ApplyResources(Me, "$this")
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.Controls.Add(Me.MainWorkspace)
			Me.Name = "VisualizerForm"
			Me.ResumeLayout(False)

		End Sub

#End Region

		Private MainWorkspace As TabWorkspace
	End Class

End Namespace