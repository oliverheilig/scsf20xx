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
namespace Microsoft.Practices.SmartClient.ConnectionMonitor
{
	/// <summary>
	/// Service to discover if the client is able to get to a specific network address
	/// </summary>
	public interface INetworkStatusStrategy
	{
		/// <summary>
		/// Informs if the specified host or address is reachable from the client.
		/// </summary>
		/// <param name="hostnameOrAddress">A string that identifies the computer that is the destination to test.</param>
		/// <returns><see langword="true"/> if the destination can be reach; otherwise, <see langword="false"/>.</returns>
		bool IsAlive(string hostnameOrAddress);
	}
}