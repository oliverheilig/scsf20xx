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

namespace Microsoft.Practices.SmartClient.ConnectionMonitor.Configuration
{
	/// <summary>
	/// Represents a collection of network configuration elements.
	/// </summary>
	public class NetworkCollectionElement : ConfigurationElementCollection
	{
		/// <summary>
		/// Creates a new <see cref="NetworkElement"/>.
		/// </summary>
		protected override ConfigurationElement CreateNewElement()
		{
			return new NetworkElement();
		}

		/// <summary>
		/// Gets the element key for a specified configuration element.
		/// </summary>
		/// <param name="element">The <see cref="ConfigurationElement"/> to get the key for.</param>
		/// <returns>The key for the <see cref="ConfigurationElement"/>.</returns>
		protected override object GetElementKey(ConfigurationElement element)
		{
			NetworkElement e = (NetworkElement) element;

			return e.Address;
		}

		/// <summary>
		/// Indexer for the collection.
		/// </summary>
		/// <param name="index">The element index in the collection.</param>
		/// <returns>The <see cref="NetworkElement"/> found at the specified index.</returns>
		public NetworkElement this[int index]
		{
			get { return (NetworkElement) BaseGet(index); }
		}

		/// <summary>
		/// Gets or sets the type name of the <see cref="INetworkStatusStrategy"/> implementation
		/// to use with the <see cref="NetworkCollection"/>.
		/// </summary>
		[ConfigurationProperty("StrategyType", IsRequired = false)]
		public string StrategyType
		{
			get { return (string) this["StrategyType"]; }
			set { this["StrategyType"] = value; }
		}
	}
}