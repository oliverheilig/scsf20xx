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
using GlobalBank.CreditCardAccounts.Interface.Constants;
using GlobalBank.Infrastructure.Interface;
using GlobalBank.Infrastructure.Interface.BusinessEntities;
using GlobalBank.Infrastructure.Interface.Services;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;
using BranchSystemsConstants = GlobalBank.BranchSystems.Interface.Constants;

namespace GlobalBank.CreditCardAccounts.Module
{
	public class ModuleController : WorkItemController
	{
		private WorkItem _customerWorkItem;

		[EventSubscription(EventTopicNames.CustomerWorkItemCreated, ThreadOption.UserInterface)]
		public void OnCustomerWorkItemCreated(object sender, EventArgs<WorkItem> args)
		{
			_customerWorkItem = args.Data;
			_customerWorkItem.Items.AddNew<ModuleActions>();
		}

		[EventSubscription(BranchSystemsConstants.EventTopicNames.CreditCardAccountOpen, ThreadOption.UserInterface)]
		public void OnCreditCardAccountOpen(object sender, EventArgs<CreditCard> args)
		{
			IActionCatalogService catalog = _customerWorkItem.Services.Get<IActionCatalogService>();
			catalog.Execute(ActionNames.ShowCreditCardAccountViewAction, _customerWorkItem, this, args.Data);
		}
	}
}