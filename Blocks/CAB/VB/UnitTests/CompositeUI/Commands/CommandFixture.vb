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
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Diagnostics
Imports Microsoft.Practices.CompositeUI.Utility

Namespace Tests.Commands
	<TestClass()> _
	Public Class CommandFixture
		Private Shared command As Command
		Private Shared executed As Boolean

		<TestInitialize()> _
		Public Sub SetUp()
			command = New Command()
			executed = False
		End Sub

		Private Shared Sub ActionHandler(ByVal sender As Object, ByVal e As EventArgs)
			executed = True
		End Sub

		<TestMethod()> _
		Public Sub CommandIsNotNull()
			Assert.IsNotNull(command, "The command was not created.")
		End Sub

		<TestMethod()> _
		Public Sub CommandExposesExecuteHandler()
			AddHandler command.ExecuteAction, AddressOf ActionHandler
			command.Execute()

			Assert.IsTrue(executed)
		End Sub

		<TestMethod()> _
		Public Sub CanRegisterAdapter()
			Dim adapter As MockAdapter = New MockAdapter()
			command.AddCommandAdapter(adapter)

			Assert.AreEqual(1, command.Adapters.Count)
			Assert.AreSame(adapter, command.Adapters(0))
		End Sub

		<TestMethod()> _
		Public Sub CanRemoveAdapter()
			Dim adapter As MockAdapter = New MockAdapter()
			command.AddCommandAdapter(adapter)
			command.RemoveCommandAdapter(adapter)

			Assert.AreEqual(0, command.Adapters.Count)
			Assert.IsFalse(command.Adapters.Contains(adapter))
		End Sub

		<TestMethod()> _
		Public Sub AdapterIsNotifiedAboutCommandChanges()
			Dim adapter As MockAdapter = New MockAdapter()
			command.AddCommandAdapter(adapter)
			Assert.AreEqual(0, adapter.OnChangedCalled)

			command.Status = CommandStatus.Disabled

			Assert.AreEqual(1, adapter.OnChangedCalled)
		End Sub

#Region "Utility subroutine for CommandIsTheSender() and other tests"
		Public Sub CheckCommandIsTheSender(ByVal sender As Object, ByVal e As EventArgs)
			Assert.AreSame(command, sender)
		End Sub
#End Region

		<TestMethod()> _
		Public Sub CommandIsTheSender()
			AddHandler command.ExecuteAction, AddressOf CheckCommandIsTheSender
			Dim adapter As MockAdapter = New MockAdapter()
			command.AddCommandAdapter(adapter)
			adapter.Fire()
		End Sub

		<TestMethod()> _
		Public Sub CanRemoveInvokerWithMultipleAdapters()
			Dim command As Command = New Command()
			Dim invokerA As MockInvokerA = New MockInvokerA()
			Dim invokerB As MockInvokerB = New MockInvokerB()
			Dim adapterA As EventCommandAdapter(Of MockInvokerA) = New EventCommandAdapter(Of MockInvokerA)(invokerA, "Event")
			Dim adapterB As EventCommandAdapter(Of MockInvokerB) = New EventCommandAdapter(Of MockInvokerB)(invokerB, "Event")

			command.AddCommandAdapter(adapterA)
			command.AddCommandAdapter(adapterB)

			command.RemoveInvoker(invokerA, "Event")

			Assert.AreEqual(1, adapterB.Invokers.Count)
			Assert.AreEqual(0, adapterA.Invokers.Count)
		End Sub

		<TestMethod()> _
		Public Sub RemovingAllInvokersFromAdapterRemovesAdapterFromCommand()
			Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
			Dim svc As ICommandAdapterMapService = workItem.Services.Get(Of ICommandAdapterMapService)()
			svc.Register(GetType(MockInvokerA), GetType(MockAdapter))

			Dim command As Command = New Command()
			workItem.Commands.Add(command)

			Dim invoker As MockInvokerA = New MockInvokerA()
			command.AddInvoker(invoker, "Event")
			Assert.AreEqual(1, command.Adapters.Count)

			command.RemoveInvoker(invoker, "Event")
			Assert.AreEqual(0, command.Adapters.Count)
		End Sub


		<TestMethod()> _
		Public Sub RemovingCommandFromWorkItemUnWiresTheCommandHandler()
			Dim workItem As TestableRootWorkItem = New TestableRootWorkItem()
			Assert.AreEqual(0, workItem.Commands.Count)

			Dim handler As MockHandler = workItem.Items.AddNew(Of MockHandler)()
			Assert.AreEqual(1, workItem.Commands.Count)

			Dim command As Command = workItem.Commands.Get("TestCommand")
			Assert.IsNotNull(command)

			workItem.Items.Remove(handler)

			command.Execute()
			Assert.IsFalse(handler.TestCommandHandlerCalled)
		End Sub

#Region "Utility class for EnabledCommandFiresExecutedEvent() and other tests"
		Private Class ExecuteAction
			Public executed As Boolean
			Public Sub New(ByVal executed As Boolean)
				Me.executed = executed
			End Sub
			Public Sub Execute(ByVal sender As Object, ByVal e As EventArgs)
				executed = True
			End Sub
		End Class
#End Region

		<TestMethod()> _
		Public Sub EnabledCommandFiresExecutedEvent()
			Dim executed As Boolean = False
			Dim executeAction As ExecuteAction = New ExecuteAction(executed)
			AddHandler command.ExecuteAction, AddressOf executeAction.Execute

			command.Status = CommandStatus.Enabled
			command.Execute()

			executed = executeAction.executed
			Assert.IsTrue(executed)
		End Sub

		<TestMethod()> _
		Public Sub DisabledCommandDoesNotFireExecutedEvent()
			Dim executed As Boolean = False
			Dim executeAction As ExecuteAction = New ExecuteAction(executed)
			AddHandler command.ExecuteAction, AddressOf executeAction.Execute

			command.Status = CommandStatus.Disabled
			command.Execute()

			executed = executeAction.executed
			Assert.IsFalse(executed)
		End Sub

		<TestMethod()> _
		Public Sub UnavailableCommandDoesNotFireExecutedEvent()
			Dim executed As Boolean = False
			Dim executeAction As ExecuteAction = New ExecuteAction(executed)
			AddHandler command.ExecuteAction, AddressOf executeAction.Execute

			command.Status = CommandStatus.Unavailable
			command.Execute()

			executed = executeAction.executed
			Assert.IsFalse(executed)
		End Sub

		<TestMethod()> _
		Public Sub InvokerDoesNotCauseExecuteOnDisabledCommand()
			Dim invoker As MockInvokerA = New MockInvokerA()
			Dim executed As Boolean = False
			Dim executeAction As ExecuteAction = New ExecuteAction(executed)
			AddHandler command.ExecuteAction, AddressOf executeAction.Execute

			Dim adapter As EventCommandAdapter(Of MockInvokerA) = New EventCommandAdapter(Of MockInvokerA)(invoker, "Event")
			command.AddCommandAdapter(adapter)

			command.Status = CommandStatus.Disabled
			invoker.DoInvokeEvent()

			executed = executeAction.executed
			Assert.IsFalse(executed)
		End Sub

		<TestMethod()> _
		Public Sub InvokerDoesNotCauseExecuteOnUnavailableCommand()
			Dim invoker As MockInvokerA = New MockInvokerA()
			Dim executed As Boolean = False
			Dim executeAction As ExecuteAction = New ExecuteAction(executed)
			AddHandler command.ExecuteAction, AddressOf executeAction.Execute

			Dim adapter As EventCommandAdapter(Of MockInvokerA) = New EventCommandAdapter(Of MockInvokerA)(invoker, "Event")
			command.AddCommandAdapter(adapter)

			command.Status = CommandStatus.Unavailable
			invoker.DoInvokeEvent()

			executed = ExecuteAction.executed
			Assert.IsFalse(executed)
		End Sub


		<TestMethod()> _
		Public Sub AddingCommandWithNameCreatesCommandWithName()
			Dim workItem As WorkItem = New TestableRootWorkItem()
			Dim cmd As Command = workItem.Commands("TestCommand")
		End Sub


		<TestMethod()> _
		Public Sub CommandAddedWithANameGetsItsNameThroughBuilder()
			Dim workItem As WorkItem = New TestableRootWorkItem()
			Dim cmd As Command = workItem.Commands.AddNew(Of Command)("TestCommand")

			Assert.AreEqual("TestCommand", cmd.Name)
		End Sub

		<TestMethod()> _
		Public Sub DisposingCommandRemovesAllOfItsAdapters()
			Dim wi As WorkItem = New TestableRootWorkItem()
			Dim svc As ICommandAdapterMapService = wi.Services.Get(Of ICommandAdapterMapService)()
			svc.Register(GetType(MockInvokerA), GetType(MockAdapter))
			Dim cmd As Command = wi.Commands.AddNew(Of Command)()
			cmd.AddInvoker(New MockInvokerA(), "Event")
			cmd.AddInvoker(New MockInvokerA(), "Event")
			cmd.AddInvoker(New MockInvokerA(), "Event")
			cmd.AddInvoker(New MockInvokerA(), "Event")

			Assert.AreEqual(4, cmd.Adapters.Count)
			cmd.Dispose()
			Assert.AreEqual(0, cmd.Adapters.Count)
		End Sub

		<TestMethod()> _
		Public Sub StatusDefaultsToEnabled()
			Assert.AreEqual(CommandStatus.Enabled, command.Status)
		End Sub

		<TestMethod()> _
		Public Sub ChangedEventFiredWhenStatusChanges()
			Dim changedFired As Boolean = False
			Dim executeAction As ExecuteAction = New ExecuteAction(changedFired)
			AddHandler command.Changed, AddressOf executeAction.Execute

			command.Status = CommandStatus.Disabled

			changedFired = executeAction.executed
			Assert.IsTrue(changedFired)
		End Sub

		<TestMethod()> _
		Public Sub ChangedEventDoesNotFireIfAssignedStatusIsSame()
			Dim changedFired As Boolean = False
			Dim executeAction As ExecuteAction = New ExecuteAction(changedFired)
			AddHandler command.Changed, AddressOf executeAction.Execute

			command.Status = CommandStatus.Enabled

			changedFired = executeAction.executed
			Assert.IsFalse(changedFired)
		End Sub

		<TestMethod()> _
		Public Sub AdapterCratedByCommandIsDisposedWithCommand()
			Dim wi As WorkItem = New TestableRootWorkItem()
			Dim svc As ICommandAdapterMapService = wi.Services.Get(Of ICommandAdapterMapService)()
			svc.Register(GetType(Control), GetType(MockControlAdapter))

			Dim cmd As Command = wi.Commands.AddNew(Of Command)()

			Dim invoker As Control = New Control()
			cmd.AddInvoker(invoker, "GotFocus")

			Dim adapter As MockControlAdapter = DirectCast(cmd.Adapters(0), MockControlAdapter)
			cmd.Dispose()
			Assert.IsTrue(adapter.IsDisposed)
		End Sub

		<TestMethod()> _
		Public Sub CommandDoesNotReuseAdpatersOnAddInvoker()
			Dim wi As WorkItem = New TestableRootWorkItem()
			Dim svc As ICommandAdapterMapService = wi.Services.Get(Of ICommandAdapterMapService)()
			svc.Register(GetType(Control), GetType(MockControlAdapter))

			Dim cmd As Command = wi.Commands.AddNew(Of Command)()

			Dim invoker As Control = New Control()
			cmd.AddInvoker(invoker, "GotFocus")
			cmd.AddInvoker(invoker, "Click")

			Assert.AreEqual(2, cmd.Adapters.Count)
		End Sub

		<TestMethod()> _
		Public Sub AdapterCreatedByCommandIsDisposedWhenInvokerRemoved()
			Dim wi As WorkItem = New TestableRootWorkItem()
			Dim svc As ICommandAdapterMapService = wi.Services.Get(Of ICommandAdapterMapService)()
			svc.Register(GetType(Control), GetType(MockControlAdapter))

			Dim cmd As Command = wi.Commands.AddNew(Of Command)()

			Dim invoker As Control = New Control()
			cmd.AddInvoker(invoker, "GotFocus")

			Dim adapter As MockControlAdapter = DirectCast(cmd.Adapters(0), MockControlAdapter)

			cmd.RemoveInvoker(invoker, "GotFocus")

			Assert.IsTrue(adapter.IsDisposed)
		End Sub

		<TestMethod()> _
		Public Sub AdapterCreatedByCommandIsRemovedWhenInvokerRemoved()

			Dim wi As WorkItem = New TestableRootWorkItem()
			Dim svc As ICommandAdapterMapService = wi.Services.Get(Of ICommandAdapterMapService)()
			svc.Register(GetType(Control), GetType(MockControlAdapter))

			Dim cmd As Command = wi.Commands.AddNew(Of Command)()

			Dim invoker As Control = New Control()
			cmd.AddInvoker(invoker, "GotFocus")

			Dim adapter As MockControlAdapter = DirectCast(cmd.Adapters(0), MockControlAdapter)

			cmd.RemoveInvoker(invoker, "GotFocus")

			adapter.IsDisposed = False

			cmd.Dispose()
			' Should not be disposed again as it shouldn't be contained at all in the command anymore.
			Assert.IsFalse(adapter.IsDisposed)
		End Sub

		<TestMethod()> _
		Public Sub AdapterCreatedByCommandIsRemovedWhenLastInvokerRemoved()
			Dim wi As WorkItem = New TestableRootWorkItem()
			Dim svc As ICommandAdapterMapService = wi.Services.Get(Of ICommandAdapterMapService)()
			svc.Register(GetType(Control), GetType(MockControlAdapter))

			Dim cmd As Command = wi.Commands.AddNew(Of Command)()

			Dim invoker As Control = New Control()
			cmd.AddInvoker(invoker, "GotFocus")

			Dim adapter As MockControlAdapter = DirectCast(cmd.Adapters(0), MockControlAdapter)
			adapter.AddInvoker(invoker, "Click")

			cmd.RemoveInvoker(invoker, "GotFocus")

			Assert.IsFalse(adapter.IsDisposed)

			cmd.RemoveInvoker(invoker, "Click")

			Assert.IsTrue(adapter.IsDisposed)

			adapter.IsDisposed = False

			cmd.Dispose()
			' Should not be disposed again as it shouldn't be contained at all in the command anymore.
			Assert.IsFalse(adapter.IsDisposed)
		End Sub

		<TestMethod()> _
		Public Sub UserAddedAdapterIsNotDisposedWithCommand()

			Dim invoker As Control = New Control()
			Dim adapter As MockControlAdapter = New MockControlAdapter(invoker, "GotFocus")

			command.AddCommandAdapter(adapter)

			command.Dispose()

			Assert.IsFalse(adapter.IsDisposed)
		End Sub

#Region "Utility code for AddingCommandToCommandsCollectionFiresAddeddEvent"

		Private Class AddingCommandToCommandsCollectionFiresAddeddEventUtility
			Public AddedCalled As Boolean
			Public Sub New(ByVal addedCalled As Boolean)
				Me.AddedCalled = addedCalled
			End Sub
			Public Sub EventHandler(ByVal sender As Object, ByVal args As DataEventArgs(Of Command))
				AddedCalled = True
			End Sub
		End Class

#End Region

		<TestMethod()> _
		Public Sub AddingCommandToCommandsCollectionFiresAddeddEvent()
			Dim item As WorkItem = New TestableRootWorkItem()
			Dim utilityInstance As AddingCommandToCommandsCollectionFiresAddeddEventUtility = _
				New AddingCommandToCommandsCollectionFiresAddeddEventUtility(False)

			AddHandler item.Commands.Added, AddressOf utilityInstance.eventhandler
			Dim cmd As Command = item.Commands.AddNew(Of Command)()

			Assert.IsTrue(utilityInstance.AddedCalled)
		End Sub

		<TestMethod()> _
		<ExpectedException(GetType(ArgumentException))> _
		Public Sub RemovingCommandFromItemsCollectionThrows()
			Dim item As WorkItem = New TestableRootWorkItem()
			Dim cmd As Command = item.Commands.AddNew(Of Command)()
			item.Items.Remove(cmd)
		End Sub

		<TestMethod()> _
		<ExpectedException(GetType(ArgumentException))> _
		Public Sub RemovingCommandFromCommandsCollectionThrows()
			Dim item As WorkItem = New TestableRootWorkItem()
			Dim cmd As Command = item.Commands.AddNew(Of Command)()
			item.Commands.Remove(cmd)
		End Sub

		<TestMethod()> _
		Public Sub AddingCommandSetsItsName()
			Dim wi As TestableRootWorkItem = New TestableRootWorkItem()
			Dim cmd As Command = wi.Commands("TestCommand1")
			Assert.AreEqual("TestCommand1", cmd.Name)

			cmd = wi.Commands.AddNew(Of Command)("TestCommand2")
			Assert.AreEqual("TestCommand2", cmd.Name)
		End Sub

#Region "Helper classes"

		Private Class MockCommand : Inherits Command
			Public Function GetTraceSource() As TraceSource
				Return MyBase.TraceSource
			End Function
		End Class

		Private Class InstanceHandlerClass
			Public HandlerCalled As Boolean = False
			Public counter As Integer = 0

			<CommandHandler("TestCommand")> _
			Public Sub InstanceHandler(ByVal sender As Object, ByVal e As EventArgs)
				HandlerCalled = True
				counter += 1
			End Sub
		End Class

		Private Class MockHandler
			Public TestCommandHandlerCalled As Boolean = False

			<CommandHandler("TestCommand")> _
			Public Sub TestCommandHandler(ByVal sender As Object, ByVal e As EventArgs)
				TestCommandHandlerCalled = True
			End Sub
		End Class

		Private Class MockInvokerA
			Public Event [Event] As EventHandler

			Public Sub DoInvokeEvent()
				If Not EventEvent Is Nothing Then
					RaiseEvent Event(Me, EventArgs.Empty)
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

		Private Class MockAdapter
			Inherits CommandAdapter

			Public DisposeCalled As Boolean = False

			Public Sub New()
			End Sub

			Public Sub Fire()
				MyBase.FireCommand()
			End Sub

			Public OnChangedCalled As Integer = 0

			Protected Overrides Sub OnCommandChanged(ByVal command As Command)
				OnChangedCalled += 1
			End Sub

			Public AddInvokerCalled As Integer = 0
			Public Overrides Sub AddInvoker(ByVal invoker As Object, ByVal evenName As String)
				AddInvokerCalled += 1
			End Sub

			Public Overrides Sub RemoveInvoker(ByVal invoker As Object, ByVal eventName As String)
				AddInvokerCalled -= 1
			End Sub

			Public Overrides Function ContainsInvoker(ByVal invoker As Object) As Boolean
				Return True
			End Function

			Public Overrides ReadOnly Property InvokerCount() As Integer
				Get
					Return AddInvokerCalled
				End Get
			End Property

			Public Overloads Sub Dispose()
				DisposeCalled = True
			End Sub
		End Class

		Class MockControlAdapter
			Inherits EventCommandAdapter(Of Control)

			Public IsDisposed As Boolean

			Public Sub New()
				MyBase.new()
			End Sub

			Public Sub New(ByVal invoker As Control, ByVal eventName As String)
				MyBase.New(invoker, eventName)
			End Sub

			Protected Overrides Sub Dispose(ByVal disposing As Boolean)
				MyBase.Dispose(disposing)
				IsDisposed = True
			End Sub
		End Class

#End Region

	End Class
End Namespace
