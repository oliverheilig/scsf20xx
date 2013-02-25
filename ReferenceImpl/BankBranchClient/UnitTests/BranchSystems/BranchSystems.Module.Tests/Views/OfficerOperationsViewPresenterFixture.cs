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
using System.Security.Principal;
using System.Threading;
using GlobalBank.BranchSystems.Interface.Constants;
using GlobalBank.BranchSystems.Interface.Services;
using GlobalBank.BranchSystems.Module.Tests.Mocks;
using GlobalBank.BranchSystems.Module.Views;
using GlobalBank.Infrastructure.Interface.BusinessEntities;
using GlobalBank.UnitTest.Library;
using GlobalBank.UnitTest.Library.MockObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GlobalBank.BranchSystems.Module.Tests.Views
{
	[TestClass]
	public class OfficerOperationsViewPresenterFixture
	{
		private TestableRootWorkItem workItem;
		private OfficerPanelViewPresenter presenter;
		private MockCustomerQueueService queueService;
		private MockOfficerOperationsView view;
		private MockWorkspace workspace;
		private MockCurrentQueueEntryService currentEntryService;

		[TestInitialize]
		public void Initialize()
		{
			workItem = new TestableRootWorkItem();
			queueService = workItem.Services.AddNew<MockCustomerQueueService, ICustomerQueueService>();
			workItem.Services.AddNew<MockCustomerFinderService, ICustomerFinderService>();
			currentEntryService = workItem.Services.AddNew<MockCurrentQueueEntryService, ICurrentQueueEntryService>();
			workspace = workItem.Workspaces.AddNew<MockWorkspace>(WorkspaceNames.BranchSystemsWorkspace);
			presenter = workItem.Items.AddNew<OfficerPanelViewPresenter>();
			view = workItem.Items.AddNew<MockOfficerOperationsView>();
			presenter.View = view;
		}

		[TestMethod]
		public void PresenterGetsOfficerQueueEntries()
		{
			GenericIdentity identity = new GenericIdentity("Tom");
			GenericPrincipal principal = new GenericPrincipal(identity, null);
			Thread.CurrentPrincipal = principal;

			presenter.UpdateQueue();

			Assert.AreEqual(1, view.Entries.Length);
			Assert.AreSame(queueService.Queue[0], view.Entries[0]);
			Assert.AreSame(queueService.Queue[0].Description, identity.Name);
		}
	}

	class MockOfficerOperationsView : IOfficerPanelView
	{
		public QueueEntry[] Entries = null;
		public QueueEntry Selected = null;

		public void ShowQueue(QueueEntry[] entries)
		{
			Entries = entries;
		}


		public void SelectServicedCustomer(QueueEntry customer)
		{
		}
	}

	class MockCurrentQueueEntryService : ICurrentQueueEntryService
	{
		public QueueEntry Entry;

		public QueueEntry CurrentEntry
		{
			get { return Entry; }
			set { Entry = value; }
		}
	}
}