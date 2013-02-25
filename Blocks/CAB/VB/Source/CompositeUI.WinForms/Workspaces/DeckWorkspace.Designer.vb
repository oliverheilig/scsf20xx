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
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.ComponentModel.Design
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms.Design


''' <summary>
''' Used to support designer.
''' </summary>
<Designer(GetType(DeckWorkspace.DeckWorkspaceDesigner), GetType(IDesigner)), Docking(DockingBehavior.Ask), ToolboxBitmap(GetType(DeckWorkspace), "DeckWorkspace")> _
Partial Public Class DeckWorkspace
	Friend Class DeckWorkspaceDesigner : Inherits ControlDesigner
		Protected Overrides Sub OnPaintAdornments(ByVal pe As PaintEventArgs)
			Dim bmp As Bitmap = New Bitmap(Control.Width, Control.Height)
			Dim gr As Graphics = Graphics.FromImage(bmp)

			Using pen As Pen = New Pen(Control.ForeColor)
				pen.DashStyle = DashStyle.Dash

				Using outerBrush As Brush = New SolidBrush(Control.ForeColor)
					Dim r As Rectangle = Control.ClientRectangle
					r.Height -= 1
					r.Width -= 1

					' Outer rectangle.
					gr.DrawRectangle(pen, r)

					Dim oneNoteColours As Color() = New Color() {Control.BackColor, Control.BackColor, Control.BackColor}

					pen.DashStyle = DashStyle.Solid
					Dim scaleFactor As Integer = 10
					Dim delta As Integer = scaleFactor * (oneNoteColours.Length + 1)
					Dim deckSize As Size = New Size(r.Width - delta, r.Height - delta)

					Dim i As Integer = 1
					Do While i < oneNoteColours.Length + 1
						Dim deck As Rectangle = New Rectangle(New Point(r.X + scaleFactor * i, r.Y + scaleFactor * i), deckSize)
						gr.DrawRectangle(pen, deck)
						Using fill As Brush = New SolidBrush(oneNoteColours(i - 1))
							gr.FillRectangle(fill, New Rectangle(deck.X, deck.Y, deck.Width, deck.Height))
							deck.X += 1
							deck.Y += 1
							deck.Width -= 1
							deck.Height -= 1
						End Using
						i += 1
					Loop
				End Using
			End Using

			pe.Graphics.DrawImage(bmp, 0, 0)
		End Sub

		Protected Overrides Sub OnGiveFeedback(ByVal e As GiveFeedbackEventArgs)
			MyBase.OnGiveFeedback(New GiveFeedbackEventArgs(DragDropEffects.None, e.UseDefaultCursors))
		End Sub

		Protected Overrides Sub OnDragDrop(ByVal de As DragEventArgs)
			' Do not allow drops.
		End Sub
	End Class
End Class
