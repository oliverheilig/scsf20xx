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
	/// Represents a collection of connection configuration elements.
	/// </summary>
	public class ConnectionItemElementCollection : ConfigurationElementCollection
	{
		/// <summary>
		/// Creates a new <see cref="ConnectionItemElement"/>
		/// </summary>
		protected override ConfigurationElement CreateNewElement()
		{
			return new ConnectionItemElement();
		}

		/// <summary>
		/// Gets the element key for a specified configuration element.
		/// </summary>
		/// <param name="element">The <see cref="ConfigurationElement"/> to get the key for.</param>
		/// <returns>The key for the <see cref="ConfigurationElement"/>.</returns>
		protected override object GetElementKey(ConfigurationElement element)
		{
			ConnectionItemElement e = (ConnectionItemElement) element;

			return e.Type;
		}

		/// <summary>
		/// Gets the <see cref="ConnectionItemElement"/> at the specified index./
		/// </summary>
		/// <param name="index">The element index in the collection.</param>
		/// <returns>The <see cref="ConnectionItemElement"/> found at that index.</returns>
		public ConnectionItemElement this[int index]
		{
			get { return (ConnectionItemElement) BaseGet(index); }
		}

		/// <summary>
		/// Gets the <see cref="ConnectionItemElement"/> indexed by the key.
		/// </summary>
		/// <param name="key">A <see cref="string"/> with the key value.</param>
		/// <returns>A reference to a <see cref="ConnectionItemElement"/>.</returns>
		public new ConnectionItemElement this[string key]
		{
			get { return (ConnectionItemElement) BaseGet(key); }
		}
	}
}