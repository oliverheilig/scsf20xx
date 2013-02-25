Imports Microsoft.VisualBasic
Imports System
Namespace CustomerMapExtensionModule
	Partial Public Class CustomerMap
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
			Me.browser = New System.Windows.Forms.WebBrowser()
			Me.SuspendLayout()
			' 
			' browser
			' 
			Me.browser.Dock = System.Windows.Forms.DockStyle.Fill
			Me.browser.Location = New System.Drawing.Point(0, 0)
			Me.browser.Name = "browser"
			Me.browser.Size = New System.Drawing.Size(354, 339)
			Me.browser.TabIndex = 0
			' 
			' CustomerMap
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.Controls.Add(Me.browser)
			Me.Name = "CustomerMap"
			Me.Size = New System.Drawing.Size(354, 339)
			Me.ResumeLayout(False)

		End Sub

#End Region

		Private browser As System.Windows.Forms.WebBrowser
	End Class
End Namespace
