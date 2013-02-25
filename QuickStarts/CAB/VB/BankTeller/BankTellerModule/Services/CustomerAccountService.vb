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

' The example companies, organizations, products, domain names, e-mail addresses,
' logos, people, places, and events depicted herein are fictitious.  No association
' with any real company, organization, product, domain name, email address, logo,
' person, places, or events is intended or should be inferred.


Imports Microsoft.VisualBasic
Imports System.Collections
Imports System.Collections.Generic
Imports BankTellerCommon
Imports Microsoft.Practices.CompositeUI
Imports Microsoft.Practices.CompositeUI.Utility

Namespace BankTellerModule
	' The service that simulates a data provider for customer account business
	' entities (model data). Any class annotated with the [Service] attribute
	' is automatically registered in the root Work Item during module
	' initialization.

	<Service()> _
	Public Class CustomerAccountService
		Private customerAccounts As ListDictionary(Of Integer, CustomerAccount)

		Public Sub New()
			customerAccounts = New ListDictionary(Of Integer, CustomerAccount)()

			customerAccounts.Add(1, New CustomerAccount(123456781, "Checking", 1842.75D))
			customerAccounts.Add(1, New CustomerAccount(123456782, "Savings", 9367.92D))

			customerAccounts.Add(2, New CustomerAccount(987654321, "Interest Checking", 2496.44D))
			customerAccounts.Add(2, New CustomerAccount(987654322, "Money Market", 21959.38D))
			customerAccounts.Add(2, New CustomerAccount(987654323, "Car Loan", -19483.95D))
		End Sub

		Public Function GetByCustomerID(ByVal customerID As Integer) As IEnumerable
			Return customerAccounts(customerID)
		End Function
	End Class
End Namespace
