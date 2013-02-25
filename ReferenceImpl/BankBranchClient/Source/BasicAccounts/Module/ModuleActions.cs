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
using System.Windows.Forms;
using GlobalBank.BasicAccounts.Module;
using GlobalBank.BasicAccounts.Module.Constants;
using GlobalBank.BasicAccounts.Properties;
using GlobalBank.Infrastructure.Interface;
using GlobalBank.Infrastructure.Interface.BusinessEntities;
using GlobalBank.Infrastructure.Interface.Services;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;

namespace GlobalBank.BasicAccounts
{
	public class ModuleActions
	{
		private WorkItem _workItem;
		private QueueEntry _queueEntry;
		private IActionCatalogService _actionCatalog;
		private const string PurchaseCDViewKey = "PurchaseCDViewKey";

		[InjectionConstructor]
		public ModuleActions
			(
			[ServiceDependency] WorkItem workItem,
			[ServiceDependency] IActionCatalogService actionCatalog,
			[ComponentDependency("QueueEntry")] QueueEntry queueEntry
			)
		{
			_workItem = workItem;
			_queueEntry = queueEntry;
			_actionCatalog = actionCatalog;
		}

		[Action(ActionNames.ShowPurchaseCDCommand)]
		public void ShowPurchaseCDCommandAction(object caller, object target)
		{
			if (_queueEntry.Person == null || !(_queueEntry.Person is Customer))
				return;

			UIExtensionSite site = _workItem.UIExtensionSites[UIExtensionSiteNames.ProductsPanel];
			LinkLabel linkLabel = new LinkLabel();
			linkLabel.Text = Resources.PurchaseCDLabelText;
			site.Add(linkLabel);
			linkLabel.Margin = new Padding(0);

			linkLabel.Click += delegate(object sender, EventArgs args)
			                   	{
			                   		_actionCatalog.Execute(ActionNames.ShowPurchaseCDView, _workItem, this, null);
			                   	};
		}

		[Action(ActionNames.ShowPurchaseCDView)]
		public void ShowPurchaseCDViewAction(object caller, object target)
		{
			IPurchaseCDView purchaseCDView = null;
			if (_workItem.SmartParts.Contains(PurchaseCDViewKey) == false)
			{
				purchaseCDView = _workItem.SmartParts.AddNew<PurchaseCDView>(PurchaseCDViewKey);
			}
			else
			{
				purchaseCDView = _workItem.SmartParts.Get<PurchaseCDView>(PurchaseCDViewKey);
			}
			_workItem.Workspaces[WorkspaceNames.OfficerWorkspace].Show(purchaseCDView);
		}
	}
}