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
Imports Microsoft.Practices.CompositeUI.EventBroker

''' <summary>
''' Indicates a method that will handle state changed events.
''' </summary>
Public Class StateChangedAttribute
	Inherits EventSubscriptionAttribute

	''' <summary>
	''' Initializes a new instance of the <see cref="StateChangedAttribute"/> using the provided
	''' topic and thread options.
	''' </summary>
	''' <param name="topic">The state topic.</param>
	''' <param name="option">The threading option.</param>
	Public Sub New(ByVal topic As String, ByVal anOption As ThreadOption)
		MyBase.New(StateChangedTopic.BuildStateChangedTopicString(topic))
		Me.Thread = anOption
	End Sub
End Class
