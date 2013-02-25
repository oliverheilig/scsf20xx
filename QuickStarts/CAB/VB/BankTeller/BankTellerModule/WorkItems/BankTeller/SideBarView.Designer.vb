Imports Microsoft.VisualBasic
Imports System
Namespace BankTellerModule
	Partial Public Class SideBarView
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
			Me.splitContainer1 = New System.Windows.Forms.SplitContainer()
			Me.smartPartPlaceholder1 = New Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder()
			Me.customerQueueView1 = New BankTellerModule.CustomerQueueView()
			Me.splitContainer1.Panel1.SuspendLayout()
			Me.splitContainer1.Panel2.SuspendLayout()
			Me.splitContainer1.SuspendLayout()
			Me.SuspendLayout()
			' 
			' splitContainer1
			' 
			Me.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.splitContainer1.Location = New System.Drawing.Point(0, 0)
			Me.splitContainer1.Name = "splitContainer1"
			Me.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
			' 
			' splitContainer1.Panel1
			' 
			Me.splitContainer1.Panel1.Controls.Add(Me.smartPartPlaceholder1)
			' 
			' splitContainer1.Panel2
			' 
			Me.splitContainer1.Panel2.Controls.Add(Me.customerQueueView1)
			Me.splitContainer1.Size = New System.Drawing.Size(199, 500)
			Me.splitContainer1.SplitterDistance = 55
			Me.splitContainer1.TabIndex = 0
			' 
			' smartPartPlaceholder1
			' 
			Me.smartPartPlaceholder1.SmartPartName = "UserInfo"
			Me.smartPartPlaceholder1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.smartPartPlaceholder1.Location = New System.Drawing.Point(0, 0)
			Me.smartPartPlaceholder1.Name = "smartPartPlaceholder1"
			Me.smartPartPlaceholder1.Size = New System.Drawing.Size(199, 55)
			Me.smartPartPlaceholder1.TabIndex = 0
			Me.smartPartPlaceholder1.Text = "smartPartPlaceholder1"
			' 
			' customerQueueView1
			' 
			Me.customerQueueView1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.customerQueueView1.Location = New System.Drawing.Point(0, 0)
			Me.customerQueueView1.Name = "customerQueueView1"
			Me.customerQueueView1.Size = New System.Drawing.Size(199, 441)
			Me.customerQueueView1.TabIndex = 0
			' 
			' SideBarView
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.Controls.Add(Me.splitContainer1)
			Me.Name = "SideBarView"
			Me.Size = New System.Drawing.Size(199, 500)
			Me.splitContainer1.Panel1.ResumeLayout(False)
			Me.splitContainer1.Panel2.ResumeLayout(False)
			Me.splitContainer1.ResumeLayout(False)
			Me.ResumeLayout(False)

		End Sub

#End Region

		Private splitContainer1 As System.Windows.Forms.SplitContainer
		Private smartPartPlaceholder1 As Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder
		Private customerQueueView1 As CustomerQueueView
	End Class
End Namespace
