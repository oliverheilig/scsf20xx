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
using System.Windows.Forms;
using GlobalBank.BranchSystems.Module.Constants;
using GlobalBank.BranchSystems.Module.Views;
using GlobalBank.BranchSystems.Properties;
using GlobalBank.Infrastructure.Interface;
using Microsoft.Practices.CompositeUI;

namespace GlobalBank.BranchSystems.Module
{
	public class ModuleActions
	{
		private WorkItem _workItem = null;

		[ServiceDependency]
		public WorkItem WorkItem
		{
			get { return _workItem; }
			set { _workItem = value; }
		}

		[Action(ActionNames.ShowCustomerQueueManagementView)]
		public void ShowCustomerQueueManagementView(object caller, object target)
		{
			CustomerQueueManagementView view = WorkItem.Items.AddNew<CustomerQueueManagementView>();
			WorkItem.Workspaces[WorkspaceNames.LaunchBarWorkspace].Show(view);
			WorkItem.RootWorkItem.UIExtensionSites.RegisterSite(UIExtensionSiteNames.CustomerQueueLinks, view.LinksPanel);
		}

		[Action(ActionNames.ShowOfficerQueueView)]
		public void ShowOfficerQueueView(object caller, object target)
		{
			OfficerPanelView view = WorkItem.SmartParts.AddNew<OfficerPanelView>("OfficerPanelView");
			WorkItem.Workspaces[WorkspaceNames.LaunchBarWorkspace].Show(view);
		}

		[Action(ActionNames.ActivateOfficerQueueView)]
		public void ActivateOfficerQueueView(object caller, object target)
		{
			OfficerPanelView view = WorkItem.SmartParts.Get<OfficerPanelView>("OfficerPanelView");
			WorkItem.Workspaces[WorkspaceNames.LaunchBarWorkspace].Activate(view);
		}

		[Action(ActionNames.ShowFindCustomerCommand)]
		public void FindCustomerAction(object caller, object target)
		{
			if (WorkItem.UIExtensionSites.Contains(UIExtensionSiteNames.CustomerQueueLinks))
			{
				UIExtensionSite site = WorkItem.UIExtensionSites[UIExtensionSiteNames.CustomerQueueLinks];
				CreateAndRegisterLinkLabel(site, Resources.FindCustomerLabelText, CommandNames.FindCustomer);
			}
		}

		[Action(ActionNames.ShowAddVisitorToQueueCommand)]
		public void AddVisitorToQueueAction(object caller, object target)
		{
			if (WorkItem.UIExtensionSites.Contains(UIExtensionSiteNames.CustomerQueueLinks))
			{
				UIExtensionSite site = WorkItem.UIExtensionSites[UIExtensionSiteNames.CustomerQueueLinks];
				CreateAndRegisterLinkLabel(site, Resources.QueueVisitorLabelText, CommandNames.EnqueueVisitor);
			}
		}

		[Action(ActionNames.ServiceCustomerAction)]
		public void ServiceCustomerAction(object caller, object target)
		{
			if (WorkItem.UIExtensionSites.Contains(UIExtensionSiteNames.CustomerQueueLinks))
			{
				UIExtensionSite site = WorkItem.UIExtensionSites[UIExtensionSiteNames.CustomerQueueLinks];
				CreateAndRegisterLinkLabel(site, Resources.ServiceCustomerLabelText, CommandNames.ServiceCustomer);
			}
		}

		private void CreateAndRegisterLinkLabel(UIExtensionSite site, string label, string command)
		{
			LinkLabel linkLabel = new LinkLabel();
			linkLabel.Text = label;
			site.Add(linkLabel);
			linkLabel.Margin = new Padding(0);

			WorkItem.Commands[command].AddInvoker(linkLabel, "Click");
		}
	}
}