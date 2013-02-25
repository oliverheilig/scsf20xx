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
Imports System.CodeDom
Imports System.CodeDom.Compiler
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Diagnostics
Imports System.IO
Imports System.Reflection
Imports System.Text
Imports Microsoft.CSharp
Imports Microsoft.Practices.CompositeUI.Configuration
Imports Microsoft.Practices.CompositeUI.Services
Imports Microsoft.Practices.CompositeUI.Utility

Namespace Services
	<TestClass()> _
	Public Class ModuleLoaderServiceFixture
#Region "private fields"

		Private Shared moduleTemplate As String = _
			"Imports System" & ControlChars.CrLf & _
			"Imports System.ComponentModel" & ControlChars.CrLf & _
			"Imports Microsoft.Practices.CompositeUI" & ControlChars.CrLf & _
			"Imports System.Diagnostics" & ControlChars.CrLf & _
			"#module#" & ControlChars.CrLf & _
			"#dependencies#" & ControlChars.CrLf & _
			 "Namespace TestModules" & ControlChars.CrLf & _
			"    Public Class #className#Class : Inherits ModuleInit" & ControlChars.CrLf & _
			"        Public Overrides Sub AddServices()" & ControlChars.CrLf & _
			"            Trace.Write(""#className#.AddServices"")" & ControlChars.CrLf & _
			"            Console.WriteLine(""#className#.AddServices"")" & ControlChars.CrLf & _
			"        End Sub" & ControlChars.CrLf & _
			"        Public Overrides Sub Load()" & ControlChars.CrLf & _
			"           Trace.Write(""#className#.Start"")" & ControlChars.CrLf & _
			"           Console.WriteLine(""#className#.Start"")" & ControlChars.CrLf & _
			"        End Sub" & ControlChars.CrLf & _
			"    End Class" & ControlChars.CrLf & _
			"End Namespace"

		Private Shared generatedAssemblies As Dictionary(Of String, Assembly) = New Dictionary(Of String, Assembly)()

#End Region

		Shared Sub New()
            AppDomain.CurrentDomain.SetData("APPBASE", Environment.CurrentDirectory)

            generatedAssemblies.Add("ModuleExposingServices", CompileFileAndLoadAssembly("Microsoft.Practices.CompositeUI.Tests.ModuleExposingServices.vb", ".\ModuleExposingServices1\ModuleExposingServices.dll"))

            generatedAssemblies.Add("ModuleExposingSameServices", CompileFileAndLoadAssembly("Microsoft.Practices.CompositeUI.Tests.ModuleExposingSameServices.vb", ".\ModuleExposingSameServices\ModuleExposingSameServices.dll"))

			CompileFile("Microsoft.Practices.CompositeUI.Tests.ModuleReferencedAssembly.vb", ".\ModuleReferencingAssembly\ModuleReferencedAssembly.dll")

            generatedAssemblies.Add("ModuleReferencingAssembly", CompileFileAndLoadAssembly("Microsoft.Practices.CompositeUI.Tests.ModuleReferencingAssembly.vb", ".\ModuleReferencingAssembly\ModuleReferencingAssembly.dll", ".\ModuleReferencingAssembly\ModuleReferencedAssembly.dll"))

            generatedAssemblies.Add("ModuleThrowingException", CompileFileAndLoadAssembly("Microsoft.Practices.CompositeUI.Tests.ModuleThrowingException.vb", ".\ModuleThrowingException\ModuleThrowingException.dll"))

            generatedAssemblies.Add("ModuleExposingOnlyServices", CompileFileAndLoadAssembly("Microsoft.Practices.CompositeUI.Tests.ModuleExposingOnlyServices.vb", ".\ModuleExposingOnlyServices\ModuleExposingOnlyServices.dll"))

            generatedAssemblies.Add("ModuleExposingDuplicatedServices", CompileFileAndLoadAssembly("Microsoft.Practices.CompositeUI.Tests.ModuleExposingDuplicatedServices.vb", ".\ModuleExposingDuplicatedServices\ModuleExposingDuplicatedServices.dll"))

            generatedAssemblies.Add("ModuleDependency1", CompileFileAndLoadAssembly("Microsoft.Practices.CompositeUI.Tests.ModuleDependency1.vb", ".\ModuleDependency1\ModuleDependency1.dll"))

            generatedAssemblies.Add("ModuleDependency2", CompileFileAndLoadAssembly("Microsoft.Practices.CompositeUI.Tests.ModuleDependency2.vb", ".\ModuleDependency2\ModuleDependency2.dll"))
		End Sub

		<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
		Public Sub NullWorkItemThrows()
			Dim loader As ModuleLoaderService = New ModuleLoaderService(Nothing)
			loader.Load(Nothing, New MockModuleInfo())
		End Sub

		<TestMethod(), ExpectedException(GetType(ModuleLoadException))> _
		Public Sub InitializationExceptionsAreWrapped()
			Dim mockContainer As WorkItem = New TestableRootWorkItem()
			Dim loader As ModuleLoaderService = New ModuleLoaderService(Nothing)

			loader.Load(mockContainer, New ModuleInfo(generatedAssemblies("ModuleThrowingException").CodeBase.Replace("file:///", "")))
		End Sub

		<TestMethod()> _
		Public Sub LoadSampleModule()
			Dim container As WorkItem = New TestableRootWorkItem()
			Dim loader As IModuleLoaderService = New ModuleLoaderService(Nothing)
			container.Services.Add(GetType(IModuleLoaderService), loader)
			Dim containerCount As Integer = GetItemCount(container)

			Dim info As ModuleInfo = New ModuleInfo()
			info.SetAssemblyFile(GenerateDynamicModule("SampleModule", "SampleModule"))

			Dim consoleOut As TextWriter = Console.Out
			Dim sb As StringBuilder = New StringBuilder()
			Console.SetOut(New StringWriter(sb))

			loader.Load(container, info)

			Assert.AreEqual(1, GetItemCount(container) - containerCount)

			Dim foundUs As Boolean = False

			For Each pair As KeyValuePair(Of String, Object) In container.Items
				If pair.Value.GetType().FullName = "TestModules.SampleModuleClass" Then
					foundUs = True
					Exit For
				End If
			Next pair

			Assert.IsTrue(foundUs)

			Console.SetOut(consoleOut)
		End Sub

		Private Function GetItemCount(ByVal container As WorkItem) As Integer
			Dim count As Integer = 0
			For Each item As Object In container.Items
				count += 1
			Next item
			Return count
		End Function

		<TestMethod(), ExpectedException(GetType(ModuleLoadException))> _
		Public Sub LoadModuleReferencingMissingAssembly()
			Dim mockContainer As WorkItem = New TestableRootWorkItem()
			Dim loader As ModuleLoaderService = New ModuleLoaderService(Nothing)

			Dim info As ModuleInfo = New ModuleInfo()
			info.SetAssemblyFile(generatedAssemblies("ModuleReferencingAssembly").CodeBase.Replace("file:///", ""))

            File.Delete(".\ModuleReferencingAssembly\ModuleReferencedAssembly.dll")

			loader.Load(mockContainer, info)
		End Sub

		<TestMethod()> _
		Public Sub LoadProfileWithAcyclicModuleDependencies()
			Dim assemblies As List(Of String) = New List(Of String)()

			' Create several modules with this dependency graph (X->Y meaning Y depends on X)
			' a->b, b->c, b->d, c->e, d->e, f
			assemblies.Add(GenerateDynamicModule("ModuleA", "ModuleA"))
			assemblies.Add(GenerateDynamicModule("ModuleB", "ModuleB", "ModuleA"))
			assemblies.Add(GenerateDynamicModule("ModuleC", "ModuleC", "ModuleB"))
			assemblies.Add(GenerateDynamicModule("ModuleD", "ModuleD", "ModuleB"))
			assemblies.Add(GenerateDynamicModule("ModuleE", "ModuleE", "ModuleC", "ModuleD"))
			assemblies.Add(GenerateDynamicModule("ModuleF", "ModuleF"))

			Dim Modules As ModuleInfo() = New ModuleInfo(assemblies.Count - 1) {}
			Dim i As Integer = 0
			Do While i < assemblies.Count
				Modules(i) = New ModuleInfo(assemblies(i))
				i += 1
			Loop

			Dim consoleOut As TextWriter = Console.Out

			Dim sb As StringBuilder = New StringBuilder()
			Console.SetOut(New StringWriter(sb))
			Dim mockContainer As WorkItem = New TestableRootWorkItem()
			Dim loader As ModuleLoaderService = New ModuleLoaderService(Nothing)
			loader.Load(mockContainer, Modules)

			Dim trace As List(Of String) = New List(Of String)(sb.ToString().Split(New String() {Constants.vbCrLf}, StringSplitOptions.RemoveEmptyEntries))
			Assert.AreEqual(12, trace.Count)
			Assert.IsTrue(trace.IndexOf("ModuleE.AddServices") > trace.IndexOf("ModuleC.AddServices"), "ModuleC must precede ModuleE")
			Assert.IsTrue(trace.IndexOf("ModuleE.AddServices") > trace.IndexOf("ModuleD.AddServices"), "ModuleD must precede ModuleE")
			Assert.IsTrue(trace.IndexOf("ModuleD.AddServices") > trace.IndexOf("ModuleB.AddServices"), "ModuleB must precede ModuleD")
			Assert.IsTrue(trace.IndexOf("ModuleC.AddServices") > trace.IndexOf("ModuleB.AddServices"), "ModuleB must precede ModuleC")
			Assert.IsTrue(trace.IndexOf("ModuleB.AddServices") > trace.IndexOf("ModuleA.AddServices"), "ModuleA must precede ModuleB")
			Assert.IsTrue(trace.Contains("ModuleF.AddServices"), "ModuleF must be loaded")
			Console.SetOut(consoleOut)
		End Sub

        <TestMethod(), ExpectedException(GetType(CyclicDependencyFoundException))> _
        Public Sub FailWhenLoadingModulesWithCyclicDependencies()
            Dim assemblies As List(Of String) = New List(Of String)()

            ' Create several modules with this dependency graph (X->Y meaning Y depends on X)
            ' 1->2, 2->3, 3->4, 4->5, 4->2
            assemblies.Add(GenerateDynamicModule("Module1", "Module1"))
            assemblies.Add(GenerateDynamicModule("Module2", "Module2", "Module1", "Module4"))
            assemblies.Add(GenerateDynamicModule("Module3", "Module3", "Module2"))
            assemblies.Add(GenerateDynamicModule("Module4", "Module4", "Module3"))
            assemblies.Add(GenerateDynamicModule("Module5", "Module5", "Module4"))

            Dim modules As ModuleInfo() = New ModuleInfo(assemblies.Count - 1) {}
            Dim i As Integer = 0
            Do While i < assemblies.Count
                modules(i) = New ModuleInfo(assemblies(i))
                i += 1
            Loop
            Dim mockContainer As WorkItem = New TestableRootWorkItem()
            Dim loader As ModuleLoaderService = New ModuleLoaderService(Nothing)
            loader.Load(mockContainer, modules)
        End Sub

		<TestMethod(), ExpectedException(GetType(ModuleLoadException))> _
		Public Sub FailWhenDependingOnMissingModule()
			Dim aModule As ModuleInfo = New ModuleInfo(GenerateDynamicModule("ModuleK", Nothing, "ModuleL"))

			Dim mockContainer As WorkItem = New TestableRootWorkItem()
			Dim loader As ModuleLoaderService = New ModuleLoaderService(Nothing)
			loader.Load(mockContainer, aModule)
		End Sub

		<TestMethod()> _
		Public Sub CanLoadAnonymousModulesWithDepedencies()
			Dim assemblies As List(Of String) = New List(Of String)()

			' Create several modules with this dependency graph (X->Y meaning Y depends on X)
			' a->b, b->c, b->d, c->e, d->e, f
			assemblies.Add(GenerateDynamicModule("ModuleX", "ModuleX"))
			assemblies.Add(GenerateDynamicModule("ModuleY", Nothing, "ModuleX"))
			assemblies.Add(GenerateDynamicModule("ModuleP", "ModuleP"))
			assemblies.Add(GenerateDynamicModule("ModuleQ", Nothing, "ModuleP"))

			Dim modules As ModuleInfo() = New ModuleInfo(assemblies.Count - 1) {}
			Dim i As Integer = 0
			Do While i < assemblies.Count
				modules(i) = New ModuleInfo(assemblies(i))
				i += 1
			Loop

			Dim consoleOut As TextWriter = Console.Out

			Dim sb As StringBuilder = New StringBuilder()
			Console.SetOut(New StringWriter(sb))
			Dim mockContainer As WorkItem = New TestableRootWorkItem()
			Dim loader As ModuleLoaderService = New ModuleLoaderService(Nothing)
			loader.Load(mockContainer, modules)

			Dim trace As List(Of String) = New List(Of String)(sb.ToString().Split(New String() {Constants.vbCrLf}, StringSplitOptions.RemoveEmptyEntries))
			Assert.AreEqual(8, trace.Count)
			Assert.IsTrue(trace.IndexOf("ModuleX.AddServices") < trace.IndexOf("ModuleY.AddServices"), "ModuleX must precede ModuleY")
			Assert.IsTrue(trace.IndexOf("ModuleP.AddServices") < trace.IndexOf("ModuleQ.AddServices"), "ModuleP must precede ModuleQ")
			Console.SetOut(consoleOut)

		End Sub

		<TestMethod(), ExpectedException(GetType(ModuleLoadException))> _
		Public Sub ThrowsIfAssemblyNotRelativeSolutionProfile()
			Dim container As WorkItem = New TestableRootWorkItem()
			Dim service As ModuleLoaderService = New ModuleLoaderService(Nothing)
			service.Load(container, New ModuleInfo("C:\module.dll"))
		End Sub

		<TestMethod(), ExpectedException(GetType(ModuleLoadException))> _
		Public Sub ThrowsIfAssemblyRelativeNotUnderRootSolutionProfile()
			Dim container As WorkItem = New TestableRootWorkItem()
			Dim service As ModuleLoaderService = New ModuleLoaderService(Nothing)
			service.Load(container, New ModuleInfo("..\..\module.dll"))
		End Sub

		<TestMethod()> _
		Public Sub LoadModuleWithServices()
			Dim compiledAssembly As System.Reflection.Assembly = generatedAssemblies("ModuleExposingServices")
			Dim container As WorkItem = New TestableRootWorkItem()
			Dim service As ModuleLoaderService = New ModuleLoaderService(Nothing)
			Dim info As ModuleInfo = New ModuleInfo(compiledAssembly.CodeBase.Replace("file:///", ""))

			service.Load(container, info)

			Assert.IsNotNull(container.Services.Get(compiledAssembly.GetType("ModuleExposingServices.SimpleService")))
			Assert.IsNotNull(container.Services.Get(compiledAssembly.GetType("ModuleExposingServices.ITestService")))
		End Sub

		<TestMethod(), ExpectedException(GetType(ModuleLoadException))> _
		Public Sub ModuleAddingDuplicatedServices()
			Dim moduleService As System.Reflection.Assembly = generatedAssemblies("ModuleExposingDuplicatedServices")

			Dim aModule As ModuleInfo = New ModuleInfo(moduleService.CodeBase.Replace("file:///", ""))

			Dim container As WorkItem = New TestableRootWorkItem()
			Dim service As ModuleLoaderService = New ModuleLoaderService(Nothing)
			service.Load(container, aModule)
		End Sub

		<TestMethod()> _
		Public Sub ServicesCanBeAddedOnDemand()
			Dim asm As System.Reflection.Assembly = generatedAssemblies("ModuleExposingServices")
			Dim aModule As ModuleInfo = New ModuleInfo(asm.CodeBase.Replace("file:///", ""))

			Dim container As WorkItem = New TestableRootWorkItem()
			Dim service As ModuleLoaderService = New ModuleLoaderService(Nothing)
			service.Load(container, aModule)

			Dim typeOnDemand As Type = asm.GetType("ModuleExposingServices.OnDemandService")
			Dim fldInfo As FieldInfo = typeOnDemand.GetField("ServiceCreated")
			Assert.IsFalse(CBool(fldInfo.GetValue(Nothing)), "The service was created.")

			container.Services.Get(typeOnDemand)

			Assert.IsTrue(CBool(fldInfo.GetValue(Nothing)), "The service was not created.")
		End Sub

		<TestMethod()> _
		Public Sub CanLoadModuleAssemblyWhichOnlyExposesServices()
			Dim asm As System.Reflection.Assembly = generatedAssemblies("ModuleExposingOnlyServices")
			Dim container As WorkItem = New TestableRootWorkItem()
			Dim service As ModuleLoaderService = New ModuleLoaderService(Nothing)
			service.Load(container, New ModuleInfo(asm.CodeBase.Replace("file:///", "")))

			Dim typeSimpleService As Type = asm.GetType("ModuleExposingOnlyServices.SimpleService")
			Dim typeITestService As Type = asm.GetType("ModuleExposingOnlyServices.ITestService")
			Assert.IsNotNull(container.Services.Get(typeSimpleService), "The SimpleService service was not loaded.")
			Assert.IsNotNull(container.Services.Get(typeITestService), "The ITestService service was not loaded.")
		End Sub

		<TestMethod()> _
		Public Sub CanLoadDependentModulesWithoutInitialization()
			Dim container As WorkItem = New TestableRootWorkItem()
			Dim service As ModuleLoaderService = New ModuleLoaderService(Nothing)
			service.Load(container, New ModuleInfo(generatedAssemblies("ModuleDependency2").CodeBase.Replace("file:///", "")), New ModuleInfo(generatedAssemblies("ModuleDependency1").CodeBase.Replace("file:///", "")))
		End Sub

#Region "Utilitary code for subprocedure CanGetModuleMetaDataFromAssembly"

		Private Class CanGetModuleMetaDataFromAssemblyUtilityProvider
			Private wasAddedField As Boolean
			Public Property WasAdded() As Boolean
				Get
					Return wasAddedField
				End Get
				Set(ByVal wasAddedArgument As Boolean)
					wasAddedField = wasAddedArgument
				End Set
			End Property

			Public Sub AddedHandler(ByVal sender As Object, ByVal e As Utility.DataEventArgs(Of Object))
				If e.Data.GetType().Name = "TestService" Then
					wasAddedField = True
				End If
			End Sub
		End Class

		Private CanGetModuleMetaDataFromAssemblyUtility As CanGetModuleMetaDataFromAssemblyUtilityProvider = New CanGetModuleMetaDataFromAssemblyUtilityProvider

#End Region

		<TestMethod()> _
		Public Sub CanGetModuleMetaDataFromAssembly()
			Dim asm As System.Reflection.Assembly = generatedAssemblies("ModuleExposingOnlyServices")
			Dim service As ModuleLoaderService = New ModuleLoaderService(Nothing)
			Dim wi As WorkItem = New TestableRootWorkItem()

			CanGetModuleMetaDataFromAssemblyUtility.WasAdded = False
			AddHandler wi.Services.Added, AddressOf CanGetModuleMetaDataFromAssemblyUtility.AddedHandler

			service.Load(wi, asm)

			Assert.IsTrue(CanGetModuleMetaDataFromAssemblyUtility.WasAdded)
		End Sub

		<TestMethod()> _
		Public Sub CanEnumerateLoadedModules()
			Dim compiledAssembly1 As System.Reflection.Assembly = generatedAssemblies("ModuleDependency1")
			Dim compiledAssembly2 As System.Reflection.Assembly = generatedAssemblies("ModuleDependency2")
			Dim wi As WorkItem = New TestableRootWorkItem()
			Dim service As ModuleLoaderService = New ModuleLoaderService(Nothing)
			service.Load(wi, compiledAssembly1)
			service.Load(wi, compiledAssembly2)

			Assert.AreEqual(2, service.LoadedModules.Count)

			Assert.AreSame(compiledAssembly1, service.LoadedModules(0).Assembly)
			Assert.AreEqual("module1", service.LoadedModules(0).Name)
			Assert.AreEqual(0, service.LoadedModules(0).Dependencies.Count)

			Assert.AreSame(compiledAssembly2, service.LoadedModules(1).Assembly)
			Assert.AreEqual("module2", service.LoadedModules(1).Name)
			Assert.AreEqual(1, service.LoadedModules(1).Dependencies.Count)
			Assert.AreEqual("module1", service.LoadedModules(1).Dependencies(0))
		End Sub

#Region "Utilitary code for subprocedure CanBeNotifiedOfAddedModules"

		Private Class CanBeNotifiedOfAddedModulesUtilityProvider
			Private moduleInfoField As LoadedModuleInfo
			Public Property ModuleInfo() As LoadedModuleInfo
				Get
					Return moduleInfoField
				End Get
				Set(ByVal moduleInfoArgument As LoadedModuleInfo)
					moduleInfoField = moduleInfoArgument
				End Set
			End Property

			Public Sub LoadedHandler(ByVal sender As Object, ByVal e As Utility.DataEventArgs(Of LoadedModuleInfo))
				moduleInfoField = e.Data
			End Sub
		End Class

		Private CanBeNotifiedOfAddedModulesUtility As CanBeNotifiedOfAddedModulesUtilityProvider = New CanBeNotifiedOfAddedModulesUtilityProvider

#End Region

		<TestMethod()> _
		Public Sub CanBeNotifiedOfAddedModules()
			Dim wi As WorkItem = New TestableRootWorkItem()
			Dim svc As IModuleLoaderService = New ModuleLoaderService(Nothing)
			Dim lmi As LoadedModuleInfo = Nothing
			Dim assm As System.Reflection.Assembly = generatedAssemblies("ModuleDependency1")

			AddHandler svc.ModuleLoaded, AddressOf CanBeNotifiedOfAddedModulesUtility.LoadedHandler

			svc.Load(wi, assm)
			lmi = CanBeNotifiedOfAddedModulesUtility.ModuleInfo

			Assert.IsNotNull(lmi)
			Assert.AreSame(assm, lmi.Assembly)
		End Sub

		Private Class MockApplication
			Inherits CabApplication(Of MockWorkItem)


			Protected Overrides Sub Start()
				Throw New Exception("The method or operation is not implemented.")
			End Sub
		End Class

		Private Class MockWorkItem
			Inherits WorkItem

		End Class

		Private Class MockModuleInfo
			Implements IModuleInfo

			Public ReadOnly Property AssemblyFile() As String Implements IModuleInfo.AssemblyFile
				Get
					Throw New Exception("The method or operation is not implemented.")
				End Get
			End Property

			Public ReadOnly Property UpdateLocation() As String Implements IModuleInfo.UpdateLocation
				Get
					Throw New Exception("The method or operation is not implemented.")
				End Get
			End Property

			Public ReadOnly Property AllowedRoles() As IList(Of String) Implements IModuleInfo.AllowedRoles
				Get
					Throw New Exception("The method or operation is not implemented.")
				End Get
			End Property
		End Class

#Region "Helper methods"

		Public Shared Function GenerateDynamicModule(ByVal assemblyName As String, ByVal moduleName As String, ByVal ParamArray dependencies As String()) As String
			Dim assemblyFile As String = assemblyName & ".dll"
			If (Not Directory.Exists(assemblyName)) Then
				Directory.CreateDirectory(assemblyName)
			End If
			Dim outpath As String = Path.Combine(assemblyName, assemblyFile)
			If File.Exists(outpath) Then
				File.Delete(outpath)
			End If

			' Create temporary module.
			Dim moduleCode As String = moduleTemplate.Replace("#className#", assemblyName)
			If Not moduleName Is Nothing AndAlso moduleName.Length > 0 Then
				moduleCode = moduleCode.Replace("#module#", "<Assembly: Microsoft.Practices.CompositeUI.Module(""" & moduleName & """)>")
			Else
				moduleCode = moduleCode.Replace("#module#", "")
			End If

			Dim depString As String = String.Empty
			For Each aModule As String In dependencies
				depString &= String.Format("<Assembly: ModuleDependency(""{0}"")>" & Constants.vbCrLf, aModule)
			Next aModule
			moduleCode = moduleCode.Replace("#dependencies#", depString)

			CompileCode(moduleCode, outpath)

			Return outpath
		End Function

        Public Shared Function CompileFileAndLoadAssembly(ByVal input As String, ByVal output As String, ByVal ParamArray references As String()) As System.Reflection.Assembly
            Return CompileFile(input, output, references).CompiledAssembly
        End Function

        Public Shared Function CompileFile(ByVal input As String, ByVal output As String, ByVal ParamArray references As String()) As CompilerResults
            CreateOutput(output)

            Dim referencedAssemblies As List(Of String) = New List(Of String)(references.Length + 3)

            referencedAssemblies.AddRange(references)
            referencedAssemblies.Add("System.dll")
            referencedAssemblies.Add(GetType(IModule).Assembly.CodeBase.Replace("file:///", ""))

            Dim codeProvider As VisualBasic.VBCodeProvider = New VisualBasic.VBCodeProvider()
            Dim cp As CompilerParameters = New CompilerParameters(referencedAssemblies.ToArray(), output)

            Using stream As Stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(input)
                If stream Is Nothing Then
                    Throw New ArgumentException("input")
                End If

                Dim reader As StreamReader = New StreamReader(stream)
                Dim source As String = reader.ReadToEnd()
                Dim results As CompilerResults = codeProvider.CompileAssemblyFromSource(cp, source)
                ThrowIfCompilerError(results)
                Return results
            End Using
        End Function

		Public Shared Function CompileCode(ByVal code As String, ByVal output As String) As System.Reflection.Assembly
			CreateOutput(output)
			Dim unit As CodeCompileUnit = New CodeSnippetCompileUnit(code)
			unit.ReferencedAssemblies.Add("System")
			unit.ReferencedAssemblies.Add("Microsoft.Practices.CompositeUI")

			Dim codeProvider As VisualBasic.VBCodeProvider = New VisualBasic.VBCodeProvider()
			Dim results As CompilerResults = codeProvider.CompileAssemblyFromSource(New CompilerParameters(New String() {"System.dll", GetType(IModule).Assembly.CodeBase.Replace("file:///", "")}, output), code)

			ThrowIfCompilerError(results)

			Return results.CompiledAssembly
		End Function

		Public Shared Sub CreateOutput(ByVal output As String)
			Dim dir As String = Path.GetDirectoryName(output)
			If (Not Directory.Exists(dir)) Then
				Directory.CreateDirectory(dir)
			End If
		End Sub


		Public Shared Sub ThrowIfCompilerError(ByVal results As CompilerResults)
			If results.Errors.HasErrors Then
				Dim sb As StringBuilder = New StringBuilder()
				sb.AppendLine("Compilation failed.")
				For Each anError As CompilerError In results.Errors
					sb.AppendLine(anError.ToString())
				Next anError
				Assert.IsFalse(results.Errors.HasErrors, sb.ToString())
			End If
		End Sub

#End Region
	End Class
End Namespace
