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
using System.Windows.Forms;
using GlobalBank.BasicAccounts.Interface.Services;
using GlobalBank.BasicAccounts.Module.Constants;
using GlobalBank.BasicAccounts.Module.Tests.Mocks;
using GlobalBank.BranchSystems.Interface.Services;
using GlobalBank.Infrastructure.Interface.BusinessEntities;
using GlobalBank.Infrastructure.Interface.Services;
using GlobalBank.UnitTest.Library;
using GlobalBank.UnitTest.Library.MockObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GlobalBank.BasicAccounts.Module.Tests
{
	[TestClass]
	public class ModuleActionsFixture
	{
		private ModuleActions actions;
		private WorkItem workItem;
		private QueueEntry queueEntry;
		private MockActionCatalog actionCatalog;
		private MockUIElementAdapter elementAdapter;
		private MockWorkspace workspace;

		[TestInitialize]
		public void TestInitialize()
		{
			workItem = new TestableRootWorkItem();

			MockUIElementAdapterFactory factory = new MockUIElementAdapterFactory();
			workItem.Services.Get<IUIElementAdapterFactoryCatalog>().RegisterFactory(factory);
			elementAdapter = new MockUIElementAdapter();
			workItem.UIExtensionSites.RegisterSite(UIExtensionSiteNames.ProductsPanel, elementAdapter);

			actionCatalog = workItem.Services.AddNew<MockActionCatalog, IActionCatalogService>();
			queueEntry = workItem.Items.AddNew<QueueEntry>("QueueEntry");
			workspace = workItem.Workspaces.AddNew<MockWorkspace>(WorkspaceNames.OfficerWorkspace);

			workItem.Services.AddNew<MockQuoteService, IQuoteService>();
			workItem.Services.AddNew<MockCustomerAccountService, ICustomerAccountService>();
			workItem.Services.AddNew<MockAccountService, IAccountService>();

			actions = workItem.Items.AddNew<ModuleActions>();
		}

		[TestMethod]
		public void ActionsArePublished()
		{
			Assert.IsTrue(actionCatalog.ActionNames.Contains(ActionNames.ShowPurchaseCDCommand));
			Assert.IsTrue(actionCatalog.ActionNames.Contains(ActionNames.ShowPurchaseCDView));
		}

		[TestMethod]
		public void ShowPurchaseCDActionPublishedCommandWhenServicingCustomer()
		{
			queueEntry.Person = new Customer();

			actions.ShowPurchaseCDCommandAction(null, null);

			Assert.IsNotNull(elementAdapter.AddedObject);
			Assert.IsTrue(elementAdapter.AddedObject is LinkLabel);
		}

		[TestMethod]
		public void ShowPurchaseCDActionNotPublishedCommandWhenServicingVisitor()
		{
			queueEntry.Person = new WalkIn();

			actions.ShowPurchaseCDCommandAction(null, null);

			Assert.IsNull(elementAdapter.AddedObject);
		}

		[TestMethod]
		public void ShowPurchaseCDViewActionAddsViewToWorkspace()
		{
			actions.ShowPurchaseCDViewAction(null, null);

			Assert.IsTrue(CollectionUtilities.ContainsOneOf(workspace.SmartParts, typeof (PurchaseCDView)));
		}
	}
}