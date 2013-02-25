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

namespace Microsoft.Practices.SmartClient.ConnectionMonitor
{
	/// <summary>
	///	This class has the responsibility to manage the connections and networks.
	/// </summary>
	/// <remarks>
	///	The monitor has two collections, a connections collection and a networks collection.
	///	It wires the added connections to get the connection status changes
	///	and to get the current Network.
	/// </remarks>
	public class ConnectionMonitor : IDisposable
	{
		private ConnectionCollection connections;
		private NetworkCollection networks;


		/// <summary>
		/// Constructor of the <see cref="ConnectionMonitor"/> class.
		/// </summary>
		public ConnectionMonitor()
			: this(new ConnectionCollection(), new NetworkCollection())
		{
		}

		/// <summary>
		/// Initializes a new instance using the specified <see cref="ConnectionCollection"/> and 
		/// <see cref="NetworkCollection"/>.
		/// </summary>
		/// <param name="connections"></param>
		/// <param name="networks"></param>
		public ConnectionMonitor(ConnectionCollection connections, NetworkCollection networks)
		{
			this.connections = connections;
			this.networks = networks;

			if (this.connections.Count > 0)
			{
				foreach (Connection conn in this.connections)
				{
					conn.StateChanged += OnStateChanged;
				}
			}

			this.connections.ConnectionAdded += OnConnectionAdded;
			this.connections.ConnectionRemoved += OnConnectionRemoved;
		}

		/// <summary>
		/// Handles the <see cref="ConnectionCollection.ConnectionAdded"/> event.
		/// </summary>
		protected void OnConnectionAdded(object sender, ConnectionEventArgs e)
		{
			e.Connection.StateChanged += OnStateChanged;
			networks.UpdateStatus();
		}

		/// <summary>
		/// Handles the <see cref="ConnectionCollection.ConnectionRemoved"/> event.
		/// </summary>
		protected void OnConnectionRemoved(object server, ConnectionEventArgs e)
		{
			e.Connection.StateChanged -= OnStateChanged;
			networks.UpdateStatus();
		}

		/// <summary>
		/// Get the networks collection.
		/// </summary>
		public NetworkCollection Networks
		{
			get { return networks; }
		}

		/// <summary>
		/// Get the connections collection.
		/// </summary>
		public ConnectionCollection Connections
		{
			get { return connections; }
		}

		/// <summary>
		/// Returns <see langword="true"/> if any connection is connected; otherwise, returns <see langword="false"/>.
		/// </summary>
		public bool IsConnected
		{
			get { return ((connections.ActiveConnections.Count > 0) && (Networks.ActiveNetworks.Count > 0)); }
		}


		private void OnStateChanged(object sender, StateChangedEventArgs args)
		{
			networks.UpdateStatus();
		}

		// IDisposable implementation
		private bool disposed;

		/// <summary>
		/// Instance destructor.
		/// </summary>
		~ConnectionMonitor()
		{
			Dispose(false);
		}

		/// <summary>
		/// Releases resources handled resources.
		/// </summary>
		/// <param name="disposing">Indicates if the object is being disposed or finalized.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (!disposed)
				{
					foreach (Connection connection in connections)
					{
						connection.StateChanged -= OnStateChanged;
					}
					connections.ConnectionAdded -= OnConnectionAdded;
					connections.ConnectionRemoved -= OnConnectionRemoved;
				}
				disposed = true;
			}
		}

		/// <summary>
		/// Disposes the instance.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}