Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text

Namespace MyModule
	Public Class MyPresenter
		Private WithEvents view As IMyView
		Public Sub New(ByVal view As IMyView)
			Me.view = view
			AddHandler view.Load, AddressOf view_Load
		End Sub

		Private Sub view_Load(ByVal sender As Object, ByVal e As EventArgs)
			view.Message = "Hello World from a Module"
		End Sub
	End Class
End Namespace
