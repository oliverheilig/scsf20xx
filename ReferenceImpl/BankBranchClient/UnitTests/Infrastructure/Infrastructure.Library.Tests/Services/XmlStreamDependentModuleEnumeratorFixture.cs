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
using System.Reflection;
using GlobalBank.Infrastructure.Library.Services;
using Microsoft.Practices.CompositeUI.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GlobalBank.Infrastructure.Library.Tests.Services
{
	[TestClass]
	public class XmlStreamDependentModuleEnumeratorFixture
	{
		[TestMethod]
		public void EnumeratorReturnsEmptyArrayWhenPassedNullData()
		{
			XmlStreamDependentModuleEnumerator enumerator = new XmlStreamDependentModuleEnumerator();
			enumerator.ModuleInfoStore = new MockModuleInfoStore();

			IModuleInfo[] results = enumerator.EnumerateModules();

			Assert.IsNotNull(results);
			Assert.AreEqual(0, results.Length);
		}

		[TestMethod]
		public void EnumeratorReturnsEmptyArrayWhenPassedEmptyString()
		{
			XmlStreamDependentModuleEnumerator enumerator = new XmlStreamDependentModuleEnumerator();
			enumerator.ModuleInfoStore = new MockModuleInfoStore("");

			IModuleInfo[] results = enumerator.EnumerateModules();

			Assert.IsNotNull(results);
			Assert.AreEqual(0, results.Length);
		}

		[TestMethod]
		public void PassingV1XmlDoesNotThrow()
		{
			string infoXml = @"<SolutionProfile xmlns='http://schemas.microsoft.com/pag/cab-profile' />";
			XmlStreamDependentModuleEnumerator enumerator = new XmlStreamDependentModuleEnumerator();
			enumerator.ModuleInfoStore = new MockModuleInfoStore(infoXml);

			IModuleInfo[] results = enumerator.EnumerateModules();

			Assert.IsNotNull(results);
			Assert.AreEqual(0, results.Length);
		}

		[TestMethod]
		[ExpectedException(typeof (InvalidOperationException))]
		public void PassingInvalidXmlThrows()
		{
			string infoXml = @"<InvalidSolutionProfile xmlns='http://schemas.microsoft.com/pag/cab-profile' />";
			XmlStreamDependentModuleEnumerator enumerator = new XmlStreamDependentModuleEnumerator();
			enumerator.ModuleInfoStore = new MockModuleInfoStore(infoXml);

			enumerator.EnumerateModules();
		}

		[TestMethod]
		public void EnumeratorReturnsSingleModuleListedInV1Xml()
		{
			string infoXml =
				@"
<SolutionProfile xmlns='http://schemas.microsoft.com/pag/cab-profile'>
  <Modules>
		<ModuleInfo AssemblyFile='Bogus.dll' />
  </Modules>
</SolutionProfile>";

			XmlStreamDependentModuleEnumerator enumerator = new XmlStreamDependentModuleEnumerator();
			enumerator.ModuleInfoStore = new MockModuleInfoStore(infoXml);

			IModuleInfo[] results = enumerator.EnumerateModules();

			Assert.IsNotNull(results);
			Assert.AreEqual(1, results.Length);
			Assert.AreEqual("Bogus.dll", results[0].AssemblyFile);
			Assert.IsNotNull(results[0].AllowedRoles);
			Assert.AreEqual(0, results[0].AllowedRoles.Count);
			Assert.IsNull(results[0].UpdateLocation);
		}

		[TestMethod]
		public void EnumeratorUnderstandsV1Roles()
		{
			string infoXml =
				@"
<SolutionProfile xmlns='http://schemas.microsoft.com/pag/cab-profile'>
  <Modules>
		<ModuleInfo AssemblyFile='Bogus.dll'>
			<Roles>
				<Role Allow='Administrators' />
			</Roles>
		</ModuleInfo>
  </Modules>
</SolutionProfile>";

			XmlStreamDependentModuleEnumerator enumerator = new XmlStreamDependentModuleEnumerator();
			enumerator.ModuleInfoStore = new MockModuleInfoStore(infoXml);

			IModuleInfo[] results = enumerator.EnumerateModules();

			Assert.IsNotNull(results);
			Assert.IsNotNull(results[0].AllowedRoles);
			Assert.AreEqual(1, results[0].AllowedRoles.Count);
			Assert.AreEqual("Administrators", results[0].AllowedRoles[0]);
		}

		[TestMethod]
		public void EnumeratorReturnsSingleModuleListedInV2Xml()
		{
			string infoXml =
				@"
<SolutionProfile xmlns='http://schemas.microsoft.com/pag/cab-profile/2.0'>
	<Section>
		<Modules>
			<ModuleInfo AssemblyFile='Bogus.dll' />
		</Modules>
	</Section>
</SolutionProfile>";

			XmlStreamDependentModuleEnumerator enumerator = new XmlStreamDependentModuleEnumerator();
			enumerator.ModuleInfoStore = new MockModuleInfoStore(infoXml);

			IModuleInfo[] results = enumerator.EnumerateModules();

			Assert.IsNotNull(results);
			Assert.AreEqual(1, results.Length);
			Assert.AreEqual("Bogus.dll", results[0].AssemblyFile);
			Assert.IsNotNull(results[0].AllowedRoles);
			Assert.AreEqual(0, results[0].AllowedRoles.Count);
			Assert.IsNull(results[0].UpdateLocation);
		}

		[TestMethod]
		public void EnumeratorUnderstandsV2Roles()
		{
			string infoXml =
				@"
<SolutionProfile xmlns='http://schemas.microsoft.com/pag/cab-profile/2.0'>
	<Section>
		<Modules>
			<ModuleInfo AssemblyFile='Bogus.dll'>
				<Roles>
					<Role Allow='Administrators' />
				</Roles>
			</ModuleInfo>
		</Modules>
	</Section>
</SolutionProfile>";

			XmlStreamDependentModuleEnumerator enumerator = new XmlStreamDependentModuleEnumerator();
			enumerator.ModuleInfoStore = new MockModuleInfoStore(infoXml);

			IModuleInfo[] results = enumerator.EnumerateModules();

			Assert.IsNotNull(results);
			Assert.IsNotNull(results[0].AllowedRoles);
			Assert.AreEqual(1, results[0].AllowedRoles.Count);
			Assert.AreEqual("Administrators", results[0].AllowedRoles[0]);
		}

		[TestMethod]
		public void UsesReflectionForNamesAndDependenciesWhenGivenV2XmlWithoutNameOrDependenciesAttributes()
		{
			Assembly assembly1 = ModuleCompilerHelper.GeneratedAssemblies["ModuleDependency1"];
			Assembly assembly2 = ModuleCompilerHelper.GeneratedAssemblies["ModuleDependency2"];

			string infoXmlTemplate =
				@"
<SolutionProfile xmlns='http://schemas.microsoft.com/pag/cab-profile/2.0'>
	<Section>
		<Modules>
			<ModuleInfo AssemblyFile='{0}'/>
			<ModuleInfo AssemblyFile='{1}'/>
		</Modules>
	</Section>
</SolutionProfile>";

			XmlStreamDependentModuleEnumerator enumerator = new XmlStreamDependentModuleEnumerator();
			enumerator.ModuleInfoStore =
				new MockModuleInfoStore(
					String.Format(infoXmlTemplate, CodeBaseToFilename(assembly1.CodeBase), CodeBaseToFilename(assembly2.CodeBase)));

			IDependentModuleInfo[] results = (IDependentModuleInfo[]) enumerator.EnumerateModules();

			Assert.IsNotNull(results);
			Assert.AreEqual("module1", results[0].Name);
			Assert.AreEqual("module2", results[1].Name);
			Assert.AreEqual(0, results[0].Dependencies.Count);
			Assert.AreEqual(1, results[1].Dependencies.Count);
			Assert.AreEqual("module1", results[1].Dependencies[0]);
		}

		[TestMethod]
		public void IgnoresReflectionForNameAndDependenciesWhenGivenV2XmlWithNameAndDependenciesElements()
		{
			Assembly assembly1 = ModuleCompilerHelper.GeneratedAssemblies["ModuleDependency1"];
			Assembly assembly2 = ModuleCompilerHelper.GeneratedAssemblies["ModuleDependency2"];

			string infoXmlTemplate =
				@"
<SolutionProfile xmlns='http://schemas.microsoft.com/pag/cab-profile/2.0'>
	<Section>
		<Modules>
			<ModuleInfo AssemblyFile='{0}' Name='Flarble'>
				<Dependencies/>
			</ModuleInfo>
			<ModuleInfo AssemblyFile='{1}' Name='Blarble'>
				<Dependencies/>
			</ModuleInfo>
		</Modules>
	</Section>
</SolutionProfile>";

			XmlStreamDependentModuleEnumerator enumerator = new XmlStreamDependentModuleEnumerator();
			enumerator.ModuleInfoStore =
				new MockModuleInfoStore(
					String.Format(infoXmlTemplate, CodeBaseToFilename(assembly1.CodeBase), CodeBaseToFilename(assembly2.CodeBase)));

			IDependentModuleInfo[] results = (IDependentModuleInfo[]) enumerator.EnumerateModules();

			Assert.IsNotNull(results);
			Assert.AreEqual("Flarble", results[0].Name);
			Assert.AreEqual("Blarble", results[1].Name);
			Assert.AreEqual(0, results[0].Dependencies.Count);
			Assert.AreEqual(0, results[1].Dependencies.Count);
		}

		[TestMethod]
		public void HandlesMultipleSectionsInV2Xml()
		{
			string infoXml =
				@"
<SolutionProfile xmlns='http://schemas.microsoft.com/pag/cab-profile/2.0'>
	<Section>
		<Modules>
			<ModuleInfo AssemblyFile='Bogus1.dll' />
		</Modules>
	</Section>
	<Section>
		<Modules>
			<ModuleInfo AssemblyFile='Bogus2.dll' />
			<ModuleInfo AssemblyFile='Bogus3.dll' />
		</Modules>
	</Section>
</SolutionProfile>";

			XmlStreamDependentModuleEnumerator enumerator = new XmlStreamDependentModuleEnumerator();
			enumerator.ModuleInfoStore = new MockModuleInfoStore(infoXml);

			IModuleInfo[] results = enumerator.EnumerateModules();

			Assert.IsNotNull(results);
			Assert.AreEqual(3, results.Length);
			Assert.AreEqual("Bogus1.dll", results[0].AssemblyFile);
			Assert.AreEqual("Bogus2.dll", results[1].AssemblyFile);
			Assert.AreEqual("Bogus3.dll", results[2].AssemblyFile);
		}

		[TestMethod]
		public void HandlesModuleToModuleDependencies()
		{
			string infoXml =
				@"
<SolutionProfile xmlns='http://schemas.microsoft.com/pag/cab-profile/2.0'>
	<Section>
		<Modules>
			<ModuleInfo AssemblyFile='Bogus1.dll' Name='Bogus1'>
				<Dependencies>
					<Dependency Name='Bogus2' />
				</Dependencies>
			</ModuleInfo>
			<ModuleInfo AssemblyFile='Bogus2.dll' Name='Bogus2' />
			<ModuleInfo AssemblyFile='Bogus3.dll' Name='Bogus3'>
				<Dependencies>
					<Dependency Name='Bogus1' />
				</Dependencies>
			</ModuleInfo>
		</Modules>
	</Section>
</SolutionProfile>";

			XmlStreamDependentModuleEnumerator enumerator = new XmlStreamDependentModuleEnumerator();
			enumerator.ModuleInfoStore = new MockModuleInfoStore(infoXml);

			IDependentModuleInfo[] results = (IDependentModuleInfo[]) enumerator.EnumerateModules();

			Assert.IsNotNull(results);
			Assert.AreEqual(3, results.Length);
			Assert.AreEqual("Bogus1.dll", results[0].AssemblyFile);
			Assert.AreEqual("Bogus2", results[0].Dependencies[0]);
			Assert.AreEqual("Bogus2.dll", results[1].AssemblyFile);
			Assert.AreEqual("Bogus3.dll", results[2].AssemblyFile);
			Assert.AreEqual("Bogus1", results[2].Dependencies[0]);
		}

		[TestMethod]
		public void HandlesSectionToSectionDependencies()
		{
			string infoXml =
				@"
<SolutionProfile xmlns='http://schemas.microsoft.com/pag/cab-profile/2.0'>
	<Section Name='Foo'>
		<Modules>
			<ModuleInfo AssemblyFile='Foo.dll' Name='Baz' />
		</Modules>
	</Section>
	<Section>
		<Dependencies>
			<Dependency Name='Foo' />
		</Dependencies>
		<Modules>
			<ModuleInfo AssemblyFile='Bar.dll' Name='Foo' />
		</Modules>
	</Section>
</SolutionProfile>";

			XmlStreamDependentModuleEnumerator enumerator = new XmlStreamDependentModuleEnumerator();
			enumerator.ModuleInfoStore = new MockModuleInfoStore(infoXml);

			IDependentModuleInfo[] results = (IDependentModuleInfo[]) enumerator.EnumerateModules();

			Assert.AreEqual(2, results.Length);
			Assert.AreEqual("Foo.dll", results[0].AssemblyFile);
			Assert.AreEqual("Bar.dll", results[1].AssemblyFile);
			Assert.AreEqual(0, results[0].Dependencies.Count);
			Assert.AreEqual(1, results[1].Dependencies.Count);
			Assert.AreEqual("Baz", results[1].Dependencies[0]);
		}

		[TestMethod]
		public void CanHandleNamelessModulesForSectionToSectionDependencies()
		{
			string infoXml =
				@"
<SolutionProfile xmlns='http://schemas.microsoft.com/pag/cab-profile/2.0'>
	<Section Name='Foo'>
		<Modules>
			<ModuleInfo AssemblyFile='Foo.dll' />
		</Modules>
	</Section>
	<Section>
		<Dependencies>
			<Dependency Name='Foo' />
		</Dependencies>
		<Modules>
			<ModuleInfo AssemblyFile='Bar.dll' />
		</Modules>
	</Section>
</SolutionProfile>";

			XmlStreamDependentModuleEnumerator enumerator = new XmlStreamDependentModuleEnumerator();
			enumerator.ModuleInfoStore = new MockModuleInfoStore(infoXml);

			IDependentModuleInfo[] results = (IDependentModuleInfo[]) enumerator.EnumerateModules();

			Assert.AreEqual(2, results.Length);
			Assert.AreEqual("Foo.dll", results[0].AssemblyFile);
			Assert.IsNotNull(results[0].Name);
			Assert.AreEqual("Bar.dll", results[1].AssemblyFile);
			Assert.AreEqual(0, results[0].Dependencies.Count);
			Assert.AreEqual(1, results[1].Dependencies.Count);
			Assert.IsNotNull(results[1].Dependencies[0]);
			Assert.AreEqual(results[0].Name, results[1].Dependencies[0]);
		}

		[TestMethod]
		[ExpectedException(typeof (InvalidOperationException))]
		public void SectionDependencyForMissingSectionThrows()
		{
			string infoXml =
				@"
<SolutionProfile xmlns='http://schemas.microsoft.com/pag/cab-profile/2.0'>
	<Section>
		<Dependencies>
			<Dependency Name='TheUnknownSection' />
		</Dependencies>
		<Modules>
			<ModuleInfo AssemblyFile='Foo.dll' />
		</Modules>
	</Section>
</SolutionProfile>";

			XmlStreamDependentModuleEnumerator enumerator = new XmlStreamDependentModuleEnumerator();
			enumerator.ModuleInfoStore = new MockModuleInfoStore(infoXml);

			enumerator.EnumerateModules();
		}

		#region Helpers

		private string CodeBaseToFilename(string filename)
		{
			return filename.Replace("file:///", "").Replace('/', '\\');
		}

		class MockModuleInfoStore : IModuleInfoStore
		{
			public string ReturnValue = null;

			public MockModuleInfoStore()
				: this(null)
			{
			}

			public MockModuleInfoStore(string returnValue)
			{
				ReturnValue = returnValue;
			}

			public string GetModuleListXml()
			{
				return ReturnValue;
			}
		}

		#endregion
	}
}