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
Imports System.Collections.Specialized
Imports System.Runtime.Serialization
Imports System.Security.Permissions
Imports Microsoft.Practices.CompositeUI.Utility
Imports System.Threading


''' <summary>
''' Provides a dictionary of information which provides notification when items change
''' in the collection.
''' </summary>
''' <remarks>
''' Strongly-typed derived elements have access to the protected indexer on the class, 
''' but clients of these classes will have to forcedly use the getters/setters, thus 
''' the derived class is always in control of its state.
''' </remarks>
<Serializable()> _
Public Class StateElement
	Inherits NameObjectCollectionBase
	Implements IChangeNotification
	Implements IDisposable

	Friend syncRoot As ReaderWriterLock = New ReaderWriterLock()
	Private valueIndexed As IDictionary(Of Object, String) = New Dictionary(Of Object, String)()

	''' <summary>
	''' This event is raised when the state has changed.
	''' </summary>
	Public Event StateChanged As EventHandler(Of StateChangedEventArgs)

	''' <summary>
	''' Initializes a new instance of the <see cref="StateElement"/> class.
	''' </summary>
	Public Sub New()
	End Sub

	''' <summary>
	''' Initializes a new instance of the <see cref="StateElement"/> class using the provided
	''' serialization information.
	''' </summary>
	''' <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo"/> to populate with data.</param>
	''' <param name="context">The destination (see <see cref="System.Runtime.Serialization.StreamingContext"/>) for this serialization. </param>
	Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
		MyBase.New(info, context)
	End Sub

	''' <summary>
	''' Gets and sets values on the state.
	''' </summary>
	''' <remarks>
	''' Derived classes must use this accessor in order to get the <see cref="StateChanged"/> fired. 
	''' This is made protected to force that pattern for strongly typed models, and forbid access to 
	''' the generic dictionary in those cases (unless the developer explicitly exposes a public 
	''' indexer as the <see cref="State"/> class does).
	''' </remarks>
	Default Protected Property Item(ByVal key As String) As Object
		Get
			Guard.ArgumentNotNull(key, "key")

			Using oTemp As ReaderLock = New ReaderLock(syncRoot)
				Return BaseGet(key)
			End Using
		End Get
		Set(ByVal value As Object)
			Guard.ArgumentNotNull(key, "key")

			Using oTemp As WriterLock = New WriterLock(syncRoot)
				' Remove old value from valueIndexed
				Dim oldvalue As Object = BaseGet(key)
				If Not oldvalue Is Nothing Then
					Remove(key)
				End If

				BaseSet(key, value)

				If Not value Is Nothing Then
					valueIndexed(value) = key
				End If

				RaiseStateChanged(key, value, oldvalue)

				If Not value Is Nothing Then
					HookChangeNotification(TryCast(value, IChangeNotification))
				End If
			End Using
		End Set
	End Property

	Private Sub HookChangeNotification(ByVal element As IChangeNotification)
		If Not element Is Nothing Then
			' Unhook first to avoid double-hooking if the same 
			' element is passed twice.
			RemoveHandler element.Changed, AddressOf ChildChanged
			AddHandler element.Changed, AddressOf ChildChanged
		End If
	End Sub

	Private Sub UnHookChangeNotification(ByVal element As IChangeNotification)
		If Not element Is Nothing Then
			RemoveHandler element.Changed, AddressOf ChildChanged
		End If
	End Sub

	Private Sub ChildChanged(ByVal sender As Object, ByVal e As EventArgs)
		If valueIndexed.ContainsKey(sender) Then
			RaiseStateChanged(valueIndexed(sender), sender)
		End If
	End Sub

	''' <summary>
	''' Removes the item with the given key from the state.
	''' </summary>
	Public Sub Remove(ByVal key As String)
		Guard.ArgumentNotNull(key, "key")

		Using New WriterLock(syncRoot)
			Dim value As Object = BaseGet(key)
			UnHookChangeNotification(TryCast(value, IChangeNotification))
			BaseRemove(key)

			If Not value Is Nothing Then
				valueIndexed.Remove(value)
			End If
		End Using
	End Sub

	''' <summary>
	''' Raises the <see cref="StateChanged"/> event for the given state key 
	''' (can be null if it is unknown).
	''' </summary>
	Public Sub RaiseStateChanged(ByVal key As String, ByVal newValue As Object, ByVal oldValue As Object)
		OnStateChanged(New StateChangedEventArgs(key, newValue, oldValue))
	End Sub

	''' <summary>
	''' Raises the <see cref="StateChanged"/> event for the given state key 
	''' (can be null if it is unknown).
	''' </summary>
	Public Sub RaiseStateChanged(ByVal key As String, ByVal newValue As Object)
		RaiseStateChanged(key, newValue, Nothing)
	End Sub

	''' <summary>
	''' Raises the <see cref="StateChanged"/> event.
	''' </summary>
	Protected Overridable Sub OnStateChanged(ByVal e As StateChangedEventArgs)
		Dim stateHandler As EventHandler(Of StateChangedEventArgs) = StateChangedEvent
		If Not stateHandler Is Nothing Then
			stateHandler(Me, e)
		End If

		Dim changedHandler As EventHandler = CType(handlers(changedKey), EventHandler)
		If Not changedHandler Is Nothing Then
			changedHandler(Me, EventArgs.Empty)
		End If
	End Sub

#Region "IChangeNotification Members"

	Private changedKey As Object = New Object()
	Private handlers As System.ComponentModel.EventHandlerList = New System.ComponentModel.EventHandlerList()

	''' <summary>
	''' Implemented explicitly to avoid confusion over which event to use at the State level.
	''' </summary>
	Public Custom Event Changed As EventHandler Implements IChangeNotification.Changed
		AddHandler(ByVal value As EventHandler)
			handlers.AddHandler(changedKey, value)
		End AddHandler
		RemoveHandler(ByVal value As EventHandler)
			handlers.RemoveHandler(changedKey, value)
		End RemoveHandler
		RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
		End RaiseEvent
	End Event

#End Region

#Region "IDisposable Members"

	''' <summary>
	''' Free resources.
	''' </summary>
	Public Sub Dispose() Implements IDisposable.Dispose
		Dispose(True)
		GC.SuppressFinalize(Me)
	End Sub

	''' <summary>
	''' Called to free resources.
	''' </summary>
	''' <param name="disposing">Should be true when calling from Dispose().</param>
	Protected Overridable Sub Dispose(ByVal disposing As Boolean)
		If (disposing) Then
			handlers.Dispose()
		End If
	End Sub

#End Region

End Class
