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
	///	Event information associated to the event fired when a
	/// <see cref="Connection"/>  status has changed.
	/// </summary>
	public class StateChangedEventArgs : EventArgs
	{
		private bool isConnected;

		/// <summary>
		///	Initializes a new instance with the specified connectivity status.
		/// </summary>
		/// <param name="isConnected">The current connection status.</param>
		public StateChangedEventArgs(bool isConnected)
		{
			this.isConnected = isConnected;
		}

		/// <summary>
		/// Gets the connection status when the event has been fired.
		/// </summary>
		public bool IsConnected
		{
			get { return isConnected; }
		}
	}
}