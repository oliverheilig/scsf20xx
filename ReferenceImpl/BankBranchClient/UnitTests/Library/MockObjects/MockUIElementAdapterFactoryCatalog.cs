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
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI.UIElements;

namespace GlobalBank.UnitTest.Library.MockObjects
{
	public class MockUIElementAdapterFactoryCatalog : IUIElementAdapterFactoryCatalog
	{
		private List<IUIElementAdapterFactory> _factories = new List<IUIElementAdapterFactory>();

		public IList<IUIElementAdapterFactory> Factories
		{
			get { return _factories.AsReadOnly(); }
		}

		public IUIElementAdapterFactory GetFactory(object element)
		{
			if (_factories.Count > 0)
				return _factories[0];
			return null;
		}

		public void RegisterFactory(IUIElementAdapterFactory factory)
		{
			_factories.Add(factory);
		}
	}
}
