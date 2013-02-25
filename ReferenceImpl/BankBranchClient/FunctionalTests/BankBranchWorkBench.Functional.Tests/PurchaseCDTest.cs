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
using BankBranchWorkBench.MAUI.Common;
using BankBranchWorkBench.MAUI.Entities;

namespace BankBranchWorkBench.Functional.Tests
{
    [TestClass]
    public class PurchaseCDTest
    {
        ShellAdapter shell;        
        PurchaseCDAdapter purchaseCD;
        FunctionalTestHelper testHelper;
        RightPaneAdapter rightPane;
        FindCustomerAdapter findCustomer;
        FindCustomerResultsAdapter findCustomerResults;
        CustomerSummaryAdapter customerSummary;
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
            purchaseCD = rightPane.PurchaseCD();           
        }

        [TestCleanup]
        public void CloseApplication()
        {     
            
            //if (findCustomerResults != null && !findCustomerResults.Closed)
            //{
            //    findCustomerResults.Close();
            //}
            //if (purchaseCD != null && !purchaseCD.Closed)
            //{
            //    purchaseCD.Cancel();
            //}
            //if (findCustomer != null && !findCustomer.Closed)
            //{
            //    findCustomer.Close();
            //}
            if (shell != null && !(shell.Closed))
            {
                shell.Close();
            }
        }
		
		/// <summary>
        /// Test Case : Check Purchase CD screen is closed on "Cancel" button click.
		/// </summary>
		[TestMethod]
		public void PurchaseCDScreenClosedOnCancelClick()
		{   
            
		    purchaseCD.Cancel();

		    Assert.IsTrue(purchaseCD.Closed, "Purchase CD Screen is not closed");
		}

        /// <summary>
        /// Test Case : Check Purchase CD tab is listed in the middle pane when clicked on "Purchase CD" link
        /// </summary>
        [TestMethod]
        public void CheckPurchaseCDTabPosition()
        { 
            
            Assert.AreEqual<int>(2, purchaseCD.TabPosition,
                    "The Purchase CD should be the Second Tab");
        }

        /// <summary>
        /// Test Case : Check all controls are present on the Purchase CD screen
        /// </summary>
        [TestMethod]
        public void AllControlsArePresentInPurchaseCDTab()
        {
            purchaseCD.CheckPresenceOfControls();
            Assert.IsTrue(purchaseCD.CheckRatesTableDataGridViewColumnHeaders());            
        }        

        /// <summary>
        /// Test Case : Check whether all the accounts of the customer is displayed in the Account ComboBox
        /// </summary>
        [TestMethod]
        public void CheckAccountComboBoxDisplaysValidValues()
        {
            Assert.IsTrue(CheckAccountComboBoxValues(1));            
        }
         
        /// <summary>
        /// Test Case : Check "GetRate" functionality. The Rate should be displayed in the Calculated RateText box and in the Rates Table 
        /// </summary>
        [TestMethod]
        public void GetRateFunctionality()
        {
            //Scenario 1 : Duration - 0 Amount - 0 Expected Calculated Rate - 0.00%
            purchaseCD.DurationTextBox = "0";
            purchaseCD.AmountTextBox = "0";
            purchaseCD.GetRate();
            Assert.AreEqual("0.00%", purchaseCD.CalculatedRateTextBox, "Calculated Rate Value is not proper : Duration - 0 Amount - 0 Expected Calculated Rate - 0.00%");


            //Scenario 2 : Duration - 0 Amount - 100 Expected Calculated Rate - 0.00%
            purchaseCD.DurationTextBox = "0";
            purchaseCD.AmountTextBox = "100";
            purchaseCD.GetRate();
            Assert.AreEqual("0.00%", purchaseCD.CalculatedRateTextBox, "Calculated Rate Value is not proper : Duration - 0 Amount - 100 Expected Calculated Rate - 0.00%");
            
            //Scenario 3 : Duration - 91 Amount - 9999 Expected Calculated Rate - 1.99%
            purchaseCD.DurationTextBox = "91";
            purchaseCD.AmountTextBox = "9999";
            purchaseCD.GetRate();
            Assert.AreEqual("1.99%", purchaseCD.CalculatedRateTextBox, "Calculated Rate Value is not proper : Duration - 91 Amount - 9999 Expected Calculated Rate - 1.99%");
           
            //Scenario 4 : Duration - 149 Amount - 10000 Expected Calculated Rate - 3.21%
            purchaseCD.DurationTextBox = "149";
            purchaseCD.AmountTextBox = "10000";
            purchaseCD.GetRate();
            Assert.AreEqual("3.21%", purchaseCD.CalculatedRateTextBox, "Calculated Rate Value is not proper : Duration - 149 Amount - 10000 Expected Calculated Rate - 3.21%");

            //Scenario 5 : Duration - 149 Amount - 50000 Expected Calculated Rate - 3.21%
            purchaseCD.DurationTextBox = "149";
            purchaseCD.AmountTextBox = "50000";
            purchaseCD.GetRate();
            Assert.AreEqual("3.21%", purchaseCD.CalculatedRateTextBox, "Calculated Rate Value is not proper : Duration - 149 Amount - 50000 Expected Calculated Rate - 3.21%");

            //Scenario 6 : Duration - 149 Amount - 100000 Expected Calculated Rate - 0.00%
            purchaseCD.DurationTextBox = "149";
            purchaseCD.AmountTextBox = "100000";
            purchaseCD.GetRate();
            Assert.AreEqual("0.00%", purchaseCD.CalculatedRateTextBox, "Calculated Rate Value is not proper : Duration - 149 Amount - 100000 Expected Calculated Rate - 0.00%");

            //Scenario 7 : Duration - 150 Amount - 1000 Expected Calculated Rate - 1.99%
            purchaseCD.DurationTextBox = "150";
            purchaseCD.AmountTextBox = "1000";
            purchaseCD.GetRate();
            Assert.AreEqual("1.99%", purchaseCD.CalculatedRateTextBox, "Calculated Rate Value is not proper : Duration - 150 Amount - 1000 Expected Calculated Rate - 1.99%");

            //Scenario 8 : Duration - 150 Amount - 25000 Expected Calculated Rate - 3.21%
            purchaseCD.DurationTextBox = "150";
            purchaseCD.AmountTextBox = "25000";
            purchaseCD.GetRate();
            Assert.AreEqual("3.21%", purchaseCD.CalculatedRateTextBox, "Calculated Rate Value is not proper : Duration - 150 Amount - 25000 Expected Calculated Rate - 3.21%");

            //Scenario 9 : Duration - 185 Amount - 9999 Expected Calculated Rate - 2.14%
            purchaseCD.DurationTextBox = "185";
            purchaseCD.AmountTextBox = "9999";
            purchaseCD.GetRate();
            Assert.AreEqual("2.14%", purchaseCD.CalculatedRateTextBox, "Calculated Rate Value is not proper : Duration - 185 Amount - 9999 Expected Calculated Rate - 2.14%");

            //Scenario 10 : Duration - 239 Amount - 49999 Expected Calculated Rate - 3.38%
            purchaseCD.DurationTextBox = "239";
            purchaseCD.AmountTextBox = "49999";
            purchaseCD.GetRate();
            Assert.AreEqual("3.38%", purchaseCD.CalculatedRateTextBox, "Calculated Rate Value is not proper : Duration - 239 Amount - 49999 Expected Calculated Rate - 3.38%");

            //Scenario 11 : Duration - 240 Amount - 9999 Expected Calculated Rate - 2.24%
            purchaseCD.DurationTextBox = "240";
            purchaseCD.AmountTextBox = "9999";
            purchaseCD.GetRate();
            Assert.AreEqual("2.24%", purchaseCD.CalculatedRateTextBox, "Calculated Rate Value is not proper : Duration - 240 Amount - 9999 Expected Calculated Rate - 2.24%");

            //Scenario 12 : Duration - 299 Amount - 99999 Expected Calculated Rate - 3.58%
            purchaseCD.DurationTextBox = "299";
            purchaseCD.AmountTextBox = "99999";
            purchaseCD.GetRate();
            Assert.AreEqual("3.58%", purchaseCD.CalculatedRateTextBox, "Calculated Rate Value is not proper : Duration - 299 Amount - 99999 Expected Calculated Rate - 3.58%");

            //Scenario 13 : Duration - 300 Amount - 9999 Expected Calculated Rate - 2.96%
            purchaseCD.DurationTextBox = "300";
            purchaseCD.AmountTextBox = "9999";
            purchaseCD.GetRate();
            Assert.AreEqual("2.96%", purchaseCD.CalculatedRateTextBox, "Calculated Rate Value is not proper : Duration - 300 Amount - 9999 Expected Calculated Rate - 2.96%");

            //Scenario 14 : Duration - 599 Amount - 50000 Expected Calculated Rate - 3.63%
            purchaseCD.DurationTextBox = "599";
            purchaseCD.AmountTextBox = "50000";
            purchaseCD.GetRate();
            Assert.AreEqual("3.63%", purchaseCD.CalculatedRateTextBox, "Calculated Rate Value is not proper : Duration - 599 Amount - 50000 Expected Calculated Rate - 3.63%");

            //Scenario 15 : Duration - 600 Amount - 2000 Expected Calculated Rate - 2.96%
            purchaseCD.DurationTextBox = "600";
            purchaseCD.AmountTextBox = "2000";
            purchaseCD.GetRate();
            Assert.AreEqual("2.96%", purchaseCD.CalculatedRateTextBox, "Calculated Rate Value is not proper : Duration - 600 Amount - 2000 Expected Calculated Rate - 2.96%");
    
            //Scenario 16 : Duration - 2147483648 Amount - 1100000 Expected Calculated Rate - 0.00%
            purchaseCD.DurationTextBox = "2147483648";
            purchaseCD.AmountTextBox = "1100000";
            purchaseCD.GetRate();
            Assert.AreEqual("0.00%", purchaseCD.CalculatedRateTextBox, "Calculated Rate Value is not proper : Duration - 2147483648 Amount - 1100000 Expected Calculated Rate - 0.00%");

            //Scenario 17 : Duration - 2147483647 Amount - 99999 Expected Calculated Rate - 3.73%
            purchaseCD.DurationTextBox = "2147483647";
            purchaseCD.AmountTextBox = "99999";
            purchaseCD.GetRate();
            Assert.AreEqual("3.73%", purchaseCD.CalculatedRateTextBox, "Calculated Rate Value is not proper : Duration - 2147483647 Amount - 99999 Expected Calculated Rate - 3.73%");
            
            //Scenario 18 : Duration - Empty Amount - Empty Expected Calculated Rate - 0.00%
            purchaseCD.DurationTextBox = "";
            purchaseCD.AmountTextBox = "";
            purchaseCD.GetRate();
            Assert.AreEqual("3.73%", purchaseCD.CalculatedRateTextBox, "Calculated Rate Value is not proper : Duration - Empty Amount - Empty Expected Get Rate Button should be disabled");
        }
        
        /// <summary>
        /// Test Case : Check  Manager Bump up check when entered amount is greater than 10K
        /// </summary>
        [TestMethod]
        public void CheckManagerBumpUpCheckWhenAmtExceeds10K()
        {
            purchaseCD.DurationTextBox = "150";
            purchaseCD.AmountTextBox = "10500";
            purchaseCD.Purchase();
            
            Assert.IsTrue(purchaseCD.IsUserNotAuthorizedMessageDisplayed(), "UserNotAuthorized Message is not displayed");            

        }

        /// <summary>
        /// Test Case : Check CD gets added in Customer Product on Approval of Branch Manager
        /// </summary>
       
        [TestMethod]
        public void CheckCDGetListedOnApprovalOfManager()
        {
            purchaseCD.DurationTextBox = "150";
            purchaseCD.AmountTextBox = "10500";
            purchaseCD.SetAccountComboBoxValue("Savings[34522603] $ 15000");
            purchaseCD.Purchase();
            authentication = purchaseCD.ManagerApprovalLinkClick();
            authentication.SetUserNameAndPassword("Spike", "Password3");
            authentication.OkClick();           
           
            IsCDListedInCustomerProduct(10500); 
        }

        /// <summary>
        /// Test Case : Check CD doesn't get added in Customer Product on Rejection of Branch Manager
        /// </summary>

        [TestMethod]
        public void CheckCDNotListedOnRejectionOfManager()
        {
            purchaseCD.DurationTextBox = "150";
            purchaseCD.AmountTextBox = "10500";
            purchaseCD.SetAccountComboBoxValue("Savings[34522603] $ 15000");
            purchaseCD.Purchase();
            authentication = purchaseCD.ManagerApprovalLinkClick();
            authentication.Cancel();           
            customerSummary.SummaryTabClick();
            IsCDNotListedInCustomerProduct(); 
        }

        /// <summary>
        /// Test Case : Check CD doesn't get added in Customer Product for Greeter approval in case of amount exceeds 10k
        /// </summary>

        [TestMethod]
        public void CheckCDNotListedOnGreeterApproval()
        {
            purchaseCD.DurationTextBox = "150";
            purchaseCD.AmountTextBox = "10500";
            purchaseCD.SetAccountComboBoxValue("Savings[34522603] $ 15000");
            purchaseCD.Purchase();
            authentication = purchaseCD.ManagerApprovalLinkClick();
            authentication.SetUserNameAndPassword("Jerry", "Password1");
            authentication.OkClick();        
            customerSummary.SummaryTabClick();
            IsCDNotListedInCustomerProduct();
        }

        /// <summary>
        /// Test Case : Check CD doesn't get added in Customer Product for Officer approval in case of amount exceeds 10k
        /// </summary>

        [TestMethod]
        public void CheckCDNotListedOnOfficerApproval()
        {
            purchaseCD.DurationTextBox = "150";
            purchaseCD.AmountTextBox = "10500";
            purchaseCD.SetAccountComboBoxValue("Savings[34522603] $ 15000");
            purchaseCD.Purchase();
            authentication = purchaseCD.ManagerApprovalLinkClick();
            authentication.SetUserNameAndPassword("Tom", "Password2");
            authentication.OkClick();
            customerSummary.SummaryTabClick();
            IsCDNotListedInCustomerProduct();
        }
        /// <summary>
        /// Test Case : Check  InSufficientFund Message is displayed when entered amount is greater than Selected Account Balance
        /// </summary>
        [TestMethod]
        public void CheckInSufficientFundMessage()
        {
            purchaseCD.DurationTextBox = "150";
            purchaseCD.AmountTextBox = "6500";
            purchaseCD.Purchase();
            
            Assert.IsTrue(purchaseCD.IsInSufficientFundMessageDisplayed(), "InSufficientFund Message is not displayed");

        }

        /// <summary>
        /// Test Case : Check CD get listed in Customer Product
        /// </summary>
        
        [TestMethod]
        public void CheckCDListedInCustomerProduct()
        {
            purchaseCD.DurationTextBox = "150";
            purchaseCD.AmountTextBox = "100";
            purchaseCD.Purchase();           
            IsCDListedInCustomerProduct(100);  
        }

        /// <summary>
        /// Test Case : Check Account balance is consistent and its shown properly in Account combobox alongwith AccountNo 
        ///             after done purchasement of CD.    
        /// </summary>

        [TestMethod]
        public void CheckAccountValueAfterPurchasingCD()
        {
            purchaseCD.DurationTextBox = "150";
            purchaseCD.AmountTextBox = "100";
            purchaseCD.Purchase();
            customerSummary.ServiceCompleted();
            //check again the account combobox
            findCustomer = shell.FindCustomer();
            findCustomer.FirstName = "Kari";
            findCustomerResults = findCustomer.Find();
            addReason = findCustomerResults.QueueForService();
            addReason.ReasonForVisit = "Checking Account - Opening request";
            addReason.Description = "Test Visit";
            addReason.OkClick();
            rightPane = shell.ServiceCustomer();

            customerSummary = rightPane.CustomerSummary();
            purchaseCD = rightPane.PurchaseCD();
            Assert.IsTrue(CheckAccountComboBoxValues(2));
        }

        /// <summary>
        /// Test Case : Check CD get listed in Customer Product and Fund Transfer is consistent
        /// </summary>

        [TestMethod]
        public void CheckFundTransferIsConsistent()
        {
            purchaseCD.DurationTextBox = "150";
            purchaseCD.AmountTextBox = "100";
            purchaseCD.Purchase();
            IsFundTransferConsistent();
        }

        /// <summary>
        /// Test Case : Check Purchase CD functionality for Empty Amount and Duration
        /// </summary>
        [TestMethod]
        public void CheckPurchaseFunctionalityForEmptyAmount()
        {            
            purchaseCD.Purchase();
            customerSummary.SummaryTabClick();
            IsCDNotListedInCustomerProduct(); 
        }

        /// <summary>
        /// Test Case : Check Purchase CD functionality for 0 Amount value
        /// </summary>
        [TestMethod]
        public void CheckCDNotListedForAmountZero()
        {
            purchaseCD.DurationTextBox = "150";
            purchaseCD.AmountTextBox = "0";
            purchaseCD.Purchase();
            customerSummary.SummaryTabClick();
            IsCDNotListedInCustomerProduct(); 
        }

        private void IsCDListedInCustomerProduct(double CDCost)
        {
            AccountSummary[] accounts = customerSummary.GetAccountSummary();
            Assert.AreEqual<int>(3, accounts.Length, "There should be 3 accounts listed");

            Assert.AreEqual<string>("Checkings", accounts[0].AccountType,
                     "First Account should be Checking");
            Assert.AreEqual<string>("12332603", accounts[0].AccountNumber,
                     "Account Number is not correct");

            if (accounts[1].AccountType.Equals("Savings"))
            {
                Assert.AreEqual<string>("Savings", accounts[1].AccountType,
                        "Second Account should be Savings");
                Assert.AreEqual<string>("34522603", accounts[1].AccountNumber,
                        "Account Number is not correct");                

                Assert.AreEqual<string>("CD", accounts[2].AccountType,
                         "Third Account should be CD");               
            }
            else
            {
                Assert.AreEqual<string>("Savings", accounts[2].AccountType,
                        "Third Account should be Savings");
                Assert.AreEqual<string>("34522603", accounts[2].AccountNumber,
                        "Account Number is not correct");               

                Assert.AreEqual<string>("CD", accounts[1].AccountType,
                         "Second Account should be CD");                
            }

        }

        private void IsCDNotListedInCustomerProduct()
        {
            AccountSummary[] accounts = customerSummary.GetAccountSummary();
            Assert.AreEqual<int>(2, accounts.Length, "There should be 2 accounts listed");

            Assert.AreEqual<string>("Checkings", accounts[0].AccountType,
                    "First Account should be Checking");
            Assert.AreEqual<string>("12332603", accounts[0].AccountNumber,
                    "Account Number is not correct");

            Assert.AreEqual<string>("Savings", accounts[1].AccountType,
                    "Second Account should be Savings");
            Assert.AreEqual<string>("34522603", accounts[1].AccountNumber,
                    "Account Number is not correct");

            Assert.AreNotEqual<string>("CD", accounts[0].AccountType,
                     "CD should not be listed in the list");
            Assert.AreNotEqual<string>("CD", accounts[1].AccountType,
                     "CD should not be listed in the list");
        }

        private void IsFundTransferConsistent()
        {
            AccountSummary[] accounts = customerSummary.GetAccountSummary();
            Assert.AreEqual<int>(3, accounts.Length, "There should be 3 accounts listed");

            Assert.AreEqual<string>("Checkings", accounts[0].AccountType,
                    "First Account should be Checking");
            Assert.AreEqual<string>("12332603", accounts[0].AccountNumber,
                    "Account Number is not correct");
            Assert.AreEqual<double>(5400.34, accounts[0].Balance,
                    "Checkings 12332603 - Balance is wrong");
            if(accounts[1].AccountType.Equals("Savings"))
            {
                Assert.AreEqual<string>("Savings", accounts[1].AccountType,
                        "Second Account should be Savings");
                Assert.AreEqual<string>("34522603", accounts[1].AccountNumber,
                        "Account Number is not correct");
                Assert.AreEqual<double>(15000, accounts[1].Balance,
                        "Savings 34522603 - Balance is wrong");

                Assert.AreEqual<string>("CD", accounts[2].AccountType,
                         "Third Account should be CD");
                Assert.AreEqual<double>(100, accounts[2].Balance,
                        "CD - Balance is wrong");
            }
            else
            {
                Assert.AreEqual<string>("Savings", accounts[2].AccountType,
                        "Third Account should be Savings");
                Assert.AreEqual<string>("34522603", accounts[2].AccountNumber,
                        "Account Number is not correct");
                Assert.AreEqual<double>(15000, accounts[2].Balance,
                        "Savings 34522603 - Balance is wrong");

                Assert.AreEqual<string>("CD", accounts[1].AccountType,
                         "Second Account should be CD");
                Assert.AreEqual<double>(100, accounts[1].Balance,
                        "CD - Balance is wrong");
            }

        }

        private bool CheckAccountComboBoxValues(int scenario)
        {
            if (scenario != 1)
            {
                return ((purchaseCD.GetAccountComboBoxValueByIndex(0).Equals("Checkings[12332603] $ 5400.34")) &&
                    (purchaseCD.GetAccountComboBoxValueByIndex(1).Equals("Savings[34522603] $ 15000")));
            }
            else
            {
                return ((purchaseCD.GetAccountComboBoxValueByIndex(0).Equals("Checkings[12332603] $ 5500.34")) &&
                    (purchaseCD.GetAccountComboBoxValueByIndex(1).Equals("Savings[34522603] $ 15000")));
            }

        }
    }
}
