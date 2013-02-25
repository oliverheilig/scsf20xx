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
using GlobalBank.UnitTest.Library;
using Microsoft.Practices.CompositeUI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProfileCatalogServiceImplementation;

namespace GlobalBank.Infrastructure.Library.Tests.Services
{
	[TestClass]
	public class WebServiceCatalogModuleInfoStoreFixture
	{
		WorkItem mockContainer;

		[TestInitialize]
		public void Init()
		{
			mockContainer = new TestableRootWorkItem();
			mockContainer.Services.AddNew<MockProfileCatalogService, IProfileCatalogService>();
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentNullException))]
		public void NullFileThrows()
		{
			WebServiceCatalogModuleInfoStore store =
				mockContainer.Services.AddNew<WebServiceCatalogModuleInfoStore, IModuleInfoStore>();

			store.CatalogUrl = null;
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentException))]
		public void EmptyStringThrows()
		{
			WebServiceCatalogModuleInfoStore store =
				mockContainer.Services.AddNew<WebServiceCatalogModuleInfoStore, IModuleInfoStore>();

			store.CatalogUrl = "";
		}

		[TestMethod]
		public void DefaultCatalogUrlIsCorrect()
		{
			WebServiceCatalogModuleInfoStore store =
				mockContainer.Services.AddNew<WebServiceCatalogModuleInfoStore, IModuleInfoStore>();

			Assert.AreEqual("http://localhost:54092/profilecatalogservices/profilecatalog.asmx",
			                store.CatalogUrl.ToLowerInvariant());
		}

		[TestMethod]
        [DeploymentItem("TestableProfileCatalog.xml")]
		public void RequestCatalogContentsFromWebServiceAsString()
		{
			string contents = "<SolutionProfile xmlns=\"http://schemas.microsoft.com/pag/cab-profile/2.0\">"
			                  + "</SolutionProfile>";

			WebServiceCatalogModuleInfoStore store =
				mockContainer.Services.AddNew<WebServiceCatalogModuleInfoStore, IModuleInfoStore>();

			store.Roles = new string[] {"tester"};

			string results = store.GetModuleListXml();

			Assert.AreEqual(contents, results);
		}

		[TestMethod]
        [ExpectedException(typeof(UnexistingUrlException))]
		public void ThrowExWhenWebServiceFails()
		{
			string filename = "http://localhost/unexisting/no.asmx";
			WebServiceCatalogModuleInfoStore store =
				mockContainer.Services.AddNew<WebServiceCatalogModuleInfoStore, IModuleInfoStore>();

			store.CatalogUrl = filename;
			store.Roles = new string[] {"null"};
			string results = store.GetModuleListXml();

			Assert.IsNull(results);
		}
	}

	class MockProfileCatalogService : IProfileCatalogService
	{
		#region IProfileCatalogService Members

		public string GetProfileCatalog(string[] roles)
		{
			if (_url == "http://localhost/unexisting/no.asmx")
                throw new UnexistingUrlException();
			string baseDir = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
			int iFolder = baseDir.LastIndexOf('\\');
			baseDir = baseDir.Substring(0, iFolder);
			ProfileCatalog catalog = new ProfileCatalog(baseDir);
			return catalog.GetProfileCatalog(roles);
			;
		}

		private string _url;

		public string Url
		{
			get { return _url; }
			set { _url = value; }
		}

		#endregion
	}

    class UnexistingUrlException : Exception
    {
    }
}