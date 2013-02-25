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
	/// Helper class to get the configuration NetworkElement collection.
	/// </summary>
	public class NetworkElementCollection : ConfigurationElementCollection
	{
		/// <summary>
		/// Gets the NetworkElement for the given name.
		/// </summary>
		/// <param name="name">Name of the NetworkElement.</param>
		/// <returns>Matching NetworkElement.</returns>
		public NetworkElement GetNetwork(string name)
		{
			return (NetworkElement) BaseGet(name);
		}

		/// <summary>
		/// Creates a Configuration Element
		/// </summary>
		/// <returns>A new NetworkElement object</returns>
		protected override ConfigurationElement CreateNewElement()
		{
			return new NetworkElement();
		}

		/// <summary>
		/// Gets the Key for a particular element in the collection
		/// </summary>
		/// <param name="element">The element to get the key of.</param>
		/// <returns>An object representing the element's key.</returns>
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((NetworkElement) element).Name;
		}
	}
}