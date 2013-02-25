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

Namespace Utility
	''' <summary>
	''' Generic arguments class to pass to event handlers that need to receive data.
	''' </summary>
	''' <typeparam name="TData">The type of data to pass.</typeparam>
	Public Class DataEventArgs(Of TData)
		Inherits EventArgs

		Private innerData As TData

		''' <summary>
		''' Initializes the DataEventArgs class.
		''' </summary>
		''' <param name="data">Information related to the event.</param>
		''' <exception cref="ArgumentNullException">The data is null.</exception>
		Public Sub New(ByVal data As TData)
			If data Is Nothing Then
				Throw New ArgumentNullException("data")
			End If
			Me.innerData = data
		End Sub

		''' <summary>
		''' Gets the information related to the event.
		''' </summary>
		Public ReadOnly Property Data() As TData
			Get
				Return innerData
			End Get
		End Property

		''' <summary>
		''' Provides a string representation of the argument data.
		''' </summary>
		Public Overrides Function ToString() As String
			Return innerData.ToString()
		End Function
	End Class
End Namespace
