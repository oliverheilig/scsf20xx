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
Imports System.Collections.Generic
Imports System.Text
Imports Microsoft.Practices.CompositeUI
Imports Microsoft.Practices.CompositeUI.SmartParts

Namespace GPSModule
	' This is a very simple work item that just shows a view
	Public Class SampleWorkItem
		Inherits WorkItem

		Protected Overrides Sub OnRunStarted()
			MyBase.OnRunStarted()
			Dim workspace As IWorkspace = Workspaces("MainWorkspace")
			workspace.Show(Items.AddNew(Of GPSView)())
		End Sub
	End Class
End Namespace
