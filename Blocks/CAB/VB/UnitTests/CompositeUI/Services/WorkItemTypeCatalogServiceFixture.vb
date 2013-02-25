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
Imports Microsoft.Practices.CompositeUI.Services

Namespace Services
	<TestClass()> _
	Public Class WorkItemTypeCatalogServiceFixture
		<TestMethod()> _
		Public Sub CanRegisterWorkItem()
			Dim svc As WorkItemTypeCatalogService = New WorkItemTypeCatalogService()

			svc.RegisterWorkItem(Of WorkItem)()

			Assert.AreEqual(1, svc.RegisteredWorkItemTypes.Count)
		End Sub

#Region "Utilitary code for subprocedure CanCreateInstancesOfWorkItem"

		Private Class CanCreateInstancesOfWorkItemUtilityProvider
			Private createdField As Boolean
			Public Property Created() As Boolean
				Get
					Return createdField
				End Get
				Set(ByVal createdArgument As Boolean)
					createdField = createdArgument
				End Set
			End Property

			Public Sub WorkItemCreation(ByVal obj As WorkItem)
				createdField = True
			End Sub
		End Class

		Private canCreateInstancesOfWorkItemUtility As CanCreateInstancesOfWorkItemUtilityProvider = New CanCreateInstancesOfWorkItemUtilityProvider

#End Region

		<TestMethod()> _
		Public Sub CanCreateInstancesOfWorkItem()
			Dim wi As WorkItem = New TestableRootWorkItem()
			Dim svc As WorkItemTypeCatalogService = wi.Services.AddNew(Of WorkItemTypeCatalogService, IWorkItemTypeCatalogService)()

			svc.RegisterWorkItem(Of WorkItem)()
			canCreateInstancesOfWorkItemUtility.Created = False
			svc.CreateEachWorkItem(Of WorkItem)(wi, AddressOf canCreateInstancesOfWorkItemUtility.WorkItemCreation)

			Assert.IsTrue(canCreateInstancesOfWorkItemUtility.Created)
		End Sub

#Region "Utilitary code for subprocedure CreatingEachWorkItemCheckAssigableRight"

		Private Class CreatingEachWorkItemCheckAssigableRightUtilityProvider
			Private createdField As Boolean
			Public Property Created() As Boolean
				Get
					Return createdField
				End Get
				Set(ByVal createdArgument As Boolean)
					createdField = createdArgument
				End Set
			End Property

			Public Sub WorkItemCreation(ByVal obj As ITest)
				createdField = True
			End Sub
		End Class

		Private CreatingEachWorkItemCheckAssigableRightUtility As CreatingEachWorkItemCheckAssigableRightUtilityProvider = New CreatingEachWorkItemCheckAssigableRightUtilityProvider

#End Region

		<TestMethod()> _
		Public Sub CreatingEachWorkItemCheckAssigableRight()
			Dim wi As WorkItem = New TestableRootWorkItem()
			Dim svc As WorkItemTypeCatalogService = wi.Services.AddNew(Of WorkItemTypeCatalogService, IWorkItemTypeCatalogService)()
			svc.RegisterWorkItem(Of MockWorkItem)()

			CreatingEachWorkItemCheckAssigableRightUtility.Created = False
			svc.CreateEachWorkItem(Of ITest)(wi, AddressOf CreatingEachWorkItemCheckAssigableRightUtility.WorkItemCreation)

			Assert.IsTrue(CreatingEachWorkItemCheckAssigableRightUtility.Created)
		End Sub

		Private Class MockWorkItem
			Inherits WorkItem
			Implements ITest

		End Class

		Private Interface ITest
		End Interface

	End Class
End Namespace
