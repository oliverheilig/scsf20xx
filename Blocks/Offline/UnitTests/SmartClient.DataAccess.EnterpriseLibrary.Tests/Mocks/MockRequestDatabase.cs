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
using System.Data.Common;
using System.Data.SqlServerCe;
using Microsoft.Practices.TestDataAccess;

namespace Microsoft.Practices.SmartClient.EnterpriseLibrary.Tests.Mocks
{
	internal class MockRequestDatabase : SmartClientDatabase
	{
		private MockRequestTable table = new MockRequestTable();
		public bool RequestTablePresent = false;

		public MockRequestDatabase()
			: base("DataSource=Datastore.sdf;")
		{
		}

#pragma warning disable 0649
		public string TableName;
#pragma warning restore 0649


		public new DbParameter CreateParameter(string name, DbType type, int size, object value)
		{
			DbParameter parameter = new SqlCeParameter();
			parameter.ParameterName = name;
			parameter.DbType = type;
			parameter.Size = size;
			parameter.Value = value == null ? DBNull.Value : value;
			return parameter;
		}

		public new int ExecuteNonQuery(string sqlCommand, params DbParameter[] parameters)
		{
			MockSql info = new MockSql(sqlCommand);

			if (sqlCommand.StartsWith("CREATE TABLE " + TableName))
				RequestTablePresent = true;

			if (sqlCommand.StartsWith("INSERT "))
				MockSql.AddRow(table, parameters);

			if (sqlCommand.StartsWith("UPDATE "))
				table.Rows[0]["Status"] = 1;

			if (sqlCommand.StartsWith("DELETE FROM "))
			{
				DataRow row = table.Rows.Find(parameters[0].Value);
				if (row != null)
					table.Rows.Remove(row);
			}

			return 0;
		}

		public new DbDataReader ExecuteReader(string sqlCommand, params DbParameter[] parameters)
		{
			MockSql info = new MockSql(sqlCommand);

			if (info.Where != null && info.Where.Length > 0)
			{
				foreach (DbParameter parameter in parameters)
				{
					if (parameter.DbType == DbType.String)
						info.Where = info.Where.Replace("= " + parameter.ParameterName, "= '" + parameter.Value.ToString() + "'");
					else if (parameter.DbType == DbType.Guid)
						info.Where = info.Where.Replace("= " + parameter.ParameterName, "= '" + parameter.Value.ToString() + "'");
					else
						info.Where = info.Where.Replace(">= " + parameter.ParameterName, ">= " + parameter.Value.ToString());
				}
				DataView view = new DataView(table, info.Where, info.OrderBy, DataViewRowState.CurrentRows);
				return view.ToTable().CreateDataReader();
			}
			else
				return table.CreateDataReader();
		}

		public new object ExecuteScalar(string sqlCommand, params DbParameter[] parameters)
		{
			MockSql info = new MockSql(sqlCommand);
			if (sqlCommand.StartsWith("SELECT Count"))
			{
				if (info.Where != null && info.Where.Length > 0)
				{
					DataView view = new DataView(table, info.Where, info.OrderBy, DataViewRowState.CurrentRows);
					return view.Count;
				}
				else
					return table.Rows.Count;
			}

			return 0;
		}

		public new bool TableExists(string tableName)
		{
			return RequestTablePresent;
		}
	}
}