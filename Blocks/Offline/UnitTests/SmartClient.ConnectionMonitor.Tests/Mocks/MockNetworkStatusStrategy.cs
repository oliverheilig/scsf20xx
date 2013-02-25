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

namespace Microsoft.Practices.SmartClient.ConnectionMonitor.Tests.Mocks
{
	public class MockNetworkStatusStrategy : INetworkStatusStrategy
	{
		public Dictionary<string, bool> NetworkStatus = new Dictionary<string, bool>();

		public MockNetworkStatusStrategy()
		{
			NetworkStatus["http://www.contoso.com"] = true;
			NetworkStatus["http://intranet"] = true;
			NetworkStatus["http://myserver"] = true;
		}

		public List<string> LastIsAliveAddresses = new List<string>();

		public bool IsAlive(string hostnameOrAddress)
		{
			LastIsAliveAddresses.Add(hostnameOrAddress);
			bool result = false;
			NetworkStatus.TryGetValue(hostnameOrAddress, out result);
			return result;
		}
	}
}