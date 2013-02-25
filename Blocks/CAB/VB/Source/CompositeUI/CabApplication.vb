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
Imports System.Configuration
Imports System.Diagnostics
Imports System.Globalization
Imports System.Reflection
Imports System.Security.Permissions
Imports Microsoft.Practices.CompositeUI.BuilderStrategies
Imports Microsoft.Practices.CompositeUI.Commands
Imports Microsoft.Practices.CompositeUI.Configuration
Imports Microsoft.Practices.CompositeUI.EventBroker
Imports Microsoft.Practices.CompositeUI.Services
Imports Microsoft.Practices.CompositeUI.UIElements
Imports Microsoft.Practices.ObjectBuilder
Imports Microsoft.Practices.CompositeUI.Utility
Imports System.Collections
Imports System.Collections.Generic
Imports System.Collections.Specialized


''' <summary>
''' Defines the <see cref="CabApplication{TWorkItem}"/> as an application having a well known lifecycle
''' and a root <see cref="WorkItem"/> instance providing the scope for the application.
''' </summary>
''' <typeparam name="TWorkItem">The type of the root application work item.</typeparam>
Public MustInherit Class CabApplication(Of TWorkItem As {WorkItem, New})

	Private innerRootWorkItem As TWorkItem
	Private innerConfiguration As SettingsSection
	Private innerVisualizerType As Type = Nothing

	''' <summary>
	''' Sets the visualizer type to be used for the application; if set to null, will turn
	''' off visualizer support (even if visualizations are configured in App.config).
	''' </summary>
	Protected Property VisualizerType() As Type
		Get
			Return innerVisualizerType
		End Get
		Set(ByVal value As Type)
			If Not value Is Nothing Then
				Guard.TypeIsAssignableFromType(value, GetType(IVisualizer), "VisualizerType")
			End If
			innerVisualizerType = value
		End Set
	End Property

	''' <summary>
	''' Starts the application.
	''' </summary>
	Public Sub Run()
		RegisterUnhandledExceptionHandler()
		Dim builder As Builder = CreateBuilder()
		AddBuilderStrategies(builder)
		CreateRootWorkItem(builder)

		Dim visualizer As IVisualizer = CreateVisualizer()
		If Not visualizer Is Nothing Then
			visualizer.Initialize(innerRootWorkItem, builder)
		End If

		AddRequiredServices()
		AddConfiguredServices()
		AddServices()
		AuthenticateUser()
		ProcessShellAssembly()
		innerRootWorkItem.BuildUp()
		LoadModules()
		innerRootWorkItem.FinishInitialization()

		innerRootWorkItem.Run()
		Start()

		innerRootWorkItem.Dispose()
		If Not visualizer Is Nothing Then
			visualizer.Dispose()
		End If
	End Sub

	''' <summary>
	''' May be overridden in a derived class to add specific services required by the application
	''' </summary>
	Protected Overridable Sub AddServices()
	End Sub

	''' <summary>
	''' May be overriden in a derived class to provide a visualizer
	''' </summary>
	''' <returns></returns>
	Protected Function CreateVisualizer() As IVisualizer
		If innerVisualizerType Is Nothing OrElse Configuration.Visualizer.Count = 0 Then
			Return Nothing
		End If

		Return CType(Activator.CreateInstance(innerVisualizerType), IVisualizer)
	End Function

	Private Sub LoadModules()
		Dim loader As IModuleLoaderService = innerRootWorkItem.Services.Get(Of IModuleLoaderService)(True)
		Dim modEnumerator As IModuleEnumerator = innerRootWorkItem.Services.Get(Of IModuleEnumerator)(True)

		If Not modEnumerator Is Nothing Then
			loader.Load(innerRootWorkItem, modEnumerator.EnumerateModules())
		End If
	End Sub

	Private Sub ProcessShellAssembly()
		Dim loader As IModuleLoaderService = innerRootWorkItem.Services.Get(Of IModuleLoaderService)(True)
		Dim [assembly] As System.Reflection.Assembly = System.Reflection.Assembly.GetEntryAssembly()

		If Not [assembly] Is Nothing Then
			loader.Load(innerRootWorkItem, [assembly])
		End If
	End Sub

	Private Sub AuthenticateUser()
		Dim auth As IAuthenticationService = innerRootWorkItem.Services.Get(Of IAuthenticationService)(True)
		auth.Authenticate()
	End Sub

	Private Sub AddConfiguredServices()
		For Each service As ServiceElement In Configuration.Services
			Dim registerAs As Type
			If Not service.ServiceType Is Nothing Then
				registerAs = service.ServiceType
			Else
				registerAs = service.InstanceType
			End If

			If Not innerRootWorkItem.Services.Get(registerAs) Is Nothing Then
				innerRootWorkItem.Services.Remove(registerAs)
			End If

			Dim serviceinstance As Object = innerRootWorkItem.Services.AddNew(service.InstanceType, registerAs)

			Dim tmpConfigurable As IConfigurable = TryCast(serviceinstance, IConfigurable)

			If Not tmpConfigurable Is Nothing Then
				tmpConfigurable.Configure(service.Parameters)
			End If
		Next service
	End Sub

	''' <summary>
	''' Returns the <see cref="SettingsSection"/> that controls the configuration for this <see cref="CabApplication{TWorkItem}"/>.
	''' </summary>
	Protected Overridable ReadOnly Property Configuration() As SettingsSection
		Get
			If innerConfiguration Is Nothing Then
				innerConfiguration = LoadConfiguration()
			End If
			Return innerConfiguration
		End Get
	End Property

	Private Shared Function LoadConfiguration() As SettingsSection
		Dim section As Object = ConfigurationManager.GetSection(SettingsSection.SectionName)
		Dim configSection As SettingsSection = TryCast(section, SettingsSection)

		If Not section Is Nothing AndAlso configSection Is Nothing Then
			Throw New ConfigurationErrorsException(String.Format(CultureInfo.CurrentCulture, My.Resources.InvalidConfigurationSectionType, SettingsSection.SectionName, GetType(SettingsSection)))
		End If

		If configSection Is Nothing Then
			configSection = New SettingsSection()
		End If
		Return configSection
	End Function

	Private Sub AddRequiredServices()
		innerRootWorkItem.Services.AddNew(Of TraceSourceCatalogService, ITraceSourceCatalogService)()
		innerRootWorkItem.Services.AddNew(Of WorkItemExtensionService, IWorkItemExtensionService)()
		innerRootWorkItem.Services.AddNew(Of WorkItemTypeCatalogService, IWorkItemTypeCatalogService)()
		innerRootWorkItem.Services.AddNew(Of SimpleWorkItemActivationService, IWorkItemActivationService)()
		innerRootWorkItem.Services.AddNew(Of WindowsPrincipalAuthenticationService, IAuthenticationService)()
		innerRootWorkItem.Services.AddNew(Of ModuleLoaderService, IModuleLoaderService)()
		innerRootWorkItem.Services.AddNew(Of FileCatalogModuleEnumerator, IModuleEnumerator)()
		innerRootWorkItem.Services.AddOnDemand(Of DataProtectionCryptographyService, ICryptographyService)()
		innerRootWorkItem.Services.AddNew(Of CommandAdapterMapService, ICommandAdapterMapService)()
		innerRootWorkItem.Services.AddNew(Of UIElementAdapterFactoryCatalog, IUIElementAdapterFactoryCatalog)()
	End Sub

	<SecurityPermission(SecurityAction.Demand, ControlAppDomain:=True)> _
	Private Sub CreateRootWorkItem(ByVal builder As Builder)
		innerRootWorkItem = New TWorkItem()
		innerRootWorkItem.InitializeRootWorkItem(builder)
	End Sub

	Private Sub RegisterUnhandledExceptionHandler()
		If (Not Debugger.IsAttached) AndAlso IsOnUnhandledExceptionOverridden() Then
			AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf OnUnhandledException
		End If
	End Sub

	Private Function IsOnUnhandledExceptionOverridden() As Boolean
		Return Not Me.GetType().GetMethod("OnUnhandledException").DeclaringType Is GetType(CabApplication(Of TWorkItem))
	End Function

	Private Function CreateBuilder() As Builder
		Dim builder As Builder = New Builder()

		builder.Strategies.AddNew(Of EventBrokerStrategy)(BuilderStage.Initialization)
		builder.Strategies.AddNew(Of CommandStrategy)(BuilderStage.Initialization)
		builder.Strategies.Add(New RootWorkItemInitializationStrategy(AddressOf Me.OnRootWorkItemInitialized), BuilderStage.Initialization)
		builder.Strategies.AddNew(Of ObjectBuiltNotificationStrategy)(BuilderStage.PostInitialization)

		builder.Policies.SetDefault(Of ISingletonPolicy)(New SingletonPolicy(True))
		builder.Policies.SetDefault(Of IBuilderTracePolicy)(New BuilderTraceSourcePolicy(New TraceSource("Microsoft.Practices.ObjectBuilder")))
		builder.Policies.SetDefault(Of ObjectBuiltNotificationPolicy)(New ObjectBuiltNotificationPolicy())

		Return builder
	End Function

	''' <summary>
	''' Returns the root <see cref="WorkItem"/> for the application.
	''' </summary>
	Public ReadOnly Property RootWorkItem() As TWorkItem
		Get
			Return innerRootWorkItem
		End Get
	End Property

	''' <summary>
	''' May be overridden in a derived class to perform work after the root <see cref="WorkItem"/> has
	''' been fully initialized.
	''' </summary>
	Protected Overridable Sub OnRootWorkItemInitialized()
	End Sub

	''' <summary>
	''' May be overridden in a derived class to add strategies to the <see cref="Builder"/>.
	''' </summary>
	Protected Overridable Sub AddBuilderStrategies(ByVal builder As Builder)
	End Sub

	''' <summary>
	''' Must be overriden. This method is called when the application is fully created and
	''' ready to run.
	''' </summary>
	Protected MustOverride Sub Start()

	''' <summary>
	''' May be overridden in a derived class to be notified of any unhandled exceptions
	''' in the <see cref="AppDomain"/>. If the application is started with a debugger,
	''' this will not be called, so that the normal debugger handling of exceptions
	''' can be used.
	''' </summary>
	''' <param name="sender">unused</param>
	''' <param name="e">The exception event information</param>
	Public Overridable Sub OnUnhandledException(ByVal sender As Object, ByVal e As UnhandledExceptionEventArgs)
	End Sub
End Class