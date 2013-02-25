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
Imports System.Collections.Specialized
Imports System.Configuration

Namespace Configuration
	''' <summary>
	''' Base class for those configuration elements that support receiving 
	''' arbitrary name-value pairs through configuration.
	''' </summary>
	Public MustInherit Class ParametersElement
		Inherits ConfigurationElement

		''' <summary>
		''' Properties we're creating on the fly as unrecognized attributes appear.
		''' </summary>
		Private dynamicProperties As ConfigurationPropertyCollection = New ConfigurationPropertyCollection()

		Private configAttributes As NameValueCollection = New NameValueCollection()

		''' <summary>
		''' Constructor for use by derived classes.
		''' </summary>
		Protected Sub New()
		End Sub

		''' <summary>
		''' Retrieves the accumulated properties for the element, which include
		''' the dynamically generated ones.
		''' </summary>
		Protected Overrides ReadOnly Property Properties() As ConfigurationPropertyCollection
			Get
				Dim baseprops As ConfigurationPropertyCollection = MyBase.Properties
				For Each dynprop As ConfigurationProperty In dynamicProperties
					baseprops.Add(dynprop)
				Next dynprop
				Return baseprops
			End Get
		End Property

		''' <summary>
		''' Parameters received by the element as attributes in the configuration file.
		''' </summary>
		Public ReadOnly Property Parameters() As NameValueCollection
			Get
				Return configAttributes
			End Get
		End Property

		''' <summary>
		''' Create a new property on the fly for the attribute.
		''' </summary>
		Protected Overrides Function OnDeserializeUnrecognizedAttribute(ByVal name As String, ByVal value As String) As Boolean
			Dim dynprop As ConfigurationProperty = New ConfigurationProperty(name, GetType(String), value)
			dynamicProperties.Add(dynprop)
			Me(dynprop) = value	' Add the value to values bag
			configAttributes.Add(name, value)
			Return True
		End Function
	End Class
End Namespace