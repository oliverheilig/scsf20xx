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

''' <summary>
''' Extends <see cref="CabApplication{TWorkItem}"/> to support applications with a shell.
''' </summary>
''' <typeparam name="TWorkItem">The type of the root application work item.</typeparam>
''' <typeparam name="TShell">The type of the shell the application uses.</typeparam>
Public MustInherit Class CabShellApplication(Of TWorkItem As {WorkItem, New}, TShell)
	Inherits CabApplication(Of TWorkItem)

	Private innerShell As TShell

	''' <summary>
	''' Creates the shell.
	''' </summary>
	Protected NotOverridable Overrides Sub OnRootWorkItemInitialized()
		BeforeShellCreated()
		innerShell = RootWorkItem.Items.AddNew(Of TShell)()
		AfterShellCreated()
	End Sub

	''' <summary>
	''' May be overridden in derived classes to perform activities just before the shell
	''' is created.
	''' </summary>
	Protected Overridable Sub BeforeShellCreated()
	End Sub

	''' <summary>
	''' May be overridden in derived classes to perform activities just after the shell
	''' has been created.
	''' </summary>
	Protected Overridable Sub AfterShellCreated()
	End Sub

	''' <summary>
	''' Returns the shell that was created. Will not be valid until <see cref="AfterShellCreated"/>
	''' has been called.
	''' </summary>
	Protected ReadOnly Property Shell() As TShell
		Get
			Return innerShell
		End Get
	End Property
End Class
