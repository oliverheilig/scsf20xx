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
Imports Microsoft.Practices.CompositeUI.SmartParts

Namespace Tests.SmartParts
	<TestClass()> _
	Public Class WorkspaceCancelEventArgsFixture
		Private eventArgs As WorkspaceCancelEventArgs

		<TestInitialize()> _
		Public Sub Setup()
			eventArgs = New WorkspaceCancelEventArgs(New Object())
		End Sub

		<TestMethod()> _
		Public Sub DefaultCancelValueIsFalse()
			Assert.IsFalse(eventArgs.Cancel)
		End Sub

		<TestMethod()> _
		Public Sub SmartPartIsNotNull()
			Assert.IsNotNull(eventArgs.SmartPart)
		End Sub
	End Class
End Namespace
