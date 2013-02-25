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
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using Microsoft.Practices.SmartClient.EndpointCatalog.Properties;

namespace Microsoft.Practices.SmartClient.EndpointCatalog
{
	/// <summary>
	/// This class stores the networks address and credential for an endpoint defined in the 
	/// <see cref="EndpointCatalog"/>. You can also define the default credential for the endpoint. 
	/// </summary>
	public class Endpoint
	{
		[NonSerialized] private EndpointConfig defaultConfig;
		[NonSerialized] private string name;

		[NonSerialized] private Dictionary<string, EndpointConfig> configurationCatalog =
			new Dictionary<string, EndpointConfig>();

		/// <summary>
		/// Sets/Gets the default credential for an endpoint.
		/// </summary>
		public EndpointConfig Default
		{
			get { return defaultConfig; }
			set { defaultConfig = value; }
		}

		/// <summary>
		/// Sets/Gets the name for an endpoint. 
		/// </summary>
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		/// <summary>
		/// Returns a list of all the network names defined in an endpoint. 
		/// Helper function for use in the unit tests.
		/// </summary>
		public List<string> NetworkNames
		{
			get
			{
				List<string> result = new List<string>();

				foreach (string key in configurationCatalog.Keys)
				{
					result.Add(key);
				}
				return result;
			}
		}

		/// <summary>
		/// Constructor which creates an Endpoint with the given name.
		/// </summary>
		/// <param name="name">Name for the endpoint.</param>
		public Endpoint(string name)
		{
			this.name = name;
		}

		/// <summary>
		/// Checks if a network exists in the endpoint. 
		/// </summary>
		/// <param name="networkName">Network name to check.</param>
		/// <returns>If a default credential exists for the endpoint, returns true. If not, checks in the 
		/// network's dictionary to see if it's defined or not.
		/// </returns>
		public bool ContainsConfigForNetwork(string networkName)
		{
			if (Default == null)
			{
				if (networkName != null)
					return configurationCatalog.ContainsKey(networkName);
				else
					return false;
			}
			else
				return true;
		}

		/// <summary>
		/// Gets the credential for a given network. If the <paramref name="networkName"/> parameter is null,
		/// then returns the default credential of the endpoint. 
		/// </summary>
		/// <param name="networkName">Network name to look for.</param>
		/// <returns>The network credential of the <paramref name="networkName"/> network.</returns>
		public NetworkCredential GetCredentialForNetwork(string networkName)
		{
			if (networkName == null)
			{
				if (Default != null)
					return Default.Credential;
				else
					throw new KeyNotFoundException(String.Format(CultureInfo.CurrentCulture,
					                                             Resources.ExceptionNonDefault, name));
			}

			Guard.ArgumentNotNullOrEmptyString(networkName, "networkName");

			if (configurationCatalog.ContainsKey(networkName))
				return configurationCatalog[networkName].Credential;
			else
			{
				if (Default == null)
					throw new KeyNotFoundException(String.Format(CultureInfo.CurrentCulture,
					                                             Resources.ExceptionNetworkNotInEndpoint, networkName, name));
				return Default.Credential;
			}
		}

		/// <summary>
		/// Gets the address for a network. If the address is not found, it returns the default address (if it exists).
		/// </summary>
		/// <param name="networkName">Network name.</param>
		/// <returns>The address for the network name or the default address if the network is not found.</returns>
		public string GetAddressForNetwork(string networkName)
		{
			Guard.StringArgumentNotEmptyString(networkName, "networkName");

			if (HasNetworkNameInConfiguration(networkName))
			{
				return configurationCatalog[networkName].Address;
			}
			else
			{
				return ReturnDefaultAddress(networkName);
			}
		}

		private bool HasNetworkNameInConfiguration(string networkName)
		{
			return networkName != null && configurationCatalog.ContainsKey(networkName);
		}

		private string ReturnDefaultAddress(string networkName)
		{
			if (Default == null)
			{
				throw new KeyNotFoundException(String.Format(CultureInfo.CurrentCulture,
				                                             Resources.ExceptionNetworkNotInEndpoint, networkName, name));
			}
			return Default.Address;
		}

		/// <summary>
		/// Sets the endpoint configuration (address and credential) for a network.
		/// </summary>
		/// <param name="networkName">String with the network name.</param>
		/// <param name="endpointConfiguration">EndpointConfig with the address and credential for the network.</param>
		public void SetConfiguration(string networkName, EndpointConfig endpointConfiguration)
		{
			Guard.ArgumentNotNull(endpointConfiguration, "endpointConfiguration");

			if (String.IsNullOrEmpty(networkName))
				throw new ArgumentException("networkName");

			if (String.IsNullOrEmpty(endpointConfiguration.Address))
				throw new ArgumentException("endpointConfiguration.Address");

			if (configurationCatalog.ContainsKey(networkName))
				configurationCatalog.Remove(networkName);

			configurationCatalog.Add(networkName, endpointConfiguration);
		}
	}
}