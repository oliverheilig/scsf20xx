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
Public Class ModuleAttributeTestFixture
	<TestMethod()> _
	Public Sub ModuleAttributeIsAvailable()
		Dim attr As ModuleAttribute = New ModuleAttribute("SomeModule")
		Assert.IsNotNull(attr)
		Assert.IsTrue(TypeOf attr Is Attribute)
	End Sub

	<TestMethod()> _
	Public Sub ModuleNameIsRecorded()
		Dim attr As ModuleAttribute = New ModuleAttribute("SomeModule")
		Assert.AreEqual("SomeModule", attr.Name)
	End Sub
End Class

