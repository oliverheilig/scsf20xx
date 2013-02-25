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
	Public Class ServiceCollectionFixture
		Private collection As ServiceElementCollection

		<TestInitialize()> _
		Public Sub Setup()
			collection = New ServiceElementCollection()
		End Sub

		<TestMethod()> _
		Public Sub CollectionIsEmpty()
			Assert.AreEqual(0, collection.Count)
		End Sub

		<TestMethod()> _
		Public Sub CanAddService()
			Dim se As ServiceElement = New ServiceElement()
			se.ServiceType = GetType(Object)
			collection.Add(se)

			Assert.IsNotNull(collection(GetType(Object)))
			Assert.AreEqual(1, collection.Count)
		End Sub

		<TestMethod()> _
		Public Sub CanAddUsingIndexer()
			Dim se As ServiceElement = New ServiceElement()
			se.ServiceType = GetType(Object)
			collection(se.ServiceType) = se

			Assert.IsNotNull(collection(GetType(Object)))
			Assert.AreEqual(1, collection.Count)
		End Sub

		<TestMethod()> _
		Public Sub CanRemoveElement()
			Dim se As ServiceElement = New ServiceElement()
			se.ServiceType = GetType(Object)
			collection(se.ServiceType) = se
			collection.Remove(GetType(Object))

			Assert.AreEqual(0, collection.Count)
			Assert.IsNull(collection(GetType(Object)))
		End Sub

		<TestMethod()> _
		Public Sub AddingServiceOfSameTimeReplacesOldOne()
			Dim se1 As ServiceElement = New ServiceElement()
			Dim se2 As ServiceElement = New ServiceElement()
			se1.ServiceType = GetType(Object)
			se2.ServiceType = GetType(Object)

			collection(GetType(Object)) = se1
			collection(GetType(Object)) = se2

			Assert.AreSame(se2, collection(GetType(Object)))
		End Sub

	End Class
End Namespace
