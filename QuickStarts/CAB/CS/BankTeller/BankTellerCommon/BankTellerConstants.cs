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

namespace BankTellerCommon
{
	public class UIExtensionConstants
	{
		public static readonly string CUSTOMERCONTEXT = "CustomerContext";

		public static readonly string FILE = "File";

		public static readonly string MAINMENU = "MainMenu";

		public static readonly string FILEDROPDOWN = "FileDropDown";

		public static readonly string MAINSTATUS = "MainStatus";

		public static readonly string QUEUE = "Queue";

		public static readonly string CUSTOMER = "Customer";
	}

	/// <summary>
	/// Used for creating and handling commands.
	/// These are constants because they are used in attributes.
	/// </summary>
	public class CommandConstants
	{
		public const string ACCEPT_CUSTOMER = "QueueAcceptCustomer";

		public const string EDIT_CUSTOMER = "EditCustomer";

		public const string CUSTOMER_MOUSEOVER = "CustomerMouseOver";
	}

	/// <summary>
	/// Used for handling State.
	/// These are constants because they are used in attributes.
	/// </summary>
	public class StateConstants
	{
		public const string CUSTOMER = "Customer2";
	}

	public class WorkspacesConstants
	{
		public static readonly string SHELL_SIDEBAR = "sideBarWorkspace";
		public static readonly string SHELL_CONTENT = "contentWorkspace";
	}
}
