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

namespace Microsoft.Practices.SmartClient.DisconnectedAgent
{
	/// <summary>
	/// Defines the data for the ConnectionStatusChanged event.
	/// </summary>
	public class ConnectionStatusChangedEventArgs : EventArgs
	{
		private IConnectionMonitor connection;

		/// <summary>
		/// Creates a ConnectionStatusChangedEventArgs object.
		/// </summary>
		/// <param name="connection"></param>
		public ConnectionStatusChangedEventArgs(IConnectionMonitor connection)
		{
			this.connection = connection;
		}

		/// <summary>
		/// The Connection which had its status change.
		/// </summary>
		public IConnectionMonitor Connection
		{
			get { return connection; }
		}
	}
}