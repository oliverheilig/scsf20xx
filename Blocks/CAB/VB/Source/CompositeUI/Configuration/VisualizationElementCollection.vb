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
Imports System.Configuration
Imports System.Collections.Generic

Namespace Configuration
	''' <summary>
	''' Contains the definition of a narrator.
	''' </summary>
	<ConfigurationCollection(GetType(VisualizationElement))> _
	Public Class VisualizationElementCollection
		Inherits ConfigurationElementCollection
		Implements IEnumerable(Of VisualizationElement)

		''' <summary>
		''' See <see cref="ConfigurationElementCollection.CreateNewElement()"/> for more information.
		''' </summary>
		Protected Overrides Function CreateNewElement() As ConfigurationElement
			Return New VisualizationElement()
		End Function

		''' <summary>
		''' See <see cref="ConfigurationElementCollection.GetElementKey"/> for more information.
		''' </summary>
		Protected Overrides Function GetElementKey(ByVal element As ConfigurationElement) As Object
			Return (CType(element, VisualizationElement)).Type
		End Function

		''' <summary>
		''' See <see cref="IEnumerable{T}.GetEnumerator"/> for more information.
		''' </summary>
		Public Shadows Function GetEnumerator() As IEnumerator(Of VisualizationElement) _
			Implements IEnumerable(Of VisualizationElement).GetEnumerator

			Dim count As Integer = MyBase.Count
			Dim baseList As List(Of VisualizationElement) = New List(Of VisualizationElement)

			Dim i As Integer = 0
			Do While i < count
				baseList.Add(CType(MyBase.BaseGet(i), VisualizationElement))
				i += 1
			Loop
			Return baseList.GetEnumerator()
		End Function

		''' <summary>
		''' Adds a new element to the collection.
		''' </summary>
		''' <param name="element">The element to be added.</param>
		Public Sub Add(ByVal element As VisualizationElement)
			MyBase.BaseAdd(element)
		End Sub

	End Class
End Namespace