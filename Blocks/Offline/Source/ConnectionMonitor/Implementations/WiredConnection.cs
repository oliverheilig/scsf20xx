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
	/// An implementation of <see cref="Connection"/> that only detects 
	/// availabily for wired (Ethernet) connections.
	/// </summary>
	public class WiredConnection : NicConnection
	{
		/// <summary>
		/// Initializes a new instance.
		/// </summary>
		/// <param name="name">The type name to use for this connection.</param>
		/// <param name="price">The associated price to use this connection.</param>
		public WiredConnection(string name, int price)
			: base(name, price)
		{
		}

		/// <summary>
		/// Detects a connected <see cref="NetworkInterface"/> adapter. 
		/// Overriden to only detect wired adapters.
		/// </summary>
		/// <returns>The first connected <see cref="NetworkInterface"/> found, or <see langworg="null"/>.</returns>
		protected override NetworkInterface DetectConnectedAdapter()
		{
			List<string> wifiAdapters = WifiInterfaceEnumerator.EnumerateWirelessAdapters();
			foreach (NetworkInterface adapter in NetworkInterface.GetAllNetworkInterfaces())
			{
				if ((adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet ||
				     adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet3Megabit ||
				     adapter.NetworkInterfaceType == NetworkInterfaceType.FastEthernetFx ||
				     adapter.NetworkInterfaceType == NetworkInterfaceType.FastEthernetT ||
				     adapter.NetworkInterfaceType == NetworkInterfaceType.GigabitEthernet) &&
				    wifiAdapters.Contains(adapter.Id) == false &&
				    adapter.OperationalStatus == OperationalStatus.Up)
				{
					return adapter;
				}
			}

			return null;
		}
	}
}