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
using System.Text;
using Microsoft.Practices.CompositeUI;
using BankTellerModule;
using Microsoft.Practices.CompositeUI.WinForms;
using Microsoft.Practices.CompositeUI.SmartParts;
using System.Windows.Forms;
using BankTellerCommon;
using Microsoft.Practices.CompositeUI.UIElements;

namespace CustomerMapExtensionModule
{
	[WorkItemExtension(typeof(CustomerWorkItem))]
	public class CustomerWorkItemExtension : WorkItemExtension
	{
		private CustomerMap mapView;

		protected override void OnActivated()
		{
			if (mapView == null)
			{
				mapView = WorkItem.Items.AddNew<CustomerMap>();

				TabSmartPartInfo info = new TabSmartPartInfo();
				info.Title = "Customer Map";
				info.Description = "Map of the customer location";
				WorkItem.Workspaces[CustomerWorkItem.CUSTOMERDETAIL_TABWORKSPACE].Show(mapView, info);
			}
		}
	}
}
