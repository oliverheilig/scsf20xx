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

Namespace UIElements
	''' <summary>
	''' Describes a factory for creating UI Element adapters
	''' </summary>
	Public Interface IUIElementAdapterFactory
		''' <summary>
		''' Gets an adapter for the given UI Element.
		''' </summary>
		''' <param name="uiElement">The UI Element to get the adapter for.</param>
		''' <returns>An <see cref="IUIElementAdapter"/>.</returns>
		Function GetAdapter(ByVal uiElement As Object) As IUIElementAdapter

		''' <summary>
		''' Inidicates whether the factory supports the given UI Element type.
		''' </summary>
		''' <param name="uiElement">The UI Element to check for support.</param>
		''' <returns>true if the UI Element is supported by the factory; 
		''' otherwise false.</returns>
		Function Supports(ByVal uiElement As Object) As Boolean
	End Interface
End Namespace
