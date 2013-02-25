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
Imports Microsoft.Practices.CompositeUI.Utility

<TestClass()> _
Public Class GuardFixture
	<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
	Public Sub StringNotNullThrowsWithNullString()
		Guard.ArgumentNotNull(Nothing, "Foo")
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
	Public Sub StringNotNullOrEmptyThrowsWithNullString()
		Guard.ArgumentNotNullOrEmptyString(Nothing, "Foo")
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentException))> _
	Public Sub StringNotNullOrEmptyThrowsWithEmptyString()
		Guard.ArgumentNotNullOrEmptyString("", "Foo")
	End Sub

	<TestMethod()> _
	Public Sub StringNotNullOrEmptyDoesNotThrowWithValidString()
		Guard.ArgumentNotNullOrEmptyString("Foo", "Foo")
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentException))> _
	Public Sub EnumValueIsDefinedThrowIfValueIsUndefined()
		Guard.EnumValueIsDefined(GetType(TestEnum), 2, "argument")
	End Sub

	<TestMethod()> _
	Public Sub EnumValueIsDefinedDoesNotThrowIfValueIsDefined()
		Guard.EnumValueIsDefined(GetType(TestEnum), 0, "argument")
		Guard.EnumValueIsDefined(GetType(TestEnum), 1, "argument")
		Guard.EnumValueIsDefined(GetType(TestEnum), TestEnum.value1, "argument")
		Guard.EnumValueIsDefined(GetType(TestEnum), TestEnum.value2, "argument")
	End Sub

	<TestMethod()> _
	Public Sub AssignableTypesDoNotThrow()
		Guard.TypeIsAssignableFromType(GetType(String), GetType(Object), "argument")
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentException))> _
	Public Sub NonAssignableTypesThrow()
		Guard.TypeIsAssignableFromType(GetType(Object), GetType(String), "argument")
	End Sub

	Private Enum TestEnum
		value1
		value2
	End Enum
End Class
