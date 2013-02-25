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
Imports System.Windows.Forms

Namespace UIElements
	''' <summary>
	''' Provides an adapter for ToolStripItems where new items will be added to the item's owner collection, 
	''' after the item to which the adapter is attached.
	''' </summary>
	Public Class ToolStripItemOwnerCollectionUIAdapter : Inherits ToolStripItemCollectionUIAdapter
		Private item As ToolStripItem

		''' <summary>
		''' Initializes a new instance of the <see cref="ToolStripItemOwnerCollectionUIAdapter"/> using the
		''' specified item.
		''' </summary>
		''' <param name="item"></param>
		Public Sub New(ByVal item As ToolStripItem)
			MyBase.New(item.Owner.Items)
			Me.item = item
			AddHandler item.OwnerChanged, AddressOf item_OwnerChanged
		End Sub

		''' <summary>
		''' Returns the index immediately after the <see cref="ToolStripItem"/> that
		''' was provided to the constructor.
		''' </summary>
		''' <param name="uiElement"></param>
		''' <returns></returns>
		Protected Overrides Function GetInsertingIndex(ByVal uiElement As Object) As Integer
			Dim index As Integer = InternalCollection.IndexOf(item)
			If index < 0 Then
				Throw New InvalidOperationException()
			End If

			Return index + 1
		End Function

		Private Sub item_OwnerChanged(ByVal sender As Object, ByVal e As EventArgs)
			If item.Owner Is Nothing Then
				InternalCollection = Nothing
			Else
				InternalCollection = item.Owner.Items
			End If
		End Sub
	End Class
End Namespace
