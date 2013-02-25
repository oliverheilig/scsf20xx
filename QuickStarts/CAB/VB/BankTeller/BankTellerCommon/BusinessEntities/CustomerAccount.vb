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
	Public Class CustomerAccount
		Private innerAccountNumber As Long
		Private innerAccountType As String
		Private innerCurrentBalance As Decimal

		Public Sub New(ByVal accountNumber As Long, ByVal accountType As String, ByVal currentBalance As Decimal)
			Me.innerAccountNumber = accountNumber
			Me.innerAccountType = accountType
			Me.innerCurrentBalance = CurrentBalance
		End Sub

		Public ReadOnly Property AccountNumber() As Long
			Get
				Return innerAccountNumber
			End Get
		End Property

		Public ReadOnly Property AccountType() As String
			Get
				Return innerAccountType
			End Get
		End Property

		Public ReadOnly Property CurrentBalance() As Decimal
			Get
				Return innerCurrentBalance
			End Get
		End Property

	End Class
End Namespace
