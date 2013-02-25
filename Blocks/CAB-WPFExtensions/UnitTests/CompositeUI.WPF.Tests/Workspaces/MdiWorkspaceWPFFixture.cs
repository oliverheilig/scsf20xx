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
//===============================================================================
// Microsoft patterns & practices
// CompositeUI Application Block
//===============================================================================
// Copyright � Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

#if !NUNIT
using Microsoft.VisualStudio.TestTools.UnitTesting;
#else
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
#endif

using System;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.SmartParts;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.IO;
using Microsoft.Practices.CompositeUI.WPF.Tests;
using Microsoft.Practices.CompositeUI.WPF;
using Microsoft.Practices.CompositeUI.WPF.Tests.Workspaces;

namespace Microsoft.Practices.CompositeUI.WPF.Tests
{
	[TestClass]
	public class MdiWorkspaceWPFFixture
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
		public void MDIWorkspaceIsMDIContainer()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			Form parentForm = workItem.Items.AddNew<Form>();
			MdiWorkspace workspace = new MdiWorkspace(parentForm);
			workItem.Workspaces.Add(workspace);
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			Assert.IsTrue(workspace.ParentMdiForm.IsMdiContainer);
		}

		#region Show

		[TestMethod]
		public void ShowShowsNewMDIChildWithSmartPart()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			Form parentForm = workItem.Items.AddNew<Form>();
			MdiWorkspace workspace = new MdiWorkspace(parentForm);
			workItem.Workspaces.Add(workspace);
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			workspace.Show(smartPart);

			Assert.AreEqual(1, workspace.ParentMdiForm.MdiChildren.Length);
			Assert.IsTrue(workspace.Windows[smartPart.ElementHost].IsMdiChild);
			Assert.AreEqual(smartPart.ElementHost, workspace.ParentMdiForm.MdiChildren[0].Controls[0]);
		}

		[TestMethod]
		public void ShowSizesFormCorrectly()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			Form parentForm = workItem.Items.AddNew<Form>();
			MdiWorkspace workspace = new MdiWorkspace(parentForm);
			workItem.Workspaces.Add(workspace);
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			//smartPart.Size = new System.Drawing.Size(300, 200);
			smartPart.Width = 300;
			smartPart.Height = 200;
			workspace.Show(smartPart);

			Assert.AreEqual(300, workspace.ParentMdiForm.MdiChildren[0].Size.Width);
			Assert.AreEqual(220, workspace.ParentMdiForm.MdiChildren[0].Size.Height);
		}

		[TestMethod]
		public void ShowSetTextOnFormFromSPInfo()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			Form parentForm = workItem.Items.AddNew<Form>();
			MdiWorkspace workspace = new MdiWorkspace(parentForm);
			workItem.Workspaces.Add(workspace);
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			ISmartPartInfo info = new WindowSmartPartInfo();
			info.Title = "Smart Part";
			workItem.RegisterSmartPartInfo(smartPart, info);

			workspace.Show(smartPart);

			Assert.AreEqual("Smart Part", workspace.ParentMdiForm.MdiChildren[0].Text);
		}

		[TestMethod]
		public void ShowSetTextFromWindowSPInfo()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			Form parentForm = workItem.Items.AddNew<Form>();
			MdiWorkspace workspace = new MdiWorkspace(parentForm);
			workItem.Workspaces.Add(workspace);
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			WindowSmartPartInfo info = new WindowSmartPartInfo();
			info.Title = "SP";

			workspace.Show(smartPart, info);

			Assert.AreEqual("SP", workspace.ParentMdiForm.MdiChildren[0].Text);
		}

		[TestMethod]
		public void ShowSmartpartTwice()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			Form parentForm = workItem.Items.AddNew<Form>();
			MdiWorkspace workspace = new MdiWorkspace(parentForm);
			workItem.Workspaces.Add(workspace);
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			parentForm.Show();
			workspace.Show(smartPart);
			workspace.Show(smartPart);

			Assert.AreNotEqual(System.Windows.Visibility.Hidden, smartPart.Visibility);
		}

		[TestMethod]
		public void ShowingFiresActivatedEvent()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			Form parentForm = workItem.Items.AddNew<Form>();
			MdiWorkspace workspace = new MdiWorkspace(parentForm);
			workItem.Workspaces.Add(workspace);
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			bool activated = false;
			parentForm.Show();
			workspace.SmartPartActivated += delegate { activated = true; };

			workspace.Show(smartPart);

			Assert.IsTrue(activated);
		}

		[TestMethod]
		public void ShowingFiresActivatedWithSmartPart()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			Form parentForm = workItem.Items.AddNew<Form>();
			MdiWorkspace workspace = new MdiWorkspace(parentForm);
			workItem.Workspaces.Add(workspace);
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			object argsSmartPart = null;
			parentForm.Show();
			workspace.SmartPartActivated +=
				delegate(object sender, WorkspaceEventArgs args) { argsSmartPart = args.SmartPart; };

			workspace.Show(smartPart);

			Assert.AreEqual(smartPart, argsSmartPart);
		}

		[TestMethod]
		[Ignore] // Focus issue, 
		// smartPart IsFocused property of smartparts never get set to False when
		// smartPart2 is showed.
		//
		// ElementHosts Focused property are allways False.
		//
		// BringsToFront method of corresponding window is called, but can't test it.
		public void ShowExistingFormShouldBringToFront()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			Form parentForm = workItem.Items.AddNew<Form>();
			MdiWorkspace workspace = new MdiWorkspace(parentForm);
			workItem.Workspaces.Add(workspace);

			parentForm.Show();
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();
			MockWPFSmartPart smartPart2 = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			workspace.Show(smartPart);
			workspace.Show(smartPart2);
			workspace.Show(smartPart);

			Assert.IsTrue(smartPart.ElementHost.Focused);
		}

		[TestMethod]
		public void CanSpecifySizeOnShow()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			Form parentForm = workItem.Items.AddNew<Form>();
			MdiWorkspace workspace = new MdiWorkspace(parentForm);
			workItem.Workspaces.Add(workspace);
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			WindowSmartPartInfo info = new WindowSmartPartInfo();
			info.Title = "Mock Smart Part";
			info.Width = 300;
			info.Height = 400;

			workspace.Show(smartPart, info);

			Assert.AreEqual(300, workspace.Windows[smartPart.ElementHost].Width);
			Assert.AreEqual(400, workspace.Windows[smartPart.ElementHost].Height);
		}

		[TestMethod]
		public void CanSpecifyLocationOnShow()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			Form parentForm = workItem.Items.AddNew<Form>();
			MdiWorkspace workspace = new MdiWorkspace(parentForm);
			workItem.Workspaces.Add(workspace);
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			WindowSmartPartInfo info = new WindowSmartPartInfo();
			info.Title = "Mock Smart Part";
			info.Location = new Point(10, 50);

			workspace.Show(smartPart, info);

			Assert.AreEqual(10, workspace.Windows[smartPart.ElementHost].Location.X);
			Assert.AreEqual(50, workspace.Windows[smartPart.ElementHost].Location.Y);
		}

		[TestMethod]
		public void CanSetWindowOptionsOnShow()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			Form parentForm = workItem.Items.AddNew<Form>();
			MdiWorkspace workspace = new MdiWorkspace(parentForm);
			workItem.Workspaces.Add(workspace);
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			WindowSmartPartInfo info = new WindowSmartPartInfo();
			info.Title = "Mock Smart Part";
			info.ControlBox = false;
			info.MinimizeBox = false;
			info.MaximizeBox = false;
			Icon icon = null;
			Assembly asm = Assembly.GetExecutingAssembly();

			using (Stream imgStream =
				asm.GetManifestResourceStream("Microsoft.Practices.CompositeUI.WPF.Tests.test.ico"))
			{
				icon = new Icon(imgStream);
			}
			info.Icon = icon;

			workspace.Show(smartPart, info);

			Assert.IsFalse(workspace.Windows[smartPart.ElementHost].ControlBox);
			Assert.IsFalse(workspace.Windows[smartPart.ElementHost].MinimizeBox);
			Assert.IsFalse(workspace.Windows[smartPart.ElementHost].MaximizeBox);
			Assert.AreSame(icon, workspace.Windows[smartPart.ElementHost].Icon);
		}

		[TestMethod]
		public void CanShowWithNonWindowSPI()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			Form parentForm = workItem.Items.AddNew<Form>();
			MdiWorkspace workspace = new MdiWorkspace(parentForm);
			workItem.Workspaces.Add(workspace);
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			WPFSmartPartInfo info = new WPFSmartPartInfo();
			info.Title = "Foo";

			workspace.Show(smartPart, info);
		}

		[TestMethod]
		public void UsesSPInfoIfNoWindowSPInfoExists()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			Form parentForm = workItem.Items.AddNew<Form>();
			MdiWorkspace workspace = new MdiWorkspace(parentForm);
			workItem.Workspaces.Add(workspace);
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			WPFSmartPartInfo info = new WPFSmartPartInfo();
			info.Title = "Foo";
			workItem.RegisterSmartPartInfo(smartPart, info);

			workspace.Show(smartPart);

			Assert.AreEqual("Foo", workspace.Windows[smartPart.ElementHost].Text);
		}

		[TestMethod]
		public void CloseSmartPartDoesNotDisposeIt()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			MdiWorkspace workspace = workItem.Workspaces.AddNew<MdiWorkspace>();
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			workspace.Show(smartPart);
			workspace.Close(smartPart);

			Assert.IsFalse(smartPart.IsDisposed);
		}

		#endregion
		
		#region Hide

		[TestMethod]
		public void CanHideFormWithSmartPart()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			Form parentForm = workItem.Items.AddNew<Form>();
			MdiWorkspace workspace = new MdiWorkspace(parentForm);
			workItem.Workspaces.Add(workspace);
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			workspace.Show(smartPart);
			workspace.Hide(smartPart);

			Assert.IsFalse(workspace.Windows[smartPart.ElementHost].Visible);
			Assert.IsFalse(smartPart.ElementHost.Visible);
			// Hosted UIElement doesn't get Visibility property set upon ElementHost Visible change
			// Assert.AreNotEqual(System.Windows.Visibility.Visible, smartPart.Visibility);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void HideNonExistSmartPartThrows()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			Form parentForm = workItem.Items.AddNew<Form>();
			MdiWorkspace workspace = new MdiWorkspace(parentForm);
			workItem.Workspaces.Add(workspace);
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			workspace.Hide(smartPart);
		}

		#endregion

		#region Close

		[TestMethod]
		public void CanCloseMdiChild()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			Form parentForm = workItem.Items.AddNew<Form>();
			MdiWorkspace workspace = new MdiWorkspace(parentForm);
			workItem.Workspaces.Add(workspace);
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			workspace.Show(smartPart);
			Form form = workspace.Windows[smartPart.ElementHost];

			workspace.Close(smartPart);

			Assert.IsTrue(form.IsDisposed);
			Assert.IsFalse(smartPart.IsDisposed);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void CloseNonExistSmartPartThrows()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			Form parentForm = workItem.Items.AddNew<Form>();
			MdiWorkspace workspace = new MdiWorkspace(parentForm);
			workItem.Workspaces.Add(workspace);
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			workspace.Close(smartPart);
		}

		[TestMethod]
		public void WorkspaceFiresSmartPartClosing()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			Form parentForm = workItem.Items.AddNew<Form>();
			MdiWorkspace workspace = new MdiWorkspace(parentForm);
			workItem.Workspaces.Add(workspace);
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			bool closing = false;
			parentForm.Show();
			workspace.Show(smartPart);
			workspace.SmartPartClosing += delegate { closing = true; };

			workspace.Close(smartPart);

			Assert.IsTrue(closing);
		}

		[TestMethod]
		public void CanCancelSmartPartClosing()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			Form parentForm = workItem.Items.AddNew<Form>();
			MdiWorkspace workspace = new MdiWorkspace(parentForm);
			workItem.Workspaces.Add(workspace);
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			parentForm.Show();
			workspace.Show(smartPart);
			workspace.SmartPartClosing += delegate(object sender, WorkspaceCancelEventArgs args) { args.Cancel = true; };

			workspace.Close(smartPart);

			Assert.IsFalse(smartPart.IsDisposed, "Smart Part Was Disposed");
		}

		#endregion

		[TestMethod]
		public void SettingFocusOnWindowFiresActivated()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			Form parentForm = workItem.Items.AddNew<Form>();
			MdiWorkspace workspace = new MdiWorkspace(parentForm);
			workItem.Workspaces.Add(workspace);
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			object argsSmartPart = null;
			parentForm.Show();
			MockWPFSmartPart sp1 = workItem.SmartParts.AddNew<MockWPFSmartPart>();
			workspace.Show(sp1);
			workspace.Show(smartPart);

			workspace.SmartPartActivated +=
				delegate(object sender, WorkspaceEventArgs args) { argsSmartPart = args.SmartPart; };

			workspace.Windows[sp1.ElementHost].Focus();

			Assert.AreEqual(sp1, argsSmartPart);
		}

		[TestMethod]
		public void WindowActivatedFiresCorrectNumberOfTimes()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			Form parentForm = workItem.Items.AddNew<Form>();
			MdiWorkspace workspace = new MdiWorkspace(parentForm);
			workItem.Workspaces.Add(workspace);
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			int activated = 0;
			parentForm.Show();
			MockWPFSmartPart sp1 = workItem.SmartParts.AddNew<MockWPFSmartPart>();
			workspace.SmartPartActivated +=
				delegate(object sender, WorkspaceEventArgs args) { activated++; };

			workspace.Show(sp1);
			workspace.Show(smartPart);
			workspace.Windows[sp1.ElementHost].Focus();

			Assert.AreEqual(3, activated);
		}
	}
}
