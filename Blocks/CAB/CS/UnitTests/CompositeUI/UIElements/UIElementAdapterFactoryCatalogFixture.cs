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
//===============================================================================
// Microsoft patterns & practices
// CompositeUI Application Block
//===============================================================================
// Copyright � Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI.UIElements;

namespace Microsoft.Practices.CompositeUI.Tests.UIElements
{
	[TestClass]
	public class UIElementAdapterFactoryCatalogFixture
	{
		[TestMethod]
		public void CanRegisterFactory()
		{
			MockFactory factory = new MockFactory();
			UIElementAdapterFactoryCatalog catalog = new UIElementAdapterFactoryCatalog();
			catalog.RegisterFactory(factory);

			Assert.IsTrue(catalog.Factories.Contains(factory));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void RegisteringNullFactoryThrows()
		{
			UIElementAdapterFactoryCatalog catalog = new UIElementAdapterFactoryCatalog();
			catalog.RegisterFactory(null);
		}

		[TestMethod]
		public void CanGetRegisteredFactory()
		{
			UIElementAdapterFactoryCatalog catalog = new UIElementAdapterFactoryCatalog();
			MockFactory factory = new MockFactory();
			catalog.RegisterFactory(factory);

			IUIElementAdapterFactory uiFactory = catalog.GetFactory("Foo");

			Assert.AreSame(factory, uiFactory);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void GetFactoryWhenNoAppropriateFactoryRegisteredThrows()
		{
			UIElementAdapterFactoryCatalog catalog = new UIElementAdapterFactoryCatalog();
			catalog.GetFactory(typeof(object));
		}

		class MockFactory : IUIElementAdapterFactory
		{
			public IUIElementAdapter GetAdapter(object managedExtension)
			{
				return new MockManager();
			}

			public bool Supports(object uiElement)
			{
				return (uiElement is string);
			}
		}

		class MockManager : UIElementAdapter<string>
		{
			public bool AddCalled = false;
			public List<string> Strings = new List<string>();

			protected override string Add(string uiElement)
			{
				AddCalled = true;
				Strings.Add(uiElement);

				return uiElement;
			}

			protected override void Remove(string uiElement)
			{
				Strings.Remove(uiElement);
			}

		}
	}
}
