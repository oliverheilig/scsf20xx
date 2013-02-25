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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.Practices.CompositeUI.WPF.Tests;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.WPF.BuilderStrategies;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI.WPF.Tests.Workspaces;
using Microsoft.Practices.CompositeUI.SmartParts;
using System.Collections.Generic;

namespace Microsoft.Practices.CompositeUI.WinForms.Tests
{
	[TestClass]
    public class WPFControlSmartPartStrategyFixture
	{
        private static System.Windows.Controls.ItemsControl control;
        private static WorkItem workItem;
        private static WPFControlSmartPartStrategy strat;
        private static MockBuilderContext context;

		[TestInitialize]
		public void Setup()
		{
            control = new System.Windows.Controls.ItemsControl();
            workItem = new TestableRootWorkItem();
            strat = new WPFControlSmartPartStrategy();
            context = new MockBuilderContext(strat);
            context.Locator.Add(new DependencyResolutionLocatorKey(typeof(WorkItem), null), workItem);
		}

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
        [Ignore] // Strategy does not add children controls when they are not framework elements.
        public void AddingControlWithPlaceholderReplacesWSP()
        {
            SmartPartPlaceholder placeholder = new SmartPartPlaceholder();
            placeholder.SmartPartName = "SP1";
            control.Items.Add(placeholder);
            MockWPFSmartPart smartPart1 = new MockWPFSmartPart();

            workItem.Items.Add(smartPart1, "SP1");
            workItem.Items.Add(control);

            Assert.AreSame(smartPart1, placeholder.SmartPart);
        }

        [TestMethod]
        public void TestSmartPartHolderHavingNoSmartPartInContainerNoOp()
        {
            SmartPartPlaceholder smartpartHolder = new SmartPartPlaceholder();
            smartpartHolder.SmartPartName = "SampleSmartPart";
            control.Items.Add(smartpartHolder);

            workItem.Items.Add(control);

            Assert.IsNull(smartpartHolder.SmartPart);
        }

        [TestMethod]
        public void RemovingControlRemovesSmartParts()
        {
            int originalCount = workItem.Items.Count;

            MockWPFSmartPart smartPart1 = new MockWPFSmartPart();
            smartPart1.Name = "SmartPart1";
            MockWPFSmartPart smartPart2 = new MockWPFSmartPart();
            smartPart2.Name = "SmartPart2";
            smartPart1.Items.Add(smartPart2);
            control.Items.Add(smartPart1);
            workItem.Items.Add(control);

            Assert.AreEqual(3, workItem.Items.Count - originalCount);

            workItem.Items.Remove(control);

            Assert.AreEqual(0, workItem.Items.Count - originalCount);
        }

        //[TestMethod]
        //[Ignore] // Workspaces are not WPF controls.
        //public void MonitorCallsRegisterWorkspace()
        //{
        //    MockControlWithWorkspace control = new MockControlWithWorkspace();

        //    workItem.Items.Add(control);

        //    Assert.AreEqual(control.Workspace, workItem.Workspaces[control.Workspace.Name]);
        //}

        //[TestMethod]
        //[Ignore] // Workspaces are not WPF controls.
        //public void WorkspacesAreRegisteredWithName()
        //{
        //    MockControlWithWorkspace mockControl = new MockControlWithWorkspace();

        //    workItem.Items.Add(mockControl);

        //    Assert.AreEqual(mockControl.Workspace, workItem.Workspaces[mockControl.Workspace.Name]);
        //}

        [TestMethod]
        [Ignore] // Strategy does not add children controls when they are not framework elements.
        public void EmptyStringNameIsReplaceWhenAdded()
        {
            TabWorkspace workspace = new TabWorkspace();
            control.Items.Add(workspace);

            workItem.Items.Add(control);

            ICollection<TabWorkspace> tabWorkSpaces = workItem.Workspaces.FindByType<TabWorkspace>();

            Assert.IsNull(workItem.Workspaces[workspace.Name]);
            Assert.IsTrue(tabWorkSpaces.Contains(workspace));
        }


        [TestMethod]
        public void StrategyShouldAddChildSmartPartsToWorkItem()
        {
            MockWPFSmartPart parentSmartPart = new MockWPFSmartPart();
            MockWPFSmartPart childSmartPart = new MockWPFSmartPart();

            parentSmartPart.Items.Add(childSmartPart);
            workItem.SmartParts.Add(parentSmartPart);

            Assert.IsTrue(workItem.SmartParts.ContainsObject(parentSmartPart));
            Assert.IsTrue(workItem.SmartParts.ContainsObject(childSmartPart));
        }

        [TestMethod]
        public void StrategyShouldAddChildSmartPartsUsingTheirName()
        {
            MockWPFSmartPart parentSmartPart = new MockWPFSmartPart();
            parentSmartPart.Name = "parentSmartPart";
            MockWPFSmartPart childSmartPart = new MockWPFSmartPart();
            childSmartPart.Name = "childSmartPart";

            parentSmartPart.Items.Add(childSmartPart);
            workItem.SmartParts.Add(parentSmartPart, "parentSmartPart");

            Assert.IsTrue(workItem.SmartParts.Contains("childSmartPart"));
            Assert.IsTrue(workItem.SmartParts.Contains("parentSmartPart"));
        }

        [SmartPart]
        public class MockWPFSmartPart : System.Windows.Controls.ItemsControl
        {
        }
    }
}