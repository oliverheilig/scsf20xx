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
using System.Collections.Specialized;
using System.Text;
using Maui.Core;
using Maui.Core.WinControls;
using Maui.Core.Utilities;
using BankBranchWorkBench.MAUI.Common;

namespace BankBranchWorkBench.MAUI
{
	public class ShellAdapter
	{
		#region Variables and Properties

		private Window shellWindow;

		public int LauchBarIconCount
		{
			get
			{
				return LauchBartToolStrip.ToolStripItems.Count;
			}
		}

		private ListBox CustomerQueueListBox
		{
			get
			{
				return MAUIUtilities.GetControl<ListBox>(shellWindow,
						ResourceNames.Shell.CustomerQueueListBox);
			}
		}

		private ListBox MyCustomerQueueListBox
		{
			get
			{
				return MAUIUtilities.GetControl<ListBox>(shellWindow,
						ResourceNames.Shell.MyCustomerQueueListBox);
			}
		}

		private ToolStrip LauchBartToolStrip
		{
			get
			{
				return new ToolStrip(shellWindow, ResourceNames.Shell.LaunchBarToolStripName);
			}
		}

		private Button FindCustomerButton
		{
			get
			{
				return MAUIUtilities.GetControl<Button>(shellWindow,
						ResourceNames.Shell.FindCustomerButtonName);
			}
		}

		private Button AddNewCustomertoQueueButton
		{
			get
			{
				return MAUIUtilities.GetControl<Button>(shellWindow,
						ResourceNames.Shell.AddNewCustomertoQueueButtonName);
			}
		}

		private Button ServiceCustomerButton
		{
			get
			{
				return MAUIUtilities.GetControl<Button>(shellWindow,
						ResourceNames.Shell.ServiceCustomerButtonName);
			}
		}


        private ToolStrip MainStatusBar
        {
            get
            {

                return new ToolStrip(shellWindow,
						ResourceNames.Shell.MainStatusStrip);
			
            }
            
        }
		public bool ServiceCustomerEnabled
		{
			get
			{
				return ServiceCustomerButton.IsEnabled;
			}
		}

		public bool CustomerQueueDisplayed
		{
			get
			{
				try
				{
					//Labels
					MAUIUtilities.GetControl<StaticControl>(shellWindow,
							ResourceNames.Shell.CustomerQueueLabelName, StringMatchSyntax.ExactMatch,
							"*", StringMatchSyntax.WildCard);

					//ListBox
					MAUIUtilities.GetControl<ListBox>(shellWindow,
							ResourceNames.Shell.CustomerQueueListBox);
				}
				catch
				{
					return false;
				}

				return true;
			}
		}

		public bool MyCustomerQueueDisplayed
		{
			get
			{
				try
				{
					//Labels
					MAUIUtilities.GetControl<StaticControl>(shellWindow,
							ResourceNames.Shell.MyCustomerQueueLabelName, StringMatchSyntax.ExactMatch,
							"*", StringMatchSyntax.WildCard);

					//ListBox
					MAUIUtilities.GetControl<ListBox>(shellWindow,
							ResourceNames.Shell.MyCustomerQueueListBox);
				}
				catch
				{
					return false;
				}

				return true;
			}
		}

		#endregion

		#region LifeTime

		public ShellAdapter()
		{
			shellWindow = MAUIUtilities.GetWindow(BankWorkBench.Application,
					ResourceNames.Shell.WindowTitle);
		}

		public void Close()
		{
			BankWorkBench.Close();
			shellWindow = null;
		}

		public bool Closed
		{
			get
			{
				return shellWindow == null;
			}
		}

		#endregion

		#region Screen Operations

		public void CheckInitialShellView()
		{
			//Buttons
			MAUIUtilities.GetControl<Button>(shellWindow,
					ResourceNames.Shell.FindCustomerButtonName);
            //MAUIUtilities.GetControl<Button>(shellWindow,
            //                ResourceNames.Shell.ServiceCustomerButtonName);

			//Labels
			MAUIUtilities.GetControl<StaticControl>(shellWindow,
					ResourceNames.Shell.MyCustomerQueueLabelName, StringMatchSyntax.ExactMatch,
					"*", StringMatchSyntax.WildCard);

			//ListBox
			MAUIUtilities.GetControl<ListBox>(shellWindow,
					ResourceNames.Shell.MyCustomerQueueListBox);

			//ToolStrips
			new ToolStrip(shellWindow, ResourceNames.Shell.LaunchBarToolStripName);
            
		}

		public void CheckForServiceCustomerLink()
		{
			//Buttons
			MAUIUtilities.GetControl<Button>(shellWindow,
					ResourceNames.Shell.ServiceCustomerButtonName);
		}

		public AddCustomerToQueueAdapter AddNewCustomerToQueue()
		{
			CustomerQueue();
			AddNewCustomertoQueueButton.Click();
            Sleeper.Delay(500);
			return new AddCustomerToQueueAdapter();
		}

		public FindCustomerAdapter FindCustomer()
		{
			CustomerQueue();
			FindCustomerButton.Click();
            Sleeper.Delay(500);
			return new FindCustomerAdapter();
		}

		public StringCollection GetCustomersInQueue()
		{
			StringCollection customersInQueue = new StringCollection();

			foreach (ListBoxItem queueItem in CustomerQueueListBox.Items)
			{
				customersInQueue.Add(queueItem.Text);
			}

			return customersInQueue;
		}

		public StringCollection GetCustomersInMyQueue()
		{

            Sleeper.Delay(3000);
            StringCollection customersInQueue = new StringCollection();

			foreach (ListBoxItem queueItem in MyCustomerQueueListBox.Items)
			{
				customersInQueue.Add(queueItem.Text);
			}

			return customersInQueue;
		}

		public RightPaneAdapter ServiceCustomer()
		{
			ServiceCustomerButton.Click();
            Sleeper.Delay(500);
			return new RightPaneAdapter(shellWindow);
		}

        public RightPaneAdapter GetRightPaneAdapter()
        {
            Sleeper.Delay(500);
            return new RightPaneAdapter(shellWindow);
        }

		public void CustomerQueue()
		{
			LauchBartToolStrip[0, true].Click();
		}

		public void MyCustomerQueue()
		{
			LauchBartToolStrip[1, true].Click();
		}

        public string StatusBarValue(string LabelName)
        {
            return MainStatusBar[LabelName].Text;            
        }       

		#endregion

	}
}
