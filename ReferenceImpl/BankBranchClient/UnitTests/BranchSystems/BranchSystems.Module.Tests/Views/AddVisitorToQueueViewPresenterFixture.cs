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
using System;
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

namespace GlobalBank.BranchSystems.Module.Tests.Views
{
	[TestClass]
	public class AddVisitorToQueueViewPresenterFixture
	{
		private TestableRootWorkItem workItem;
		private MockCustomerQueueService queueService;
		private MockWorkspace mockWorkspace;
		MockAddVisitorToQueueView mockView;
		AddVisitorToQueueViewPresenter presenter;

		[TestInitialize]
		public void Initialize()
		{
			workItem = new TestableRootWorkItem();
			workItem.Services.AddNew<WorkspaceLocatorService, IWorkspaceLocatorService>();
			mockWorkspace = workItem.Workspaces.AddNew<MockWorkspace>(WorkspaceNames.ModalWindows);
			queueService = workItem.Services.AddNew<MockCustomerQueueService, ICustomerQueueService>();
			mockView = workItem.SmartParts.AddNew<MockAddVisitorToQueueView>();
			presenter = workItem.Items.AddNew<AddVisitorToQueueViewPresenter>();
			mockWorkspace.Show(mockView);
			presenter.View = mockView;
		}

		[TestMethod]
		public void QueueForServiceCallWebServiceWithReasonAndDescription()
		{
			Customer customer = workItem.Items.AddNew<Customer>("CustomerToAdd");
			string reason = "reason";
			string description = "description";

			presenter.QueueForService(reason, description);

			Assert.AreEqual(1, queueService.Queue.Count);
			Assert.AreSame(customer, queueService.Queue[0].Person);
			Assert.AreEqual(reason, queueService.Queue[0].ReasonCode);
			Assert.AreEqual(description, queueService.Queue[0].Description);
		}

		[TestMethod]
		public void PresenterSetsWorkingModeToAddCustomerWhenAddingCustomer()
		{
			Customer customer = workItem.Items.AddNew<Customer>("CustomerToAdd");

			Assert.AreEqual(AddVisitorToQueueWorkingMode.AddCustomer, presenter.WorkingMode);
		}

		[TestMethod]
		public void PresenterSetsWorkingModeToAddVisitorWhenAddingWalkin()
		{
			Assert.AreEqual(AddVisitorToQueueWorkingMode.AddVisitor, presenter.WorkingMode);
		}

		[TestMethod]
		public void PresenterQueuesCustomerWhenWorkingModeIsCustomer()
		{
			Customer customer = workItem.Items.AddNew<Customer>("CustomerToAdd");

			presenter.QueueForService("reason", "description");

			Assert.AreEqual(1, queueService.Queue.Count);
			Assert.IsNotNull(queueService.Queue[0].Person);
			Assert.AreSame(customer, queueService.Queue[0].Person);
		}

		[TestMethod]
		public void PresenterQueuesWalkinWhenWorkingModeIsVisitor()
		{
			presenter.QueueForService("reason", "description");

			Assert.AreEqual(1, queueService.Queue.Count);
			Assert.IsNotNull(queueService.Queue[0].Person);
		}

		class MockAddVisitorToQueueView : IAddVisitorToQueueView
		{
			public WalkIn Walkin = null;
			public Customer Customer = null;

			#region IAddVisitorToQueueView Members

			public void ShowWalkin(WalkIn walkIn)
			{
				Walkin = walkIn;
			}

			public void ShowCustomer(Customer customer)
			{
				Customer = customer;
			}

			#endregion

			#region IAddVisitorToQueueView Members

			public void NotifyCustomerAlreadyInQueue()
			{
				throw new Exception("The method or operation is not implemented.");
			}

			#endregion
		}
	}
}