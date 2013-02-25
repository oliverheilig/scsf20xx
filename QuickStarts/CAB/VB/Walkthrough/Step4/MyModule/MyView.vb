Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data
Imports System.Text
Imports System.Windows.Forms
Imports Microsoft.Practices.CompositeUI.SmartParts

Namespace MyModule
	<SmartPart()> _
	Partial Public Class MyView : Inherits UserControl : Implements IMyView

		Public Sub New()
			InitializeComponent()
		End Sub

#Region "IMyView Members"

		Public Property Message() As String Implements IMyView.Message
			Get
				Return Me.label1.Text
			End Get
			Set(ByVal value As String)
				Me.label1.Text = value
			End Set
		End Property

		Public Sub LoadHandler(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
			RaiseEvent IMyViewLoad(sender, e)
		End Sub

#End Region

		Public Event IMyViewLoad(ByVal sender As Object, ByVal e As System.EventArgs) Implements IMyView.Load
	End Class
End Namespace
