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
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Threading
Imports Microsoft.Practices.CompositeUI
Imports Microsoft.Practices.CompositeUI.EventBroker
Imports Microsoft.Practices.CompositeUI.Utility

Namespace EventBrokerDemo
	<DesignerCategory("Code")> _
	Public Class CustomerListController : Inherits Controller
		' Declares a WorkItem scoped event that signals the starting of the background processing
		<EventPublication("topic://EventBrokerQuickStart/StartProcess", PublicationScope.WorkItem)> _
		Public Event StartProcess As EventHandler(Of DictionaryEventArgs)

		' Declares a WorkItem scoped event that signals the completion of the background processing
		<EventPublication("topic://EventBrokerQuickStart/ProcessCompleted", PublicationScope.WorkItem)> _
		Public Event ProcessCompleted As EventHandler

		' Declares a WorkItem scoped event that reports progress on the background processing
		<EventPublication("topic://EventBrokerQuickStart/ProgressChanged", PublicationScope.WorkItem)> _
		Public Event ProgressChanged As EventHandler(Of ProgressChangedEventArgs)

		' Declares a global scoped event that signals that a customer was added to the global list
		<EventPublication("topic://EventBrokerQuickStart/CustomerAdded")> _
		Public Event GlobalCustomerAdded As EventHandler(Of DataEventArgs(Of String))

		Private processing As Boolean = False
		Private cancelled As Boolean = False

		Private innerCustomers As List(Of String)

		<State("customers")> _
		Public WriteOnly Property Customers() As List(Of String)
			Set(ByVal value As List(Of String))
				innerCustomers = value
			End Set
		End Property

		' This method adds a customer to the local work item state.
		Public Sub AddLocalCustomer(ByVal customerId As String)
			innerCustomers.Add(customerId)
			State.RaiseStateChanged("customers", customerId)
		End Sub

		' This method just fires the GlobalCustomerAdded event, which is associated
		' to the "topic://EventBrokerQuickStart/CustomerAdded" event topic with
		' global scope.
		Public Sub AddGlobalCustomer(ByVal customerId As String)
			If Not GlobalCustomerAddedEvent Is Nothing Then
				RaiseEvent GlobalCustomerAdded(Me, New DataEventArgs(Of String)(customerId))
			End If
		End Sub

		' Starts the asynchronous processing of the customers added to the local work item.
		' The process simply moves one customer after anothe to the parent work item, which is
		' the application default work item.
		Public Sub ProcessLocalCustomers()
			' Only start the process if we're not processing
			' and if we have someone interested in doing the job 
			' (in this case, this very same class).
			If (Not processing) AndAlso Not StartProcessEvent Is Nothing Then
				processing = True
				cancelled = False

				' Prepare the arguments to the processor
				Dim args As DictionaryEventArgs = New DictionaryEventArgs()
				Dim toProcess As List(Of String) = New List(Of String)(innerCustomers)
				args.Data("customers") = toProcess

				' Fire the StartProcess event (meaning, fire the
				' "topic://EventBrokerQuickStart/StartProcess" topic.

				If Not StartProcessEvent Is Nothing Then
					RaiseEvent StartProcess(Me, args)
				End If

				' Clear the local customer list
				innerCustomers.Clear()

				' Raise the local state change event, so the UI can update itself
				State.RaiseStateChanged("customers", Nothing)

				' Create a progress view that consumes some progress events
				' and show that progress
				Me.WorkItem.Items.AddNew(Of ProgressView)().Show()
			End If
		End Sub

		' This is a background subscription that will be called in another thread.
		<EventSubscription("topic://EventBrokerQuickStart/StartProcess", Thread:=ThreadOption.Background)> _
		Public Sub StartProcessHandler(ByVal sender As Object, ByVal args As DictionaryEventArgs)
			' Get the lis of customers to process
			Dim customers As List(Of String) = DirectCast(args.Data("customers"), List(Of String))

			' Process the customer list
			Dim i As Integer = 0
			Do While i < customers.Count
				If cancelled Then
					Exit Do
				End If

				' Report progress, and along with it send the customer we're processing
				OnProgressChanged(CInt((i + 1) * (100 / customers.Count)), customers(i))

				' Simulate some time consuming proccess.
				Thread.Sleep(1000)
				i += 1
			Loop
			OnProcessCompleted()
		End Sub

		Private Sub OnProcessCompleted()
			If Not ProcessCompletedEvent Is Nothing Then
				RaiseEvent ProcessCompleted(Me, EventArgs.Empty)
			End If
		End Sub

		Private Sub OnProgressChanged(ByVal progress As Integer, ByVal data As String)
			If Not ProgressChangedEvent Is Nothing Then
				Dim args As ProgressChangedEventArgs = New ProgressChangedEventArgs(progress, data)
				RaiseEvent ProgressChanged(Me, args)
			End If
		End Sub

		'// Handles the ProgressChanged event
		<EventSubscription("topic://EventBrokerQuickStart/ProgressChanged", Thread:=ThreadOption.UserInterface)> _
		Public Sub ProgressChangedHandler(ByVal sender As Object, ByVal args As ProgressChangedEventArgs)
			Me.AddGlobalCustomer(CStr(args.UserState))
		End Sub


		' Gets the BackgroundWorker (if all) and send it a cancel message.
		' This method shows how to access the event catalog service and
		' how access an event topic and its background workers
		Public Sub CancelProcess()
			If processing Then
				cancelled = True
			End If
		End Sub

		'// Handles the ProcessCompleted event
		<EventSubscription("topic://EventBrokerQuickStart/ProcessCompleted", Thread:=ThreadOption.UserInterface)> _
		Public Sub ProcessCompletedHandler(ByVal sender As Object, ByVal args As EventArgs)
			cancelled = False
			processing = False
		End Sub

		' Here we publish our and our parent's state
		' to make easy for the view to perform the UI updates

		Public Shadows ReadOnly Property State() As State
			Get
				Return MyBase.State
			End Get
		End Property

		Public ReadOnly Property ParentState() As State
			Get
				Return Me.WorkItem.Parent.State
			End Get
		End Property

	End Class
End Namespace