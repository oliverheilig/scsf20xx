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
Public Class DictionaryEventArgsFixture
	Private eventArgs As DictionaryEventArgs

	<TestInitialize()> _
	Public Sub TestSetup()
		eventArgs = New DictionaryEventArgs()
	End Sub

	<TestMethod()> _
	Public Sub ToStringShowsContents()
		eventArgs.Data.Add("one", "1")
		eventArgs.Data.Add("two", "2")

		Assert.AreEqual("one: 1, two: 2", eventArgs.ToString())
	End Sub
End Class

