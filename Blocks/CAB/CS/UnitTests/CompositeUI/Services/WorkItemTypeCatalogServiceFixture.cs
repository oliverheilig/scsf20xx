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
using Microsoft.Practices.CompositeUI.Services;

namespace Microsoft.Practices.CompositeUI.Tests.Services
{
	[TestClass]
	public class WorkItemTypeCatalogServiceFixture
	{
		[TestMethod]
		public void CanRegisterWorkItem()
		{
			WorkItemTypeCatalogService svc = new WorkItemTypeCatalogService();

			svc.RegisterWorkItem<WorkItem>();

			Assert.AreEqual(1, svc.RegisteredWorkItemTypes.Count);
		}

		[TestMethod]
		public void CanCreateInstancesOfWorkItem()
		{
			bool created = false;
			WorkItem wi = new TestableRootWorkItem();
			WorkItemTypeCatalogService svc = wi.Services.AddNew<WorkItemTypeCatalogService, IWorkItemTypeCatalogService>();

			svc.RegisterWorkItem<WorkItem>();
			svc.CreateEachWorkItem<WorkItem>(wi, delegate(WorkItem item) { created = true; });

			Assert.IsTrue(created);
		}

		[TestMethod]
		public void CreatingEachWorkItemCheckAssigableRight()
		{
			bool created = false;
			WorkItem wi = new TestableRootWorkItem();
			WorkItemTypeCatalogService svc = wi.Services.AddNew<WorkItemTypeCatalogService, IWorkItemTypeCatalogService>();
			svc.RegisterWorkItem<MockWorkItem>();

			svc.CreateEachWorkItem<ITest>(wi, delegate(ITest item) { created = true; });

			Assert.IsTrue(created);
		}

		class MockWorkItem : WorkItem, ITest
		{
		}

		interface ITest
		{
		}

	}
}
