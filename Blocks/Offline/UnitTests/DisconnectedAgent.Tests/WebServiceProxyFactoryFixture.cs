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
using System.Net;
using System.Web.Services;
using System.Web.Services.Protocols;
using Microsoft.Practices.SmartClient.ApplicationBlocks.DisconnectedAgent.Tests.Mocks;
using Microsoft.Practices.SmartClient.DisconnectedAgent;
using Microsoft.Practices.SmartClient.EndpointCatalog;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.SmartClient.ApplicationBlocks.DisconnectedAgent.Tests
{
	[TestClass]
	public class WebServiceProxyFactoryFixture
	{
		private string defaultURL = @"http://default/url";

		[TestMethod]
		public void WebServiceProxyFactoryCreatesAnObjectOfTheOnlineProxyType()
		{
			Request request = new Request();
			request.Endpoint = "endpoint";
			request.OnlineProxyType = typeof (MockWebServiceProxy);

			NetworkCredential creds = new NetworkCredential("user", "pwd");

			MockEndpointCatalog catalog = new MockEndpointCatalog();
			MockEndpoint endpoint = new MockEndpoint("endpoint");
			endpoint.Default = new MockEndpointConfig(defaultURL);
			endpoint.Default.Credential = creds;
			catalog.Endpoints.Add("endpoint", endpoint);

			WebServiceProxyFactory factory = new WebServiceProxyFactory((IEndpointCatalog) catalog);

			SoapHttpClientProtocol proxy = (SoapHttpClientProtocol) factory.GetOnlineProxy(request, "Network");

			Assert.IsNotNull(proxy);
			Assert.AreEqual(typeof (MockWebServiceProxy), proxy.GetType());
		}

		[TestMethod]
		public void WebServiceProxyFactorySetsCredential()
		{
			Request request = new Request();
			request.Endpoint = "endpoint";
			request.OnlineProxyType = typeof (MockWebServiceProxy);

			NetworkCredential creds = new NetworkCredential("user", "pwd");

			MockEndpointCatalog catalog = new MockEndpointCatalog();
			MockEndpoint endpoint = new MockEndpoint("endpoint");
			endpoint.Default = new MockEndpointConfig(defaultURL);
			endpoint.Default.Credential = creds;
			catalog.Endpoints.Add("endpoint", endpoint);

			WebServiceProxyFactory factory = new WebServiceProxyFactory((IEndpointCatalog) catalog);

			SoapHttpClientProtocol proxy = (SoapHttpClientProtocol) factory.GetOnlineProxy(request, "Network");

			Assert.IsNotNull(proxy);
			Assert.AreEqual(creds, proxy.Credentials);
		}

		[TestMethod]
		public void WebServiceProxyFactorySetsAddress()
		{
			Request request = new Request();
			request.Endpoint = "endpoint";
			request.OnlineProxyType = typeof (MockWebServiceProxy);

			NetworkCredential creds = new NetworkCredential("user", "pwd");

			MockEndpointCatalog catalog = new MockEndpointCatalog();
			MockEndpoint endpoint = new MockEndpoint("endpoint");
			endpoint.Default = new MockEndpointConfig(defaultURL);
			endpoint.Default.Credential = creds;
			catalog.Endpoints.Add("endpoint", endpoint);

			WebServiceProxyFactory factory = new WebServiceProxyFactory((IEndpointCatalog) catalog);

			SoapHttpClientProtocol proxy = (SoapHttpClientProtocol) factory.GetOnlineProxy(request, "Network");

			Assert.IsNotNull(proxy);
			Assert.AreEqual(defaultURL, proxy.Url);
		}

		[TestMethod]
		public void GetOnlineProxyUsesProxyAddressIfNoEndPointConfigured()
		{
			Request request = new Request();
			request.Endpoint = "MissingEndpoint";
			request.OnlineProxyType = typeof(MockWebServiceProxy);
			MockEndpointCatalog catalog = new MockEndpointCatalog();
			WebServiceProxyFactory factory = new WebServiceProxyFactory((IEndpointCatalog)catalog);

			SoapHttpClientProtocol proxy = (SoapHttpClientProtocol)factory.GetOnlineProxy(request, "Network");

			Assert.IsNotNull(proxy);
			Assert.IsNotNull(proxy.Url);
			Assert.AreEqual("http://MyUrl.myurl.com/".ToLower(), proxy.Url.ToLower());
		}

	}


	[WebServiceBinding(Name = "ServiceSoap", Namespace = "http://tempuri.org/")]
	public class MockWebServiceProxy : SoapHttpClientProtocol
	{
		public MockWebServiceProxy() :base()
		{
			this.Url = "http://MyUrl.myurl.com";
		}
	}
}