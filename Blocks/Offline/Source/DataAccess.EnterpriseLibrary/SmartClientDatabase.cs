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
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.SqlCe;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Instrumentation;

namespace Microsoft.Practices.SmartClient.EnterpriseLibrary
{
	/// <summary>
	/// A disposable <see cref="SqlCeDatabase"/>.
	/// </summary>        
    [ConfigurationElementType(typeof(SmartClientDatabaseData))]
    public class SmartClientDatabase : SqlCeDatabase, IDisposable
	{
		private DbConnection connection;

		/// <summary>
		/// Creates a <see cref="SmartClientDatabase"/> using the provided connection string.
		/// </summary>
		/// <param name="connectionString">The connection string to use.</param>
		public SmartClientDatabase(string connectionString)
			: base(connectionString)
		{
		}
        
        /// <summary>
        /// Creates a <see cref="SmartClientDatabase"/> using the provided connection string.
        /// </summary>
        /// <param name="connectionString">The connection string to use.</param>
        /// <param name="instrumentationProvider">The data instrumentation provider to use.</param>
        public SmartClientDatabase(string connectionString, IDataInstrumentationProvider instrumentationProvider)
            : base(connectionString, instrumentationProvider)
        { 
        }

		/// <summary>
		/// Cleans up after the object.  Closes the file used by the <see cref="SqlCeDatabase"/>.
		/// </summary>
		public void Dispose()
		{
			base.CloseSharedConnection();
			if (connection != null)
			{
				connection.Close();
				connection = null;
			}
		}

		/// <summary>
		/// Determines if a table exists in the database.
		/// </summary>
		/// <param name="tableName">The name of the table to look for.</param>
		/// <returns>True if the table exists, otherwise false.</returns>
		public override bool TableExists(string tableName)
		{
			Guard.ArgumentNotNullOrEmptyString(tableName, "tableName");

			return base.TableExists(tableName);
		}

		/// <summary>
		/// Executes a Transact-SQL statement and returns the number of rows affected. 
		/// </summary>
		/// <param name="sqlCommand">The command to execute.</param>
		/// <param name="parameters">Parameters to provide for the command.</param>
		/// <returns>The number of rows affected.</returns>
		public int ExecuteNonQuery(string sqlCommand, params DbParameter[] parameters)
		{
			Guard.ArgumentNotNullOrEmptyString(sqlCommand, "sqlCommand");
			return base.ExecuteNonQuerySql(sqlCommand, parameters);
		}

		/// <summary>
		/// Executes a Transact-SQL statement and returns the number of rows affected. 
		/// </summary>
		/// <param name="command">The <see cref="DbCommand"/> to execute.</param>
		/// <param name="parameters">Parameters to provide for the command.</param>
		/// <returns>The number of rows affected.</returns>
		public int ExecuteNonQuery(DbCommand command, params DbParameter[] parameters)
		{
			base.AddParameters(command, parameters);
			return base.ExecuteNonQuery(command);
		}

		/// <summary>
		/// Sends the command and builds a <see cref="SqlCeDataReader"/> . 
		/// </summary>
		/// <param name="sqlCommand">The command to execute.</param>
		/// <param name="parameters">The parameter to pass to the command.</param>
		/// <returns>A <see cref="DbDataReader"/> object.</returns>
        public IDataReader ExecuteReader(string sqlCommand, params DbParameter[] parameters)
		{			
            Guard.ArgumentNotNullOrEmptyString(sqlCommand, "sqlCommand");
            return base.ExecuteReaderSql(sqlCommand, parameters);
		}

		/// <summary>
		/// Sends the command and builds a <see cref="SqlCeDataReader"/> . 
		/// </summary>
		/// <param name="command">The <see cref="DbCommand"/> to execute.</param>
		/// <param name="parameters">The parameter to pass to the command.</param>
		/// <returns>A <see cref="DbDataReader"/> object.</returns>
		public IDataReader ExecuteReader(DbCommand command, params DbParameter[] parameters)
		{
			base.AddParameters(command, parameters);
			return base.ExecuteReader(command);
		}

		/// <summary>
		/// Executes the command and returns the first column of the first row in the result set that is returned by the query. Extra columns or rows are ignored. 
		/// </summary>
		/// <param name="sqlCommand">The command to execute.</param>
		/// <param name="parameters">The parameter to pass to the command.</param>
		/// <returns>The first column of the first row in the result set.</returns>
		public object ExecuteScalar(string sqlCommand, params DbParameter[] parameters)
		{
			Guard.ArgumentNotNullOrEmptyString(sqlCommand, "sqlCommand");

			return base.ExecuteScalarSql(sqlCommand, parameters);
		}

		/// <summary>
		/// Executes the command and returns the first column of the first row in the result set that is returned by the query. Extra columns or rows are ignored. 
		/// </summary>
		/// <param name="command">The <see cref="DbCommand"/> to execute.</param>
		/// <param name="parameters">The parameter to pass to the command.</param>
		/// <returns>The first column of the first row in the result set.</returns>
		public object ExecuteScalar(DbCommand command, params DbParameter[] parameters)
		{
			base.AddParameters(command, parameters);
			return base.ExecuteScalar(command);
		}

		/// <summary>
		/// Sends the command to the database and builds a <see cref="SqlCeResultSet"/>. 
		/// </summary>
		/// <param name="sqlCommand">The command to execute.</param>
		/// <param name="parameters">The parameter to pass to the command.</param>
		/// <param name="options">The <see cref="ResultSetOptions"/> to use when building the <see cref="SqlCeResultSet"/>.</param>
		/// <returns>A <see cref="SqlCeResultSet"/>.</returns>        
		public SqlCeResultSet ExecuteResultSet(string sqlCommand, ResultSetOptions options, params DbParameter[] parameters)
		{
			Guard.ArgumentNotNullOrEmptyString(sqlCommand, "sqlCommand");

			using (SqlCeCommand command = (SqlCeCommand) DbProviderFactory.CreateCommand())
			{
				command.CommandText = sqlCommand;                
                SqlCeResultSet result = ExecuteResultSet(command, options, parameters);                                
				connection = command.Connection;
				return result;
			}
		}

		/// <summary>
		/// Sends the command to the database and builds a <see cref="SqlCeResultSet"/>. 
		/// </summary>
		/// <param name="command">The <see cref="DbCommand"/> to execute.</param>
		/// <param name="parameters">The parameter to pass to the command.</param>
		/// <param name="options">The <see cref="ResultSetOptions"/> to use when building the <see cref="SqlCeResultSet"/>.</param>
		/// <returns>A <see cref="SqlCeResultSet"/>.</returns>
		public override SqlCeResultSet ExecuteResultSet(DbCommand command, ResultSetOptions options,
		                                                params DbParameter[] parameters)
		{
			Guard.ArgumentNotNull(command, "command");
			
            SqlCeResultSet result = base.ExecuteResultSet(command, options, parameters);
			connection = command.Connection;
			return result;
		}

		/// <summary>
		/// Creates a <see cref="DbParameter"/> object.
		/// </summary>
		/// <param name="name">The name of the parameter.</param>
		/// <param name="value">The value of the parameter.</param>
		/// <returns>A <see cref="DbParameter"/> object.</returns>
		public DbParameter CreateParameter(string name, object value)
		{
			Guard.ArgumentNotNullOrEmptyString(name, "name");

			DbParameter parameter = base.CreateParameter(name);
			parameter.Value = value == null ? DBNull.Value : value;
			return parameter;
		}

		/// <summary>
		/// Creates a <see cref="DbParameter"/> object.
		/// </summary>
		/// <param name="name">The name of the parameter.</param>
		/// <param name="type">The <see cref="SqlDbType"/> of the parameter.</param>
		/// <param name="size">The size of the parameter.</param>
		/// <param name="value">The value of the parameter.</param>
		/// <returns>A <see cref="DbParameter"/> object.</returns>
		public DbParameter CreateParameter(string name, SqlDbType type, int size, object value)
		{
			SqlCeParameter parameter = (SqlCeParameter) CreateParameter(name);
			parameter.SqlDbType = type;
			parameter.Size = size;
			parameter.Value = (value == null) ? DBNull.Value : value;
			return parameter;
		}

		/// <summary>
		/// Gets the <see cref="DbConnection"/>.
		/// </summary>
		/// <returns>A <see cref="DbConnection"/> object.</returns>
		public DbConnection GetConnection()
		{
			return base.GetOpenConnection().Connection;
		}
	}
}