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
Imports Microsoft.Practices.CompositeUI.BuilderStrategies
Imports Microsoft.Practices.CompositeUI.Commands
Imports Microsoft.Practices.CompositeUI.EventBroker
Imports Microsoft.Practices.CompositeUI.Services
Imports Microsoft.Practices.CompositeUI.UIElements
Imports Microsoft.Practices.ObjectBuilder
Imports Microsoft.Practices.CompositeUI.WinForms.Visualizer


''' <summary>
''' Implements a <see cref="CabVisualizer"/> to be used with WinForms applications.
''' </summary>
Public Class WinFormsVisualizer : Inherits CabVisualizer
	''' <summary>
	''' Added Windows Forms specific strategies to the builder.
	''' </summary>
	Protected Overrides Sub AddBuilderStrategies(ByVal builder As Builder)
		builder.Strategies.AddNew(Of WinFormServiceStrategy)(BuilderStage.Initialization)
		builder.Strategies.AddNew(Of ControlActivationStrategy)(BuilderStage.Initialization)
		builder.Strategies.AddNew(Of ControlSmartPartStrategy)(BuilderStage.Initialization)
	End Sub

	''' <summary>
	''' See <see cref="CabApplication{TWorkItem}.AddServices"/>
	''' </summary>
	Protected Overrides Sub AddServices()
		RootWorkItem.Services.AddNew(Of ControlActivationService, IControlActivationService)()
	End Sub

	''' <summary>
	''' Creates a new instance of the <see cref="VisualizerForm"/> and shows it.
	''' </summary>
	Protected NotOverridable Overrides Sub CreateVisualizationShell()
		RootWorkItem.Items.AddNew(Of VisualizerForm)("VisualizerForm").Show()
	End Sub
End Class

