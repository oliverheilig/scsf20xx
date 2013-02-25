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
	Friend Class GlobalEventPublisher : Inherits Component
		<EventPublication("GlobalEvent", PublicationScope.Global)> _
		Public Event [Event] As EventHandler

		Public Sub OnEvent()
			If Not [EventEvent] Is Nothing Then
				RaiseEvent [Event](Me, New EventArgs())
			End If
		End Sub
	End Class

	Friend Class GlobalObjectEventPublisher
		<EventPublication("GlobalEvent", PublicationScope.Global)> _
		Public Event [Event] As EventHandler

		Public Sub OnEvent()
			If Not [EventEvent] Is Nothing Then
				RaiseEvent [Event](Me, New EventArgs())
			End If
		End Sub
	End Class
End Namespace