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

Namespace SmartParts
	''' <summary>
	''' Provides data for the non-cancellable events exposed by 
	''' <see cref="IWorkspace"/>.
	''' </summary>
	Public Class WorkspaceEventArgs : Inherits EventArgs
		Private innerSmartPart As Object

		''' <summary>
		''' Initializes a new instance of the <see cref="WorkspaceEventArgs"/> using
		''' the specified SmartPart.
		''' </summary>
		''' <param name="smartPart"></param>
		Public Sub New(ByVal aSmartPart As Object)
			Me.innerSmartPart = aSmartPart
		End Sub

		''' <summary>
		''' Gets the SmartPart associated with this event.
		''' </summary>
		Public ReadOnly Property SmartPart() As Object
			Get
				Return innerSmartPart
			End Get
		End Property
	End Class
End Namespace