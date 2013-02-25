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
Imports System.Configuration

Namespace BankShell
	Friend Class MenuItemElementCollection : Inherits ConfigurationElementCollection
		Protected Overrides Function CreateNewElement() As ConfigurationElement
			Return New MenuItemElement()
		End Function

		Protected Overrides Function GetElementKey(ByVal element As ConfigurationElement) As Object
			Dim e As MenuItemElement = CType(element, MenuItemElement)

			Return e.ID
		End Function
	End Class
End Namespace
