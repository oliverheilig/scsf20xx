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
Imports System.Configuration

Namespace Configuration
	''' <summary>
	''' List of services configured for the <see cref="CabApplication{TWorkItem}"/>.
	''' </summary>
	<ConfigurationCollection(GetType(ServiceElement))> _
	Public Class ServiceElementCollection
		Inherits ConfigurationElementCollection
		Implements IEnumerable(Of ServiceElement)

		''' <summary>
		''' Creates a new <see cref="ServiceElement"/>.
		''' </summary>
		Protected Overrides Function CreateNewElement() As ConfigurationElement
			Return New ServiceElement()
		End Function

		''' <summary>
		''' Retrieves the key for the configuration element.
		''' </summary>
		Protected Overrides Function GetElementKey(ByVal element As ConfigurationElement) As Object
			Return (CType(element, ServiceElement)).ServiceType
		End Function

		''' <summary>
		''' Provides access to the service elements by the type of service registered.
		''' </summary>
		Default Public Overloads Property Item(ByVal serviceType As Type) As ServiceElement
			Get
				Return CType(MyBase.BaseGet(serviceType), ServiceElement)
			End Get
			Set(ByVal value As ServiceElement)
				If Not MyBase.BaseGet(serviceType) Is Nothing Then
					MyBase.BaseRemove(serviceType)
				End If
				MyBase.BaseAdd(value)
			End Set
		End Property

		''' <summary>
		''' Adds a new service to the collection.
		''' </summary>
		Public Sub Add(ByVal service As ServiceElement)
			MyBase.BaseAdd(service)
		End Sub

		''' <summary>
		''' Removes a service from the collection.
		''' </summary>
		Public Sub Remove(ByVal serviceType As Type)
			MyBase.BaseRemove(serviceType)
		End Sub

#Region "IEnumerable<ServiceElement> Members"

		''' <summary>
		''' Enumerates the services in the collection.
		''' </summary>
		Public Shadows Function GetEnumerator() As IEnumerator(Of ServiceElement) Implements IEnumerable(Of ServiceElement).GetEnumerator
			Dim count As Integer = MyBase.Count
			Dim i As Integer = 0
			Dim baseList As List(Of ServiceElement) = New List(Of ServiceElement)

			Do While i < count
				baseList.Add(CType(MyBase.BaseGet(i), ServiceElement))
				i += 1
			Loop
			Return baseList.GetEnumerator()
		End Function

#End Region

	End Class
End Namespace