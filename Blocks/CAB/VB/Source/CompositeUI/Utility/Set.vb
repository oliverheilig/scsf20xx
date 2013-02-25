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

Namespace Utility
	''' <summary>
	''' Represents a set of items. Adding an item to a set twice will only yield one instance
	''' in the collection.
	''' </summary>
	''' <typeparam name="TItem">The type of items to be stored in the <see cref="[Set](Of TItem)"/>.</typeparam>
	Public Class [Set](Of TItem)
		Implements ICollection(Of TItem)

		Private innerIsReadOnly As Boolean
		Private dictionary As Dictionary(Of TItem, Object)

		''' <summary>
		''' 
		''' </summary>
		Public Sub New()
			dictionary = New Dictionary(Of TItem, Object)()
			innerIsReadOnly = False
		End Sub

		Private Sub New(ByVal innerSet As [Set](Of TItem))
			dictionary = innerSet.dictionary
			innerIsReadOnly = True
		End Sub

		''' <summary>
		''' See <see cref="ICollection(Of TItem).Add"/> for more information.
		''' </summary>
		Public Sub Add(ByVal item As TItem) Implements ICollection(Of TItem).Add
			If IsReadOnly Then
				Throw New NotSupportedException()
			End If

			dictionary(item) = String.Empty
		End Sub

		''' <summary>
		''' Returns a read-only wrapper around the <see cref="[Set](Of TItem)"/>.
		''' </summary>
		Public Function AsReadOnly() As [Set](Of TItem)
			Return New [Set](Of TItem)(Me)
		End Function

		''' <summary>
		''' See <see cref="ICollection(Of TItem).Clear"/> for more information.
		''' </summary>
		Public Sub Clear() Implements ICollection(Of TItem).Clear
			If IsReadOnly Then
				Throw New NotSupportedException()
			End If

			dictionary.Clear()
		End Sub

		''' <summary>
		''' See <see cref="ICollection(Of TItem).Contains"/> for more information.
		''' </summary>
		Public Function Contains(ByVal item As TItem) As Boolean Implements ICollection(Of TItem).Contains
			Return dictionary.ContainsKey(item)
		End Function

		''' <summary>
		''' See <see cref="ICollection(Of TItem).CopyTo"/> for more information.
		''' </summary>
		Public Sub CopyTo(ByVal array As TItem(), ByVal arrayIndex As Integer) Implements ICollection(Of TItem).CopyTo
			Throw New NotImplementedException()
		End Sub

		''' <summary>
		''' See <see cref="ICollection(Of TItem).Count"/> for more information.
		''' </summary>
		Public ReadOnly Property Count() As Integer Implements ICollection(Of TItem).Count
			Get
				Return dictionary.Count
			End Get
		End Property

		''' <summary>
		''' See <see cref="ICollection(Of TItem).IsReadOnly"/> for more information.
		''' </summary>
		Public ReadOnly Property IsReadOnly() As Boolean Implements ICollection(Of TItem).IsReadOnly
			Get
				Return innerIsReadOnly
			End Get
		End Property

		''' <summary>
		''' See <see cref="ICollection(Of TItem).Remove"/> for more information.
		''' </summary>
		Public Function Remove(ByVal item As TItem) As Boolean Implements ICollection(Of TItem).Remove
			If IsReadOnly Then
				Throw New NotSupportedException()
			End If

			Return dictionary.Remove(item)
		End Function

		''' <summary>
		''' See <see cref="IEnumerable(Of TItem).GetEnumerator"/> for more information.
		''' </summary>
		Public Function GetEnumerator() As IEnumerator(Of TItem) Implements IEnumerable(Of TItem).GetEnumerator
			Return dictionary.Keys.GetEnumerator()
		End Function

		''' <summary>
		''' See <see cref="IEnumerable(Of TItem).GetEnumerator"/> for more information.
		''' </summary>
		Private Function GetEnumeratorBase() As IEnumerator Implements IEnumerable.GetEnumerator
			Return GetEnumerator()
		End Function

	End Class

End Namespace
