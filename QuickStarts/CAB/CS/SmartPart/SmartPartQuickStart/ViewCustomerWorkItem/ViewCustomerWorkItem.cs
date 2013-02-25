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

using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.WinForms;

namespace SmartPartQuickStart.ViewCustomerWorkItem
{
	/// <summary>
	/// A WorkItem to handle the viewing of one customer.
	/// </summary>
	public class ViewCustomerWorkItem : WorkItem
	{
		private CustomerTabView tabView;
		private CustomerCommentsView commentsView;
		private CustomerSummaryView customerSummary;

		/// <summary>
		/// Starts the workitem.
		/// </summary>
		/// <param name="workspace"></param>
		public void Run(IWorkspace workspace)
		{
			//Create views to be used by workitem
			CreateSummaryView();
			CreateTabView();

			//Make the tabview visible in the workspace.
			workspace.Show(tabView);
		}

		/// <summary>
		/// Shows the custoemr comments in a new tab.
		/// </summary>
		public void ShowCustomerComments()
		{
			//retrieve the tab workspace from the workitem.
			//"tabWorkspace1" is the name of the control.
			IWorkspace tabbedSpace = Workspaces["tabWorkspace1"];

			if (tabbedSpace != null)
			{
				if (commentsView == null)
				{
					commentsView = this.Items.AddNew<CustomerCommentsView>("CustomerCommentsView");
					ISmartPartInfo info = new SmartPartInfo();
					info.Title = "Comments";
					this.RegisterSmartPartInfo(commentsView, info);
				}
				//The "Show" of the tabworkspace creates 
				//a new tab and show the comments view.
				tabbedSpace.Show(commentsView);
			}
		}

		/// <summary>
		/// State that is inject in the workitem.
		/// The State is set this way so child items
		/// can get inject with the state.
		/// </summary>
		public Customer Customer
		{
			get { return (Customer)State["Customer"]; }
			set { State["Customer"] = value; }
		}

		private void CreateSummaryView()
		{
			if (customerSummary == null)
			{
				customerSummary = this.Items.AddNew<CustomerSummaryView>("CustomerSummary");
			}
		}

		private void CreateTabView()
		{
			if (tabView == null)
			{
				tabView = this.Items.AddNew<CustomerTabView>("CustomerView");
			}
		}


	}
}
