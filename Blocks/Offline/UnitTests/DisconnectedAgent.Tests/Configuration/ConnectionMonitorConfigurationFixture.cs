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
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.SmartClient.DisconnectedAgent.Configuration;
using System.Configuration;

namespace Microsoft.Practices.SmartClient.DisconnectedAgent.Tests.Configuration
{
	/// <summary>
	/// Summary description for ConnectionMonitorConfigurationFixture
	/// </summary>
	[TestClass]
	public class ConnectionMonitorConfigurationFixture
	{
		[TestMethod]
		public void GetSectionDoesNotGetNull()
		{
			ConnectionMonitorConfigurationSection section = ConfigurationManager.GetSection("ConnectionMonitors") as ConnectionMonitorConfigurationSection;

			Assert.IsNotNull(section);
		}

		[TestMethod]
		public void MonitorCollectionIsNotNull()
		{
			ConnectionMonitorConfigurationSection section = ConfigurationManager.GetSection("ConnectionMonitors") as ConnectionMonitorConfigurationSection;

			Assert.IsNotNull(section.Monitors);
		}

		[TestMethod]
		public void CollectionGetsConfiguredItems()
		{
			ConnectionMonitorConfigurationSection section = ConfigurationManager.GetSection("ConnectionMonitors") as ConnectionMonitorConfigurationSection;

			Assert.AreEqual(2, section.Monitors.Count);
			Assert.IsNotNull(section.Monitors["Monitor1"]);
			Assert.IsNotNull(section.Monitors["Monitor2"]);

			Assert.AreEqual("Monitor1Type", section.Monitors["Monitor1"].MonitorType);
			Assert.AreEqual("Monitor2Type", section.Monitors["Monitor2"].MonitorType);
		}

		[TestMethod]
		public void MonitorElementGetConfiguredEndpoints()
		{
			ConnectionMonitorConfigurationSection section = ConfigurationManager.GetSection("ConnectionMonitors") as ConnectionMonitorConfigurationSection;

			Assert.AreEqual(0, section.Monitors["Monitor1"].Endpoints.Count);
			Assert.IsNotNull(section.Monitors["Monitor2"].Endpoints["Endpoint1"]);
			Assert.IsNotNull(section.Monitors["Monitor2"].Endpoints["Endpoint2"]);
		}

	}
}
