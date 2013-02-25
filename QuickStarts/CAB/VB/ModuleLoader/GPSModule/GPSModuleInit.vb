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
Imports System.Windows.Forms
Imports GPSModule
Imports Microsoft.Practices.CompositeUI.Services

Namespace SampleModule1
	' The module initialization class
	Public Class GPSModuleInit
		Inherits ModuleInit

		Private innerRootWorkItem As WorkItem

		<ServiceDependency()> _
		Public WriteOnly Property RootWorkItem() As WorkItem
			Set(ByVal value As WorkItem)
				innerRootWorkItem = value
			End Set
		End Property

		Public Overrides Sub Load()
			' This is just to show when the module initialization happens
			MessageBox.Show("SampleModule Started")

			Dim wi As SampleWorkItem = innerRootWorkItem.WorkItems.AddNew(Of SampleWorkItem)()
			wi.Run()
		End Sub
	End Class
End Namespace
