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
	///	Abstract class represents the connection type interface.
	/// </summary>
	///	<remarks>
	///	Concrete implementations should provide connection status detection and notification.
	/// </remarks>
	public abstract class Connection
	{
		private string connectionTypeName;
		private int price;

		/// <summary>
		///	This method raises the StateChanged event from a connection concrete 
		///	implementation.
		/// </summary>
		/// <param name="arg">
		///	Status change information in an <see cref="StateChangedEventArgs"/> object.
		/// </param>
		protected void RaiseStateChanged(StateChangedEventArgs arg)
		{
			if (StateChanged != null)
				StateChanged(this, arg);
		}

		/// <summary>
		///	Default functionality for constructors of concrete derived classes of the <see cref="Connection"/> class.
		/// </summary>
		/// <param name="connectionTypeName">
		///	Provides the connection type name for the connection type object.
		/// </param>
		/// <param name="price">
		///	Provides the price for the connection type object.
		/// </param>
		protected Connection(string connectionTypeName, int price)
		{
			if (price < 0)
			{
				throw new ArgumentOutOfRangeException("price");
			}

			this.connectionTypeName = connectionTypeName;
			this.price = price;
		}

		/// <summary>
		///	Event fired when something in the connection status has changed.
		/// </summary>
		/// <remarks>
		///	After subscribing to this event, the <see cref="UpdateStatus"/> method
		///	should be called to get the initial status from the connection type
		///	if it's already connected (firing this event).
		/// </remarks>
		public event EventHandler<StateChangedEventArgs> StateChanged;

		/// <summary>
		///	Gets the connection type name.
		/// </summary>
		public string ConnectionTypeName
		{
			get { return connectionTypeName; }
		}

		/// <summary>
		///	Pricing information for the connection.
		/// </summary>
		public int Price
		{
			get { return price; }
			set { price = value; }
		}

		/// <summary>
		///	Get the connection status for the connection type.
		/// </summary>
		public abstract bool IsConnected { get; }

		/// <summary>
		///	Updates the StateChanged event subscribers if the connection is already connected.
		/// </summary>
		public void UpdateStatus()
		{
			if (IsConnected)
			{
				StateChangedEventArgs arg = new StateChangedEventArgs(true);
				RaiseStateChanged(arg);
			}
		}

		/// <summary>
		/// Returns a string containing detailed information about the connection 
		/// </summary>
		public abstract string GetDetailedInfoString();
	}
}