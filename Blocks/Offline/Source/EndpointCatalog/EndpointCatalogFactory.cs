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
using Microsoft.Practices.SmartClient.EndpointCatalog.Configuration;
using Microsoft.Practices.SmartClient.EndpointCatalog.Properties;

namespace Microsoft.Practices.SmartClient.EndpointCatalog
{
	/// <summary>
	/// A factory that can create an EndpointCatalog based on configuration settings.
	/// </summary>
	public class EndpointCatalogFactory : IEndpointCatalogFactory
	{
		private EndpointSection endpointSection;

		/// <summary>
		/// Constructor for the factory with a section name.
		/// </summary>
		/// <param name="sectionName">Name for the section corresponding to the endpoint catalog in the configuration.</param>
		public EndpointCatalogFactory(string sectionName)
		{
			endpointSection = ConfigurationManager.GetSection(sectionName) as EndpointSection;
		}

		/// <summary>
		/// Creates a new EndpointCatalog using the section name in the configuration.
		/// </summary>
		/// <returns>
		///		EndpointCatalog created from the section in the configuration, 
		///		implementing the IEndpointCatalog interface.
		///	</returns>
		public IEndpointCatalog CreateCatalog()
		{
			EndpointCatalog catalog = null;
			if (endpointSection != null)
			{
				catalog = new EndpointCatalog();

				foreach (EndpointItemElement endpointItem in endpointSection.EndpointItemCollection)
				{
					Endpoint endpoint = new Endpoint(endpointItem.Name);
					catalog.SetEndpoint(endpoint);

					//if an Address exists for the endpoint, must be the default one.
					if (endpointItem.Address != null)
					{
						endpoint.Default = new EndpointConfig(endpointItem.Address);
                        if (endpointItem.UserName != null)
                            endpoint.Default.Credential.UserName = endpointItem.UserName;
                        if (endpointItem.Password != null)
                            endpoint.Default.Credential.Password = endpointItem.Password;
                        if (endpointItem.Domain != null)
                            endpoint.Default.Credential.Domain = endpointItem.Domain;
					}

					foreach (NetworkElement network in endpointItem.Networks)
					{
						EndpointConfig config = new EndpointConfig(network.Address);
						endpoint.SetConfiguration(network.Name, config);

						if (network.UserName != null)
							config.Credential.UserName = network.UserName;
						if (network.Password != null)
							config.Credential.Password = network.Password;
						if (network.Domain != null)
							config.Credential.Domain = network.Domain;
					}
				}
			}

			return catalog;
		}
	}
}