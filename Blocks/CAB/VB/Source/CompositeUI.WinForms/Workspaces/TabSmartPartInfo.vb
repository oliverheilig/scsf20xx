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
Imports System.ComponentModel
Imports Microsoft.Practices.CompositeUI.SmartParts
Imports System.Drawing


''' <summary>
''' Specifies the position of the tab page on a <see cref="TabWorkspace"/>.
''' </summary>
Public Enum TabPosition
	''' <summary>
	''' Place tab page at begining.
	''' </summary>
	''' <summary>
	''' Place tab page at end.
	''' </summary>
	Beginning
	[End]

End Enum

''' <summary>
''' A <see cref="SmartPartInfo"/> that describes how a specific smartpart
''' will be shown in a tab workspace.
''' </summary>
<ToolboxBitmap(GetType(TabSmartPartInfo), "TabSmartPartInfo")> _
Public Class TabSmartPartInfo : Inherits SmartPartInfo
	Private innerPosition As TabPosition = TabPosition.End
	Private innerActivateTab As Boolean = True

	''' <summary>
	''' Specifies whether the tab will get focus when shown.
	''' </summary>
	<Category("Layout"), DefaultValue(True)> _
	Public Property ActivateTab() As Boolean
		Get
			Return innerActivateTab
		End Get
		Set(ByVal value As Boolean)
			innerActivateTab = value
		End Set
	End Property

	''' <summary>
	''' Specifies the position of the tab page.
	''' </summary>
	<Category("Layout"), DefaultValue(TabPosition.End)> _
	Public Property Position() As TabPosition
		Get
			Return innerPosition
		End Get
		Set(ByVal value As TabPosition)
			innerPosition = value
		End Set
	End Property


End Class
