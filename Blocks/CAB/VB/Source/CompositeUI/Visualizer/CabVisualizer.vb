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
Imports Microsoft.Practices.CompositeUI.Configuration
Imports System.Configuration
Imports System.Globalization
Imports System.Collections.Generic

#Region "Dummy classes"

'Public Class VisualizationElement
'    Inherits ConfigurationElement
'    Public Property Type() As Type
'        Get
'            Return Nothing
'        End Get
'        Set(ByVal value As Type)
'        End Set
'    End Property
'End Class

#End Region

''' <summary>
''' Base class for the CAB visualizer system.
''' </summary>
Public Class CabVisualizer
	Implements IVisualizer

	Private innerCabBuilder As Builder
	Private innerCabRootWorkItem As WorkItem
	Private innerRootWorkItem As WorkItem
	Private innerConfiguration As VisualizationElementCollection
	Private innerVisualizations As List(Of Object) = New List(Of Object)()

	''' <summary>
	''' Returns all of the visualizations in the visualizer
	''' </summary>
	Protected ReadOnly Property Visualizations() As ICollection(Of Object)
		Get
			Return innerVisualizations.AsReadOnly()
		End Get
	End Property

	''' <summary>
	''' Loads the configuration section for the visualizer
	''' </summary>
	Protected Overridable ReadOnly Property Configuration() As VisualizationElementCollection
		Get
			If innerConfiguration Is Nothing Then
				innerConfiguration = LoadConfiguration()
			End If

			Return innerConfiguration
		End Get
	End Property

	Private Function LoadConfiguration() As VisualizationElementCollection
		Dim section As Object = ConfigurationManager.GetSection(SettingsSection.SectionName)
		Dim configSection As SettingsSection = TryCast(section, SettingsSection)

		If Not section Is Nothing AndAlso configSection Is Nothing Then
			Throw New ConfigurationErrorsException(String.Format(CultureInfo.CurrentCulture, My.Resources.InvalidConfigurationSectionType, SettingsSection.SectionName, GetType(SettingsSection)))
		End If

		If configSection Is Nothing Then
			configSection = New SettingsSection()
		End If

		Return configSection.Visualizer
	End Function

	''' <summary>
	''' See <see cref="IVisualizer.CabRootWorkItem"/> for more information.
	''' </summary>
	Public ReadOnly Property CabBuilder() As Builder Implements IVisualizer.CabBuilder
		Get
			Return innerCabBuilder
		End Get
	End Property

	''' <summary>
	''' See <see cref="IVisualizer.CabRootWorkItem"/> for more information.
	''' </summary>
	Public ReadOnly Property CabRootWorkItem() As WorkItem Implements IVisualizer.CabRootWorkItem
		Get
			Return innerCabRootWorkItem
		End Get
	End Property

	''' <summary>
	''' Returns the root <see cref="WorkItem"/> for the Visualizer hierarchy.
	''' </summary>
	Protected ReadOnly Property RootWorkItem() As WorkItem
		Get
			Return innerRootWorkItem
		End Get
	End Property

	''' <summary>
	''' See <see cref="IVisualizer.Initialize"/> for more information.
	''' </summary>
	Public Sub Initialize(ByVal cabRootWorkItem As WorkItem, ByVal cabBuilder As Builder) Implements IVisualizer.Initialize
		If Not Me.innerCabRootWorkItem Is Nothing Then
			Throw New InvalidOperationException(My.Resources.VisualizerAlreadyInitialized)
		End If

		Me.innerCabRootWorkItem = cabRootWorkItem
		Me.innerCabBuilder = cabBuilder

		Dim builder As Builder = CreateBuilder()
		AddBuilderStrategies(builder)
		CreateRootWorkItem(builder)
		AddRequiredServices()
		AddServices()

		innerRootWorkItem.BuildUp()
		CreateVisualizationShell()
		LoadVisualizerPlugins()

		innerRootWorkItem.Run()
	End Sub

	''' <summary>
	''' May be overridden in derived class to create the main shell of the visualizer.
	''' </summary>
	Protected Overridable Sub CreateVisualizationShell()
	End Sub

	''' <summary>
	''' May be overridden in a derived class to add strategies to the <see cref="Builder"/>.
	''' </summary>
	Protected Overridable Sub AddBuilderStrategies(ByVal builder As Builder)
	End Sub

	''' <summary>
	''' May be overridden in a derived class to add services to visualizer's root <see cref="WorkItem"/>.
	''' </summary>
	Protected Overridable Sub AddServices()
	End Sub

	Private Function CreateBuilder() As Builder
		Dim builder As Builder = New Builder()

		builder.Strategies.AddNew(Of EventBrokerStrategy)(BuilderStage.Initialization)
		builder.Strategies.AddNew(Of CommandStrategy)(BuilderStage.Initialization)
		builder.Policies.SetDefault(Of ISingletonPolicy)(New SingletonPolicy(True))

		Return builder
	End Function

	Private Sub CreateRootWorkItem(ByVal builder As Builder)
		innerRootWorkItem = New WorkItem()
		RootWorkItem.InitializeRootWorkItem(builder)
	End Sub

	Private Sub AddRequiredServices()
		RootWorkItem.Services.AddNew(Of TraceSourceCatalogService, ITraceSourceCatalogService)()
		RootWorkItem.Services.AddNew(Of WorkItemTypeCatalogService, IWorkItemTypeCatalogService)()
		RootWorkItem.Services.AddNew(Of SimpleWorkItemActivationService, IWorkItemActivationService)()
		RootWorkItem.Services.AddNew(Of CommandAdapterMapService, ICommandAdapterMapService)()
		RootWorkItem.Services.AddNew(Of UIElementAdapterFactoryCatalog, IUIElementAdapterFactoryCatalog)()
		RootWorkItem.Services.Add(Of IVisualizer)(Me)
	End Sub

	Private Sub LoadVisualizerPlugins()
		For Each elt As VisualizationElement In Configuration
			AddNewVisualization(elt.Type)
		Next elt
	End Sub


	''' <summary>
	''' See <see cref="IDisposable.Dispose"/> for more information.
	''' </summary>
	Public Sub Dispose() Implements IDisposable.Dispose
		Dispose(True)
		GC.SuppressFinalize(Me)
	End Sub

	''' <summary>
	''' Called to free resources.
	''' </summary>
	''' <param name="disposing">Should be true when calling from Dispose().</param>
	Protected Overridable Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			RootWorkItem.Services.Remove(Of IVisualizer)()
			RootWorkItem.Dispose()
		End If
	End Sub

	''' <summary>
	''' Adds a new visualization to the visualizer.
	''' </summary>
	''' <typeparam name="TVisualization">The type of the visualization to make.</typeparam>
	''' <returns>The new visualization.</returns>
	Protected Function AddNewVisualization(Of TVisualization)() As TVisualization
		Return CType(AddNewVisualization(GetType(TVisualization)), TVisualization)
	End Function

	''' <summary>
	''' Adds a new visualization to the visualizer.
	''' </summary>
	''' <param name="type">The type of the visualization to make.</param>
	''' <returns>The new visualization.</returns>
	Protected Function AddNewVisualization(ByVal type As Type) As Object
		Dim result As Object = RootWorkItem.Items.AddNew(type)
		innerVisualizations.Add(result)
		Return result
	End Function
End Class

