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
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms

Namespace Visualizer
	Partial Friend Class VisualizerForm : Inherits Form
		''' <summary>
		''' Initializes a new instance of the <see cref="VisualizerForm"/> class
		''' </summary>
		Public Sub New()
			InitializeComponent()
		End Sub

		''' <summary>
		''' Handles the closing event.
		''' </summary>
		''' <param name="e"></param>
		Protected Overrides Sub OnClosing(ByVal e As CancelEventArgs)
			e.Cancel = True
			Me.WindowState = FormWindowState.Minimized
			MyBase.OnClosing(e)
		End Sub
	End Class
End Namespace