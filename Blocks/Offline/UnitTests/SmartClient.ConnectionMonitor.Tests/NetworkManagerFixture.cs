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
using System.Collections.ObjectModel;
using Microsoft.Practices.SmartClient.ConnectionMonitor.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.SmartClient.ConnectionMonitor.Tests
{
	/// <summary>
	/// Summary description for NetworkDetectorFixture
	/// </summary>
	[TestClass]
	public class NetworkManagerFixture
	{
		private TestableNetworkManager CreateNetworkManager()
		{
			TestableNetworkManager manager = new TestableNetworkManager();
			manager.Add(new Network("Internet", "http://www.contoso.com"));
			manager.Add(new Network("Work", "http://intranet"));
			manager.Add(new Network("Home", "http://myserver"));

			return manager;
		}

		[TestMethod]
		public void CanAddNetwork()
		{
			TestableNetworkManager manager = new TestableNetworkManager();

			Assert.AreEqual(0, manager.Count);

			manager.Add(new Network("TestNetwork", "http://test"));

			Assert.AreEqual(1, manager.Count);
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentException))]
		public void AddNetworkFailsWhenAddingDuplicatedTarget()
		{
			NetworkCollection manager = CreateNetworkManager();
			manager.UpdateStatus();

			manager.Add(new Network("TestNetwork", "http://test1"));
			manager.Add(new Network("TestNetwork", "http://test2"));
		}

		[TestMethod]
		public void EnumerateConfiguredNetworks()
		{
			NetworkCollection manager = CreateNetworkManager();

			Assert.IsNotNull(manager);
			Assert.IsTrue(manager.Contains("Internet"));
			Assert.IsTrue(manager.Contains("Work"));
			Assert.IsTrue(manager.Contains("Home"));
		}

		[TestMethod]
		public void ActiveNetworksReturnsConnectedNetworksList()
		{
			TestableNetworkManager manager = CreateNetworkManager();
			MockNetworkStatusStrategy statusStrategy = manager.MockStatusStrategy;
			statusStrategy.NetworkStatus["http://myserver"] = false;
			manager.UpdateStatus();

			ReadOnlyCollection<Network> networks = manager.ActiveNetworks;

			Assert.AreEqual(2, networks.Count);
			Assert.AreEqual("Internet", networks[0].Name);
			Assert.AreEqual("Work", networks[1].Name);
		}

		[TestMethod]
		public void TestUpdateStatus()
		{
			TestableNetworkManager manager = CreateNetworkManager();
			MockNetworkStatusStrategy statusStrategy = manager.MockStatusStrategy;

			manager.UpdateStatus();

			ReadOnlyCollection<Network> networks = manager.ActiveNetworks;
			Assert.AreEqual(3, networks.Count);

			statusStrategy.NetworkStatus["http://myserver"] = false;
			statusStrategy.NetworkStatus["http://www.contoso.com"] = false;

			manager.UpdateStatus();

			networks = manager.ActiveNetworks;

			Assert.AreEqual(1, networks.Count);
			Assert.AreEqual("Work", manager.ActiveNetworks[0].Name);
		}

		[TestMethod]
		public void NetworkChangeEventRaisedWhenActiveNetworkChanges()
		{
			TestableNetworkManager manager = CreateNetworkManager();
			MockNetworkStatusStrategy statusStrategy = manager.MockStatusStrategy;
			statusStrategy.NetworkStatus["http://myserver"] = false;
			Network homeNetwork = manager["Home"];

			manager.UpdateStatus();

			bool handlerCalled = false;
			Network reportedNetwork = null;
			manager.NetworkConnectionStatusChanged += delegate(object sender, NetworkConnectionStatusChangedEventArgs e)
			                                          	{
			                                          		handlerCalled = true;
			                                          		reportedNetwork = e.Network;
			                                          	};

			// Connect a network
			statusStrategy.NetworkStatus["http://myserver"] = true;
			manager.UpdateStatus();


			Assert.AreEqual(3, manager.ActiveNetworks.Count);
			Assert.IsTrue(manager.ActiveNetworks.Contains(homeNetwork));
			Assert.IsTrue(handlerCalled);
			Assert.AreSame(homeNetwork, reportedNetwork);
		}

        [TestMethod]
        public void NetworkChangeEventRaisedWhenAnyNetworkChanges()
        {
            TestableNetworkManager manager = new TestableNetworkManager();
            manager.Add(new Network("Home", "http://myserver"));

            int eventRaisedCount = 0;
            manager.NetworkConnectionStatusChanged += delegate(object sender, NetworkConnectionStatusChangedEventArgs e)
            {
                eventRaisedCount++;
            };
            MockNetworkStatusStrategy statusStrategy = manager.MockStatusStrategy;
            statusStrategy.NetworkStatus["http://myserver"] = false;
            manager.UpdateStatus();

            statusStrategy.NetworkStatus["http://myserver"] = false;
            manager.UpdateStatus();

            statusStrategy.NetworkStatus["http://myserver"] = true;
            manager.UpdateStatus();

            Assert.AreEqual(3, eventRaisedCount);

        }

	}
}