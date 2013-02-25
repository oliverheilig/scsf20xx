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
	''' Provides data for the cancellable events exposed by 
	''' <see cref="IWorkspace"/>.
	''' </summary>
	Public Class WorkspaceCancelEventArgs : Inherits WorkspaceEventArgs
		Private innerCancel As Boolean

		''' <summary>
		''' Initializes a new <see cref="WorkspaceCancelEventArgs"/> using
		''' the specified SmartPart.
		''' </summary>
		''' <param name="smartPart"></param>
		Public Sub New(ByVal smartPart As Object)
			MyBase.New(smartPart)
		End Sub

		''' <summary>
		''' Gets or sets a value indicating whether the event should be canceled.
		''' </summary>
		Public Property Cancel() As Boolean
			Get
				Return innerCancel
			End Get
			Set(ByVal value As Boolean)
				innerCancel = value
			End Set
		End Property
	End Class
End Namespace