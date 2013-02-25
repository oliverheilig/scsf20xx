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
Imports System.Windows.Forms
Imports SmartPartQuickStart
Imports Microsoft.Practices.CompositeUI
Imports System.Collections.Generic
Imports Microsoft.Practices.CompositeUI.WinForms

Namespace SmartPartQuickStart
	Friend Class SmartPartApplication
		Inherits FormShellApplication(Of BrowseCustomersWorkItem.BrowseCustomersWorkItem, MainForm)

		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		<STAThread()> _
		Public Shared Sub Main()
			Dim app As SmartPartApplication = New SmartPartApplication()
			app.Run()
		End Sub

		Protected Overrides Sub BeforeShellCreated()
			MyBase.BeforeShellCreated()
			RootWorkItem.State("Customers") = New List(Of Customer)()
		End Sub

#Region "Unhandled Exception"

		Public Overrides Sub OnUnhandledException(ByVal sender As Object, ByVal e As UnhandledExceptionEventArgs)
			Dim ex As Exception = TryCast(e.ExceptionObject, Exception)

			If Not ex Is Nothing Then
				MessageBox.Show(BuildExceptionString(ex))
			Else
				MessageBox.Show("An Exception has occured, unable to get details")
			End If

			Environment.Exit(0)
		End Sub

		Private Function BuildExceptionString(ByVal exception As Exception) As String
			Dim errMessage As String = String.Empty

			errMessage &= exception.Message & Environment.NewLine & exception.StackTrace

			Do While Not exception.InnerException Is Nothing
				errMessage &= BuildInnerExceptionString(exception.InnerException)

				exception = exception.InnerException
			Loop

			Return errMessage
		End Function

		Private Function BuildInnerExceptionString(ByVal innerException As Exception) As String
			Dim errMessage As String = String.Empty

			errMessage &= Environment.NewLine & " InnerException "
			errMessage &= Environment.NewLine & innerException.Message & Environment.NewLine & innerException.StackTrace

			Return errMessage
		End Function
#End Region
	End Class
End Namespace