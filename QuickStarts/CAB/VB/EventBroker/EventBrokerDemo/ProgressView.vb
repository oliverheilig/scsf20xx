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
Imports System.ComponentModel
Imports System.Windows.Forms
Imports Microsoft.Practices.CompositeUI
Imports Microsoft.Practices.CompositeUI.EventBroker

Namespace EventBrokerDemo
	Partial Public Class ProgressView
		Inherits Form

		Public Sub New()
			InitializeComponent()
		End Sub

		''' <summary>
		''' Listens for on Process complete event.
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		<EventSubscription("topic://EventBrokerQuickStart/ProcessCompleted", Thread:=ThreadOption.UserInterface)> _
		Public Sub OnProcessCompleted(ByVal sender As Object, ByVal e As EventArgs)
			Me.Close()
		End Sub

		''' <summary>
		''' Listens for the Progress Changed event.
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		<EventSubscription("topic://EventBrokerQuickStart/ProgressChanged", Thread:=ThreadOption.UserInterface)> _
		Public Sub OnProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
			progressWork.Value = e.ProgressPercentage
		End Sub

		Private Sub btnClose_Click(ByVal sender As Object, ByVal e As EventArgs)
			Close()
		End Sub
	End Class
End Namespace