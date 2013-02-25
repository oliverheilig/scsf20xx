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
	/// Event data for the <see cref="ConnectionCollection.ConnectionAdded"/> 
	/// and the <see cref="ConnectionCollection.ConnectionRemoved"/> events.
	/// </summary>
	public class ConnectionEventArgs : EventArgs
	{
		private Connection _connection;

		/// <summary>
		/// Initializes a new instance with the specified <see cref="Connection"/>
		/// </summary>
		/// <param name="connection"></param>
		public ConnectionEventArgs(Connection connection)
		{
			_connection = connection;
		}

		/// <summary>
		/// The connection being added or removed from the <see cref="ConnectionCollection"/>
		/// </summary>
		public Connection Connection
		{
			get { return _connection; }
		}
	}
}