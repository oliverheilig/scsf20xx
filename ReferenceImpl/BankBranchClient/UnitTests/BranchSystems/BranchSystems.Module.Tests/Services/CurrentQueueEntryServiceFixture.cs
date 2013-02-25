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
using GlobalBank.BranchSystems.Module.Services;
using GlobalBank.Infrastructure.Interface.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GlobalBank.BranchSystems.Module.Tests.Services
{
	[TestClass]
	public class CurrentQueueEntryServiceFixture
	{
		CurrentQueueEntryService service;
		QueueEntry entry;

		[TestInitialize]
		public void Initialize()
		{
			service = new CurrentQueueEntryService();
			entry = new QueueEntry();
		}

		[TestMethod]
		public void AssignCurrentCustomer()
		{
			service.CurrentEntry = entry;

			Assert.AreEqual(entry, service.CurrentEntry);
		}
	}
}
