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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.CompositeUI.WPF.Tests.Workspaces
{
    [TestClass]
    public class WPFSmartPartInfoFixture
    {
        // Added a TestCleanup method to deal with the fact that the code was throwing an InvalidComObjectException
        // with the information "COM object that has been separated from its underlying RCW cannot be used."
        // Fix is based on this bug logged on Connect.Microsoft.Com:
        // http://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=318333
        [TestCleanup]
        public void CleanUp()
        {
            System.Windows.Threading.Dispatcher.CurrentDispatcher.InvokeShutdown();
        }
        
        [TestMethod]
        public void CanSetTitleAndDescription()
        {
            WPFSmartPartInfo info = new WPFSmartPartInfo();

            info.Title = "Title";
            info.Description = "Description";

            Assert.AreEqual("Title", info.Title);
            Assert.AreEqual("Description", info.Description);
        }

        [TestMethod]
        public void CanConvertSmartPartInfo()
        {
            WPFSmartPartInfo info = new WPFSmartPartInfo();

            info.Title = "Title";
            info.Description = "Description";

            TabSmartPartInfo copied = WPFSmartPartInfo.ConvertTo<TabSmartPartInfo>(info);

            Assert.AreEqual("Title", copied.Title);
            Assert.AreEqual("Description", copied.Description);
        }
    }
}
