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
using System.Configuration;

namespace Microsoft.Practices.SmartClient.ConnectionMonitor.Configuration
{
	/// <summary>
	/// Configuration section used by the <see cref="ConnectionMonitorFactory"/>.
	/// </summary>
	public class ConnectionSettingsSection : ConfigurationSection
	{
		/// <summary>
		/// Get the connection Items.
		/// </summary>
		[ConfigurationProperty("Connections")]
		public ConnectionItemElementCollection Connections
		{
			get { return (ConnectionItemElementCollection) (this["Connections"]); }
		}

		/// <summary>
		/// Gets or sets he configuration for the <see cref="NetworkCollection"/>.
		/// </summary>
		[ConfigurationProperty("Networks")]
		public NetworkCollectionElement Networks
		{
			get { return (NetworkCollectionElement) this["Networks"]; }
		}
	}
}