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








Imports Microsoft.Practices.CompositeUI.Commands
Imports System

Namespace Tests.Commands
	<TestClass()> _
	Public Class CommandHandlerAttributeFixture
		<TestMethod()> _
		Public Sub CanCreateAttribute()
			Dim attr As CommandHandlerAttribute = New CommandHandlerAttribute("TestCommand")

			Assert.IsNotNull(attr)
			Assert.AreEqual("TestCommand", attr.CommandName)
		End Sub


		<TestMethod()> _
		Public Sub AddingObjectWithoutCommandHandlerAttributeDoesNotCreateCommand()
			Dim item As TestableRootWorkItem = New TestableRootWorkItem()
			Dim cmdCount As Integer = item.Commands.Count

			Dim cmdHandler As Object = item.Items.AddNew(Of Object)()

			Assert.AreEqual(cmdCount, item.Commands.Count)
		End Sub

		<TestMethod()> _
		Public Sub SingleCommandHandlerAttributeAddsOneCommand()
			Dim item As TestableRootWorkItem = New TestableRootWorkItem()
			Dim cmdCount As Integer = item.Commands.Count

			Dim cmdHandler As SingleTestCommandHandler = item.Items.AddNew(Of SingleTestCommandHandler)()

			Assert.AreEqual(cmdCount + 1, item.Commands.Count)
		End Sub

		<TestMethod()> _
		Public Sub SingleCommandHandlerAttributeIsCalledOnce()
			Dim item As TestableRootWorkItem = New TestableRootWorkItem()
			Dim cmdHandler As SingleTestCommandHandler = item.Items.AddNew(Of SingleTestCommandHandler)()

			item.Commands("TestCommand").Execute()

			Assert.AreEqual(1, cmdHandler.CommandHandlerCalledCount)
		End Sub


		<TestMethod()> _
		Public Sub MultipleEqualAttributesRegisterCommandOnlyOnce()
			Dim item As TestableRootWorkItem = New TestableRootWorkItem()
			Dim cmdCount As Integer = item.Commands.Count

			Dim cmdHandler As TwoTestCommandHandler = item.Items.AddNew(Of TwoTestCommandHandler)()

			Assert.AreEqual(cmdCount + 1, item.Commands.Count)
		End Sub


		<TestMethod()> _
		Public Sub MultipleEqualAttributesRegisterHandlerWithCommandOnlyOnce()
			Dim item As TestableRootWorkItem = New TestableRootWorkItem()
			Dim cmdHandler As TwoTestCommandHandler = item.Items.AddNew(Of TwoTestCommandHandler)()

			item.Commands("TestCommand").Execute()

			Assert.AreEqual(1, cmdHandler.CommandHandlerCalledCount)
		End Sub

		<TestMethod(), ExpectedException(GetType(CommandException))> _
		Public Sub StaticHandlerThrows()
			Dim item As TestableRootWorkItem = New TestableRootWorkItem()

			Dim cmdHandler As SingleStaticTestCommandHandler = item.Items.AddNew(Of SingleStaticTestCommandHandler)()
		End Sub

		Private Class SingleStaticTestCommandHandler
			Public CommandHandlerCalledCount As Integer = 0

			<CommandHandler("TestCommand")> _
			Public Shared Sub TestCommandHandler(ByVal sender As Object, ByVal e As EventArgs)
			End Sub
		End Class

		Private Class SingleTestCommandHandler
			Public CommandHandlerCalledCount As Integer = 0

			<CommandHandler("TestCommand")> _
			Public Sub TestCommandHandler(ByVal sender As Object, ByVal e As EventArgs)
				CommandHandlerCalledCount += 1
			End Sub
		End Class

		Private Class TwoTestCommandHandler
			Public CommandHandlerCalledCount As Integer = 0

			<CommandHandler("TestCommand"), CommandHandler("TestCommand")> _
			Public Sub TestCommandHandler(ByVal sender As Object, ByVal e As EventArgs)
				CommandHandlerCalledCount += 1
			End Sub

		End Class
	End Class
End Namespace
