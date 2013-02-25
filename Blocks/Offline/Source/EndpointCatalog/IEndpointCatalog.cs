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
using System.Net;

namespace Microsoft.Practices.SmartClient.EndpointCatalog
{
	/// <summary>
	/// An interface that objects can implement to shwo that they are a catalog containing a collection of Endpoints.
	/// </summary>
	public interface IEndpointCatalog
	{
		/// <summary>
		/// Must be implemented to get the endpoints quantity.
		/// </summary>
		int Count { get; }

		/// <summary>
		/// Must be implemented to verify the existance of endpoint information (address and credentials) 
		/// for a given endpoint in the given network name.
		/// </summary>
		/// <param name="endpointName">Name of the endpoint to be searched.</param>
		/// <param name="networkName">Network name.</param>
		/// <returns>true if the information has been found. Otherwise false.</returns>
		bool AddressExistsForEndpoint(string endpointName, string networkName);

		/// <summary>
		/// Must be implemented to provide the credentials for 
		/// a given endpoint in the given network name.
		/// </summary>
		/// <param name="endpointName">Name of the endpoint.</param>
		/// <param name="networkName">Network name.</param>
		/// <returns>NetworkCredential corresponding to the given parameters.</returns>
		NetworkCredential GetCredentialForEndpoint(string endpointName, string networkName);

		/// <summary>
		/// Must be implemented to provide the address for
		/// a given endpoint in the given network name.
		/// </summary>
		/// <param name="endpointName">Name of the endpoint.</param>
		/// <param name="networkName">Network name.</param>
		/// <returns>String containing the address for the given parameters.</returns>
		string GetAddressForEndpoint(string endpointName, string networkName);

		/// <summary>
		/// Must be implemented to add/update an endpoint in the Catalog.
		/// </summary>
		/// <param name="endpoint">Endpoint to be added/updated.</param>
		void SetEndpoint(Endpoint endpoint);

		/// <summary>
		/// Must be implemented to verify the existance of an endpoint in the catalog.
		/// </summary>
		/// <param name="endpointName">Name of the endpoint to be searched.</param>
		/// <returns>True if the endpoint exists in the catalog. Otherwise false.</returns>
		bool EndpointExists(string endpointName);
	}
}