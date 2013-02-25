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
Imports System.Drawing
Imports System.Data
Imports System.Text
Imports System.Windows.Forms
Imports Microsoft.Practices.CompositeUI.SmartParts

Namespace DesignTime
	<SmartPart()> _
	 Partial Friend Class MySmartPart : Inherits UserControl : Implements ISmartPartInfoProvider
		Public Sub New()
			InitializeComponent()
		End Sub

		Public Function GetSmartPartInfo(ByVal smartPartInfoType As Type) As ISmartPartInfo Implements ISmartPartInfoProvider.GetSmartPartInfo
			' The containing smart part must add the ISmartPartInfo to its implemented interfaces in order for contained smart part infos to work.
			Dim ensureProvider As Microsoft.Practices.CompositeUI.SmartParts.ISmartPartInfoProvider = Me
			Return Me.infoProvider.GetSmartPartInfo(smartPartInfoType)
		End Function
	End Class
End Namespace
