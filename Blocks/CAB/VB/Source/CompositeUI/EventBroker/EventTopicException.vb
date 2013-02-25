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
Imports System.Collections.ObjectModel
Imports System.Runtime.Serialization
Imports System.Globalization

Namespace EventBroker
	''' <summary>
	''' An <see cref="Exception"/> thrown by the <see cref="EventTopic"/> when exceptions occurs
	''' on its subscriptions during a firing sequence.
	''' </summary>
	<Serializable()> _
	Public Class EventTopicException : Inherits Exception
		Private innerExceptions As ReadOnlyCollection(Of Exception)

		Private innerTopic As EventTopic

		''' <summary>
		''' Initializes a new instance of the <see cref="EventTopicException"/> class.
		''' </summary>
		Public Sub New()
			MyBase.New()
		End Sub

		''' <summary>
		''' Initializes a new instance with a specified error message.
		''' </summary>
		''' <param name="message">The message that describes the error.</param>
		Public Sub New(ByVal message As String)
			MyBase.New(message)
		End Sub

		''' <summary>
		''' Initializes a new instance with a specified error message 
		''' and a reference to the inner exception that is the cause of this exception.
		''' </summary>
		''' <param name="message">The error message that explains the reason for the exception.</param>
		''' <param name="innerException">The exception that is the cause of the current exception, 
		''' or a null reference if no inner exception is specified.</param>
		Public Sub New(ByVal message As String, ByVal innerException As Exception)
			MyBase.New(message, innerException)
		End Sub

		''' <summary>
		''' Initializes a new instance with serialized data.
		''' </summary>
		''' <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
		''' <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
		Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
			MyBase.New(info, context)
		End Sub

		''' <summary>
		''' Initializes a new instance with the specified list of exceptions.
		''' </summary>
		''' <param name="exceptions">The list of exceptions that ocurred during the subscribers invocation.</param>
		''' <param name="topic">The <see cref="EventTopic"/> instance whose subscribers incurred into an exception.</param>
		Public Sub New(ByVal topic As EventTopic, ByVal exceptions As ReadOnlyCollection(Of Exception))
			MyBase.New(String.Format(CultureInfo.CurrentCulture, My.Resources.EventTopicFireException, topic.Name))
			Me.innerTopic = topic
			Me.innerExceptions = exceptions
		End Sub

		''' <summary>
		''' Gets the list of exceptions that ocurred during the subscribers invocation.
		''' </summary>
		Public ReadOnly Property Exceptions() As ReadOnlyCollection(Of Exception)
			Get
				Return innerExceptions
			End Get
		End Property

		''' <summary>
		''' Gets the <see cref="EventTopic"/> which incurred into errors.
		''' </summary>
		Public ReadOnly Property Topic() As EventTopic
			Get
				Return innerTopic
			End Get
		End Property
	End Class
End Namespace