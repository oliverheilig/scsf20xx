//===============================================================================
// Microsoft patterns & practices
// Smart Client Software Factory 2010
//===============================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===============================================================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading;
using GlobalBank.Infrastructure.Library.Services;
using GlobalBank.UnitTest.Library;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Configuration;
using Microsoft.Practices.CompositeUI.Services;
using Microsoft.Practices.CompositeUI.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GlobalBank.Infrastructure.Library.Tests.Services
{
	[TestClass]
	public class DependentModuleLoaderServiceFixture
	{
		[TestMethod]
		[ExpectedException(typeof (ArgumentNullException))]
		public void NullWorkItemThrows()
		{
			DependentModuleLoaderService loader = new DependentModuleLoaderService(null);
			loader.Load(null, new MockModuleInfo());
		}

		[TestMethod]
		[ExpectedException(typeof (ModuleLoadException))]
		public void InitializationExceptionsAreWrapped()
		{
			WorkItem mockContainer = new TestableRootWorkItem();
			DependentModuleLoaderService loader = new DependentModuleLoaderService(null);

			loader.Load(mockContainer,
			            new ModuleInfo(
			            	ModuleCompilerHelper.GeneratedAssemblies["ModuleThrowingException"].CodeBase.Replace(@"file:///", "")));
		}

		[TestMethod]
		public void LoadSampleModule()
		{
			WorkItem container = new TestableRootWorkItem();
			IModuleLoaderService loader = new DependentModuleLoaderService(null);
			container.Services.Add(typeof (IModuleLoaderService), loader);
			int originalCount = container.Items.Count;

			ModuleInfo info = new ModuleInfo();
			info.SetAssemblyFile(ModuleCompilerHelper.GenerateDynamicModule("SampleModule", "SampleModule"));

			TextWriter consoleOut = Console.Out;
			StringBuilder sb = new StringBuilder();
			Console.SetOut(new StringWriter(sb));

			loader.Load(container, info);

			Assert.AreEqual(1, container.Items.Count - originalCount);

			bool foundUs = false;

			foreach (KeyValuePair<string, object> pair in container.Items)
			{
				if (pair.Value.GetType().FullName == "TestModules.SampleModuleClass")
				{
					foundUs = true;
					break;
				}
			}

			Assert.IsTrue(foundUs);

			Console.SetOut(consoleOut);
		}

		[TestMethod]
		[ExpectedException(typeof (ModuleLoadException))]
		public void LoadModuleReferencingMissingAssembly()
		{
			WorkItem mockContainer = new TestableRootWorkItem();
			DependentModuleLoaderService loader = new DependentModuleLoaderService(null);

			ModuleInfo info = new ModuleInfo();
			info.SetAssemblyFile(
				ModuleCompilerHelper.GeneratedAssemblies["ModuleReferencingAssembly"].CodeBase.Replace(@"file:///", ""));

            File.Delete(@".\ModuleReferencingAssembly\ModuleReferencedAssembly.dll");

			loader.Load(mockContainer, info);
		}

		[TestMethod]
		public void LoadProfileWithAcyclicModuleDependencies()
		{
			List<string> assemblies = new List<string>();

			// Create several modules with this dependency graph (X->Y meaning Y depends on X)
			// a->b, b->c, b->d, c->e, d->e, f
			assemblies.Add(ModuleCompilerHelper.GenerateDynamicModule("ModuleA", "ModuleA"));
			assemblies.Add(ModuleCompilerHelper.GenerateDynamicModule("ModuleB", "ModuleB", "ModuleA"));
			assemblies.Add(ModuleCompilerHelper.GenerateDynamicModule("ModuleC", "ModuleC", "ModuleB"));
			assemblies.Add(ModuleCompilerHelper.GenerateDynamicModule("ModuleD", "ModuleD", "ModuleB"));
			assemblies.Add(ModuleCompilerHelper.GenerateDynamicModule("ModuleE", "ModuleE", "ModuleC", "ModuleD"));
			assemblies.Add(ModuleCompilerHelper.GenerateDynamicModule("ModuleF", "ModuleF"));

			ModuleInfo[] Modules = new ModuleInfo[assemblies.Count];
			for (int i = 0; i < assemblies.Count; i++)
			{
				Modules[i] = new ModuleInfo(assemblies[i]);
			}

			TextWriter consoleOut = Console.Out;

			StringBuilder sb = new StringBuilder();
			Console.SetOut(new StringWriter(sb));
			WorkItem mockContainer = new TestableRootWorkItem();
			DependentModuleLoaderService loader = new DependentModuleLoaderService(null);
			loader.Load(mockContainer, Modules);

			List<string> trace =
				new List<string>(sb.ToString().Split(new string[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries));
			Assert.AreEqual(12, trace.Count);
			Assert.IsTrue(trace.IndexOf("ModuleE.AddServices") > trace.IndexOf("ModuleC.AddServices"),
			              "ModuleC must precede ModuleE");
			Assert.IsTrue(trace.IndexOf("ModuleE.AddServices") > trace.IndexOf("ModuleD.AddServices"),
			              "ModuleD must precede ModuleE");
			Assert.IsTrue(trace.IndexOf("ModuleD.AddServices") > trace.IndexOf("ModuleB.AddServices"),
			              "ModuleB must precede ModuleD");
			Assert.IsTrue(trace.IndexOf("ModuleC.AddServices") > trace.IndexOf("ModuleB.AddServices"),
			              "ModuleB must precede ModuleC");
			Assert.IsTrue(trace.IndexOf("ModuleB.AddServices") > trace.IndexOf("ModuleA.AddServices"),
			              "ModuleA must precede ModuleB");
			Assert.IsTrue(trace.Contains("ModuleF.AddServices"), "ModuleF must be loaded");
			Console.SetOut(consoleOut);
		}

		[TestMethod]
		[ExpectedException(typeof (CyclicDependencyFoundException))]
		public void FailWhenLoadingModulesWithCyclicDependencies()
		{
			List<string> assemblies = new List<string>();

			// Create several modules with this dependency graph (X->Y meaning Y depends on X)
			// 1->2, 2->3, 3->4, 4->5, 4->2
			assemblies.Add(ModuleCompilerHelper.GenerateDynamicModule("Module1", "Module1"));
			assemblies.Add(ModuleCompilerHelper.GenerateDynamicModule("Module2", "Module2", "Module1", "Module4"));
			assemblies.Add(ModuleCompilerHelper.GenerateDynamicModule("Module3", "Module3", "Module2"));
			assemblies.Add(ModuleCompilerHelper.GenerateDynamicModule("Module4", "Module4", "Module3"));
			assemblies.Add(ModuleCompilerHelper.GenerateDynamicModule("Module5", "Module5", "Module4"));

			ModuleInfo[] modules = new ModuleInfo[assemblies.Count];
			for (int i = 0; i < assemblies.Count; i++)
			{
				modules[i] = new ModuleInfo(assemblies[i]);
			}
			WorkItem mockContainer = new TestableRootWorkItem();
			DependentModuleLoaderService loader = new DependentModuleLoaderService(null);
			loader.Load(mockContainer, modules);
		}

		[TestMethod]
		[ExpectedException(typeof (ModuleLoadException))]
		public void FailWhenDependingOnMissingModule()
		{
			ModuleInfo module = new ModuleInfo(ModuleCompilerHelper.GenerateDynamicModule("ModuleK", null, "ModuleL"));

			WorkItem mockContainer = new TestableRootWorkItem();
			DependentModuleLoaderService loader = new DependentModuleLoaderService(null);
			loader.Load(mockContainer, module);
		}

		[TestMethod]
		public void CanLoadAnonymousModulesWithDepedencies()
		{
			List<string> assemblies = new List<string>();

			// Create several modules with this dependency graph (X->Y meaning Y depends on X)
			// a->b, b->c, b->d, c->e, d->e, f
			assemblies.Add(ModuleCompilerHelper.GenerateDynamicModule("ModuleX", "ModuleX"));
			assemblies.Add(ModuleCompilerHelper.GenerateDynamicModule("ModuleY", null, "ModuleX"));
			assemblies.Add(ModuleCompilerHelper.GenerateDynamicModule("ModuleP", "ModuleP"));
			assemblies.Add(ModuleCompilerHelper.GenerateDynamicModule("ModuleQ", null, "ModuleP"));

			ModuleInfo[] modules = new ModuleInfo[assemblies.Count];
			for (int i = 0; i < assemblies.Count; i++)
			{
				modules[i] = new ModuleInfo(assemblies[i]);
			}

			TextWriter consoleOut = Console.Out;

			StringBuilder sb = new StringBuilder();
			Console.SetOut(new StringWriter(sb));
			WorkItem mockContainer = new TestableRootWorkItem();
			DependentModuleLoaderService loader = new DependentModuleLoaderService(null);
			loader.Load(mockContainer, modules);

			List<string> trace =
				new List<string>(sb.ToString().Split(new string[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries));
			Assert.AreEqual(8, trace.Count);
			Assert.IsTrue(trace.IndexOf("ModuleX.AddServices") < trace.IndexOf("ModuleY.AddServices"),
			              "ModuleX must precede ModuleY");
			Assert.IsTrue(trace.IndexOf("ModuleP.AddServices") < trace.IndexOf("ModuleQ.AddServices"),
			              "ModuleP must precede ModuleQ");
			Console.SetOut(consoleOut);
		}

		// TODO: Bad test! Bad!
		[TestMethod]
		[ExpectedException(typeof (ModuleLoadException))]
		public void ThrowsIfAssemblyNotRelativeSolutionProfile()
		{
			WorkItem container = new TestableRootWorkItem();
			DependentModuleLoaderService service = new DependentModuleLoaderService(null);
			service.Load(container, new ModuleInfo(@"C:\module.dll"));
		}

		// TODO: Also bad!
		[TestMethod]
		[ExpectedException(typeof (ModuleLoadException))]
		public void ThrowsIfAssemblyRelativeNotUnderRootSolutionProfile()
		{
			WorkItem container = new TestableRootWorkItem();
			DependentModuleLoaderService service = new DependentModuleLoaderService(null);
			service.Load(container, new ModuleInfo(@"..\..\module.dll"));
		}

		[TestMethod]
		public void LoadModuleWithServices()
		{
			Assembly compiledAssembly = ModuleCompilerHelper.GeneratedAssemblies["ModuleExposingServices"];
			WorkItem container = new TestableRootWorkItem();
			DependentModuleLoaderService service = new DependentModuleLoaderService(null);
			ModuleInfo info = new ModuleInfo(compiledAssembly.CodeBase.Replace(@"file:///", ""));

			service.Load(container, info);

			Assert.IsNotNull(container.Services.Get(compiledAssembly.GetType("ModuleExposingServices.SimpleService")));
			Assert.IsNotNull(container.Services.Get(compiledAssembly.GetType("ModuleExposingServices.ITestService")));
		}

		[TestMethod]
		[ExpectedException(typeof (ModuleLoadException))]
		public void ModuleAddingDuplicatedServices()
		{
			Assembly moduleService = ModuleCompilerHelper.GeneratedAssemblies["ModuleExposingDuplicatedServices"];

			ModuleInfo module = new ModuleInfo(moduleService.CodeBase.Replace(@"file:///", ""));

			WorkItem container = new TestableRootWorkItem();
			DependentModuleLoaderService service = new DependentModuleLoaderService(null);
			service.Load(container, module);
		}

		[TestMethod]
		public void ServicesCanBeAddedOnDemand()
		{
			Assembly asm = ModuleCompilerHelper.GeneratedAssemblies["ModuleExposingServices"];
			ModuleInfo module = new ModuleInfo(asm.CodeBase.Replace(@"file:///", ""));

			WorkItem container = new TestableRootWorkItem();
			DependentModuleLoaderService service = new DependentModuleLoaderService(null);
			service.Load(container, module);

			Type typeOnDemand = asm.GetType("ModuleExposingServices.OnDemandService");
			FieldInfo fldInfo = typeOnDemand.GetField("ServiceCreated");
			Assert.IsFalse((bool) fldInfo.GetValue(null), "The service was created.");

			container.Services.Get(typeOnDemand);

			Assert.IsTrue((bool) fldInfo.GetValue(null), "The service was not created.");
		}

		[TestMethod]
		public void CanLoadModuleAssemblyWhichOnlyExposesServices()
		{
			Assembly asm = ModuleCompilerHelper.GeneratedAssemblies["ModuleExposingOnlyServices"];
			WorkItem container = new TestableRootWorkItem();
			DependentModuleLoaderService service = new DependentModuleLoaderService(null);
			service.Load(container, new ModuleInfo(asm.CodeBase.Replace(@"file:///", "")));

			Type typeSimpleService = asm.GetType("ModuleExposingOnlyServices.SimpleService");
			Type typeITestService = asm.GetType("ModuleExposingOnlyServices.ITestService");
			Assert.IsNotNull(container.Services.Get(typeSimpleService), "The SimpleService service was not loaded.");
			Assert.IsNotNull(container.Services.Get(typeITestService), "The ITestService service was not loaded.");
		}

		[TestMethod]
		public void CanLoadDependentModulesWithoutInitialization()
		{
			WorkItem container = new TestableRootWorkItem();
			DependentModuleLoaderService service = new DependentModuleLoaderService(null);
			service.Load(container,
			             new ModuleInfo(
			             	ModuleCompilerHelper.GeneratedAssemblies["ModuleDependency2"].CodeBase.Replace(@"file:///", "")),
			             new ModuleInfo(
			             	ModuleCompilerHelper.GeneratedAssemblies["ModuleDependency1"].CodeBase.Replace(@"file:///", "")));
		}

		// TODO: Redundant with CanLoadModuleAssemblyWhichOnlyExposesServices()
		[TestMethod]
		public void CanGetModuleMetaDataFromAssembly()
		{
			Assembly asm = ModuleCompilerHelper.GeneratedAssemblies["ModuleExposingOnlyServices"];
			DependentModuleLoaderService service = new DependentModuleLoaderService(null);
			WorkItem wi = new TestableRootWorkItem();

			bool wasAdded = false;

			wi.Services.Added += delegate(object sender, DataEventArgs<object> e)
			                     	{
			                     		if (e.Data.GetType().Name == "TestService")
			                     			wasAdded = true;
			                     	};

			service.Load(wi, asm);

			Assert.IsTrue(wasAdded);
		}

		[TestMethod]
		public void CanEnumerateLoadedModules()
		{
			Assembly compiledAssembly1 = ModuleCompilerHelper.GeneratedAssemblies["ModuleDependency1"];
			Assembly compiledAssembly2 = ModuleCompilerHelper.GeneratedAssemblies["ModuleDependency2"];
			WorkItem wi = new TestableRootWorkItem();
			DependentModuleLoaderService service = new DependentModuleLoaderService(null);
			service.Load(wi, compiledAssembly1);
			service.Load(wi, compiledAssembly2);

			Assert.AreEqual(2, service.LoadedModules.Count);

			Assert.AreSame(compiledAssembly1, service.LoadedModules[0].Assembly);
			Assert.AreEqual("module1", service.LoadedModules[0].Name);
			Assert.AreEqual(0, service.LoadedModules[0].Dependencies.Count);

			Assert.AreSame(compiledAssembly2, service.LoadedModules[1].Assembly);
			Assert.AreEqual("module2", service.LoadedModules[1].Name);
			Assert.AreEqual(1, service.LoadedModules[1].Dependencies.Count);
			Assert.AreEqual("module1", service.LoadedModules[1].Dependencies[0]);
		}

		[TestMethod]
		public void CanBeNotifiedOfAddedModules()
		{
			WorkItem wi = new TestableRootWorkItem();
			IModuleLoaderService svc = new DependentModuleLoaderService(null);
			LoadedModuleInfo lmi = null;
			Assembly assembly = ModuleCompilerHelper.GeneratedAssemblies["ModuleDependency1"];

			svc.ModuleLoaded += delegate(object sender, DataEventArgs<LoadedModuleInfo> e)
			                    	{
			                    		lmi = e.Data;
			                    	};

			svc.Load(wi, assembly);

			Assert.IsNotNull(lmi);
			Assert.AreSame(assembly, lmi.Assembly);
		}

		[TestMethod]
		public void LoaderExcludesModulesBasedOnRoles()
		{
			IPrincipal oldPrincipal = Thread.CurrentPrincipal;
			TextWriter originalConsoleOut = Console.Out;
			StringBuilder sb = new StringBuilder();
			Console.SetOut(new StringWriter(sb));

			try
			{
				GenericIdentity identity = new GenericIdentity("Bob");
				GenericPrincipal principal = new GenericPrincipal(identity, new string[] {"Bar"});
				Thread.CurrentPrincipal = principal;
				WorkItem wi = new TestableRootWorkItem();
				IModuleLoaderService svc = new DependentModuleLoaderService(null);
				string moduleAFilename = ModuleCompilerHelper.GenerateDynamicModule("ModuleAAA", "ModuleA");
				string moduleBFilename = ModuleCompilerHelper.GenerateDynamicModule("ModuleAAB", "ModuleB");

				ModuleInfo[] modules = new ModuleInfo[2];
				modules[0] = new ModuleInfo(moduleAFilename);
				modules[0].AddRoles("Foo");
				modules[1] = new ModuleInfo(moduleBFilename);
				modules[1].AddRoles("Bar");

				svc.Load(wi, modules);

				Assert.AreEqual(1, svc.LoadedModules.Count);
				Assert.AreEqual("ModuleB", svc.LoadedModules[0].Name);
			}
			finally
			{
				Thread.CurrentPrincipal = oldPrincipal;
				Console.SetOut(originalConsoleOut);
			}
		}

		[TestMethod]
		public void LoaderDoesNotLoadModulesExcludedBasedOnRole()
		{
			IPrincipal oldPrincipal = Thread.CurrentPrincipal;
			StringBuilder sb = new StringBuilder();

			using (ConsoleHelper consoleHelper = new ConsoleHelper())
			{
				try
				{
					GenericIdentity identity = new GenericIdentity("Bob");
					GenericPrincipal principal = new GenericPrincipal(identity, new string[] {"Bar", "Baz"});
					Thread.CurrentPrincipal = principal;
					WorkItem wi = new TestableRootWorkItem();
					IModuleLoaderService svc = new DependentModuleLoaderService(null);
					string moduleAFilename = Guid.NewGuid().ToString() + ".dll";
					string moduleBFilename = ModuleCompilerHelper.GenerateDynamicModule("ModuleAAC", "ModuleB");

					ModuleInfo[] modules = new ModuleInfo[2];
					modules[0] = new ModuleInfo(moduleAFilename);
					modules[0].AddRoles("Foo");
					modules[1] = new ModuleInfo(moduleBFilename);
					modules[1].AddRoles("Bar");
					modules[1].AddRoles("Baz");

					svc.Load(wi, modules);

					Assert.AreEqual(1, svc.LoadedModules.Count);
					Assert.AreEqual("ModuleB", svc.LoadedModules[0].Name);
				}
				finally
				{
					Thread.CurrentPrincipal = oldPrincipal;
				}
			}
		}

		[TestMethod]
		public void WorkItemExtensionsAreInitializedBeforeModuleInitClasses()
		{
			WorkItem wi = new TestableRootWorkItem();
			IModuleLoaderService svc = new DependentModuleLoaderService(null);
			Assembly assembly = ModuleCompilerHelper.GeneratedAssemblies["WorkItemExtension"];
			List<string> output;

			wi.Services.AddNew<WorkItemExtensionService, IWorkItemExtensionService>();

			using (ConsoleHelper consoleHelper = new ConsoleHelper())
			{
				svc.Load(wi, assembly);
				output = new List<string>(consoleHelper.GetOutput());
			}

			Assert.IsTrue(output.Contains("Module..ctor()"));
			Assert.IsTrue(output.Contains("Module.Load()"));
			Assert.IsTrue(output.Contains("MyWorkItem..ctor()"));
			Assert.IsTrue(output.Contains("WorkItemExt..ctor()"));
			Assert.IsTrue(output.Contains("WorkItemExt.OnInitialized()"));
			Assert.IsTrue(output.Contains("WorkItemExt.OnActivated()"));
		}

		[TestMethod]
		public void AddServicesCalledBeforeLoad()
		{
			WorkItem wi = new TestableRootWorkItem();
			IModuleLoaderService svc = new DependentModuleLoaderService(null);
			Assembly assembly = ModuleCompilerHelper.GeneratedAssemblies["WorkItemExtension"];
			List<string> output;

			wi.Services.AddNew<WorkItemExtensionService, IWorkItemExtensionService>();

			using (ConsoleHelper consoleHelper = new ConsoleHelper())
			{
				svc.Load(wi, assembly);
				output = new List<string>(consoleHelper.GetOutput());
			}

			Assert.IsTrue(output.Contains("Module..ctor()"));
			Assert.IsTrue(output.Contains("Module.AddServices()"));
			Assert.IsTrue(output.Contains("Module.Load()"));
			Assert.IsTrue(output.IndexOf("Module.AddServices()") < output.IndexOf("Module.Load()"));
		}

		[TestMethod]
		public void WillNotUseReflectionWhenGivenDependentModuleInfos()
		{
			WorkItem wi = new TestableRootWorkItem();
			DependentModuleLoaderService service = new DependentModuleLoaderService(null);
			Assembly assembly1 = ModuleCompilerHelper.GeneratedAssemblies["ModuleDependency1"];
			Assembly assembly2 = ModuleCompilerHelper.GeneratedAssemblies["ModuleDependency2"];
			string filename1 = assembly1.CodeBase.Replace(@"file:///", "");
			string filename2 = assembly2.CodeBase.Replace(@"file:///", "");

			DependentModuleInfo[] dmis = new DependentModuleInfo[2];
			dmis[0] = new DependentModuleInfo(filename1);
			dmis[0].Dependencies.Add("Flarble2");
			dmis[0].SetName("Flarble1");
			dmis[1] = new DependentModuleInfo(filename2);
			dmis[1].SetName("Flarble2");

			service.Load(wi, dmis);

			Assert.AreSame(assembly1, service.LoadedModules[0].Assembly);
			Assert.AreEqual("Flarble1", service.LoadedModules[0].Name);
			Assert.AreEqual(1, service.LoadedModules[0].Dependencies.Count);
			Assert.AreEqual("Flarble2", service.LoadedModules[0].Dependencies[0]);

			Assert.AreSame(assembly2, service.LoadedModules[1].Assembly);
			Assert.AreEqual("Flarble2", service.LoadedModules[1].Name);
			Assert.AreEqual(0, service.LoadedModules[1].Dependencies.Count);
		}

		#region Helper Classes

		class MockApplication : CabApplication<MockWorkItem>
		{
			protected override void Start()
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}

		class MockWorkItem : WorkItem
		{
		}

		class MockModuleInfo : IModuleInfo
		{
			public string AssemblyFile
			{
				get { throw new Exception("The method or operation is not implemented."); }
			}

			public string UpdateLocation
			{
				get { throw new Exception("The method or operation is not implemented."); }
			}

			public IList<string> AllowedRoles
			{
				get { throw new Exception("The method or operation is not implemented."); }
			}
		}

		#endregion
	}
}