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
	''' A dictionary of lists.
	''' </summary>
	''' <typeparam name="TKey">The key to use for lists.</typeparam>
	''' <typeparam name="TValue">The type of the value held by lists.</typeparam>
	Public Class ListDictionary(Of TKey, TValue)
		Implements IDictionary(Of TKey, List(Of TValue))

		Private innerValues As Dictionary(Of TKey, List(Of TValue)) = New Dictionary(Of TKey, List(Of TValue))()

#Region "Public Methods"

		''' <summary>
		''' If a list does not already exist, it will be created automatically.
		''' </summary>
		''' <param name="key">The key of the list that will hold the value.</param>
		Public Sub Add(ByVal key As TKey)
			Guard.ArgumentNotNull(key, "key")

			CreateNewList(key)
		End Sub

		''' <summary>
		''' Adds a value to a list with the given key. If a list does not already exist, 
		''' it will be created automatically.
		''' </summary>
		''' <param name="key">The key of the list that will hold the value.</param>
		''' <param name="value">The value to add to the list under the given key.</param>
		Public Sub Add(ByVal key As TKey, ByVal value As TValue)
			Guard.ArgumentNotNull(key, "key")
			Guard.ArgumentNotNull(value, "value")

			If innerValues.ContainsKey(key) Then
				innerValues(key).Add(value)
			Else
				Dim values As List(Of TValue) = CreateNewList(key)
				values.Add(value)
			End If
		End Sub

		Private Function CreateNewList(ByVal key As TKey) As List(Of TValue)
			Dim values As List(Of TValue) = New List(Of TValue)()
			innerValues.Add(key, values)

			Return values
		End Function

		''' <summary>
		''' Removes all entries in the dictionary.
		''' </summary>
		Public Sub Clear() Implements ICollection(Of KeyValuePair(Of TKey, List(Of TValue))).Clear
			innerValues.Clear()
		End Sub

		''' <summary>
		''' Determines whether the dictionary contains the specified value.
		''' </summary>
		''' <param name="value">The value to locate.</param>
		''' <returns>true if the dictionary contains the value in any list; otherwise, false.</returns>
		Public Function ContainsValue(ByVal value As TValue) As Boolean
			For Each pair As KeyValuePair(Of TKey, List(Of TValue)) In innerValues
				If pair.Value.Contains(value) Then
					Return True
				End If
			Next

			Return False
		End Function

		''' <summary>
		''' Determines whether the dictionary contains the given key.
		''' </summary>
		''' <param name="key">The key to locate.</param>
		''' <returns>true if the dictionary contains the given key; otherwise, false.</returns>
		Public Function ContainsKey(ByVal key As TKey) As Boolean Implements IDictionary(Of TKey, System.Collections.Generic.List(Of TValue)).ContainsKey
			Guard.ArgumentNotNull(key, "key")
			Return innerValues.ContainsKey(key)
		End Function

#Region "Utility class for FindValuesByKey"

		Private Class FindValuesByKeyEnumerable
			Implements IEnumerable(Of TValue)

			Private baseDictionary As ListDictionary(Of TKey, TValue)
			Private filter As Predicate(Of TKey)

			Public Sub New(ByVal aDictionary As ListDictionary(Of TKey, TValue), _
				ByVal valueFilter As Predicate(Of TKey))
				baseDictionary = aDictionary
				filter = valueFilter
			End Sub

			Public Function GetEnumerator() As IEnumerator(Of TValue) Implements IEnumerable(Of TValue).GetEnumerator
				Dim sortedList As List(Of TValue) = New List(Of TValue)()
				For Each pair As KeyValuePair(Of TKey, List(Of TValue)) In baseDictionary
					If (filter(pair.Key)) Then
						For Each value As TValue In pair.Value
							sortedList.Add(value)
						Next
					End If
				Next
				Return sortedList.GetEnumerator()
			End Function

			Public Function GetEnumeratorBase() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				Return GetEnumerator()
			End Function
		End Class

#End Region

		''' <summary>
		''' Retrieves the all the elements from the list which have a key that matches the condition 
		''' defined by the specified predicate.
		''' </summary>
		''' <param name="keyFilter">The filter with the condition to use to filter lists by their key.</param>
		''' <returns></returns>
		Public Function FindAllValuesByKey(ByVal keyFilter As Predicate(Of TKey)) As IEnumerable(Of TValue)
			Return New FindValuesByKeyEnumerable(Me, keyFilter)
		End Function

#Region "Utility class for FindAllValues"

		Public Class FindAllValuesEnumerable
			Implements IEnumerable(Of TValue)

			Private baseDictionary As ListDictionary(Of TKey, TValue)
			Private filter As Predicate(Of TValue)

			Public Sub New(ByVal aDictionary As ListDictionary(Of TKey, TValue), _
				ByVal valueFilter As Predicate(Of TValue))
				baseDictionary = aDictionary
				filter = valueFilter
			End Sub

			Public Function GetEnumerator() As IEnumerator(Of TValue) Implements IEnumerable(Of TValue).GetEnumerator
				Dim sortedList As List(Of TValue) = New List(Of TValue)()
				For Each pair As KeyValuePair(Of TKey, List(Of TValue)) In baseDictionary
					For Each value As TValue In pair.Value
						If (filter(value)) Then
							sortedList.Add(value)
						End If
					Next
				Next
				Return sortedList.GetEnumerator()
			End Function

			Public Function GetEnumeratorBase() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				Return GetEnumerator()
			End Function
		End Class

#End Region

		''' <summary>
		''' Retrieves all the elements that match the condition defined by the specified predicate.
		''' defined by the specified predicate.
		''' </summary>
		''' <param name="valueFilter">The filter with the condition to use to filter values.</param>
		''' <returns></returns>
		Public Function FindAllValues(ByVal valueFilter As Predicate(Of TValue)) As IEnumerable(Of TValue)
			Return New FindAllValuesEnumerable(Me, valueFilter)
		End Function

		''' <summary>
		''' Removes a list by key.
		''' </summary>
		''' <param name="key">The key of the list to remove.</param>
		''' <returns></returns>
		Public Function Remove(ByVal key As TKey) As Boolean Implements IDictionary(Of TKey, System.Collections.Generic.List(Of TValue)).Remove
			Guard.ArgumentNotNull(key, "key")
			Return innerValues.Remove(key)
		End Function

#Region "Utility inner class for Remove method"

		Private Class RemoveUtilityProvider
			Private valueField As TValue
			Public Property Value() As TValue
				Get
					Return valueField
				End Get
				Set(ByVal valueArgument As TValue)
					valueField = valueArgument
				End Set
			End Property

			Public Function Matches(ByVal item As TValue) As Boolean
				Return valueField.Equals(item)
			End Function
		End Class

		Private removeUtility As RemoveUtilityProvider = New RemoveUtilityProvider

#End Region

		''' <summary>
		''' Removes a value from the list with the given key.
		''' </summary>
		''' <param name="key">The key of the list where the value exists.</param>
		''' <param name="value">The value to remove.</param>
		Public Sub Remove(ByVal key As TKey, ByVal value As TValue)
			Guard.ArgumentNotNull(key, "key")
			Guard.ArgumentNotNull(value, "value")

			If innerValues.ContainsKey(key) Then
				removeUtility.Value = value
				innerValues(key).RemoveAll(AddressOf removeUtility.Matches)
			End If
		End Sub

		''' <summary>
		''' Removes a value from all lists where it may be found.
		''' </summary>
		''' <param name="value">The value to remove.</param>
		Public Sub Remove(ByVal value As TValue)
			For Each pair As KeyValuePair(Of TKey, List(Of TValue)) In innerValues
				Remove(pair.Key, value)
			Next
		End Sub

#End Region

#Region "Properties"

		''' <summary>
		''' Gets a shallow copy of all values in all lists.
		''' </summary>
		Public ReadOnly Property Values() As List(Of TValue)
			Get
				Dim newValues As List(Of TValue) = New List(Of TValue)()
				For Each list As IEnumerable(Of TValue) In innerValues.Values
					newValues.AddRange(list)
				Next
				Return newValues
			End Get
		End Property

		''' <summary>
		''' Gets the list of keys in the dictionary.
		''' </summary>
		Public ReadOnly Property Keys() As ICollection(Of TKey) Implements IDictionary(Of TKey, System.Collections.Generic.List(Of TValue)).Keys
			Get
				Return innerValues.Keys
			End Get
		End Property

		''' <summary>
		''' Gets or sets the list associated with the given key. The 
		''' access always succeeds, eventually returning an empty list.
		''' </summary>
		''' <param name="key">The key of the list to access.</param>
		''' <returns>The list associated with the key.</returns>
		Default Public Property Item(ByVal key As TKey) As List(Of TValue) Implements IDictionary(Of TKey, System.Collections.Generic.List(Of TValue)).Item
			Get
				If innerValues.ContainsKey(key) = False Then
					innerValues.Add(key, New List(Of TValue)())
				End If
				Return innerValues(key)
			End Get
			Set(ByVal value As List(Of TValue))
				innerValues(key) = value
			End Set
		End Property

		''' <summary>
		''' Gets the number of lists in the dictionary.
		''' </summary>
		Public ReadOnly Property Count() As Integer Implements IDictionary(Of TKey, System.Collections.Generic.List(Of TValue)).Count
			Get
				Return innerValues.Count
			End Get
		End Property

#End Region

#Region "IDictionary<TKey,List<TValue>> Members"

		''' <summary>
		''' See <see cref="IDictionary(Of TKey,TValue).Add"/> for more information.
		''' </summary>
		Sub Add(ByVal key As TKey, ByVal value As List(Of TValue)) Implements IDictionary(Of TKey, List(Of TValue)).Add
			Guard.ArgumentNotNull(key, "key")
			Guard.ArgumentNotNull(value, "value")
			innerValues.Add(key, value)
		End Sub

		''' <summary>
		''' See <see cref="IDictionary(Of TKey, TValue).TryGetValue"/> for more information.
		''' </summary>
		Function TryGetValue(ByVal key As TKey, ByRef value As List(Of TValue)) As Boolean Implements IDictionary(Of TKey, List(Of TValue)).TryGetValue
			value = Me(key)
			Return True
		End Function

		''' <summary>
		''' See <see cref="IDictionary(Of TKey, TValue).Values"/> for more information.
		''' </summary>
		ReadOnly Property ValuesBase() As ICollection(Of List(Of TValue)) Implements IDictionary(Of TKey, List(Of TValue)).Values
			Get
				Return innerValues.Values
			End Get
		End Property

#End Region

#Region "ICollection<KeyValuePair<TKey,List<TValue>>> Members"

		''' <summary>
		''' See <see cref="ICollection(Of TValue).Add"/> for more information.
		''' </summary>
		Sub Add(ByVal item As KeyValuePair(Of TKey, List(Of TValue))) Implements ICollection(Of KeyValuePair(Of TKey, List(Of TValue))).Add
			Dim innerCollection As ICollection(Of KeyValuePair(Of TKey, List(Of TValue))) = innerValues
			innerCollection.Add(item)
		End Sub

		''' <summary>
		''' See <see cref="ICollection(Of TValue).Contains"/> for more information.
		''' </summary>
		Function Contains(ByVal item As KeyValuePair(Of TKey, List(Of TValue))) As Boolean Implements ICollection(Of KeyValuePair(Of TKey, List(Of TValue))).Contains
			Dim innerCollection As ICollection(Of KeyValuePair(Of TKey, List(Of TValue))) = innerValues
			Return innerCollection.Contains(item)
		End Function

		''' <summary>
		''' See <see cref="ICollection{TValue}.CopyTo"/> for more information.
		''' </summary>
		Sub CopyTo(ByVal array As KeyValuePair(Of TKey, List(Of TValue))(), ByVal arrayIndex As Integer) Implements ICollection(Of KeyValuePair(Of TKey, List(Of TValue))).CopyTo
			Dim innerCollection As ICollection(Of KeyValuePair(Of TKey, List(Of TValue))) = innerValues
			innerCollection.CopyTo(array, arrayIndex)
		End Sub

		''' <summary>
		''' See <see cref="ICollection{TValue}.IsReadOnly"/> for more information.
		''' </summary>
		ReadOnly Property IsReadOnly() As Boolean Implements ICollection(Of KeyValuePair(Of TKey, List(Of TValue))).IsReadOnly
			Get
				Dim innerCollection As ICollection(Of KeyValuePair(Of TKey, List(Of TValue))) = innerValues
				Return innerCollection.IsReadOnly
			End Get
		End Property

		''' <summary>
		''' See <see cref="ICollection{TValue}.Remove"/> for more information.
		''' </summary>
		Function Remove(ByVal item As KeyValuePair(Of TKey, List(Of TValue))) As Boolean Implements ICollection(Of KeyValuePair(Of TKey, List(Of TValue))).Remove
			Dim innerCollection As ICollection(Of KeyValuePair(Of TKey, List(Of TValue))) = innerValues
			Return innerCollection.Remove(item)
		End Function

#End Region

#Region "IEnumerable<KeyValuePair<TKey,List<TValue>>> Members"

		''' <summary>
		''' See <see cref="IEnumerable{TValue}.GetEnumerator"/> for more information.
		''' </summary>
		Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of System.Collections.Generic.KeyValuePair(Of TKey, System.Collections.Generic.List(Of TValue))) Implements System.Collections.Generic.IEnumerable(Of System.Collections.Generic.KeyValuePair(Of TKey, System.Collections.Generic.List(Of TValue))).GetEnumerator
			Return innerValues.GetEnumerator()
		End Function

#End Region

#Region "IEnumerable Members"

		''' <summary>
		''' See <see cref="System.Collections.IEnumerable.GetEnumerator"/> for more information.
		''' </summary>
		Private Function GetEnumeratorBase() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
			Return innerValues.GetEnumerator()
		End Function

#End Region

	End Class

End Namespace
