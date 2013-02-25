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
using GlobalBank.BranchSystems.Interface.Services;
using GlobalBank.BranchSystems.Module.Tests.Mocks;
using GlobalBank.BranchSystems.Module.Views;
using GlobalBank.Infrastructure.Interface.BusinessEntities;
using GlobalBank.Infrastructure.Interface.Services;
using GlobalBank.UnitTest.Library;
using GlobalBank.UnitTest.Library.MockObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.SmartClient.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GlobalBank.BranchSystems.Module.Tests.Views
{
	[TestClass]
	public class CustomerSummaryViewPresenterFixture
	{
		private TestableRootWorkItem workItem;
		private MockCustomerFinderService finderService;
		private TestablePresenter presenter;
		private MockCustomerSummaryView view;
		private MockCustomerAccountService customerAccountService;
		private MockAlertService alertService;
		private MockQueueService queueService;
		private MockWorkspace workspace;
		private QueueEntry queueEntry;

		[TestInitialize]
		public void Initialize()
		{
			workItem = new TestableRootWorkItem();
			workItem.Services.AddNew<WorkspaceLocatorService, IWorkspaceLocatorService>();
			workspace = workItem.Workspaces.AddNew<MockWorkspace>();

			queueEntry = workItem.Items.AddNew<QueueEntry>("QueueEntry");
			queueEntry.Person = new Customer();
			finderService = workItem.Services.AddNew<MockCustomerFinderService, ICustomerFinderService>();
			queueService = workItem.Services.AddNew<MockQueueService, ICustomerQueueService>();
			customerAccountService = workItem.Services.AddNew<MockCustomerAccountService, ICustomerAccountService>();
			alertService = workItem.Services.AddNew<MockAlertService, ICustomerAlertService>();

			view = workItem.SmartParts.AddNew<MockCustomerSummaryView>();
			presenter = workItem.Items.AddNew<TestablePresenter>();
			presenter.View = view;
			workspace.Show(view);
		}

		[TestMethod]
		public void PresenterCallGetCustomerAccounts()
		{
			presenter.GetCustomerAccounts();
			Assert.IsTrue(customerAccountService.GetCustomerAccountsCalled);
		}

		[TestMethod]
		public void PresenterCallGetCustomerAlerts()
		{
			presenter.GetCustomerAlerts();
			Assert.IsTrue(alertService.GetCustomerAlertsCalled);
		}

		[TestMethod]
		public void PresenterCallGetCustomerCreditCards()
		{
			presenter.GetCustomerCreditCards();
			Assert.IsTrue(customerAccountService.GetCustomerCreditCardsCalled);
		}

		[TestMethod]
		public void PresenterDontAskForDataWhenThereIsNoCustomer()
		{
			queueEntry.Person = null;

			presenter.GetCustomerAccounts();
			presenter.GetCustomerAlerts();
			presenter.GetCustomerCreditCards();

			Assert.IsFalse(customerAccountService.GetCustomerAccountsCalled);
			Assert.IsFalse(alertService.GetCustomerAlertsCalled);
			Assert.IsFalse(customerAccountService.GetCustomerCreditCardsCalled);
		}

		[TestMethod]
		public void FinishCustomerService()
		{
			presenter.ServiceComplete();

			Assert.AreSame(queueService.RemovedQueueEntryID, queueEntry.QueueEntryID);
			Assert.AreSame(view, workspace.ClosedSmartPart);
		}


		[TestMethod]
		public void OnViewReady()
		{
			presenter.OnViewReady();

			Assert.AreSame(customerAccountService.Accounts, view.UpdatedAccounts);
			Assert.AreSame(customerAccountService.CreditCards, view.UpdatedCreditCards);
			Assert.AreSame(alertService.Alerts, view.UpdatedAlerts);
			Assert.AreSame(queueEntry.Person, view.UpdatedCustomer);
			Assert.AreSame(queueEntry, view.UpdatedQueueEntry);
		}

		class TestablePresenter : CustomerSummaryViewPresenter
		{
			[InjectionConstructor]
			public TestablePresenter
				(
				[ComponentDependency("QueueEntry")] QueueEntry queueEntry,
				[ServiceDependency] ICustomerAccountService accountService,
				[ServiceDependency] ICustomerAlertService alertService,
				[ServiceDependency] ICustomerQueueService queueService
				)
				: base(queueEntry, accountService, alertService, queueService)
			{
			}

			public new void GetCustomerAccounts()
			{
				base.GetCustomerAccounts();
			}

			public new void GetCustomerAlerts()
			{
				base.GetCustomerAlerts();
			}

			public new void GetCustomerCreditCards()
			{
				base.GetCustomerCreditCards();
			}
		}

		class MockCustomerSummaryView : ICustomerSummaryView
		{
			public Account[] UpdatedAccounts;
			public CreditCard[] UpdatedCreditCards;
			public Alert[] UpdatedAlerts;
			public Person UpdatedCustomer;
			public QueueEntry UpdatedQueueEntry;

			public void UpdateCustomerAccounts(Account[] accounts)
			{
				UpdatedAccounts = accounts;
			}

			public void UpdateCustomerAlerts(Alert[] alerts)
			{
				UpdatedAlerts = alerts;
			}

			public void UpdateCustomerCreditCards(CreditCard[] creditCards)
			{
				UpdatedCreditCards = creditCards;
			}


			public void UpdateCustomerInfo(Person visitor)
			{
				UpdatedCustomer = visitor;
			}

			public void UpdateQueueInfo(QueueEntry queueEntry)
			{
				UpdatedQueueEntry = queueEntry;
			}
		}
	}
}