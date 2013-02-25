Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports Microsoft.Practices.CompositeUI.WinForms

Namespace ShellApplication
	Public Class ShellApplication : Inherits FormShellApplication(Of ShellWorkItem, ShellForm)
		<STAThread()> _
		Shared Sub Main()
			Dim oTemp As ShellApplication = New ShellApplication()
			oTemp.Run()
		End Sub
	End Class
End Namespace
