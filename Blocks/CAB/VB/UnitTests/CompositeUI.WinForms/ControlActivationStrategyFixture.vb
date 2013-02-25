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
Imports Microsoft.Practices.CompositeUI.SmartParts
Imports Microsoft.Practices.ObjectBuilder


<TestClass()> _
Public Class ControlActivationStrategyFixture
	<TestMethod()> _
	Public Sub StrategyAddsActivationService()
		Dim workItem As WorkItem = New TestableRootWorkItem()
		workItem.Services.Remove(GetType(IControlActivationService))
		Dim service As MockControlActivationService = New MockControlActivationService()
		workItem.Services.Add(Of IControlActivationService)(service)
		Dim context As MockBuilderContext = New MockBuilderContext()
		Dim strat As ControlActivationStrategy = New ControlActivationStrategy()
		context.Locator.Add(New DependencyResolutionLocatorKey(GetType(WorkItem), Nothing), workItem)

		Dim view As Control = New Control()
		workItem.Items.Add(view)

		Assert.IsTrue(service.ControlToMonitorCalled, "Control.Enter didn't cause WorkItem.Activate to be called")
	End Sub

	Private Class MockControlActivationService
		Implements IControlActivationService
		Public ControlToMonitorCalled As Boolean = False

#Region "IControlActivationService Members"

		Public Sub ControlAdded(ByVal control As Control) Implements IControlActivationService.ControlAdded
			ControlToMonitorCalled = True
		End Sub

		Public Sub ControlRemoved(ByVal control As Control) Implements IControlActivationService.ControlRemoved
			Throw New System.Exception("The method or operation is not implemented.")
		End Sub

#End Region
	End Class
End Class
