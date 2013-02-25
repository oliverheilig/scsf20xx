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

Imports Microsoft.VisualStudio.TestTools.UnitTesting








Imports System
Imports System.Collections.Generic
Imports Microsoft.Practices.CompositeUI.Utility


<TestClass()> _
Public Class ListDictionaryFixture
	Private Shared list As ListDictionary(Of String, Object)

	<TestInitialize()> _
	Public Sub SetUp()
		list = New ListDictionary(Of String, Object)()
	End Sub

	<ExpectedException(GetType(ArgumentNullException)), TestMethod()> _
	Public Sub AddThrowsIfKeyNull()
		list.Add(Nothing, New Object())
	End Sub

	<ExpectedException(GetType(ArgumentNullException)), TestMethod()> _
	Public Sub AddThrowsIfValueNull()
		list.Add("", Nothing)
	End Sub

	<TestMethod()> _
	Public Sub CanAddValue()
		Dim value1 As Object = New Object()
		Dim value2 As Object = New Object()

		list.Add("foo", value1)
		list.Add("foo", value2)

		Assert.AreEqual(2, list("foo").Count)
		Assert.AreSame(value1, list("foo")(0))
		Assert.AreSame(value2, list("foo")(1))
	End Sub

	<TestMethod()> _
	Public Sub CanIndexValuesByKey()
		list.Add("foo", New Object())
		list.Add("foo", New Object())

		Assert.AreEqual(2, list("foo").Count)
	End Sub

	<ExpectedException(GetType(ArgumentNullException)), TestMethod()> _
	Public Sub ThrowsIfRemoveKeyNull()
		list.Remove(Nothing, New Object())
	End Sub

	<TestMethod()> _
	Public Sub CanRemoveValue()
		Dim value As Object = New Object()

		list.Add("foo", value)
		list.Remove("foo", value)

		Assert.AreEqual(0, list("foo").Count)
	End Sub

	<TestMethod()> _
	Public Sub CanRemoveValueFromAllLists()
		Dim value As Object = New Object()
		list.Add("foo", value)
		list.Add("bar", value)

		list.Remove(value)

		Assert.AreEqual(0, list.Values.Count)
	End Sub

	<TestMethod()> _
	Public Sub RemoveNonExistingValueNoOp()
		list.Add("foo", New Object())

		list.Remove("foo", New Object())
	End Sub

	<TestMethod()> _
	Public Sub RemoveNonExistingKeyNoOp()
		list.Remove("foo", New Object())
	End Sub

	<ExpectedException(GetType(ArgumentNullException)), TestMethod()> _
	Public Sub ThrowsIfRemoveListKeyNull()
		list.Remove(CType(Nothing, String))
	End Sub

	<TestMethod()> _
	Public Sub CanRemoveList()
		list.Add("foo", New Object())
		list.Add("foo", New Object())

		Dim removed As Boolean = list.Remove("foo")

		Assert.IsTrue(removed)
		Assert.AreEqual(0, list.Keys.Count)
	End Sub

	<TestMethod()> _
	Public Sub CanSetList()
		Dim values As List(Of Object) = New List(Of Object)()
		values.Add(New Object())
		list.Add("foo", New Object())
		list.Add("foo", New Object())

		list("foo") = values

		Assert.AreEqual(1, list("foo").Count)
	End Sub

	<TestMethod()> _
	Public Sub CanEnumerateKeyValueList()
		Dim count As Integer = 0
		list.Add("foo", New Object())
		list.Add("foo", New Object())

		For Each pair As KeyValuePair(Of String, List(Of Object)) In list
			For Each value As Object In pair.Value
				count += 1
			Next value
			Assert.AreEqual("foo", pair.Key)
		Next

		Assert.AreEqual(2, count)
	End Sub

	<TestMethod()> _
	Public Sub CanGetFlatListOfValues()
		list.Add("foo", New Object())
		list.Add("foo", New Object())
		list.Add("bar", New Object())

		Dim values As List(Of Object) = list.Values

		Assert.AreEqual(3, values.Count)
	End Sub

	<TestMethod()> _
	Public Sub IndexerAccessAlwaysSucceeds()
		Dim values As List(Of Object) = list("foo")

		Assert.IsNotNull(values)
	End Sub


	<ExpectedException(GetType(ArgumentNullException)), TestMethod()> _
	Public Sub ThrowsIfContainsKeyNull()
		list.ContainsKey(Nothing)
	End Sub

	<TestMethod()> _
	Public Sub CanAskContainsKey()
		Assert.IsFalse(list.ContainsKey("foo"))
	End Sub

	<TestMethod()> _
	Public Sub CanAskContainsValueInAnyList()
		Dim obj As Object = New Object()
		list.Add("foo", New Object())
		list.Add("bar", New Object())
		list.Add("baz", obj)

		Dim contains As Boolean = list.ContainsValue(obj)

		Assert.IsTrue(contains)
	End Sub

	<TestMethod()> _
	Public Sub CanClearDictionary()
		list.Add("foo", New Object())
		list.Add("bar", New Object())
		list.Add("baz", New Object())

		list.Clear()

		Assert.AreEqual(0, list.Count)
	End Sub

#Region "Utility code for CanGetFilteredValuesByKeys"

	Private Function KeyFilter(ByVal key As String) As Boolean
		Return key.StartsWith("b")
	End Function

#End Region

	<TestMethod()> _
	Public Sub CanGetFilteredValuesByKeys()
		list.Add("foo", New Object())
		list.Add("bar", New Object())
		list.Add("baz", New Object())

		Dim filtered As IEnumerable(Of Object) = list.FindAllValuesByKey(AddressOf KeyFilter)

		Dim count As Integer = 0
		For Each obj As Object In filtered
			count += 1
		Next obj

		Assert.AreEqual(2, count)
	End Sub

#Region "Utility code for CanGetFilteredValues"

	Private Function ValueFilter(ByVal value As Object) As Boolean
		Return TypeOf value Is DateTime
	End Function

#End Region

	<TestMethod()> _
	Public Sub CanGetFilteredValues()
		list.Add("foo", DateTime.Now)
		list.Add("bar", New Object())
		list.Add("baz", DateTime.Today)

		Dim filtered As IEnumerable(Of Object) = list.FindAllValues(AddressOf ValueFilter)
		Dim count As Integer = 0
		For Each obj As Object In filtered
			count += 1
		Next obj

		Assert.AreEqual(2, count)
	End Sub
End Class
