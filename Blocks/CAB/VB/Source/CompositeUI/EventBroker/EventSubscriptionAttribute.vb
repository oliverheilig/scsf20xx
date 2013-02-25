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

Namespace EventBroker
	''' <summary>
	''' Declares a method as an <see cref="EventTopic"/> subscription.
	''' </summary>
	<AttributeUsage(AttributeTargets.Method, AllowMultiple:=True)> _
	Public Class EventSubscriptionAttribute : Inherits Attribute
		Private eventTopic As String
		Private threadOption As ThreadOption

		''' <summary>
		''' Declares a method as an <see cref="EventTopic"/> subscription.
		''' </summary>
		''' <param name="anEventTopic">The name of the <see cref="EventTopic"/> to subscribe to.</param>
		Public Sub New(ByVal anEventTopic As String)
			Me.New(anEventTopic, threadOption.Publisher)
		End Sub

		''' <summary>
		''' Delcares a method as an <see cref="EventTopic"/> subscription using the specified <see cref="ThreadOption"/>.
		''' </summary>
		''' <param name="anEventTopic">The name of the <see cref="EventTopic"/> to subscribe to.</param>
		''' <param name="aThreadOption">The <see cref="ThreadOption"/> indicating how the method should be called.</param>
		Public Sub New(ByVal anEventTopic As String, ByVal aThreadOption As ThreadOption)
			Me.eventTopic = anEventTopic
			Me.threadOption = aThreadOption
		End Sub

		''' <summary>
		''' The name of the <see cref="EventTopic"/> the decorated method is subscribed to.
		''' </summary>
		Public ReadOnly Property Topic() As String
			Get
				Return eventTopic
			End Get
		End Property

		''' <summary>
		''' Indicates the way the subscription method should be called, see
		''' <see cref="ThreadOption"/> enumeration for the supported modes.
		''' </summary>
		<DefaultValue(threadOption.Publisher)> _
		Public Property Thread() As ThreadOption
			Set(ByVal value As ThreadOption)
				threadOption = Value
			End Set
			Get
				Return threadOption
			End Get
		End Property
	End Class
End Namespace