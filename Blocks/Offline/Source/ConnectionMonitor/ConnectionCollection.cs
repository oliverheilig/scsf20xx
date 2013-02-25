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
	///	A collection of connections. 
	/// </summary>
	public class ConnectionCollection : KeyedCollection<string, Connection>
	{
		private List<Connection> _activeConnections = new List<Connection>();

		/// <summary>
		/// Event fired when a <see cref="Connection"/> is added to the collection.
		/// </summary>
		public event EventHandler<ConnectionEventArgs> ConnectionAdded;

		/// <summary>
		/// Event fired when a <see cref="Connection"/> is removed from the collection.
		/// </summary>
		public event EventHandler<ConnectionEventArgs> ConnectionRemoved;

		/// <summary>
		/// Event fired when the connectivity status of any connection in the collection changed.
		/// </summary>
		public event EventHandler<ConnectionEventArgs> ConnectionStatusChanged;

		/// <summary>
		/// Initializes a new instance of <see cref="ConnectionCollection"/>.
		/// </summary>
		public ConnectionCollection()
		{
		}

		/// <summary>
		/// Extractes the key for the <see cref="Connection"/>.
		/// </summary>
		/// <param name="item">The <see cref="Connection"/> to get the key for.</param>
		/// <returns>A <see cref="string"/> with the value of the key.</returns>
		protected override string GetKeyForItem(Connection item)
		{
			Guard.ArgumentNotNull(item, "item");

			return item.ConnectionTypeName;
		}

		/// <summary>
		/// Inserts a <see cref="Connection"/> item at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index at which item should be inserted.</param>
		/// <param name="item">The <see cref="Connection"/> to insert.</param>
		protected override void InsertItem(int index, Connection item)
		{
			base.InsertItem(index, item);
			item.StateChanged += OnConnectionStateChanged;

			OnConnectionAdded(item);
		}

		/// <summary>
		/// Removes the element at the specified index.
		/// </summary>
		/// <param name="index">The index of the element to remove.</param>
		protected override void RemoveItem(int index)
		{
			Connection connection = Items[index];
			connection.StateChanged -= OnConnectionStateChanged;
			base.RemoveItem(index);

			OnConnectionRemoved(connection);
		}

		/// <summary>
		/// Fires the <see cref="ConnectionAdded"/> event.
		/// </summary>
		/// <param name="connection">The <see cref="Connection"/> added to the collection.</param>
		protected void OnConnectionAdded(Connection connection)
		{
			if (connection.IsConnected)
			{
				_activeConnections.Add(connection);
			}
			if (ConnectionAdded != null)
			{
				ConnectionAdded(this, new ConnectionEventArgs(connection));
			}
		}

		/// <summary>
		/// Fires the <see cref="ConnectionRemoved"/> event.
		/// </summary>
		/// <param name="connection">The removed connection.</param>
		protected void OnConnectionRemoved(Connection connection)
		{
			if (_activeConnections.Contains(connection))
			{
				_activeConnections.Remove(connection);
			}

			if (ConnectionRemoved != null)
			{
				ConnectionRemoved(this, new ConnectionEventArgs(connection));
			}
		}

		/// <summary>
		/// Handles the <see cref="Connection.StateChanged"/> event. This method keeps 
		/// the <see cref="ActiveConnections"/> collection updated and fires the 
		/// <see cref="ConnectionStatusChanged"/> event.
		/// </summary>
		/// <param name="sender">The <see cref="Connection"/> firing the event.</param>
		/// <param name="args">The <see cref="StateChangedEventArgs"/> for the event.</param>
		protected virtual void OnConnectionStateChanged(object sender, StateChangedEventArgs args)
		{
			Connection connection = (Connection) sender;

			if (connection.IsConnected == false)
			{
				_activeConnections.Remove(connection);
			}
			else
			{
                if (!_activeConnections.Contains(connection))
                {
                    _activeConnections.Add(connection);
                }
			}

			if (ConnectionStatusChanged != null)
			{
				ConnectionStatusChanged(this, new ConnectionEventArgs(connection));
			}
		}

		/// <summary>
		/// Gets the collection of current active (or connected) <see cref="Connection"/> 
		/// in the collection.
		/// </summary>
		public ReadOnlyCollection<Connection> ActiveConnections
		{
			get { return _activeConnections.AsReadOnly(); }
		}
	}
}