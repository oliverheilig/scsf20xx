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

using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using BankTellerCommon;
using Microsoft.Practices.CompositeUI.Utility;

namespace BankTellerModule
{
	// The CustomerQueueController is the controller used by CustomerQueueView.
	// The queue view displays a list of customers in your queue, so the user can
	// select a customer and view/edit the details of them.

	public class CustomerQueueController : Controller
	{
		// We depend on the customer queue service to tell us which customer is next
		private CustomerQueueService customerQueueService;

		[ServiceDependency]
		public CustomerQueueService CustomerQueueService
		{
			set { customerQueueService = value; }
		}

		public new BankTellerWorkItem WorkItem
		{
			get { return base.WorkItem as BankTellerWorkItem; }
		}

		public Customer GetNextCustomerInQueue()
		{
			return customerQueueService.GetNext();
		}

		public void WorkWithCustomer(Customer customer)
		{
			WorkItem.WorkWithCustomer(customer);
		}
	}
}
