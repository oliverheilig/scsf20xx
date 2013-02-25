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
Imports System.Diagnostics
Imports Microsoft.Practices.CompositeUI.Utility
Imports System.Security.Permissions

''' <summary>
''' Defines a set of methods and an event which represent a trace source catalog
''' service. The purpose of the service is to manage <see cref="TraceSource"/> instances
''' that are used in the application.
''' </summary>
Public Interface ITraceSourceCatalogService
	''' <summary>
	''' Raised when a trace source is added to the collection
	''' </summary>
	Event TraceSourceAdded As EventHandler(Of DataEventArgs(Of TraceSource))

	''' <summary>
	''' Retrieves a <see cref="TraceSource"/> by name.
	''' </summary>
	''' <param name="sourceName">Name of the source to retrieve.</param>
	''' <returns>Always retrieves a valid source. If one 
	''' was not previously created, it will be initialized upon retrieval.</returns>
	Function GetTraceSource(ByVal sourceName As String) As TraceSource

	''' <summary>
	''' Returns a <see cref="ReadOnlyDictionary(Of TKey, TValue)"/> for all the known trace sources.
	''' </summary>
	''' <returns>The dictionary of trace sources</returns>
	ReadOnly Property TraceSources() As ReadOnlyDictionary(Of String, TraceSource)
End Interface
