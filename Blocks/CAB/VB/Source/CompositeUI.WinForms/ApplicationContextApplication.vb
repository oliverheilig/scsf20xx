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
Imports System.Windows.Forms


''' <summary>
''' Extends <see cref="WindowsFormsApplication{TWorkItem,TShell}"/> to support an application which
''' uses a Windows Forms <see cref="ApplicationContext"/> to communicate information
''' about its shell.
''' </summary>
''' <typeparam name="TWorkItem">The type of the root application work item.</typeparam>
''' <typeparam name="TShell">The type for the shell to use.</typeparam>
Public MustInherit Class ApplicationContextApplication(Of TWorkItem As {WorkItem, New}, TShell As ApplicationContext)
	Inherits WindowsFormsApplication(Of TWorkItem, TShell)

	''' <summary>
	''' Calls <see cref="Application.Run(ApplicationContext)"/> to start the application.
	''' </summary>
	Protected Overrides Sub Start()
		Application.Run(Shell)
	End Sub
End Class
