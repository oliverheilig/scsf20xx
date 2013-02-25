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
Imports System.Collections.Generic
Imports System.Configuration

Namespace BankShell
	Friend Class ShellItemsSection : Inherits ConfigurationSection
		<ConfigurationProperty("menuitems", IsDefaultCollection:=True)> _
		Public ReadOnly Property MenuItems() As MenuItemElementCollection
			Get
				Return CType(Me("menuitems"), MenuItemElementCollection)
			End Get
		End Property
	End Class
End Namespace
