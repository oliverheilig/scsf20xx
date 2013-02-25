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
using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.Practices.SmartClient.ApplicationBlocks.DisconnectedAgent.Tests.Mocks;
using Microsoft.Practices.SmartClient.DisconnectedAgent.Tests.Mocks;
using Microsoft.Practices.SmartClient.EndpointCatalog;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace Microsoft.Practices.SmartClient.DisconnectedAgent.Tests
{
    /// <summary>
    /// Summary description for WCFProxyFactoryFixture
    /// </summary>
    [TestClass]
    public class WCFProxyFactoryFixture
    {
        private string defaultURL = @"http://default/url";

        [TestMethod]
        public void FactoryIsIProxyFactory()
        {
            WCFProxyFactory<IMockService> factory = new WCFProxyFactory<IMockService>();

            Assert.IsTrue(factory is IProxyFactory);
        }

        [TestMethod]
        public void CanCreateWithEndpointCatalog()
        {
            WCFProxyFactory<IMockService> factory = new WCFProxyFactory<IMockService>(new MockEndpointCatalog());

            Assert.IsNotNull(factory);
        }

        [TestMethod]
        public void WebServiceProxyFactoryCreatesAnObjectOfTheOnlineProxyType()
        {
            Request request = new Request();
            request.Endpoint = "endpoint";
            request.OnlineProxyType = typeof(MockClient);

            NetworkCredential creds = new NetworkCredential("user", "pwd");

            MockEndpointCatalog catalog = new MockEndpointCatalog();
            MockEndpoint endpoint = new MockEndpoint("endpoint");
            endpoint.Default = new MockEndpointConfig(defaultURL);
            endpoint.Default.Credential = creds;
            catalog.Endpoints.Add("endpoint", endpoint);

            WCFProxyFactory<IMockService> factory = new WCFProxyFactory<IMockService>((IEndpointCatalog)catalog);


            object proxy = factory.GetOnlineProxy(request, "Network");

            Assert.IsNotNull(proxy);
            Assert.AreEqual(typeof(MockClient), proxy.GetType());
        }

        [TestMethod]
        public void WebServiceProxyFactorySetsCredential()
        {
            Request request = new Request();
            request.Endpoint = "endpoint";
            request.OnlineProxyType = typeof(MockClient);

            NetworkCredential creds = new NetworkCredential("user", "pwd");

            MockEndpointCatalog catalog = new MockEndpointCatalog();
            MockEndpoint endpoint = new MockEndpoint("endpoint");
            endpoint.Default = new MockEndpointConfig(defaultURL);
            endpoint.Default.Credential = creds;
            catalog.Endpoints.Add("endpoint", endpoint);

            WCFProxyFactory<IMockService> factory = new WCFProxyFactory<IMockService>((IEndpointCatalog)catalog);

            MockClient proxy = (MockClient)factory.GetOnlineProxy(request, "Network");

            Assert.IsNotNull(proxy);

            Assert.AreEqual("user", proxy.ClientCredentials.UserName.UserName);
            Assert.AreEqual("pwd", proxy.ClientCredentials.UserName.Password);
        }

        [TestMethod]
        public void WebServiceProxyFactorySetsAddress()
        {
            Request request = new Request();
            request.Endpoint = "endpoint";
            request.OnlineProxyType = typeof(MockClient);

            NetworkCredential creds = new NetworkCredential("user", "pwd");

            MockEndpointCatalog catalog = new MockEndpointCatalog();
            MockEndpoint endpoint = new MockEndpoint("endpoint");
            endpoint.Default = new MockEndpointConfig(defaultURL);
            endpoint.Default.Credential = creds;
            catalog.Endpoints.Add("endpoint", endpoint);

            WCFProxyFactory<IMockService> factory = new WCFProxyFactory<IMockService>((IEndpointCatalog)catalog);

            MockClient proxy = (MockClient)factory.GetOnlineProxy(request, "Network");

            Assert.IsNotNull(proxy);
            Assert.AreEqual(defaultURL, proxy.Endpoint.Address.Uri.AbsoluteUri);
        }


        [TestMethod]
        public void FactoryCallTheSpecifiedMethod()
        {
            Request request = new Request();
            request.Endpoint = "endpoint";
            request.MethodName = "CalledMethod";
            request.OnlineProxyType = typeof(MockClient);

            NetworkCredential creds = new NetworkCredential("user", "pwd");

            MockEndpointCatalog catalog = new MockEndpointCatalog();
            MockEndpoint endpoint = new MockEndpoint("endpoint");
            endpoint.Default = new MockEndpointConfig(defaultURL);
            endpoint.Default.Credential = creds;
            catalog.Endpoints.Add("endpoint", endpoint);

            WCFProxyFactory<IMockService> factory = new WCFProxyFactory<IMockService>((IEndpointCatalog)catalog);

            MockClient proxy = (MockClient)factory.GetOnlineProxy(request, "Network");

            Exception exception = null;

            factory.CallOnlineProxyMethod(proxy, request, ref exception);

            Assert.IsTrue(proxy.MethodCalled);
        }

        [TestMethod]
        public void GetOnlineProxyUsesProxyAddressIfNoEndPointConfigured()
        {
            Request request = new Request();
            request.Endpoint = "MissingEndpoint";
            request.MethodName = "CalledMethod";
            request.OnlineProxyType = typeof(MockClient);
            MockEndpointCatalog catalog = new MockEndpointCatalog();
            WCFProxyFactory<IMockService> factory = new WCFProxyFactory<IMockService>((IEndpointCatalog)catalog);

            MockClient proxy = (MockClient)factory.GetOnlineProxy(request, "Network");

            Assert.IsNotNull(proxy);
            Assert.IsNotNull(proxy.Endpoint);
            Assert.AreEqual(new EndpointAddress("http://myurl.com"), proxy.Endpoint.Address);
        }

        [TestMethod]
        public void CloseOpenedWCFClientBaseOnReleaseOnlineProxy()
        {
            using (ServiceHost serviceHost = new ServiceHost(typeof(MockForReleaseService)))
            {
                serviceHost.Open();
                WCFProxyFactory<IMockForReleaseService> factory = new WCFProxyFactory<IMockForReleaseService>();
                MockClientForRelease client = new MockClientForRelease();
                client.Foo();
                factory.ReleaseOnlineProxy(client);

                Assert.AreEqual(CommunicationState.Closed, client.State);
            }
        }

        [TestMethod]
        public void WhenServiceHostIsDownProxyFactoryShouldLeaveTheClientInClosedState()
        {
            WCFProxyFactory<IMockForReleaseService> factory = new WCFProxyFactory<IMockForReleaseService>();
            MockClientForRelease client = new MockClientForRelease();
            try
            {
                client.Foo();
                Assert.Fail("Should have thrown exception");
            }
            catch (CommunicationException)
            {
                Assert.AreEqual(CommunicationState.Faulted, client.State);
                factory.ReleaseOnlineProxy(client);
                Assert.AreEqual(CommunicationState.Closed, client.State);
            }
        }

        [TestMethod]
        public void WhenServiceThrowsProxyFactoryShouldLeaveTheClientInClosedState()
        {
            using (ServiceHost serviceHost = new ServiceHost(typeof(MockForReleaseService)))
            {
                serviceHost.Open();

                WCFProxyFactory<IMockForReleaseService> factory = new WCFProxyFactory<IMockForReleaseService>();
                MockClientForRelease client = new MockClientForRelease();
                try
                {
                    client.FooThrow();
                    Assert.Fail("Should have thrown exception");
                }
                catch (Exception)
                {
                    Assert.AreEqual(CommunicationState.Faulted, client.State);
                    factory.ReleaseOnlineProxy(client);
                    Assert.AreEqual(CommunicationState.Closed, client.State);
                }
            }
        }

    }

    internal class MockClient : ClientBase<IMockService>, IMockService
    {
        private ClientCredentials clientCredentials = new ClientCredentials();
        private ServiceEndpoint endpoint = new ServiceEndpoint(new ContractDescription("contract"));

        public bool MethodCalled = false;

        public MockClient()
        {
            base.Endpoint.Address = new EndpointAddress("http://myurl.com");
        }

        public void CalledMethod()
        {
            MethodCalled = true;
        }
    }

    internal class MockClientForRelease : ClientBase<IMockForReleaseService>, IMockForReleaseService
    {
        #region IMockForReleaseService Members

        public void Foo()
        {
            base.Channel.Foo();
        }

        public void FooThrow()
        {
            base.Channel.FooThrow();
        }
        #endregion
    }
}