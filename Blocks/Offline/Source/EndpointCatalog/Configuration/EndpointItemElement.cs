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
	/// Helper class to get an endpoint from the configuration.
	/// </summary>
	public class EndpointItemElement : ConfigurationElement
	{
		/// <summary>
		/// Endpoint name for the configuration.
		/// </summary>
		[ConfigurationProperty("Name", IsKey = true, IsRequired = true)]
		public string Name
		{
			get { return GetNullIfMissing((string) this["Name"]); }
			set { this["Name"] = value; }
		}

		/// <summary>
		/// Default Address for the endpoint.
		/// </summary>
		[ConfigurationProperty("Address", IsKey = false, IsRequired = false)]
		public string Address
		{
			get { return GetNullIfMissing((string) this["Address"]); }
			set { this["Address"] = value; }
		}

		/// <summary>
		/// Default user name for the endpoint.
		/// </summary>
		[ConfigurationProperty("UserName", IsKey = false, IsRequired = false)]
		public string UserName
		{
			get { return GetNullIfMissing((string) this["UserName"]); }
			set { this["UserName"] = value; }
		}

		/// <summary>
		/// Default password for the endpoint.
		/// </summary>
		[ConfigurationProperty("Password", IsKey = false, IsRequired = false)]
		public string Password
		{
			get { return GetNullIfMissing((string) this["Password"]); }
			set { this["Password"] = value; }
		}

		/// <summary>
		/// Default domain for the endpoint.
		/// </summary>
		[ConfigurationProperty("Domain")]
		public string Domain
		{
			get { return GetNullIfMissing((string) base["Domain"]); }
			set { this["Domain"] = value; }
		}

		/// <summary>
		/// Networks section with the configuration overrides for specific networks.
		/// </summary>
		[ConfigurationProperty("NetworkItems", IsRequired = false)]
		public NetworkElementCollection Networks
		{
			get { return (NetworkElementCollection) this["NetworkItems"]; }
			//set { this["NetworkItems"] = value; }
		}

		private static string GetNullIfMissing(string value)
		{
			return String.IsNullOrEmpty(value) ? null : value;
		}
	}
}