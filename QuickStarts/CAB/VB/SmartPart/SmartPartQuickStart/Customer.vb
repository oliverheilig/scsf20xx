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
Imports System.ComponentModel

Namespace SmartPartQuickStart
	''' <summary>
	''' The Model for data.
	''' </summary>
	Public Class Customer
		Private innerFirstName As String
		Private innerLastName As String
		Private innerAddress As String
		Private innerComments As String
		Private innerId As String = Guid.NewGuid().ToString()

		''' <summary>
		''' Fires when the customer data changes.
		''' </summary>
		Public Event CustomerInfoChanged As EventHandler

		''' <summary>
		''' Constructor
		''' </summary>
		''' <param name="firstName">First Name of customer.</param>
		''' <param name="lastName">Last Name of customer.</param>
		''' <param name="address">Address of customer.</param>
		''' <param name="comments">Comments associated with customer.</param>
		Public Sub New(ByVal firstName As String, ByVal lastName As String, ByVal address As String, ByVal comments As String)
			Me.innerFirstName = firstName
			Me.innerLastName = lastName
			Me.innerAddress = address
			Me.innerComments = comments
		End Sub

		''' <summary>
		''' The unique id of the customer.
		''' </summary>
		Public ReadOnly Property Id() As String
			Get
				Return innerId
			End Get
		End Property

		''' <summary>
		''' The concatination of the first name and last name.
		''' </summary>
		Public ReadOnly Property FullName() As String
			Get
				Return innerFirstName & " " & innerLastName
			End Get
		End Property

		''' <summary>
		''' Last Name of customer.
		''' </summary>
		Public Property LastName() As String
			Get
				Return innerLastName
			End Get
			Set(ByVal value As String)
				innerLastName = value
			End Set
		End Property

		''' <summary>
		''' First Name of customer.
		''' </summary>
		Public Property FirstName() As String
			Get
				Return innerFirstName
			End Get
			Set(ByVal value As String)
				innerFirstName = value
			End Set
		End Property

		''' <summary>
		''' Address of customer.
		''' </summary>
		Public Property Address() As String
			Get
				Return innerAddress
			End Get
			Set(ByVal value As String)
				innerAddress = value
			End Set
		End Property

		''' <summary>
		''' Comments associated with customer.
		''' </summary>
		Public Property Comments() As String
			Get
				Return innerComments
			End Get
			Set(ByVal value As String)
				innerComments = value
			End Set
		End Property

		''' <summary>
		''' Fires the customer chnaged event.
		''' </summary>
		Public Sub FireCustomerInfoChanged()
			If Not Me.CustomerInfoChangedEvent Is Nothing Then
				RaiseEvent CustomerInfoChanged(Me, EventArgs.Empty)
			End If
		End Sub

	End Class
End Namespace
