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

Namespace Services
	<TestClass()> _
	Public Class ServiceAttributeTestFixture
		<TestMethod()> _
		Public Sub ServiceAttributeIsAvailable()
			Dim attr As ServiceAttribute = New ServiceAttribute()
			Assert.IsNotNull(attr)
			Assert.IsNull(attr.RegisterAs)
		End Sub

		<TestMethod()> _
		Public Sub TypeGetStored()
			Dim attr As ServiceAttribute = New ServiceAttribute(GetType(ServiceAttributeTestFixture))
			Assert.AreEqual(GetType(ServiceAttributeTestFixture), attr.RegisterAs)
		End Sub

		<TestMethod()> _
		Public Sub HasCreateOnDemandProperty()
			Dim attr As ServiceAttribute = New ServiceAttribute()
			Assert.IsFalse(attr.AddOnDemand)
		End Sub
	End Class
End Namespace
