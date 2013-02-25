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
//===============================================================================
// Microsoft patterns & practices
// CompositeUI Application Block
//===============================================================================
// Copyright � Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Microsoft.Practices.CompositeUI.Configuration;
using Microsoft.Practices.CompositeUI.Services;

namespace Microsoft.Practices.CompositeUI.Tests.Services
{
	[TestClass]
	public class ReflectionModuleEnumeratorFixture
	{
		private static ReflectionModuleEnumerator enumerator;
		private static string location;

		static ReflectionModuleEnumeratorFixture()
		{			
			location = AppDomain.CurrentDomain.BaseDirectory;

			ModuleLoaderServiceFixture.CompileFile(
				"Microsoft.Practices.CompositeUI.Tests.Mocks.Src.ReflectionModule1.cs",
				@".\Reflection1\Module1.dll");

			ModuleLoaderServiceFixture.CompileFile(
				"Microsoft.Practices.CompositeUI.Tests.Mocks.Src.ReflectionModule2.cs",
				@".\Reflection2\Module2.dll");

			ModuleLoaderServiceFixture.CompileFile(
				"Microsoft.Practices.CompositeUI.Tests.Mocks.Src.ReflectionModule3.cs",
				@".\Reflection2\Recurse\Module3.dll");
		}

		[TestMethod]
		public void BasePathDefaultIsApplicationPath()
		{
            enumerator = new ReflectionModuleEnumerator();
			Assert.AreEqual(location, enumerator.BasePath);
		}

		[TestMethod]
		public void EnumeratorFindsModules()
		{
            enumerator = new ReflectionModuleEnumerator();
			enumerator.BasePath = Path.Combine(location, "Reflection1");

			IModuleInfo[] modules = enumerator.EnumerateModules();

			Assert.AreEqual(1, modules.Length);
			Assert.AreEqual(Path.Combine(enumerator.BasePath, "Module1.dll"), modules[0].AssemblyFile);
		}

		[TestMethod]
		public void EnumeratorSearchDirectoryRecursively()
		{
            enumerator = new ReflectionModuleEnumerator();
			enumerator.BasePath = Path.Combine(location, "Reflection2");

			IModuleInfo[] modules = enumerator.EnumerateModules();

			Assert.AreEqual(2, modules.Length);
		}


	}
}
