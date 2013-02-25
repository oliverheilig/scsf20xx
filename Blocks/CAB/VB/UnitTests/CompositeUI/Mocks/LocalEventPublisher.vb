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
Imports System.ComponentModel
Imports Microsoft.Practices.CompositeUI.EventBroker

Namespace Mocks

	<System.ComponentModel.DesignerCategory("Code")> _
	Public Class LocalEventPublisher : Inherits Component
		<EventPublication("LocalEvent", PublicationScope.WorkItem)> _
		Public Event [Event] As EventHandler

		Public Sub FireTheEventHandler()
			If Not EventEvent Is Nothing Then
				RaiseEvent Event(Me, EventArgs.Empty)
			End If
		End Sub

		Public ReadOnly Property EventIsNull() As Boolean
			Get
				Return EventEvent Is Nothing
			End Get
		End Property
	End Class


	Public Class LocalObjectEventPublisher
		<EventPublicationAttribute("LocalEvent", PublicationScope.WorkItem)> _
		Public Event [Event] As EventHandler

		Public Sub FireTheEventHandler()
			If Not EventEvent Is Nothing Then
				RaiseEvent Event(Me, EventArgs.Empty)
			End If
		End Sub

		Public ReadOnly Property EventIsNull() As Boolean
			Get
				Return EventEvent Is Nothing
			End Get
		End Property
	End Class
End Namespace