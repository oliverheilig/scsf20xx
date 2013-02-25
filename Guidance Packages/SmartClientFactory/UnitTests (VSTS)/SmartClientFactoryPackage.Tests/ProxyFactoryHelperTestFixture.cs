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
using System.ServiceModel;
using System.Web.Services.Protocols;
using Microsoft.Practices.SmartClientFactory.ValueProviders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SmartClientFactoryPackage.Tests
{
	/// <summary>
	/// Summary description for ProxyFactoryHelperTestFixture
	/// </summary>
	[TestClass]
	public class ProxyFactoryHelperTestFixture
	{
		public ProxyFactoryHelperTestFixture()
		{
		}

		[TestMethod]
		public void ShouldReturnNullFactoryForNullProxy()
		{
			Assert.AreEqual(null, ProxyFactoryHelper.GetProxyFactory(null, "CS"));
		}

		[TestMethod]
		public void ShouldReturnWebServiceFactoryForWebService()
		{
			Type webServiceType = typeof (MockWebService);

			Assert.AreEqual(ProxyFactoryHelper.WebServiceProxyFactoryName,
			                ProxyFactoryHelper.GetProxyFactory(webServiceType, "CS"));
		}

		[TestMethod]
		public void ShouldReturnWCFPFactoryForWCFService()
		{
			Type wcfServiceType = typeof (MockWCFService);

			string wcfProxyFactoryTypeName =
				"Microsoft.Practices.SmartClient.DisconnectedAgent.WCFProxyFactory<SmartClientFactoryPackage.Tests.MockCustomService>";
			Assert.AreEqual(wcfProxyFactoryTypeName, ProxyFactoryHelper.GetProxyFactory(wcfServiceType, "CS"));
		}

		[TestMethod]
		public void ShouldReturnWCFPFactoryForWCFServiceInVB()
		{
			Type wcfServiceType = typeof (MockWCFService);

			string wcfProxyFactoryTypeName =
				"Microsoft.Practices.SmartClient.DisconnectedAgent.WCFProxyFactory(Of SmartClientFactoryPackage.Tests.MockCustomService)";
			Assert.AreEqual(wcfProxyFactoryTypeName, ProxyFactoryHelper.GetProxyFactory(wcfServiceType, "VB"));
		}

		[TestMethod]
		public void ShouldReturnWCFFactoryForWCFService()
		{
			Type anyServiceType = typeof (MockCustomService);

			Assert.AreEqual(ProxyFactoryHelper.ObjectProxyFactoryName, ProxyFactoryHelper.GetProxyFactory(anyServiceType, "CS"));
		}

        [TestMethod]
        public void ShouldReturnProperTechnology()
        {

            Assert.AreEqual(ProxyTechnology.Asmx, ProxyFactoryHelper.GetProxyTechnology(typeof(MockWebService)));
            Assert.AreEqual(ProxyTechnology.Wcf, ProxyFactoryHelper.GetProxyTechnology(typeof(MockWCFService)));
            Assert.AreEqual(ProxyTechnology.Object, ProxyFactoryHelper.GetProxyTechnology(typeof(MockCustomService)));
        }
	}

	internal class MockWebService : SoapHttpClientProtocol
	{
	}

	internal class MockWCFService : ClientBase<MockCustomService>
	{
	}

	internal class MockCustomService
	{
	}
}