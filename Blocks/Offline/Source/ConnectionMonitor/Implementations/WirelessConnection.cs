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
using System.Net.NetworkInformation;

namespace Microsoft.Practices.SmartClient.ConnectionMonitor.Implementations
{
	/// <summary>
	/// An implementation of <see cref="Connection"/> that only detects availabilty
	/// for wireless connections.
	/// </summary>
	public class WirelessConnection : NicConnection
	{
		/// <summary>
		/// Initializes a new instance.
		/// </summary>
		/// <param name="name">The name for this connection type.</param>
		/// <param name="price">The associated price for using this connection.</param>
		public WirelessConnection(string name, int price)
			: base(name, price)
		{
		}

		/// <summary>
		/// Detects a connected <see cref="NetworkInterface"/> adapter.
		/// </summary>
		/// <returns>The first connected wifi adapter found, or <see langworg="null"/>.</returns>
		protected override NetworkInterface DetectConnectedAdapter()
		{
			List<string> wifiAdapters = WifiInterfaceEnumerator.EnumerateWirelessAdapters();
			foreach (NetworkInterface adapter in NetworkInterface.GetAllNetworkInterfaces())
			{
				if ((adapter.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || wifiAdapters.Contains(adapter.Id)) &&
				    adapter.OperationalStatus == OperationalStatus.Up)
				{
					return adapter;
				}
			}
			return null;
		}
	}
}