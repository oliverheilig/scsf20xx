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
using BankBranchWorkBench.Functional.Tests.TestHelperService;
using BankBranchWorkBench.MAUI;
using BankBranchWorkBench.MAUI.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankBranchWorkBench.Functional.Tests
{
	[TestClass]
	public class CustomerSummaryTest
	{
		ShellAdapter shell;
		CustomerSummaryAdapter customerSummary;
		FindCustomerAdapter findCustomer;
		FindCustomerResultsAdapter findCustomerResults;
		RightPaneAdapter rightPane;
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
			addReason = findCustomerResults.QueueForService();
            addReason.ReasonForVisit = "Checking Account - Opening request";
            addReason.Description = "Test Visit";
            addReason.OkClick();
			rightPane = shell.ServiceCustomer();
			customerSummary = rightPane.CustomerSummary();
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
		public void CheckCustomerSummaryTabPosition()
		{
			Assert.AreEqual<int>(0, customerSummary.TabPosition,
					"The Customer Summary should be the First Tab");
		}

		[TestMethod]
		public void AllControlsArePresentInCustomerSummaryTab()
		{
			customerSummary.CheckPresenceOfControls();
			//Assert.IsTrue(customerSummary.CheckAlertDataGridViewColumnHeaders());
			Assert.IsTrue(customerSummary.CheckAccountDataGridViewColumnHeaders());
			//Assert.IsTrue(customerSummary.CheckCreditCardDataGridViewColumnHeaders());
		}

		[TestMethod]
		public void CustomerSummaryTabIsReadOnly()
		{
			string previousValue = customerSummary.Name;
			customerSummary.Name = "Dravid";
			Assert.AreNotEqual<string>("Dravid", customerSummary.Name, "Name is not read-only");
			Assert.AreEqual<string>(previousValue, customerSummary.Name, "Name is not read-only");

			previousValue = customerSummary.Address;
			customerSummary.Address = "NE 40th";
			Assert.AreNotEqual<string>("NE 40th", customerSummary.Address, "Address is not read-only");
			Assert.AreEqual<string>(previousValue, customerSummary.Address, "Address is not read-only");

			previousValue = customerSummary.HomeNumber;
			customerSummary.HomeNumber = "2432213567";
			Assert.AreNotEqual<string>("2432213567", customerSummary.HomeNumber, "Home Number is not read-only");
			Assert.AreEqual<string>(previousValue, customerSummary.HomeNumber, "Home Number is not read-only");

			previousValue = customerSummary.Email;
			customerSummary.Email = "abc@abc.com";
			Assert.AreNotEqual<string>("abc@abc.com", customerSummary.Email, "Email is not read-only");
			Assert.AreEqual<string>(previousValue, customerSummary.Email, "Email is not read-only");

			previousValue = customerSummary.SSN;
			customerSummary.SSN = "243223567";
			Assert.AreNotEqual<string>("243223567", customerSummary.SSN, "SSN is not read-only");
			Assert.AreEqual<string>(previousValue, customerSummary.SSN, "SSN is not read-only");

			previousValue = customerSummary.TimeIn;
			customerSummary.TimeIn = "9:30";
			Assert.AreNotEqual<string>("9:30", customerSummary.TimeIn, "Time In is not read-only");
			Assert.AreEqual<string>(previousValue, customerSummary.TimeIn, "Time In is not read-only");

			previousValue = customerSummary.ReasonCode;
			customerSummary.ReasonCode = "243223567";
			Assert.AreNotEqual<string>("243223567", customerSummary.ReasonCode, "Reason Code is not read-only");
			Assert.AreEqual<string>(previousValue, customerSummary.ReasonCode, "Reason Code is not read-only");

			previousValue = customerSummary.ReasonDescription;
			customerSummary.ReasonDescription = "243223567";
			Assert.AreNotEqual<string>("243223567", customerSummary.ReasonDescription,
					"Reason Description is not read-only");
			Assert.AreEqual<string>(previousValue, customerSummary.ReasonDescription,
					"Reason Description is not read-only");

			previousValue = customerSummary.Status;
			customerSummary.Status = "Status";
			Assert.AreNotEqual<string>("Status", customerSummary.Status, "Status is not read-only");
			Assert.AreEqual<string>(previousValue, customerSummary.Status, "Status is not read-only");
		}

		[TestMethod]
		public void CustomerSummaryDisplayed()
		{
            Assert.AreEqual<string>("Kari  Hensien", customerSummary.Name,
					"Full Name does not have the correct value");
            Assert.AreEqual<string>("14250 S.E. Newport Way, Bellevue, WA 98006",
					customerSummary.Address, "Address does not have the correct value");
			Assert.AreEqual<string>("222222222", customerSummary.SSN,
					"SSN does not have the correct number");
			Assert.AreEqual<string>("4444444444", customerSummary.HomeNumber,
					"Home Phone does not have the correct number");
            Assert.AreEqual<string>("kari@contoso.com", customerSummary.Email,
					"Email does not have the correct value");

			CheckAccountSummary();
			CheckCreditCardSummary();
			CheckAlertSummary();
		}        

		private void CheckAlertSummary()
		{
			AlertSummary[] alerts = customerSummary.GetAlertSummary();
			Assert.AreEqual<int>(0, alerts.Length, "There should be 0 Alert listed");
            //Assert.AreEqual<string>("Cash Loan", alerts[0].AlertType,
            //        "First Alert should be 'Cash Loan'");
            //Assert.AreEqual<string>("Pre-approved Cash Loan", alerts[0].Description,
            //        "Alert Description is not correct");
            //Assert.AreEqual<DateTime>(Convert.ToDateTime("05/30/2006"), alerts[0].ExpiryDate,
            //        "Expiry Date is not correct");
		}

		private void CheckCreditCardSummary()
		{
			CreditCardSummary[] creditCards = customerSummary.GetCreditCardSummary();
			Assert.AreEqual<int>(0, creditCards.Length, "There should be 0 card listed");
        //    Assert.AreEqual<string>("Gold", creditCards[0].CreditCardType,
        //            "First Card should be Gold");
        //    Assert.AreEqual<string>("2345435412341212", creditCards[0].CreditCardNumber,
        //            "Card Number is not correct");
        //    //Assert.AreEqual<string>(5300, creditCards[0].PaymentDueDate,
        //    //    "Balance amount is not correct");
        //    Assert.AreEqual<double>(5300, creditCards[0].Balance,
        //            "Balance amount is not correct");
        }

		private void CheckAccountSummary()
		{
			AccountSummary[] accounts = customerSummary.GetAccountSummary();
			Assert.AreEqual<int>(2, accounts.Length, "There should be 2 accounts listed");
			Assert.AreEqual<string>("Checkings", accounts[0].AccountType,
					"First Account should be Checking");
			Assert.AreEqual<string>("12332603", accounts[0].AccountNumber,
					"Account Number is not correct");
			Assert.AreEqual<double>(5500.34, accounts[0].Balance,
					"Balance amount is not correct");

			Assert.AreEqual<string>("Savings", accounts[1].AccountType,
					"Second Account should be Savings");
			Assert.AreEqual<string>("34522603", accounts[1].AccountNumber,
					"Account Number is not correct");
            Assert.AreEqual<double>(15000, accounts[1].Balance,
					"Balance amount is not correct");
		}

		private void CustomerSummaryClosedOnServiceCompletedClicked()
		{
			customerSummary.ServiceCompleted();
		}
	}
}
