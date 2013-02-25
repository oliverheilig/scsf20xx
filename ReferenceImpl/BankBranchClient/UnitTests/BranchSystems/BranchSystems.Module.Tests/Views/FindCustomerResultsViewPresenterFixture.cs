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
using System.Windows.Forms;
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
	public class FindCustomerResultsViewPresenterFixture
	{		
		[TestMethod]
		public void AddForReasonShowsTheAddCustomerToQueueView()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			workItem.Services.AddNew<MockMessageBoxService, IMessageBoxService>();
			MockWorkspace workspace = workItem.Workspaces.AddNew<MockWorkspace>(WorkspaceNames.ModalWindows);
			MockFindCustomerResultsView view = workItem.SmartParts.AddNew<MockFindCustomerResultsView>();
			WorkspaceLocatorService locatorService =
				workItem.Services.AddNew<WorkspaceLocatorService, IWorkspaceLocatorService>();
			MockCustomerQueueService queueService = workItem.Services.AddNew<MockCustomerQueueService, ICustomerQueueService>();

			Customer customer = new Customer();
			Customer[] response = new Customer[] {customer};
			workItem.Items.Add(response, "FindCustomerResponse");


			workspace.Show(view);
			FindCustomerResultsViewPresenter presenter = workItem.Items.AddNew<FindCustomerResultsViewPresenter>();
			presenter.View = view;


			presenter.AddReasonForVisit(customer);

			Assert.IsTrue(CollectionUtilities.ContainsOneOf(workspace.SmartParts, typeof (IAddVisitorToQueueView)));
		}

		class MockFindCustomerResultsView : IFindCustomerResultsView
		{
			public void NotifyCustomerAlreadyInQueue()
			{
				throw new Exception("The method or operation is not implemented.");
			}


			public void ShowResults(Customer[] customers)
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}

		class MockMessageBoxService : IMessageBoxService
		{
			public DialogResult Show(string text)
			{
				return DialogResult.OK;
			}

			public DialogResult Show(string text, string caption)
			{
				return DialogResult.OK;
			}

			public DialogResult Show(string text, string caption, MessageBoxButtons buttons)
			{
				return DialogResult.OK;
			}

			public DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
			{
				return DialogResult.OK;
			}
		}
	}
}