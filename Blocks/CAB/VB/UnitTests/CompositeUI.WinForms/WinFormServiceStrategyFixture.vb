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


<TestClass()> _
Public Class WinFormServiceStrategyFixture
	<TestMethod()> _
	Public Sub ControlActivationServiceCalledWhenControlAdded()
		Dim workItem As WorkItem = New TestableRootWorkItem()
		Dim wi As WorkItem = workItem.WorkItems.AddNew(Of WorkItem)()

		Assert.IsTrue(wi.Services.ContainsLocal(GetType(IControlActivationService)))
	End Sub
End Class

