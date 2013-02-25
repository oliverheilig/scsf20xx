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
Imports System.Windows.Forms
Imports Microsoft.Practices.CompositeUI.SmartParts

Namespace SmartPartQuickStart
	''' <summary>
	''' Base class for building other smartparts
	''' </summary>
	<SmartPart()> _
	Partial Public Class TitledSmartPart
		Inherits UserControl

		''' <summary>
		''' Constructor
		''' </summary>
		Public Sub New()
			InitializeComponent()
		End Sub

		''' <summary>
		''' Text that will show in the title label.
		''' </summary>
		Public Property Title() As String
			Get
				Return titleLabel.Text
			End Get
			Set(ByVal value As String)
				titleLabel.Text = value
			End Set
		End Property

		''' <summary>
		''' Tooltip for the smartpart.
		''' </summary>
		Public Property Description() As String
			Get
				Return toolTip.GetToolTip(titleLabel)
			End Get
			Set(ByVal value As String)
				toolTip.SetToolTip(titleLabel, value)
			End Set
		End Property
	End Class
End Namespace
