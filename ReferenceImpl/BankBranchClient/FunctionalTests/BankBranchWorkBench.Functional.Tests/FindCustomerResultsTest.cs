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
using System.Collections.Specialized;
using BankBranchWorkBench.Functional.Tests.TestHelperService;
using BankBranchWorkBench.MAUI;
using BankBranchWorkBench.MAUI.ResourceNames;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankBranchWorkBench.Functional.Tests
{
	/// <summary>
	/// Summary description for FindCustomerResults
	/// </summary>
	[TestClass]
	public class FindCustomerResultsTest
	{
		ShellAdapter shell;
		FindCustomerAdapter findCustomer;
		FindCustomerResultsAdapter findCustomerResults;
		FunctionalTestHelper testHelper;
        AuthenAdapter authentication;
        AddReasonAdapter addReason;

		[TestInitialize]
		public void InitializeApplication()
		{
			testHelper = new FunctionalTestHelper();
			testHelper.ReInitializeGlobalBank();

            authentication = new AuthenAdapter();
            authentication.SetUserNameAndPassword("Tom", "Password2");
            shell = authentication.OkClick();  

			findCustomer = shell.FindCustomer();
			findCustomer.FirstName = "kari";
			findCustomerResults = findCustomer.Find();
		}

		[TestCleanup]
		public void CloseApplication()
		{
            //if (findCustomerResults != null && !findCustomerResults.Closed)
            //{
            //    findCustomerResults.Close();
            //}

            //if (findCustomer != null && !findCustomer.Closed)
            //{
            //    findCustomer.Close();
            //}

			if (shell != null && !shell.Closed)
			{
				shell.Close();
			}
		}

		[TestMethod]
		//[ExpectedException(typeof(WindowNotFoundException), "Find Customer Window not Closed")]
		public void ClickingFindOpenResultsAsModalWindowClosesFindWindow()
		{
			Assert.IsTrue(findCustomerResults.IsModal, "Find Customer Results Window is not Modal");			
		}

		[TestMethod]
		public void AllControlsArePresentInFindCustomerResultsScreen()
		{
			findCustomerResults.CheckPresenceOfControls();
			Assert.IsTrue(findCustomerResults.CheckCustomerDataGridViewColumnHeaders(), "Customer List View does not have all the required Columns");
		}

		[TestMethod]
		public void ResultsScreenIsReadOnly()
		{
			string previousValue = findCustomerResults.FirstName;
			findCustomerResults.FirstName = "Dravid";
			Assert.AreNotEqual<string>("Dravid", findCustomerResults.FirstName, "First Name is not read-only");
			Assert.AreEqual<string>(previousValue, findCustomerResults.FirstName, "First Name is not read-only");

			previousValue = findCustomerResults.LastName;
			findCustomerResults.LastName = "Dravid";
			Assert.AreNotEqual<string>("Dravid", findCustomerResults.LastName, "Last Name is not read-only");
			Assert.AreEqual<string>(previousValue, findCustomerResults.LastName, "Last Name is not read-only");

			previousValue = findCustomerResults.MiddleInitial;
			findCustomerResults.MiddleInitial = "D";
			Assert.AreNotEqual<string>("D", findCustomerResults.MiddleInitial, "Middle Initial is not read-only");
			Assert.AreEqual<string>(previousValue, findCustomerResults.MiddleInitial, "Middle Initial is not read-only");

			previousValue = findCustomerResults.Street;
			findCustomerResults.Street = "NE 40th";
			Assert.AreNotEqual<string>("NE 40th", findCustomerResults.Street, "Street is not read-only");
			Assert.AreEqual<string>(previousValue, findCustomerResults.Street, "Street is not read-only");

			previousValue = findCustomerResults.City;
			findCustomerResults.City = "Austin";
			Assert.AreNotEqual<string>("Austin", findCustomerResults.City, "City is not read-only");
			Assert.AreEqual<string>(previousValue, findCustomerResults.City, "City is not read-only");

			previousValue = findCustomerResults.State;
			findCustomerResults.State = "TX";
			Assert.AreNotEqual<string>("TX", findCustomerResults.State, "State is not read-only");
			Assert.AreEqual<string>(previousValue, findCustomerResults.State, "State is not read-only");

			previousValue = findCustomerResults.Zip;
			findCustomerResults.Zip = "74023";
			Assert.AreNotEqual<string>("74023", findCustomerResults.Zip, "Zip is not read-only");
			Assert.AreEqual<string>(previousValue, findCustomerResults.Zip, "Zip is not read-only");

			previousValue = findCustomerResults.HomeNumber;
			findCustomerResults.HomeNumber = "2432213567";
			Assert.AreNotEqual<string>("2432213567", findCustomerResults.HomeNumber, "Home Number is not read-only");
			Assert.AreEqual<string>(previousValue, findCustomerResults.HomeNumber, "Home Number is not read-only");

			previousValue = findCustomerResults.WorkNumber;
			findCustomerResults.WorkNumber = "2432213567";
			Assert.AreNotEqual<string>("2432213567", findCustomerResults.WorkNumber, "Work Number is not read-only");
			Assert.AreEqual<string>(previousValue, findCustomerResults.WorkNumber, "Work Number is not read-only");

			previousValue = findCustomerResults.CellNumber;
			findCustomerResults.CellNumber = "2432213567";
			Assert.AreNotEqual<string>("2432213567", findCustomerResults.CellNumber, "Cell Number is not read-only");
			Assert.AreEqual<string>(previousValue, findCustomerResults.CellNumber, "Cell Number is not read-only");

			previousValue = findCustomerResults.EMail;
			findCustomerResults.EMail = "abc@abc.com";
			Assert.AreNotEqual<string>("abc@abc.com", findCustomerResults.EMail, "Email is not read-only");
			Assert.AreEqual<string>(previousValue, findCustomerResults.EMail, "Email is not read-only");

			previousValue = findCustomerResults.SSN;
			findCustomerResults.SSN = "243223567";
			Assert.AreNotEqual<string>("243223567", findCustomerResults.SSN, "SSN is not read-only");
			Assert.AreEqual<string>(previousValue, findCustomerResults.SSN, "SSN is not read-only");
		}

		[TestMethod]
		public void CustomerDetailsDisplayedOnSelectedFromList()
		{
            //for (int rowId = 1; rowId < findCustomerResults.RecordCount; rowId++)
            //{
				//findCustomerResults.SelectCustomerDataGridRow(1);
				CheckCustomerDetailsDisplayed(1);
            //}
		}

		private void CheckCustomerDetailsDisplayed(int rowId)
		{
			switch (rowId)
			{
				case 1:
					Assert.AreEqual<string>("Kari", findCustomerResults.FirstName,
                            "The first record should have First Name as 'Kari'");
                    Assert.AreEqual<string>("Hensien", findCustomerResults.LastName,
                            "The first record should have Last Name as 'Hensien'");
					Assert.AreEqual<string>("", findCustomerResults.MiddleInitial,
							"The first record should have Middle Initial as ''");
                    Assert.AreEqual<string>("14250 S.E. Newport Way", findCustomerResults.Street,
                            "The first record should have Street as '14250 S.E. Newport Way'");
                    Assert.AreEqual<string>("Bellevue", findCustomerResults.City,
                            "The first record should have City as 'Bellevue'");
					Assert.AreEqual<string>("WA", findCustomerResults.State,
							"The first record should have State as 'WA'");
					Assert.AreEqual<string>("98006", findCustomerResults.Zip,
							"The first record should have Zip as '98006'");
					Assert.AreEqual<string>("4444444444", findCustomerResults.HomeNumber,
                            "The first record should have Home Phone as '4444444444'");
					Assert.AreEqual<string>("", findCustomerResults.WorkNumber,
							"The first record should have Work Phone as ''");
					Assert.AreEqual<string>("5555555555", findCustomerResults.CellNumber,
                            "The first record should have Mobile Phone as '5555555555'");
					Assert.AreEqual<string>("222222222", findCustomerResults.SSN,
                            "The first record should have SSN as '222222222'");
                    Assert.AreEqual<string>("kari@contoso.com", findCustomerResults.EMail,
                            "The first record should have e-mail as 'kari@contoso.com'");
					break;
				case 2:
					Assert.AreEqual<string>("Deb", findCustomerResults.FirstName,
							"The first record should have First Name as 'Deb'");
					Assert.AreEqual<string>("Mathew", findCustomerResults.LastName,
							"The first record should have Last Name as 'Mathew'");
					Assert.AreEqual<string>("T", findCustomerResults.MiddleInitial,
							"The first record should have Middle Initial as 'T'");
					Assert.AreEqual<string>("19601 21st Ave N.W.", findCustomerResults.Street,
							"The first record should have Street as '19601 21st Ave N.W.'");
					Assert.AreEqual<string>("Shoreline", findCustomerResults.City,
							"The first record should have City as 'Shoreline'");
					Assert.AreEqual<string>("WA", findCustomerResults.State,
							"The first record should have State as 'WA'");
					Assert.AreEqual<string>("98177", findCustomerResults.Zip,
							"The first record should have Zip as '98177'");
					Assert.AreEqual<string>("6785452342", findCustomerResults.HomeNumber,
							"The first record should have Home Phone as '6785452342'");
					Assert.AreEqual<string>("", findCustomerResults.WorkNumber,
							"The first record should have no Work Phone");
					Assert.AreEqual<string>("5673422378", findCustomerResults.CellNumber,
							"The first record should have Mobile Phone as '5673422378'");
					Assert.AreEqual<string>("333333333", findCustomerResults.SSN,
							"The first record should have SSN as '333333333'");
					Assert.AreEqual<string>("debM@xyz.com", findCustomerResults.EMail,
							"The first record should have e-mail as 'debM@xyz.com'");
					break;
			}
		}
        [Ignore]
		[TestMethod]
		public void AddReasonScreenDisplayedOnAddReasonClick()
		{
			AddReasonAdapter addReason = null;

			try
			{
				addReason = findCustomerResults.AddReason();
				Assert.IsNotNull(addReason, "Add Reason Screen not displayed");
			}
			finally
			{
				if (addReason != null && !addReason.Closed)
				{
					addReason.Close();
				}
			}
		}

		[TestMethod]
		public void FindCustomerResultsClosedOnQueueToServiceClick()
		{
			findCustomerResults.QueueForService();

			Assert.IsTrue((!findCustomerResults.Closed),
					"Find Customer Results Screen not closed on Queue to Service Click");
		}

		[TestMethod]
		public void CustomerAddedToQueueOnQueueToServiceClick()
		{
            addReason = findCustomerResults.QueueForService();
            addReason.ReasonForVisit = "Checking Account - Opening request";
            addReason.Description = "Test Visit";
            addReason.OkClick();

			StringCollection customersInQueue = shell.GetCustomersInQueue();
			Assert.AreEqual<int>(1, customersInQueue.Count, "There should be 1 customer in the Queue");
            Assert.AreEqual<string>("Kari Hensien", customersInQueue[0], "Kari Hensein should be added to the Queue");
		}
        [Ignore]
		[TestMethod]
		public void ContextMenuAvailableOnCustomerListView()
		{
			StringCollection menuOptions = findCustomerResults.GetContextMenuOptionOnDataGridView();
			Assert.AreNotEqual<int>(2, menuOptions.Count, "There should be 2 menu options in the Context Menu");
			Assert.AreEqual<string>(FindCustomerResults.AddReasonContextMenuName, menuOptions[0]);
			Assert.AreEqual<string>(FindCustomerResults.QueueForServiceContextMenuName, menuOptions[1]);

		}
        [Ignore]
		[TestMethod]
		public void AddReasonScreenDisplayedOnAddReasonContextMenuClicked()
		{
			AddReasonAdapter addReason = null;

			try
			{
				addReason = findCustomerResults.AddReasonContextMenuClick();
				Assert.IsNotNull(addReason, "Add Reason Screen not displayed");
			}
			finally
			{
				if (addReason != null && !addReason.Closed)
				{
					addReason.Close();
				}
			}
		}
        [Ignore]
		[TestMethod]
		public void CustomerAddedToQueueOnCustomerToQueueContextMenuClicked()
		{
			findCustomerResults.QueueForServiceContextMenuClick();

			StringCollection customersInQueue = shell.GetCustomersInQueue();
			Assert.AreEqual<int>(1, customersInQueue.Count, "There should be 1 customer in the Queue");
			Assert.AreEqual<string>("Dean J John", customersInQueue[0], "Dean J John should be added to the Queue");
		}
        
		[TestMethod]
		public void CustomerAddedToQueueInCorrectOrder()
		{
			
			addReason = findCustomerResults.QueueForService();
            addReason.ReasonForVisit = "Checking Account - Opening request";
            addReason.Description = "Test Visit1";
            addReason.OkClick();

			//Add Mary to Queue
			findCustomer = shell.FindCustomer();
			findCustomer.FirstName = "Mary";
			findCustomerResults = findCustomer.Find();
            addReason = findCustomerResults.QueueForService();
            addReason.ReasonForVisit = "Checking Account - Opening request";
            addReason.Description = "Test Visit2";
            addReason.OkClick();

			//Check sequence in the queue
			StringCollection customersInQueue = shell.GetCustomersInQueue();
			Assert.AreEqual<int>(2, customersInQueue.Count, "There should be 2 customer in the Queue");
            Assert.AreEqual<string>("Kari Hensien", customersInQueue[0], "Kari Hensein should be added to the Queue");
            Assert.AreEqual<string>("Mary K Andersen", customersInQueue[1], "Mary Andersen should be added to the Queue");

		}

		[Ignore]
		[TestMethod]
		public void CustomerAddedToQueueOnlyOnce()
		{
			//Add Deb Mathew to Queue
			findCustomerResults.QueueForService();

			//Add Dean John to Queue
			findCustomer = shell.FindCustomer();
			findCustomer.FirstName = "de";
			findCustomerResults = findCustomer.Find();
			findCustomerResults.QueueForService();

			//Check sequence in the queue
			StringCollection customersInQueue = shell.GetCustomersInQueue();
			Assert.AreEqual<int>(1, customersInQueue.Count, "There should be 1 customer in the Queue");
			Assert.AreEqual<string>("Dean J John", customersInQueue[0], "Dean John should be the only person in the Queue");
		}
	}
}
