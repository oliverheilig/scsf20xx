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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankBranchWorkBench.Functional.Tests
{
	
    [TestClass]
	public class AddReasonTest
	{
		ShellAdapter shell;
		FindCustomerAdapter findCustomer;
		FindCustomerResultsAdapter findCustomerResults;
		AddReasonAdapter addReason;
		FunctionalTestHelper testHelper;
        AuthenAdapter authentication;

		[TestInitialize]
		public void InitializeApplication()
		{
			testHelper = new FunctionalTestHelper();
			testHelper.ReInitializeGlobalBank();

            authentication = new AuthenAdapter();
            authentication.SetUserNameAndPassword("Tom", "Password2");
            shell = authentication.OkClick();  
			findCustomer = shell.FindCustomer();
			findCustomer.FirstName = "Kari";
			findCustomerResults = findCustomer.Find();
                       
		}

		[TestCleanup]
		public void CloseApplication()
		{
            //if (addReason != null && !(addReason.Closed))
            //{
            //    addReason.Close();
            //}

            //if (findCustomerResults != null && !(findCustomerResults.Closed))
            //{
            //    findCustomerResults.Close();
            //}

            //if (findCustomer != null && !(findCustomer.Closed))
            //{
            //    findCustomer.Close();
            //}

			if (shell != null && !(shell.Closed))
			{
				shell.Close();
			}
		}

        [TestMethod]
        public void AddReasonScreenDisplayedOnQueueForServiceClick()
        {            
            addReason = findCustomerResults.QueueForService();
            Assert.IsNotNull(addReason, "Add Reason Screen not displayed");           
        }
		[TestMethod]
		public void ClickingAddReasonOpenAsModalWindow()
		{
            addReason = findCustomerResults.QueueForService(); 
            Assert.IsTrue(addReason.IsModal, "Add Reason Window is not Modal");
		}

		[TestMethod]
		public void AddReasonWindowIsNotResizable()
		{
            addReason = findCustomerResults.QueueForService(); 
            Assert.IsFalse(addReason.IsResizable, "Add Reason Window is Resizable");
		}

		[TestMethod]
		public void VerifyInitialButtonStatusForAddReason()
		{
			//Assert.IsFalse(addReason.IsQueueForServiceButtonEnabled, "Queue For Service Button is Initially Enabled");
			//Assert.IsFalse(addReason.IsSelfServiceButtonEnabled, "Self Service Button is Initially Enabled");
            addReason = findCustomerResults.QueueForService(); 
            Assert.IsTrue((!addReason.IsOkButtonEnabled), "Ok button is not Initially Disabled");
			Assert.IsTrue(addReason.IsCancelButtonEnabled, "Cancel button is Initially Disabled");
		}

		[TestMethod]
		public void AddReasonCustomerInfoIsReadOnly()
		{

			addReason = findCustomerResults.QueueForService();
 
			string previousValue = addReason.FirstName;
			addReason.FirstName = "Dravid";
			Assert.AreNotEqual<string>("Dravid", addReason.FirstName, "First Name is not read-only");
			Assert.AreEqual<string>(previousValue, addReason.FirstName, "First Name is not read-only");

			previousValue = addReason.LastName;
			addReason.LastName = "Dravid";
			Assert.AreNotEqual<string>("Dravid", addReason.LastName, "Last Name is not read-only");
			Assert.AreEqual<string>(previousValue, addReason.LastName, "Last Name is not read-only");

			previousValue = addReason.MiddleInitial;
			addReason.MiddleInitial = "D";
			Assert.AreNotEqual<string>("D", addReason.MiddleInitial, "Middle Initial is not read-only");
			Assert.AreEqual<string>(previousValue, addReason.MiddleInitial, "Middle Initial is not read-only");

			previousValue = addReason.Street;
			addReason.Street = "NE 40th";
			Assert.AreNotEqual<string>("NE 40th", addReason.Street, "Street is not read-only");
			Assert.AreEqual<string>(previousValue, addReason.Street, "Street is not read-only");

			previousValue = addReason.City;
			addReason.City = "Austin";
			Assert.AreNotEqual<string>("Austin", addReason.City, "City is not read-only");
			Assert.AreEqual<string>(previousValue, addReason.City, "City is not read-only");

			previousValue = addReason.State;
			addReason.State = "TX";
			Assert.AreNotEqual<string>("TX", addReason.State, "State is not read-only");
			Assert.AreEqual<string>(previousValue, addReason.State, "State is not read-only");

			previousValue = addReason.Zip;
			addReason.Zip = "74023";
			Assert.AreNotEqual<string>("74023", addReason.Zip, "Zip is not read-only");
			Assert.AreEqual<string>(previousValue, addReason.Zip, "Zip is not read-only");

			previousValue = addReason.HomeNumber;
			addReason.HomeNumber = "2432213567";
			Assert.AreNotEqual<string>("2432213567", addReason.HomeNumber, "Home Number is not read-only");
			Assert.AreEqual<string>(previousValue, addReason.HomeNumber, "Home Number is not read-only");

			previousValue = addReason.WorkNumber;
			addReason.WorkNumber = "2432213567";
			Assert.AreNotEqual<string>("2432213567", addReason.WorkNumber, "Work Number is not read-only");
			Assert.AreEqual<string>(previousValue, addReason.WorkNumber, "Work Number is not read-only");

			previousValue = addReason.CellNumber;
			addReason.CellNumber = "2432213567";
			Assert.AreNotEqual<string>("2432213567", addReason.CellNumber, "Cell Number is not read-only");
			Assert.AreEqual<string>(previousValue, addReason.CellNumber, "Cell Number is not read-only");

			previousValue = addReason.EMail;
			addReason.EMail = "abc@abc.com";
			Assert.AreNotEqual<string>("abc@abc.com", addReason.EMail, "Email is not read-only");
			Assert.AreEqual<string>(previousValue, addReason.EMail, "Email is not read-only");

			previousValue = addReason.SSN;
			addReason.SSN = "243223567";
			Assert.AreNotEqual<string>("243223567", addReason.SSN, "SSN is not read-only");
			Assert.AreEqual<string>(previousValue, addReason.SSN, "SSN is not read-only");
		}

		[TestMethod]
		public void AllControlsArePresentInAddReasonWindow()
		{
            addReason = findCustomerResults.QueueForService(); 
            addReason.CheckPresenceOfControls();
		}

		[TestMethod]
		public void AddReasonWindowClosedOnCancelClick()
		{
            addReason = findCustomerResults.QueueForService(); 
            addReason.Description = "Test Visit";
			addReason.Cancel();

			Assert.IsTrue(addReason.Closed, "Add Reason Window is not closed");
		}

		[TestMethod]
		public void EscKeyMappedToCancelButtonInAddReasonWindow()
		{
            addReason = findCustomerResults.QueueForService(); 
            addReason.Description = "Test Visit";
			addReason.EscPressed();

			Assert.IsTrue(addReason.Closed, "Add Reason Window is not closed");
		}

		// This test is ignored because the functionality is working but selecting values using MAUI
		// doesn't enable the buttons. Changing this to manual test case
		[Ignore]
		[TestMethod]
		public void VerifyButtonStatusChangesInAddReasonWindow()
		{
            addReason = findCustomerResults.QueueForService(); 
            addReason.ReasonForVisit = "Certificate Deposit - Claims";
			Assert.IsTrue(addReason.IsQueueForServiceButtonEnabled, "Queue For Service Button is Disabled when Reason Code is not empty");
			Assert.IsTrue(addReason.IsSelfServiceButtonEnabled, "Self Service Button is Disabled when Description is not empty");
			Assert.IsTrue(addReason.IsCancelButtonEnabled, "Cancel button is Initially Disabled");
			
			addReason.Clear();
			Assert.IsFalse(addReason.IsQueueForServiceButtonEnabled, "Queue For Service Button is Enabled when the Form is cleared");
			Assert.IsFalse(addReason.IsSelfServiceButtonEnabled, "Self Service Button is Enabled when the Form is cleared");
			Assert.IsTrue(addReason.IsCancelButtonEnabled, "Cancel button is Disabled");

			addReason.Description = "Test Visit";
			Assert.IsTrue(addReason.IsQueueForServiceButtonEnabled, "Queue For Service Button is Disabled when Description is not empty");
			Assert.IsTrue(addReason.IsSelfServiceButtonEnabled, "Self Service Button is Disabled when Description is not empty");
			Assert.IsTrue(addReason.IsCancelButtonEnabled, "Cancel button is Initially Disabled");
			
			addReason.Clear();
			Assert.IsFalse(addReason.IsQueueForServiceButtonEnabled, "Queue For Service Button is Enabled when the Form is cleared");
			Assert.IsFalse(addReason.IsSelfServiceButtonEnabled, "Self Service Button is Enabled when the Form is cleared");
			Assert.IsTrue(addReason.IsCancelButtonEnabled, "Cancel button is Disabled");
		}

		[TestMethod]
		public void QueueCustomerForServiceInAddReasonWindow()
		{
            addReason = findCustomerResults.QueueForService(); 
            addReason.ReasonForVisit = "Checking Account - Opening request";
			addReason.Description = "Test Visit";
			addReason.OkClick();

			StringCollection customersInQueue = shell.GetCustomersInQueue();
			Assert.AreEqual<int>(1, customersInQueue.Count, "There should be 1 customer in the Queue");
            Assert.AreEqual<string>("Kari Hensien", customersInQueue[0], "Kari Hensien should be added to the Queue");
		}
        
        //Note : Self Service Functionality is removed.
        [Ignore]
		[TestMethod]
		public void SelfServiceCustomerInAddReasonWindow()
		{
            findCustomerResults.SelfService();            
		}

        [Ignore]
		[TestMethod]
		public void QueueForServiceContextMenuClickedInAddReasonWindow()
		{
            addReason.ReasonForVisit = "Checking Account - Opening request";
			addReason.Description = "Test Visit";
			addReason.QueueForServiceContextMenuClick();

			StringCollection customersInQueue = shell.GetCustomersInQueue();
			Assert.AreEqual<int>(1, customersInQueue.Count, "There should be 1 customer in the Queue");
			Assert.AreEqual<string>("Kumar Abhishek", customersInQueue[0], "Kumar Abhishek should be added to the Queue");
		}
        [Ignore]
		[TestMethod]
		public void SelfServiceContextMenuClickedInAddReasonWindow()
		{
			addReason.ReasonForVisit = "1";
			addReason.Description = "Test Visit";
			addReason.SelfService();
		}
	}
}
