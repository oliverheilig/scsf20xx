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
	///	Represents a logical network.
	/// </summary>
	public class Network
	{
		private string _name;
		private string _address;
		private bool _connected;

		/// <summary>
		/// Event fired when the connectivity status of <see cref="Network"/> in the collection changed.
		/// </summary>
		public event EventHandler<StateChangedEventArgs> StateChanged;

		/// <summary>
		/// Default ..ctor
		/// </summary>
		/// <param name="name">Logical name for the network.</param>
		/// <param name="address">The address to use for testing connectivity to the network.</param>
		public Network(string name, string address)
		{
			Guard.StringNotNullOrEmpty(name, "name");
			Guard.StringNotNullOrEmpty(address, address);

			_name = name;
			_address = address;
		}

		/// <summary>
		/// Gets or sets the network connectivity status.
		/// </summary>
		public bool Connected
		{
			get { return _connected; }
			set
			{
				_connected = value;
				if (StateChanged != null)
				{
					StateChanged(this, new StateChangedEventArgs(_connected));
				}
			}
		}

		/// <summary>
		/// Gets or set the network test address.
		/// </summary>
		public string Address
		{
			get { return _address; }
			set { _address = value; }
		}

		/// <summary>
		/// Gets or set the network logical name.
		/// </summary>
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}
	}
}