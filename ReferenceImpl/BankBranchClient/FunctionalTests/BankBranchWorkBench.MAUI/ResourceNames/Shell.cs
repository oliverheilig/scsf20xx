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

namespace BankBranchWorkBench.MAUI.ResourceNames
{
	public class Shell
	{
		public const string WindowTitle = "Global Bank Branch WorkBench";

        //ListView
        public const string CustomerQueueListBox = "_visitorQueueListBox";
        public const string MyCustomerQueueListBox = "_officerQueueListBox";

		//Labels
		public const string CustomerQueueLabelName = "Customer Queue";
		public const string MyCustomerQueueLabelName = "My Customers";

		//Buttons
		public const string FindCustomerButtonName = "Find Customer";
		public const string AddNewCustomertoQueueButtonName = "Add Visitor to Queue";
		public const string ServiceCustomerButtonName = "Service Customer";

		//ToolStrip
		public const string LaunchBarToolStripName = "toolStrip";
        public const string UserNameToolStrip = "_userLabel";
        public const string RoleNameToolStrip = "_rolesLabel";
        public const string MainStatusStrip = "statusStrip1";
	}
}
