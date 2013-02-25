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

Namespace Tests.Commands
	<TestClass()> _
	Public Class CommandAdapterFixture
		Private Shared adapter As MockAdapter

		<TestInitialize()> _
		Public Sub SetUp()
			adapter = New MockAdapter()
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub ThrowsIfBindNullCommand()
			adapter.BindCommand(Nothing)
		End Sub

		<TestMethod(), ExpectedException(GetType(InvalidOperationException))> _
		Public Sub ThrowsIfBindTwice()
			adapter.BindCommand(New Command())
			adapter.BindCommand(New Command())
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub ThrowsIfUnbindNullCommand()
			adapter.UnbindCommand(Nothing)
		End Sub

		<TestMethod(), ExpectedException(GetType(InvalidOperationException))> _
		Public Sub ThrowsIfUnbindToNotBoundCommand()
			adapter.UnbindCommand(New Command())
		End Sub

		<TestMethod(), ExpectedException(GetType(InvalidOperationException))> _
		Public Sub ThrowsIfUnbindWithDifferentCommand()
			adapter.BindCommand(New Command())
			adapter.UnbindCommand(New Command())
		End Sub

		<TestMethod()> _
		Public Sub CanBindAgainIfUnbound()
			Dim cmd As Command = New Command()
			adapter.BindCommand(cmd)
			adapter.UnbindCommand(cmd)

			adapter.BindCommand(New Command())
		End Sub

		Private Class MockAdapter
			Inherits CommandAdapter

			Public Overrides Sub AddInvoker(ByVal invoker As Object, ByVal eventName As String)
				Throw New Exception("The method or operation is not implemented.")
			End Sub

			Public Overrides Sub RemoveInvoker(ByVal invoker As Object, ByVal eventName As String)
				Throw New Exception("The method or operation is not implemented.")
			End Sub

			Public Overrides ReadOnly Property InvokerCount() As Integer
				Get
					Throw New Exception("The method or operation is not implemented.")
				End Get
			End Property

			Public Overrides Function ContainsInvoker(ByVal invoker As Object) As Boolean
				Throw New Exception("The method or operation is not implemented.")
			End Function

		End Class
	End Class
End Namespace
