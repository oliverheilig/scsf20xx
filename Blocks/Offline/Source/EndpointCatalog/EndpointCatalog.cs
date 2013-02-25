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
using System.Net;
using Microsoft.Practices.SmartClient.EndpointCatalog.Properties;

namespace Microsoft.Practices.SmartClient.EndpointCatalog
{
	/// <summary>
	/// Provides features to store and manage the endpoints and credentials used by other applications 
	/// running on the mobile device.
	/// Implements an IEndpointCatalog interface.
	/// </summary>
	public class EndpointCatalog : IEndpointCatalog
	{
		[NonSerialized] private Dictionary<string, Endpoint> endpoints = new Dictionary<string, Endpoint>();

		/// <summary>
		/// Returns the number of endpoints in the catalog.
		/// </summary>
		public int Count
		{
			get { return endpoints.Count; }
		}

		/// <summary>
		/// Checks if the network <paramref name="networkName"/> is defined for the endpoint 
		/// <paramref name="endpointName"/>. 
		/// </summary>
		/// <param name="endpointName">The name of the endpoint.</param>
		/// <param name="networkName">The name of the network.</param>
		/// <returns>If there is a default network defined for the endpoint, the method will return true.</returns>
		public bool AddressExistsForEndpoint(string endpointName, string networkName)
		{
			if (endpoints.ContainsKey(endpointName))
			{
				return endpoints[endpointName].ContainsConfigForNetwork(networkName);
			}
			return false;
		}

		/// <summary>
		/// Retrives the credential for a network in a given endpoint. 
		/// </summary>
		/// <param name="endpointName">The name of the endpoint.</param>
		/// <param name="networkName">The name of the network. This parameter can be null.</param>
		/// <returns>If the <paramref name="networkName"/> parameter is null or doesn't exist, the method returns 
		/// the default credential defined for the endpoint. If there isn't a default one, throws an exception.
		/// </returns>
		public NetworkCredential GetCredentialForEndpoint(string endpointName, string networkName)
		{
			if (endpoints.ContainsKey(endpointName))
			{
				return endpoints[endpointName].GetCredentialForNetwork(networkName);
			}
			else
				throw new KeyNotFoundException(Resources.ExceptionEndpointNotInCatalog);
		}

		/// <summary>
		/// Retrives the address for a network in a given endpoint. 
		/// </summary>
		/// <param name="endpointName">The name of the endpoint.</param>
		/// <param name="networkName">The name of the network. This parameter can be null.</param>
		/// <returns>If the <paramref name="networkName"/> parameter is null or doesn't exist, the method returns 
		/// the address defined for the endpoint. If there isn't a default one, throws an exception.
		/// </returns>
		public string GetAddressForEndpoint(string endpointName, string networkName)
		{
			if (endpoints.ContainsKey(endpointName))
			{
				return endpoints[endpointName].GetAddressForNetwork(networkName);
			}
			else
				throw new KeyNotFoundException(Resources.ExceptionEndpointNotInCatalog);
		}

		/// <summary>
		/// Add a new endpoint to the catalog. If the endpoint name already exist, then is removed an replaced with
		/// the new endpoint.
		/// </summary>
		/// <param name="endpoint">The new endpoint for the catalog.</param>
		public void SetEndpoint(Endpoint endpoint)
		{
			Guard.ArgumentNotNull(endpoint, "endpoint");

			if (String.IsNullOrEmpty(endpoint.Name))
				throw new ArgumentException("endpoint.Name");

			if (endpoints.ContainsKey(endpoint.Name))
				endpoints.Remove(endpoint.Name);

			endpoints.Add(endpoint.Name, endpoint);
		}

		/// <summary>
		/// Checks if an endpoint is defined in the catalog.
		/// </summary>
		/// <param name="endpointName">The name of the endpoint to look for.</param>
		/// <returns>Returns true if the endpoint name is defined in the catalog, false otherwise.</returns>
		public bool EndpointExists(string endpointName)
		{
			if (endpointName != null)
				return endpoints.ContainsKey(endpointName);
			else
				return false;
		}
	}
}