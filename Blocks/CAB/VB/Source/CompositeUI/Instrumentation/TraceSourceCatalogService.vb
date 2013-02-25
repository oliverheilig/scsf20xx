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
Imports System.Diagnostics
Imports Microsoft.Practices.CompositeUI.Utility
Imports System.Security.Permissions

''' <summary>
''' Service that manages the <see cref="TraceSource"/> objects used in the application.
''' </summary>
Public Class TraceSourceCatalogService : Implements ITraceSourceCatalogService
	Private sources As Dictionary(Of String, TraceSource) = New Dictionary(Of String, TraceSource)()

	''' <summary>
	''' Initializes a new instance of the <see cref="TraceSourceCatalogService"/> class.
	''' </summary>
	Public Sub New()
	End Sub

	''' <summary>
	''' Raised when a trace source is added.
	''' </summary>
	Public Event TraceSourceAdded As EventHandler(Of DataEventArgs(Of TraceSource)) Implements ITraceSourceCatalogService.TraceSourceAdded

	''' <summary>
	''' Gets a <see cref="TraceSource"/> with the given source name.
	''' </summary>
	''' <param name="sourceName">The name of the <see cref="TraceSource"/> to retrieve.</param>
	''' <returns>Either an existing <see cref="TraceSource"/> or a new one if 
	''' it does not exist already.</returns>
	<SecurityPermission(SecurityAction.Demand, Flags:=SecurityPermissionFlag.UnmanagedCode)> _
	Public Function GetTraceSource(ByVal sourceName As String) As TraceSource Implements ITraceSourceCatalogService.GetTraceSource
		Guard.ArgumentNotNull(sourceName, "sourceName")

		If sources.ContainsKey(sourceName) Then
			Return sources(sourceName)
		End If

		Dim source As TraceSource = New TraceSource(sourceName)
		sources.Add(source.Name, source)

		' Only clear the built-in default listener added when the 
		' source is initialized with no config.
		If source.Listeners.Count = 1 AndAlso TypeOf source.Listeners(0) Is DefaultTraceListener Then
			source.Listeners.Clear()
		End If

		source.Listeners.AddRange(Trace.Listeners)

		If Not TraceSourceAddedEvent Is Nothing Then
			TraceSourceAddedEvent.Invoke(Me, New DataEventArgs(Of TraceSource)(source))
		End If

		Return source
	End Function

	''' <summary>
	''' Adds the listener to all the trace sources.
	''' </summary>
	Public Sub AddSharedListener(ByVal listener As TraceListener)
		Guard.ArgumentNotNull(listener, "listener")

		AddSharedListener(listener, String.Empty)
	End Sub

	''' <summary>
	''' Adds the listener to all the trace sources with a specific name.
	''' </summary>
	<SecurityPermission(SecurityAction.Demand, Flags:=SecurityPermissionFlag.UnmanagedCode)> _
	Public Sub AddSharedListener(ByVal listener As TraceListener, ByVal name As String)
		Guard.ArgumentNotNull(listener, "listener")
		Guard.ArgumentNotNull(name, "name")

		If name.Length <> 0 Then
			listener.Name = name
		End If

		For Each pair As KeyValuePair(Of String, TraceSource) In sources
			pair.Value.Listeners.Add(listener)
		Next pair
	End Sub

	''' <summary>
	''' Returns a <see cref="ReadOnlyDictionary(Of TKey, TValue)"/> for all the known trace sources.
	''' </summary>
	''' <returns>The dictionary of trace sources</returns>
	Public ReadOnly Property TraceSources() As ReadOnlyDictionary(Of String, TraceSource) Implements ITraceSourceCatalogService.TraceSources
		Get
			Return New ReadOnlyDictionary(Of String, TraceSource)(sources)
		End Get
	End Property
End Class