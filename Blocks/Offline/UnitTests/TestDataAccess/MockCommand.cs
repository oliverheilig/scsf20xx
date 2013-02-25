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
	public class MockCommand : DbCommand
	{
		private CommandType commandType = CommandType.Text;
		private string commandText;
		private int commandTimeout;

		public override void Cancel()
		{
		}

		public override string CommandText
		{
			get { return commandText; }
			set { commandText = value; }
		}

		public override int CommandTimeout
		{
			get { return commandTimeout; }
			set { commandTimeout = value; }
		}

		public override CommandType CommandType
		{
			get { return commandType; }
			set { commandType = value; }
		}

		protected override DbParameter CreateDbParameter()
		{
			return new MockParameter();
		}

		protected override DbConnection DbConnection
		{
			get { return null; }
			set { }
		}

		protected override DbParameterCollection DbParameterCollection
		{
			get { return null; }
		}

		protected override DbTransaction DbTransaction
		{
			get { return null; }
			set { }
		}

		public override bool DesignTimeVisible
		{
			get { return false; }
			set { }
		}

		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			return null;
		}

		public override int ExecuteNonQuery()
		{
			return 0;
		}

		public override object ExecuteScalar()
		{
			return 0;
		}

		public override void Prepare()
		{
		}

		public override UpdateRowSource UpdatedRowSource
		{
			get { return UpdateRowSource.None; }
			set { }
		}
	}
}