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
Imports System.ComponentModel

Namespace SmartParts
	''' <summary>
	''' Default implementation of a <see cref="ISmartPartInfoProvider"/> that 
	''' can be used to aggregate the behavior on smart parts that use 
	''' a designer to drag and drop the <see cref="ISmartPartInfo"/> components.
	''' </summary>
	<DesignTimeVisible(False)> _
	Public Class SmartPartInfoProvider : Inherits Component : Implements ISmartPartInfoProvider
		Private innerItems As List(Of ISmartPartInfo) = New List(Of ISmartPartInfo)()

		''' <summary>
		''' The list of <see cref="ISmartPartInfo"/> items the provider exposes.
		''' </summary>
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False)> _
		Public ReadOnly Property Items() As ICollection(Of ISmartPartInfo)
			Get
				Return innerItems
			End Get
		End Property

#Region "ISmartPartInfoProvider Members"

#Region "Utility code for the function GetSmartPartInfo"
		Private Class SmartPartInfoFinder
			Private smartPartInfoType As Type
			Public Sub New(ByVal smartPartInfoType As Type)
				Me.smartPartInfoType = smartPartInfoType
			End Sub
			Public Function Compare(ByVal info As ISmartPartInfo) As Boolean
				Return Not info Is Nothing AndAlso CObj(info).GetType() Is smartPartInfoType
			End Function
		End Class
#End Region

		''' <summary>
		''' Retrieves a smart part information object of the given type from the 
		''' registered <see cref="Items"/>.
		''' </summary>
		''' <seealso cref="ISmartPartInfoProvider.GetSmartPartInfo"/>
		Public Function GetSmartPartInfo(ByVal smartPartInfoType As Type) As ISmartPartInfo Implements ISmartPartInfoProvider.GetSmartPartInfo
			Dim finder As SmartPartInfoFinder = New SmartPartInfoFinder(smartPartInfoType)
			Return innerItems.Find(AddressOf finder.Compare)
		End Function

#End Region
	End Class
End Namespace
