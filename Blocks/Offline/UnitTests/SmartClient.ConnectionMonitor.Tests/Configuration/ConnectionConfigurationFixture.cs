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
	//<Connections>
	//	<ConnectionItems>
	//		<add  Type="CellConnection" Price="8"/>
	//		<add  Type="NicConnection" Price="2"/>
	//		<add  Type="DesktopConnection" Price="1"/>
	//	</ConnectionItems>
	//</Connections>
	[TestClass]
	public class ConnectionConfigurationFixture
	{
		[TestMethod]
		public void GetConnectionsSectionDoesntGetNull()
		{
			ConnectionSettingsSection section = (ConnectionSettingsSection) ConfigurationManager.GetSection("ConnectionMonitor");
			Assert.IsNotNull(section);
		}

		[TestMethod]
		public void SectionGetConnectionItemsCollectionDoesntGetNull()
		{
			ConnectionSettingsSection section = (ConnectionSettingsSection) ConfigurationManager.GetSection("ConnectionMonitor");

			Assert.IsNotNull(section.Connections);
		}

		[TestMethod]
		public void SectionGetEndpointItemsCollectionContainsSameQuantityThanAppConfig()
		{
			ConnectionSettingsSection section = (ConnectionSettingsSection) ConfigurationManager.GetSection("ConnectionMonitor");

			Assert.AreEqual(3, section.Connections.Count);
		}

		[TestMethod]
		public void ConnectionItemsInConnectionCollectionAreTheSameThanAppConfig()
		{
			ConnectionSettingsSection section = (ConnectionSettingsSection) ConfigurationManager.GetSection("ConnectionMonitor");

			foreach (ConnectionItemElement itemConnection in section.Connections)
			{
				switch (itemConnection.Type)
				{
					case ConnectionFactory.WiredConnection:
						Assert.AreEqual(2, itemConnection.Price);
						break;
					case ConnectionFactory.WirelessConnection:
						Assert.AreEqual(4, itemConnection.Price);
						break;
					case ConnectionFactory.DesktopConnection:
						Assert.AreEqual(8, itemConnection.Price);
						break;
					default:
						Assert.Fail();
						break;
				}
			}

			Assert.AreEqual(3, section.Connections.Count);
		}
	}
}