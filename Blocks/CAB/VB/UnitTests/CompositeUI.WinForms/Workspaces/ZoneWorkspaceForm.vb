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
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms


Partial Friend Class ZoneWorkspaceForm : Inherits Form
	Public Sub New()
		InitializeComponent()
	End Sub

	Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs)
		Me.Workspace.Show(New MonthCalendar(), New ZoneSmartPartInfo("ContentZone"))
	End Sub
End Class
