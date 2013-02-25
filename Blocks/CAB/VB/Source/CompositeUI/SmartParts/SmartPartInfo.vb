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
Imports Microsoft.Practices.CompositeUI.Utility

Namespace SmartParts
	''' <summary>
	''' Provides information about a specific smartpart.
	''' </summary>
	Partial Public Class SmartPartInfo : Implements ISmartPartInfo
#Region "Fields"

		Private innerDescription As String = String.Empty
		Private innerTitle As String = String.Empty

#End Region

#Region "Constructors"

		''' <summary>
		''' Initializes a new instance of the <see cref="SmartPartInfo"/> class.
		''' </summary>
		Public Sub New()
		End Sub

		''' <summary>
		''' Initializes a new instance of the <see cref="SmartPartInfo"/> class 
		''' with the title and description values.
		''' </summary>
		Public Sub New(ByVal title As String, ByVal description As String)
			Me.innerTitle = title
			Me.innerDescription = description
		End Sub

#End Region

#Region "Properties"

		''' <summary>
		''' Description to associate with the related smart part.
		''' </summary>
		<Category("Layout")> _
		Public Property Description() As String Implements ISmartPartInfo.Description
			Get
				Return innerDescription
			End Get
			Set(ByVal value As String)
				innerDescription = value
			End Set
		End Property

		''' <summary>
		''' Title to associate with the related smart part.
		''' </summary>
		<Category("Layout")> _
		Public Property Title() As String Implements ISmartPartInfo.Title
			Get
				Return innerTitle
			End Get
			Set(ByVal value As String)
				innerTitle = value
			End Set
		End Property

#End Region

#Region "Public Methods"

		''' <summary>
		''' Creates a new instance of the TSmartPartInfo 
		''' and copies over the information in the source smart part.
		''' </summary>
		Public Shared Function ConvertTo(Of TSmartPartInfo As {ISmartPartInfo, New})(ByVal source As ISmartPartInfo) As TSmartPartInfo
			Guard.ArgumentNotNull(source, "source")

			Dim info As TSmartPartInfo = New TSmartPartInfo()

			info.Description = source.Description
			info.Title = source.Title

			Return info
		End Function

#End Region
	End Class
End Namespace