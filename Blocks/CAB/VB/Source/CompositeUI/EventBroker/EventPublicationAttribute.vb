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

Namespace EventBroker
	''' <summary>
	''' Declares an event as an <see cref="EventTopic"/> publication.
	''' </summary>
	<AttributeUsage(AttributeTargets.Event, AllowMultiple:=True)> _
	Public NotInheritable Class EventPublicationAttribute : Inherits Attribute
		Private eventTopic As String

		Private eventScope As PublicationScope

		''' <summary>
		''' Declares an event publication for an <see cref="EventTopic"/> with the specified topic and 
		''' a <c>Global</c> <see cref="PublicationScope"/>.
		''' </summary>
		''' <param name="anEventTopic">The name of the <see cref="EventTopic"/> the decorated event will publish.</param>
		Public Sub New(ByVal anEventTopic As String)
			Me.New(anEventTopic, PublicationScope.Global)
		End Sub

		''' <summary>
		''' Declares an event publication for an <see cref="EventTopic"/> with the specified topic and
		''' <see cref="PublicationScope"/>.
		''' </summary>
		''' <param name="anEventTopic">The name of the <see cref="EventTopic"/> the event will publish.</param>
		''' <param name="anEventScope">The scope for the publication.</param>
		Public Sub New(ByVal anEventTopic As String, ByVal anEventScope As PublicationScope)
			Me.eventTopic = anEventTopic
			Me.eventScope = anEventScope
		End Sub

		''' <summary>
		''' The name of the <see cref="EventTopic"/> the decorated event will publish.
		''' </summary>
		Public ReadOnly Property Topic() As String
			Get
				Return eventTopic
			End Get
		End Property

		''' <summary>
		''' The <see cref="PublicationScope"/> for the publication.
		''' </summary>
		Public ReadOnly Property Scope() As PublicationScope
			Get
				Return Me.eventScope
			End Get
		End Property
	End Class
End Namespace