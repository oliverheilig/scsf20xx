Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports Microsoft.Practices.CompositeUI
Imports Microsoft.Practices.CompositeUI.SmartParts

Namespace MyModule
	Public Class MyWorkItem : Inherits WorkItem
		Public Overloads Sub Run(ByVal tabWorkspace As IWorkspace)
			Dim view As IMyView = Me.Items.AddNew(Of MyView)()
			Dim presenter As MyPresenter = New MyPresenter(view)
			Me.Items.Add(presenter)
			tabWorkspace.Show(view)
		End Sub
	End Class
End Namespace
