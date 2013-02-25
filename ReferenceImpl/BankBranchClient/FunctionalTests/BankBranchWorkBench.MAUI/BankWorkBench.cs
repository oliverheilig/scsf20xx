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
using Maui.Core;
using BankBranchWorkBench.MAUI.Common;

namespace BankBranchWorkBench.MAUI
{
	static class BankWorkBench
	{
		private static App application;

		public static void Kill()
		{
			if (application != null)
			{
				application.Kill();
				application = null;
			}
		}

		public static App Application
		{
			get
			{
				if (application == null)
				{
					application = StartWorkBench();
				}
				return application;
			}
		}

		private static App StartWorkBench()
		{
			try
			{
				App workBench = new App(Utilities.BankBranchApplicationPath);
				return workBench;
			}
			catch (InvalidOperationException ex)
			{
				throw new InvalidOperationException("Invalid Application Path Specified", ex);
			}
		}

		public static void Close()
		{
			application.Kill();
			application = null;
		}
	}
}
