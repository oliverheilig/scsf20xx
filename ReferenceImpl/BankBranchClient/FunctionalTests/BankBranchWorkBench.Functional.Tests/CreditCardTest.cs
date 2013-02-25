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
    public class CreditCardTest
    {
        ShellAdapter shell;
        CustomerSummaryAdapter customerSummary;
        FindCustomerAdapter findCustomer;
        FindCustomerResultsAdapter findCustomerResults;
        RightPaneAdapter rightPane;
        FunctionalTestHelper testHelper;
        AuthenAdapter authentication;
        AddReasonAdapter addReason;
        CreditCardAdapter creditCard;

        [TestInitialize]
        public void InitializeApplication()
        {
            testHelper = new FunctionalTestHelper();
            testHelper.ReInitializeGlobalBank();

            authentication = new AuthenAdapter();
            authentication.SetUserNameAndPassword("Tom", "Password2");
            shell = authentication.OkClick();

            findCustomer = shell.FindCustomer();
            findCustomer.FirstName = "Mary";
            findCustomerResults = findCustomer.Find();
            addReason = findCustomerResults.QueueForService();
            addReason.ReasonForVisit = "Checking Account - Opening request";
            addReason.Description = "Test Visit";
            addReason.OkClick();
            rightPane = shell.ServiceCustomer();
            customerSummary = rightPane.CustomerSummary();
            creditCard = customerSummary.CreditCardListClick();
        }
        [TestCleanup]
        public void CloseApplication()
        {
            if (shell != null && !shell.Closed)
            {
                shell.Close();
            }
        }
        /// <summary>
        /// Test Case : Check the Credit Card Tab is displayed with proper title
        /// </summary>
        [TestMethod]
        public void CreditCardTabDisplayedWithCorrectTitle()
        {           
            Assert.AreNotEqual(null, creditCard, "Credit Card Tab is not displayed");
        }

        /// <summary>
        /// Test Case : Check position of "Credit Card " tab in Customer Summary  screen
        /// </summary>
        [TestMethod]
        public void CheckCreditCardTabPosition()
        {
            Assert.AreEqual(2, creditCard.TabPosition, "Credit Card Tab should be displayed as second Tab near the Summary tab");
        }

        [TestMethod]
        public void CreditCardTabClosedOnCancelClick()
        {
            creditCard.Cancel();
            Assert.IsTrue(creditCard.Closed, "Credit Card Screen is not closed");

        }
    }
}
