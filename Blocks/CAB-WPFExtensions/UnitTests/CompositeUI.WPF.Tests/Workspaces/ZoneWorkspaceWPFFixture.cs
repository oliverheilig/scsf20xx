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
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.SmartParts;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI.WPF.Tests;
using Microsoft.Practices.CompositeUI.WPF;
using Microsoft.Practices.CompositeUI.WPF.Tests.Workspaces;
using System.Windows;

namespace Microsoft.Practices.CompositeUI.WPF.Tests
{
	[TestClass]
	public class ZoneWorkspaceWinformsFixture
	{
		private static ZoneWorkspace workspace;
		private static Control zoneControl;
        private static MockWPFSmartPart smartPart;
		private static WorkItem workItem;

		[TestInitialize]
		public void SetUp()
		{
			workItem = new TestableRootWorkItem();
			workspace = new ZoneWorkspace();
			zoneControl = new Control();
            smartPart = new MockWPFSmartPart();

            workItem.Services.Add(typeof(IWorkItemActivationService), new SimpleWorkItemActivationService());
            workItem.Items.Add(smartPart);
			workItem.Workspaces.Add(workspace);
			ISmartPartInfo info = new ZoneSmartPartInfo("Main");
			workItem.RegisterSmartPartInfo(zoneControl, info);
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
		public void CanSetControlZoneName()
		{
			workspace.Controls.Add(zoneControl);

			workspace.SetZoneName(zoneControl, "Main");

			Assert.AreEqual(1, workspace.Zones.Count);
			Assert.AreSame(zoneControl, workspace.Zones["Main"]);
			Assert.AreEqual("Main", workspace.GetZoneName(zoneControl));
		}

		[TestMethod]
		public void CanExtendScrollableControl()
		{
			ContainerControl cc = new ContainerControl();
			workspace.Controls.Add(cc);
			SplitContainer sc = new SplitContainer();
			workspace.Controls.Add(sc);
			FlowLayoutPanel fp = new FlowLayoutPanel();
			workspace.Controls.Add(fp);

			Assert.IsTrue(((IExtenderProvider)workspace).CanExtend(cc));
			Assert.IsTrue(((IExtenderProvider)workspace).CanExtend(sc.Panel1));
			Assert.IsTrue(((IExtenderProvider)workspace).CanExtend(sc));
			Assert.IsTrue(((IExtenderProvider)workspace).CanExtend(fp));
		}

		[TestMethod]
		public void SetControlZoneNameNotDescendentNoOp()
		{
			workspace.SetZoneName(zoneControl, "Main");

			Assert.AreEqual(0, workspace.Zones.Count);
		}

		[TestMethod]
		public void RemoveControlRemovesZoneName()
		{
			workspace.Controls.Add(zoneControl);
			workspace.SetZoneName(zoneControl, "Main");

			workspace.Controls.Remove(zoneControl);

			Assert.AreEqual(0, workspace.Zones.Count);
		}

		[TestMethod]
		public void DisposeControlRemovesZoneName()
		{
			workspace.Controls.Add(zoneControl);
			workspace.SetZoneName(zoneControl, "Main");

			zoneControl.Dispose();

			Assert.AreEqual(0, workspace.Zones.Count);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ShowNoZonesThrows()
		{
			workspace.Show(smartPart);
		}


		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void ShowWithNonExistingZoneNameThrows()
		{
			Control zone = new Control();
			workspace.Controls.Add(zone);
			workspace.SetZoneName(zone, "Main");
			workspace.Show(smartPart, new ZoneSmartPartInfo("Blah"));
		}

		[TestMethod]
		public void ShowOnZoneAddsControlAndSetsVisible()
		{
            smartPart.Visibility = Visibility.Hidden;
			Control zone = new Control();
			workspace.Controls.Add(zone);
			workspace.SetZoneName(zone, "Main");

			workspace.Show(smartPart, new ZoneSmartPartInfo("Main"));

			Assert.AreEqual(1, zone.Controls.Count);
			Assert.AreEqual(Visibility.Visible, smartPart.Visibility);
		}

		[TestMethod]
		public void HideSmartPartHidesControl()
		{
			Control zone = new Control();
			workspace.Controls.Add(zone);
			workspace.SetZoneName(zone, "Main");

			workspace.Show(smartPart, new ZoneSmartPartInfo("Main"));
			workspace.Hide(smartPart);

			Assert.AreEqual(1, zone.Controls.Count);
			Assert.AreNotEqual(Visibility.Visible, smartPart.Visibility);
		}

        [TestMethod]
        public void CloseSmartPartRemovesControl()
        {
            Control zone = new Control();
            workspace.Controls.Add(zone);
            workspace.SetZoneName(zone, "Main");

            workspace.Show(smartPart, new ZoneSmartPartInfo("Main"));
            workspace.Close(smartPart);

            Assert.AreEqual(0, zone.Controls.Count);
            Assert.IsFalse(smartPart.IsDisposed);
        }

        [TestMethod]
        public void CloseSmartPartFiresClosing()
        {
            Control zone = new Control();
            bool closingcalled = false;
            workspace.Controls.Add(zone);
            workspace.SetZoneName(zone, "Main");
            workspace.SmartPartClosing += delegate { closingcalled = true; };

            workspace.Show(smartPart, new ZoneSmartPartInfo("Main"));
            workspace.Close(smartPart);

            Assert.IsTrue(closingcalled);
        }

        [TestMethod]
        public void CancelClosingDoesNotRemoveControl()
        {
            Control zone = new Control();
            workspace.Controls.Add(zone);
            workspace.SetZoneName(zone, "Main");
            workspace.SmartPartClosing += delegate(object sender, WorkspaceCancelEventArgs e) { e.Cancel = true; };

            workspace.Show(smartPart, new ZoneSmartPartInfo("Main"));
            workspace.Close(smartPart);

            Assert.AreEqual(1, zone.Controls.Count);
        }

        [TestMethod]
        public void ClosingByDisposingControlDoesNotFireClosingEvent()
        {
            bool closing = false;
            Control zone = new Control();
            workspace.Controls.Add(zone);
            workspace.SetZoneName(zone, "Main");
            workspace.SmartPartClosing += delegate { closing = true; };

            workspace.Show(smartPart, new ZoneSmartPartInfo("Main"));
            smartPart.Dispose();

            Assert.IsFalse(closing);
            Assert.AreEqual(0, workspace.SmartParts.Count);
        }

        [TestMethod]
        public void ShowNoParamsShowsInDefaultZoneDesigner()
        {
            ZoneWorkspaceForm form = new ZoneWorkspaceForm();
            workItem.Items.Add(form.Workspace);
            System.Windows.Controls.Button button = new System.Windows.Controls.Button();
            workItem.RegisterSmartPartInfo(button, new ZoneSmartPartInfo("ContentZone"));

            form.Workspace.Show(button);

            Assert.AreEqual(1, form.Workspace.Zones["ContentZone"].Controls.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowsIfHideNotShownControl()
        {
            Control sampleControl = new Control();

            workspace.Hide(sampleControl);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowsIfCloseNotShownControl()
        {
            Control sampleControl = new Control();

            workspace.Close(sampleControl);
        }

        [TestMethod]
        public void FocusingSmartPartFiresActivated()
        {
            ZoneWorkspaceForm form = CreateFormAddWorkspace();
            Control zone = new Control();
            bool activated = false;

            workspace.Controls.Add(zone);
            workspace.SetZoneName(zone, "Main");
            workspace.SmartPartActivated += delegate { activated = true; };
            form.Show();

            workspace.Show(smartPart, new ZoneSmartPartInfo("Main"));
            workspace.Zones["Main"].Focus();

            Assert.IsTrue(activated);
        }

        [TestMethod]
        public void FocusingSmartPartFiresActivatedWithSmartPart()
        {
            ZoneWorkspaceForm form = CreateFormAddWorkspace();
            Control zone = new Control();
            object argsSmartPart = null;

            workspace.Controls.Add(zone);
            workspace.SetZoneName(zone, "Main");
            workspace.SmartPartActivated +=
                delegate(object sender, WorkspaceEventArgs args) { argsSmartPart = args.SmartPart; };
            form.Show();

            workspace.Show(smartPart, new ZoneSmartPartInfo("Main"));
            workspace.Zones["Main"].Focus();

            Assert.AreEqual(smartPart, argsSmartPart);
        }

        [TestMethod]
        public void ActivatedFiresCorrectNumberOfTimes()
        {
            ZoneWorkspaceForm form = CreateFormAddWorkspace();
            Control zone = new Control();
            Control zone1 = new Control();
            MockWPFSmartPart control2 = new MockWPFSmartPart();
            workItem.Items.Add(control2);
            int activated = 0;

            AddZones(zone, zone1);
            workspace.SmartPartActivated +=
                delegate(object sender, WorkspaceEventArgs args) { activated++; };
            form.Show();

            workspace.Show(smartPart, new ZoneSmartPartInfo("Main"));
            workspace.Show(control2, new ZoneSmartPartInfo("Main1"));

            smartPart.ElementHost.Select();
            control2.ElementHost.Select();
            smartPart.ElementHost.Select();

            //Will fire five times because it fires when show is called.
            Assert.AreEqual(5, activated);
        }

        [TestMethod]
        public void ControlIsRemovedWhenSmartPartIsDisposed()
        {
            Control zone = new Control();
            workspace.Controls.Add(zone);
            workspace.SetZoneName(zone, "Main");
            workspace.Show(smartPart);
            Assert.IsTrue(workspace.Zones["Main"].Contains(smartPart.ElementHost));

            smartPart.Dispose();

            Assert.IsFalse(workspace.Zones["Main"].Contains(smartPart.ElementHost));
        }

        [TestMethod]
        public void ZoneDocksControlCorrectly()
        {
            Control zone = new Control();
            workspace.Controls.Add(zone);
            workspace.SetZoneName(zone, "Main");
            ZoneSmartPartInfo info = new ZoneSmartPartInfo();
            info.Dock = DockStyle.Fill;
            workspace.Show(smartPart, info);

            Assert.AreEqual(DockStyle.Fill, workspace.Zones["Main"].Controls[0].Dock);
        }

        [TestMethod]
        public void WorkspaceGetsRegisteredSPI()
        {
            Control zone = new Control();
            workspace.Controls.Add(zone);
            workspace.SetZoneName(zone, "Main");
            ZoneSmartPartInfo info = new ZoneSmartPartInfo();
            info.Dock = DockStyle.Fill;

            workItem.RegisterSmartPartInfo(smartPart, info);
            workspace.Show(smartPart, info);

            Assert.AreEqual(DockStyle.Fill, workspace.Zones["Main"].Controls[0].Dock);
        }

        [TestMethod]
        public void ZoneDoesNotFireSPActivatedEventIfNoSPPresent()
        {
            Control zone1 = new Control();
            workspace.Controls.Add(zone1);

            workspace.SetZoneName(zone1, "TestZone");

            Assert.AreEqual("TestZone", workspace.GetZoneName(zone1));

            bool activatedCalled = false;
            workspace.SmartPartActivated += delegate(object sender, WorkspaceEventArgs e)
            {
                activatedCalled = true;
            };

            zone1.Focus();

            Assert.IsFalse(activatedCalled);
        }

        [TestMethod]
        public void CanShowInDefaultZone()
        {
            Control zone = new Control();
            workspace.Controls.Add(zone);
            workspace.SetZoneName(zone, "TestZone");
            workspace.SetIsDefaultZone(zone, true);

            MockWPFSmartPart smartPartA = new MockWPFSmartPart();
            workItem.Items.Add(smartPartA);
            workspace.Show(smartPartA);

            Assert.IsTrue(workspace.GetIsDefaultZone(zone));
            Assert.AreSame(smartPartA.ElementHost, workspace.Zones["TestZone"].Controls[0]);
        }

        [TestMethod]
        public void CanShowInDefaultZoneWithInfoNoZoneName()
        {
            Control zone = new Control();
            workspace.Controls.Add(zone);
            workspace.SetZoneName(zone, "TestZone");
            workspace.SetIsDefaultZone(zone, true);

            MockWPFSmartPart smartPartA = new MockWPFSmartPart();
            workItem.Items.Add(smartPartA);
            ZoneSmartPartInfo info = new ZoneSmartPartInfo();
            info.Dock = DockStyle.Fill;
            info.Title = "Test";
            workspace.Show(smartPartA, info);

            Assert.IsTrue(workspace.GetIsDefaultZone(zone));
            Assert.AreSame(smartPartA.ElementHost, workspace.Zones["TestZone"].Controls[0]);
        }

        [TestMethod]
        public void RemovingZoneUnregistersGotFocusEvent()
        {
            Control zone1 = new Control();
            workspace.Controls.Add(zone1);

            workspace.SetZoneName(zone1, "TestZone");

            Assert.AreEqual("TestZone", workspace.GetZoneName(zone1));

            workspace.Controls.Remove(zone1);

            Assert.IsNull(workspace.GetZoneName(zone1));
            Assert.AreEqual(0, workspace.Zones.Count);

            bool activatedCalled = false;
            workspace.SmartPartActivated += delegate(object sender, WorkspaceEventArgs e)
            {
                activatedCalled = true;
            };

            Form form1 = new Form();
            form1.Controls.Add(zone1);
            form1.Show();

            Assert.IsFalse(activatedCalled);
        }

        [TestMethod]
        public void ActivatingChildControlRaisesSmartPartActivatedForContainingSP()
        {
            ZoneWorkspaceForm zoneForm = new ZoneWorkspaceForm();
            workItem.Items.Add(zoneForm);
            zoneForm.Show();
            System.Windows.Controls.ContentControl sp = new System.Windows.Controls.ContentControl();
            System.Windows.Controls.TextBox tb = new System.Windows.Controls.TextBox();

            sp.Content = tb;
            zoneForm.Workspace.Show(sp);
            zoneForm.Workspace.Show(new Control());

            System.Windows.Controls.Control received = null;
            zoneForm.Workspace.SmartPartActivated += delegate(object sender, WorkspaceEventArgs e)
            {
                received = (System.Windows.Controls.Control)e.SmartPart;
            };

            tb.Focus();

            Assert.AreSame(sp, received);
        }

        [TestMethod]
        public void RenamingZoneDoesNotRegisterEventsMultipleTimes()
        {
            int activatedCalled = 0;
            ZoneWorkspaceForm zoneForm = new ZoneWorkspaceForm();
            zoneForm.Workspace.WPFUIElementAdapter = workItem.Services.Get<IWPFUIElementAdapter>();
            zoneForm.Show();
            Control zone1 = new Control();

            zoneForm.Workspace.Controls.Add(zone1);
            zoneForm.Workspace.SetZoneName(zone1, "TestZone");

            //rename
            zoneForm.Workspace.SetZoneName(zone1, "NewZone");

            zoneForm.Workspace.SmartPartActivated += delegate(object sender, WorkspaceEventArgs e)
            {
                activatedCalled++;
            };

            zoneForm.Workspace.Show(new MockWPFSmartPart(), new ZoneSmartPartInfo("NewZone"));

            Assert.AreEqual("NewZone", zoneForm.Workspace.GetZoneName(zone1));
            Assert.AreEqual(1, activatedCalled);
        }

        [TestMethod]
        public void ShowFiresActivatedEventWithSPAsParameter()
        {
            MockWPFSmartPart smartPartA = new MockWPFSmartPart();
            ZoneSmartPartInfo smartPartInfoA = new ZoneSmartPartInfo();
            smartPartInfoA.ZoneName = "Zone";

            Control zone = new Control();
            workspace.Controls.Add(zone);
            workspace.SetZoneName(zone, "Zone");

            bool activatedCalled = false;
            workspace.SmartPartActivated += delegate(object sender, WorkspaceEventArgs e)
            {
                activatedCalled = true;
                Assert.AreSame(e.SmartPart, smartPartA);
            };

            workspace.Show(smartPartA, smartPartInfoA);
            Assert.AreEqual(Visibility.Visible, smartPartA.Visibility);

            Assert.IsTrue(activatedCalled);
        }

        [TestMethod]
        public void RemovingZoneFromWorkspaceRemovesContainedSmartPart()
        {
            Control zone = new Control();
            workspace.Controls.Add(zone);

            workspace.SetZoneName(zone, "TestZone3");

            MockWPFSmartPart smartPartA = new MockWPFSmartPart();
            ZoneSmartPartInfo spInfoA = new ZoneSmartPartInfo();
            spInfoA.ZoneName = "TestZone3";

            workspace.Show(smartPartA, spInfoA);
            zone.Focus();
            Form f = new Form();
            f.Controls.Add(zone);

            //Fails
            Assert.IsFalse(workspace.SmartParts.Contains(smartPartA));
        }


        [TestMethod]
        public void RemovingControlChainUnregistersZone()
        {
            Control parent = new Control();
            Control zone1 = new Control();
            parent.Controls.Add(zone1);
            workspace.Controls.Add(parent);
            workspace.SetZoneName(zone1, "TestZone");

            workspace.Controls.Remove(parent);

            Assert.IsNull(workspace.GetZoneName(zone1));
            Assert.AreEqual(0, workspace.Zones.Count);
        }

        [TestMethod]
        public void SetZoneWithMultipleNames()
        {
            Control zone = new Control();
            workspace.Controls.Add(zone);

            workspace.SetZoneName(zone, "TestZone");
            workspace.SetZoneName(zone, "TestZone1");
            workspace.SetZoneName(zone, "TestZone2");
            workspace.SetZoneName(zone, "TestZone3");

            Assert.AreEqual("TestZone3", workspace.GetZoneName(zone));
            Assert.AreEqual(1, workspace.Zones.Count);
        }

        [TestMethod]
        public void RemovingControlChainUnregistersZones()
        {
            Control parent = new Control();
            Control zone1 = new Control();
            Control zone2 = new Control();

            parent.Controls.Add(zone1);
            parent.Controls.Add(zone2);

            workspace.Controls.Add(parent);
            workspace.SetZoneName(zone1, "TestZone");
            workspace.SetZoneName(zone2, "TestZone2");

            workspace.Controls.Remove(parent);

            Assert.IsNull(workspace.GetZoneName(zone1));
            Assert.AreEqual(0, workspace.Zones.Count);
        }

        [TestMethod]
        public void ShowGetsSmartPartInfoRegisteredWithWorkItem1()
        {
            MockWPFSmartPart smartPartA = new MockWPFSmartPart();
            workItem.Items.Add(smartPartA);

            ZoneSmartPartInfo smartPartInfoA = new ZoneSmartPartInfo();
            smartPartInfoA.ZoneName = "ZoneA";
            smartPartInfoA.Dock = DockStyle.Left;

            workItem.RegisterSmartPartInfo(smartPartA, smartPartInfoA);

            Control zoneA = new Control();
            workspace.Controls.Add(zoneA);
            workspace.SetZoneName(zoneA, "ZoneA");

            workspace.Show(smartPartA);

            Assert.IsTrue(workspace.Zones["ZoneA"].Controls.Contains(smartPartA.ElementHost));
            Assert.AreEqual(DockStyle.Left, smartPartA.ElementHost.Dock);
        }

        [TestMethod]
        public void FocusOnInnerControlActivatesContainingSmartPart()
        {
            ZoneWorkspaceForm form = new ZoneWorkspaceForm();
            workItem.Items.Add(form);
            form.Show();

            System.Windows.Controls.ContentControl sp1 = new System.Windows.Controls.ContentControl();
            System.Windows.Controls.TextBox tb = new System.Windows.Controls.TextBox();
            sp1.Content = tb;

            System.Windows.Controls.ContentControl sp2 = new System.Windows.Controls.ContentControl();
            System.Windows.Controls.TextBox tb2 = new System.Windows.Controls.TextBox();
            sp2.Content = tb2;

            form.Workspace.Show(sp1, new ZoneSmartPartInfo("LeftZone"));
            form.Workspace.Show(sp2, new ZoneSmartPartInfo("ContentZone"));

            Assert.AreSame(sp2, form.Workspace.ActiveSmartPart);
            tb.Focus();
            Assert.AreSame(sp1, form.Workspace.ActiveSmartPart);
            tb2.Focus();
            Assert.AreSame(sp2, form.Workspace.ActiveSmartPart);
        }

        //[TestMethod]
        //[Ignore] // Docking does not apply to WPF controls
        //public void ShowWithNoSPIDoesNotOverrideDockInformation()
        //{
        //    ControlSmartPart sp = new ControlSmartPart();
        //    sp.Dock = DockStyle.Fill;

        //    AddZones(new Control(), new Control());
        //    workspace.Show(sp);

        //    Assert.AreEqual(DockStyle.Fill, sp.Dock);
        //}

        [TestMethod]
        public void FiresOneEventOnlyIfSmartPartIsShownMultipleTimes()
        {
            // Show First SmartPart
            MockWPFSmartPart smartPartA = new MockWPFSmartPart();
            ZoneSmartPartInfo smartPartInfoA = new ZoneSmartPartInfo();
            smartPartInfoA.ZoneName = "Zone";

            Control zone = new Control();
            workspace.Controls.Add(zone);
            workspace.SetZoneName(zone, "Zone");

            workspace.Show(smartPartA, smartPartInfoA);
            Assert.AreEqual(Visibility.Visible, smartPartA.Visibility);

            // Show Second SmartPart
            MockWPFSmartPart smartPartB = new MockWPFSmartPart();
            ZoneSmartPartInfo smartPartInfoB = new ZoneSmartPartInfo();
            smartPartInfoB.ZoneName = "Zone1";

            Control zone1 = new Control();
            workspace.Controls.Add(zone1);
            workspace.SetZoneName(zone1, "Zone1");

            workspace.Show(smartPartB, smartPartInfoB);
            Assert.AreEqual(Visibility.Visible, smartPartB.Visibility);

            // Show first SmartPart again
            int activatedCalled = 0;
            workspace.SmartPartActivated += delegate(object sender, WorkspaceEventArgs e)
            {
                activatedCalled++;
                Assert.AreSame(e.SmartPart, smartPartA);
            };

            workspace.Show(smartPartA, smartPartInfoA);

            Assert.AreEqual(1, activatedCalled);
        }

        //[TestMethod]
        //[Ignore] WPF controls can't be added at design time.
        //public void DesignTimeControlsProcessedAtEndInit()
        //{
        //    bool activated = false;
        //    workspace.SmartPartActivated += delegate { activated = true; };
        //    ((ISupportInitialize)workspace).BeginInit();

        //    Control zone = new Control();
        //    workspace.Controls.Add(zone);
        //    workspace.SetZoneName(zone, "Zone");

        //    zone.Controls.Add(new MonthCalendar());

        //    ((ISupportInitialize)workspace).EndInit();

        //    Assert.AreEqual(1, workspace.SmartParts.Count);
        //    Assert.IsTrue(activated);
        //}

        //[TestMethod]
        //[Ignore] WPF controls can't be added at design time.
        //public void DesignTimeControlsOnlyFirstLevelProcessed()
        //{
        //    ((ISupportInitialize)workspace).BeginInit();

        //    Control zone = new Control();
        //    workspace.Controls.Add(zone);
        //    workspace.SetZoneName(zone, "Zone");

        //    Control sp = new Control();
        //    Control inner = new Control();
        //    sp.Controls.Add(inner);

        //    zone.Controls.Add(sp);

        //    ((ISupportInitialize)workspace).EndInit();

        //    Assert.AreEqual(1, workspace.SmartParts.Count);

        //    inner.Dispose();
        //    Assert.AreEqual(1, workspace.SmartParts.Count);
        //}

        //[TestMethod]
        //[Ignore] // Zones are not defined with WPF controls.
        //public void RemovingControlChainUnregistersZone1()
        //{
        //    Control parent = new Control();
        //    Control zone1 = new Control();

        //    parent.Controls.Add(zone1);

        //    workspace.Controls.Add(parent);
        //    workspace.SetZoneName(zone1, "TestZone");

        //    workspace.Controls.Remove(parent);

        //    // Debug here. It goes in the OnZoneParentChanged method of ZoneWorkspace
        //    parent.Controls.Remove(zone1);

        //    Assert.IsNull(workspace.GetZoneName(zone1));
        //    Assert.AreEqual(0, workspace.Zones.Count);
        //}

		private static void AddZones(Control zone, Control zone1)
		{
			workspace.Controls.Add(zone);
			workspace.Controls.Add(zone1);
			workspace.SetZoneName(zone, "Main");
			workspace.SetZoneName(zone1, "Main1");
		}

		private static ZoneWorkspaceForm CreateFormAddWorkspace()
		{
			ZoneWorkspaceForm form = new ZoneWorkspaceForm();
			form.Controls.Add(workspace);
			return form;
		}
	}
}
