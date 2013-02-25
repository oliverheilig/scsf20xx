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
Imports System.Drawing
Imports Microsoft.Practices.CompositeUI.SmartParts


''' <summary>
''' Provides information to show smartparts in the <see cref="WindowWorkspace"/>.
''' </summary>
<ToolboxBitmap(GetType(WindowSmartPartInfo), "WindowSmartPartInfo")> _
Public Class WindowSmartPartInfo : Inherits SmartPartInfo
	Private showModal As Boolean = False
	Private innerControlBox As Boolean = True
	Private maximizeButton As Boolean = True
	Private minimizeButton As Boolean = True
	Private innerHeight As Integer = 0
	Private innerWidth As Integer = 0
	Private innerLocation As Point = Nothing
	Private innerIcon As Icon = Nothing

	''' <summary>
	''' Gets/Sets the location of the window.
	''' </summary>
	<DefaultValue(GetType(Point), "0,0"), Category("Layout")> _
	Public Property Location() As Point
		Get
			Return innerLocation
		End Get
		Set(ByVal value As Point)
			innerLocation = value
		End Set
	End Property

	''' <summary>
	''' The Icon that will appear on the window.
	''' </summary>
	<DefaultValue(CType(Nothing, Icon)), Category("Layout")> _
	Public Property Icon() As Icon
		Get
			Return innerIcon
		End Get
		Set(ByVal value As Icon)
			innerIcon = value
		End Set
	End Property

	''' <summary>
	''' Width of the window.
	''' </summary>
	<Category("Layout"), DefaultValue(0)> _
	Public Property Width() As Integer
		Get
			Return innerWidth
		End Get
		Set(ByVal value As Integer)
			innerWidth = value
		End Set
	End Property

	''' <summary>
	''' Height of the window.
	''' </summary>
	<Category("Layout"), DefaultValue(0)> _
	Public Property Height() As Integer
		Get
			Return innerHeight
		End Get
		Set(ByVal value As Integer)
			innerHeight = value
		End Set
	End Property

	''' <summary>
	''' Make minimize button visible.
	''' </summary>
	<DefaultValue(True), Category("Layout")> _
	Public Property MinimizeBox() As Boolean
		Get
			Return minimizeButton
		End Get
		Set(ByVal value As Boolean)
			minimizeButton = Value
		End Set
	End Property

	''' <summary>
	''' Make maximize button visible.
	''' </summary>
	<DefaultValue(True), Category("Layout")> _
	Public Property MaximizeBox() As Boolean
		Get
			Return maximizeButton
		End Get
		Set(ByVal value As Boolean)
			maximizeButton = Value
		End Set
	End Property

	''' <summary>
	''' Whether the controlbox will be visible.
	''' </summary>
	<DefaultValue(True), Category("Layout")> _
	Public Property ControlBox() As Boolean
		Get
			Return innerControlBox
		End Get
		Set(ByVal value As Boolean)
			innerControlBox = value
		End Set
	End Property

	''' <summary>
	''' Whether the form should be shown as modal.
	''' </summary>
	<DefaultValue(False), Category("Layout")> _
	Public Property Modal() As Boolean
		Get
			Return showModal
		End Get
		Set(ByVal value As Boolean)
			showModal = Value
		End Set
	End Property

End Class
