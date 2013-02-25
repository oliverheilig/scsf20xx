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
Imports System.Text

Namespace UIElements
	''' <summary>
	''' Interface implemented by objects that provide UIElement support for named locations in a shell.
	''' </summary>
	Public Interface IUIElementAdapter
		''' <summary>
		''' Adds a UI Element.
		''' </summary>
		''' <param name="uiElement">The UI element to add.</param>
		''' <returns>The added UI element.</returns>
		Function Add(ByVal uiElement As Object) As Object

		''' <summary>
		''' Removes a UI Element.
		''' </summary>
		''' <param name="uiElement">The UI element to remove.</param>
		Sub Remove(ByVal uiElement As Object)

	End Interface

End Namespace
