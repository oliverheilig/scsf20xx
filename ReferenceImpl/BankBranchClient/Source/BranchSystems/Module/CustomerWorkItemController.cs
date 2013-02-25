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
using GlobalBank.BranchSystems.Interface.Constants;
using GlobalBank.BranchSystems.Module.Views;
using GlobalBank.Infrastructure.Interface;
using GlobalBank.Infrastructure.Interface.BusinessEntities;
using GlobalBank.Infrastructure.Interface.Services;
using GlobalBank.Infrastructure.Library.ActionConditions;
using GlobalBank.Infrastructure.Library.Services;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace GlobalBank.BranchSystems.Module
{
	public class CustomerWorkItemController : WorkItemController
	{
		private QueueEntry _queueEntry = null;
        private IWorkspace _workspace = null;
		private IOfficerView _officerView = null;

		public void Run(QueueEntry queueEntry, IWorkspace workspace)
		{
			IActionCatalogService catalog = WorkItem.Services.AddNew<ActionCatalogService, IActionCatalogService>();
			catalog.RegisterGeneralCondition(new EnterpriseLibraryAuthorizationActionCondition());

			WorkItem.Items.Add(queueEntry, "QueueEntry");
			WorkItem.Activated += new EventHandler(WorkItem_Activated);

			_queueEntry = queueEntry;
			_workspace = workspace;
			_officerView = WorkItem.SmartParts.AddNew<OfficerView>();

			// Show the summary as default
			CustomerSummaryView summary = WorkItem.SmartParts.AddNew<CustomerSummaryView>();
			WorkItem.Workspaces[WorkspaceNames.OfficerWorkspace].Show(summary);
		}

		void WorkItem_Activated(object sender, EventArgs e)
		{
			_workspace.Show(_officerView);			
		}
	}
}
