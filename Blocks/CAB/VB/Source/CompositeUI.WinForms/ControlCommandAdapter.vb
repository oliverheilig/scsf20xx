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
Imports System.Text
Imports System.Windows.Forms
Imports Microsoft.Practices.CompositeUI.Commands


''' <summary>
''' An <see cref="EventCommandAdapter{Control}"/> that updates a <see cref="Control"/> based on the changes to 
''' the <see cref="Command.Status"/> property value.
''' </summary>
Public Class ControlCommandAdapter : Inherits EventCommandAdapter(Of Control)
	''' <summary>
	''' Initializes a new instance of the <see cref="ControlCommandAdapter"/> class.
	''' </summary>
	Public Sub New()
		MyBase.New()
	End Sub

	''' <summary>
	''' Initializes the adapter with the given <see cref="Control"/>.
	''' </summary>
	Public Sub New(ByVal control As Control, ByVal eventName As String)
		MyBase.New(control, eventName)
	End Sub

	''' <summary>
	''' Handles the changes in the <see cref="Command"/> by refreshing 
	''' the controls properties.
	''' </summary>
	''' <param name="command"></param>
	Protected Overrides Sub OnCommandChanged(ByVal command As Command)
		MyBase.OnCommandChanged(command)
		For Each pair As KeyValuePair(Of Control, List(Of String)) In Invokers
			pair.Key.Enabled = (command.Status = CommandStatus.Enabled)
			pair.Key.Visible = (command.Status <> CommandStatus.Unavailable)
		Next
	End Sub

End Class

