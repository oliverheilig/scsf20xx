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
using System.Collections.Generic;
using System.Text;
using BankBranchWorkBench.MAUI;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace BankBranchWorkBench.Functional.Tests
{
    [TestClass]
    public class AuthenTest
    {
        AuthenAdapter authen;
        
        [TestInitialize]
        public void InitializeApplication()
        {
            authen = new AuthenAdapter();
        }

        [TestCleanup]
        public void CloseApplication()
        {
            if (authen != null && !(authen.Closed))
            {
                authen.Close();
            }            
        }

        [TestMethod]
        public void LoginWindowIsNotResizable()
        {
            Assert.IsFalse(authen.IsResizable, "Login Window is Resizable");
        }

        [TestMethod]
        public void VerifyInitialButtonStatusForLoginWindow()
        {
            Assert.IsTrue(authen.IsOkButtonEnabled, "Ok Button is Initially Disabled");
            Assert.IsTrue(authen.IsCancelButtonEnabled, "Cancel button is Initially Disabled");
        }

        [TestMethod]
        public void AllControlsArePresentInLoginWindow()
        {
            authen.CheckPresenceOfControls();
        }

        [TestMethod]
        public void LogIntoApplicationWhenOkClickedInLoginWindow()
        {
            authen.SetUserNameAndPassword("Jerry", "Password1");
            ShellAdapter shell = authen.OkClick();
            shell.Close(); 
        }
        //These two test cases are passing in Manual Testing.
        [Ignore]
        [TestMethod]
        public void LoginWindowClosedOnCancelClick()
        {
            authen.SetUserNameAndPassword("Tom", "");
            authen.Cancel();
            Assert.IsTrue(authen.Closed, "Login Window is not closed");
        }
        [Ignore]
        [TestMethod]
        public void EscKeyMappedToCancelButtonInLoginWindow()
        {
            authen.SetUserNameAndPassword("Tom", "");
            authen.EscPressed();
            Assert.IsTrue(authen.Closed, "Login Window is not closed");
        }        
        
    }
}
