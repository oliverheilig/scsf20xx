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
	Public Class GlobalEventHandler : Inherits Component
		Public OnGlobalEventCalledCount As Integer = 0

		<EventSubscription("GlobalEvent")> _
		Public Sub OnGlobalEvent(ByVal sender As Object, ByVal args As EventArgs)
			OnGlobalEventCalledCount += 1
		End Sub
	End Class

	Public Class GlobalObjectEventHandler
		Public OnGlobalEventCalledCount As Integer = 0

		<EventSubscription("GlobalEvent")> _
		Public Sub OnGlobalEvent(ByVal sender As Object, ByVal args As EventArgs)
			OnGlobalEventCalledCount += 1
		End Sub
	End Class

End Namespace