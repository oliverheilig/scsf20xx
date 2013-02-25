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
Imports System.Collections
Imports System.Collections.Generic
Imports Microsoft.Practices.CompositeUI.UIElements

''' <summary>
''' Represents an extension site for UI elements.
''' </summary>
Public Class UIExtensionSite
	Implements IEnumerable(Of Object)

	Private adapter As IUIElementAdapter
	Private items As List(Of Object) = New List(Of Object)()

	''' <summary>
	''' Initializes a new instance of the <see cref="UIExtensionSite"/> class with the provided
	''' adapter.
	''' </summary>
	Public Sub New(ByVal adapter As IUIElementAdapter)
		Me.adapter = adapter
	End Sub

	''' <summary>
	''' Returns the number of items added to the site.
	''' </summary>
	Public ReadOnly Property Count() As Integer
		Get
			Return items.Count
		End Get
	End Property

	''' <summary>
	''' Adds an element to the site.
	''' </summary>
	''' <typeparam name="TElement">The type of the element to be added.</typeparam>
	''' <param name="uiElement">The element to be added.</param>
	''' <returns>The added element. The adapter may return a different element than was
	''' passed; in this case, the returned element is the new element provided by the
	''' adapter.</returns>
	Public Function Add(Of TElement)(ByVal uiElement As TElement) As TElement
		Dim element As TElement = CType(adapter.Add(uiElement), TElement)
		items.Add(element)
		Return element
	End Function

	''' <summary>
	''' Removes all items from the site, and removes them from the UI.
	''' </summary>
	Public Sub Clear()
		For Each obj As Object In items
			adapter.remove(obj)
		Next obj

		items.Clear()
	End Sub

	''' <summary>
	''' Determines if the site contains a UI element.
	''' </summary>
	''' <param name="uiElement">The element to find.</param>
	''' <returns>true if the element is present; false otherwise.</returns>
	Public Function Contains(ByVal uiElement As Object) As Boolean
		Return items.Contains(uiElement)
	End Function

	''' <summary>
	''' Removes an item from the site, and removes it from the UI.
	''' </summary>
	''' <param name="uiElement">The element to be removed.</param>
	Public Sub Remove(ByVal uiElement As Object)
		If items.Contains(uiElement) Then
			adapter.remove(uiElement)
			items.Remove(uiElement)
		End If
	End Sub

	Friend Function Duplicate() As UIExtensionSite
		Return New UIExtensionSite(Me.adapter)
	End Function

	Private Function GetEnumerator() As IEnumerator(Of Object) Implements IEnumerable(Of Object).GetEnumerator
		Return items.GetEnumerator()
	End Function

	Private Function GetEnumeratorBase() As IEnumerator Implements IEnumerable.GetEnumerator
		Return items.GetEnumerator()
	End Function
End Class
