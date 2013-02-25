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
using Microsoft.Practices.SmartClient.ConnectionMonitor.Implementations;
using Microsoft.Practices.SmartClient.ConnectionMonitor.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.SmartClient.ConnectionMonitor.Tests
{
	[TestClass]
	public class ConnectionMonitorFixture
	{
		private ConnectionMonitor CreateConnectionMonitor()
		{
			ConnectionMonitor monitor = new ConnectionMonitor(new ConnectionCollection(), new TestableNetworkManager());

			monitor.Networks.Add(new Network("Internet", "http://www.contoso.com"));
			monitor.Networks.Add(new Network("Work", "http://intranet"));
			monitor.Networks.Add(new Network("Home", "http://myserver"));

			return monitor;
		}

		[TestMethod]
		public void CanCreateConnectionMonitor()
		{
			ConnectionMonitor monitor = new ConnectionMonitor();
			Assert.IsNotNull(monitor);
		}

		[TestMethod]
		public void ExposesNetworks()
		{
			ConnectionMonitor monitor = CreateConnectionMonitor();

			Assert.IsNotNull(monitor.Networks);
		}

		[TestMethod]
		public void ExposesConnections()
		{
			ConnectionMonitor monitor = CreateConnectionMonitor();

			Assert.IsNotNull(monitor.Connections);
		}

		[TestMethod]
		public void UpdatesActiveNetworksOnConnectionAdded()
		{
			ConnectionMonitor monitor = CreateConnectionMonitor();
			MockNetworkStatusStrategy strategy = ((TestableNetworkManager) monitor.Networks).MockStatusStrategy;
			MockConnection connection = new MockConnection("Test", 10);
			strategy.LastIsAliveAddresses.Clear();
			monitor.Connections.Add(connection);

			Assert.AreEqual(3, strategy.LastIsAliveAddresses.Count);
		}

		[TestMethod]
		public void UpdatesActiveNetworksOnConnectionRemoved()
		{
			ConnectionMonitor monitor = CreateConnectionMonitor();
			MockNetworkStatusStrategy strategy = ((TestableNetworkManager) monitor.Networks).MockStatusStrategy;
			MockConnection connection = new MockConnection("Test", 10);

			monitor.Connections.Add(connection);
			strategy.LastIsAliveAddresses.Clear();
			monitor.Connections.Remove(connection);

			Assert.AreEqual(3, strategy.LastIsAliveAddresses.Count);
		}

		[TestMethod]
		public void UpdatesActiveNetworksOnConnectionStatusChanged()
		{
			ConnectionMonitor monitor = CreateConnectionMonitor();
			MockNetworkStatusStrategy strategy = ((TestableNetworkManager) monitor.Networks).MockStatusStrategy;
			MockConnection connection = new MockConnection("Test", 10);
			monitor.Connections.Add(connection);
			strategy.LastIsAliveAddresses.Clear();

			connection.Disconnect();

			Assert.AreEqual(3, strategy.LastIsAliveAddresses.Count);
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentException))]
		public void TestAddConnectionsThroughMonitor()
		{
			DesktopConnection objDesktop = new DesktopConnection("Test", 100);
			NicConnection objNIC = new WiredConnection("Local Area Connection", 100);
			Connection objConn1 = objNIC;
			ConnectionMonitor objConMonitor1;
			objConMonitor1 = new ConnectionMonitor();
			ConnectionCollection objConnColl1 = objConMonitor1.Connections;

			objConnColl1.Add(objDesktop);
			objConnColl1.Add(objNIC);
			objConnColl1.Add(objConn1);
		}

		[TestMethod]
		public void TestAddConnectionToConstructedMonitor()
		{
			using (new ConnectionMonitorFactoryReset())
			{
				NicConnection objNIC = new WiredConnection("Local Area Connection", 100);
				ConnectionMonitor objConMonitor1;
				objConMonitor1 = ConnectionMonitorFactory.CreateFromConfiguration();
				ConnectionCollection objConnColl1;
				objConnColl1 = objConMonitor1.Connections;
				objConnColl1.Add(objNIC);

				// 3 from config plus the one just added
				Assert.AreEqual(4, objConnColl1.Count);
			}
		}
	}
}