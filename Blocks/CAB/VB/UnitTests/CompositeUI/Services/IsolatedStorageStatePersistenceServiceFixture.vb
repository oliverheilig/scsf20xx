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
Imports System.Collections.Specialized
Imports System.IO.IsolatedStorage
Imports Microsoft.Practices.CompositeUI.Services

Namespace Services
	<TestClass()> _
	Public Class IsolatedStorageStatePersistenceServiceFixture
		Private Shared service As IsolatedStorageStatePersistenceService

		<TestInitialize()> _
		Public Sub SetUp()
			service = New IsolatedStorageStatePersistenceService()
		End Sub

		<TestCleanup()> _
		Public Sub TearDown()
			service.Clear()
		End Sub

		<TestMethod()> _
		Public Sub DoesNotFindFile()
			Dim contains As Boolean = service.Contains(Guid.NewGuid().ToString())
		End Sub

		<TestMethod()> _
		Public Sub CanSaveState()
			Dim s As State = New State()

			service.Save(s)
		End Sub

		<TestMethod()> _
		Public Sub CanSaveStateAndVerify()
			Dim id As String = "DummyID"
			Dim s As State = New State(id)

			service.Save(s)

			Assert.IsTrue(service.Contains(id))
		End Sub

		<TestMethod()> _
		Public Sub CanSaveAndLoadState()
			Dim id As String = "DummyID"
			Dim s As State = New State(id)
			s("key") = "value"

			service.Save(s)
			Dim loaded As State = service.Load(id)

			Assert.AreEqual("value", loaded("key"))
		End Sub

		<TestMethod(), ExpectedException(GetType(StatePersistenceException))> _
		Public Sub LoadNonExistingThrows()
			service.Load(Guid.NewGuid().ToString())
		End Sub

		<TestMethod()> _
		Public Sub RemoveNonExistingNoOp()
			service.Remove(Guid.NewGuid().ToString())
		End Sub

		<TestMethod()> _
		Public Sub CanOpenArbitraryStore()
			Assert.IsFalse(New IsolatedStorageStatePersistenceService(IsolatedStorageScope.Assembly Or IsolatedStorageScope.User).Contains(Guid.NewGuid().ToString()))
		End Sub

		<TestMethod()> _
		Public Sub CanConfigureScope()
			Dim settings As NameValueCollection = New NameValueCollection()
			settings.Add(IsolatedStorageStatePersistenceService.ScopeAttribute, "Assembly| Roaming |   User")

			service.Configure(settings)

			Assert.AreEqual(IsolatedStorageScope.Assembly Or IsolatedStorageScope.User Or IsolatedStorageScope.Roaming, service.Scope)
		End Sub
	End Class
End Namespace
