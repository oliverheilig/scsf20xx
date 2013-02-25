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
Imports System.Windows.Forms

Imports Microsoft.Practices.CompositeUI.UIElements
Imports System.Collections.Generic

Namespace Microsoft.Practices.CompositeUI.Tests.UIElements
	<TestClass()> _
	Public Class UIElementAdapterFixture
		Private Shared adapter As MockUIElementAdapter

		<TestInitialize()> _
		Public Sub Setup()
			adapter = New MockUIElementAdapter()
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub AddingWithNullElementThorws()
			adapter.Add(Nothing)
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub RemovingWithNullElementThrows()
			adapter.Remove(Nothing)
		End Sub

		<TestMethod()> _
		Public Sub AddCallsTypedAdd()
			adapter.Add("Test")

			Assert.IsTrue(adapter.AddCalled)
		End Sub

		<TestMethod()> _
		Public Sub RemoveCallsTypedRemove()
			adapter.Remove("Test")

			Assert.IsTrue(adapter.RemoveCalled)
		End Sub

		<TestMethod()> _
		Public Sub AddReturnTypedElement()
			Dim item As String = "Test"
			Dim returnItem As Object = adapter.Add(item)

			Assert.AreEqual(item, returnItem)
			Assert.AreEqual(GetType(String), item.GetType())
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentException))> _
		Public Sub AddThrowsIfElementNotAssignable()
			adapter.Add(25)
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentException))> _
		Public Sub RemoveThrowsIfElementNotAssignable()
			adapter.Remove(25)
		End Sub

		<TestMethod()> _
		Public Sub AddingItemShowsInList()
			Dim item As String = "Test"
			Dim returnItem As Object = adapter.Add(item)

			Assert.AreEqual(1, adapter.Strings.Count)
			Assert.AreSame(item, returnItem)
		End Sub

		<TestMethod()> _
		Public Sub RemovingItemRemovesFromList()
			Dim item As String = "Test"
			adapter.Add(item)

			adapter.Remove(item)

			Assert.AreEqual(0, adapter.Strings.Count)
		End Sub

		Private Class MockUIElementAdapter : Inherits UIElementAdapter(Of String)
			Public AddCalled As Boolean = False
			Public RemoveCalled As Boolean = False
			Public Strings As List(Of String) = New List(Of String)()

			Protected Overrides Function Add(ByVal uiElement As String) As String
				AddCalled = True
				Strings.Add(uiElement)

				Return uiElement
			End Function

			Protected Overrides Sub Remove(ByVal uiElement As String)
				RemoveCalled = True
				Strings.Remove(uiElement)
			End Sub

		End Class

	End Class
End Namespace
