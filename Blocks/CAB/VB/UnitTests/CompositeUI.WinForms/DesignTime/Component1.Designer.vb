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
Namespace DesignTime
	Partial Friend Class Component1
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary> 
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (Not components Is Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

#Region "Component Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.mySmartPart1 = New Microsoft.Practices.CompositeUI.WinForms.Tests.DesignTime.MySmartPart()
			Me.monthCalendar1 = New System.Windows.Forms.MonthCalendar()
			Me.listBox1 = New System.Windows.Forms.ListBox()
			' 
			' mySmartPart1
			' 
			Me.mySmartPart1.Location = New System.Drawing.Point(0, 0)
			Me.mySmartPart1.Name = "mySmartPart1"
			Me.mySmartPart1.Size = New System.Drawing.Size(208, 187)
			Me.mySmartPart1.TabIndex = 0
			' 
			' monthCalendar1
			' 
			Me.monthCalendar1.Location = New System.Drawing.Point(0, 0)
			Me.monthCalendar1.Name = "monthCalendar1"
			Me.monthCalendar1.Size = New System.Drawing.Size(178, 155)
			Me.monthCalendar1.TabIndex = 0
			' 
			' listBox1
			' 
			Me.listBox1.FormattingEnabled = True
			Me.listBox1.Location = New System.Drawing.Point(0, 0)
			Me.listBox1.Name = "listBox1"
			Me.listBox1.Size = New System.Drawing.Size(120, 95)
			Me.listBox1.TabIndex = 0

		End Sub

#End Region

		Private mySmartPart1 As MySmartPart
		Private monthCalendar1 As System.Windows.Forms.MonthCalendar
		Private listBox1 As System.Windows.Forms.ListBox
	End Class
End Namespace
