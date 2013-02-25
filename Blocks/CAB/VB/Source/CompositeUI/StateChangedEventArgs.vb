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
Imports System.ComponentModel


''' <summary>
''' Provides data for a StateChanged event.
''' </summary>
Public Class StateChangedEventArgs
	Inherits EventArgs

	Private innerNewValue As Object
	Private innerOldValue As Object
	Private innerKey As String

	''' <summary>
	''' Initializes a new instance of the <see cref="StateChangedEventArgs"/> class using the
	''' provided key and new value.
	''' </summary>
	Public Sub New(ByVal aKey As String, ByVal aNewValue As Object)
		Me.innerKey = aKey
		Me.innerNewValue = aNewValue
	End Sub

	''' <summary>
	''' Initializes a new instance of the <see cref="StateChangedEventArgs"/> class using the
	''' provided key, new value, and old value.
	''' </summary>
	Public Sub New(ByVal aKey As String, ByVal aNewValue As Object, ByVal anOldValue As Object)
		Me.innerKey = aKey
		Me.innerNewValue = aNewValue
		Me.innerOldValue = anOldValue
	End Sub

	''' <summary>
	''' Gets and sets the changed <see cref="State"/> item key.
	''' </summary>
	Public Property Key() As String
		Get
			Return innerKey
		End Get
		Set(ByVal value As String)
			innerKey = value
		End Set
	End Property

	''' <summary>
	''' Gets and sets the <see cref="State"/> item's new value.
	''' </summary>
	<DefaultValue(CType(Nothing, Object))> _
	Public Property NewValue() As Object
		Get
			Return innerNewValue
		End Get
		Set(ByVal value As Object)
			Me.innerNewValue = value
		End Set
	End Property

	''' <summary>
	''' Gets and sets the <see cref="State"/> item's old value.
	''' </summary>
	<DefaultValue(CType(Nothing, Object))> _
	Public Property OldValue() As Object
		Get
			Return innerOldValue
		End Get
		Set(ByVal value As Object)
			innerOldValue = value
		End Set
	End Property
End Class
