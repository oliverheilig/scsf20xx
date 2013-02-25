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
Imports System.Diagnostics
Imports System.Globalization
Imports System.IO
Imports System.Reflection
Imports Microsoft.Practices.CompositeUI.Configuration
Imports Microsoft.Practices.CompositeUI.Utility
Imports Microsoft.Practices.ObjectBuilder

Namespace Services
	''' <summary>
	''' Service to load modules into the application.
	''' </summary>
	Public Class ModuleLoaderService
		Implements IModuleLoaderService

		Private innerLoadedModules As Dictionary(Of Assembly, ModuleMetadata) = New Dictionary(Of Assembly, ModuleMetadata)()
		Private traceSource As TraceSource = Nothing

		''' <summary>
		''' Initializes a new instance of the <see cref="ModuleLoaderService"/> class with the
		''' provided trace source.
		''' </summary>
		''' <param name="traceSource">The trace source for tracing. If null is
		''' passed, the service does not perform tracing.</param>
		<InjectionConstructor()> _
		Public Sub New(<ClassNameTraceSource()> ByVal traceSource As TraceSource)
			Me.traceSource = traceSource
		End Sub

		''' <summary>
		''' See <see cref="IModuleLoaderService.ModuleLoaded"/> for more information.
		''' </summary>
		Public Event ModuleLoaded As EventHandler(Of DataEventArgs(Of LoadedModuleInfo)) Implements IModuleLoaderService.ModuleLoaded

		''' <summary>
		''' See <see cref="IModuleLoaderService.LoadedModules"/> for more information.
		''' </summary>
		Public ReadOnly Property LoadedModules() As IList(Of LoadedModuleInfo) Implements IModuleLoaderService.LoadedModules
			Get
				Dim result As List(Of LoadedModuleInfo) = New List(Of LoadedModuleInfo)()

				For Each aModule As ModuleMetadata In innerLoadedModules.Values
					result.Add(aModule.ToLoadedModuleInfo())
				Next aModule

				Return result.AsReadOnly()
			End Get
		End Property

		''' <summary>
		''' See <see cref="IModuleLoaderService.Load(WorkItem, IModuleInfo[])"/> for more information.
		''' </summary>
		Public Sub Load(ByVal workItem As WorkItem, ByVal ParamArray modules As IModuleInfo()) Implements IModuleLoaderService.Load
			Guard.ArgumentNotNull(workItem, "workItem")
			Guard.ArgumentNotNull(modules, "modules")

			InnerLoad(workItem, modules)
		End Sub

		''' <summary>
		''' See <see cref="IModuleLoaderService.Load(WorkItem, Assembly[])"/> for more information.
		''' </summary>
		Public Sub Load(ByVal workItem As WorkItem, ByVal ParamArray assemblies As System.Reflection.Assembly()) Implements IModuleLoaderService.Load
			Guard.ArgumentNotNull(workItem, "workItem")
			Guard.ArgumentNotNull(assemblies, "assemblies")

			Dim modules As List(Of IModuleInfo) = New List(Of IModuleInfo)()

			For Each anAssembly As System.Reflection.Assembly In assemblies
				modules.Add(New ModuleInfo(anAssembly))
			Next anAssembly

			InnerLoad(workItem, modules.ToArray())
		End Sub

		''' <summary>
		''' Fires the ModuleLoaded event.
		''' </summary>
		''' <param name="module">The module that was loaded.</param>
		Protected Overridable Sub OnModuleLoaded(ByVal aModule As LoadedModuleInfo)
			If Not ModuleLoadedEvent Is Nothing Then
				RaiseEvent ModuleLoaded(Me, New DataEventArgs(Of LoadedModuleInfo)(aModule))
			End If
		End Sub

		Private Sub InnerLoad(ByVal workItem As WorkItem, ByVal modules As IModuleInfo())
			If modules.Length = 0 Then
				Return
			End If

			LoadAssemblies(modules)
			Dim loadOrder As List(Of ModuleMetadata) = GetLoadOrder()

			For Each aModule As ModuleMetadata In loadOrder
				aModule.LoadServices(workItem)
			Next aModule

			For Each aModule As ModuleMetadata In loadOrder
				aModule.InitializeModuleClasses(workItem)
			Next aModule

			For Each aModule As ModuleMetadata In loadOrder
				aModule.InitializeWorkItemExtensions(workItem)
			Next aModule

			For Each aModule As ModuleMetadata In loadOrder
				aModule.NotifyOfLoadedModule(AddressOf OnModuleLoaded)
			Next aModule
		End Sub

		Private Function GetLoadOrder() As List(Of ModuleMetadata)
			Dim indexedInfo As Dictionary(Of String, ModuleMetadata) = New Dictionary(Of String, ModuleMetadata)()
			Dim solver As ModuleDependencySolver = New ModuleDependencySolver()
			Dim result As List(Of ModuleMetadata) = New List(Of ModuleMetadata)()

			For Each data As ModuleMetadata In innerLoadedModules.Values
				If indexedInfo.ContainsKey(data.Name) Then
					Throw New ModuleLoadException(String.Format(CultureInfo.CurrentCulture, My.Resources.DuplicatedModule, data.Name))
				End If

				indexedInfo.Add(data.Name, data)
				solver.AddModule(data.Name)

				For Each dependency As String In data.Dependencies
					solver.AddDependency(data.Name, dependency)
				Next dependency
			Next data

			If solver.ModuleCount > 0 Then
				Dim loadOrder As String() = solver.Solve()

				Dim i As Integer = 0
				Do While i < loadOrder.Length
					result.Add(indexedInfo(loadOrder(i)))
					i += 1
				Loop
			End If

			Return result
		End Function

		Private Sub LoadAssemblies(ByVal modules As IModuleInfo())
			For Each aModule As IModuleInfo In modules
				GuardLegalAssemblyFile(aModule)
				Dim anAssembly As System.Reflection.Assembly = LoadAssembly(aModule.AssemblyFile)

				If innerLoadedModules.ContainsKey(anAssembly) Then
					Continue For
				End If

				innerLoadedModules.Add(anAssembly, New ModuleMetadata(anAssembly, traceSource))
			Next aModule
		End Sub

		Private Function LoadAssembly(ByVal assemblyFile As String) As System.Reflection.Assembly
			Guard.ArgumentNotNullOrEmptyString(assemblyFile, "assemblyFile")

			assemblyFile = GetModulePath(assemblyFile)

			Dim file As FileInfo = New FileInfo(assemblyFile)
			Dim anAssembly As System.Reflection.Assembly = Nothing

			Try
				anAssembly = System.Reflection.Assembly.LoadFrom(file.FullName)
			Catch ex As Exception
				Throw New ModuleLoadException(assemblyFile, ex.Message, ex)
			End Try

			If Not traceSource Is Nothing Then
				traceSource.TraceInformation(My.Resources.LogModuleAssemblyLoaded, file.FullName)
			End If
			Return anAssembly
		End Function

		Private Function GetModulePath(ByVal assemblyFile As String) As String
			If Path.IsPathRooted(assemblyFile) = False Then
				assemblyFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, assemblyFile)
			End If

			Return assemblyFile
		End Function

#Region "Guards"

		Private Sub GuardLegalAssemblyFile(ByVal modInfo As IModuleInfo)
			Guard.ArgumentNotNull(modInfo, "modInfo")
			Guard.ArgumentNotNull(modInfo.AssemblyFile, "modInfo.AssemblyFile")

			Dim assemblyFilePath As String = GetModulePath(modInfo.AssemblyFile)

			If File.Exists(assemblyFilePath) = False Then
				Throw New ModuleLoadException(String.Format(CultureInfo.CurrentCulture, My.Resources.ModuleNotFound(), assemblyFilePath))
			End If
		End Sub

#End Region

#Region "Helper classes"

		Private Class ModuleMetadata
			Private innerAssembly As System.Reflection.Assembly
			Private loadedServices As Boolean = False
			Private extensionsInitialized As Boolean = False
			Private modulesInitialzed As Boolean = False
			Private innerName As String = Nothing
			Private notified As Boolean = False

			Private innerDependencies As List(Of String) = New List(Of String)()
			Private moduleTypes As List(Of Type) = New List(Of Type)()
			Private moduleClasses As List(Of IModule) = New List(Of IModule)()
			Private roles As List(Of String) = New List(Of String)()
			Private services As List(Of ServiceMetadata) = New List(Of ServiceMetadata)()
			Private workItemExtensions As List(Of KeyValuePair(Of Type, Type)) = New List(Of KeyValuePair(Of Type, Type))()
			Private workItemRootExtensions As List(Of Type) = New List(Of Type)()

			Private traceSource As TraceSource

			Public Sub New(ByVal anAssembly As System.Reflection.Assembly, ByVal traceSource As TraceSource)
				Me.innerAssembly = anAssembly
				Me.traceSource = traceSource

				For Each attr As ModuleAttribute In anAssembly.GetCustomAttributes(GetType(ModuleAttribute), True)
					innerName = attr.Name
				Next attr

				For Each attr As ModuleDependencyAttribute In anAssembly.GetCustomAttributes(GetType(ModuleDependencyAttribute), True)
					innerDependencies.Add(attr.Name)
				Next attr

				For Each type As Type In anAssembly.GetExportedTypes()
					For Each attr As ServiceAttribute In type.GetCustomAttributes(GetType(ServiceAttribute), True)
						If Not attr.RegisterAs Is Nothing Then
							services.Add(New ServiceMetadata(type, attr.RegisterAs, attr.AddOnDemand))
						Else
							services.Add(New ServiceMetadata(type, type, attr.AddOnDemand))
						End If
					Next attr

					For Each attr As WorkItemExtensionAttribute In type.GetCustomAttributes(GetType(WorkItemExtensionAttribute), True)
						workItemExtensions.Add(New KeyValuePair(Of Type, Type)(attr.WorkItemType, type))
					Next attr

					For Each attr As RootWorkItemExtensionAttribute In type.GetCustomAttributes(GetType(RootWorkItemExtensionAttribute), True)
						workItemRootExtensions.Add(type)
					Next attr

					If (Not type.IsAbstract) AndAlso GetType(IModule).IsAssignableFrom(type) Then
						moduleTypes.Add(type)
					End If
				Next type
			End Sub

			Public ReadOnly Property Dependencies() As IEnumerable(Of String)
				Get
					Return innerDependencies
				End Get
			End Property

			Public Property Name() As String
				Get
					If innerName Is Nothing Then
						innerName = innerAssembly.FullName
					End If

					Return innerName
				End Get
				Set(ByVal value As String)
					innerName = Value
				End Set
			End Property

			Public Sub LoadServices(ByVal workItem As WorkItem)
				If loadedServices Then
					Return
				End If

				loadedServices = True
				EnsureModuleClassesExist(workItem)

				Try
					For Each moduleClass As IModule In moduleClasses
						moduleClass.AddServices()
						If Not traceSource Is Nothing Then
							traceSource.TraceInformation(My.Resources.AddServicesCalled, CObj(moduleClass).GetType())
						End If
					Next moduleClass

					For Each svc As ServiceMetadata In services
						If svc.AddOnDemand Then
							workItem.Services.AddOnDemand(svc.InstanceType, svc.RegistrationType)
							If Not traceSource Is Nothing Then
								traceSource.TraceInformation(My.Resources.ServiceAddedOnDemand, Name, svc.InstanceType)
							End If
						Else
							workItem.Services.AddNew(svc.InstanceType, svc.RegistrationType)
							If Not traceSource Is Nothing Then
								traceSource.TraceInformation(My.Resources.ServiceAdded, Name, svc.InstanceType)
							End If
						End If
					Next svc
				Catch ex As Exception
					ThrowModuleLoadException(ex)
				End Try
			End Sub

			Private Sub EnsureModuleClassesExist(ByVal workItem As WorkItem)
				If moduleClasses.Count = moduleTypes.Count Then
					Return
				End If

				Try
					For Each moduleType As Type In moduleTypes
						Dim aModule As IModule = CType(workItem.Items.AddNew(moduleType), IModule)
						moduleClasses.Add(aModule)
						If Not traceSource Is Nothing Then
							traceSource.TraceInformation(My.Resources.LogModuleAdded, moduleType)
						End If
					Next moduleType
				Catch ex As FileNotFoundException
					ThrowModuleReferenceException(ex)
				Catch ex As Exception
					ThrowModuleLoadException(ex)
				End Try
			End Sub

			Public Sub InitializeModuleClasses(ByVal workItem As WorkItem)
				If modulesInitialzed Then
					Return
				End If

				modulesInitialzed = True
				EnsureModuleClassesExist(workItem)

				Try
					For Each aModule As IModule In moduleClasses
						aModule.Load()
						If Not traceSource Is Nothing Then
							traceSource.TraceInformation(My.Resources.ModuleStartCalled, CObj(aModule).GetType())
						End If
					Next aModule
				Catch ex As FileNotFoundException
					ThrowModuleReferenceException(ex)
				Catch ex As Exception
					ThrowModuleLoadException(ex)
				End Try
			End Sub

			Public Sub InitializeWorkItemExtensions(ByVal workItem As WorkItem)
				If extensionsInitialized Then
					Return
				End If

				extensionsInitialized = True

				Dim svc As IWorkItemExtensionService = workItem.Services.Get(Of IWorkItemExtensionService)()

				If svc Is Nothing Then
					Return
				End If

				For Each kvp As KeyValuePair(Of Type, Type) In workItemExtensions
					svc.RegisterExtension(kvp.Key, kvp.Value)
				Next kvp

				For Each type As Type In workItemRootExtensions
					svc.RegisterRootExtension(type)
				Next type
			End Sub

			Public Sub NotifyOfLoadedModule(ByVal action As Action(Of LoadedModuleInfo))
				If notified Then
					Return
				End If

				notified = True
				action(ToLoadedModuleInfo())
			End Sub

			Public Function ToLoadedModuleInfo() As LoadedModuleInfo
				Return New LoadedModuleInfo(innerAssembly, Name, roles, innerDependencies)
			End Function

			Private Sub ThrowModuleLoadException(ByVal innerException As Exception)
				Throw New ModuleLoadException(Name, String.Format(CultureInfo.CurrentCulture, _
					My.Resources.FailedToLoadModule, innerAssembly.FullName, innerException.Message), _
					innerException)
			End Sub

			Private Sub ThrowModuleReferenceException(ByVal innerException As Exception)
				Throw New ModuleLoadException(Name, My.Resources.ReferencedAssemblyNotFound, innerException)
			End Sub
		End Class

		Private Class ServiceMetadata
			Public AddOnDemand As Boolean = False
			Public InstanceType As Type = Nothing
			Public RegistrationType As Type = Nothing

			Public Sub New(ByVal instanceType As Type, ByVal registrationType As Type, ByVal addOnDemand As Boolean)
				Me.InstanceType = instanceType
				Me.RegistrationType = registrationType
				Me.AddOnDemand = addOnDemand
			End Sub
		End Class

		Private Class ClassNameTraceSourceAttribute
			Inherits TraceSourceAttribute

			''' <summary>
			''' Initializes the attribute using the <see cref="IModuleLoaderService"/> 
			''' interface namespace as the source name.
			''' </summary>
			Public Sub New()
				MyBase.New(GetType(ModuleLoaderService).FullName)
			End Sub
		End Class

#End Region
	End Class
End Namespace
