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
Imports System.Configuration

Namespace Configuration
	''' <summary>
	''' Contains the definition of a service.
	''' </summary>
	Public Class ServiceElement
		Inherits ParametersElement

		''' <summary>
		''' Optional type used to expose the service. If not provided, the 
		''' <see cref="InstanceType"/> will be used to publish the service.
		''' </summary>
		<ConfigurationProperty("serviceType", IsKey:=True, IsRequired:=True), TypeConverter(GetType(TypeNameConverter))> _
		Public Property ServiceType() As Type
			Get
				Return CType(Me("serviceType"), Type)
			End Get
			Set(ByVal value As Type)
				Me("serviceType") = value
			End Set
		End Property

		''' <summary>
		''' The type of the service implementation to instantiate.
		''' </summary>
		<ConfigurationProperty("instanceType", IsRequired:=True), TypeConverter(GetType(TypeNameConverter))> _
		Public Property InstanceType() As Type
			Get
				Return CType(Me("instanceType"), Type)
			End Get
			Set(ByVal value As Type)
				Me("instanceType") = value
			End Set
		End Property
	End Class
End Namespace