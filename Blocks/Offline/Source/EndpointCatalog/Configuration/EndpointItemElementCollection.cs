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

namespace Microsoft.Practices.SmartClient.EndpointCatalog.Configuration
{
	/// <summary>
	/// Helper class to get the configuration element collection for endpoints.
	/// </summary>
	public class EndpointItemElementCollection : ConfigurationElementCollection
	{
		/// <summary>
		/// Gets the EndpointItemElement with the name.
		/// </summary>
		/// <param name="name">EndpointItemElement name to get.</param>
		/// <returns>Matching EndpointItemElement.</returns>
		public EndpointItemElement GetEndpoint(string name)
		{
			return (EndpointItemElement) (BaseGet(name));
		}

		/// <summary>
		/// Creates a Configuration Element
		/// </summary>
		/// <returns>A new EndpointItemElement</returns>
		protected override ConfigurationElement CreateNewElement()
		{
			return new EndpointItemElement();
		}

		/// <summary>
		/// Gets the Key for a particular element in the collection
		/// </summary>
		/// <param name="element">The element to get the key of.</param>
		/// <returns>An object representing the element's key.</returns>
		protected override object GetElementKey(ConfigurationElement element)
		{
			EndpointItemElement e = (EndpointItemElement) element;

			return e.Name;
		}
	}
}