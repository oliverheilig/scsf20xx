Imports Microsoft.VisualBasic
Imports System
Namespace ShellApplication
	Partial Public Class ShellForm
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

#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.splitContainer1 = New System.Windows.Forms.SplitContainer()
			Me.tabWorkspace1 = New Microsoft.Practices.CompositeUI.WinForms.TabWorkspace()
			Me.splitContainer1.Panel1.SuspendLayout()
			Me.splitContainer1.SuspendLayout()
			Me.SuspendLayout()
			' 
			' splitContainer1
			' 
			Me.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.splitContainer1.Location = New System.Drawing.Point(0, 0)
			Me.splitContainer1.Name = "splitContainer1"
			' 
			' splitContainer1.Panel1
			' 
			Me.splitContainer1.Panel1.Controls.Add(Me.tabWorkspace1)
			Me.splitContainer1.Size = New System.Drawing.Size(492, 271)
			Me.splitContainer1.SplitterDistance = 242
			Me.splitContainer1.TabIndex = 0
			' 
			' tabWorkspace1
			' 
			Me.tabWorkspace1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.tabWorkspace1.Location = New System.Drawing.Point(0, 0)
			Me.tabWorkspace1.Name = "tabWorkspace1"
			Me.tabWorkspace1.SelectedIndex = 0
			Me.tabWorkspace1.Size = New System.Drawing.Size(242, 271)
			Me.tabWorkspace1.TabIndex = 0
			' 
			' ShellForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(492, 271)
			Me.Controls.Add(Me.splitContainer1)
			Me.Name = "ShellForm"
			Me.Text = "Hello World Shell"
			Me.splitContainer1.Panel1.ResumeLayout(False)
			Me.splitContainer1.ResumeLayout(False)
			Me.ResumeLayout(False)

		End Sub

#End Region

		Private splitContainer1 As System.Windows.Forms.SplitContainer
		Private tabWorkspace1 As Microsoft.Practices.CompositeUI.WinForms.TabWorkspace
	End Class
End Namespace

