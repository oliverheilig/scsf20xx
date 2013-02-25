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
Imports Microsoft.Practices.CompositeUI.Utility

<TestClass()> _
Public Class DataEventArgsFixture
	<TestMethod()> _
	Public Sub IsCreatable()
		Dim e As DataEventArgs(Of Integer) = New DataEventArgs(Of Integer)(32)
		Assert.IsNotNull(e, "Cannot create an instance of ApplicationEventArgs")
	End Sub

	<TestMethod()> _
	Public Sub CanPassData()
		Dim e As DataEventArgs(Of Integer) = New DataEventArgs(Of Integer)(32)
		Assert.AreEqual(32, e.Data)
	End Sub

	<TestMethod()> _
	Public Sub IsEventArgs()
		Assert.IsTrue(GetType(EventArgs).IsAssignableFrom(GetType(DataEventArgs(Of Integer))), "ApplicationEventArgs is not a subtype of EventArgs")
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
	Public Sub ThrowsIfDataIsNull()
		Dim data As DataEventArgs(Of Object) = New DataEventArgs(Of Object)(Nothing)
	End Sub
End Class
