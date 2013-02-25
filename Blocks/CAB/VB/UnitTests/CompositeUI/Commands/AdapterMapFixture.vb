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
Imports System.Collections.ObjectModel
Imports Microsoft.Practices.CompositeUI.Commands
Imports System.Windows.Forms

Namespace Tests.Commands
	<TestClass()> _
	Public Class AdapterMapFixture
		Private Shared container As WorkItem
		Private Shared mapSvc As ICommandAdapterMapService

		<TestInitialize()> _
		Public Sub Setup()
			container = New TestableRootWorkItem()
			mapSvc = container.Services.Get(Of ICommandAdapterMapService)(True)
		End Sub

		<TestMethod()> _
		Public Sub CanRegisterAnAdapter()
			mapSvc.Register(GetType(Object), GetType(MockAdapter))

			Dim ad As CommandAdapter = mapSvc.CreateAdapter(GetType(Object))

			Assert.AreSame(GetType(MockAdapter), ad.GetType())
		End Sub

		<TestMethod(), ExpectedException(GetType(AdapterMapServiceException))> _
		Public Sub ThrowsWhenRegisteringANonAdapter()
			mapSvc.Register(GetType(Object), GetType(Object))
		End Sub


		<TestMethod()> _
		Public Sub UnregisterAdapter()
			mapSvc.Register(GetType(Object), GetType(MockAdapter))
			mapSvc.UnRegister(GetType(Object))

			Dim ad As CommandAdapter = mapSvc.CreateAdapter(GetType(Object))

			Assert.IsNull(ad)
		End Sub

		<TestMethod()> _
		Public Sub AddingInvokerToCommandCreatesAdapter()
			mapSvc.Register(GetType(MockInvoker), GetType(MockAdapter))
			Dim cmd As Command = New Command()
			container.Items.Add(cmd)

			Dim inv As MockInvoker = New MockInvoker()
			cmd.AddInvoker(inv, "Event")

			Dim list As ReadOnlyCollection(Of MockAdapter) = cmd.FindAdapters(Of MockAdapter)()

			Assert.AreEqual(1, list.Count)
		End Sub

#Region "Utility class for AddedInvokerFiresTheCommand() and other tests"
		Private Class ExecuteAction
			Public called As Boolean
			Public Sub New(ByVal called As Boolean)
				Me.called = called
			End Sub
			Public Sub Execute(ByVal sender As Object, ByVal e As EventArgs)
				called = True
			End Sub
		End Class
#End Region

		<TestMethod()> _
		Public Sub AddedInvokerFiresTheCommand()
			mapSvc.Register(GetType(MockInvoker), GetType(MockAdapter))
			Dim cmd As Command = New Command()
			Dim called As Boolean = False
			Dim eAction As ExecuteAction = New ExecuteAction(called)
			AddHandler cmd.ExecuteAction, AddressOf eAction.Execute

			container.Items.Add(cmd)

			Dim invoker As MockInvoker = New MockInvoker()
			cmd.AddInvoker(invoker, "Event")

			invoker.DoInvoke()

			called = eAction.called
			Assert.IsTrue(called)
		End Sub

		<TestMethod()> _
		Public Sub RemovedInvokerDoesNotFiresTheCommand()
			mapSvc.Register(GetType(MockInvoker), GetType(MockAdapter))
			Dim cmd As Command = New Command()
			container.Items.Add(cmd)
			Dim called As Boolean = False
			Dim eAction As ExecuteAction = New ExecuteAction(called)
			AddHandler cmd.ExecuteAction, AddressOf eAction.Execute

			Dim invoker As MockInvoker = New MockInvoker()
			cmd.AddInvoker(invoker, "Event")
			cmd.RemoveInvoker(invoker, "Event")

			invoker.DoInvoke()

			called = eAction.called
			Assert.IsFalse(called)
		End Sub

		<TestMethod()> _
		Public Sub CanFindAdapterForAssignableType()
			mapSvc.Register(GetType(MockInvoker), GetType(MockAdapter))

			Dim adapter As CommandAdapter = mapSvc.CreateAdapter(GetType(AnotherMockInvoker))

			Assert.IsNotNull(adapter)
			Assert.AreEqual(GetType(MockAdapter), adapter.GetType())
		End Sub

		Private Class MockInvoker
			Public Event [Event] As EventHandler

			Public Sub DoInvoke()
				If Not EventEvent Is Nothing Then
					RaiseEvent Event(Me, EventArgs.Empty)
				End If
			End Sub
		End Class

		Private Class AnotherMockInvoker : Inherits MockInvoker
		End Class

		Private Class MockAdapter
			Inherits CommandAdapter

			Private invoker As MockInvoker

			Public Overrides Sub AddInvoker(ByVal invoker As Object, ByVal evenName As String)
				Me.invoker = CType(invoker, MockInvoker)
				AddHandler Me.invoker.Event, AddressOf InvokerEventHandler

			End Sub

			Public Overrides Sub RemoveInvoker(ByVal invoker As Object, ByVal eventName As String)
				If Me.invoker Is invoker Then
					RemoveHandler Me.invoker.Event, AddressOf InvokerEventHandler
				End If
			End Sub

			Private Sub InvokerEventHandler(ByVal sender As Object, ByVal e As EventArgs)
				MyBase.FireCommand()
			End Sub

			Public Overrides Function ContainsInvoker(ByVal invoker As Object) As Boolean
				Return True
			End Function

			Public Overrides ReadOnly Property InvokerCount() As Integer
				Get
					Return 0
				End Get
			End Property

			Public Sub New()

			End Sub

			Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)

			End Sub
		End Class
	End Class
End Namespace
