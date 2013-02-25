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
using System.Collections.Specialized;
using System.IO.IsolatedStorage;
using Microsoft.Practices.CompositeUI.Services;

namespace Microsoft.Practices.CompositeUI.Tests.Services
{
	[TestClass]
	public class IsolatedStorageStatePersistenceServiceFixture
	{
		static IsolatedStorageStatePersistenceService service;

		[TestInitialize]
		public void SetUp()
		{
			service = new IsolatedStorageStatePersistenceService();
		}

		[TestCleanup]
		public void TearDown()
		{
			service.Clear();
		}

		[TestMethod]
		public void DoesNotFindFile()
		{
			bool contains = service.Contains(Guid.NewGuid().ToString());
		}

		[TestMethod]
		public void CanSaveState()
		{
			State s = new State();

			service.Save(s);
		}

		[TestMethod]
		public void CanSaveStateAndVerify()
		{
			string id = "DummyID";
			State s = new State(id);

			service.Save(s);

			Assert.IsTrue(service.Contains(id));
		}

		[TestMethod]
		public void CanSaveAndLoadState()
		{
			string id = "DummyID";
			State s = new State(id);
			s["key"] = "value";

			service.Save(s);
			State loaded = service.Load(id);

			Assert.AreEqual("value", loaded["key"]);
		}

		[TestMethod]
		[ExpectedException(typeof (StatePersistenceException))]
		public void LoadNonExistingThrows()
		{
			service.Load(Guid.NewGuid().ToString());
		}

		[TestMethod]
		public void RemoveNonExistingNoOp()
		{
			service.Remove(Guid.NewGuid().ToString());
		}

		[TestMethod]
		public void CanOpenArbitraryStore()
		{
			Assert.IsFalse(new IsolatedStorageStatePersistenceService(
			               	IsolatedStorageScope.Assembly | IsolatedStorageScope.User).Contains(
									Guid.NewGuid().ToString()));
		}

		[TestMethod]
		public void CanConfigureScope()
		{
			NameValueCollection settings = new NameValueCollection();
			settings.Add(IsolatedStorageStatePersistenceService.ScopeAttribute, "Assembly| Roaming |   User");

			service.Configure(settings);

			Assert.AreEqual(IsolatedStorageScope.Assembly | IsolatedStorageScope.User | IsolatedStorageScope.Roaming,
			                service.Scope);
		}
	}
}
