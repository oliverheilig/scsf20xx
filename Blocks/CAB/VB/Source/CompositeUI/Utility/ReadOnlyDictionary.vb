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
Imports System.Collections

Namespace Utility
	''' <summary>
	''' Provides a read-only implementation of <see cref="IDictionary{TKey,TValue}"/>.
	''' </summary>
	''' <typeparam name="TKey"></typeparam>
	''' <typeparam name="TValue"></typeparam>
	Public Class ReadOnlyDictionary(Of TKey, TValue)
		Implements IDictionary(Of TKey, TValue)

		Private inner As IDictionary(Of TKey, TValue)

		''' <summary>
		''' Initializes a new instance of the <see cref="ReadOnlyDictionary(Of TKey, TValue)"/>
		''' class using the specified dictionary as the base.
		''' </summary>
		''' <param name="innerDictionary"></param>
		Public Sub New(ByVal innerDictionary As IDictionary(Of TKey, TValue))
			Guard.ArgumentNotNull(innerDictionary, "innerDictionary")
			inner = innerDictionary
		End Sub

#Region "IDictionary<TKey,TValue> Members"

		''' <summary>
		''' This method is not implemented.
		''' </summary>
		''' <param name="key"></param>
		''' <param name="value"></param>
		Private Sub Add(ByVal key As TKey, ByVal value As TValue) Implements IDictionary(Of TKey, TValue).Add
			Throw New NotSupportedException(My.Resources.DictionaryIsReadOnly)
		End Sub

		''' <summary>
		''' Determines whether the ReadOnlyDictionary{TKey,TValue} contains a specific key.
		''' </summary>
		''' <param name="key"></param>
		''' <returns></returns>
		Public Function ContainsKey(ByVal key As TKey) As Boolean Implements IDictionary(Of TKey, TValue).ContainsKey
			Return inner.ContainsKey(key)
		End Function

		''' <summary>
		''' Gets an <see cref="ICollection(Of TKey)"/> containing the keys of <see cref="IDictionary(Of TKey, TValue)"/>.
		''' </summary>
		Public ReadOnly Property Keys() As ICollection(Of TKey) Implements IDictionary(Of TKey, TValue).Keys
			Get
				Return inner.Keys
			End Get
		End Property

		''' <summary>
		''' This method is not implemented.
		''' </summary>
		''' <param name="key"></param>
		''' <returns></returns>
		Private Function Remove(ByVal key As TKey) As Boolean Implements IDictionary(Of TKey, TValue).Remove
			Throw New NotSupportedException(My.Resources.DictionaryIsReadOnly)
		End Function

		''' <summary>
		''' Gets the value associated with the specified key.
		''' </summary>
		''' <param name="key"></param>
		''' <param name="value"></param>
		''' <returns></returns>
		Public Function TryGetValue(ByVal key As TKey, ByRef value As TValue) As Boolean Implements IDictionary(Of TKey, TValue).TryGetValue
			Return inner.TryGetValue(key, value)
		End Function

		''' <summary>
		''' Gets an <see cref="ICollection(Of TValue)"/> containing the values in the 
		''' <see cref="IDictionary(Of TKey, TValue)"/>.
		''' </summary>
		Public ReadOnly Property Values() As ICollection(Of TValue) Implements IDictionary(Of TKey, TValue).Values
			Get
				Return inner.Values
			End Get
		End Property

		''' <summary>
		''' Gets the value associated with the specified key
		''' </summary>
		''' <param name="key"></param>
		''' <returns></returns>
		Default Public Property Item(ByVal key As TKey) As TValue Implements IDictionary(Of TKey, TValue).Item
			Get
				Return inner(key)
			End Get
			Set(ByVal value As TValue)
				Throw New NotSupportedException(My.Resources.DictionaryIsReadOnly)
			End Set
		End Property

#End Region

#Region "ICollection<KeyValuePair<TKey,TValue>> Members"

		''' <summary>
		''' See <see cref="ICollection{TValue}.Add"/> for more information.
		''' </summary>
		Private Sub Add(ByVal item As KeyValuePair(Of TKey, TValue)) Implements ICollection(Of KeyValuePair(Of TKey, TValue)).Add
			Throw New NotSupportedException(My.Resources.DictionaryIsReadOnly)
		End Sub

		''' <summary>
		''' See <see cref="ICollection{TValue}.Clear"/> for more information.
		''' </summary>
		Private Sub Clear() Implements ICollection(Of KeyValuePair(Of TKey, TValue)).Clear
			Throw New NotSupportedException(My.Resources.DictionaryIsReadOnly)
		End Sub

		''' <summary>
		''' See <see cref="ICollection{TValue}.Contains"/> for more information.
		''' </summary>
		Private Function Contains(ByVal item As KeyValuePair(Of TKey, TValue)) As Boolean Implements ICollection(Of KeyValuePair(Of TKey, TValue)).Contains
			Dim innerDictionary As IDictionary(Of TKey, TValue) = inner
			Return inner.Contains(item)
		End Function

		''' <summary>
		''' See <see cref="ICollection{TValue}.CopyTo"/> for more information.
		''' </summary>
		Private Sub CopyTo(ByVal array() As KeyValuePair(Of TKey, TValue), ByVal arrayIndex As Integer) Implements ICollection(Of KeyValuePair(Of TKey, TValue)).CopyTo
			Dim innerDictionary As IDictionary(Of TKey, TValue) = inner
			innerDictionary.CopyTo(array, arrayIndex)
		End Sub

		''' <summary>
		''' Returns the number of elements in the <see cref="ReadOnlyDictionary{TKey,TValue}"/>
		''' </summary>
		Public ReadOnly Property Count() As Integer Implements ICollection(Of KeyValuePair(Of TKey, TValue)).Count
			Get
				Return inner.Count
			End Get
		End Property

		''' <summary>
		''' See <see cref="ICollection{TValue}.IsReadOnly"/> for more information.
		''' </summary>
		ReadOnly Property IsReadOnly() As Boolean Implements ICollection(Of KeyValuePair(Of TKey, TValue)).IsReadOnly
			Get
				Return True
			End Get
		End Property

		''' <summary>
		''' See <see cref="ICollection{TValue}.Remove"/> for more information.
		''' </summary>
		Private Function Remove(ByVal item As KeyValuePair(Of TKey, TValue)) As Boolean Implements ICollection(Of KeyValuePair(Of TKey, TValue)).Remove
			Throw New NotSupportedException(My.Resources.DictionaryIsReadOnly)
		End Function

#End Region

#Region "IEnumerable<KeyValuePair<TKey,TValue>> Members"

		''' <summary>
		''' See <see cref="IEnumerable{TValue}.GetEnumerator"/> for more information.
		''' </summary>
		Public Function GetEnumerator() As IEnumerator(Of KeyValuePair(Of TKey, TValue)) Implements System.Collections.Generic.IEnumerable(Of KeyValuePair(Of TKey, TValue)).GetEnumerator
			Return inner.GetEnumerator()
		End Function

#End Region

#Region "IEnumerable Members"

		''' <summary>
		''' See <see cref="IEnumerable.GetEnumerator"/> for more information.
		''' </summary>
		Public Function GetEnumeratorBase() As IEnumerator Implements IEnumerable.GetEnumerator
			Return (CType(inner, IEnumerable)).GetEnumerator()
		End Function

#End Region

	End Class

End Namespace
