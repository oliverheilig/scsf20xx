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
	/// Configuration data for a network entry.
	/// </summary>
	public class NetworkElement : ConfigurationElement
	{
		/// <summary>
		/// Gets or sets the network address.
		/// </summary>
		[ConfigurationProperty("Address", IsRequired = true)]
		public string Address
		{
			get { return (string) this["Address"]; }
			set { this["Address"] = value; }
		}

		/// <summary>
		/// Gets or sets the network logical name.
		/// </summary>
		[ConfigurationProperty("Name", IsKey = true, IsRequired = true)]
		public string Name
		{
			get { return (string) this["Name"]; }
			set { this["Name"] = value; }
		}
	}
}