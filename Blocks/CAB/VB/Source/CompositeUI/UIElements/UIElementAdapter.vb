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
Imports Microsoft.Practices.CompositeUI.UIElements
Imports Microsoft.Practices.CompositeUI.Utility
Imports System.Globalization

Namespace UIElements
	''' <summary>
	''' Support class to simplify creation of <see cref="IUIElementAdapter"/>-derived classes.
	''' </summary>
	Public MustInherit Class UIElementAdapter(Of TUIElement)
		Implements IUIElementAdapter

		''' <summary>
		''' Adds a UI Element.
		''' </summary>
		''' <param name="uiElement"></param>
		''' <returns></returns>
		Protected MustOverride Function Add(ByVal uiElement As TUIElement) As TUIElement

		''' <summary>
		''' Removes a UI Element.
		''' </summary>
		''' <param name="uiElement">The UI Element to remove.</param>
		Protected MustOverride Sub Remove(ByVal uiElement As TUIElement)

		''' <summary>
		''' Adds a UI Element.
		''' </summary>
		''' <param name="uiElement">The UI Element to add.</param>
		''' <returns>The UI Element that was actually added.</returns>
		Public Function Add(ByVal uiElement As Object) As Object Implements IUIElementAdapter.Add
			Guard.ArgumentNotNull(uiElement, "uiElement")

			Dim element As TUIElement = GetTypedElement(uiElement)

			Return Add(element)

		End Function

		''' <summary>
		''' Removes a UI Element.
		''' </summary>
		''' <param name="uiElement">The UI Element to remove.</param>
		Public Sub Remove(ByVal uiElement As Object) Implements IUIElementAdapter.Remove
			Guard.ArgumentNotNull(uiElement, "uiElement")

			Dim element As TUIElement = GetTypedElement(uiElement)
			Remove(element)
		End Sub

		Private Function GetTypedElement(ByVal uiElement As Object) As TUIElement
			If Not (TypeOf uiElement Is TUIElement) Then
				Throw New ArgumentException(String.Format(CultureInfo.CurrentCulture, My.Resources.UIElementNotCorrectType, GetType(TUIElement).ToString()))
			End If

			Return CType(uiElement, TUIElement)
		End Function

	End Class
End Namespace
