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
using System.Data.SqlServerCe;
using System.Globalization;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.SmartClient.DisconnectedAgent;
using Microsoft.Practices.SmartClient.EnterpriseLibrary.Properties;

namespace Microsoft.Practices.SmartClient.EnterpriseLibrary
{
	/// <summary>
	/// Implements the IRequestQueue interface to provide a request queue saved in a database.
	/// </summary>
	public class DatabaseRequestQueue : IRequestQueue
	{
		private string tableName;
		private SmartClientDatabase database;

		/// <summary>
		/// An event that is fired to indicate that a <see cref="Request"/> was added to the queue.
		/// </summary>
		public event EventHandler<RequestEnqueuedEventArgs> RequestEnqueued;

		/// <summary>
		/// Creates a DatabaseRequestQueue object
		/// </summary>
		/// <remarks>
		/// By default, a <see cref="SmartClientDatabase"/> is used as the database provider.
		/// </remarks>
		public DatabaseRequestQueue()
			: this(DatabaseFactory.CreateDatabase() as SmartClientDatabase)
		{
		}

		/// <summary>
		/// Creates a DatabaseRequestQueue object
		/// </summary>
		/// <param name="databaseName">The name of the database to use as storage.</param>
		/// <param name="tableName">The name of the table in the database to uyse as storage.</param>
		/// <remarks>
		/// By default, a <see cref="SmartClientDatabase"/> is used as the database provider.
		/// </remarks>
		public DatabaseRequestQueue(string databaseName, string tableName)
			: this(DatabaseFactory.CreateDatabase(databaseName) as SmartClientDatabase, tableName)
		{
		}

		/// <summary>
		/// Creates a DatabaseRequestQueue object
		/// </summary>
		/// <param name="database">The <see cref="SmartClientDatabase"/> to use for storage.</param>
		public DatabaseRequestQueue(SmartClientDatabase database)
			: this(database, Settings.Default.RequestQueueTableName)
		{
		}

		/// <summary>
		/// Creates a DatabaseRequestQueue object
		/// </summary>
		/// <param name="database">The <see cref="SmartClientDatabase"/> to use for storage.</param>
		/// <param name="tableName">The name of the table in the database to uyse as storage.</param>
		public DatabaseRequestQueue(SmartClientDatabase database, string tableName)
		{
			Guard.ArgumentNotNull(database, "database");
			this.tableName = tableName;
			this.database = database;

			if (!this.database.TableExists(tableName))
				CreateDatabaseTables();
		}

		/// <summary>
		/// Adds a request to the queue.
		/// </summary>
		/// <param name="request">The Request to add to the queue.</param>
		/// <exception cref="RequestManagerException" />
		public void Enqueue(Request request)
		{
			Guard.ArgumentNotNull(request, "request");
			Guard.ArgumentNotNull(request.Behavior, "request.Behavior");
			Guard.ArgumentNotNull(request.MethodName, "request.MethodName");
			Guard.ArgumentNotNull(request.Behavior.ProxyFactoryType,
			                      "request.Behavior.ProxyFactoryType");
			Guard.ArgumentNotNull(request.OnlineProxyType, "request.OnlineProxyType");
			Guard.ArgumentNotNull(request.Endpoint, "request.Endpoint");

			request.Behavior.QueuedDate = DateTime.Now;

			string serializedParameters = String.Empty;
			if (request.CallParameters != null)
				serializedParameters = CallParametersSerializer.Serialize(request.CallParameters);

			string crLf = Environment.NewLine;

			StringBuilder sqlSb = new StringBuilder();
			sqlSb.AppendFormat("Insert {0}{1} ", tableName, crLf);
			sqlSb.AppendLine("          (");
			sqlSb.AppendLine("              RequestId,");
			sqlSb.AppendLine("              Endpoint,");
			sqlSb.AppendLine("              ProxyFactoryType,");
			sqlSb.AppendLine("              OnlineProxyType,");
			sqlSb.AppendLine("              MethodName,");
			sqlSb.AppendLine("              Tag,");
			sqlSb.AppendLine("              Stamps,");
			sqlSb.AppendLine("              ReturnTargetType,");
			sqlSb.AppendLine("              ReturnMethodName,");
			sqlSb.AppendLine("              ExceptionTargetType,");
			sqlSb.AppendLine("              ExceptionMethodName,");
			sqlSb.AppendLine("              MaxRetries,");
			sqlSb.AppendLine("              CallParameters,");
			sqlSb.AppendLine("              QueuedDate,");
			sqlSb.AppendLine("              MessageID,");
			sqlSb.AppendLine("              Expiration)");
			sqlSb.AppendLine("          VALUES(");
			sqlSb.AppendFormat("                {0},{1}", database.BuildParameterName("RequestId"), crLf);
			sqlSb.AppendFormat("                {0},{1}", database.BuildParameterName("Endpoint"), crLf);
			sqlSb.AppendFormat("                {0},{1}", database.BuildParameterName("ProxyFactoryType"), crLf);
			sqlSb.AppendFormat("                {0},{1}", database.BuildParameterName("OnlineProxyType"), crLf);
			sqlSb.AppendFormat("                {0},{1}", database.BuildParameterName("MethodName"), crLf);
			sqlSb.AppendFormat("                {0},{1}", database.BuildParameterName("Tag"), crLf);
			sqlSb.AppendFormat("                {0},{1}", database.BuildParameterName("Stamps"), crLf);
			sqlSb.AppendFormat("                {0},{1}", database.BuildParameterName("ReturnTargetType"), crLf);
			sqlSb.AppendFormat("                {0},{1}", database.BuildParameterName("ReturnMethodName"), crLf);
			sqlSb.AppendFormat("                {0},{1}", database.BuildParameterName("ExceptionTargetType"), crLf);
			sqlSb.AppendFormat("                {0},{1}", database.BuildParameterName("ExceptionMethodName"), crLf);
			sqlSb.AppendFormat("                {0},{1}", database.BuildParameterName("MaxRetries"), crLf);
			sqlSb.AppendFormat("                {0},{1}", database.BuildParameterName("CallParameters"), crLf);
			sqlSb.AppendFormat("                {0},{1}", database.BuildParameterName("QueuedDate"), crLf);
			sqlSb.AppendFormat("                {0},{1}", database.BuildParameterName("MessageID"), crLf);
			sqlSb.AppendFormat("                {0}{1}", database.BuildParameterName("Expiration"), crLf);
			sqlSb.AppendLine("          )");

			string sql = sqlSb.ToString();
			string returnTargetType = null;
			string returnMethodName = null;
			string exceptionTargetType = null;
			string exceptionMethodName = null;

			if (request.Behavior.ReturnCallback != null)
			{
				returnTargetType = request.Behavior.ReturnCallback.TargetType.AssemblyQualifiedName;
				returnMethodName = request.Behavior.ReturnCallback.TargetMethodName;
			}
			if (request.Behavior.ExceptionCallback != null)
			{
				exceptionTargetType =
					request.Behavior.ExceptionCallback.TargetType.AssemblyQualifiedName;
				exceptionMethodName = request.Behavior.ExceptionCallback.TargetMethodName;
			}

			DbParameter[] parameters = new DbParameter[]
				{
					// Due to the limitation that limits DbType.String to 255 characters,
					// the SqlDbType.NText is used on all string fields with a length greater 
					// than 250.
					database.CreateParameter("RequestId", DbType.Guid, 1, request.RequestId),
					database.CreateParameter("Endpoint", SqlDbType.NText, 300, request.Endpoint),
					database.CreateParameter("ProxyFactoryType", SqlDbType.NText, 1000,
					                         request.Behavior.ProxyFactoryType.AssemblyQualifiedName),
					database.CreateParameter("OnlineProxyType", SqlDbType.NText, 1000,
					                         request.OnlineProxyType.AssemblyQualifiedName),
					database.CreateParameter("MethodName", DbType.String, 200, request.MethodName),
					database.CreateParameter("Tag", SqlDbType.NText, 300, request.Behavior.Tag),
					database.CreateParameter("Stamps", DbType.Int16, 1, request.Behavior.Stamps),
					database.CreateParameter("ReturnTargetType", SqlDbType.NText, 1000,
					                         returnTargetType),
					database.CreateParameter("ReturnMethodName", DbType.String, 200,
					                         returnMethodName),
					database.CreateParameter("ExceptionTargetType", SqlDbType.NText, 1000,
					                         exceptionTargetType),
					database.CreateParameter("ExceptionMethodName", DbType.String, 200,
					                         exceptionMethodName),
					database.CreateParameter("MaxRetries", DbType.Int16, 1,
					                         request.Behavior.MaxRetries),
					database.CreateParameter("CallParameters", SqlDbType.NText,
					                         serializedParameters.Length + 1, serializedParameters),
					database.CreateParameter("QueuedDate", DbType.DateTime, 0,
					                         request.Behavior.QueuedDate),
					database.CreateParameter("MessageID", DbType.Guid, 0, request.Behavior.MessageId)
					,
					database.CreateParameter("Expiration", DbType.DateTime, 0,
					                         request.Behavior.Expiration)
				};

			try
			{
				database.ExecuteNonQuery(sql, parameters);
			}
			catch (SqlCeException ex)
			{
				throw new RequestManagerException(
					String.Format(CultureInfo.CurrentCulture, ex.Message,
					              request.RequestId), ex);
			}

			if (RequestEnqueued != null)
			{
				RequestEnqueued(this, new RequestEnqueuedEventArgs(request));
			}
		}

		/// <summary>
		/// Gets the number of items in the queue.
		/// </summary>
		/// <returns>The number of items in the queue.</returns>
		public int GetCount()
		{
			string sql = @"SELECT Count(*) FROM " + tableName;
			return (int) database.ExecuteScalar(sql);
		}

		/// <summary>
		/// Gets the next <see cref="Request"/> from the queue.
		/// </summary>
		/// <returns>The first <see cref="Request"/> object from the queue.</returns>
		public Request GetNextRequest()
		{
			Request request = null;

			string sql = "SELECT" + GetColumns() + @"
					  FROM	" + tableName +
			             @"
					  ORDER BY sequence";

			using (IDataReader reader = database.ExecuteReader(sql))
			{
				if (reader.Read())
				{
					request = GetRequestFromReader(reader);
				}
			}

			return request;
		}

		/// <summary>
		/// Gets all the <see cref="Request"/>s with the given tag.
		/// </summary>
		/// <param name="tag">A tag to identify the <see cref="Request"/>s to retrieve.</param>
		/// <returns>An enumerable collection of <see cref="Request"/>s.</returns>
		public IEnumerable<Request> GetRequests(string tag)
		{
			string sql = "SELECT" + GetColumns() + @"
					  FROM	" + tableName +
			             @"
					  WHERE Tag LIKE " +
			             database.BuildParameterName("Tag") + @"
					  ORDER BY Sequence";

			DbParameter tagParameter = database.CreateParameter("Tag", SqlDbType.NText, 300, tag);

			using (IDataReader reader = database.ExecuteReader(sql, tagParameter))
			{
				while (reader.Read())
				{
					Request request = GetRequestFromReader(reader);
					yield return request;
				}
			}
		}

		/// <summary>
		/// Gets all the <see cref="Request"/>s with at least the specified number of stamps
		/// </summary>
		/// <param name="stampsEqualOrMoreThan">The minimum number of stamps necesary for a <see cref="Request"/>.</param>
		/// <returns>An enumerable collection of <see cref="Request"/>s.</returns>
		public IEnumerable<Request> GetRequests(int stampsEqualOrMoreThan)
		{
			string sql = "SELECT" + GetColumns() + @"
					  FROM	" + tableName +
			             @"
					  WHERE Stamps >= " +
			             database.BuildParameterName("Stamps") + @"
					  ORDER BY Sequence";

			DbParameter priceParameter =
				database.CreateParameter("Stamps", DbType.Int32, 0, stampsEqualOrMoreThan);

			using (IDataReader reader = database.ExecuteReader(sql, priceParameter))
			{
				while (reader.Read())
				{
					Request request = GetRequestFromReader(reader);
					yield return request;
				}
			}
		}

		/// <summary>
		/// Gets all pending <see cref="Request"/>s.
		/// </summary>
		/// <returns>An enumerable collection of <see cref="Request"/>s.</returns>
		public IEnumerable<Request> GetRequests()
		{
			string sql = "SELECT" + GetColumns() + @"
					  FROM	" + tableName +
			             @"
					  ORDER BY Sequence";

			using (IDataReader reader = database.ExecuteReader(sql))
			{
				while (reader.Read())
				{
					Request request = GetRequestFromReader(reader);
					yield return request;
				}
			}
		}

		/// <summary>
		/// Gets the <see cref="Request"/> with the specified unique identifier.
		/// </summary>
		/// <param name="requestId">The unique identifier specifying which <see cref="Request"/> to retrieve.</param>
		/// <returns>The specified <see cref="Request"/>.</returns>
		public Request GetRequest(Guid requestId)
		{
			string sql = "SELECT" + GetColumns() + @"
					  FROM	" + tableName +
			             @"
					  WHERE RequestId = " +
			             database.BuildParameterName("RequestId");

			DbParameter tagParameter =
				database.CreateParameter("RequestId", DbType.Guid, 1, requestId);

			using (IDataReader reader = database.ExecuteReader(sql, tagParameter))
			{
				while (reader.Read())
				{
					Request request = GetRequestFromReader(reader);
					return request;
				}
			}
			return null;
		}

		/// <summary>
		/// Removes a <see cref="Request"/> from the queue.
		/// </summary>
		/// <param name="request">The <see cref="Request"/> to remove.</param>
		public void Remove(Request request)
		{
			Guard.ArgumentNotNull(request, "request");
			string sql = @"DELETE FROM " + tableName + @" WHERE RequestId = " +
			             database.BuildParameterName("RequestId");

			DbParameter[] parameters = new DbParameter[]
				{
					database.CreateParameter("RequestId", DbType.Guid, 1, request.RequestId),
				};
			database.ExecuteNonQuery(sql, parameters);
		}

		private static Request GetRequestFromReader(IDataReader reader)
		{
			Request request = new Request();
			request.Behavior = new OfflineBehavior();
			request.RequestId = (Guid) reader["RequestId"];
			request.Endpoint = (string) reader["Endpoint"];
			request.MethodName = (string) reader["MethodName"];
			request.Behavior.ProxyFactoryType = Type.GetType((string) reader["ProxyFactoryType"]);
			request.OnlineProxyType = Type.GetType((string) reader["OnlineProxyType"]);

			request.Behavior.Stamps = (ushort) (int) reader["Stamps"];
			if (!(reader["Tag"] is DBNull))
				request.Behavior.Tag = (string) reader["Tag"];
			if (!(reader["ReturnTargetType"] is DBNull))
				request.Behavior.ReturnCallback =
					GetCallbackFrom(reader, "ReturnTargetType", "ReturnMethodName");
			if (!(reader["ExceptionTargetType"] is DBNull))
				request.Behavior.ExceptionCallback =
					GetCallbackFrom(reader, "ExceptionTargetType", "ExceptionMethodName");
			if (!(reader["MaxRetries"] is DBNull))
				request.Behavior.MaxRetries =
					Convert.ToInt32(reader["MaxRetries"], CultureInfo.InvariantCulture);
			if (!(reader["CallParameters"] is DBNull))
			{
				string serializedParameters = (string) reader["CallParameters"];
				request.CallParameters = CallParametersSerializer.Deserialize(serializedParameters);
			}
			request.Behavior.QueuedDate = (DateTime) reader["QueuedDate"];
			if (!(reader["MessageId"] is DBNull))
				request.Behavior.MessageId = (Guid) reader["MessageId"];
			if (!(reader["Expiration"] is DBNull))
				request.Behavior.Expiration = (DateTime) reader["Expiration"];

			return request;
		}

		private static CommandCallback GetCallbackFrom(
			IDataReader reader, string typeNameColumn, string methodNameColumn)
		{
			string typename = (string) reader[typeNameColumn];
			Type type = Type.GetType(typename);
			return new CommandCallback(type, (string) reader[methodNameColumn]);
		}

		private static string GetColumns()
		{
			return
				@" RequestId,
					Endpoint,
					ProxyFactoryType,
					OnlineProxyType,
					MethodName,
					Tag,
					Stamps,
					ReturnTargetType,
					ReturnMethodName,
					ExceptionTargetType,
					ExceptionMethodName,
					MaxRetries,
					CallParameters,
					QueuedDate,
					MessageID,
					Expiration ";
		}

		private void CreateDatabaseTables()
		{
			string sql = @"CREATE TABLE " + tableName +
			             @"(
						 RequestId uniqueidentifier PRIMARY KEY NOT NULL,
						 Endpoint NVARCHAR(300) NOT NULL,
						 ProxyFactoryType NVARCHAR(1000) NOT NULL,
						 OnlineProxyType NVARCHAR(1000) NOT NULL,
						 MethodName NVARCHAR(200) NOT NULL,
						 Tag NVARCHAR(300),
						 Stamps int NOT NULL,
						 ReturnTargetType NVARCHAR(1000), 
						 ReturnMethodName NVARCHAR(200), 
						 ExceptionTargetType NVARCHAR(1000), 
						 ExceptionMethodName NVARCHAR(200),
						 MaxRetries int NOT NULL,
						 CallParameters NTEXT,
						 Sequence int IDENTITY(1,1),
						 QueuedDate DATETIME NOT NULL,
						 MessageID UNIQUEIDENTIFIER NULL,
						 Expiration DATETIME NULL
						 )";
			database.ExecuteNonQuery(sql);

			//Create Indexes
			sql = @"CREATE INDEX request_sequence_asc ON " + tableName + "(Sequence)";
			database.ExecuteNonQuery(sql);
			sql = @"CREATE INDEX request_sequence_desc ON " + tableName + "(Sequence desc)";
			database.ExecuteNonQuery(sql);
		}
	}
}