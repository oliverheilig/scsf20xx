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
Imports Microsoft.Practices.CompositeUI
Imports Microsoft.Practices.CompositeUI.UIElements
Imports Microsoft.Practices.CompositeUI.WinForms
Imports Microsoft.Practices.CompositeUI.Commands
Imports Microsoft.Practices.CompositeUI.Services
Imports BankTellerCommon

Namespace BankShell
	Public Class BankShellApplication : Inherits FormShellApplication(Of WorkItem, BankShellForm)
		<STAThread()> _
		Public Shared Sub Main()
			Dim oTemp As BankShellApplication = New BankShellApplication()
			oTemp.Run()
		End Sub

		' This method is called just after your shell has been created (the root work item
		' also exists). Here, you might want to:
		'   - Attach UIElementManagers
		'   - Register the form with a name.
		'   - Register additional workspaces (e.g. a named WindowWorkspace)
		Protected Overrides Sub AfterShellCreated()
			MyBase.AfterShellCreated()

			Dim fileItem As ToolStripMenuItem = CType(Shell.MainMenuStrip.Items("File"), ToolStripMenuItem)

			RootWorkItem.UIExtensionSites.RegisterSite(UIExtensionConstants.MAINMENU, Shell.MainMenuStrip)
			RootWorkItem.UIExtensionSites.RegisterSite(UIExtensionConstants.MAINSTATUS, Shell.mainStatusStrip)
			RootWorkItem.UIExtensionSites.RegisterSite(UIExtensionConstants.FILE, fileItem)
			RootWorkItem.UIExtensionSites.RegisterSite(UIExtensionConstants.FILEDROPDOWN, fileItem.DropDownItems)

			' Load the menu structure from App.config
			UIElementBuilder.LoadFromConfig(RootWorkItem)
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
