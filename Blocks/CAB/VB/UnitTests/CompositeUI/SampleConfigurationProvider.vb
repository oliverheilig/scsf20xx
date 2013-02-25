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
Imports System.Windows.Forms


Public Class SampleConfigurationProvider : Inherits LocalFileSettingsProvider
	Public Overrides Sub Initialize(ByVal name As String, ByVal values As NameValueCollection)
		MessageBox.Show("Initialize")
	End Sub

	Public Overrides Sub SetPropertyValues(ByVal context As SettingsContext, ByVal values As SettingsPropertyValueCollection)
		MessageBox.Show("SetPropertyValues")
		MyBase.SetPropertyValues(context, values)
	End Sub

	Public Overrides Function GetPropertyValues(ByVal context As SettingsContext, ByVal properties As SettingsPropertyCollection) As SettingsPropertyValueCollection
		MessageBox.Show("GetPropertyValues")
		Return MyBase.GetPropertyValues(context, properties)
	End Function
End Class
