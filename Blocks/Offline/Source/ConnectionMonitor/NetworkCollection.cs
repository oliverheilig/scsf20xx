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
using System.Collections.ObjectModel;

namespace Microsoft.Practices.SmartClient.ConnectionMonitor
{
	/// <summary>
	///	This class is a collection of networks. 
	/// </summary>
	public class NetworkCollection : KeyedCollection<string, Network>
	{
		private INetworkStatusStrategy _statusStrategy;
		private List<Network> _connectedNetworks = new List<Network>();

		/// <summary>
		/// Event fired whenever the status of a <see cref="Network"/> in the collection has changed.
		/// </summary>
		public event EventHandler<NetworkConnectionStatusChangedEventArgs> NetworkConnectionStatusChanged;

		/// <summary>
		/// Initializes a new <see cref="NetworkCollection"/> using the
		/// <see cref="HttpPingStatusStrategy"/> for the network connectivity detection strategy.
		/// </summary>
		public NetworkCollection()
			: this(new HttpPingStatusStrategy())
		{
		}

		/// <summary>
		/// Initializes a new instance using the specified <see cref="INetworkStatusStrategy"/>
		/// for the network connectivity detection strategy.
		/// </summary>
		/// <param name="networkStatusStrategy"></param>
		public NetworkCollection(INetworkStatusStrategy networkStatusStrategy)
		{
			Guard.ArgumentNotNull(networkStatusStrategy, "networkStatusService");

			_statusStrategy = networkStatusStrategy;
		}

		/// <summary>
		/// Gets the collection of the last known connected networks.
		/// </summary>
		public ReadOnlyCollection<Network> ActiveNetworks
		{
			get { return _connectedNetworks.AsReadOnly(); }
		}

		/// <summary>
		/// Gets the <see cref="INetworkStatusStrategy"/> used by the collection to determine online status.
		/// </summary>
		public INetworkStatusStrategy NetworkStatusStrategy
		{
			get { return _statusStrategy; }
		}

		/// <summary>
		/// Forces the collection to update the connectivity status for all the networks 
		/// in the collection.
		/// </summary>
		public void UpdateStatus()
		{
			_connectedNetworks.Clear();

			foreach (Network network in base.Items)
			{
				bool connectedNow = _statusStrategy.IsAlive(network.Address);
				if (connectedNow)
				{
					_connectedNetworks.Add(network);
				} 
				
				network.Connected = connectedNow;
				OnNetworkConnectionStatusChanged(network);
			}
		}

		/// <summary>
		/// Fires the <see cref="NetworkConnectionStatusChanged"/> event.
		/// </summary>
		/// <param name="network"></param>
		protected void OnNetworkConnectionStatusChanged(Network network)
		{
			if (NetworkConnectionStatusChanged != null)
			{
				NetworkConnectionStatusChanged(this, new NetworkConnectionStatusChangedEventArgs(network));
			}
		}

		/// <summary>
		/// Extracts the key information for the <see cref="Network"/> item.
		/// </summary>
		/// <returns>A <see cref="string"/> with the key value for the<see cref="Network"/>.</returns>
		protected override string GetKeyForItem(Network item)
		{
			Guard.ArgumentNotNull(item, "item");

			return item.Name;
		}

		/// <summary>
		/// Inserts a <see cref="Network"/> item at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index at which the item should be inserted.</param>
		/// <param name="item">The <see cref="Network"/> item to insert in the collection.</param>
		protected override void InsertItem(int index, Network item)
		{
			base.InsertItem(index, item);

			UpdateStatus();
		}
	}
}