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

using System;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using BankTellerCommon;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.ObjectBuilder;

namespace BankTellerModule
{
	[SmartPart]
	public partial class CustomerDetailView : UserControl
	{
		private Customer customer;
		private WorkItem parentWorkItem;
		private CustomerDetailController controller;

		public CustomerDetailView()
		{
			InitializeComponent();
		}

		[ServiceDependency]
		public WorkItem ParentWorkItem
		{
			set { parentWorkItem = value; }
		}

		// The Customer state is stored in our parent work item
		[State]
		public Customer Customer
		{
			set { customer = value; }
		}

		// We use our controller so we can show the comments page
		[CreateNew]
		public CustomerDetailController Controller
		{
			set { controller = value; }
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			if (customer != null)
			{
				this.customerBindingSource.Add(customer);
			}
		}

		private void OnShowComments(object sender, EventArgs e)
		{
			controller.ShowCustomerComments();
		}
	}
}
