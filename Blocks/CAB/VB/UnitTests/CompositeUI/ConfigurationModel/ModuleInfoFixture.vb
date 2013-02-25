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
Imports Microsoft.Practices.CompositeUI.Configuration

Namespace ConfigurationModel
	<TestClass()> _
	Public Class ModuleInfoFixture
		<TestMethod()> _
		Public Sub InitializesCorrectly()
			Dim mInfo As ModuleInfo = New ModuleInfo()
			Assert.AreEqual(0, mInfo.AllowedRoles.Count)
		End Sub

		<TestMethod()> _
		Public Sub CanInitializeWithAssemblyFile()
			Dim mInfo As ModuleInfo = New ModuleInfo("MyAssembly.dll")
			Assert.AreEqual("MyAssembly.dll", mInfo.AssemblyFile)
		End Sub

		<TestMethod()> _
		Public Sub CanSetUpdateLocation()
			Dim mInfo As ModuleInfo = New ModuleInfo()
			mInfo.SetUpdateLocation("http://somelocation/someapplication")
			Assert.AreEqual("http://somelocation/someapplication", mInfo.UpdateLocation)
		End Sub

		<TestMethod()> _
		Public Sub CanAddSingleRole()
			Dim mInfo As ModuleInfo = New ModuleInfo()
			mInfo.AddRoles("role1")
			Assert.AreEqual(1, mInfo.AllowedRoles.Count)
			Assert.AreEqual("role1", mInfo.AllowedRoles(0))
		End Sub

		<TestMethod()> _
		Public Sub CanAddSeveralRoles()
			Dim mInfo As ModuleInfo = New ModuleInfo()
			mInfo.AddRoles("role1", "role2")
			Assert.AreEqual(2, mInfo.AllowedRoles.Count)
			Assert.AreEqual("role1", mInfo.AllowedRoles(0))
			Assert.AreEqual("role2", mInfo.AllowedRoles(1))
		End Sub

		<TestMethod()> _
		Public Sub CanClearRoles()
			Dim mInfo As ModuleInfo = New ModuleInfo()
			mInfo.AddRoles("role1", "role2")
			mInfo.ClearRoles()
			Assert.AreEqual(0, mInfo.AllowedRoles.Count)
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub ThrowsIfAddingNullRole()
			Dim mInfo As ModuleInfo = New ModuleInfo()
			mInfo.AddRoles(Nothing)
		End Sub

	End Class
End Namespace
