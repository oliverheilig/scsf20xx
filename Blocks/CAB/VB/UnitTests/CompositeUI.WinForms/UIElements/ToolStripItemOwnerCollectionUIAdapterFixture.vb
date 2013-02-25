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
Imports Microsoft.Practices.CompositeUI.WinForms.UIElements

Namespace UIElements
	<TestClass()> _
	Public Class ToolStripItemOwnerCollectionUIAdapterFixture
		<TestMethod()> _
		Public Sub AddInsertAfterAttachedItem()
			Dim strip As ToolStrip = New ToolStrip()
			Dim extensionSite As ToolStripSeparator = New ToolStripSeparator()
			strip.Items.Add(extensionSite)
			Dim adapter As ToolStripItemOwnerCollectionUIAdapter = New ToolStripItemOwnerCollectionUIAdapter(extensionSite)

			Dim button As ToolStripButton = New ToolStripButton()
			adapter.Add(button)

			Assert.AreEqual(2, strip.Items.Count)
			Assert.AreSame(extensionSite, strip.Items(0))
			Assert.AreSame(button, strip.Items(1))
		End Sub

		<TestMethod()> _
		Public Sub AddInsertsAfterAttachedItemWhenOneFollowsIt()
			Dim strip As ToolStrip = New ToolStrip()
			Dim extensionSite As ToolStripSeparator = New ToolStripSeparator()
			strip.Items.Add(extensionSite)
			Dim followingButton As ToolStripButton = New ToolStripButton()
			strip.Items.Add(followingButton)
			Dim adapter As ToolStripItemOwnerCollectionUIAdapter = New ToolStripItemOwnerCollectionUIAdapter(extensionSite)

			Dim button As ToolStripButton = New ToolStripButton()
			adapter.Add(button)

			Assert.AreEqual(3, strip.Items.Count)
			Assert.AreSame(extensionSite, strip.Items(0))
			Assert.AreSame(button, strip.Items(1))
			Assert.AreSame(followingButton, strip.Items(2))
		End Sub


		<TestMethod()> _
		Public Sub ItemCanBeRemovedWhenManagedItemHasAlreadyBeenRemoved()
			Dim strip As ToolStrip = New ToolStrip()
			Dim separator As ToolStripSeparator = New ToolStripSeparator()
			strip.Items.Add(separator)
			Dim adapterDependingOnSeparator As ToolStripItemOwnerCollectionUIAdapter = New ToolStripItemOwnerCollectionUIAdapter(separator)
			Dim button As ToolStripButton = New ToolStripButton("Foo")
			adapterDependingOnSeparator.Add(button)

			strip.Items.Remove(separator)
			adapterDependingOnSeparator.Remove(button)

			Assert.IsFalse(strip.Items.Contains(button))
		End Sub

		<TestMethod(), ExpectedException(GetType(InvalidOperationException))> _
		Public Sub AddThrowsIfManagedItemIsRemoved()
			Dim strip As ToolStrip = New ToolStrip()
			Dim separator As ToolStripSeparator = New ToolStripSeparator()
			strip.Items.Add(separator)
			Dim adapterDependingOnSeparator As ToolStripItemOwnerCollectionUIAdapter = New ToolStripItemOwnerCollectionUIAdapter(separator)

			strip.Items.Remove(separator)
			adapterDependingOnSeparator.Add(New ToolStripButton())
		End Sub

		<TestMethod()> _
		Public Sub RemoveAnItemFromTheOriginalCollectionWhenTheManagedItemIsMovedToAnotherCollection()
			Dim strip1 As ToolStrip = New ToolStrip()
			Dim separator As ToolStripSeparator = New ToolStripSeparator()
			strip1.Items.Add(separator)
			Dim adapterDependingOnSeparator As ToolStripItemOwnerCollectionUIAdapter = New ToolStripItemOwnerCollectionUIAdapter(separator)

			Dim button As ToolStripButton = New ToolStripButton("Foo")
			adapterDependingOnSeparator.Add(button)

			strip1.Items.Remove(separator)

			Dim strip2 As ToolStrip = New ToolStrip()
			strip2.Items.Add(separator)

			adapterDependingOnSeparator.Remove(button)

			Assert.IsFalse(strip1.Items.Contains(button))
		End Sub

		<TestMethod()> _
		Public Sub AddInsertsInNewCollectionWhenManagedItemMoved()
			Dim strip1 As ToolStrip = New ToolStrip()
			Dim strip2 As ToolStrip = New ToolStrip()
			Dim extensionSite As ToolStripSeparator = New ToolStripSeparator()
			strip1.Items.Add(extensionSite)
			Dim adapter As ToolStripItemOwnerCollectionUIAdapter = New ToolStripItemOwnerCollectionUIAdapter(extensionSite)

			strip1.Items.Remove(extensionSite)
			strip2.Items.Add(extensionSite)

			Dim button As ToolStripButton = New ToolStripButton()
			adapter.Add(button)

			Assert.AreEqual(0, strip1.Items.Count)
			Assert.AreEqual(2, strip2.Items.Count)
			Assert.AreSame(extensionSite, strip2.Items(0))
			Assert.AreSame(button, strip2.Items(1))
		End Sub


		<TestMethod()> _
		Public Sub RemovingAnItemFromDisposedStripDoesNotThrow()
			Dim strip1 As ToolStrip = New ToolStrip()
			Dim separator As ToolStripSeparator = New ToolStripSeparator()
			strip1.Items.Add(separator)

			Dim button As ToolStripButton = New ToolStripButton("Foo")
			Dim adapterDependingOnSeparator As ToolStripItemOwnerCollectionUIAdapter = New ToolStripItemOwnerCollectionUIAdapter(separator)
			adapterDependingOnSeparator.Add(button)

			strip1.Dispose()

			adapterDependingOnSeparator.Remove(button)
		End Sub
	End Class
End Namespace
