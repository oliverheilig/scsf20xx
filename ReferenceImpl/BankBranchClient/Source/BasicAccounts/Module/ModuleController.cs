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
using GlobalBank.BasicAccounts.Module.Constants;
using GlobalBank.Infrastructure.Interface;
using GlobalBank.Infrastructure.Interface.Services;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;

namespace GlobalBank.BasicAccounts.Module
{
	public class ModuleController : WorkItemController
	{
		[EventSubscription(EventTopicNames.CustomerWorkItemCreated, ThreadOption.UserInterface)]
		public void OnCustomerWorkItemCreated(object sender, EventArgs<WorkItem> args)
		{
			WorkItem customerWorkItem = args.Data;
			customerWorkItem.Items.AddNew<ModuleActions>();
			IActionCatalogService catalog = customerWorkItem.Services.Get<IActionCatalogService>();
			catalog.Execute(ActionNames.ShowPurchaseCDCommand, customerWorkItem, this, null);
		}
	}
}