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
using System.Security.Authentication;
using System.Security.Principal;
using System.Threading;
using GlobalBank.BasicAccounts.Interface.Services;
using GlobalBank.BasicAccounts.Module.Tests.Mocks;
using GlobalBank.BranchSystems.Interface.Services;
using GlobalBank.Infrastructure.Interface.BusinessEntities;
using GlobalBank.Infrastructure.Interface.Services;
using GlobalBank.Infrastructure.Module.Services;
using GlobalBank.UnitTest.Library;
using GlobalBank.UnitTest.Library.MockObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Services;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.SmartClient.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GlobalBank.BasicAccounts.Module.Tests.Views
{
	[TestClass]
	public class PurchaseCDViewPresenterFixture
	{
		private TestableRootWorkItem workItem;
		private TestablePresenter presenter;
		private MockPurchaseCDView view;
		private MockCustomerFinderService finderService;
		private MockQuoteService quoteService;
		private MockCustomerAccountService customerAccountService;
		private MockAccountService accountService;
		private MockAuthenticationService authService;
		private GenericPrincipalImpersonationService impersonationService;
		private QueueEntry entry;
		private MockWorkspace workspace;

		[TestInitialize]
		public void Initialize()
		{
			Customer customer = new Customer();
			customer.CustomerId = "0001";
			workItem = new TestableRootWorkItem();
			workItem.Services.AddNew<WorkspaceLocatorService, IWorkspaceLocatorService>();
			workspace = workItem.Workspaces.AddNew<MockWorkspace>();
			entry = workItem.Items.AddNew<QueueEntry>("QueueEntry");
			entry.Person = customer;

			finderService = workItem.Services.AddNew<MockCustomerFinderService, ICustomerFinderService>();
			customerAccountService = workItem.Services.AddNew<MockCustomerAccountService, ICustomerAccountService>();
			accountService = workItem.Services.AddNew<MockAccountService, IAccountService>();
			quoteService = workItem.Services.AddNew<MockQuoteService, IQuoteService>();

			authService = workItem.Services.AddNew<MockAuthenticationService, IAuthenticationService>();
			impersonationService = workItem.Services.AddNew<GenericPrincipalImpersonationService, IImpersonationService>();

			presenter = workItem.Items.AddNew<TestablePresenter>();
			view = workItem.Items.AddNew<MockPurchaseCDView>();
			presenter.View = view;
			workspace.Show(view);
		}

		[TestMethod]
		public void GetRatesTable()
		{
			Rate[] result = presenter.GetRatesTable();
			Assert.AreSame(quoteService.Rates, result);
		}

		[TestMethod]
		public void GetAccounts()
		{
			CreateServiceAccounts();

			Account[] accounts = presenter.GetCustomerAccounts();

			Assert.AreEqual(2, accounts.Length);
			Assert.AreSame(customerAccountService.Accounts[0], accounts[0]);
			Assert.AreSame(customerAccountService.Accounts[2], accounts[1]);
		}

		[TestMethod]
		public void CalculateRateForGivenAmountAndDuration()
		{
			Assert.AreEqual(10M, presenter.GetInterestRate(500, 10));
			Assert.AreEqual(12M, presenter.GetInterestRate(1500, 10));
		}

		[TestMethod]
		public void PurchaseCDIsClearOnViewReady()
		{
			presenter.OnViewReady();

			Assert.IsTrue(view.Cleared);
		}

		[TestMethod]
		public void PurchaseCDShowAccountsOnViewReady()
		{
			presenter.OnViewReady();

			Assert.IsNotNull(view.Accounts);
		}

		[TestMethod]
		public void PurchaseCDShowRatesOnViewReady()
		{
			presenter.OnViewReady();

			Assert.IsNotNull(view.Rates);
		}

		[TestMethod]
		public void CalculateRatesShowRate()
		{
			decimal amount = 500;
			int duration = 10;
			presenter.CalculateInterestRate(amount, duration);

			Assert.IsTrue(view.Rate > 0M);
		}


		[TestMethod]
		public void PurchaseCD()
		{
			Account source = new Account();
			source.Balance = 10000;
			decimal amount = 1000;
			int duration = 10;
			presenter.PurchaseCD(source, amount, duration);

			Assert.IsNotNull(accountService.CDAccount);
			Assert.AreEqual(((Customer) entry.Person).CustomerId, accountService.CDAccount.CustomerId);
			Assert.AreEqual(Convert.ToSingle(amount), accountService.CDAccount.Balance);
		}


		[TestMethod]
		public void PurchaseCDWithNoEnoughBalance()
		{
			Account source = new Account();
			source.Balance = 500;
			decimal amount = 1000;
			int duration = 100;
			presenter.PurchaseCD(source, amount, duration);

			Assert.IsNotNull(view.Message);
		}

		[TestMethod]
		public void OfficerPurchaseNotShowsApproval()
		{
			GenericIdentity identity = new GenericIdentity("BranchManagerUser");
			GenericPrincipal principal = new GenericPrincipal(identity, new string[] {"BranchManager"});
			Thread.CurrentPrincipal = principal;

			Account source = new Account();
			source.Balance = 10500;
			decimal amount = 10001; // need BranchManager approval
			int duration = 10;
			presenter.PurchaseCD(source, amount, duration);

			Assert.IsFalse(view.Approval);
			Assert.IsNotNull(accountService.CDAccount);
			Assert.AreEqual(((Customer) entry.Person).CustomerId, accountService.CDAccount.CustomerId);
			Assert.AreEqual(Convert.ToSingle(amount), accountService.CDAccount.Balance);
		}

		[TestMethod]
		public void GreeterPurchaseCDGreaterThanTheAllowedLimit()
		{
			GenericIdentity identity = new GenericIdentity("GreeterUser");
			GenericPrincipal principal = new GenericPrincipal(identity, new string[] {"Greeter"});
			Thread.CurrentPrincipal = principal;

			Account source = new Account();
			source.Balance = 10500;
			decimal amount = 10001; // need officer approval
			int duration = 100;
			presenter.PurchaseCD(source, amount, duration);

			Assert.IsTrue(view.Approval);
		}

		[TestMethod]
		public void GreeterPurchaseCDThroughOfficerApproval()
		{
			GenericIdentity identity = new GenericIdentity("GreeterUser");
			GenericPrincipal principal = new GenericPrincipal(identity, new string[] {"Greeter"});
			Thread.CurrentPrincipal = principal;

			// authenticate with an Officer role
			authService.Identity = "BranchManagerUser";
			authService.Roles = new string[] {"BranchManager"};

			Account source = new Account();
			source.Balance = 10500;
			decimal amount = 10001; // need officer approval
			int duration = 10;
			presenter.ApproveAndPurchaseCD(source, amount, duration);

			Assert.IsNotNull(accountService.CDAccount);
			Assert.AreEqual(((Customer) entry.Person).CustomerId, accountService.CDAccount.CustomerId);
			Assert.AreEqual(Convert.ToSingle(amount), accountService.CDAccount.Balance);
		}


		private void CreateServiceAccounts()
		{
			customerAccountService.Accounts = new Account[3];
			Account account = new Account();
			account.AccountType = AccountType.Savings;
			customerAccountService.Accounts[0] = account;

			account = new Account();
			account.AccountType = AccountType.CD;
			customerAccountService.Accounts[1] = account;

			account = new Account();
			account.AccountType = AccountType.Checkings;
			customerAccountService.Accounts[2] = account;
		}
	}


	class MockPurchaseCDView : IPurchaseCDView
	{
		public string Message = null;
		public bool Approval;
		public Account[] Accounts = null;
		public Rate[] Rates = null;
		public bool Cleared;
		public decimal Rate;

		public void ShowMessage(string message)
		{
			Message = message;
		}


		public void ShowApproval(bool visible)
		{
			Approval = visible;
		}


		public void ShowCustomerAccounts(Account[] accounts)
		{
			Accounts = accounts;
		}

		public void ShowRatesTable(Rate[] rates)
		{
			Rates = rates;
		}

		public void Clear()
		{
			Cleared = true;
		}

		public void ShowInterestRate(decimal rate)
		{
			Rate = rate;
		}
	}

	class TestablePresenter : PurchaseCDViewPresenter
	{
		[InjectionConstructor]
		public TestablePresenter
			(
			[ComponentDependency("QueueEntry")] QueueEntry queueEntry,
			[ServiceDependency] IQuoteService quoteService,
			[ServiceDependency] ICustomerAccountService customerAccountsService,
			[ServiceDependency] IAccountService accountService
			)
			: base(queueEntry, quoteService, customerAccountsService, accountService)
		{
		}

		public new Account[] GetCustomerAccounts()
		{
			return base.GetCustomerAccounts();
		}

		public new Rate[] GetRatesTable()
		{
			return base.GetRatesTable();
		}

		public new decimal GetInterestRate(decimal amount, int duration)
		{
			return base.GetInterestRate(amount, duration);
		}
	}

	public class MockAuthenticationService : IAuthenticationService
	{
		public string Identity = "";
		public string[] Roles;

		#region IAuthenticationService Members

		public void Authenticate()
		{
			if (Identity == "NotExists")
				throw new AuthenticationException();

			GenericIdentity identity = new GenericIdentity(Identity);
			GenericPrincipal principal = new GenericPrincipal(identity, Roles);
			Thread.CurrentPrincipal = principal;
		}

		#endregion
	}
}