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
using System.Globalization;
using GlobalBank.CreditCardAccounts.Interface.Constants;
using GlobalBank.Infrastructure.Interface;
using GlobalBank.Infrastructure.Interface.BusinessEntities;
using GlobalBank.Infrastructure.Interface.Services;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;

namespace GlobalBank.CreditCardAccounts.Module
{
	public class ModuleActions
	{
		private WorkItem _workItem;
		private QueueEntry _queueEntry;
		private IActionCatalogService _actionCatalog;
		private const string CreditCardAccountViewKey = "CreditCardAccountViewKey{0}";

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

		[Action(ActionNames.ShowCreditCardAccountViewAction)]
		public void ShowCreditCardAccountViewAction(object caller, object target)
		{
			CreditCard selected = target as CreditCard;
			string creditCardViewKey =
				String.Format(CultureInfo.InvariantCulture, CreditCardAccountViewKey, selected.CreditCardNumber);
			if (selected != null)
			{
				AddCreditCardToWorkitem(selected);
				ShowCreditCardView(creditCardViewKey);
			}
		}

		private void ShowCreditCardView(string creditCardViewKey)
		{
			ICreditCardAccountView creditCardAccountView = null;
			if (_workItem.SmartParts.Contains(creditCardViewKey) == false)
			{
				creditCardAccountView = _workItem.SmartParts.AddNew<CreditCardAccountView>(creditCardViewKey);
			}
			else
			{
				creditCardAccountView = _workItem.SmartParts.Get<CreditCardAccountView>(creditCardViewKey);
			}
			_workItem.Workspaces[WorkspaceNames.OfficerWorkspace].Show(creditCardAccountView);
		}

		private void AddCreditCardToWorkitem(CreditCard selected)
		{
			object existing = _workItem.Items.Get("CreditCard");
			if (existing != null)
				_workItem.Items.Remove(existing);

			_workItem.Items.Add(selected, "CreditCard");
		}
	}
}