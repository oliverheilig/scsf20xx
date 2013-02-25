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
Imports Microsoft.Practices.CompositeUI.Configuration

Namespace ConfigurationModel
	Public Class TestSection
		Inherits ConfigurationSection

		<ConfigurationProperty("services", IsRequired:=True)> _
		Public ReadOnly Property Services() As TestServiceCollection
			Get
				Return CType(Me("services"), TestServiceCollection)
			End Get
		End Property

		<ConfigurationProperty("element", IsRequired:=True)> _
		Public ReadOnly Property Element() As ElementWithAttributes
			Get
				Return CType(Me("element"), ElementWithAttributes)
			End Get
		End Property
	End Class

	Public Class ElementWithAttributes
		Inherits ParametersElement

		<ConfigurationProperty("name")> _
		Public ReadOnly Property Name() As String
			Get
				Return CStr(Me("name"))
			End Get
		End Property

		<ConfigurationProperty("value")> _
		Public ReadOnly Property Value() As Integer
			Get
				Return CInt(Me("value"))
			End Get
		End Property
	End Class

	<ConfigurationCollection(GetType(TestServiceElement))> _
	Public Class TestServiceCollection
		Inherits ConfigurationElementCollection

		Protected Overrides Function CreateNewElement() As ConfigurationElement
			Return New TestServiceElement()
		End Function

		Protected Overrides Function GetElementKey(ByVal element As ConfigurationElement) As Object
			Return (CType(element, TestServiceElement)).ServiceType
		End Function

		Default Public Overloads Property Item(ByVal serviceType As Type) As TestServiceElement
			Get
				Return CType(BaseGet(serviceType), TestServiceElement)
			End Get
			Set(ByVal value As TestServiceElement)
				If Not BaseGet(serviceType) Is Nothing Then
					BaseRemove(serviceType)
				End If
				BaseAdd(value)
			End Set
		End Property

		Default Public Overloads Property Item(ByVal index As Integer) As TestServiceElement
			Get
				Return CType(BaseGet(index), TestServiceElement)
			End Get
			Set(ByVal value As TestServiceElement)
				If Not BaseGet(index) Is Nothing Then
					BaseRemoveAt(index)
				End If
				BaseAdd(index, value)
			End Set
		End Property
	End Class

	Public Class TestServiceElement
		Inherits ConfigurationElement

		<ConfigurationProperty("serviceType", IsKey:=True, IsRequired:=True), TypeConverter(GetType(TypeNameConverter))> _
		Public ReadOnly Property ServiceType() As Type
			Get
				Return CType(Me("serviceType"), Type)
			End Get
		End Property

		<ConfigurationProperty("instanceType", IsRequired:=True), TypeConverter(GetType(TypeNameConverter))> _
		Public ReadOnly Property InstanceType() As Type
			Get
				Return CType(Me("instanceType"), Type)
			End Get
		End Property
	End Class
End Namespace