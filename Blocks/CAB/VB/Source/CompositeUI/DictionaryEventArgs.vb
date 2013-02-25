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
Imports Microsoft.Practices.CompositeUI.Utility


''' <summary>
''' Provides data for an event that requires a dictionary of information.
''' </summary>
Public Class DictionaryEventArgs
	Inherits DataEventArgs(Of Dictionary(Of String, Object))

	''' <summary>
	''' Initializes a new instance of the <see cref="DictionaryEventArgs"/> class.
	''' </summary>
	Public Sub New()
		MyBase.New(New Dictionary(Of String, Object)())
	End Sub

	''' <summary>
	''' Provides a string representation of the argument data.
	''' </summary>
	Public Overrides Function ToString() As String
		Dim values As String() = New String(Data.Count - 1) {}
		Dim i As Integer = 0

		For Each pair As KeyValuePair(Of String, Object) In Data
			values(i) = pair.Key & ": " & pair.Value.ToString()
			i = i + 1
		Next

		Return String.Join(", ", values)
	End Function
End Class
