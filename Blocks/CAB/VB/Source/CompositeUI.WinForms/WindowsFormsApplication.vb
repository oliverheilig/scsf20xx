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
Imports Microsoft.Practices.ObjectBuilder
Imports Microsoft.Practices.CompositeUI.UIElements
Imports Microsoft.Practices.CompositeUI.BuilderStrategies
Imports Microsoft.Practices.CompositeUI.Commands
Imports Microsoft.Practices.CompositeUI.WinForms.UIElements


''' <summary>
''' Extends <see cref="CabShellApplication{TWorkItem,TShell}"/> to support shell-based applications that
''' use Windows Forms.
''' </summary>
''' <typeparam name="TWorkItem">The type of the root application work item.</typeparam>
''' <typeparam name="TShell">The type for the shell to use.</typeparam>
Public MustInherit Class WindowsFormsApplication(Of TWorkItem As {WorkItem, New}, TShell)
	Inherits CabShellApplication(Of TWorkItem, TShell)

	''' <summary>
	''' Initializes an instance of the <see cref="WindowsFormsApplication{TWorkItem,TShell}"/> class.
	''' </summary>
	Protected Sub New()
		Application.EnableVisualStyles()
		VisualizerType = GetType(WinFormsVisualizer)
	End Sub

	''' <summary>
	''' Adds Windows Forms specific strategies to the builder.
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
	''' See <see cref="CabShellApplication{TWorkItem,TShell}.AfterShellCreated"/>
	''' </summary>
	Protected Overrides Sub AfterShellCreated()
		RegisterUIElementAdapterFactories()
		RegisterCommandAdapters()
	End Sub

	Private Sub RegisterCommandAdapters()
		Dim mapService As ICommandAdapterMapService = RootWorkItem.Services.Get(Of ICommandAdapterMapService)()
		mapService.Register(GetType(ToolStripItem), GetType(ToolStripItemCommandAdapter))
		mapService.Register(GetType(Control), GetType(ControlCommandAdapter))
	End Sub

	Private Sub RegisterUIElementAdapterFactories()
		Dim catalog As IUIElementAdapterFactoryCatalog = RootWorkItem.Services.Get(Of IUIElementAdapterFactoryCatalog)()
		catalog.RegisterFactory(New ToolStripUIAdapterFactory())
	End Sub
End Class
