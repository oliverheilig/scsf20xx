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
using System.Configuration;
using Microsoft.Practices.SmartClient.ConnectionMonitor.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.SmartClient.ConnectionMonitor.Tests.Configuration
{
	/// <summary>
	/// Summary description for NetworkDetectionFixture
	/// </summary>
	[TestClass]
	public class NetworkConfigurationFixture
	{
		[TestMethod]
		public void NetworkDetectionStrategyMatchesConfigFile()
		{
			{
				ConnectionSettingsSection config = (ConnectionSettingsSection) ConfigurationManager.GetSection("ConnectionMonitor");

				Assert.IsNotNull(config.Networks);
				Assert.AreEqual(3, config.Networks.Count);
				Assert.AreEqual("Internet", config.Networks[0].Name);
				Assert.AreEqual("Work", config.Networks[1].Name);
				Assert.AreEqual("Home", config.Networks[2].Name);
			}
		}
	}
}