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
using Microsoft.Practices.CompositeUI.Configuration;

namespace Microsoft.Practices.CompositeUI.Tests.ConfigurationModel
{
	[TestClass]
	public class ServiceCollectionFixture
	{
		private ServiceElementCollection collection;

		[TestInitialize]
		public void Setup()
		{
			collection = new ServiceElementCollection();
		}

		[TestMethod]
		public void CollectionIsEmpty()
		{
			Assert.AreEqual(0, collection.Count);
		}

		[TestMethod]
		public void CanAddService()
		{
			ServiceElement se = new ServiceElement();
			se.ServiceType = typeof (object);
			collection.Add(se);

			Assert.IsNotNull(collection[typeof (object)]);
			Assert.AreEqual(1, collection.Count);
		}

		[TestMethod]
		public void CanAddUsingIndexer()
		{
			ServiceElement se = new ServiceElement();
			se.ServiceType = typeof (object);
			collection[se.ServiceType] = se;

			Assert.IsNotNull(collection[typeof (object)]);
			Assert.AreEqual(1, collection.Count);
		}

		[TestMethod]
		public void CanRemoveElement()
		{
			ServiceElement se = new ServiceElement();
			se.ServiceType = typeof (object);
			collection[se.ServiceType] = se;
			collection.Remove(typeof (object));

			Assert.AreEqual(0, collection.Count);
			Assert.IsNull(collection[typeof (object)]);
		}

		[TestMethod]
		public void AddingServiceOfSameTimeReplacesOldOne()
		{
			ServiceElement se1 = new ServiceElement();
			ServiceElement se2 = new ServiceElement();
			se1.ServiceType = typeof (object);
			se2.ServiceType = typeof (object);

			collection[typeof (object)] = se1;
			collection[typeof (object)] = se2;

			Assert.AreSame(se2, collection[typeof (object)]);
		}

	}
}
