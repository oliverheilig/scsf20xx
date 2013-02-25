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
Imports Microsoft.Practices.CompositeUI.UIElements

Namespace Collections
	<TestClass()> _
	Public Class UIExtensionSiteFixture
		<TestMethod()> _
		Public Sub AddingItemToSiteAddsToAdapter()
			Dim adapter As MockAdapter = New MockAdapter()
			Dim site As UIExtensionSite = New UIExtensionSite(adapter)
			Dim obj As Object = New Object()

			Dim result As Object = site.Add(obj)

			Assert.AreSame(obj, result)
			Assert.AreSame(obj, adapter.AddedElement)
		End Sub

		<TestMethod()> _
		Public Sub AddingItemToSiteShowsItemInSiteCollection()
			Dim adapter As MockAdapter = New MockAdapter()
			Dim site As UIExtensionSite = New UIExtensionSite(adapter)
			Dim obj As Object = New Object()

			site.Add(obj)

			Assert.AreEqual(1, site.Count)
			Assert.IsTrue(site.Contains(obj))
		End Sub

		<TestMethod()> _
		Public Sub CanEnumerateItemsInSite()
			Dim adapter As MockAdapter = New MockAdapter()
			Dim site As UIExtensionSite = New UIExtensionSite(adapter)
			Dim obj1 As Object = New Object()
			Dim obj2 As Object = New Object()
			site.Add(obj1)
			site.Add(obj2)

			Dim foundObj1 As Boolean = False
			Dim foundObj2 As Boolean = False

			For Each obj As Object In site
				If Object.ReferenceEquals(obj, obj1) Then
					foundObj1 = True
				ElseIf Object.ReferenceEquals(obj, obj2) Then
					foundObj2 = True
				End If
			Next obj

			Assert.IsTrue(foundObj1)
			Assert.IsTrue(foundObj2)
		End Sub

		<TestMethod()> _
		Public Sub RemovingItemFromSiteRemovesItFromAdapter()
			Dim adapter As MockAdapter = New MockAdapter()
			Dim site As UIExtensionSite = New UIExtensionSite(adapter)
			Dim obj As Object = New Object()

			site.Add(obj)
			site.Remove(obj)

			Assert.AreSame(obj, adapter.RemovedElement)
			Assert.AreEqual(0, site.Count)
		End Sub

		<TestMethod()> _
		Public Sub RemovingItemNotInSiteDoesNotRemoveFromAdapter()
			Dim adapter As MockAdapter = New MockAdapter()
			Dim site As UIExtensionSite = New UIExtensionSite(adapter)
			Dim obj As Object = New Object()

			site.Remove(obj)

			Assert.IsNull(adapter.RemovedElement)
		End Sub

		<TestMethod()> _
		Public Sub ClearingItemsRemovesFromAdapter()
			Dim adapter As MockAdapter = New MockAdapter()
			Dim site As UIExtensionSite = New UIExtensionSite(adapter)
			Dim obj As Object = New Object()

			site.Add(obj)
			site.Clear()

			Assert.AreSame(obj, adapter.RemovedElement)
			Assert.AreEqual(0, site.Count)
		End Sub

		Private Class MockAdapter
			Implements IUIElementAdapter

			Public AddedElement As Object = Nothing
			Public RemovedElement As Object = Nothing

			Public Function Add(ByVal uiElement As Object) As Object Implements IUIElementAdapter.Add
				AddedElement = uiElement
				Return uiElement
			End Function

			Public Sub Remove(ByVal uiElement As Object) Implements IUIElementAdapter.Remove
				RemovedElement = uiElement
			End Sub
		End Class
	End Class
End Namespace
