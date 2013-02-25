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
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankBranchWorkBench.MAUI;
using BankBranchWorkBench.MAUI.Common;
using BankBranchWorkBench.MAUI.Entities;
using BankBranchWorkBench.Functional.Tests.TestHelperService;

namespace BankBranchWorkBench.Functional.Tests
{

	[TestClass]
	public class FindCustomerTest
	{
		ShellAdapter shell;
		FindCustomerAdapter findCustomer;
		FindCustomerResultsAdapter findCustomerResult;
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
		}

		[TestCleanup]
		public void CloseApplication()
		{
            //if (findCustomerResult != null && !findCustomerResult.Closed)
            //{
            //    findCustomerResult.Close();
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
		public void ClickingFindCustomerOpenAsModalWindow()
		{
			Assert.IsTrue(findCustomer.IsModal, "Find Customer Window is not Modal");
		}

		[TestMethod]
		public void FindCustomerWindowIsNotResizable()
		{
			Assert.IsFalse(findCustomer.IsResizable, "Find Customer Window is Resizable");
		}

		[TestMethod]
		public void AllControlsArePresentInFindCustomerScreen()
		{
			findCustomer.CheckPresenceOfControls();
		}

		[TestMethod]
		public void SearchFieldsAreInitiallyBlank()
		{
			Assert.IsTrue(findCustomer.FirstName == null || findCustomer.FirstName == String.Empty, "First Name is not Empty");
			Assert.IsTrue(findCustomer.LastName == null || findCustomer.LastName == String.Empty, "Last Name is not Empty");
			Assert.IsTrue(findCustomer.MiddleInitial == null || findCustomer.MiddleInitial == String.Empty, "Middle Name is not Empty");
			Assert.IsTrue(findCustomer.Street == null || findCustomer.Street == String.Empty, "Street is not Empty");
			Assert.IsTrue(findCustomer.City == null || findCustomer.City == String.Empty, "City is not Empty");
			Assert.IsTrue(findCustomer.State == null || findCustomer.State == String.Empty, "State is not Empty");
			Assert.IsTrue(findCustomer.Zip == null || findCustomer.Zip == String.Empty, "Zip is not Empty");
			Assert.IsTrue(findCustomer.HomeNumber == null || findCustomer.HomeNumber == String.Empty, "Home Number is not Empty");
			Assert.IsTrue(findCustomer.WorkNumber == null || findCustomer.WorkNumber == String.Empty, "Work Number is not Empty");
			Assert.IsTrue(findCustomer.CellNumber == null || findCustomer.CellNumber == String.Empty, "Cell Number is not Empty");
			Assert.IsTrue(findCustomer.SSN == null || findCustomer.SSN == String.Empty, "SSN is not Empty");
			Assert.IsTrue(findCustomer.EMail == null || findCustomer.EMail == String.Empty, "Email is not Empty");
		}

		[TestMethod]
		public void VerifyInitialButtonStatus()
		{
			Assert.IsFalse(findCustomer.IsFindButtonEnabled, "Find Button is Initially Enabled");
			Assert.IsTrue(findCustomer.IsCancelButtonEnabled, "Cancel button is Initially Disabled");
		}

		[TestMethod]
		public void VerfiryButtonStatusChanges()
		{
			findCustomer.FirstName = "First Name";
			Assert.IsTrue(findCustomer.IsFindButtonEnabled, "Find Button is Disabled when First name is not empty");
			Assert.IsTrue(findCustomer.IsCancelButtonEnabled, "Cancel button is Disabled");

			findCustomer.Clear();
			Assert.IsFalse(findCustomer.IsFindButtonEnabled, "Find Button is Enabled when the Form is cleared");
			Assert.IsTrue(findCustomer.IsCancelButtonEnabled, "Cancel button is Disabled");

			findCustomer.LastName = "Last Name";
			Assert.IsTrue(findCustomer.IsFindButtonEnabled, "Find Button is Disabled when Last name is not empty");
			Assert.IsTrue(findCustomer.IsCancelButtonEnabled, "Cancel button is Disabled");

			findCustomer.Clear();
			Assert.IsFalse(findCustomer.IsFindButtonEnabled, "Find Button is Enabled when the Form is cleared");
			Assert.IsTrue(findCustomer.IsCancelButtonEnabled, "Cancel button is Disabled");

			findCustomer.MiddleInitial = "M";
			Assert.IsTrue(findCustomer.IsFindButtonEnabled, "Find Button is Disabled when Middle Initial is not empty");
			Assert.IsTrue(findCustomer.IsCancelButtonEnabled, "Cancel button is Disabled");

			findCustomer.Clear();
			Assert.IsFalse(findCustomer.IsFindButtonEnabled, "Find Button is Enabled when the Form is cleared");
			Assert.IsTrue(findCustomer.IsCancelButtonEnabled, "Cancel button is Disabled");

			findCustomer.Street = "234 24th Street";
			Assert.IsTrue(findCustomer.IsFindButtonEnabled, "Find Button is Disabled when Street is not empty");
			Assert.IsTrue(findCustomer.IsCancelButtonEnabled, "Cancel button is Disabled");

			findCustomer.Clear();
			Assert.IsFalse(findCustomer.IsFindButtonEnabled, "Find Button is Enabled when the Form is cleared");
			Assert.IsTrue(findCustomer.IsCancelButtonEnabled, "Cancel button is Disabled");

			findCustomer.City = "Bellevue";
			Assert.IsTrue(findCustomer.IsFindButtonEnabled, "Find Button is Disabled when City is not empty");
			Assert.IsTrue(findCustomer.IsCancelButtonEnabled, "Cancel button is Disabled");

			findCustomer.Clear();
			Assert.IsFalse(findCustomer.IsFindButtonEnabled, "Find Button is Enabled when the Form is cleared");
			Assert.IsTrue(findCustomer.IsCancelButtonEnabled, "Cancel button is Disabled");

			findCustomer.State = "WA";
			Assert.IsTrue(findCustomer.IsFindButtonEnabled, "Find Button is Disabled when State is not empty");
			Assert.IsTrue(findCustomer.IsCancelButtonEnabled, "Cancel button is Disabled");

			findCustomer.Clear();
			Assert.IsFalse(findCustomer.IsFindButtonEnabled, "Find Button is Enabled when the Form is cleared");
			Assert.IsTrue(findCustomer.IsCancelButtonEnabled, "Cancel button is Disabled");

			findCustomer.Zip = "98007";
			Assert.IsTrue(findCustomer.IsFindButtonEnabled, "Find Button is Disabled when Zip is not empty");
			Assert.IsTrue(findCustomer.IsCancelButtonEnabled, "Cancel button is Disabled");

			findCustomer.Clear();
			Assert.IsFalse(findCustomer.IsFindButtonEnabled, "Find Button is Enabled when the Form is cleared");
			Assert.IsTrue(findCustomer.IsCancelButtonEnabled, "Cancel button is Disabled");

			findCustomer.HomeNumber = "1103456321";
			Assert.IsTrue(findCustomer.IsFindButtonEnabled, "Find Button is Disabled when Home Number is not empty");
			Assert.IsTrue(findCustomer.IsCancelButtonEnabled, "Cancel button is Disabled");

			findCustomer.Clear();
			Assert.IsFalse(findCustomer.IsFindButtonEnabled, "Find Button is Enabled when the Form is cleared");
			Assert.IsTrue(findCustomer.IsCancelButtonEnabled, "Cancel button is Disabled");

			findCustomer.WorkNumber = "1103456321";
			Assert.IsTrue(findCustomer.IsFindButtonEnabled, "Find Button is Disabled when Work Number is not empty");
			Assert.IsTrue(findCustomer.IsCancelButtonEnabled, "Cancel button is Disabled");

			findCustomer.Clear();
			Assert.IsFalse(findCustomer.IsFindButtonEnabled, "Find Button is Enabled when the Form is cleared");
			Assert.IsTrue(findCustomer.IsCancelButtonEnabled, "Cancel button is Disabled");

			findCustomer.CellNumber = "1103456321";
			Assert.IsTrue(findCustomer.IsFindButtonEnabled, "Find Button is Disabled when Cell Number is not empty");
			Assert.IsTrue(findCustomer.IsCancelButtonEnabled, "Cancel button is Disabled");

			findCustomer.Clear();
			Assert.IsFalse(findCustomer.IsFindButtonEnabled, "Find Button is Enabled when the Form is cleared");
			Assert.IsTrue(findCustomer.IsCancelButtonEnabled, "Cancel button is Disabled");

			findCustomer.SSN = "111111111";
			Assert.IsTrue(findCustomer.IsFindButtonEnabled, "Find Button is Disabled when SSN is not empty");
			Assert.IsTrue(findCustomer.IsCancelButtonEnabled, "Cancel button is Disabled");

			findCustomer.Clear();
			Assert.IsFalse(findCustomer.IsFindButtonEnabled, "Find Button is Enabled when the Form is cleared");
			Assert.IsTrue(findCustomer.IsCancelButtonEnabled, "Cancel button is Disabled");

			findCustomer.EMail = "a@a.com";
			Assert.IsTrue(findCustomer.IsFindButtonEnabled, "Find Button is Disabled when Email is not empty");
			Assert.IsTrue(findCustomer.IsCancelButtonEnabled, "Cancel button is Disabled");

			findCustomer.Clear();
			Assert.IsFalse(findCustomer.IsFindButtonEnabled, "Find Button is Enabled when the Form is cleared");
			Assert.IsTrue(findCustomer.IsCancelButtonEnabled, "Cancel button is Disabled");
		}

		[TestMethod]
		public void AllFieldsTruncateToMaxLength()
		{
			findCustomer.FirstName = "ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLM";
			Assert.AreNotEqual<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLM",
				findCustomer.FirstName, "First Name accepts more than 64 characters");
			Assert.AreEqual<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKL",
				findCustomer.FirstName, "First Name not truncated to the first 64 characters entered");

			findCustomer.MiddleInitial = "AB";
			Assert.AreNotEqual<string>("AB", findCustomer.MiddleInitial,
				"Middle Intital accepts more than 1 character");
			Assert.AreEqual<string>("A", findCustomer.MiddleInitial,
				"Middle Intital not truncated to the first character entered");

			findCustomer.LastName = "ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLM";
			Assert.AreNotEqual<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLM",
				findCustomer.LastName, "Last Name accepts more than 64 characters");
			Assert.AreEqual<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKL",
				findCustomer.LastName, "Last Name not truncated to the first 64 characters entered");

			findCustomer.Street = "123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789";
			Assert.AreNotEqual<string>("123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789",
				findCustomer.Street, "Street accepts more than 128 characters");
			Assert.AreEqual<string>("12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678",
				findCustomer.Street, "Street not truncated to the first 128 characters entered");

			findCustomer.City = "ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLM";
			Assert.AreNotEqual<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLM",
				findCustomer.City, "City accepts more than 64 characters");
			Assert.AreEqual<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKL",
				findCustomer.City, "City not truncated to the first 64 characters entered");

			findCustomer.State = "ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLM";
			Assert.AreNotEqual<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLM",
				findCustomer.State, "State accepts more than 64 characters");
			Assert.AreEqual<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKL",
				findCustomer.State, "State not truncated to the first 64 characters entered");

			findCustomer.Zip = "123456";
			Assert.AreNotEqual<string>("123456", findCustomer.Zip, "Zip accepts more than 5 characters");
			Assert.AreEqual<string>("12345", findCustomer.Zip,
				"Zip not truncated to the first 5 characters entered");

			findCustomer.HomeNumber = "1234567890123456789012345";
			Assert.AreNotEqual<string>("1234567890123456789012345", findCustomer.HomeNumber,
				"Home Number accepts more than 24 numberals");
			Assert.AreEqual<string>("123456789012345678901234", findCustomer.HomeNumber,
				"Home Number not truncated to the first 24 numerals entered");

			findCustomer.WorkNumber = "1234567890123456789012345";
			Assert.AreNotEqual<string>("1234567890123456789012345",
				findCustomer.WorkNumber, "Work Number accepts more than 24 numberals");
			Assert.AreEqual<string>("123456789012345678901234", findCustomer.WorkNumber,
				"Work Number not truncated to the first 24 numerals entered");

			findCustomer.CellNumber = "1234567890123456789012345";
			Assert.AreNotEqual<string>("1234567890123456789012345", findCustomer.CellNumber,
				"Cell Number accepts more than 10 numberals");
			Assert.AreEqual<string>("123456789012345678901234", findCustomer.CellNumber,
				"Cell Number not truncated to the first 24 numerals entered");

			findCustomer.SSN = "1234567890";
			Assert.AreNotEqual<string>("1234567890", findCustomer.SSN, "SSN accepts more than 9 numberals");
			Assert.AreEqual<string>("123456789", findCustomer.SSN,
				"SSN not truncated to the first 9 numerals entered");

			findCustomer.EMail = "ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLABCDEF@HIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZAB.DEFGHIJKLM";
			Assert.AreNotEqual<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLABCDEF@HIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZAB.DEFGHIJKLM",
				findCustomer.EMail, "Email accepts more than 128 numberals");
			findCustomer.EMail = "ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLABCDEF@HIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZAB.DEFGHIJKLM";
			Assert.AreEqual<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLABCDEF@HIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZAB.DEFGHIJKL",
				findCustomer.EMail, "Email not truncated to the first 128 numerals entered");
		}

		[TestMethod]
		public void NameFieldsOnlyAllowAlphabetsAndSpaces()
		{
			findCustomer.FirstName = "Anil Kumble";
			Assert.AreEqual<string>("Anil Kumble", findCustomer.FirstName, "First Name does not accept valid names");

			findCustomer.FirstName = "Ten%dulkar1";
			Assert.AreNotEqual<string>("Ten%dulkar", findCustomer.FirstName, "First Name accepts special characters");
			Assert.AreEqual<string>("Tendulkar", findCustomer.FirstName, "First Name does not truncate special characters properly");

			findCustomer.MiddleInitial = "%J";
			Assert.AreNotEqual<string>("%", findCustomer.MiddleInitial, "Middle Initial accepts special character");
			Assert.AreEqual<string>("J", findCustomer.MiddleInitial, "Middile Initial does not truncate special characters properly");

			findCustomer.LastName = "Anil Kumble";
			Assert.AreEqual<string>("Anil Kumble", findCustomer.LastName, "Last Name does not accept valid names");

			findCustomer.LastName = "Ten%dulkar1";
			Assert.AreNotEqual<string>("Ten%dulkar", findCustomer.LastName, "Last Name accepts special characters");
			Assert.AreEqual<string>("Tendulkar", findCustomer.LastName, "Last Name does not truncate special characters properly");
		}

		[TestMethod]
		public void CityAndStateOnlyAllowAlphabets()
		{
			findCustomer.City = "Bellevue";
			Assert.AreEqual<string>("Bellevue", findCustomer.City, "City does not accept valid values");

			findCustomer.City = "S@alt Lake City";
			Assert.AreNotEqual<string>("S@alt Lake City", findCustomer.City, "City accepts special characters");
			Assert.AreEqual<string>("Salt Lake City", findCustomer.City, "City does not truncate invalid characters properly");

			findCustomer.State = "WA";
			Assert.AreEqual<string>("WA", findCustomer.State, "State does not accept valid values");

			findCustomer.State = "Ne+w York";
			Assert.AreNotEqual<string>("Ne+w York", findCustomer.State, "State accepts special characters");
			Assert.AreEqual<string>("New York", findCustomer.State, "State does not truncate invalid characters properly");

		}

		[TestMethod]
		public void ZipOnlyAllowNumerals()
		{
			findCustomer.Zip = "98007";
			Assert.AreEqual<string>("98007", findCustomer.Zip, "Zip does not accept valid values");

			findCustomer.Zip = "98A4$ 35";
			Assert.AreNotEqual<string>("98A4$ 35", findCustomer.Zip, "Zip accepts special characters");
			Assert.AreEqual<string>("98435", findCustomer.Zip, "Zip does not truncate invalid characters properly");
		}

		[TestMethod]
		public void SearchResultsDisplayedOnFindClick()
		{
			findCustomer.FirstName = "Kari";
			findCustomerResult = findCustomer.Find();

			Assert.IsNotNull(findCustomerResult, "Find Customer Results Screen is not displayed");
		}

		[TestMethod]
		public void FincCustomerScreenClosedOnCancelClick()
		{
			findCustomer.LastName = "Mathew";
			findCustomer.Cancel();

			Assert.IsTrue(findCustomer.Closed, "Find Customer Screen is not closed");
		}

		[TestMethod]
		[ExpectedException(typeof(WindowNotFoundException), "Find Customer Results Screen is displayed")]
		public void SearchResultsNotDisplayedOnCancelClick()
		{
			findCustomer.LastName = "Mathew";
			findCustomer.Cancel();

			findCustomerResult = new FindCustomerResultsAdapter();
			Assert.IsNull(findCustomerResult, "Find Customer Results Screen is displayed");
		}

		[TestMethod]
		public void EnterKeyMappedToFindButton()
		{
			findCustomer.FirstName = "kari";
			findCustomerResult = findCustomer.EnterPressed();
			Assert.IsNotNull(findCustomerResult, "Find Customer Results Screen is not displayed");
		}

		[TestMethod]
		[ExpectedException(typeof(WindowNotFoundException), "Find Customer Results Screen is displayed")]
		public void EscKeyMappedToCancelButton()
		{
			findCustomer.LastName = "Abishek";
			findCustomer.EscPressed();

			findCustomerResult = new FindCustomerResultsAdapter();
			Assert.IsNull(findCustomerResult, "Find Customer Results Screen is displayed");
		}

		[TestMethod]
		public void StartWithSearchBasedOnFirstName()
		{
			findCustomer.FirstName = "Ma";
			findCustomerResult = findCustomer.Find();
			CustomerResult[] result = findCustomerResult.GetResults();

			Assert.AreEqual<int>(1, result.Length,
				"Should return 1 record for First Name starting with 'Ma'");
			Assert.AreEqual<string>("Mary", result[0].FirstName,
				"The first record in the list should be 'Mary Andersen'");
            Assert.AreEqual<string>("Andersen", result[0].LastName,
                "The first record in the list should be 'Mary Andersen'");			
		}

		[TestMethod]
		public void StartWithSearchBasedOnLastName()
		{
			findCustomer.LastName = "a";
			findCustomerResult = findCustomer.Find();
			CustomerResult[] result = findCustomerResult.GetResults();

			Assert.AreEqual<int>(1, result.Length,
				"Should return 1 record for Last Name starting with 'a'");           
            Assert.AreEqual<string>("Mary", result[0].FirstName,
                "The first record in the list should be 'Mary Andersen'");
            Assert.AreEqual<string>("Andersen", result[0].LastName,
                "The first record in the list should be 'Mary Andersen'");	
		}

		[TestMethod]
		public void StartWithSearchBasedOnStreet()
		{
			findCustomer.Street = "1";
			findCustomerResult = findCustomer.Find();
			CustomerResult[] result = findCustomerResult.GetResults();

			Assert.AreEqual<int>(2, result.Length,
				"Should return 2 record for Street starting with '1'");
            Assert.AreEqual<string>("Mary", result[0].FirstName,
                "The first record in the list should be 'Mary Andersen'");
            Assert.AreEqual<string>("Andersen", result[0].LastName,
                "The first record in the list should be 'Mary Andersen'");	
			Assert.AreEqual<string>("Kari", result[1].FirstName,
                "The second record in the list should be 'Kari Hensien'");
            Assert.AreEqual<string>("Hensien", result[1].LastName,
                "The second record in the list should be 'Kari Hensien'");
		}

		[TestMethod]
		public void ExactSearchBasedOnHomeNumber()
		{
            findCustomer.HomeNumber = "4444444444";
			findCustomerResult = findCustomer.Find();
			CustomerResult[] result = findCustomerResult.GetResults();

			Assert.AreEqual<int>(1, result.Length,
                "Should return 1 record with HomePhone as 4444444444");
            Assert.AreEqual<string>("Kari", result[0].FirstName,
                "The first record should have First Name as 'Kari'");
            Assert.AreEqual<string>("Hensien", result[0].LastName,
                "The first record should have Last Name as 'Hensien'");
		}

		[TestMethod]
		public void ExactSearchBasedOnWorkNumber()
		{
            findCustomer.WorkNumber = "2222222222";
			findCustomerResult = findCustomer.Find();
			CustomerResult[] result = findCustomerResult.GetResults();

			Assert.AreEqual<int>(1, result.Length,
				"Should return 1 record with WorkPhone as 222-222-2222");
            Assert.AreEqual<string>("Leonids", result[0].FirstName,
                "The first record in the list should be 'Leonids Paturskis'");
            Assert.AreEqual<string>("Paturskis", result[0].LastName,
                "The first record in the list should be 'Leonids Paturskis'");
		}

		[TestMethod]
		public void ExactSearchBasedOnCellNumber()
		{
            findCustomer.CellNumber = "7777777777";
			findCustomerResult = findCustomer.Find();
			CustomerResult[] result = findCustomerResult.GetResults();

			Assert.AreEqual<int>(1, result.Length,
                "Should return 1 record with Mobile Phone as 777-777-7777");
            Assert.AreEqual<string>("Mary", result[0].FirstName,
                "The first record in the list should be 'Mary Andersen'");
            Assert.AreEqual<string>("Andersen", result[0].LastName,
                "The first record in the list should be 'Mary Andersen'");
		}

		[TestMethod]
		public void ExactSearchBasedOnSSN()
		{
			findCustomer.SSN = "333333333";
			findCustomerResult = findCustomer.Find();
			CustomerResult[] result = findCustomerResult.GetResults();

			Assert.AreEqual<int>(1, result.Length,
				"Should return 1 record with SSN as 333-33-3333");
			Assert.AreEqual<string>("Mary", result[0].FirstName,
                "The first record in the list should be 'Mary Andersen'");
            Assert.AreEqual<string>("Andersen", result[0].LastName,
                "The first record in the list should be 'Mary Andersen'");
		}

		[TestMethod]
		public void ExactSearchBasedOnCity()
		{
			findCustomer.City = "Bellevue";
			findCustomerResult = findCustomer.Find();
			CustomerResult[] result = findCustomerResult.GetResults();

			Assert.AreEqual<int>(1, result.Length,
				"Should return 1 record with City as 'Bellevue'");
            Assert.AreEqual<string>("Kari", result[0].FirstName,
                "The third record in the list should be 'Kari Hensien'");
            Assert.AreEqual<string>("Hensien", result[0].LastName,
                "The third record in the list should be 'Kari Hensien'");
		}

		[TestMethod]
		public void ExactSearchBasedOnState()
		{
			findCustomer.State = "WA";
			findCustomerResult = findCustomer.Find();
			CustomerResult[] result = findCustomerResult.GetResults();

			Assert.AreEqual<int>(3, result.Length,
				"Should return 3 records with State as 'WA'");
            Assert.AreEqual<string>("Leonids", result[0].FirstName,
                "The first record in the list should be 'Leonids Paturskis'");
            Assert.AreEqual<string>("Paturskis", result[0].LastName,
                "The first record in the list should be 'Leonids Paturskis'");
            Assert.AreEqual<string>("Mary", result[1].FirstName,
                "The second record in the list should be 'Mary Andersen'");
			Assert.AreEqual<string>("Andersen", result[1].LastName,
                "The second record in the list should be 'Mary Andersen'");
			Assert.AreEqual<string>("Kari", result[2].FirstName,
                "The third record in the list should be 'Kari Hensien'");
            Assert.AreEqual<string>("Hensien", result[2].LastName,
                "The third record in the list should be 'Kari Hensien'");
		}

		[TestMethod]
		public void ExactSearchBasedOnZip()
		{
            findCustomer.Zip = "98177";
			findCustomerResult = findCustomer.Find();
			CustomerResult[] result = findCustomerResult.GetResults();

			Assert.AreEqual<int>(1, result.Length,
				"Should return 1 record with Zip as '98177'");
            Assert.AreEqual<string>("Mary", result[0].FirstName,
                "The first record in the list should be 'Mary Andersen'");
            Assert.AreEqual<string>("Andersen", result[0].LastName,
                "The first record in the list should be 'Mary Andersen'");
		}

		[TestMethod]
		public void SearchBasedOnMultipleCriteria()
		{
			findCustomer.FirstName = "Ma";
			findCustomer.LastName = "An";

			findCustomerResult = findCustomer.Find();
			CustomerResult[] result = findCustomerResult.GetResults();

			Assert.AreEqual<int>(1, result.Length,
				"Should return 1 record for First Name starting with 'Ma' and Last Name starting with 'An'");
            Assert.AreEqual<string>("Mary", result[0].FirstName,
                "The first record in the list should be 'Mary Andersen'");
            Assert.AreEqual<string>("Andersen", result[0].LastName,
                "The first record in the list should be 'Mary Andersen'");
		}

		[TestMethod]
		public void NoRecordsFoundMessageDisplayedWhenZeroResults()
		{
			Exception caughtException = null;
			findCustomerResult = null;
			findCustomer.HomeNumber = "2345461212";

			try
			{
				findCustomerResult = findCustomer.Find();
			}
			catch (Exception ex)
			{
				caughtException = ex;
			}

			Assert.IsNull(findCustomerResult, "Find Cusomer Results Screen displayed when no results are found");
			Assert.IsTrue(caughtException != null && caughtException is WindowNotFoundException, "Find Cusomer Results Screen displayed when no results are found");
			Assert.IsTrue(findCustomer.IsMessageDisplayed, "'No Results Found' message not displayed");
		}
	}
}
