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
Imports System.Diagnostics

Namespace Instrumentation
	<TestClass()> _
	Public Class TraceSourceCatalogServiceFixture
		Private Shared catalog As TraceSourceCatalogService

		<TestInitialize()> _
		Public Sub SetUp()
			catalog = New TraceSourceCatalogService()
		End Sub

		<TestMethod()> _
		Public Sub GetTraceSourceCreatesNewOne()
			Dim ts As TraceSource = catalog.GetTraceSource("Foo")

			Assert.IsNotNull(ts)
			Assert.AreEqual("Foo", ts.Name)
		End Sub

		<TestMethod()> _
		Public Sub GetTraceSourceTwiceReturnsSame()
			Dim ts1 As TraceSource = catalog.GetTraceSource("Foo")
			Dim ts2 As TraceSource = catalog.GetTraceSource("Foo")

			Assert.AreSame(ts1, ts2)

		End Sub

#Region "Utilitary code for subprocedure AddingTraceFiresAddedEvent"

		Private Class AddingTraceFiresAddedEventUtilityProvider
			Private addedField As Boolean
			Public Property Added() As Boolean
				Get
					Return addedField
				End Get
				Set(ByVal addedArgument As Boolean)
					addedField = addedArgument
				End Set
			End Property

			Public Sub TraceSourceAddedHandler(ByVal sender As Object, ByVal e As Utility.DataEventArgs(Of System.Diagnostics.TraceSource))
				addedField = True
			End Sub
		End Class

		Private addingTraceFiresAddedEventUtility As AddingTraceFiresAddedEventUtilityProvider = New AddingTraceFiresAddedEventUtilityProvider

#End Region

		<TestMethod()> _
		Public Sub AddingTraceFiresAddedEvent()
			addingTraceFiresAddedEventUtility.Added = False
			AddHandler catalog.TraceSourceAdded, AddressOf addingTraceFiresAddedEventUtility.TraceSourceAddedHandler

			Dim ts As TraceSource = catalog.GetTraceSource("Foo")

			Assert.IsTrue(addingTraceFiresAddedEventUtility.Added)
		End Sub

		<TestMethod()> _
		Public Sub TraceSourcesCollectionExposesAddedTraceSource()
			Dim ts As TraceSource = catalog.GetTraceSource("Foo")

			Assert.AreEqual(1, catalog.TraceSources.Count)
		End Sub

		<ExpectedException(GetType(NotSupportedException)), TestMethod()> _
		Public Sub TraceSourcesCollectionIsReadOnly()
			catalog.TraceSources("Foo") = New TraceSource("Foo")
		End Sub

		<TestMethod()> _
		Public Sub TraceSourceContainsDefaultListenerIfNoConfigForSwitch()
			Dim source As TraceSource = New TraceSource("Foo")

			Assert.AreEqual(1, source.Listeners.Count)
			Assert.IsTrue(TypeOf source.Listeners(0) Is DefaultTraceListener)
		End Sub

		<TestMethod()> _
		Public Sub ServiceAddsTraceListenersToSource()
			Dim listener As ConsoleTraceListener = New ConsoleTraceListener()
			Trace.Listeners.Add(listener)

			Assert.IsTrue(FindListener("Foo", listener), "Listener from Trace.Listener was not added to source")
		End Sub

		<TestMethod()> _
		Public Sub AddingSharedTraceSourceIsAddedToAllSources()
			Dim source1 As TraceSource = catalog.GetTraceSource("Foo")
			Dim source2 As TraceSource = catalog.GetTraceSource("Bar")
			Dim listener As ConsoleTraceListener = New ConsoleTraceListener()

			catalog.AddSharedListener(listener)

			Assert.IsTrue(FindListener("Foo", listener))
			Assert.IsTrue(FindListener("Bar", listener))
		End Sub

		<TestMethod()> _
		Public Sub AddingSharedTraceSourceWithNameAddsToAllSources()
			Dim source1 As TraceSource = catalog.GetTraceSource("Foo")
			Dim source2 As TraceSource = catalog.GetTraceSource("Bar")
			Dim listener As ConsoleTraceListener = New ConsoleTraceListener()

			catalog.AddSharedListener(listener, "Test")

			Assert.IsNotNull(source1.Listeners("Test"))
			Assert.IsNotNull(source2.Listeners("Test"))
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub AddingSharedListenerWithNullThrows()
			catalog.AddSharedListener(Nothing)
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub AddingSharedListenerWithNameNullListenerThrows()
			catalog.AddSharedListener(Nothing, "Test")
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub AddingSharedListenerWithNameNullNameThrows()
			catalog.AddSharedListener(New ConsoleTraceListener(), Nothing)
		End Sub

		Private Function FindListener(ByVal sourceName As String, ByVal listener As TraceListener) As Boolean
			Dim result As Boolean = False

			Dim ts As TraceSource = catalog.GetTraceSource("Bar")
			For Each l As TraceListener In ts.Listeners
				If l Is listener Then
					result = True
					Exit For
				End If
			Next l

			Return result
		End Function
	End Class
End Namespace
