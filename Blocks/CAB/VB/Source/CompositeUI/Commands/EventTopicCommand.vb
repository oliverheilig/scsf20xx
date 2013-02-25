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
Imports System.Collections.Generic
Imports System.Text
Imports Microsoft.Practices.CompositeUI.Utility
Imports Microsoft.Practices.CompositeUI.EventBroker
Imports System.Globalization

Namespace Commands
	''' <summary>
	''' Defines a <see cref="Command"/> that fires an <see cref="EventTopic"/> when
	''' it is executed
	''' </summary>
	Public Class EventTopicCommand : Inherits Command
		Private innerScope As PublicationScope = PublicationScope.Descendants
		Private innerWorkItem As WorkItem
		Private Const TopicNameFormat As String = "topic://EventTopicCommand/{0}"

		''' <summary>
		''' The <see cref="WorkItem"/> that contains this command
		''' </summary>
		<ServiceDependency()> _
		Public WriteOnly Property WorkItem() As WorkItem
			Set(ByVal value As WorkItem)
				innerWorkItem = Value
			End Set
		End Property

		''' <summary>
		''' This overrides executes the command and fires the <see cref="EventTopic"/>.
		''' </summary>
		''' <param name="sender">The sender for the <see cref="Command"/> handlers
		''' and the <see cref="EventTopic"/> subscriptions.</param>
		''' <param name="e">The <see cref="EventArgs"/> for the <see cref="Command"/> handlers and
		''' the <see cref="EventTopic"/> subscriptions.</param>
		Protected Overrides Sub OnExecuteAction(ByVal sender As Object, ByVal e As EventArgs)
			MyBase.OnExecuteAction(sender, e)
			Dim topic As EventTopic = innerWorkItem.EventTopics.Get(String.Format(CultureInfo.InstalledUICulture, TopicNameFormat, Name))
			If Not topic Is Nothing Then
				topic.Fire(sender, e, innerWorkItem, innerScope)
			End If
		End Sub

		''' <summary>
		''' The <see cref="PublicationScope"/> the <see cref="EventTopic"/> will be fired with.
		''' </summary>
		Public Property Scope() As PublicationScope
			Get
				Return innerScope
			End Get
			Set(ByVal value As PublicationScope)
				innerScope = value
			End Set
		End Property

	End Class

End Namespace
