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
Imports System.Text
Imports Microsoft.Practices.CompositeUI.WinForms.UIElements
Imports System.Windows.Forms
Imports Microsoft.Practices.CompositeUI.UIElements

Namespace UIElements
	<TestClass()> _
	Public Class ToolStripUIAdapterFactoryFixture
		<TestMethod()> _
		Public Sub FactorySupportsCorrectTypes()
			Dim factory As ToolStripUIAdapterFactory = New ToolStripUIAdapterFactory()
			Dim strip As ToolStrip = New ToolStrip()

			Assert.IsTrue(factory.Supports(New ToolStrip()))
			Assert.IsTrue(factory.Supports(New MenuStrip()))
			Assert.IsTrue(factory.Supports(New ToolStripButton())) ' Derived from ToolStripItem
			Assert.IsTrue(factory.Supports(New ToolStripMenuItem()))
			Assert.IsTrue(factory.Supports(New ToolStripDropDownMenu()))
			Assert.IsTrue(factory.Supports(strip.Items))
		End Sub

		<TestMethod()> _
		Public Sub FactoryCreatesCorrectAdapterForToolStrip()
			Dim factory As ToolStripUIAdapterFactory = New ToolStripUIAdapterFactory()
			Dim adapter As IUIElementAdapter = factory.GetAdapter(New ToolStrip())
			Assert.IsTrue(TypeOf adapter Is ToolStripItemCollectionUIAdapter)
		End Sub

		<TestMethod()> _
		Public Sub FactoryCreatesCorrectAdapterForMenuStrip()
			Dim factory As ToolStripUIAdapterFactory = New ToolStripUIAdapterFactory()
			Dim adapter As IUIElementAdapter = factory.GetAdapter(New MenuStrip())
			Assert.IsTrue(TypeOf adapter Is ToolStripItemCollectionUIAdapter)
		End Sub

		<TestMethod()> _
		Public Sub FactoryCreatesCorrectAdapterForToolStripButton()
			Dim factory As ToolStripUIAdapterFactory = New ToolStripUIAdapterFactory()
			Dim strip As ToolStrip = New ToolStrip()
			Dim item As ToolStripButton = New ToolStripButton()
			strip.Items.Add(item)
			Dim adapter As IUIElementAdapter = factory.GetAdapter(item)
			Assert.IsTrue(TypeOf adapter Is ToolStripItemOwnerCollectionUIAdapter)
		End Sub

		<TestMethod()> _
		Public Sub FactoryCreatesCorrectAdapterForToolStripMenuItem()
			Dim factory As ToolStripUIAdapterFactory = New ToolStripUIAdapterFactory()
			Dim strip As ToolStrip = New ToolStrip()
			Dim item As ToolStripMenuItem = New ToolStripMenuItem()
			strip.Items.Add(item)
			Dim adapter As IUIElementAdapter = factory.GetAdapter(item)
			Assert.IsTrue(TypeOf adapter Is ToolStripItemOwnerCollectionUIAdapter)
		End Sub

		<TestMethod()> _
		Public Sub FactoryCreatesCorrectAdapterForToolStripDropDownMenu()
			Dim factory As ToolStripUIAdapterFactory = New ToolStripUIAdapterFactory()
			Dim adapter As IUIElementAdapter = factory.GetAdapter(New ToolStripDropDownMenu())
			Assert.IsTrue(TypeOf adapter Is ToolStripItemCollectionUIAdapter)
		End Sub

		<TestMethod()> _
		Public Sub FactoryCreatesCorrectAdapterForToolStripItemCollection()
			Dim factory As ToolStripUIAdapterFactory = New ToolStripUIAdapterFactory()
			Dim strip As MenuStrip = New MenuStrip()
			Dim adapter As IUIElementAdapter = factory.GetAdapter(strip.Items)
			Assert.IsTrue(TypeOf adapter Is ToolStripItemCollectionUIAdapter)
		End Sub
	End Class
End Namespace
