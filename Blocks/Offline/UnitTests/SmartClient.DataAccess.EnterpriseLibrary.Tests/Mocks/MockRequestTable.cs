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
using System.Data;

namespace Microsoft.Practices.SmartClient.EnterpriseLibrary.Tests.Mocks
{
	internal class MockRequestTable : DataTable
	{
		public MockRequestTable()
		{
			DataColumn pk = new DataColumn("RequestId", typeof (Guid));
			Columns.Add(pk);
			PrimaryKey = new DataColumn[] {pk};

			AddColumn("EndPoint", typeof (string), 300);
			AddColumn("ProxyFactoryType", typeof (string), 1000);
			AddColumn("OnlineProxyType", typeof (string), 1000);
			AddColumn("MethodName", typeof (string), 200);
			AddColumn("Tag", typeof (string), 300);
			AddColumn("Stamps", typeof (int));
			AddColumn("ReturnTargetType", typeof (string), 1000);
			AddColumn("ReturnMethodName", typeof (string), 200);
			AddColumn("ExceptionTargetType", typeof (string), 1000);
			AddColumn("ExceptionMethodName", typeof (string), 200);
			AddColumn("MaxRetries", typeof (int));
			AddColumn("MessageId", typeof (Guid));
			AddColumn("CallParameters", typeof (string));
			AddColumn("Sequence", typeof (int)).AutoIncrement = true;
			//AddColumn("Status", typeof(int));
			AddColumn("QueuedDate", typeof (DateTime));
			AddColumn("Expiration", typeof (DateTime));
		}

		private DataColumn AddColumn(string columnName, Type type, int size)
		{
			DataColumn column = new DataColumn(columnName, type);
			if (size > 0)
				column.MaxLength = size;
			Columns.Add(column);
			return column;
		}

		private DataColumn AddColumn(string columnName, Type type)
		{
			return AddColumn(columnName, type, 0);
		}
	}
}