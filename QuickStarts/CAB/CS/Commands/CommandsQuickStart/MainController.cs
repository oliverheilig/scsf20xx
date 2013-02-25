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
using Microsoft.Practices.CompositeUI.Commands;

namespace CommandsQuickStart
{
	// This is the controller for the main work item
	// and holds all commands handlers for this sample quickstart.
	public class MainController : Controller
	{
		[CommandHandler("ShowCustomer")]
		public void ShowCustomerHandler(object sender, EventArgs e)
		{
			MessageBox.Show("Show Customer");
		}

		[CommandHandler("EnableShowCustomer")]
		public void EnableShowCustomerHandler(object sender, EventArgs e)
		{
			WorkItem.Commands["ShowCustomer"].Status = CommandStatus.Enabled;
		}

		[CommandHandler("DisableShowCustomer")]
		public void DisableShowCustomer(object sender, EventArgs e)
		{
			WorkItem.Commands["ShowCustomer"].Status = CommandStatus.Disabled;
		}

		[CommandHandler("HideShowCustomer")]
		public void HideShowCustomer(object sender, EventArgs e)
		{
			WorkItem.Commands["ShowCustomer"].Status = CommandStatus.Unavailable;
		}
	}
}
