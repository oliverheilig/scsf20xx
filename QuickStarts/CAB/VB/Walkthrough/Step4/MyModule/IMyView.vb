Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text

Namespace MyModule
	Public Interface IMyView
		Event Load As EventHandler
		Property Message() As String
	End Interface
End Namespace
