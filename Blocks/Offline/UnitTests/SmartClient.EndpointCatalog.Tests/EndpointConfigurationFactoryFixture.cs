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
using System.Configuration;
using Microsoft.Practices.SmartClient.EndpointCatalog.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.SmartClient.EndpointCatalog.Tests
{
	//  <Endpoints>
	//  <EndpointItems>
	//    <add Name="MyHost" Address="http://default/MyHost" UserName="defaultUser" Password="4444">
	//      <NetworkItems>
	//        <add Name="Internet" Address="http://internet/MyHost" UserName="peter" Password="1234" Domain="myDomain"/>
	//        <add Name="Work" Address="http://work/MyHost" UserName="chris" Password="3333"/>
	//      </NetworkItems>
	//    </add>
	//    <add Name="NoDefault">
	//      <NetworkItems>
	//        <add Name="Internet" Address="http://internet/NoDefault" UserName="chris" Password="1234"/>
	//        <add Name="Work" Address="http://work/NoDefault" UserName="peter" Password="3333" Domain="MyDomain"/>
	//      </NetworkItems>
	//    </add>
	//  </EndpointItems>
	//</Endpoints>

	[TestClass]
	public class EndpointConfigurationFactoryFixture
	{
		[TestInitialize]
		public void Initialize()
		{
			ConfigurationManager.RefreshSection("Endpoints");
		}

		[TestMethod]
		public void GetEndpointSectionDoesntGetNull()
		{
			EndpointSection section = (EndpointSection) ConfigurationManager.GetSection("Endpoints");
			Assert.IsNotNull(section);
		}


		[TestMethod]
		public void SectionGetEndpointItemsCollectionDoesntGetNull()
		{
			EndpointSection section = (EndpointSection) ConfigurationManager.GetSection("Endpoints");

			Assert.IsNotNull(section.EndpointItemCollection);
		}

		[TestMethod]
		public void SectionGetEndpointItemsCollectionContainsSameQuantityThanAppConfig()
		{
			EndpointSection section = (EndpointSection) ConfigurationManager.GetSection("Endpoints");

			EndpointItemElementCollection collection = section.EndpointItemCollection;

			Assert.AreEqual(2, collection.Count);
		}

		[TestMethod]
		public void GetEndpointFromEndpointItemsCollectionDoesntGetNull()
		{
			EndpointSection section = (EndpointSection) ConfigurationManager.GetSection("Endpoints");
			EndpointItemElement element = section.EndpointItemCollection.GetEndpoint("MyHost");
			EndpointItemElement element2 = section.EndpointItemCollection.GetEndpoint("NoDefault");

			Assert.IsNotNull(element);
			Assert.IsNotNull(element2);
		}


		[TestMethod]
		public void EndpointsAddressInEndpointCollectionAreTheSameThanAppConfig()
		{
			EndpointSection section = (EndpointSection) ConfigurationManager.GetSection("Endpoints");

			Assert.AreEqual("http://default/MyHost", section.EndpointItemCollection.GetEndpoint("MyHost").Address);
		}

		[TestMethod]
		public void EndpointsCredentialsInEndpointCollectionAreTheSameThanAppConfig()
		{
			EndpointSection section = (EndpointSection) ConfigurationManager.GetSection("Endpoints");

			Assert.AreEqual("defaultUser", section.EndpointItemCollection.GetEndpoint("MyHost").UserName);
			Assert.AreEqual("4444", section.EndpointItemCollection.GetEndpoint("MyHost").Password);
			Assert.IsNull(section.EndpointItemCollection.GetEndpoint("MyHost").Domain);
		}

		[TestMethod]
		public void GetNetworkCollectionFromEndpointCollectionDoesntGetNull()
		{
			EndpointSection section = (EndpointSection) ConfigurationManager.GetSection("Endpoints");

			Assert.IsNotNull(section.EndpointItemCollection.GetEndpoint("MyHost").Networks);
		}

		[TestMethod]
		public void CountOfNetworkItemsInEndpointCollectionAreTheSameThanAppConfig()
		{
			EndpointSection section = (EndpointSection) ConfigurationManager.GetSection("Endpoints");

			Assert.AreEqual(2, section.EndpointItemCollection.GetEndpoint("MyHost").Networks.Count);
		}


		[TestMethod]
		public void GetNetworkFromNetworkCollectionDoesntGetNull()
		{
			EndpointSection section = (EndpointSection) ConfigurationManager.GetSection("Endpoints");
			NetworkElement element = section.EndpointItemCollection.GetEndpoint("MyHost").Networks.GetNetwork("Internet");

			Assert.IsNotNull(element);
		}

		[TestMethod]
		public void NetworkAddressInNetworkCollectionAreTheSameThanAppConfig()
		{
			EndpointSection section = (EndpointSection) ConfigurationManager.GetSection("Endpoints");

			Assert.AreEqual("http://internet/MyHost",
			                section.EndpointItemCollection.GetEndpoint("MyHost").Networks.GetNetwork("Internet").Address);
			Assert.AreEqual("http://work/MyHost",
			                section.EndpointItemCollection.GetEndpoint("MyHost").Networks.GetNetwork("Work").Address);
		}

		[TestMethod]
		public void NetworkCredentialsInNetworkCollectionAreTheSameThanAppConfig()
		{
			EndpointSection section = (EndpointSection) ConfigurationManager.GetSection("Endpoints");

			Assert.AreEqual("peter",
			                section.EndpointItemCollection.GetEndpoint("MyHost").Networks.GetNetwork("Internet").UserName);
			Assert.AreEqual("1234", section.EndpointItemCollection.GetEndpoint("MyHost").Networks.GetNetwork("Internet").Password);
			Assert.AreEqual("myDomain",
			                section.EndpointItemCollection.GetEndpoint("MyHost").Networks.GetNetwork("Internet").Domain);
		}

		[TestMethod]
		public void LoadEndpointCatalogFromConfigurationRetrievesACatalog()
		{
			IEndpointCatalogFactory factory = new EndpointCatalogFactory("Endpoints");
			EndpointCatalog catalog = factory.CreateCatalog() as EndpointCatalog;

			Assert.IsNotNull(catalog);
		}

		[TestMethod]
		public void LoadEndpointCatalogFromConfigurationRetrievesValidCatalog()
		{
			IEndpointCatalogFactory factory = new EndpointCatalogFactory("Endpoints");
			EndpointCatalog catalog = factory.CreateCatalog() as EndpointCatalog;

			Assert.AreEqual(2, catalog.Count);
			Assert.AreEqual("http://default/MyHost", catalog.GetAddressForEndpoint("MyHost", null));
			Assert.AreEqual("defaultUser", catalog.GetCredentialForEndpoint("MyHost", null).UserName);
			Assert.AreEqual("4444", catalog.GetCredentialForEndpoint("MyHost", null).Password);

			Assert.AreEqual("http://internet/MyHost", catalog.GetAddressForEndpoint("MyHost", "Internet"));
			Assert.AreEqual("peter", catalog.GetCredentialForEndpoint("MyHost", "Internet").UserName);
			Assert.AreEqual("1234", catalog.GetCredentialForEndpoint("MyHost", "Internet").Password);
			Assert.AreEqual("myDomain", catalog.GetCredentialForEndpoint("MyHost", "Internet").Domain);

			Assert.AreEqual("http://work/MyHost", catalog.GetAddressForEndpoint("MyHost", "Work"));
			Assert.AreEqual("chris", catalog.GetCredentialForEndpoint("MyHost", "Work").UserName);
			Assert.AreEqual("3333", catalog.GetCredentialForEndpoint("MyHost", "Work").Password);

			Assert.AreEqual("http://internet/NoDefault", catalog.GetAddressForEndpoint("NoDefault", "Internet"));
			Assert.AreEqual("chris", catalog.GetCredentialForEndpoint("NoDefault", "Internet").UserName);
			Assert.AreEqual("1234", catalog.GetCredentialForEndpoint("NoDefault", "Internet").Password);

			Assert.AreEqual("http://work/NoDefault", catalog.GetAddressForEndpoint("NoDefault", "Work"));
			Assert.AreEqual("peter", catalog.GetCredentialForEndpoint("NoDefault", "Work").UserName);
			Assert.AreEqual("3333", catalog.GetCredentialForEndpoint("NoDefault", "Work").Password);
			Assert.AreEqual("MyDomain", catalog.GetCredentialForEndpoint("NoDefault", "Work").Domain);
		}

		[TestMethod]
		[ExpectedException(typeof (KeyNotFoundException))]
		public void GetAddressForEndpointFromDefaultValueThatDoesntExistThrowsAnException()
		{
			IEndpointCatalogFactory factory = new EndpointCatalogFactory("Endpoints");
			EndpointCatalog catalog = factory.CreateCatalog() as EndpointCatalog;

			catalog.GetAddressForEndpoint("NoDefault", null);
		}

		[TestMethod]
		public void EndpointCatalogFactoryConstructorDoesNotThrowsWhenSectionDoesNotExist()
		{
			IEndpointCatalogFactory factory = new EndpointCatalogFactory("Non-ExistentEndpointSection");
		}

		[TestMethod]
		public void CreateCatalogReturnsNullWhenConfigSectionDoesNotExist()
		{
			IEndpointCatalogFactory factory = new EndpointCatalogFactory("Non-ExistentEndpointSection");
			EndpointCatalog catalog = factory.CreateCatalog() as EndpointCatalog;
			Assert.IsNull(catalog);
		}
	}
}