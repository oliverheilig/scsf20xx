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
//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Data Access Application Block
//===============================================================================
// Copyright � Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Microsoft.Practices.SmartClient.EnterpriseLibrary.Data.SqlCe
{
	/// <summary>
	///		This class provides a primitive connection pool for <see cref="SqlCeDatabase"/>.
	/// </summary>
	public class SqlCeConnectionPool
	{
		/// <summary>
		///		Keeps a list of "keep alive" connections.
		/// </summary>
		protected static Dictionary<string, DbConnection> connections = new Dictionary<string, DbConnection>();

		/// <summary>
		///		Creates a new connection. If this is the first connection,  it also creates an extra
		///		"Keep Alive" connection to keep the database open.
		/// </summary>
		/// <param name="db">The database instance that will be used to create a connection.</param>
		/// <returns>A new connection.</returns>
		internal static DbConnection CreateConnection(SqlCeDatabase db)
		{
			string connectionString = db.ConnectionStringWithoutCredentials;

			if (!connections.ContainsKey(connectionString))
			{
				lock (connections)
				{
					//
					// We have to test this again in case another thread added a connection.
					//
					if (!connections.ContainsKey(connectionString))
					{
						DbConnection keepAliveConnection = new SqlCeConnection();
						db.SetConnectionString(keepAliveConnection);
                        keepAliveConnection.Open();
						
						connections.Add(connectionString, keepAliveConnection);
					}
				}
			}

			return new SqlCeConnection();
		}

		/// <summary>
		///		Closes the "keep alive" connection that is used by all databases with the same connection
		///		string as the one you provide.
		/// </summary>
		/// <param name="database">The database with the connection string that defines which connections should be closed.</param>
		internal static void CloseSharedConnection(Database database)
		{
			DbConnection connection;
			string connectionString = database.ConnectionStringWithoutCredentials;

			lock (connections)
			{
				if (connections.TryGetValue(connectionString, out connection))
				{
					connection.Close();
					connection.Dispose();
					connections.Remove(connectionString);
				}
			}
		}

		/// <summary>
		///		Closes all "keep alive" connections for all database instanced.
		/// </summary>
		public static void CloseSharedConnections()
		{
			lock (connections)
			{
				foreach (KeyValuePair<string, DbConnection> pair in connections)
				{
					pair.Value.Close();
					pair.Value.Dispose();
				}
				connections.Clear();
			}
		}
	}
}
