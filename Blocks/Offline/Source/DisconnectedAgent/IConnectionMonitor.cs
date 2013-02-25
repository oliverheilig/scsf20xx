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
using System.Collections.Generic;

namespace Microsoft.Practices.SmartClient.DisconnectedAgent
{
	/// <summary>
	/// Specifies the contract for connection monitors.
	/// </summary>
	public interface IConnectionMonitor
	{
		/// <summary>
		/// Get the current connection price.
		/// It should throw an InvalidOperationException if there is not connection.
		/// </summary>
		int CurrentConnectionPrice { get; }

		/// <summary>
		/// Get the current network.
		/// </summary>
		string CurrentNetwork { get; }

		/// <summary>
		/// Gets the connection state.
		/// </summary>
		bool IsConnected { get; }

		/// <summary>
		/// Gets a List of the currently connected networks.
		/// </summary>
		List<string> ConnectedNetworks { get; }

		/// <summary>
		/// Event fired when the device gets connected.
		/// </summary>
		event EventHandler ConnectionStatusChanged;
	}
}