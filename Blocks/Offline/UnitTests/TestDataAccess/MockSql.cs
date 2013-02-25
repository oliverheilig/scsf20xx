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
using System.Data;
using System.Data.Common;

namespace Microsoft.Practices.TestDataAccess
{
	public class MockSql
	{
		public string Command;
		public string Where;
		public string OrderBy;

		public MockSql()
		{
		}

		public MockSql(string sql)
		{
			Command = sql;
			int index = Command.IndexOf("ORDER BY", StringComparison.InvariantCultureIgnoreCase);
			if (index > 0)
			{
				OrderBy = Command.Substring(index + 8).Trim();
				Command = Command.Substring(0, index);
			}

			index = Command.IndexOf("WHERE", StringComparison.InvariantCultureIgnoreCase);
			if (index > 0)
			{
				Where = Command.Substring(index + 5).Trim();
				Command = Command.Substring(0, index);
			}
		}

		public static DataRow AddRow(DataTable table, DbParameter[] parameters)
		{
			DataRow row = table.NewRow();
			for (int i = 0; i < parameters.Length; i++)
			{
				DbParameter parameter = parameters[i];
				row[parameter.ParameterName] = parameter.Value;
			}
			table.Rows.Add(row);
			return row;
		}

		public int ExecuteNonQuery(DataTable table, string sqlCommand, params DbParameter[] parameters)
		{
			string[] tokens = GetTokenArray(sqlCommand);

			switch (tokens[0].ToUpper())
			{
				case "UPDATE":
					return ExecuteUpdate(table, tokens, parameters);
			}

			return 0;
		}

		//
		// Executes an Update statement, which has this format:
		//
		//	UPDATE [tableName] SET [columns]=[value], ... WHERE [criteria]
		//
		private int ExecuteUpdate(DataTable table, string[] tokens, params DbParameter[] parameters)
		{
			return 0;
		}

		public string[] GetTokenArray(string sqlCommand)
		{
			List<string> tokens = new List<string>();
			foreach (string token in GetTokens(sqlCommand))
				tokens.Add(token);

			return tokens.ToArray();
		}

		public IEnumerable<string> GetTokens(string sqlCommand)
		{
			string sql = ConsolidateWhiteSpace(sqlCommand);
			bool inString = false;
			int startIndex = 0;
			int i = 0;
			char[] delimeters = new char[] {' ', '=', ','};

			while (i < sql.Length)
			{
				char c = sql[i];

				if (c == '\'')
				{
					if (inString)
					{
						yield return sql.Substring(startIndex, i - startIndex);
						startIndex = i + 1;
					}
					inString = !inString;
				}
				else if (Array.IndexOf<char>(delimeters, c) >= 0)
				{
					if (i > startIndex)
						yield return sql.Substring(startIndex, i - startIndex);
					startIndex = i + 1;

					if (c != ' ')
						yield return c.ToString();
				}

				i++;
			}

			if (i - startIndex > 0)
				yield return sql.Substring(startIndex, i - startIndex);
		}

		//
		// This method colsolidates multiple whitespace characters into single spaces to make parsing
		// a string eaiser. It's not fast, but this is just for testing.
		//
		private string ConsolidateWhiteSpace(string sqlCommand)
		{
			string sql = sqlCommand.Trim();
			sql = sql.Replace('\t', ' ');
			sql = sql.Replace('\r', ' ');
			sql = sql.Replace('\n', ' ');
			while (sql.IndexOf("  ") >= 0)
				sql = sql.Replace("  ", " ");

			return sql;
		}
	}
}