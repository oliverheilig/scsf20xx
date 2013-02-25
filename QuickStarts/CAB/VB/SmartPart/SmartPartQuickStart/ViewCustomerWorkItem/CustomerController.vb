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

Namespace SmartPartQuickStart.ViewCustomerWorkItem
	''' <summary>
	''' Controller for the customer comments view.
	''' </summary>
	Public Class CustomerController
		Inherits Controller

		''' <summary>
		''' Has to have a customer workitem to work.
		''' </summary>
		Private innerCustomerWorkItem As ViewCustomerWorkItem = Nothing

		<ServiceDependency(Type:=GetType(WorkItem))> _
		Public WriteOnly Property CustomerWorkItem() As ViewCustomerWorkItem
			Set(ByVal value As ViewCustomerWorkItem)
				innerCustomerWorkItem = value
			End Set
		End Property

		''' <summary>
		''' Calls back to the workitem to create and show
		''' the comments view.
		''' </summary>
		Public Sub ShowCustomerComments()
			innerCustomerWorkItem.ShowCustomerComments()
		End Sub
	End Class
End Namespace
