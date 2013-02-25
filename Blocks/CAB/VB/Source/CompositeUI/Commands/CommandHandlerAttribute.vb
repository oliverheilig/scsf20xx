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

Namespace Commands
	''' <summary>
	''' Declares a method as a <see cref="Command"/> handler.
	''' </summary>
	<AttributeUsage(AttributeTargets.Method, AllowMultiple:=True)> _
	Public Class CommandHandlerAttribute : Inherits Attribute
		Private innerCommandName As String

		''' <summary>
		''' Declares a method as a handler for a <see cref="Command"/> with specified name.
		''' </summary>
		''' <param name="commandName">The name of the <see cref="Command"/> the method handles.</param>
		Public Sub New(ByVal aCommandName As String)
			Me.innerCommandName = aCommandName
		End Sub

		''' <summary>
		''' Gets the name of the <see cref="Command"/> that the decorated method handles.
		''' </summary>
		Public ReadOnly Property CommandName() As String
			Get
				Return Me.innerCommandName
			End Get
		End Property
	End Class
End Namespace