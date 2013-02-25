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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.SmartClient.ConnectionMonitor.Tests
{
	/// <summary>
	/// Summary description for ConnectionMonitorFactoryFixture
	/// </summary>
	[TestClass]
	public class ConnectionMonitorFactoryFixture
	{
		[TestMethod]
		public void InitializeFromDefault()
		{
			using (new ConnectionMonitorFactoryReset())
			{
				ConnectionMonitor monitor = ConnectionMonitorFactory.CreateFromConfiguration();

				Assert.AreEqual(3, monitor.Connections.Count);
				Assert.IsTrue(monitor.Connections.Contains("WiredConnection"));
				Assert.IsTrue(monitor.Connections.Contains("WirelessConnection"));
				Assert.IsTrue(monitor.Connections.Contains("DesktopConnection"));

				Assert.AreEqual(3, monitor.Networks.Count);
				Assert.IsTrue(monitor.Networks.Contains("Internet"));
				Assert.IsTrue(monitor.Networks.Contains("Work"));
				Assert.IsTrue(monitor.Networks.Contains("Home"));

				Assert.AreSame(monitor, ConnectionMonitorFactory.Instance);
			}
		}

		[TestMethod]
		public void CanCreateNonIncludedType()
		{
			using (new ConnectionMonitorFactoryReset())
			{
				ConnectionMonitor monitor = ConnectionMonitorFactory.CreateFromConfiguration("CustomConnection");

				Assert.AreEqual(4, monitor.Connections.Count);
				Assert.IsTrue(monitor.Connections.Contains("WiredConnection"));
				Assert.IsTrue(monitor.Connections.Contains("WirelessConnection"));
				Assert.IsTrue(monitor.Connections.Contains("DesktopConnection"));
				Assert.IsTrue(monitor.Connections.Contains("MyCustomConnection"));

				Assert.AreEqual(3, monitor.Networks.Count);
				Assert.IsTrue(monitor.Networks.Contains("Internet"));
				Assert.IsTrue(monitor.Networks.Contains("Work"));
				Assert.IsTrue(monitor.Networks.Contains("Home"));

				Assert.AreSame(monitor, ConnectionMonitorFactory.Instance);
			}
		}

		
		[TestMethod]
		[ExpectedException(typeof (InvalidOperationException))]
		public void CannotGetInstanceIfNotInitialized()
		{
			using (new ConnectionMonitorFactoryReset())
			{
				ConnectionMonitor monitor = ConnectionMonitorFactory.Instance;
			}
		}


		[TestMethod]
		[ExpectedException(typeof (ArgumentException))]
		public void ReadingConfigurationWithDuplicatedNetworks()
		{
			using (new ConnectionMonitorFactoryReset())
			{
				ConnectionMonitorFactory.CreateFromConfiguration("DuplicatedNetworks");
			}
		}
	}
}