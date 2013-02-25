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
Imports Microsoft.Practices.CompositeUI.Utility
Imports System.Reflection
Imports System.Windows.Forms
Imports Microsoft.Practices.CompositeUI.Commands


''' <summary>
''' An <see cref="EventCommandAdapter{T}"/> that updates a <see cref="ToolStripItem"/> based on the changes to 
''' the <see cref="Command.Status"/> property value.
''' </summary>
Public Class ToolStripItemCommandAdapter : Inherits EventCommandAdapter(Of ToolStripItem)
	''' <summary>
	''' Initializes a new instance of the <see cref="ToolStripItemCommandAdapter"/> class
	''' </summary>
	Public Sub New()
		MyBase.New()
	End Sub

	''' <summary>
	''' Initializes the adapter with the given <see cref="ToolStripItem"/>.
	''' </summary>
	Public Sub New(ByVal item As ToolStripItem, ByVal eventName As String)
		MyBase.New(item, eventName)
	End Sub

	''' <summary>
	''' Handles the changes in the <see cref="Command"/> by refreshing 
	''' the <see cref="ToolStripItem.Enabled"/> property.
	''' </summary>
	Protected Overrides Sub OnCommandChanged(ByVal command As Command)
		MyBase.OnCommandChanged(command)

		For Each pair As KeyValuePair(Of ToolStripItem, List(Of String)) In Invokers
			pair.Key.Enabled = (command.Status = CommandStatus.Enabled)
			pair.Key.Visible = (command.Status <> CommandStatus.Unavailable)
		Next
	End Sub

End Class

