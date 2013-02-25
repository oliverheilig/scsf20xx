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
using BankBranchWorkBench.MAUI.Common;
using Maui.Core;
using Maui.Core.WinControls;
using Maui.Core.Utilities;

namespace BankBranchWorkBench.MAUI
{
	public class RightPaneAdapter
	{
		#region Variables and Properties

		private Window shellWindow;

		private TabControl CustomerDetailsTabConrol
		{
			get
			{
				return MAUIUtilities.GetControl<TabControl>(shellWindow,
								ResourceNames.RightPane.CustomerDetailsTabConrol);
			}
		}

		private Button PurchaseCDButton
		{
			get
			{
				return MAUIUtilities.GetControl<Button>(shellWindow,
						ResourceNames.RightPane.PurchaseCDButtonName);
			}
		}

		public string CustomerNameOnRightPane
		{
			get
			{
				return MAUIUtilities.GetControl<StaticControl>(shellWindow,
								ResourceNames.RightPane.RightPaneCaptionLabel).Text;
			}
		}

		#endregion

		#region LifeTime

		public RightPaneAdapter(Window shellWindow)
		{
			this.shellWindow = shellWindow;
		}

		#endregion

		#region Screen Operations

		public void CheckPresenceOfControls()
		{
			//Labels
			MAUIUtilities.GetControl<StaticControl>(shellWindow,
					ResourceNames.RightPane.RightPaneCaptionLabel);

			//Tab Controls
			MAUIUtilities.GetControl<TabControl>(shellWindow,
					ResourceNames.RightPane.CustomerDetailsTabConrol);

			//Link Labels
			MAUIUtilities.GetControl<Button>(shellWindow,
					ResourceNames.RightPane.PurchaseCDButtonName);
		}

		public CustomerSummaryAdapter CustomerSummary()
		{
			return new CustomerSummaryAdapter(shellWindow, CustomerDetailsTabConrol);
		}

		public PurchaseCDAdapter PurchaseCD()
		{
			PurchaseCDButton.Click();
            Sleeper.Delay(4000);
			return new PurchaseCDAdapter(shellWindow, CustomerDetailsTabConrol);
		}

       
		#endregion
	}
}
