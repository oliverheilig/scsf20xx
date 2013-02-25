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
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.SmartParts;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.IO;
using Microsoft.Practices.CompositeUI.WPF;

namespace Microsoft.Practices.CompositeUI.WPF.Tests.Workspaces
{
	[TestClass]
	public class WindowWorkspaceWPFFixture
	{
		private const uint WM_SYSCOMMAND = 0x0112;
		private const int SC_CLOSE = 0xF060;

		[DllImport("user32.dll")]
		static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

		#region Show

		[TestMethod]
		public void ShowShowsNewFormWithControl()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			workspace.Show(smartPart);

			Form form = workspace.Windows[smartPart.ElementHost];
			Assert.AreSame(smartPart.ElementHost, form.Controls[0]);
			Assert.IsTrue(workspace.Windows[smartPart.ElementHost].Visible);
			Assert.AreEqual(System.Windows.Visibility.Visible, smartPart.Visibility);

			form.Close();
		}

		[TestMethod]
		public void ShowShowsNewFormWithOwnerAndControl()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			Form owner = new Form();
			WindowWorkspace ws = new WindowWorkspace(owner);
			workItem.Workspaces.Add(ws);

			ws.Show(smartPart);

			Form form = ws.Windows[smartPart.ElementHost];
			Assert.AreSame(owner, form.Owner);

			form.Close();
		}

		[TestMethod]
		public void ShowingSetFormTextFromWindowSmartPartInfo()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			WindowSmartPartInfo info = new WindowSmartPartInfo();
			info.Title = "Mock Smart Part";
			workItem.RegisterSmartPartInfo(smartPart, info);
			workspace.Show(smartPart, info);

			Assert.AreEqual("Mock Smart Part", workspace.Windows[smartPart.ElementHost].Text);

			workspace.Windows[smartPart.ElementHost].Close();
		}

		[TestMethod]
		public void ShowingSmartPartISPInfoProviderSetFormText()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			ISmartPartInfo info = new WindowSmartPartInfo();
			info.Title = "Smart Part";
			workItem.RegisterSmartPartInfo(smartPart, info);

			workspace.Show(smartPart);

			Assert.AreEqual("Smart Part", workspace.Windows[smartPart.ElementHost].Text);

			workspace.Windows[smartPart.ElementHost].Close();
		}

        // This test will cause Com exceptions in the test runner.
        // It is ignored, but can be run to verify functionality
		[TestMethod]
        [Ignore]
		public void CanShowModal()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPart = null;
			WindowSmartPartInfo info = new WindowSmartPartInfo();
			info.Title = "Mock Smart Part";
			info.Modal = true;

			IWPFUIElementAdapter catalog = workItem.Services.Get<IWPFUIElementAdapter>();

			Thread thread = new Thread(new ThreadStart(delegate
				{
					// smartPart need to be created by the same thread that creates ElementHost
					smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>(); 

					workspace.Show(smartPart, info);
				}));
			// needed to create ElementHost, calling thread must be STA.
			thread.SetApartmentState(ApartmentState.STA); 

			try
			{
				thread.Start();
				Thread.Sleep(10000);

				Assert.IsTrue(workspace.Windows[catalog.Wrap(smartPart)].Visible);
			}
			finally
			{
				SendMessage(workspace.Windows[catalog.Wrap(smartPart)].Handle, WM_SYSCOMMAND, SC_CLOSE, 0);
				thread.Join();
			}
		}

        // This test will cause Com exceptions in the test runner.
        // It is ignored, but can be run to verify functionality
		[TestMethod]
		[Ignore]
		public void CanShowModalWithOwner()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			Form owner = new Form();
			WindowWorkspace workspace = new WindowWorkspace(owner);
			workItem.Workspaces.Add(workspace);
			MockWPFSmartPart smartPart = null;

			WindowSmartPartInfo info = new WindowSmartPartInfo();
			info.Title = "Mock Smart Part";
			info.Modal = true;

			IWPFUIElementAdapter catalog = workItem.Services.Get<IWPFUIElementAdapter>();

			Thread thread = new Thread(new ThreadStart(delegate
				{
					smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

					workspace.Show(smartPart, info);
				}));
			thread.SetApartmentState(ApartmentState.STA);

			try
			{
				thread.Start();
				Thread.Sleep(10000);

				Assert.IsTrue(workspace.Windows[catalog.Wrap(smartPart)].Visible);
				Assert.AreSame(owner, workspace.Windows[catalog.Wrap(smartPart)].Owner);
			}
			finally
			{
				SendMessage(workspace.Windows[catalog.Wrap(smartPart)].Handle, WM_SYSCOMMAND, SC_CLOSE, 0);
				thread.Join();
			}
		}

		[TestMethod]
		public void CanShowNonModal()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			WindowSmartPartInfo info = new WindowSmartPartInfo();
			info.Title = "Mock Smart Part";
			info.Modal = false;

			workspace.Show(smartPart, info);

			Assert.IsTrue(workspace.Windows[smartPart.ElementHost].Visible);
		}

		[TestMethod]
		public void FormSizeIsCorrectSize()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			smartPart.Width = 150;
			smartPart.Height = 125;

			workspace.Show(smartPart);

			Assert.AreEqual(150, workspace.Windows[smartPart.ElementHost].Size.Width);
			Assert.AreEqual(145, workspace.Windows[smartPart.ElementHost].Size.Height);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ShowingNonControlThrows()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();

			workspace.Show(new object());
		}

		[TestMethod]
		public void ShowingFiresActivatedEvent()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			bool activated = false;
			workspace.SmartPartActivated += delegate { activated = true; };
			workspace.Show(smartPart);

			Assert.IsTrue(activated);

			workspace.Windows[smartPart.ElementHost].Close();
		}

		[TestMethod]
		public void ShowingFiresActivatedWithSmartPart()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			object argsSmartPart = null;
			workspace.SmartPartActivated +=
				delegate(object sender, WorkspaceEventArgs args) { argsSmartPart = args.SmartPart; };
			workspace.Show(smartPart);

			Assert.AreEqual(smartPart, argsSmartPart);

			workspace.Windows[smartPart.ElementHost].Close();
		}

		[TestMethod]
		public void SettingFocusOnWindowFiresActivated()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			object argsSmartPart = null;
			MockWPFSmartPart smartPart2 = workItem.SmartParts.AddNew<MockWPFSmartPart>();
			workspace.Show(smartPart2);
			workspace.Show(smartPart);

			workspace.SmartPartActivated +=
				delegate(object sender, WorkspaceEventArgs args) { argsSmartPart = args.SmartPart; };

			workspace.Windows[smartPart2.ElementHost].Focus();

			Assert.AreEqual(smartPart2, argsSmartPart);

			workspace.Windows[smartPart2.ElementHost].Close();
			workspace.Windows[smartPart.ElementHost].Close();
		}

		[TestMethod]
		public void WindowActivatedFiresCorrectNumberOfTimes()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			int activated = 0;
			MockWPFSmartPart smartPart2 = workItem.SmartParts.AddNew<MockWPFSmartPart>();
			workspace.SmartPartActivated +=
				delegate(object sender, WorkspaceEventArgs args) { activated++; };

			workspace.Show(smartPart2);
			workspace.Show(smartPart);
			workspace.Windows[smartPart2.ElementHost].Focus();

			Assert.AreEqual(3, activated);

			workspace.Windows[smartPart2.ElementHost].Close();
			workspace.Windows[smartPart.ElementHost].Close();
		}

		[TestMethod]
		[Ignore] // Focus issue, 
		// smartPart IsFocused property of smartparts never get set to False when
		// smartPart2 is showed.
		//
		// ElementHosts Focused property are allways False.
		//
		// BringsToFront method of corresponding window is called, but can't test it.
		public void ShowExistingFormBringsToFront()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();
			MockWPFSmartPart smartPart2 = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			workspace.Show(smartPart);
			workspace.Show(smartPart2);
			workspace.Show(smartPart);

			Assert.IsTrue(smartPart.ElementHost.Focused);
			Assert.IsTrue(smartPart.IsFocused);

			workspace.Windows[smartPart2.ElementHost].Close();
			workspace.Windows[smartPart.ElementHost].Close();
		}

		[TestMethod]
		public void CanSpecifySize()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			WindowSmartPartInfo info = new WindowSmartPartInfo();
			info.Title = "Mock Smart Part";
			info.Width = 300;
			info.Height = 400;

			workspace.Show(smartPart, info);

			Assert.AreEqual(300, workspace.Windows[smartPart.ElementHost].Width);
			Assert.AreEqual(400, workspace.Windows[smartPart.ElementHost].Height);

			workspace.Windows[smartPart.ElementHost].Close();
		}

		[TestMethod]
		public void CanSpecifyLocation()
		{
		    TestableRootWorkItem workItem = new TestableRootWorkItem();
		    WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
		    MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

		    WindowSmartPartInfo info = new WindowSmartPartInfo();
		    info.Title = "Mock Smart Part";
		    info.Location = new Point(10, 50);

		    workspace.Show(smartPart, info);

		    Assert.AreEqual(10, workspace.Windows[smartPart.ElementHost].Location.X);
		    Assert.AreEqual(50, workspace.Windows[smartPart.ElementHost].Location.Y);

			workspace.Windows[smartPart.ElementHost].Close();
		}

		[TestMethod]
		public void CanSetWindowOptions()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
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

			workspace.Windows[smartPart.ElementHost].Close();
		}

		[TestMethod]
		public void CanApplyWindowOptions()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			WindowSmartPartInfo info = new WindowSmartPartInfo();
			info.Title = "Mock Smart Part";
			info.Width = 400;

			workspace.Show(smartPart, info);

			Assert.AreEqual(400, workspace.Windows[smartPart.ElementHost].Width);

			info.Width = 500;
			workspace.ApplySmartPartInfo(smartPart, info);

			Assert.AreEqual(500, workspace.Windows[smartPart.ElementHost].Width);

			workspace.Windows[smartPart.ElementHost].Close();
		}

		[TestMethod]
		public void CanShowIfSPINotWindowSPI()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			WPFSmartPartInfo info = new WPFSmartPartInfo();
			info.Title = "Foo";

			workspace.Show(smartPart, info);

			workspace.Windows[smartPart.ElementHost].Close();
		}

		[TestMethod]
		public void UsesSPInfoIfNoWindowSPInfoExists()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			WPFSmartPartInfo info = new WPFSmartPartInfo();
			info.Title = "Foo";
			workItem.RegisterSmartPartInfo(smartPart, info);

			workspace.Show(smartPart);

			Assert.AreEqual("Foo", workspace.Windows[smartPart.ElementHost].Text);

			workspace.Windows[smartPart.ElementHost].Close();
		}

		[TestMethod]
		public void FiresOneEventOnlyIfSmartPartIsShownMultipleTimes()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPartA = workItem.SmartParts.AddNew<MockWPFSmartPart>();
			MockWPFSmartPart smartPartB = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			workspace.Show(smartPartA);
			workspace.Show(smartPartB);

			int activatedCalled = 0;
			object lastActivatedSmartPart = null;
			workspace.SmartPartActivated += delegate(object sender, WorkspaceEventArgs e)
			{
				activatedCalled++;
				lastActivatedSmartPart = e.SmartPart;
			};

			workspace.Show(smartPartA);

			Assert.AreEqual(1, activatedCalled);
			Assert.AreSame(smartPartA, lastActivatedSmartPart);

			workspace.Windows[smartPartA.ElementHost].Close();
			workspace.Windows[smartPartB.ElementHost].Close();
		}

		[TestMethod]
		public void ShowTwiceReusesForm()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			workspace.Show(smartPart);
			workspace.Show(smartPart);

			Assert.AreEqual(1, workspace.Windows.Count);

			workspace.Windows[smartPart.ElementHost].Close();
		}

		[TestMethod]
		public void ShowSetsVisible()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();

			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();
			smartPart.Visibility = System.Windows.Visibility.Hidden;

			workspace.Show(smartPart);

			Assert.AreNotEqual(System.Windows.Visibility.Hidden, smartPart.Visibility);

			workspace.Windows[smartPart.ElementHost].Close();
		}

		#endregion

		#region Hide

		[TestMethod]
		public void CanHideWithSmartPart()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			workspace.Show(smartPart);

			workspace.Hide(smartPart);

			Assert.IsFalse(workspace.Windows[smartPart.ElementHost].Visible);
			Assert.IsFalse(smartPart.ElementHost.Visible);
			// Hosted UIElement doesn't get Visibility property set upon ElementHost Visible change
			// Assert.AreNotEqual(System.Windows.Visibility.Visible, smartPart.Visibility);

			workspace.Windows[smartPart.ElementHost].Close();
		}

		[TestMethod]
		public void CanShowSameWindowAfterHidden()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			workspace.Show(smartPart);
			workspace.Hide(smartPart);

			workspace.Show(smartPart);

			Assert.IsNotNull(workspace.Windows[smartPart.ElementHost]);
			//Assert.IsTrue(smartPart.Visible);
			Assert.AreNotEqual(System.Windows.Visibility.Hidden, smartPart.Visibility);

			workspace.Windows[smartPart.ElementHost].Close();
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void HideNonExistControlThrows()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			workspace.Hide(smartPart);
		}

		[TestMethod]
		public void HidingSmartPartDoesNotAutomaticallyShowPreviousForm()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPartA = workItem.SmartParts.AddNew<MockWPFSmartPart>();
			smartPartA.Visibility = System.Windows.Visibility.Hidden;
			MockWPFSmartPart smartPartB = workItem.SmartParts.AddNew<MockWPFSmartPart>();
			smartPartB.Visibility = System.Windows.Visibility.Hidden;

			WindowSmartPartInfo smartPartInfoB = new WindowSmartPartInfo();
			smartPartInfoB.Title = "Window SmartPart B";

			WindowSmartPartInfo smartPartInfoA = new WindowSmartPartInfo();
			smartPartInfoA.Title = "Window SmartPart A";

			workspace.Show(smartPartA, smartPartInfoA);
			//Assert.IsTrue(smartPartA.Visible);
			Assert.AreNotEqual(System.Windows.Visibility.Hidden, smartPartA.Visibility);

			// Force the form to non-visible so it doesn't fire
			// his own Activated event after we hide the following 
			// smart part, therefore making the condition impossible 
			// to test.

			workspace.Windows[smartPartA.ElementHost].Hide();

			workspace.Show(smartPartB, smartPartInfoB);
			//Assert.IsTrue(smartPartB.Visible);
			Assert.AreNotEqual(System.Windows.Visibility.Hidden, smartPartB.Visibility);

			workspace.Hide(smartPartB);

			Assert.IsNull(workspace.ActiveSmartPart);
		}

		#endregion

		#region Close

		[TestMethod]
		public void CloseDisposesAndClosesWindow()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			workspace.Show(smartPart);

			Form form = workspace.Windows[smartPart.ElementHost];
			workspace.Close(smartPart);

			Assert.IsTrue(form.IsDisposed, "Form not disposed");
			Assert.IsFalse(form.Visible, "Form is visible");
			Assert.IsFalse(smartPart.IsDisposed);
		}

		[TestMethod]
		public void CloseRemovesEntriesInWindowsAndSmartParts()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			workspace.Show(smartPart);

			workspace.Close(smartPart);

			Assert.AreEqual(0, workspace.Windows.Count);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void CloseNonExistControlThrows()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			workspace.Close(smartPart);
		}

		[TestMethod]
		public void WorkspaceFiresSmartPartClosing()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			bool closing = false;
			workspace.Show(smartPart);
			workspace.SmartPartClosing += delegate { closing = true; };

			workspace.Close(smartPart);

			Assert.IsTrue(closing);
		}

		[TestMethod]
		public void CanCancelSmartPartClosing()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			workspace.Show(smartPart);
			workspace.SmartPartClosing += delegate(object sender, WorkspaceCancelEventArgs args) { args.Cancel = true; };

			workspace.Close(smartPart);

			Assert.IsFalse(smartPart.IsDisposed, "Smart Part Was Disposed");
		}

		[TestMethod]
		public void ClosingIsCalledWhenClosed()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			bool closing = false;
			workspace.Show(smartPart);
			workspace.SmartPartClosing += delegate { closing = true; };

			workspace.Windows[smartPart.ElementHost].Close();

			Assert.IsTrue(closing);
		}

		[TestMethod]
		public void ClosingDoesNotFireIfNoControlsOnForm()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			bool closing = false;
			workspace.Show(smartPart);
			workspace.SmartPartClosing += delegate { closing = true; };

			workspace.Windows[smartPart.ElementHost].Controls.Clear();

			Assert.IsFalse(closing);
		}

		[TestMethod]
		public void ClosedIsCalledWhenClosed()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			bool closed = false;
			workspace.Show(smartPart);
			workspace.SmartPartClosing += delegate { closed = true; };

			workspace.Windows[smartPart.ElementHost].Close();

			Assert.IsTrue(closed);
		}

		[TestMethod]
		public void CloseRemovesWindow()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			workspace.Show(smartPart);
			workspace.Close(smartPart);

			Assert.IsFalse(workspace.Windows.ContainsKey(smartPart.ElementHost));
		}

		[TestMethod]
		public void ClosedDoesNotFireIfNoControlsOnForm()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			bool closing = false;
			workspace.Show(smartPart);
			workspace.SmartPartClosing += delegate { closing = true; };

			workspace.Windows[smartPart.ElementHost].Controls.Clear();

			Assert.IsFalse(closing);
		}

		[TestMethod]
		public void CanCancelCloseWhenFormClose()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			workspace.Show(smartPart);
			workspace.SmartPartClosing += delegate(object sender, WorkspaceCancelEventArgs args) { args.Cancel = true; };

			workspace.Windows[smartPart.ElementHost].Close();

			Assert.IsFalse(smartPart.IsDisposed);
		}

		[TestMethod]
		public void CloseSmartPartDoesNotDisposeIt()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			workspace.Show(smartPart);
			workspace.Close(smartPart);

			Assert.IsFalse(smartPart.IsDisposed);
		}

		#endregion

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
		public void ControlIsRemovedWhenSmartPartIsDisposed()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPart = workItem.SmartParts.AddNew<MockWPFSmartPart>();

			workspace.Show(smartPart);
			Assert.AreEqual(1, workspace.Windows.Count);

			smartPart.Dispose();

			Assert.AreEqual(0, workspace.Windows.Count);
			Assert.AreEqual(0, workspace.SmartParts.Count);
		}

		[TestMethod]
		public void SPIsRemovedFromWorkspaceWhenDisposed()
		{
			TestableRootWorkItem workItem = new TestableRootWorkItem();
			WindowWorkspace workspace = workItem.Workspaces.AddNew<WindowWorkspace>();
			MockWPFSmartPart smartPartA = workItem.SmartParts.AddNew<MockWPFSmartPart>();
			workspace.Show(smartPartA);
			bool Called = false;

			workspace.SmartPartClosing += delegate(object s, WorkspaceCancelEventArgs e)
			{
				Called = true;
			};

			smartPartA.Dispose();

			Assert.IsFalse(Called);
			Assert.AreEqual(0, workspace.Windows.Count);
			Assert.AreEqual(0, workspace.SmartParts.Count);
		}



		//#region Helper classes

		//[SmartPart]
		//class MockWPFSmartPart : Control
		//{
		//    private SmartPartInfo info = new SmartPartInfo();

		//    public MockWPFSmartPart()
		//    {
		//        info.Title = "Smart Part";
		//    }
		//}

		//#endregion
	}
}
