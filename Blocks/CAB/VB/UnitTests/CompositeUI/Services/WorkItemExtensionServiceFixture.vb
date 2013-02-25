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
Imports Microsoft.Practices.CompositeUI.Services

Namespace Services
	<TestClass()> _
	Public Class WorkItemExtensionServiceFixture

		<TestMethod()> _
		Public Sub Bug1713()
			Dim service As WorkItemExtensionService = New WorkItemExtensionService()
			Dim wi As WorkItem = New TestableRootWorkItem()

			service.RegisterExtension(GetType(MyWorkItem), GetType(MockExtension))
			service.RegisterExtension(GetType(MyWorkItem), GetType(MockExtension2))

			service.RegisterExtension(GetType(MyWorkItem2), GetType(MockExtension))
			service.RegisterExtension(GetType(MyWorkItem2), GetType(MockExtension2))
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub NullWorkItemTypeThrows()
			Dim service As WorkItemExtensionService = New WorkItemExtensionService()

			service.RegisterExtension(Nothing, GetType(MockExtension))
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub NullExtensionTypeThrows()
			Dim service As WorkItemExtensionService = New WorkItemExtensionService()

			service.RegisterExtension(GetType(WorkItem), Nothing)
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub NullRootExtensionTypeThrows()
			Dim service As WorkItemExtensionService = New WorkItemExtensionService()

			service.RegisterRootExtension(Nothing)
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentException))> _
		Public Sub ThrowsIfExtensionTypeIsNotIWorkItemExtension()
			Dim service As WorkItemExtensionService = New WorkItemExtensionService()

			service.RegisterExtension(GetType(WorkItem), GetType(Object))
		End Sub

		<TestMethod()> _
		Public Sub CanRegisterExtensionType()
			Dim service As WorkItemExtensionService = New WorkItemExtensionService()

			service.RegisterExtension(GetType(WorkItem), GetType(MockExtension))

			Assert.AreEqual(1, service.RegisteredExtensions.Count)
			Assert.IsTrue(service.RegisteredExtensions.ContainsKey(GetType(WorkItem)))
		End Sub

		<TestMethod()> _
		Public Sub CanInitializeExtensionsForWorkItem()
			Dim service As WorkItemExtensionService = New WorkItemExtensionService()
			Dim wi As WorkItem = New TestableRootWorkItem()
			service.RegisterExtension(GetType(WorkItem), GetType(MockExtension))

			service.InitializeExtensions(wi)

			Assert.AreEqual(True, MockExtension.Initialized)
		End Sub

		<TestMethod()> _
		Public Sub CreatingWorkItemInitializesExtensions()
			Dim wi As WorkItem = New TestableRootWorkItem()
			Dim svc As WorkItemExtensionService = wi.Services.AddNew(Of WorkItemExtensionService, IWorkItemExtensionService)()
			svc.RegisterExtension(GetType(WorkItem), GetType(MockExtension))

			wi.Items.AddNew(Of WorkItem)()

			Assert.AreEqual(True, MockExtension.Initialized)
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentException))> _
		Public Sub RegisteringExtensionTwiceThrows()
			Dim service As WorkItemExtensionService = New WorkItemExtensionService()
			Dim wi As WorkItem = New TestableRootWorkItem()
			service.RegisterExtension(GetType(WorkItem), GetType(MockExtension))

			service.RegisterExtension(GetType(WorkItem), GetType(MockExtension))
		End Sub

		<TestMethod()> _
		Public Sub CanRegisterExtensionForDifferentWorkItems()
			Dim service As WorkItemExtensionService = New WorkItemExtensionService()
			Dim wi As WorkItem = New TestableRootWorkItem()
			service.RegisterExtension(GetType(MyWorkItem), GetType(MockExtension))
			service.RegisterExtension(GetType(MyWorkItem2), GetType(MockExtension))

			Assert.IsTrue(service.RegisteredExtensions.ContainsKey(GetType(MyWorkItem)))
			Assert.IsTrue(service.RegisteredExtensions.ContainsKey(GetType(MyWorkItem2)))
		End Sub

#Region "Helper classes"

		Private Class MyWorkItem
			Inherits WorkItem

		End Class

		Private Class MyWorkItem2
			Inherits MyWorkItem

		End Class

		Private Class MockExtension
			Implements IWorkItemExtension

			Public Shared Initialized As Boolean = False

			Public Sub Initialize(ByVal workItem As WorkItem) Implements IWorkItemExtension.Initialize
				Initialized = True
			End Sub
		End Class

		Private Class MockExtension2
			Implements IWorkItemExtension

			Public Sub Initialize(ByVal workItem As WorkItem) Implements IWorkItemExtension.Initialize
			End Sub

		End Class

#End Region
	End Class
End Namespace
