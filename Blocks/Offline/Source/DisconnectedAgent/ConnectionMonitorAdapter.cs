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
using Microsoft.Practices.SmartClient.ConnectionMonitor;
using Microsoft.Practices.SmartClient.DisconnectedAgent.Properties;

namespace Microsoft.Practices.SmartClient.DisconnectedAgent
{
	/// <summary>
	/// Adapter that implements the IConnectionMonitor interface required by the
	/// Disconnected Agent Block using the ConnectionMonitor block.
	/// </summary>
	public class ConnectionMonitorAdapter : IConnectionMonitor, IDisposable
	{
		private bool disposed;
		private ConnectionMonitor.ConnectionMonitor monitor;

		/// <summary>
		/// Constructor which creates a new adapter using the given ConnectionMonitor.
		/// </summary>
		/// <param name="monitor">ConnectionMonitor to be engaged.</param>
		public ConnectionMonitorAdapter(ConnectionMonitor.ConnectionMonitor monitor)
		{
			Guard.ArgumentNotNull(monitor, "monitor");
			this.monitor = monitor;
			this.monitor.Connections.ConnectionStatusChanged +=
				new EventHandler<ConnectionEventArgs>(OnConnectionStatusChanged);
			this.monitor.Networks.NetworkConnectionStatusChanged +=
				new EventHandler<NetworkConnectionStatusChangedEventArgs>(
					OnNetworkConnectionStatusChanged);
		}

		/// <summary>
		/// Finalizer for the ConnectionMonitorAdapter.
		/// </summary>
		~ConnectionMonitorAdapter()
		{
			Dispose(false);
		}

		/// <summary>
		/// Fired when the ConnectionMonitor gets connection.
		/// </summary>
		public event EventHandler ConnectionStatusChanged;

		/// <summary>
		/// This method gets the current connection price from the ConnectionMonitor.
		/// </summary>
		/// <exception cref="InvalidOperationException">
		/// If there is not active connection this method throws an InvalidOperationException.
		/// </exception>
		public int CurrentConnectionPrice
		{
			get
			{
				if (disposed) throw new ObjectDisposedException("ConnectionManagerAdapter");
				if (monitor.Connections.ActiveConnections.Count == 0)
				{
					throw new InvalidOperationException(Resources.PriceNotConnection);
				}

				int price = int.MaxValue;
				foreach (Connection connection in monitor.Connections)
				{
					if (connection.Price < price)
					{
						price = connection.Price;
					}
				}

				return price;
			}
		}

		/// <summary>
		/// Gets the current network name.
		/// If there is not active network returns null.
		/// </summary>
		public string CurrentNetwork
		{
			get
			{
				if (disposed) throw new ObjectDisposedException("ConnectionManagerAdapter");
				if (monitor.Networks.ActiveNetworks.Count == 0) return null;
				return monitor.Networks.ActiveNetworks[0].Name;
			}
		}

		/// <summary>
		/// Gets the connection state.
		/// </summary>
		public bool IsConnected
		{
			get
			{
				if (disposed) throw new ObjectDisposedException("ConnectionManagerAdapter");
				return monitor.IsConnected;
			}
		}

		/// <summary>
		/// Gets a List of the currently connected networks.
		/// </summary>
		public List<string> ConnectedNetworks
		{
			get
			{
				List<string> result = new List<string>();
				foreach (Network network in monitor.Networks.ActiveNetworks)
				{
					result.Add(network.Name);
				}
				return result;
			}
		}

		/// <summary>
		/// This method disposes adapter used resources.
		/// </summary>
		public void Dispose()
		{
			if (disposed) throw new ObjectDisposedException("ConnectionManagerAdapter");
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Fires the <see cref="ConnectionStatusChanged"/> event.
		/// </summary>
		/// <param name="sender">The object that is firing the event.</param>
		/// <param name="e">The event arguments.</param>
		protected virtual void OnConnectionStatusChanged(object sender, ConnectionEventArgs e)
		{
			if (ConnectionStatusChanged != null)
			{
				ConnectionStatusChanged(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Fires the <see cref="ConnectionStatusChanged"/> event.
		/// </summary>
		/// <param name="sender">The object that is firing the event.</param>
		/// <param name="e">The event arguments.</param>
		protected virtual void OnNetworkConnectionStatusChanged(
			object sender, NetworkConnectionStatusChangedEventArgs e)
		{
			if (ConnectionStatusChanged != null && monitor.Networks.ActiveNetworks.Count > 0)
			{
				ConnectionStatusChanged(this, EventArgs.Empty);
			}
		}

		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (!disposed)
				{
					monitor.Connections.ConnectionStatusChanged -= OnConnectionStatusChanged;
					monitor.Networks.NetworkConnectionStatusChanged -=
						OnNetworkConnectionStatusChanged;
				}
				disposed = true;
			}
		}
	}
}