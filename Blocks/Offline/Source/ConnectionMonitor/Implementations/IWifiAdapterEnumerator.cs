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

namespace Microsoft.Practices.SmartClient.ConnectionMonitor.Implementations
{
	/// <summary>
	/// Defines the contract to enumerate wireless adapters
	/// </summary>
	public interface IWifiAdapterEnumerator
	{
		/// <summary>
		/// Enumerates an returns a list of available wireless adapters on the system.
		/// </summary>
		/// <returns>A list os srtings with the GUID ids of the wireless adapters.</returns>
		List<string> EnumerateWirelessAdapters();
	}
}