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
using System.Globalization;
using System.IO;
using System.Reflection;
using Microsoft.Practices.SmartClientFactory.Actions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SmartClientFactoryPackage.Tests
{
	[TestClass]
	public class ConvertProjectToWpfTestFixture
	{
		private const string NonWpfProjectName = "CustomerModule.csproj.txt";
		private const string WpfProjectName = "OrganizationChart.csproj.txt";
		private const string TestProjectName = "TestProject.csproj.txt";

		[TestMethod]
		public void CanOpenNonWpfProject()
		{
			string tempProjectName = UnfoldTemporaryProject(NonWpfProjectName);
			using (new TempFileDeleter(tempProjectName))
			{
				WpfProjectFileConverter converter = new WpfProjectFileConverter(tempProjectName);

				Assert.IsFalse(converter.IsWpfProject);
				Assert.IsTrue(converter.CanConvertToWpfProject);
			}
		}

		[TestMethod]
		public void CanOpenWpfProject()
		{
			string tempProjectName = UnfoldTemporaryProject(WpfProjectName);
			using (new TempFileDeleter(tempProjectName))
			{
				WpfProjectFileConverter converter = new WpfProjectFileConverter(tempProjectName);
				Assert.IsTrue(converter.IsWpfProject);
				Assert.IsFalse(converter.CanConvertToWpfProject);
			}
		}

		[TestMethod]
		public void CanConvertNonWpfProject()
		{
			string tempProjectName = UnfoldTemporaryProject(NonWpfProjectName);
			using (new TempFileDeleter(tempProjectName))
			{
				WpfProjectFileConverter converter = new WpfProjectFileConverter(tempProjectName);
				Assert.IsFalse(converter.IsWpfProject);
				converter.ConvertToWpfProject();
				Assert.IsTrue(converter.IsDirty);

				converter.Save(tempProjectName);
				Assert.IsFalse(converter.IsDirty);

				WpfProjectFileConverter verifier = new WpfProjectFileConverter(tempProjectName);
				Assert.IsTrue(verifier.IsWpfProject);
			}
		}

		[TestMethod]
		public void ConvertingExistingWpfProjectDoesNothing()
		{
			string tempProjectName = UnfoldTemporaryProject(WpfProjectName);
			using (new TempFileDeleter(tempProjectName))
			{
				WpfProjectFileConverter converter = new WpfProjectFileConverter(tempProjectName);
				Assert.IsTrue(converter.IsWpfProject);
				converter.ConvertToWpfProject();
				Assert.IsTrue(converter.IsWpfProject);
				Assert.IsFalse(converter.IsDirty);
			}
		}

		[TestMethod]
		[ExpectedException(typeof (InvalidOperationException))]
		public void WillNotConvertProjectWithDifferentTypeKindGuids()
		{
			string tempProjectName = UnfoldTemporaryProject(TestProjectName);
			using (new TempFileDeleter(tempProjectName))
			{
				WpfProjectFileConverter converter = new WpfProjectFileConverter(tempProjectName);
				Assert.IsFalse(converter.IsWpfProject);
				Assert.IsFalse(converter.CanConvertToWpfProject);
				converter.ConvertToWpfProject();
			}
		}

		private static string UnfoldTemporaryProject(string projFileName)
		{
			Assembly thisAssembly = Assembly.GetExecutingAssembly();
			Stream projFileStream = thisAssembly.GetManifestResourceStream(
				string.Format(CultureInfo.InvariantCulture, "SmartClientFactoryPackage.Tests.Support.{0}", projFileName));
			using (StreamReader reader = new StreamReader(projFileStream))
			{
				string destFileName = Path.GetTempFileName();
				using (StreamWriter writer = new StreamWriter(destFileName, false))
				{
					writer.Write(reader.ReadToEnd());
					return destFileName;
				}
			}
		}
	}

	internal class TempFileDeleter : IDisposable
	{
		private string filename;

		public TempFileDeleter(string filename)
		{
			this.filename = filename;
		}


		public void Dispose()
		{
			if (filename != null)
			{
				File.Delete(filename);
				filename = null;
			}
		}
	}
}