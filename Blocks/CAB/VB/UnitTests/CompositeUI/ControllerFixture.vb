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


<TestClass()> _
Public Class ControllerFixture
	<TestMethod()> _
	Public Sub ControllerStateIsNullByDefault()
		Dim controller As Controller = New Controller()

		Assert.IsNull(controller.State)
	End Sub

	<TestMethod()> _
	Public Sub ControllerWorkItemIsNullByDefault()
		Dim controller As Controller = New Controller()

		Assert.IsNull(controller.WorkItem)
	End Sub

	<TestMethod()> _
	Public Sub ContollerReceivedWorkItemAndStateFromWorkItem()
		Dim wi As WorkItem = New TestableRootWorkItem()
		Dim controller As Controller = wi.Items.AddNew(Of Controller)()

		Assert.AreSame(wi, controller.WorkItem)
		Assert.AreSame(wi.State, controller.State)
	End Sub
End Class
