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
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.SmartClient.EndpointCatalog.Tests
{
	[TestClass]
	public class EndpointCatalogFixture
	{
		[TestMethod]
		public void AddressExistForEndPointReturnsFalseForNullNetworkAndValidEndpointWithoutDefault()
		{
			EndpointCatalog catalog = CreateEndpointCatalog();

			Assert.IsFalse(catalog.AddressExistsForEndpoint("NoDefault", null));
		}


		[TestMethod]
		public void AddressExistForAnEndpointReturnsTrueIfItExists()
		{
			EndpointCatalog catalog = CreateEndpointCatalog();

			Assert.IsTrue(catalog.AddressExistsForEndpoint("MyHost", "Work"));
			Assert.IsTrue(catalog.AddressExistsForEndpoint("MyHost", "Internet"));
			Assert.IsTrue(catalog.AddressExistsForEndpoint("MyHost", "UseTheDefaultNetwork"),
			              "EndpointCatalog is not using the Default config");
			Assert.IsTrue(catalog.AddressExistsForEndpoint("NoDefault", "Work"));
			Assert.IsTrue(catalog.AddressExistsForEndpoint("NoDefault", "Internet"));
		}

		[TestMethod]
		public void AddressExistForAnEndpointReturnsFalseIfItDoesntExist()
		{
			EndpointCatalog catalog = CreateEndpointCatalog();

			Assert.IsFalse(catalog.AddressExistsForEndpoint("MissingEndpoint", "Work"));
			Assert.IsFalse(catalog.AddressExistsForEndpoint("NoDefault", "MissingNetworkAndNoDefault"));
		}

		[TestMethod]
		public void GetAddressGetsRightAddress()
		{
			EndpointCatalog catalog = CreateEndpointCatalog();

			Assert.AreEqual(@"http://default/MyHost", catalog.GetAddressForEndpoint("MyHost", "UseTheDefault"));
			Assert.AreEqual(@"http://internet/MyHost", catalog.GetAddressForEndpoint("MyHost", "Internet"));
			Assert.AreEqual(@"http://work/MyHost", catalog.GetAddressForEndpoint("MyHost", "Work"));
			Assert.AreEqual(@"http://internet/NoDefault", catalog.GetAddressForEndpoint("NoDefault", "Internet"));
			Assert.AreEqual(@"http://work/NoDefault", catalog.GetAddressForEndpoint("NoDefault", "Work"));
		}

		[TestMethod]
		[ExpectedException(typeof (KeyNotFoundException))]
		public void GetAddressGetsThrowsKeyNotFoundExceptionIfEndpointDoesntExists()
		{
			EndpointCatalog catalog = CreateEndpointCatalog();
			catalog.GetAddressForEndpoint("MissingEndpoint", "Network");
		}

		[TestMethod]
		[ExpectedException(typeof (KeyNotFoundException))]
		public void GetAddressGetsThrowsKeyNotFoundExceptionIfConfigurationDoesntExists()
		{
			EndpointCatalog catalog = CreateEndpointCatalog();
			catalog.GetAddressForEndpoint("NoDefault", "MissingNetwork");
		}

		[TestMethod]
		public void GetCredentialGetsRightCredentials()
		{
			EndpointCatalog catalog = CreateEndpointCatalog();

			//Username
			Assert.AreEqual(@"defaultUser", catalog.GetCredentialForEndpoint("MyHost", "UseTheDefault").UserName);
			Assert.AreEqual(@"peter", catalog.GetCredentialForEndpoint("MyHost", "Internet").UserName);
			Assert.AreEqual(@"chris", catalog.GetCredentialForEndpoint("MyHost", "Work").UserName);
			Assert.AreEqual(@"chris", catalog.GetCredentialForEndpoint("NoDefault", "Internet").UserName);
			Assert.AreEqual(@"peter", catalog.GetCredentialForEndpoint("NoDefault", "Work").UserName);

			//Password
			Assert.AreEqual(@"4444", catalog.GetCredentialForEndpoint("MyHost", "UseTheDefault").Password);
			Assert.AreEqual(@"1234", catalog.GetCredentialForEndpoint("MyHost", "Internet").Password);
			Assert.AreEqual(@"3333", catalog.GetCredentialForEndpoint("MyHost", "Work").Password);
			Assert.AreEqual(@"1234", catalog.GetCredentialForEndpoint("NoDefault", "Internet").Password);
			Assert.AreEqual(@"3333", catalog.GetCredentialForEndpoint("NoDefault", "Work").Password);

			//Domain
			Assert.AreEqual(@"myDomain", catalog.GetCredentialForEndpoint("MyHost", "Internet").Domain);
			Assert.IsTrue(string.IsNullOrEmpty(catalog.GetCredentialForEndpoint("MyHost", "UseTheDefault").Domain));
			Assert.IsTrue(string.IsNullOrEmpty(catalog.GetCredentialForEndpoint("MyHost", "Work").Domain));
			Assert.IsTrue(string.IsNullOrEmpty(catalog.GetCredentialForEndpoint("NoDefault", "Internet").Domain));

			Assert.AreEqual(@"MyDomain", catalog.GetCredentialForEndpoint("NoDefault", "Work").Domain);
		}


		[TestMethod]
		[ExpectedException(typeof (KeyNotFoundException))]
		public void GetCredentialGetsThrowsKeyNotFoundExceptionIfEndpointDoesntExists()
		{
			EndpointCatalog catalog = CreateEndpointCatalog();
			catalog.GetCredentialForEndpoint("MissingEndpoint", "Network");
		}

		[TestMethod]
		[ExpectedException(typeof (KeyNotFoundException))]
		public void GetCredentialGetsThrowsKeyNotFoundExceptionIfConfigurationDoesntExists()
		{
			EndpointCatalog catalog = CreateEndpointCatalog();
			catalog.GetCredentialForEndpoint("NoDefault", "MissingNetwork");
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentException))]
		public void EmptyEndpointNameRegistrationShouldThrowsAnArgumentException()
		{
			EndpointCatalog catalog = new EndpointCatalog();
			Endpoint endpoint = new Endpoint(""); //Emptyname endpoint
			catalog.SetEndpoint(endpoint); //Should throws an ArgumentException
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentException))]
		public void NullEndpointNameRegistrationShouldThrowsAnArgumentException()
		{
			EndpointCatalog catalog = new EndpointCatalog();
			Endpoint endpoint = new Endpoint(null);
			catalog.SetEndpoint(endpoint); //Should throws an ArgumentException
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentNullException))]
		public void NullEndpointRegistrationShouldThrowsAnArgumentException()
		{
			EndpointCatalog catalog = new EndpointCatalog();
			catalog.SetEndpoint(null);
		}

		[TestMethod]
		public void ModifyingDefaultEndpointConfigurationReplacePreviousEndpointConfigurationDefinedAsDefault()
		{
			EndpointCatalog catalog = new EndpointCatalog();

			Endpoint endpoint = new Endpoint("Endpoint");
			EndpointConfig ec = new EndpointConfig("URL1", new NetworkCredential("user", "pass"));
			endpoint.Default = ec; //Default configuration for endpoint "Endpoint"

			catalog.SetEndpoint(endpoint);

			Endpoint endpoint2 = new Endpoint("Endpoint");
			EndpointConfig ec2 = new EndpointConfig("URL2", new NetworkCredential("newuser", "newpass"));
			endpoint2.Default = ec2; //New default configuration for endpoint "Endpoint"

			catalog.SetEndpoint(endpoint2);

			Assert.AreEqual(1, catalog.Count);
			Assert.AreEqual("newuser", catalog.GetCredentialForEndpoint("Endpoint", null).UserName);
			Assert.AreEqual("newpass", catalog.GetCredentialForEndpoint("Endpoint", null).Password);
		}

		[TestMethod]
		public void ModifyingEndpointOverridesPreviousOne()
		{
			EndpointCatalog catalog = new EndpointCatalog();

			Endpoint newEndpoint = new Endpoint("Endpoint");
			newEndpoint.SetConfiguration("Network", new EndpointConfig("Address"));

			catalog.SetEndpoint(newEndpoint);

			Endpoint endpoint = new Endpoint("Endpoint");
			endpoint.SetConfiguration("Network One", new EndpointConfig("Address One"));
			endpoint.SetConfiguration("Network Two", new EndpointConfig("Address Two"));

			catalog.SetEndpoint(endpoint);

			Assert.AreEqual(1, catalog.Count);
			Assert.AreEqual("Address One", catalog.GetAddressForEndpoint("Endpoint", "Network One"));
			Assert.AreEqual("Address Two", catalog.GetAddressForEndpoint("Endpoint", "Network Two"));
		}

		[TestMethod]
		[ExpectedException(typeof (KeyNotFoundException))]
		public void GetCredentialForNullNetworkThrowsKeyNotFoundExceptionIfThereIsNotDefaultConfiguration()
		{
			EndpointCatalog catalog = CreateEndpointCatalog();
			NetworkCredential cred = catalog.GetCredentialForEndpoint("NoDefault", null);
		}

		private EndpointCatalog CreateEndpointCatalog()
		{
			EndpointCatalog catalog;
			//using (TestResourceFile resFile = new TestResourceFile(this, @"App.config"))
			//{
			IEndpointCatalogFactory factory = new EndpointCatalogFactory("Endpoints");
			catalog = factory.CreateCatalog() as EndpointCatalog;
			//}
			return catalog;
		}

		private bool AreEqual(NetworkCredential a, NetworkCredential b)
		{
			return (a.UserName == b.UserName &&
			        a.Password == b.Password &&
			        a.Domain == b.Domain);
		}
	}
}