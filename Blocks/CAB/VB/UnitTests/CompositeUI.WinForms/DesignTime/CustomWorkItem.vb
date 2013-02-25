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
Imports System.Drawing

Namespace DesignTime
	Public Class CustomWorkItem : Inherits WorkItem
		Friend customerInformation As MySmartPart
		Friend anyComponent As Component1
		Friend customerDetails As MySmartPart
		Private window As WindowWorkspace
		Private components As IContainer

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub InitializeComponent()
			Me.customerInformation = New Microsoft.Practices.CompositeUI.WinForms.Tests.DesignTime.MySmartPart()
			Me.components = New System.ComponentModel.Container()
			Me.anyComponent = New Microsoft.Practices.CompositeUI.WinForms.Tests.DesignTime.Component1(Me.components)
			Me.customerDetails = New Microsoft.Practices.CompositeUI.WinForms.Tests.DesignTime.MySmartPart()
			Me.window = New Microsoft.Practices.CompositeUI.WinForms.WindowWorkspace()
			' 
			' customerInformation
			' 
			Me.customerInformation.Location = New System.Drawing.Point(0, 0)
			Me.customerInformation.Name = "customerInformation"
			Me.customerInformation.Size = New System.Drawing.Size(208, 187)
			Me.customerInformation.TabIndex = 0
			' 
			' customerDetails
			' 
			Me.customerDetails.Location = New System.Drawing.Point(0, 0)
			Me.customerDetails.Name = "customerDetails"
			Me.customerDetails.Size = New System.Drawing.Size(208, 187)
			Me.customerDetails.TabIndex = 0
		End Sub

		' This used to be called from InitializeComponent().
		' Now, it needs to be done during OnBuiltUp, as shown here.

		Public Overrides Sub OnBuiltUp(ByVal id As String)
			MyBase.OnBuiltUp(id)

			' Do not modify this method, changes will be lost upon regeneration by the designer.
			Me.Items.Add(Me.customerInformation, "customerInformation")
			Me.Items.Add(Me.anyComponent, "anyComponent")
			Me.Items.Add(Me.customerDetails, "customerDetails")
			Me.Items.Add(Me.window, "window")
		End Sub
	End Class
End Namespace
