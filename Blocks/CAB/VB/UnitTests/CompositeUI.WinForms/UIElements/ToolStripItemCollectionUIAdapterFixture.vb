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
	Public Class ToolStripItemCollectionAdapterFixture
		<TestMethod()> _
		Public Sub AddMethodsAppendsToEmptyCollection()
			Dim strip As ToolStrip = New ToolStrip()
			Dim adapter As ToolStripItemCollectionUIAdapter = New ToolStripItemCollectionUIAdapter(strip.Items)

			Dim button As ToolStripButton = New ToolStripButton()
			adapter.Add(button)

			Assert.AreSame(button, strip.Items(0))
		End Sub

		<TestMethod()> _
		Public Sub AddAppendsToCollectionWithItems()
			Dim strip As ToolStrip = New ToolStrip()
			Dim button1 As ToolStripButton = New ToolStripButton()
			strip.Items.Add(button1)
			Dim adapter As ToolStripItemCollectionUIAdapter = New ToolStripItemCollectionUIAdapter(strip.Items)

			Dim button2 As ToolStripButton = New ToolStripButton()
			adapter.Add(button2)

			Assert.AreEqual(2, strip.Items.Count)
			Assert.AreSame(button1, strip.Items(0))
			Assert.AreSame(button2, strip.Items(1))
		End Sub

		<TestMethod()> _
		Public Sub CanRemoveAnItem()
			Dim strip As ToolStrip = New ToolStrip()
			Dim button1 As ToolStripButton = New ToolStripButton()
			strip.Items.Add(button1)
			Dim adapter As ToolStripItemCollectionUIAdapter = New ToolStripItemCollectionUIAdapter(strip.Items)

			Dim button2 As ToolStripButton = New ToolStripButton()
			adapter.Add(button2)

			adapter.Remove(button1)

			Assert.AreEqual(1, strip.Items.Count)
			Assert.AreSame(button2, strip.Items(0))
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub CreateWithNullThrows()
			Dim oTemp As ToolStripItemCollectionUIAdapter = New ToolStripItemCollectionUIAdapter(Nothing)
		End Sub
	End Class
End Namespace
