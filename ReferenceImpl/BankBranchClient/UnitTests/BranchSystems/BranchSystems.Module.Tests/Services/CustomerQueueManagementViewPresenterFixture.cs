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
using GlobalBank.BranchSystems.Interface.Constants;
using GlobalBank.BranchSystems.Interface.Services;
using GlobalBank.BranchSystems.Module.Tests.Mocks;
using GlobalBank.BranchSystems.Module.Views;
using GlobalBank.Infrastructure.Interface.BusinessEntities;
using GlobalBank.Infrastructure.Interface.Services;
using GlobalBank.UnitTest.Library;
using GlobalBank.UnitTest.Library.MockObjects;
using Microsoft.Practices.SmartClient.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GlobalBank.BranchSystems.Module.Tests.Services
{
	[TestClass]
	public class CustomerQueueManagementViewPresenterFixture
	{
		TestableRootWorkItem workItem;
		MockCustomerFinderService finderService;
		MockCustomerQueueService queueService;
		MockWorkspace workspace;
		Mocks.MockCurrentQueueEntryService currentEntryService;

		[TestInitialize]
		public void Initialize()
		{
			workItem = new TestableRootWorkItem();
			workItem.Services.AddNew<WorkspaceLocatorService, IWorkspaceLocatorService>();
			finderService = workItem.Services.AddNew<MockCustomerFinderService, ICustomerFinderService>();
			queueService = workItem.Services.AddNew<MockCustomerQueueService, ICustomerQueueService>();
			currentEntryService = workItem.Services.AddNew<Mocks.MockCurrentQueueEntryService, ICurrentQueueEntryService>();
			workspace = workItem.Workspaces.AddNew<MockWorkspace>(WorkspaceNames.ModalWindows);
		}

		[TestMethod]
		public void PresenterCallsServiceToGetQueueUpdates()
		{
			queueService.AddCustomer(new Customer(), null, null);

			MockCustomerQueueManagementView view = new MockCustomerQueueManagementView();
			CustomerQueueManagementViewPresenter presenter = workItem.Items.AddNew<CustomerQueueManagementViewPresenter>();

			presenter.View = view;
			presenter.UpdateCustomerQueue();

			Assert.IsTrue(queueService.GetEntriesCalled);
			Assert.AreEqual(1, view.Entries.Length);
			Assert.AreSame(queueService.Queue[0], view.Entries[0]);
		}
	}

	class MockCustomerQueueManagementView : ICustomerQueueManagementView
	{
		public QueueEntry[] Entries = null;

		public void UpdateCustomerQueue(QueueEntry[] entries)
		{
			Entries = entries;
		}
	}
}