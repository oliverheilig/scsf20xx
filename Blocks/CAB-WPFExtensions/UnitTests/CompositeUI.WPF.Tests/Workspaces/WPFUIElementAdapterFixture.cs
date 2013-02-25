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
using System.Windows;
using System.Windows.Forms.Integration;
using Wf = System.Windows.Forms;
using Wpf = System.Windows.Controls;
using System.Threading;

namespace Microsoft.Practices.CompositeUI.WPF.Tests.Workspaces
{
    [TestClass]
    public class WPFUIElementAdapterFixture
    {
        private WPFUIElementAdapter catalog;
		private UIElement uiElement;

        [TestInitialize]
        public void Setup()
        {
            catalog = new WPFUIElementAdapter();
			uiElement = new MockWPFSmartPart();
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

		#region Dictionary like service
		[TestMethod]
        public void CanWrapWPFElement()
        {
			Wf.Control wrapper = catalog.Wrap(uiElement);
			Assert.IsNotNull(wrapper);
        }

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void CanNotWrapNull()
		{
			catalog.Wrap(null);
		}

		[TestMethod]
		public void	UnWrapIsInverse()
		{
			Wf.Control wrapper = catalog.Wrap(uiElement);
			UIElement unwrapped = catalog.Unwrap(wrapper);

			Assert.AreSame(unwrapped, uiElement);
		}

		[TestMethod]
		public void WrapIsConstant()
		{
			Wf.Control wrapper = catalog.Wrap(uiElement);
			Wf.Control wrapper2 = catalog.Wrap(uiElement);

			Assert.AreSame(wrapper, wrapper2);
		}

		[TestMethod]
		public void UnwrapReturnsNullOnNonWrappers()
		{
			Wf.Panel nonWrapper = new Wf.Panel();
			UIElement unwrapped = catalog.Unwrap(nonWrapper);

			Assert.IsNull(unwrapped);
		}

		[TestMethod]
		public void UnwrapReturnsNullOnElementHostNonCreatedCatalog()
		{
			ElementHost otherWrapper = new ElementHost();
			otherWrapper.Child = uiElement;

			UIElement unwrapped = catalog.Unwrap(otherWrapper);

			Assert.IsNull(unwrapped);
		}

		#endregion

		#region Properties
		
        [TestMethod]
		public void SizeIsCopiedToWrapper()
		{
            Wpf.Button control = new Wpf.Button();
            control.Height = 170;
            control.Width = 130;

            Wf.Control wrapper = catalog.Wrap(control);

			Assert.AreEqual(170, wrapper.Height);
			Assert.AreEqual(130, wrapper.Width);
		}

        [TestMethod]
        public void VisibilityIsCopiedToUIElement()
        {
            Wf.Control wrapper = catalog.Wrap(uiElement);
            wrapper.Visible = false;
            Assert.AreEqual(Visibility.Hidden, uiElement.Visibility);
        }

        [TestMethod]
        public void VisibilityIsSetToUIElementWhenWrapperHides()
        {
            Wf.Control wrapper = catalog.Wrap(uiElement);
            wrapper.Hide();
            Assert.AreEqual(Visibility.Hidden, uiElement.Visibility);
        }

        [TestMethod]
        public void VisibilityIsSetToUIElementWhenControlContainingWrapperHides()
        {
            Wf.Control wrapper = catalog.Wrap(uiElement);
            Wf.Panel panel = new Wf.Panel();
            panel.Controls.Add(wrapper);

            panel.Hide();

            Assert.AreEqual(Visibility.Hidden, uiElement.Visibility);
        }

		#endregion

        #region Disposal

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UIElementCannotBeWrappedAgainAfterWrapperDisposal()
        {
            Wf.Control wrapper = catalog.Wrap(uiElement);
            wrapper.Dispose();
            Wf.Control wrapper2 = catalog.Wrap(uiElement);
        }

        [TestMethod]
        public void UIElementCannotBeWrappedAfterDisposal()
        {
            MockWPFSmartPart smartPart = new MockWPFSmartPart();
            Wf.Control wrapper = catalog.Wrap(smartPart);
            smartPart.Dispose();
            Wf.Control wrapper2 = catalog.Wrap(smartPart);        
        }

        #endregion
    }
}
