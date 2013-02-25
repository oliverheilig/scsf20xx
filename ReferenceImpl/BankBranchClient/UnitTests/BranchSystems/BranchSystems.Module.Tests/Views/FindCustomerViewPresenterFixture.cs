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
using GlobalBank.BranchSystems.Module.Views;
using GlobalBank.Infrastructure.Interface.Services;
using GlobalBank.BranchSystems.Module.Tests.Mocks;
using GlobalBank.UnitTest.Library;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.SmartClient.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GlobalBank.UnitTest.Library.MockObjects;

namespace GlobalBank.BranchSystems.Module.Tests.Views
{
	[TestClass]
	public class FindCustomerViewPresenterFixture
	{
		TestableRootWorkItem workItem;
		MockWorkspace mockWorkspace;
		MockCustomerFinderService mockService;
		MockCustomerQueueService mockQueueService;
		WorkspaceLocatorService locatorService;


		[TestInitialize]
		public void Initialize()
		{
			workItem = new TestableRootWorkItem();
			mockWorkspace = workItem.Workspaces.AddNew<MockWorkspace>(WorkspaceNames.ModalWindows);
			mockService = workItem.Services.AddNew<MockCustomerFinderService, ICustomerFinderService>();
			mockQueueService = workItem.Services.AddNew<MockCustomerQueueService, ICustomerQueueService>();
			locatorService = workItem.Services.AddNew<WorkspaceLocatorService, IWorkspaceLocatorService>();
		}

		[TestMethod]
		public void CancelClosesViewOnTheWorkspace()
		{
			MockFindCustomerView mockView = workItem.SmartParts.AddNew<MockFindCustomerView>();
			mockWorkspace.Show(mockView);

			mockView.ClickCloseButton();

			Assert.AreEqual(0, mockWorkspace.SmartParts.Count);
		}

		[TestMethod]
		public void PresenterDependsOnIGlobalBankService()
		{
			FindCustomerViewPresenter presenter = new FindCustomerViewPresenter(mockService);
		}

		[TestMethod]
		public void InformationIsDisplayedWhenNoCustomerIsFound()
		{
			FindCustomerViewPresenter presenter = new FindCustomerViewPresenter(mockService);
			MockFindCustomerView view = new MockFindCustomerView();
			presenter.View = view;

			mockService.ResultToReturn = null;
			presenter.FindCustomer(null, null, null, null, null, null, null, null, null, null, null, null);

			Assert.IsTrue(view.ShowMessageCalled);
		}

		[TestMethod]
		public void InformationIsDisplayedWhenTooManyCustomersAreFound()
		{
			FindCustomerViewPresenter presenter = new FindCustomerViewPresenter(mockService);

			MockFindCustomerView view = new MockFindCustomerView();
			presenter.View = view;

			mockService.ResultToReturn = null;
			presenter.FindCustomer(null, null, null, null, null, null, null, null, null, null, null, null);

			Assert.IsTrue(view.ShowMessageCalled);
		}
	}

	class MockFindCustomerView : IFindCustomerView
	{
		FindCustomerViewPresenter _presenter;
		public bool ShowMessageCalled;

		[CreateNew]
		public FindCustomerViewPresenter Presenter
		{
			set
			{
				_presenter = value;
				_presenter.View = this;
			}
			get { return _presenter; }
		}

		internal void ClickCloseButton()
		{
			_presenter.OnCancel();
		}

		public void ShowMessage(string message)
		{
			ShowMessageCalled = true;
		}
	}
}
