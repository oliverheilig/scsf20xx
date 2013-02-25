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
using System.Data;
using System.Data.Common;

namespace Microsoft.Practices.TestDataAccess
{
	public class MockConnection : DbConnection
	{
		private string connectionString;
		private bool isOpen = false;

		protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
		{
			return null;
		}

		public override void ChangeDatabase(string databaseName)
		{
		}

		public override void Close()
		{
			isOpen = false;
		}

		public override string ConnectionString
		{
			get { return connectionString; }
			set { connectionString = value; }
		}

		protected override DbCommand CreateDbCommand()
		{
			return new MockCommand();
		}

		public override string DataSource
		{
			get { return null; }
		}

		public override string Database
		{
			get { return null; }
		}

		public override void Open()
		{
			isOpen = true;
		}

		public override string ServerVersion
		{
			get { return null; }
		}

		public override ConnectionState State
		{
			get
			{
				if (isOpen)
					return ConnectionState.Open;
				else
					return ConnectionState.Closed;
			}
		}
	}
}