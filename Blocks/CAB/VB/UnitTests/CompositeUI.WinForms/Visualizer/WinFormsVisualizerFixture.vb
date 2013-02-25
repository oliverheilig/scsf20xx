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

Imports Microsoft.VisualStudio.TestTools.UnitTesting








Imports System

Namespace Visualizer
	<TestClass()> _
	Public Class WinFormsVisualizerFixture
		<TestMethod()> _
		Public Sub MainWorkspaceIsAvailableWhenVisualizerInitialized()
			Dim viz As TestableWinFormsVisualizer = New TestableWinFormsVisualizer()
			viz.Initialize(Nothing, Nothing)

			Assert.IsNotNull(viz.RootWorkItem.Workspaces("MainWorkspace"))
		End Sub

		Private Class TestableWinFormsVisualizer : Inherits WinFormsVisualizer
			Public Shadows ReadOnly Property RootWorkItem() As WorkItem
				Get
					Return MyBase.RootWorkItem
				End Get
			End Property
		End Class
	End Class
End Namespace
