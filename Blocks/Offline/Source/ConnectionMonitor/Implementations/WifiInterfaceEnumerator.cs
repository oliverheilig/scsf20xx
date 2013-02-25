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
using Microsoft.Practices.SmartClient.ConnectionMonitor.Properties;

namespace Microsoft.Practices.SmartClient.ConnectionMonitor.Implementations
{
	/// <summary>
	/// Helper class used to enumerate the wifi adapters available on the system
	/// </summary>
	public static class WifiInterfaceEnumerator
	{
		private static IWifiAdapterEnumerator instance;

		static WifiInterfaceEnumerator()
		{
			if (OSVersionChecker.IsWindowsVista())
			{
				instance = new VistaWirelessEnumerator();
				return;
			}
			if (OSVersionChecker.IsWindowsXPSP2())
			{
				instance = new XPWirelessEnumerator();
				return;
			}

			throw new ConnectionMonitorException(Resources.ErrorUnsupportedOS);
		}

		/// <summary>
		/// Compiles a list of the available wifi adapters in the system.
		/// </summary>
		/// <returns>A list of strings with the GUID of the wifi adapters.</returns>
		public static List<string> EnumerateWirelessAdapters()
		{
			return instance.EnumerateWirelessAdapters();
		}
	}
}