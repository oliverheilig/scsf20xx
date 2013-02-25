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
Imports System.Windows.Forms

Namespace SmartPartQuickStart.BrowseCustomersWorkItem
	''' <summary>
	''' Controller used by the views.
	''' </summary>
	Public Class CustomersController
		Inherits Controller

		''' <summary>
		''' The customer State will be injected into the view.
		''' </summary>
		<State("Customers")> _
		Public Property customers() As List(Of Customer)
			Get
				Return DirectCast(State("Customers"), List(Of Customer))
			End Get
			Set(ByVal value As List(Of Customer))
				Try
					If (Not value Is Nothing) AndAlso (Not State Is Nothing) Then
						State("Customers") = value
					End If
				Catch ex As Exception
					MessageBox.Show(ex.Message & Environment.NewLine & ex.StackTrace)
				End Try
			End Set
		End Property

		''' <summary>
		''' The controller is dependent on the BrowseCustomers WorkItem
		''' and will not run with out it.
		''' </summary>
		Private innerCustomerWorkItem As BrowseCustomersWorkItem = Nothing

		<ServiceDependency(Type:=GetType(WorkItem))> _
		Public WriteOnly Property CustomerWorkItem() As BrowseCustomersWorkItem
			Set(ByVal value As BrowseCustomersWorkItem)
				innerCustomerWorkItem = value
			End Set
		End Property

		''' <summary>
		''' Loads Mock data.
		''' </summary>
		Public Sub PopulateCustomersData()
			If customers Is Nothing Then
				Throw New ArgumentNullException("Customers")
			End If

			customers.Add(New Customer("Jesper", "Aaberg", "One Microsoft Way, Redmond WA 98052", "CAB Rocks!"))
			customers.Add(New Customer("Martin", "Bankov", "One Microsoft Way, Redmond WA 98052", "This is awesome"))
			customers.Add(New Customer("Shu", "Ito", "One Microsoft Way, Redmond WA 98052", "N/A"))
			customers.Add(New Customer("Kim", "Ralls", "One Microsoft Way, Redmond WA 98052", "N/A"))
			customers.Add(New Customer("John", "Kane", "One Microsoft Way, Redmond WA 98052", "N/A"))
		End Sub

		''' <summary>
		''' Shows the customer details.
		''' </summary>
		''' <param name="customer"></param>
		Public Sub ShowCustomerDetails(ByVal customer As Customer)
			' To maintain separation of concerns.
			innerCustomerWorkItem.ShowCustomerDetails(customer)
		End Sub
	End Class
End Namespace
