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
namespace Microsoft.Practices.SmartClient.EndpointCatalog
{
	/// <summary>
	/// Interface for the EndpointCatalog factory.
	/// </summary>
	public interface IEndpointCatalogFactory
	{
		/// <summary>
		///		This method must be implemented in order to create an EndpointCatalog.
		/// </summary>
		/// <returns>
		///		The EndpointCatalog created by the factory.
		/// </returns>
		IEndpointCatalog CreateCatalog();
	}
}