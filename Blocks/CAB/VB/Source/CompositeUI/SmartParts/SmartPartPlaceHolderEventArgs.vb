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
Imports Microsoft.Practices.CompositeUI.Utility

Namespace SmartParts
	''' <summary>
	''' Used by the the smartpart placeholder.
	''' </summary>
	Public Class SmartPartPlaceHolderEventArgs : Inherits EventArgs
		Private innerSmartPart As Object

		''' <summary>
		''' Initializes a new instance of the <see cref="SmartPartPlaceHolderEventArgs"/> using
		''' the specified SmartPart.
		''' </summary>
		''' <param name="smartPart"></param>
		Public Sub New(ByVal aSmartPart As Object)
			Guard.ArgumentNotNull(aSmartPart, "smartPart")
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