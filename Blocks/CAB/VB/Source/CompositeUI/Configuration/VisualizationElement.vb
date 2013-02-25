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
Imports System.Configuration
Imports System.ComponentModel

Namespace Configuration
	''' <summary>
	''' Contains the definition of a visualization.
	''' </summary>
	Public Class VisualizationElement
		Inherits ConfigurationElement

		''' <summary>
		''' The type of the visualization to use.
		''' </summary>
		<ConfigurationProperty("type", IsKey:=True, IsRequired:=True), TypeConverter(GetType(TypeNameConverter))> _
		Public Property Type() As Type
			Get
				Return CType(Me("type"), Type)
			End Get
			Set(ByVal value As Type)
				Me("type") = value
			End Set
		End Property
	End Class
End Namespace
