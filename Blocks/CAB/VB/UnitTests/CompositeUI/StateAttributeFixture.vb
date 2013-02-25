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
Imports System.Diagnostics


<TestClass()> _
Public Class StateAttributeFixture
	Private workItem As WorkItem

	<TestInitialize()> _
	Public Sub FixtureSetup()
		workItem = New TestableRootWorkItem()
	End Sub

	<TestMethod()> _
	Public Sub StateIsAssignedMatchingType()
		workItem.State("stringValue1") = "value1"
		workItem.State("intValue1") = 1

		Dim target As MockObject = workItem.Items.AddNew(Of MockObject)()

		Assert.AreEqual("value1", target.StringProp)
		Assert.AreEqual(1, target.IntProp)
	End Sub

	Private Class MockObject
		Private innerStringProp As String

		<State()> _
		Public Property StringProp() As String
			Get
				Return innerStringProp
			End Get
			Set(ByVal value As String)
				innerStringProp = value
			End Set
		End Property

		Private innerIntProp As Integer

		<State()> _
		Public Property IntProp() As Integer
			Get
				Return innerIntProp
			End Get
			Set(ByVal value As Integer)
				innerIntProp = value
			End Set
		End Property

	End Class
End Class
