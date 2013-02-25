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
Imports System.Collections.Generic
Imports Microsoft.Practices.CompositeUI
Imports BankTellerCommon
Imports Microsoft.Practices.CompositeUI.Utility

Namespace BankTellerModule
	' The CustomerQueueController is the controller used by CustomerQueueView.
	' The queue view displays a list of customers in your queue, so the user can
	' select a customer and view/edit the details of them.

	Public Class CustomerQueueController : Inherits Controller
		' We depend on the customer queue service to tell us which customer is next
		Private innerCustomerQueueService As CustomerQueueService

		<ServiceDependency()> _
		Public WriteOnly Property CustomerQueueService() As CustomerQueueService
			Set(ByVal value As CustomerQueueService)
				innerCustomerQueueService = value
			End Set
		End Property

		Public Shadows ReadOnly Property WorkItem() As BankTellerWorkItem
			Get
				Return TryCast(MyBase.WorkItem, BankTellerWorkItem)
			End Get
		End Property

		Public Function GetNextCustomerInQueue() As Customer
			Return innerCustomerQueueService.GetNext()
		End Function

		Public Sub WorkWithCustomer(ByVal customer As Customer)
			WorkItem.WorkWithCustomer(customer)
		End Sub
	End Class
End Namespace
