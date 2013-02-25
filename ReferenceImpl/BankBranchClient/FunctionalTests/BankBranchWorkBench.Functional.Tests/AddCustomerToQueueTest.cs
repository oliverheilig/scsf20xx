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
	public class AddCustomerToQueueTest
	{
		ShellAdapter shell;
		AddCustomerToQueueAdapter addCustomerToQueue;
		FunctionalTestHelper testHelper;
        AuthenAdapter authentication;
        RightPaneAdapter rightPane;
        CustomerSummaryAdapter customerSummary;

		[TestInitialize]
		public void InitializeApplication()
		{
			testHelper = new FunctionalTestHelper();
			testHelper.ReInitializeGlobalBank();

            authentication = new AuthenAdapter();
            authentication.SetUserNameAndPassword("Jerry", "Password1");
            shell = authentication.OkClick();  
			addCustomerToQueue = shell.AddNewCustomerToQueue();
		}

		[TestCleanup]
		public void CloseApplication()
		{
			if (addCustomerToQueue != null && !addCustomerToQueue.Closed)
			{
				addCustomerToQueue.Close();
			}

			if (shell != null && !shell.Closed)
			{
				shell.Close();
			}
		}

		[TestMethod]
		public void ClickingAddNewCustomerToQueueOpenAsModalWindow()
		{
			Assert.IsTrue(addCustomerToQueue.IsModal, "Add New Customer to Queue Window is not Modal");
		}

		[TestMethod]
		public void AddNewCustomerToQueueWindowIsNotResizable()
		{
			Assert.IsFalse(addCustomerToQueue.IsResizable, "Add New Customer to Queue Window is Resizable");
		}

		[TestMethod]
		public void VerifyInitialButtonStatusForAddNewCustomerToQueueWindow()
		{
			//Assert.IsFalse(addCustomerToQueue.IsQueueForServiceButtonEnabled, "Queue For Service Button is Initially Enabled");
			//Assert.IsFalse(addCustomerToQueue.IsSelfServiceButtonEnabled, "Self Service Button is Initially Enabled");
            Assert.IsTrue((!addCustomerToQueue.IsOkButtonEnabled), "Ok button is not Initially Disabled");
			Assert.IsTrue(addCustomerToQueue.IsCancelButtonEnabled, "Cancel button is Initially Disabled");
		}

		[TestMethod]
		public void AllControlsArePresentInAddNewCustomerToQueueWindow()
		{
			addCustomerToQueue.CheckPresenceOfControls();
		}

		[TestMethod]
		public void FieldsAreInitiallyBlankInAddNewCustomerToQueueWindow()
		{
			//Assert.IsTrue(addCustomerToQueue.CustomerID == null || addCustomerToQueue.CustomerID == String.Empty, "Customer ID is not Empty");
			Assert.IsTrue(addCustomerToQueue.FirstName == null || addCustomerToQueue.FirstName == String.Empty, "First Name is not Empty");
			Assert.IsTrue(addCustomerToQueue.LastName == null || addCustomerToQueue.LastName == String.Empty, "Last Name is not Empty");
			Assert.IsTrue(addCustomerToQueue.MiddleInitial == null || addCustomerToQueue.MiddleInitial == String.Empty, "Middle Name is not Empty");
			Assert.IsTrue(addCustomerToQueue.Street == null || addCustomerToQueue.Street == String.Empty, "Street is not Empty");
			Assert.IsTrue(addCustomerToQueue.City == null || addCustomerToQueue.City == String.Empty, "City is not Empty");
			Assert.IsTrue(addCustomerToQueue.State == null || addCustomerToQueue.State == String.Empty, "State is not Empty");
			Assert.IsTrue(addCustomerToQueue.Zip == null || addCustomerToQueue.Zip == String.Empty, "Zip is not Empty");
			Assert.IsTrue(addCustomerToQueue.HomeNumber == null || addCustomerToQueue.HomeNumber == String.Empty, "Home Number is not Empty");
			Assert.IsTrue(addCustomerToQueue.WorkNumber == null || addCustomerToQueue.WorkNumber == String.Empty, "Work Number is not Empty");
			Assert.IsTrue(addCustomerToQueue.CellNumber == null || addCustomerToQueue.CellNumber == String.Empty, "Cell Number is not Empty");
			Assert.IsTrue(addCustomerToQueue.SSN == null || addCustomerToQueue.SSN == String.Empty, "SSN is not Empty");
			Assert.IsTrue(addCustomerToQueue.EMail == null || addCustomerToQueue.EMail == String.Empty, "Email is not Empty");

		}

		[TestMethod]
		public void AllFieldsTruncateToMaxLengthInAddNewCustomerToQueueWindow()
		{
			addCustomerToQueue.FirstName = "ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLM";
			Assert.AreNotEqual<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLM",
					addCustomerToQueue.FirstName, "First Name accepts more than 64 characters");
			Assert.AreEqual<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKL",
					addCustomerToQueue.FirstName, "First Name not truncated to the first 64 characters entered");

			addCustomerToQueue.MiddleInitial = "AB";
			Assert.AreNotEqual<string>("AB", addCustomerToQueue.MiddleInitial,
					"Middle Intital accepts more than 1 character");
			Assert.AreEqual<string>("A", addCustomerToQueue.MiddleInitial,
					"Middle Intital not truncated to the first character entered");

			addCustomerToQueue.LastName = "ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLM";
			Assert.AreNotEqual<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLM",
					addCustomerToQueue.LastName, "Last Name accepts more than 64 characters");
			Assert.AreEqual<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKL",
					addCustomerToQueue.LastName, "Last Name not truncated to the first 64 characters entered");

			addCustomerToQueue.Street = "123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789";
			Assert.AreNotEqual<string>("123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789",
					addCustomerToQueue.Street, "Street accepts more than 128 characters");
			Assert.AreEqual<string>("12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678",
					addCustomerToQueue.Street, "Street not truncated to the first 128 characters entered");

			addCustomerToQueue.City = "ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLM";
			Assert.AreNotEqual<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLM",
					addCustomerToQueue.City, "City accepts more than 64 characters");
			Assert.AreEqual<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKL",
					addCustomerToQueue.City, "City not truncated to the first 64 characters entered");

			addCustomerToQueue.State = "ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLM";
			Assert.AreNotEqual<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLM",
					addCustomerToQueue.State, "State accepts more than 64 characters");
			Assert.AreEqual<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKL",
					addCustomerToQueue.State, "State not truncated to the first 64 characters entered");

			addCustomerToQueue.Zip = "123456";
			Assert.AreNotEqual<string>("123456", addCustomerToQueue.Zip, "Zip accepts more than 5 characters");
			Assert.AreEqual<string>("12345", addCustomerToQueue.Zip,
					"Zip not truncated to the first 5 characters entered");

			addCustomerToQueue.HomeNumber = "1234567890123456789012345";
			Assert.AreNotEqual<string>("1234567890123456789012345", addCustomerToQueue.HomeNumber,
					"Home Number accepts more than 24 characters");
			Assert.AreEqual<string>("123456789012345678901234", addCustomerToQueue.HomeNumber,
					"Home Number not truncated to the first 24 characters entered");

			addCustomerToQueue.WorkNumber = "1234567890123456789012345";
			Assert.AreNotEqual<string>("1234567890123456789012345",
					addCustomerToQueue.WorkNumber, "Work Number accepts more than 24 characters");
			Assert.AreEqual<string>("123456789012345678901234", addCustomerToQueue.WorkNumber,
					"Work Number not truncated to the first 24 characters entered");

			addCustomerToQueue.CellNumber = "1234567890123456789012345";
			Assert.AreNotEqual<string>("1234567890123456789012345", addCustomerToQueue.CellNumber,
					"Cell Number accepts more than 10 characters");
			Assert.AreEqual<string>("123456789012345678901234", addCustomerToQueue.CellNumber,
					"Cell Number not truncated to the first 24 characters entered");

			addCustomerToQueue.SSN = "1234567890";
			Assert.AreNotEqual<string>("1234567890", addCustomerToQueue.SSN, "SSN accepts more than 9 characters");
			Assert.AreEqual<string>("123456789", addCustomerToQueue.SSN,
					"SSN not truncated to the first 9 characters entered");

			addCustomerToQueue.EMail = "ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLABCDEF@HIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZAB.DEFGHIJKLM";
			Assert.AreNotEqual<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLABCDEF@HIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZAB.DEFGHIJKLM",
					addCustomerToQueue.EMail, "Email accepts more than 128 characters");
			Assert.AreEqual<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLABCDEF@HIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZAB.DEFGHIJKL",
					addCustomerToQueue.EMail, "Email not truncated to the first 128 characters entered");

			addCustomerToQueue.Description = "ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLABCDEF@HIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZAB.DEFGHIJKLMABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLABCDEF@HIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZAB.DEFGHIJKLM";
			Assert.AreNotEqual<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLABCDEF@HIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZAB.DEFGHIJKLMABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLABCDEF@HIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZAB.DEFGHIJKLM",
					addCustomerToQueue.Description, "Description accepts more than 256 characters");
			Assert.AreEqual<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLABCDEF@HIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZAB.DEFGHIJKLMABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLABCDEF@HIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZAB.DEFGHIJK",
					addCustomerToQueue.Description, "Description not truncated to the first 256 characters entered");
		}

		[TestMethod]
		public void NameFieldsOnlyAllowsAlphabetsAndSpacesInAddNewCustomerToQueueWindow()
		{
			addCustomerToQueue.FirstName = "Anil Kumble";
			Assert.AreEqual<string>("Anil Kumble", addCustomerToQueue.FirstName, "First Name does not accept valid names");

			addCustomerToQueue.FirstName = "Ten%dulkar1";
			Assert.AreNotEqual<string>("Ten%dulkar", addCustomerToQueue.FirstName, "First Name accepts special characters");
			Assert.AreEqual<string>("Tendulkar", addCustomerToQueue.FirstName, "First Name does not truncate special characters properly");

			addCustomerToQueue.MiddleInitial = "%J";
			Assert.AreNotEqual<string>("%", addCustomerToQueue.MiddleInitial, "Middle Initial accepts special character");
			Assert.AreEqual<string>("J", addCustomerToQueue.MiddleInitial, "Middile Initial does not truncate special characters properly");

			addCustomerToQueue.LastName = "Anil Kumble";
			Assert.AreEqual<string>("Anil Kumble", addCustomerToQueue.LastName, "Last Name does not accept valid names");

			addCustomerToQueue.LastName = "Ten%dulkar1";
			Assert.AreNotEqual<string>("Ten%dulkar", addCustomerToQueue.LastName, "Last Name accepts special characters");
			Assert.AreEqual<string>("Tendulkar", addCustomerToQueue.LastName, "Last Name does not truncate special characters properly");
		}

		[TestMethod]
		public void AddNewCustomerToQueueWindowClosedOnCancelClick()
		{
			addCustomerToQueue.LastName = "Last Name";
			addCustomerToQueue.Cancel();

			Assert.IsTrue(addCustomerToQueue.Closed, "Add New Customer To Queue Window is not closed");
		}

		[TestMethod]
		//[ExpectedException(typeof(WindowNotFoundException), "Add New Customer To Queue Window is displayed")]
		public void EscKeyMappedToCancelButtonInAddNewCustomerToQueueWindow()
		{
			addCustomerToQueue.LastName = "Last Name";
			addCustomerToQueue.EscPressed();

			Assert.IsTrue(addCustomerToQueue.Closed, "Add New Customer To Queue Window is not closed");
		}

		[TestMethod]
		public void QueueCustomerForServiceInAddNewCustomerToQueueWindow()
		{
			addCustomerToQueue.FirstName = "Jawagal";
			addCustomerToQueue.MiddleInitial = "V";
			addCustomerToQueue.LastName = "Srinath";
			addCustomerToQueue.Street = "123, Test St.";
			addCustomerToQueue.City = "Test City";
			addCustomerToQueue.State = "Test State";
			addCustomerToQueue.Zip = "123456";
			addCustomerToQueue.HomeNumber = "1234567890";
			addCustomerToQueue.WorkNumber = "1234567890";
			addCustomerToQueue.CellNumber = "1234567890";
			addCustomerToQueue.SSN = "123456789";
			addCustomerToQueue.EMail = "JS@testmail";
            addCustomerToQueue.ReasonForVisit = "Checking Account - Opening request";
			addCustomerToQueue.Description = "Test Visit";
			addCustomerToQueue.OkClick();

			StringCollection customersInQueue = shell.GetCustomersInQueue();
			Assert.AreEqual<int>(1, customersInQueue.Count, "There should be 1 customer in the Queue");
			Assert.AreEqual<string>("Jawagal V Srinath", customersInQueue[0], "Jawagal V Srinath should be added to the Queue");
		}

        [TestMethod]
        public void CheckCustomerSummaryDisplaysAllDetailsOfVisitor()
        {
            addCustomerToQueue.FirstName = "Sandesh";
            addCustomerToQueue.MiddleInitial = "P";
            addCustomerToQueue.LastName = "Ambekar";
            addCustomerToQueue.Street = "123, Test St.";
            addCustomerToQueue.City = "Test City";
            addCustomerToQueue.State = "Test State";
            addCustomerToQueue.Zip = "123456";
            addCustomerToQueue.HomeNumber = "1234567890";
            addCustomerToQueue.WorkNumber = "1234567890";
            addCustomerToQueue.CellNumber = "1234567890";
            addCustomerToQueue.SSN = "123456789";
            addCustomerToQueue.EMail = "SA@testmail";
            addCustomerToQueue.ReasonForVisit = "Checking Account - Opening request";
            addCustomerToQueue.Description = "Test Visit";
            addCustomerToQueue.OkClick();

            shell.Close();
            authentication = new AuthenAdapter();
            authentication.SetUserNameAndPassword("Tom", "Password2");
            shell = authentication.OkClick();
            shell.CustomerQueue();
            rightPane = shell.ServiceCustomer();
            customerSummary = rightPane.CustomerSummary();
            Assert.AreEqual<string>("Sandesh P Ambekar", customerSummary.Name,
                    "Full Name does not have the correct value");
            Assert.AreEqual<string>("Checking Account - Opening request", customerSummary.ReasonCode,
                    "Full Name does not have the correct value");
            Assert.AreEqual<string>("Test Visit", customerSummary.ReasonDescription,
                    "Full Name does not have the correct value");
            Assert.AreEqual<string>("123 Test St., Test City, Test State 12345",
                    customerSummary.Address, "Address does not have the correct value");
            Assert.AreEqual<string>("123456789", customerSummary.SSN,
                    "SSN does not have the correct number");
            Assert.AreEqual<string>("1234567890", customerSummary.HomeNumber,
                    "Home Phone does not have the correct number");
            Assert.AreEqual<string>("SA@testmail", customerSummary.Email,
                    "Email does not have the correct value");
            
        }
        [Ignore]
		[TestMethod]
		public void SelfServiceCustomerInAddNewCustomerToQueueWindow()
		{
			addCustomerToQueue.ReasonForVisit = "Checking Account - Opening request";
			addCustomerToQueue.Description = "Test Visit";
			addCustomerToQueue.SelfService();
		}
        [Ignore]
		[TestMethod]
		public void QueueForServiceContextMenuClickedInAddNewCustomerToQueueWindow()
		{
            addCustomerToQueue.ReasonForVisit = "Checking Account - Opening request";
			addCustomerToQueue.Description = "Test Visit";
			addCustomerToQueue.QueueForServiceContextMenuClick();

			StringCollection customersInQueue = shell.GetCustomersInQueue();
			Assert.AreEqual<int>(1, customersInQueue.Count, "There should be 1 customer in the Queue");
			Assert.AreEqual<string>("Kumar Abishek", customersInQueue[0], "Kumar Abhishek should be added to the Queue");
		}
        [Ignore]
		[TestMethod]
		public void SelfServiceContextMenuClickedInAddNewCustomerToQueueWindow()
		{
            addCustomerToQueue.ReasonForVisit = "Checking Account - Opening request";
			addCustomerToQueue.Description = "Test Visit";
			addCustomerToQueue.SelfService();
		}

	}
}
