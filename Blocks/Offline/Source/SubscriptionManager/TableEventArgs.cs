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

namespace Microsoft.Practices.SmartClient.Subscriptions
{
	/// <summary>
	///		The event arguments that are passed when the download status of a table changes.
	/// </summary>
	public class TableEventArgs : EventArgs
	{
		private string tableName;

		/// <summary>
		///		Constructor
		/// </summary>
		/// <param name="tableName">Name of the table that is being or was syncrhonized.</param>
		public TableEventArgs(string tableName)
		{
			this.tableName = tableName;
		}

		/// <summary>
		///		Gets the name of the table that is being or was syncrhonized.
		/// </summary>
		public string TableName
		{
			get { return tableName; }
		}
	}
}
