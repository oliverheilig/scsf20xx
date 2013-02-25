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
using System.Net;

namespace Microsoft.Practices.SmartClient.EndpointCatalog
{
	/// <summary>
	/// An object containing configuration information about an Endpoint
	/// </summary>
	public class EndpointConfig
	{
		[NonSerialized] private NetworkCredential credential;
		[NonSerialized] private string address;

		/// <summary>
		/// Creates an Endpoint configuration for the given address.
		/// </summary>
		/// <param name="address">The address to create a configuration for.</param>
		public EndpointConfig(string address)
			: this(address, null)
		{
		}

		/// <summary>
		/// Creates an Endpoint configuration for the given address.
		/// </summary>
		/// <param name="address">The address to create a configuration for.</param>
		/// <param name="credential">Credentials to use when connecting to the Endpoint</param>
		public EndpointConfig(string address, NetworkCredential credential)
		{
			Guard.ArgumentNotNull(address, "address");

			this.address = address;

			if (credential == null)
				this.credential = new NetworkCredential();
			else
				this.credential = credential;
		}

		/// <summary>
		/// Gets the address of the endpoint
		/// </summary>
		public string Address
		{
			get { return address; }
		}

		/// <summary>
		/// Gets or sets the credentials to use when connecting to the Endpoint.
		/// </summary>
		public NetworkCredential Credential
		{
			get { return credential; }
			set { credential = value; }
		}
	}
}