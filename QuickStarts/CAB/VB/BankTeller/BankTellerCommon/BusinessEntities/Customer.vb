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

Namespace BankTellerCommon
	<Serializable()> _
	Public Class Customer
		Private innerAddress1 As String
		Private innerAddress2 As String
		Private innerCity As String
		Private innerComments As String
		Private innerEmailAddress As String
		Private innerFirstName As String
		Private innerId As Integer
		Private innerLastName As String
		Private innerPhone1 As String
		Private innerPhone2 As String
		Private innerState As String
		Private innerZipCode As String

		Public Sub New(ByVal id As Integer, ByVal firstName As String, ByVal lastName As String, ByVal address1 As String, ByVal address2 As String, ByVal city As String, ByVal state As String, ByVal zipCode As String, ByVal emailAddress As String, ByVal phone1 As String, ByVal phone2 As String, ByVal comments As String)
			Me.innerId = id
			Me.innerFirstName = firstName
			Me.innerLastName = lastName
			Me.innerAddress1 = address1
			Me.innerAddress2 = address2
			Me.innerCity = city
			Me.innerState = state
			Me.innerZipCode = zipCode
			Me.innerEmailAddress = emailAddress
			Me.innerPhone1 = phone1
			Me.innerPhone2 = phone2
			Me.innerComments = Comments
		End Sub

		Public Property Address1() As String
			Get
				Return innerAddress1
			End Get
			Set(ByVal value As String)
				innerAddress1 = value
			End Set
		End Property

		Public Property Address2() As String
			Get
				Return innerAddress2
			End Get
			Set(ByVal value As String)
				innerAddress2 = value
			End Set
		End Property

		Public Property City() As String
			Get
				Return innerCity
			End Get
			Set(ByVal value As String)
				innerCity = value
			End Set
		End Property

		Public Property Comments() As String
			Get
				Return innerComments
			End Get
			Set(ByVal value As String)
				innerComments = value
			End Set
		End Property

		Public Property EmailAddress() As String
			Get
				Return innerEmailAddress
			End Get
			Set(ByVal value As String)
				innerEmailAddress = value
			End Set
		End Property

		Public Property FirstName() As String
			Get
				Return innerFirstName
			End Get
			Set(ByVal value As String)
				innerFirstName = value
			End Set
		End Property

		Public ReadOnly Property ID() As Integer
			Get
				Return innerId
			End Get
		End Property

		Public Property LastName() As String
			Get
				Return innerLastName
			End Get
			Set(ByVal value As String)
				innerLastName = value
			End Set
		End Property

		Public Property Phone1() As String
			Get
				Return innerPhone1
			End Get
			Set(ByVal value As String)
				innerPhone1 = value
			End Set
		End Property

		Public Property Phone2() As String
			Get
				Return innerPhone2
			End Get
			Set(ByVal value As String)
				innerPhone2 = value
			End Set
		End Property

		Public Property State() As String
			Get
				Return innerState
			End Get
			Set(ByVal value As String)
				innerState = value
			End Set
		End Property

		Public Property ZipCode() As String
			Get
				Return innerZipCode
			End Get
			Set(ByVal value As String)
				innerZipCode = value
			End Set
		End Property

		Public Overrides Function ToString() As String
			Return String.Format("{0}, {1}", LastName, FirstName)
		End Function

	End Class
End Namespace
