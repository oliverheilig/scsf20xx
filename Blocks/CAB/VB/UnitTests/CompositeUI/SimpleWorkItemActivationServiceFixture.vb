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
Public Class SimpleWorkItemActivationServiceFixture
	<TestMethod()> _
	Public Sub ChecksThatWorkItemIsNotTerminatedBeforeChangingStatus()
		Dim rootWorkItem As WorkItem = New TestableRootWorkItem()
		rootWorkItem.Services.AddNew(Of SimpleWorkItemActivationService, IWorkItemActivationService)()
		Dim w1 As WorkItem = rootWorkItem.WorkItems.AddNew(Of WorkItem)()
		Dim w2 As WorkItem = rootWorkItem.WorkItems.AddNew(Of WorkItem)()

		w2.Activate()
		w2.Terminate()
		w1.Activate()

		Assert.AreEqual(WorkItemStatus.Active, w1.Status)
		Assert.AreEqual(WorkItemStatus.Terminated, w2.Status)
	End Sub
End Class
