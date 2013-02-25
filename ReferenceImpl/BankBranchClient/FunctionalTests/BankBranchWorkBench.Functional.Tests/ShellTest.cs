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
using System.Collections.Specialized;
using BankBranchWorkBench.Functional.Tests.TestHelperService;
using BankBranchWorkBench.MAUI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace BankBranchWorkBench.Functional.Tests
{
	[TestClass]
	public class ShellTest
	{
		ShellAdapter shell;
		FunctionalTestHelper testHelper;
		AuthenAdapter authen;
        AddReasonAdapter addReason;

		[TestInitialize]
		public void InitializeApplication()
		{
			testHelper = new FunctionalTestHelper();
			testHelper.ReInitializeGlobalBank();

            authen = new AuthenAdapter();           
		}

		[TestCleanup]
		public void CloseApplication()
		{
			if (shell != null)
			{
				shell.Close();
			}
		}

		[TestMethod]
		public void CheckInitialViewofShellOnApplicationStartup()
		{
            authen.SetUserNameAndPassword("Tom", "Password2");
            shell = authen.OkClick();
            shell.CheckInitialShellView();
		}

		[TestMethod]
		public void LauchBarToolStripHasCorrectNumberOfIconsForGreeter()
		{
            authen.SetUserNameAndPassword("Jerry", "Password1");
            shell = authen.OkClick();
            Assert.AreEqual<int>(1, shell.LauchBarIconCount, "Number of Icons in LauchBar should be 1");
		}

		[TestMethod]
		public void LauchBarToolStripHasCorrectNumberOfIconsForOfficer()
		{
            authen.SetUserNameAndPassword("Tom", "Password2");
            shell = authen.OkClick();
            Assert.AreEqual<int>(2, shell.LauchBarIconCount, "Number of Icons in LauchBar should be 2");
		}

		[TestMethod]
		public void LauchBarToolStripHasCorrectNumberOfIconsForManager()
		{
            authen.SetUserNameAndPassword("Spike", "Password3");
            shell = authen.OkClick();
            Assert.AreEqual<int>(2, shell.LauchBarIconCount, "Number of Icons in LauchBar should be 2");
		}

		[TestMethod]
		public void CanNavigateToMyCustomerQueue()
		{
            authen.SetUserNameAndPassword("Tom", "Password2");
            shell = authen.OkClick();
            shell.MyCustomerQueue();
			Assert.IsTrue(shell.MyCustomerQueueDisplayed, "My Customer Queue is not displayed");
		}

		[TestMethod]
		public void CanNavigateBetweenCustomerQueueAndMyCustomerQueue()
		{
            authen.SetUserNameAndPassword("Tom", "Password2");
            shell = authen.OkClick();
            shell.MyCustomerQueue();
			Assert.IsTrue(shell.MyCustomerQueueDisplayed, "My Customer Queue is not displayed");
			shell.CustomerQueue();
			Assert.IsTrue(shell.CustomerQueueDisplayed, "Customer Queue is not displayed");
		}

		[TestMethod]
		public void CustomerQueueIsEmptyWhenCustomersNotPresentInQueue()
		{
            authen.SetUserNameAndPassword("Jerry", "Password1");
            shell = authen.OkClick();
            StringCollection customersInQueue = shell.GetCustomersInQueue();
			Assert.AreEqual<int>(0, customersInQueue.Count, "Customer Queue is not empty");
		}

		[TestMethod]
		public void CustomersInQueueDisplayedOnApplicationStartup()
		{
			FindCustomerAdapter findCustomer;
			FindCustomerResultsAdapter findCustomerResults;

            authen.SetUserNameAndPassword("Tom", "Password2");
            shell = authen.OkClick();
            //Add Kari to Queue
			findCustomer = shell.FindCustomer();
			findCustomer.FirstName = "Kari";
			findCustomerResults = findCustomer.Find();
            addReason = findCustomerResults.QueueForService();
            addReason.ReasonForVisit = "Checking Account - Opening request";
            addReason.Description = "Test Visit";
            addReason.OkClick();

			//Add Mary to Queue
			findCustomer = shell.FindCustomer();
			findCustomer.FirstName = "Mary";
			findCustomerResults = findCustomer.Find();
            addReason = findCustomerResults.QueueForService();
            addReason.ReasonForVisit = "Checking Account - Opening request";
            addReason.Description = "Test Visit";
            addReason.OkClick();

			StringCollection customersInQueue = shell.GetCustomersInQueue();
			Assert.AreEqual<int>(2, customersInQueue.Count,
					"There should be 2 customers in the Queue on start up");
            Assert.AreEqual<string>("Kari Hensien", customersInQueue[0],
                    "Kari Hensien should be the first person in the Queue");
            Assert.AreEqual<string>("Mary K Andersen", customersInQueue[1],
                    "Mary K Andersen should be the second person in the Queue");
		}

		[TestMethod]
		public void ServiceCustomerLinkPresent()
		{
            authen.SetUserNameAndPassword("Tom", "Password2");
            shell = authen.OkClick();
            shell.CustomerQueue();
            shell.CheckForServiceCustomerLink();
		}

		[TestMethod]
		public void ServiceCustomerLinkDisabledForEmptyCustomerQueue()
		{
            authen.SetUserNameAndPassword("Tom", "Password2");
            shell = authen.OkClick();
            shell.CustomerQueue();
            Assert.IsFalse(shell.ServiceCustomerEnabled,
					"Service Customer Link enabled when Queue is empty");
		}

		[TestMethod]
		public void ServiceCustomerLinkEnabledForNonEmptyCustomerQueue()
		{
			FindCustomerAdapter findCustomer;
			FindCustomerResultsAdapter findCustomerResults;
            authen.SetUserNameAndPassword("Tom", "Password2");
            shell = authen.OkClick();
			//Add Kari to Queue
			findCustomer = shell.FindCustomer();
			findCustomer.FirstName = "Kari";
			findCustomerResults = findCustomer.Find();
            addReason = findCustomerResults.QueueForService();
            addReason.ReasonForVisit = "Checking Account - Opening request";
            addReason.Description = "Test Visit";
            addReason.OkClick();
			Assert.IsTrue(shell.ServiceCustomerEnabled,
					"Service Customer Link disabled when Queue is non-empty");
		}

		[TestMethod]
		public void CustomerBeingServicedIsRemovedFromCustomerQueue()
		{
			FindCustomerAdapter findCustomer;
			FindCustomerResultsAdapter findCustomerResults;
			RightPaneAdapter rightPane;
            
            authen.SetUserNameAndPassword("Tom", "Password2");
            shell = authen.OkClick();
			//Add kari to Queue
			findCustomer = shell.FindCustomer();
			findCustomer.FirstName = "kari";
			findCustomerResults = findCustomer.Find();
			findCustomerResults.QueueForService();

			rightPane = shell.ServiceCustomer();
			shell.CustomerQueue();

			// Customer queue is updated in the background; sleep and re-check periodically
			int totalWait = 10000;

			while (totalWait > 0)
			{
				if (shell.GetCustomersInQueue().Count == 0)
					break;

				Thread.Sleep(100);
				totalWait -= 100;
			}

			Assert.AreEqual<int>(0, shell.GetCustomersInQueue().Count);
		}

		[TestMethod]
		public void CustomerBeingServicedMovedToMyCustomerQueue()
		{
			FindCustomerAdapter findCustomer;
			FindCustomerResultsAdapter findCustomerResults;
			RightPaneAdapter rightPane;
            
            authen.SetUserNameAndPassword("Tom", "Password2");
            shell = authen.OkClick();
			//Add Kari to Queue
			findCustomer = shell.FindCustomer();
			findCustomer.FirstName = "Kari";
			findCustomerResults = findCustomer.Find();
            addReason = findCustomerResults.QueueForService();
            addReason.ReasonForVisit = "Checking Account - Opening request";
            addReason.Description = "Test Visit";
            addReason.OkClick();

			rightPane = shell.ServiceCustomer();
            shell.MyCustomerQueue();
			StringCollection myCustomers = shell.GetCustomersInMyQueue();
			Assert.AreEqual<int>(1, myCustomers.Count);
            Assert.AreEqual<string>("Kari Hensien", myCustomers[0], "Customer Name in MyQueue is not correct");
		}
        [Ignore]
        [TestMethod]
        public void CheckStatusBarDisplaysUserNameAndRole()
        {
            authen.SetUserNameAndPassword("Tom", "Password2");
            shell = authen.OkClick();
            Assert.IsTrue(IsUserNameAndRoleDisplayed(1), "Officer Name and Role is not displayed");
            authen.SetUserNameAndPassword("Tom", "Password2");            
            authen.OkClick();     
            Assert.IsTrue(IsUserNameAndRoleDisplayed(2), "Greeter Name and Role is not displayed");
        }

        private bool IsUserNameAndRoleDisplayed(int scenario)
        {

            if (scenario != 1)
            {
                return ((shell.StatusBarValue("_userLabel") == "Jerry") && (shell.StatusBarValue("_rolesLabel") == "Greeter"));

            }
            else
            {
                return ((shell.StatusBarValue("_userLabel") == "Tom") && (shell.StatusBarValue("_rolesLabel") == "Officer"));
            }
        }

        
	}
}
