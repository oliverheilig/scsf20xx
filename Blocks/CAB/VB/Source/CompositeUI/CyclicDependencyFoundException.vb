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
Imports System.Runtime.Serialization


''' <summary>
''' Represents the exception that is thrown when there is a circular dependency
''' between modules during the module loading process.
''' </summary>
<Serializable()> _
Public Class CyclicDependencyFoundException : Inherits Exception
	''' <summary>
	''' Initializes a new instance of the <see cref="CyclicDependencyFoundException"/> class.
	''' </summary>
	Public Sub New()
		MyBase.New()
	End Sub

	''' <summary>
	''' Initializes a new instance of the <see cref="CyclicDependencyFoundException"/> class
	''' with the specified error message.
	''' </summary>
	''' <param name="message">The message that describes the error.</param>
	Public Sub New(ByVal message As String)
		MyBase.New(message)
	End Sub

	''' <summary>
	''' Initializes a new instance of the <see cref="CyclicDependencyFoundException"/> class
	''' with the specified error message and inner exception.
	''' </summary>
	''' <param name="message">The error message that explains the reason for the exception.</param>
	''' <param name="innerException">The exception that is the cause of the current exception.</param>
	Public Sub New(ByVal message As String, ByVal innerException As Exception)
		MyBase.New(message, innerException)
	End Sub

	''' <summary>
	''' Initializes a new instance of the <see cref="CyclicDependencyFoundException"/> class
	''' with the serialization data.
	''' </summary>
	''' <param name="info">Holds the serialized object data about the exception being thrown.</param>
	''' <param name="context">Contains contextual information about the source or destination.</param>
	Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
		MyBase.New(info, context)
	End Sub
End Class
