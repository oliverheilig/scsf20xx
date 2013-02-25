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
Imports Microsoft.Practices.CompositeUI.Commands
Imports System.Windows.Forms

Namespace Tests.Commands
	<TestClass()> _
	Public Class EventCommandAdapterFixture
		<TestMethod()> _
		Public Sub CanCreateAdapterWithOneInvoker()
			Dim invoker As MockInvoker = New MockInvoker()
			Dim adapter As EventCommandAdapter(Of MockInvoker) = New EventCommandAdapter(Of MockInvoker)(invoker, "Event")

			Assert.AreEqual(1, adapter.Invokers.Count)
			Assert.AreSame("Event", adapter.Invokers(invoker)(0))
		End Sub

		<TestMethod()> _
		Public Sub CanAddInvoker()
			Dim invoker As MockInvoker = New MockInvoker()
			Dim adapter As EventCommandAdapter(Of MockInvoker) = New EventCommandAdapter(Of MockInvoker)(invoker, "Event")

			Dim invoker2 As MockInvoker = New MockInvoker()
			adapter.AddInvoker(invoker2, "Event")

			Assert.AreEqual(2, adapter.Invokers.Count)
		End Sub


		<TestMethod()> _
		Public Sub CanRemoveInvoker()
			Dim invoker As MockInvoker = New MockInvoker()
			Dim adapter As EventCommandAdapter(Of MockInvoker) = New EventCommandAdapter(Of MockInvoker)(invoker, "Event")
			Dim invoker2 As MockInvoker = New MockInvoker()
			adapter.AddInvoker(invoker2, "Event")

			adapter.RemoveInvoker(invoker2, "Event")

			Assert.AreEqual(1, adapter.Invokers.Count)
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentException))> _
		Public Sub CreatingWithWrongTypeThrows()
			Dim invoker As ToolStripMenuItem = New ToolStripMenuItem()
			Dim adapter As EventCommandAdapter(Of MockInvoker) = New EventCommandAdapter(Of MockInvoker)()
			adapter.AddInvoker(invoker, "Click")
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentException))> _
		Public Sub AddingWrongTypeInvokerThrows()
			Dim invoker As MockInvoker = New MockInvoker()
			Dim adapter As EventCommandAdapter(Of MockInvoker) = New EventCommandAdapter(Of MockInvoker)(invoker, "Event")

			adapter.AddInvoker(New ToolStripMenuItem(), "Click")
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentException))> _
		Public Sub RemovingWrongTypeInvokerThrows()
			Dim invoker As MockInvoker = New MockInvoker()
			Dim adapter As EventCommandAdapter(Of MockInvoker) = New EventCommandAdapter(Of MockInvoker)(invoker, "Event")
			Dim invoker2 As ToolStripMenuItem = New ToolStripMenuItem()

			adapter.RemoveInvoker(invoker2, "Event")
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub CreateingWithNullInvokerThrows()
			Dim invoker As MockInvoker = New MockInvoker()
			Dim adapter As EventCommandAdapter(Of MockInvoker) = New EventCommandAdapter(Of MockInvoker)(Nothing, "Event")
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub CreatingWithNullEventThrows()
			Dim invoker As MockInvoker = New MockInvoker()
			Dim adapter As EventCommandAdapter(Of MockInvoker) = New EventCommandAdapter(Of MockInvoker)(invoker, Nothing)
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub AddingWithNullInvokerThrows()
			Dim invoker As MockInvoker = New MockInvoker()
			Dim adapter As EventCommandAdapter(Of MockInvoker) = New EventCommandAdapter(Of MockInvoker)(invoker, "Event")
			adapter.AddInvoker(Nothing, "Event")
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub AddingWithNullEventThrows()
			Dim invoker As MockInvoker = New MockInvoker()
			Dim adapter As EventCommandAdapter(Of MockInvoker) = New EventCommandAdapter(Of MockInvoker)(invoker, "Event")
			adapter.AddInvoker(New MockInvoker(), Nothing)
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub RemovingWithNullInvokerThrows()
			Dim invoker As MockInvoker = New MockInvoker()
			Dim adapter As EventCommandAdapter(Of MockInvoker) = New EventCommandAdapter(Of MockInvoker)(invoker, "Event")
			adapter.RemoveInvoker(Nothing, "Event")
		End Sub

		<TestMethod()> _
		Public Sub RemovingNonRegisteredInvokerNoOps()
			Dim invoker As MockInvoker = New MockInvoker()
			Dim adapter As EventCommandAdapter(Of MockInvoker) = New EventCommandAdapter(Of MockInvoker)(invoker, "Event")
			adapter.RemoveInvoker(New MockInvoker(), "Event")

			Assert.AreEqual(1, adapter.Invokers.Count)
		End Sub


		<TestMethod()> _
		Public Sub InvokerIsWiredUp()
			Dim command As Command = New Command()
			Dim invoker As MockInvoker = New MockInvoker()
			Dim adapter As EventCommandAdapter(Of MockInvoker) = New EventCommandAdapter(Of MockInvoker)(invoker, "Event")
			command.AddCommandAdapter(adapter)
			Dim listener As MockListener = New MockListener()
			AddHandler command.ExecuteAction, AddressOf listener.CatchCommand

			invoker.DoInvokeEvent()

			Assert.IsTrue(listener.CommandFired)
		End Sub

		<TestMethod()> _
		Public Sub InvokerIsUnwired()
			Dim command As Command = New Command()
			Dim invoker As MockInvoker = New MockInvoker()
			Dim adapter As EventCommandAdapter(Of MockInvoker) = New EventCommandAdapter(Of MockInvoker)(invoker, "Event")
			command.AddCommandAdapter(adapter)
			Dim listener As MockListener = New MockListener()
			AddHandler command.ExecuteAction, AddressOf listener.CatchCommand

			adapter.RemoveInvoker(invoker, "Event")

			invoker.DoInvokeEvent()

			Assert.IsFalse(listener.CommandFired)
		End Sub

		<TestMethod()> _
		Public Sub CanAddMoreThanOneEventToAnInvokerObject()
			Dim command As Command = New Command()

			Dim invoker As MockInvoker = New MockInvoker()
			Dim adapter As EventCommandAdapter(Of MockInvoker) = New EventCommandAdapter(Of MockInvoker)(invoker, "Event")
			adapter.AddInvoker(invoker, "Event2")

			Assert.IsTrue(invoker.EventIsHooked)
			Assert.IsTrue(invoker.Event2IsHooked)
		End Sub

		<TestMethod()> _
		Public Sub CanRemoveOneInvokerEventFromAdapter()
			Dim invoker As MockInvoker = New MockInvoker()
			Dim adapter As EventCommandAdapter(Of MockInvoker) = New EventCommandAdapter(Of MockInvoker)()

			adapter.AddInvoker(invoker, "Event")
			adapter.AddInvoker(invoker, "Event2")

			adapter.RemoveInvoker(invoker, "Event2")

			Assert.IsTrue(adapter.Invokers(invoker).Contains("Event"))
			Assert.IsFalse(adapter.Invokers(invoker).Contains("Event2"))
		End Sub

		<TestMethod()> _
		Public Sub CanTestIfInvokerIsContained()
			Dim invoker As MockInvoker = New MockInvoker()
			Dim adapter As EventCommandAdapter(Of MockInvoker) = New EventCommandAdapter(Of MockInvoker)(invoker, "Event")
			Dim invokerB As MockInvokerB = New MockInvokerB()
			Dim adapterB As EventCommandAdapter(Of MockInvokerB) = New EventCommandAdapter(Of MockInvokerB)(invokerB, "Event")

			Assert.IsTrue(adapter.ContainsInvoker(invoker))
			Assert.IsTrue(adapterB.ContainsInvoker(invokerB))
			Assert.IsFalse(adapter.ContainsInvoker(invokerB))
			Assert.IsFalse(adapterB.ContainsInvoker(invoker))
		End Sub

		<TestMethod()> _
		Public Sub DisposeUnwiresAllInvokers()
			Dim adapter As EventCommandAdapter(Of MockInvoker) = New EventCommandAdapter(Of MockInvoker)()
			Dim invoker As MockInvoker = New MockInvoker()
			adapter.AddInvoker(invoker, "Event")
			Dim invokerB As MockInvoker = New MockInvoker()
			adapter.AddInvoker(invokerB, "Event")
			adapter.AddInvoker(invokerB, "Event2")

			adapter.Dispose()

			Assert.AreEqual(0, adapter.Invokers.Count)
		End Sub

		<TestMethod(), ExpectedException(GetType(CommandException))> _
		Public Sub StaticInvokerThrows()
			Dim adapter As EventCommandAdapter(Of MockStaticInvoker) = New EventCommandAdapter(Of MockStaticInvoker)()
			Dim invoker As MockStaticInvoker = New MockStaticInvoker()
			adapter.AddInvoker(invoker, "Event")
		End Sub

		Private Class MockListener
			Public CommandFired As Boolean = False

			Public Sub CatchCommand(ByVal sender As Object, ByVal args As EventArgs)
				CommandFired = True
			End Sub
		End Class

		Private Class MockStaticInvoker
			Public Shared Event [Event] As EventHandler

			Public Shared Sub DoInvokeEvent()
				If Not EventEvent Is Nothing Then
					RaiseEvent [Event](Nothing, EventArgs.Empty)
				End If
			End Sub
		End Class

		Private Class MockInvokerB
			Public Event [Event] As EventHandler

			Public Sub DoInvokeEvent()
				If Not EventEvent Is Nothing Then
					RaiseEvent Event(Me, EventArgs.Empty)
				End If
			End Sub
		End Class

		Private Class MockInvoker
			Public Event [Event] As EventHandler
			Public Event Event2 As EventHandler

			Public Sub DoInvokeEvent()
				If Not EventEvent Is Nothing Then
					RaiseEvent Event(Me, EventArgs.Empty)
				End If
			End Sub

			Public Sub DoInvokeEvent2()
				If Not Event2Event Is Nothing Then
					RaiseEvent Event2(Me, EventArgs.Empty)
				End If
			End Sub

			Public ReadOnly Property EventIsHooked() As Boolean
				Get
					Return Not EventEvent Is Nothing
				End Get
			End Property

			Public ReadOnly Property Event2IsHooked() As Boolean
				Get
					Return Not Event2Event Is Nothing
				End Get
			End Property
		End Class

	End Class
End Namespace
