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
using System.IO;
using System.Reflection;
using System.Threading;
using Maui.Core.WinControls;

namespace BankBranchWorkBench.MAUI.Common
{
	public static class Utilities
	{
		private static string _bankBranchApplicationPath;

		public static string BankBranchApplicationPath
		{
			get
			{
				if (_bankBranchApplicationPath == null)
				{
					string basePath = new Uri(Assembly.GetExecutingAssembly().Location).LocalPath;
					basePath = Path.GetDirectoryName(basePath);
                    _bankBranchApplicationPath = Path.Combine(basePath, "GlobalBank.Shell.exe");
				}
				return _bankBranchApplicationPath;
			}
		}

		public static bool CheckListViewColumnHeaders(ListView list, string[] columnNames)
		{
			if (list.ColumnCount != columnNames.Length)
				return false;

			for (int index = 0; index < list.Columns.Count; index++)
				if (list.Columns[index].Text != columnNames[index])
					return false;

			return true;
		}

		public static bool CheckDataGridViewColumnHeaders(DataGridView grid, string[] columnNames)
		{
			if (grid.ColumnHeaders.Count != columnNames.Length)
				return false;

			for (int index = 0; index < columnNames.Length; index++)
			{
				if (grid.ColumnHeaders[index].Name != columnNames[index])
					return false;
			}

			return true;
		}


	}
}
