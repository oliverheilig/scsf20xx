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
	Partial Friend Class MySmartPart
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
			Me.monthCalendar1 = New System.Windows.Forms.MonthCalendar()
			Me.tabSmartPartInfo1 = New Microsoft.Practices.CompositeUI.WinForms.TabSmartPartInfo()
			Me.infoProvider = New Microsoft.Practices.CompositeUI.SmartParts.SmartPartInfoProvider()
			Me.zoneSmartPartInfo1 = New Microsoft.Practices.CompositeUI.WinForms.ZoneSmartPartInfo()
			Me.SuspendLayout()
			' 
			' monthCalendar1
			' 
			Me.monthCalendar1.Location = New System.Drawing.Point(16, 14)
			Me.monthCalendar1.Name = "monthCalendar1"
			Me.monthCalendar1.TabIndex = 0
			' 
			' tabSmartPartInfo1
			' 
			Me.tabSmartPartInfo1.ActivateTab = True
			Me.tabSmartPartInfo1.Description = ""
			Me.tabSmartPartInfo1.Position = Microsoft.Practices.CompositeUI.WinForms.TabPosition.End
			Me.tabSmartPartInfo1.Title = ""
			Me.infoProvider.Items.Add(Me.tabSmartPartInfo1)
			' 
			' zoneSmartPartInfo1
			' 
			Me.zoneSmartPartInfo1.Description = ""
			Me.zoneSmartPartInfo1.Title = ""
			Me.zoneSmartPartInfo1.ZoneName = Nothing
			Me.infoProvider.Items.Add(Me.zoneSmartPartInfo1)
			' 
			' MySmartPart
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.Controls.Add(Me.monthCalendar1)
			Me.Name = "MySmartPart"
			Me.Size = New System.Drawing.Size(208, 187)
			Me.ResumeLayout(False)

		End Sub

#End Region

		Private monthCalendar1 As System.Windows.Forms.MonthCalendar
		Private tabSmartPartInfo1 As TabSmartPartInfo
		Private zoneSmartPartInfo1 As ZoneSmartPartInfo
		Private infoProvider As Microsoft.Practices.CompositeUI.SmartParts.SmartPartInfoProvider
	End Class
End Namespace
