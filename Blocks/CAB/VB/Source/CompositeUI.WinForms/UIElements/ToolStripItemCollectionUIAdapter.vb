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
Imports Microsoft.Practices.CompositeUI.Utility

Namespace UIElements
	''' <summary>
	''' An adapter that wraps a <see cref="ToolStripItemCollection"/> for use as an <see cref="IUIElementAdapter"/>.
	''' </summary>
	Public Class ToolStripItemCollectionUIAdapter : Inherits UIElementAdapter(Of ToolStripItem)
		Private collection As ToolStripItemCollection

		''' <summary>
		''' Initializes a new instance of the <see cref="ToolStripItemCollectionUIAdapter"/> class.
		''' </summary>
		''' <param name="collection"></param>
		Public Sub New(ByVal collection As ToolStripItemCollection)
			Guard.ArgumentNotNull(collection, "collection")
			Me.collection = collection
		End Sub

		''' <summary>
		''' See <see cref="UIElementAdapter{TUIElement}.Add(TUIElement)"/> for more information.
		''' </summary>
		Protected Overrides Function Add(ByVal uiElement As ToolStripItem) As ToolStripItem
			If collection Is Nothing Then
				Throw New InvalidOperationException()
			End If

			collection.Insert(GetInsertingIndex(uiElement), uiElement)
			Return uiElement
		End Function

		''' <summary>
		''' See <see cref="UIElementAdapter{TUIElement}.Remove(TUIElement)"/> for more information.
		''' </summary>
		Protected Overrides Sub Remove(ByVal uiElement As ToolStripItem)
			If Not uiElement.Owner Is Nothing Then
				uiElement.Owner.Items.Remove(uiElement)
			End If
		End Sub

		''' <summary>
		''' When overridden in a derived class, returns the correct index for the item being added. By default,
		''' it will return the length of the collection.
		''' </summary>
		''' <param name="uiElement"></param>
		''' <returns></returns>
		Protected Overridable Function GetInsertingIndex(ByVal uiElement As Object) As Integer
			Return collection.Count
		End Function

		''' <summary>
		''' Returns the internal collection mananged by the <see cref="ToolStripItemCollectionUIAdapter"/>
		''' </summary>
		Protected Property InternalCollection() As ToolStripItemCollection
			Get
				Return collection
			End Get
			Set(ByVal value As ToolStripItemCollection)
				collection = Value
			End Set
		End Property
	End Class
End Namespace
