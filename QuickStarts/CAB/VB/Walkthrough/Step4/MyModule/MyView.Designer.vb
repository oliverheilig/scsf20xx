Imports Microsoft.VisualBasic
Imports System
Namespace MyModule
	Partial Public Class MyView
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
			Me.label1 = New System.Windows.Forms.Label()
			Me.SuspendLayout()
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(0)))
			Me.label1.Location = New System.Drawing.Point(22, 22)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(46, 17)
			Me.label1.TabIndex = 0
			Me.label1.Text = "label1"
			' 
			' MyView
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.Controls.Add(Me.label1)
			Me.Name = "MyView"
			Me.Size = New System.Drawing.Size(250, 250)
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

#End Region

		Private label1 As System.Windows.Forms.Label
	End Class
End Namespace
