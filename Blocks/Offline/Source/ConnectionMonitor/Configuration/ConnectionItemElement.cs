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
//--using Microsoft.Practices.SmartClient.Configuration;

namespace Microsoft.Practices.SmartClient.ConnectionMonitor.Configuration
{
	/// <summary>
	/// Configuration data for a connection
	/// </summary>
	public class ConnectionItemElement : ConfigurationElement
	{
		/// <summary>
		/// Gets or sets the connection type.
		/// </summary>
		[ConfigurationProperty("Type", IsRequired = true)]
		public string Type
		{
			get { return (string) this["Type"]; }
			set { this["Type"] = value; }
		}

		/// <summary>
		/// Numeric value with the connection price.
		/// </summary>
		[ConfigurationProperty("Price", IsRequired = true)]
		public int Price
		{
			get { return (int) this["Price"]; }
			set { this["Price"] = value; }
		}
	}
}