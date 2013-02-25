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
Imports System.Windows.Forms

Namespace UIElements
	''' <summary>
	''' A <see cref="IUIElementAdapterFactory"/> that produces adapters for ToolStrip-related UI Elements.
	''' </summary>
	Public Class ToolStripUIAdapterFactory
		Implements IUIElementAdapterFactory

		''' <summary>
		''' See <see cref="IUIElementAdapterFactory.GetAdapter"/> for more information.
		''' </summary>
		Public Function GetAdapter(ByVal uiElement As Object) As IUIElementAdapter Implements IUIElementAdapterFactory.GetAdapter
			If TypeOf uiElement Is ToolStrip Then
				Return New ToolStripItemCollectionUIAdapter((CType(uiElement, ToolStrip)).Items)
			End If

			If TypeOf uiElement Is ToolStripItem Then
				Return New ToolStripItemOwnerCollectionUIAdapter(CType(uiElement, ToolStripItem))
			End If

			If TypeOf uiElement Is ToolStripItemCollection Then
				Return New ToolStripItemCollectionUIAdapter(CType(uiElement, ToolStripItemCollection))
			End If

			Throw New ArgumentException("uiElement")
		End Function

		''' <summary>
		''' See <see cref="IUIElementAdapterFactory.Supports"/> for more information.
		''' </summary>
		Public Function Supports(ByVal uiElement As Object) As Boolean Implements IUIElementAdapterFactory.Supports
			Return (TypeOf uiElement Is ToolStrip) OrElse (TypeOf uiElement Is ToolStripItem) OrElse (TypeOf uiElement Is ToolStripItemCollection)
		End Function
	End Class
End Namespace
