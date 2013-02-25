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
using System.Reflection;
using Microsoft.Practices.CompositeUI.WPF;
using Microsoft.Practices.CompositeUI.WPF.Tests.Workspaces;
using System.Windows;
using System.Windows.Forms.Integration;

namespace Microsoft.Practices.CompositeUI.WPF.Tests
{
	[TestClass]
	public class DeckWorkspaceWPFFixture
	{
		private static DeckWorkspace workspace;
		private static MockWPFSmartPart smartPart;
        private static MockWPFUIElementAdapter catalog;

		[TestInitialize]
		public void Setup()
		{
			workspace = new DeckWorkspace();
			smartPart = new MockWPFSmartPart();
            catalog = new MockWPFUIElementAdapter();
            workspace.WPFUIElementAdapter = catalog;
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

		#region Show
        
        [TestMethod]        
        public void ShowMakesControlVisible()
        {
            smartPart.Visibility = Visibility.Hidden;

            workspace.Show(smartPart);

            Assert.AreEqual(Visibility.Visible, smartPart.Visibility);
        }
        
		[TestMethod]
		public void ShowHidesPreviouslyVisibleControl()
		{
			MockWPFSmartPart c2 = new MockWPFSmartPart();

            workspace.Show(smartPart);
            workspace.Show(c2);

            UIElement actual = catalog.Unwrap(workspace.Controls[1]);
            Assert.AreSame(smartPart, actual, "Hiden control didn't go to the bottom of the deck");
            Assert.AreSame(c2, workspace.ActiveSmartPart);
        }

        [TestMethod]        
        public void ShowSetsElementHostDockFill()
        {
            workspace.Show(smartPart);

            ElementHost actual = catalog.Hosts[smartPart];
            Assert.AreEqual(DockStyle.Fill, actual.Dock);
        }

        [TestMethod]
        public void ShowWhenControlAlreadyExistsShowsSameControl()
        {
            MockWPFSmartPart c2 = new MockWPFSmartPart();
            MockWPFSmartPart c3 = new MockWPFSmartPart();

            workspace.Show(smartPart);
            workspace.Show(c2);
            workspace.Show(c3);
            workspace.Show(c2);
            workspace.Show(smartPart);

            Assert.AreEqual(3, workspace.SmartParts.Count);
        }

        [TestMethod]
        public void CallingShowTwiceStillShowsControl()
        {
            workspace.Show(smartPart);
            workspace.Show(smartPart);

            Assert.AreSame(smartPart, workspace.ActiveSmartPart);
        }

        [TestMethod]
        public void FiresSmartPartActivateWhenShown()
        {
            object argsSmartPart = null;
            workspace.SmartPartActivated +=
                delegate(object sender, WorkspaceEventArgs args) { argsSmartPart = args.SmartPart; };

            workspace.Show(smartPart);

            Assert.AreEqual(smartPart, argsSmartPart);
        }

        #endregion

        #region Hide

        [TestMethod]
        public void HideDoesNotHideControl()
        {
            smartPart.Visibility = Visibility.Hidden;
            workspace.Show(smartPart);
            
            workspace.Hide(smartPart);

            // The reasoning is that in a deck, the factor that causes 
            // a smart part to be hiden is that another one is shown on top.
            // There's no actual hiding of the previous control.
            Assert.AreEqual(Visibility.Visible, smartPart.Visibility);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void HideNonExistSmartPartThrows()
        {
            workspace.Hide(smartPart);
        }

        [TestMethod]
        public void HidingShowsPreviousFireActivatedEvent()
        {
            object argsSmartPart = null;
            MockWPFSmartPart sp1 = new MockWPFSmartPart();
            workspace.Show(sp1);
            workspace.Show(smartPart);
            workspace.SmartPartActivated +=
                delegate(object sender, WorkspaceEventArgs args) { argsSmartPart = args.SmartPart; };

            workspace.Hide(smartPart);

            Assert.AreEqual(sp1, argsSmartPart);
        }

        [TestMethod]
        public void HideNonActiveSmartPartDoesNotChangeCurrentOne()
        {
            ControlSmartPart smartPartA = new ControlSmartPart();
            ControlSmartPart smartPartB = new ControlSmartPart();
            ControlSmartPart smartPartC = new ControlSmartPart();

            workspace.Show(smartPartA);
            workspace.Show(smartPartB);
            workspace.Show(smartPartC);

            workspace.Hide(smartPartB);

            Assert.AreSame(smartPartC, workspace.ActiveSmartPart);
        }

        [TestMethod]
        public void ShowHideKeepsOrder()
        {
            ControlSmartPart c1 = new ControlSmartPart();
            ControlSmartPart c2 = new ControlSmartPart();
            ControlSmartPart c3 = new ControlSmartPart();

            workspace.Show(c1);
            workspace.Show(c2);
            workspace.Show(c3);
            workspace.Show(c2);
            workspace.Hide(c2);

            Assert.AreSame(c3, workspace.ActiveSmartPart);
        }

        #endregion

        #region Close

        [TestMethod]        
        public void CloseRemovesSmartPartButDoesNotDispose()
        {
            workspace.Show(smartPart);

            workspace.Close(smartPart);
            
            ElementHost host = catalog.Hosts[smartPart];
            Assert.IsFalse(workspace.Controls.Contains(host));
            Assert.AreEqual(0, workspace.SmartParts.Count);
            Assert.IsFalse(smartPart.IsDisposed);
        }

        [TestMethod]
        public void WorkspaceFiresSmartPartClosing()
        {
            bool closing = false;
            workspace.Show(smartPart);
            workspace.SmartPartClosing += delegate { closing = true; };

            workspace.Close(smartPart);

            Assert.IsTrue(closing);
        }

        [TestMethod]
        public void CanCancelSmartPartClosing()
        {
            workspace.Show(smartPart);
            workspace.SmartPartClosing += delegate(object sender, WorkspaceCancelEventArgs args) { args.Cancel = true; };

            workspace.Close(smartPart);

            Assert.IsFalse(smartPart.IsDisposed);
        }

        [TestMethod]
        public void ClosingByDisposingControlDoesNotFireClosingEvent()
        {
            bool closing = false;
            workspace.Show(smartPart);
            workspace.SmartPartClosing += delegate { closing = true; };

            smartPart.Dispose();

            Assert.IsFalse(closing);            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ClosingNonExistSSmartPartThrows()
        {
            workspace.Close(smartPart);
        }

        [TestMethod]
        public void CloseShowsPreviouslyVisibleControl()
        {
            MockWPFSmartPart c1 = new MockWPFSmartPart();
            MockWPFSmartPart c2 = new MockWPFSmartPart();

            workspace.Show(c1);
            workspace.Show(c2);
            workspace.Close(c2);

            Assert.AreSame(c1, workspace.ActiveSmartPart);
        }

        #endregion

        #region Misc

        [TestMethod]
        public void DeckIsOderedCorrectly()
        {
            smartPart.Visibility = Visibility.Hidden;
            MockWPFSmartPart c2 = new MockWPFSmartPart();
            c2.Visibility = Visibility.Hidden;

            workspace.Show(smartPart);
            workspace.Show(c2);
            workspace.Show(smartPart);
            workspace.Hide(smartPart);

            Assert.AreSame(c2, workspace.ActiveSmartPart);
            Assert.AreSame(c2, workspace.SmartParts[1]);
        }

        #endregion

        #region Disposing

        [TestMethod]
        public void ElementHostIsNOTRemovedWhenSmartPartIsDisposed()
        {
            workspace.Show(smartPart);
            Assert.AreEqual(1, workspace.SmartParts.Count);

            smartPart.Dispose();

            Assert.AreEqual(1, workspace.SmartParts.Count);
        }

        [TestMethod]
        public void SmartIsRemovedWhenElementHostIsDisposed()
        {
            workspace.Show(smartPart);
            Assert.AreEqual(1, workspace.SmartParts.Count);

            ElementHost host = catalog.Hosts[smartPart];
            host.Dispose();

            Assert.IsTrue(smartPart.IsDisposed);
            Assert.AreEqual(0, workspace.SmartParts.Count);
        }

        [TestMethod]
        public void PreviousSmartPartActivatedWhenActiveElementHostDisposed()
        {
            MockWPFSmartPart smartPartA = new MockWPFSmartPart();
            MockWPFSmartPart smartPartB = new MockWPFSmartPart();
            workspace.Show(smartPartA);
            workspace.Show(smartPartB);

            ElementHost hostB = catalog.Hosts[smartPartB];
            hostB.Dispose();
            
            Assert.IsFalse(workspace.Contains(hostB));
            Assert.AreSame(smartPartA, workspace.ActiveSmartPart);
        }

        [TestMethod]
        public void WorkspaceFiresDisposedEvent()
        {
            bool disposed = false;
            DeckWorkspace workspace = new DeckWorkspace();
            Form form = new Form();
            form.Controls.Add(workspace);
            form.Show();

            workspace.Disposed += delegate { disposed = true; };
            form.Close();

            Assert.IsTrue(disposed);
        }

        [TestMethod]
        public void DisposeNonActiveElementHostDoesNotChangeActiveOne()
        {
            ControlSmartPart smartPartA = new ControlSmartPart();
            ControlSmartPart smartPartB = new ControlSmartPart();
            ControlSmartPart smartPartC = new ControlSmartPart();

            workspace.Show(smartPartA);
            workspace.Show(smartPartB);
            workspace.Show(smartPartC);

            ElementHost hostB = catalog.Hosts[smartPartB];
            hostB.Dispose();

            Assert.AreSame(smartPartC, workspace.ActiveSmartPart);
        }

        #endregion

        [TestMethod]
        public void CanCloseWorkspaceWithTwoSmartparts()
        {
            Control parent = new Control();
            parent.Controls.Add(workspace);
            MockWPFSmartPart sp1 = new MockWPFSmartPart();
            MockWPFSmartPart sp2 = new MockWPFSmartPart();
            workspace.Show(sp1);
            workspace.Show(sp2);


            parent.Dispose();
        }

        [TestMethod]
        public void ShowHidingFiresCorrectNumberOfTimes()
        {
            int activated = 0;
            MockWPFSmartPart sp1 = new MockWPFSmartPart();
            MockWPFSmartPart sp2 = new MockWPFSmartPart();
            workspace.SmartPartActivated += delegate { activated++; };

            workspace.Show(sp1);
            workspace.Show(sp2);
            workspace.Show(smartPart);

            workspace.Hide(smartPart);

            Assert.AreEqual(4, activated);
        }

        [TestMethod]
        public void ShowingHidingMultipleTimesKeepsProperDeckOrdering()
        {
            ControlSmartPart smartPartA = new ControlSmartPart();
            ControlSmartPart smartPartB = new ControlSmartPart();
            ControlSmartPart smartPartC = new ControlSmartPart();

            workspace.Show(smartPartA);
            workspace.Show(smartPartB);
            workspace.Show(smartPartC);

            workspace.Hide(smartPartC);
            Assert.AreSame(smartPartB, workspace.ActiveSmartPart);

            workspace.Hide(smartPartB);
            Assert.AreSame(smartPartA, workspace.ActiveSmartPart);

            workspace.Close(smartPartA);
            Assert.AreSame(smartPartC, workspace.ActiveSmartPart);

            workspace.Hide(smartPartC);
            Assert.AreSame(smartPartB, workspace.ActiveSmartPart);

            workspace.Hide(smartPartB);
            Assert.AreSame(smartPartC, workspace.ActiveSmartPart);
        }

		#region Supporting classes

		[SmartPart]
		private class NonMockSmartPartSmartPart : Object { }

		[SmartPart]
        class ControlSmartPart : System.Windows.Controls.Control
		{
		}
       

		#endregion
	}
}