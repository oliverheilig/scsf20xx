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
Imports System.Diagnostics
Imports System.Windows.Forms

Namespace EventBrokerDemo
	' This control shows a simple usage of the diagnostics
	' capabilities included in CAB.
	' It uses a TextBox and a custom listener to report the 
	' log messages issued by the EventBroker subsystem.
	Partial Public Class TraceTextBox
		Inherits UserControl

		Private listener As TextBoxTraceListener

		Public Sub New()
			InitializeComponent()
			listener = New TextBoxTraceListener(logBox)

			' Just add the listener to the trace system.
			Trace.Listeners.Add(listener)
		End Sub

		Private Sub enableLogging_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles enableLogging.CheckedChanged
			If Not listener Is Nothing Then
				listener.Enabled = enableLogging.Checked
			End If
		End Sub

		' This is our custom TraceListener which just write the messages
		' to a supplied TextBox control.
		Private Class TextBoxTraceListener : Inherits TraceListener
			Private log As TextBox

			Private innerEnabled As Boolean = True

			Public Property Enabled() As Boolean
				Get
					Return innerEnabled
				End Get
				Set(ByVal value As Boolean)
					innerEnabled = value
				End Set
			End Property

			Public Sub New(ByVal log As TextBox)
				Me.log = log
			End Sub

			Public Overrides Sub TraceEvent(ByVal eventCache As TraceEventCache, ByVal source As String, ByVal eventType As TraceEventType, ByVal id As Integer, ByVal format As String, ByVal ParamArray args As Object())
				If args.Length > 0 Then
					WriteLine(String.Format(format, args))
				Else
					WriteLine(format)
				End If
			End Sub

			Private Delegate Sub InvokedWrite(ByVal message As String)

			Public Overloads Overrides Sub Write(ByVal message As String)
				If innerEnabled Then
					If log.InvokeRequired Then
						log.BeginInvoke(New InvokedWrite(AddressOf Write), message)
						Return
					End If
					log.Text = log.Text & Environment.NewLine & message
					log.Select(log.Text.Length - 1, 0)
					log.ScrollToCaret()
				End If
			End Sub


			Public Overloads Overrides Sub WriteLine(ByVal message As String)
				Write(message)
			End Sub
		End Class
	End Class
End Namespace