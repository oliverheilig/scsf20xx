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
using BankBranchWorkBench.Functional.Tests.TestHelperService;
using BankBranchWorkBench.MAUI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankBranchWorkBench.Functional.Tests
{
	/// <summary>
	/// Summary description for RightPaneTest
	/// </summary>
	[TestClass]
	public class RightPaneTest
	{
		ShellAdapter shell;
		AuthenAdapter authen;
		FindCustomerAdapter findCustomer;
		FindCustomerResultsAdapter findCustomerResults;
		RightPaneAdapter rightPane;
		FunctionalTestHelper testHelper;
        AddReasonAdapter addReason;

		[TestInitialize]
		public void InitializeApplication()
		{
			testHelper = new FunctionalTestHelper();
			testHelper.ReInitializeGlobalBank();
			
            authen = new AuthenAdapter();
            authen.SetUserNameAndPassword("Tom", "Password2");
            shell = authen.OkClick();  
			
			findCustomer = shell.FindCustomer();
			findCustomer.FirstName = "Kari";
			findCustomerResults = findCustomer.Find();
            addReason = findCustomerResults.QueueForService();
            addReason.ReasonForVisit = "Checking Account - Opening request";
            addReason.Description = "Test Visit";
            addReason.OkClick();

			rightPane = shell.ServiceCustomer();
		}

		[TestCleanup]
		public void CloseApplication()
		{
			if (shell != null && !shell.Closed)
            {
                shell.Close();
            }
		}

		[TestMethod]
		public void RightPaneCaptionIsCustomerName()
		{
            Assert.AreEqual<string>("Kari  Hensien", rightPane.CustomerNameOnRightPane,
					"FullName of Customer not displayed in Right Pane");
		}

		[TestMethod]
		public void AllControlsArePresentInRightPaneScreen()
		{
			rightPane.CheckPresenceOfControls();
		}
	}
}
