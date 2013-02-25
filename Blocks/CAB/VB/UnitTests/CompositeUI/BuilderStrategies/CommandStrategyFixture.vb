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
Imports Microsoft.Practices.CompositeUI.Commands
Imports Microsoft.Practices.CompositeUI.Tests.Mocks
Imports Microsoft.Practices.CompositeUI.BuilderStrategies
Imports Microsoft.Practices.ObjectBuilder

Namespace BuilderStrategies
	<TestClass()> _
	Public Class CommandStrategyFixture
		<TestMethod()> _
		Public Sub AddingObjectWithCommandHandlerRegisterTheCommand()
			Dim strategy As CommandStrategy = New CommandStrategy()
			Dim context As MockBuilderContext = New MockBuilderContext(strategy)
			Dim wi As WorkItem = New TestableRootWorkItem()
			context.Locator.Add(New DependencyResolutionLocatorKey(GetType(WorkItem), Nothing), wi)

			Dim sample As SampleClass = New SampleClass()

			strategy.BuildUp(context, GetType(SampleClass), sample, Nothing)

			Assert.IsTrue(wi.Items.Contains("TestCommand"))
		End Sub

		<TestMethod(), ExpectedException(GetType(CommandException))> _
		Public Sub StaticHandlerThrows()
			Dim strategy As CommandStrategy = New CommandStrategy()
			Dim context As MockBuilderContext = New MockBuilderContext(strategy)
			Dim wi As WorkItem = New TestableRootWorkItem()
			context.Locator.Add(New DependencyResolutionLocatorKey(GetType(WorkItem), Nothing), wi)

			Dim sample As SampleStaticClass = New SampleStaticClass()

			strategy.BuildUp(context, GetType(SampleStaticClass), sample, Nothing)
		End Sub


		Private Class SampleClass
			<CommandHandler("TestCommand")> _
			Public Sub TestCommandHandler(ByVal sender As Object, ByVal e As EventArgs)
			End Sub
		End Class

		Private Class SampleStaticClass
			<CommandHandler("TestCommand")> _
			Public Shared Sub TestCommandHandler(ByVal sender As Object, ByVal e As EventArgs)
			End Sub
		End Class
	End Class
End Namespace
