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
Imports Microsoft.Practices.CompositeUI.Utility
Imports System.Collections.Generic

Namespace UIElements
	''' <summary>
	''' Catalog that keeps track of what factories are available.
	''' </summary>
	Public Interface IUIElementAdapterFactoryCatalog
		''' <summary>
		''' Dictionary of the availble factories.
		''' </summary>
		ReadOnly Property Factories() As IList(Of IUIElementAdapterFactory)

		''' <summary>
		''' Retrieves a factory for the given UI element.
		''' </summary>
		''' <param name="element">The UI element to lookup a factory for.</param>
		''' <returns>A factory to use to create <see cref="IUIElementAdapter"/> for the UI element.</returns>
		Function GetFactory(ByVal element As Object) As IUIElementAdapterFactory

		''' <summary>
		''' Adds an entry in the factories dictionary.
		''' </summary>
		''' <param name="factory">The factory to register with the catalog.</param>
		Sub RegisterFactory(ByVal factory As IUIElementAdapterFactory)
	End Interface
End Namespace
