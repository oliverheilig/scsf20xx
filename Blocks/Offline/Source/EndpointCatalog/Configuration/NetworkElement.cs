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
using System.Configuration;

namespace Microsoft.Practices.SmartClient.EndpointCatalog.Configuration
{
	/// <summary>
	/// Helper class 
	/// </summary>
	public class NetworkElement : ConfigurationElement
	{
		/// <summary>
		/// Name for the network.
		/// </summary>
		[ConfigurationProperty("Name", IsRequired = true, IsKey = true)]
		public string Name
		{
			get { return (string) this["Name"]; }
			set { this["Name"] = value; }
		}

		/// <summary>
		/// Address for the network.
		/// </summary>
		[ConfigurationProperty("Address", IsRequired = true)]
		public string Address
		{
			get { return (string) this["Address"]; }
			set { this["Address"] = value; }
		}

		/// <summary>
		/// UserName for the network.
		/// </summary>
		[ConfigurationProperty("UserName", IsRequired = false)]
		public string UserName
		{
			get { return GetNullIfMissing((string) this["UserName"]); }
			set { this["UserName"] = value; }
		}

		/// <summary>
		/// Password for the network.
		/// </summary>
		[ConfigurationProperty("Password", IsRequired = false)]
		public string Password
		{
			get { return GetNullIfMissing((string) this["Password"]); }
			set { this["Password"] = value; }
		}

		/// <summary>
		/// Domain for the network.
		/// </summary>
		[ConfigurationProperty("Domain", IsRequired = false)]
		public string Domain
		{
			get { return GetNullIfMissing((string) this["Domain"]); }
			set { this["Domain"] = value; }
		}

		private static string GetNullIfMissing(string value)
		{
			return String.IsNullOrEmpty(value) ? null : value;
		}
	}
}