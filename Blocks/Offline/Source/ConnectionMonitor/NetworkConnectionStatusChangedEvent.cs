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
	/// Event data for carried for the event fired when a <see cref="Network"/>
	/// connectivity status has changed.
	/// </summary>
	public class NetworkConnectionStatusChangedEventArgs : EventArgs
	{
		private Network _network;

		/// <summary>
		///	Initializes the instance with the specified <see cref="Network"/>
		/// </summary>
		/// <param name="network">The <see cref="Network"/> instance which status has changed.</param>
		public NetworkConnectionStatusChangedEventArgs(Network network)
		{
			_network = network;
		}

		/// <summary>
		/// Gets the <see cref="Network"/> instance reporting the status change.
		/// </summary>
		public Network Network
		{
			get { return _network; }
		}
	}
}