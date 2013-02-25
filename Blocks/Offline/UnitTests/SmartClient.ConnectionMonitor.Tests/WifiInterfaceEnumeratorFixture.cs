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
using System.Collections.Generic;
using Microsoft.Practices.SmartClient.ConnectionMonitor.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.SmartClient.ConnectionMonitor.Tests
{
	/// <summary>
	/// Summary description for WifiInterfaceEnumeratorFixture
	/// </summary>
	[TestClass]
	public class WifiInterfaceEnumeratorFixture
	{
		[TestMethod]
		public void TestVistaAPI()
		{
			if (OSVersionChecker.IsWindowsVista())
			{
				IWifiAdapterEnumerator enumerator = new VistaWirelessEnumerator();
				List<string> adapters = enumerator.EnumerateWirelessAdapters();
				Assert.IsNotNull(adapters);
			}
			else
			{
				Assert.IsFalse(false, "Test is skipped when not run in Vista.");
			}
		}

		[TestMethod]
		public void TestWindowsXPAPI()
		{
			if (OSVersionChecker.IsWindowsXPSP2() && !OSVersionChecker.IsWindowsVista())
			{
				IWifiAdapterEnumerator enumerator = new XPWirelessEnumerator();
				List<string> adapters = enumerator.EnumerateWirelessAdapters();
				Assert.IsNotNull(adapters);
			}
			else
			{
				Assert.IsFalse(false, "Test is skipped when not run in Windows XP/SP2.");
			}
		}
	}
}