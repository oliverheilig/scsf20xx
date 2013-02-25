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
Imports System.Globalization
Imports System.Windows.Forms
Imports Microsoft.Practices.CompositeUI.SmartParts


''' <summary>
''' Provides common workspace utilities.
''' </summary>
Public Class WorkspaceUtilities
#Region "Public"

	''' <summary>
	''' Retrieve a smartpart as a control.
	''' </summary>
	''' <param name="smartPart"></param>
	''' <returns></returns>
	Private Sub New()
	End Sub
	Public Shared Function GetSmartPartControl(ByVal smartPart As Object) As Control
		If smartPart Is Nothing Then
			Throw New ArgumentNullException("smartPart")
		End If

		Dim spcontrol As Control = TryCast(smartPart, Control)
		ThrowUnSupportedException(spcontrol)

		Return spcontrol
	End Function

	''' <summary>
	''' Check if control is in parent control.
	''' </summary>
	''' <param name="parent"></param>
	''' <param name="spcontrol"></param>
	Public Shared Sub ThrowIfNotInWorkspace(ByVal parent As Control, ByVal spcontrol As Control)
		If parent.Controls.Contains(spcontrol) = False Then
			Throw New ArgumentException(My.Resources.SmartPartNotInManager)
		End If
	End Sub

#End Region

#Region "Private"

	Private Shared Sub ThrowUnSupportedException(ByVal spcontrol As Control)
		If spcontrol Is Nothing Then
			Throw New ArgumentException(String.Format(CultureInfo.CurrentCulture, My.Resources.UnsupportedSmartPartType, GetType(Control)))
		End If
	End Sub

#End Region
End Class
