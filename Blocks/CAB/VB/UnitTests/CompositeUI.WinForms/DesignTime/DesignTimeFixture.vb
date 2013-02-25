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
Imports System.ComponentModel
Imports Microsoft.Practices.CompositeUI.SmartParts

Namespace DesignTime
	<TestClass()> _
	Public Class DesignTimeFixture
		'[TestMethod]
		'[Ignore("Design time code generation needs to change... See CustomWorkItem.cs!!!")]
		Public Sub DraggedComponentsAreSitedWithNameWhenWorkItemSited()
			Dim workItem As WorkItem = New TestableRootWorkItem()
			Dim wi As CustomWorkItem = workItem.WorkItems.AddNew(Of CustomWorkItem)()

			Assert.IsTrue(wi.Items.ContainsObject(wi.customerInformation))
			Assert.IsTrue(wi.Items.ContainsObject(wi.anyComponent))
			Assert.IsTrue(wi.Items.ContainsObject("customerInformation"))
			Assert.IsTrue(wi.Items.ContainsObject("anyComponent"))
		End Sub

		<TestMethod()> _
		Public Sub DraggedWorkspaceIsAddedToCollection()
			Dim workItem As WorkItem = New TestableRootWorkItem()
			Dim wi As CustomWorkItem = workItem.WorkItems.AddNew(Of CustomWorkItem)()

			Assert.IsTrue(wi.Workspaces.Contains("window"))
		End Sub

	End Class
End Namespace
