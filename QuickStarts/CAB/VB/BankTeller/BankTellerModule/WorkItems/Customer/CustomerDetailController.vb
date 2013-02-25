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
Imports Microsoft.Practices.CompositeUI

Namespace BankTellerModule
	' The CustomerDetailController is the controller used by CustomerDetailView.
	' The detail view contains a "comments" button which dynamically shows the
	' comments for the customer. The controller forwards this request on to the
	' containing work item (CustomerWorkItem) to process, since the action takes
	' place outside the confines of the detail view.

	Public Class CustomerDetailController : Inherits Controller
		Public Shadows ReadOnly Property WorkItem() As CustomerWorkItem
			Get
				Return TryCast(MyBase.WorkItem, CustomerWorkItem)
			End Get
		End Property

		Public Sub ShowCustomerComments()
			WorkItem.ShowCustomerComments()
		End Sub
	End Class
End Namespace
