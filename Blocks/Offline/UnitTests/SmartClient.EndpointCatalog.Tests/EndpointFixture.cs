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
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.SmartClient.EndpointCatalog.Tests
{
	[TestClass]
	public class EndpointFixture
	{
		[TestMethod]
		public void GetCredentialForNetworkReturnsDefaultWhenNetworkNameIsNull()
		{
			Endpoint endpoint = new Endpoint("MyIsp");
			EndpointConfig ec = new EndpointConfig("", new NetworkCredential("user", "pass"));
			endpoint.Default = ec;

			NetworkCredential cred = endpoint.GetCredentialForNetwork(null);
			Assert.IsNotNull(cred);
			Assert.AreEqual(ec.Credential, cred);
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentException))]
		public void GetCredentialForNetworkThrowsWhenNetworkNameIsEmpty()
		{
			Endpoint endpoint = new Endpoint("MyIsp");
			EndpointConfig ec = new EndpointConfig("", new NetworkCredential("user", "pass"));
			endpoint.Default = ec;

			NetworkCredential cred = endpoint.GetCredentialForNetwork(String.Empty);
		}

		[TestMethod]
		public void GetAddressForNetworkReturnsDefaultWhenNetworkNameIsNull()
		{
			Endpoint endpoint = new Endpoint("MyIsp");
			EndpointConfig ec = new EndpointConfig("", new NetworkCredential("user", "pass"));
			endpoint.Default = ec;

			string address = endpoint.GetAddressForNetwork(null);
			Assert.IsNotNull(address);
			Assert.AreEqual(ec.Address, address);
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentException))]
		public void GetAddressForNetworkThrowsWhenNetworkNameIsEmpty()
		{
			Endpoint endpoint = new Endpoint("MyIsp");
			EndpointConfig ec = new EndpointConfig("", new NetworkCredential("user", "pass"));
			endpoint.Default = ec;

			string address = endpoint.GetAddressForNetwork(String.Empty);
		}

		[TestMethod]
		public void EndpointProvidesCollectionOfRegisteredNetworkNames()
		{
			Endpoint ep = new Endpoint("");
			ep.SetConfiguration("One", new EndpointConfig("URL1"));
			ep.SetConfiguration("Two", new EndpointConfig("URL2"));
			ep.SetConfiguration("Three", new EndpointConfig("URL3"));

			Assert.AreEqual(3, ep.NetworkNames.Count);
			Assert.IsTrue(ep.NetworkNames.Contains("One"));
			Assert.IsTrue(ep.NetworkNames.Contains("Two"));
			Assert.IsTrue(ep.NetworkNames.Contains("Three"));
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentException))]
		public void EmptyEndpointConfigurationNameRegistrationShouldThrowsAnArgumentException()
		{
			Endpoint endpoint = new Endpoint("");
			EndpointConfig ec = new EndpointConfig("");
			endpoint.SetConfiguration("", ec); //Should throws an ArgumentException
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentException))]
		public void NullConfigurationNameRegistrationShouldThrowsAnArgumentException()
		{
			Endpoint endpoint = new Endpoint("");
			EndpointConfig ec = new EndpointConfig("");
			endpoint.SetConfiguration(null, ec); //Should throws an ArgumentException
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentNullException))]
		public void NullEndpointConfigurationRegistrationShouldThrowsAnArgumentNullException()
		{
			Endpoint endpoint = new Endpoint("");
			endpoint.SetConfiguration("A", null);
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentException))]
		public void EmptyAddressInEndpointConfigurationRegistrationShouldThrowsAnArgumentException()
		{
			Endpoint endpoint = new Endpoint("");
			EndpointConfig ec = new EndpointConfig("");

			endpoint.SetConfiguration("Network", ec);
		}

		[TestMethod]
		public void ModifyingEndpointConfigReplacePreviousConfig()
		{
			Endpoint ep = new Endpoint("Endpoint");
			EndpointConfig ec1 = new EndpointConfig("URL1");

			ep.SetConfiguration("Network1", ec1);

			EndpointConfig ec2 = new EndpointConfig("URL2");
			ep.SetConfiguration("Network1", ec2);

			Assert.AreEqual(ec2.Address, ep.GetAddressForNetwork("Network1"));
			Assert.AreEqual(ec2.Credential, ep.GetCredentialForNetwork("Network1"));
			Assert.AreEqual(1, ep.NetworkNames.Count);
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentNullException))]
		public void SetNullConfigurationThrowsArgumentNullException()
		{
			Endpoint endpoint = new Endpoint("Endpoint");
			endpoint.SetConfiguration("Network", null);
		}

		[TestMethod]
		public void ModifyDefaultCredentialReplacesPreviousDefaultCredential()
		{
			Endpoint ep = new Endpoint("Endpoint");
			EndpointConfig ec = new EndpointConfig("Address", new NetworkCredential("U", "P", "D"));
			NetworkCredential expectedCreds = new NetworkCredential("A", "B", "C");
			ep.Default = ec;
			ep.Default.Credential = expectedCreds;
			Assert.AreEqual(expectedCreds, ep.GetCredentialForNetwork(null));
		}

		private bool AreEqual(NetworkCredential a, NetworkCredential b)
		{
			return (a.UserName == b.UserName &&
			        a.Password == b.Password &&
			        a.Domain == b.Domain);
		}
	}
}