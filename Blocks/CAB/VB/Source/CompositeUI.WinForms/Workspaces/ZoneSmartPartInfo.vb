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
Imports System.Windows.Forms
Imports Microsoft.Practices.CompositeUI.SmartParts
Imports System.Drawing


''' <summary>
''' Provides infomation to show smartparts in the <see cref="ZoneWorkspace"/>.
''' </summary>
<ToolboxItem(True), ToolboxBitmap(GetType(ZoneSmartPartInfo), "ZoneSmartPartInfo")> _
Public Class ZoneSmartPartInfo : Inherits SmartPartInfo
	Private innerZoneName As String
	Private innerDock As System.Nullable(Of DockStyle) = Nothing

	''' <summary>
	''' Initializes the ZonedSmartPartInfo with no zone name.
	''' </summary>
	Public Sub New()
	End Sub

	''' <summary>
	''' Creates the information for the smart part specifying the name of the zone.
	''' </summary>
	''' <param name="zoneName">Name of the zone assigned to a smart part.</param>
	Public Sub New(ByVal zoneName As String)
		Me.innerZoneName = zoneName
	End Sub

	''' <summary>
	''' Creates the information for the smart part specifying its title and the name of the zone.
	''' </summary>
	''' <param name="zoneName">Name of the zone assigned to a smart part.</param>
	''' <param name="title">Title of the smart part.</param>
	Public Sub New(ByVal title As String, ByVal zoneName As String)
		MyBase.Title = title
		Me.innerZoneName = zoneName
	End Sub

	''' <summary>
	''' Name of the zone where the smart part should be shown.
	''' </summary>
	''' <remarks>
	''' If a zone with the given name does not exist in the <see cref="ZoneWorkspace"/> 
	''' where the smart part is being shown, an exception will be thrown.
	''' </remarks>
	<Category("Layout"), DefaultValue(CType(Nothing, String))> _
	Public Property ZoneName() As String
		Get
			Return innerZoneName
		End Get
		Set(ByVal value As String)
			innerZoneName = value
		End Set
	End Property

	''' <summary>
	''' Sets the dockstyle of the control to show in the zone.
	''' </summary>
	<DefaultValue(0), Category("Layout")> _
	Public Property Dock() As System.Nullable(Of DockStyle)
		Get
			Return innerDock
		End Get
		Set(ByVal value As System.Nullable(Of DockStyle))
			innerDock = value
		End Set
	End Property
End Class
