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
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.SmartClient.EnterpriseLibrary.Tests.Mocks;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Reflection;

namespace Microsoft.Practices.SmartClient.EnterpriseLibrary.Tests
{
	/// <summary>
	/// Summary description for SqlCeDatabaseFixture
	/// </summary>
	[TestClass]
	public class SmartClientDatabaseFixture
	{
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            IServiceLocator mockServiceLocator = new MockServiceLocator(EnterpriseLibraryContainer.Current);
            EnterpriseLibraryContainer.Current = mockServiceLocator;
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            string fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Datastore.sdf");
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }

		[TestMethod]
		public void TestCreationUsingEntLibFactory()
		{
			using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
			{
				Assert.IsNotNull(database);
			}
		}

		[TestMethod]
		public void ConnectionStringIsNotNull()
		{
			using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
			{
				Assert.IsNotNull(database.ConnectionStringWithoutCredentials);
			}
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentNullException))]
		public void ThrowsIfNullParameterNameIsPassed()
		{
			using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
			{
				using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
				{
					string sql = @"SELECT COUNT(*) FROM Customers WHERE ContactName = @Name";
					DbParameter param = database.CreateParameter(null, "Maria Anders");

					Assert.AreEqual(1, database.ExecuteScalar(sql, param));
					database.CloseSharedConnection();
				}
			}
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentNullException))]
		public void ExecuteNonQueryThrowsIfNullQueryIsPassed()
		{
			using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
			{
				using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
				{
					Assert.AreEqual(1, database.ExecuteNonQuery((string) null, null));
				}
			}
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentNullException))]
		public void TableExistsThrowsForNullName()
		{
			using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
			{
				using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
				{
					database.TableExists(null);
				}
			}
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentException))]
		public void TableExistsThrowsForEmptyName()
		{
			using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
			{
				using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
				{
					database.TableExists("");
				}
			}
		}

		[TestMethod]
		public void CanConnectToSqlServerCeDatabase()
		{
			using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
			{
				using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
				{
					DbConnection connection = database.GetConnection();
					Assert.IsNotNull(connection);
					Assert.IsTrue(connection is SqlCeConnection);
					Assert.IsTrue(connection.State == ConnectionState.Open);
					connection.Close();
				}
			}
		}

		[TestMethod]
		public void TestTableReportsTrueIfTableExists()
		{
			using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
			{
				using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
				{
					Assert.IsTrue(database.TableExists("TestTable"));
				}
			}
		}

		[TestMethod]
		public void TestTableRepotsFalseWhenNoSuchTable()
		{
			using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
			{
				using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
				{
					Assert.IsFalse(database.TableExists("JunkName"));
				}
			}
		}

		[TestMethod]
		public void AtAddedToParameterNameForSqlMobile()
		{
			using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
			{
				using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
				{
					DbParameter parameter = database.CreateParameter("test", 1);
					Assert.AreEqual("@test", parameter.ParameterName);
				}
			}
		}

		[TestMethod]
		public void CanCreateSQLSpecificParameters()
		{
			using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
			{
				using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
				{
					DbParameter param = database.CreateParameter("@Price", SqlDbType.Money, 19, 12.95);
					Assert.IsTrue(typeof (SqlCeParameter).IsAssignableFrom(param.GetType()));

					SqlCeParameter sqlParam = (SqlCeParameter) param;
					Assert.AreEqual(SqlDbType.Money, sqlParam.SqlDbType);
				}
			}
		}

		[TestMethod]
		public void ExecuteScalarWithDbCommand()
		{
			using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
			{
				using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
				{
					DbCommand command = new SqlCeCommand();
					command.CommandText = @"SELECT COUNT(*) FROM TestTable";

					int count = Convert.ToInt32(database.ExecuteScalar(command));

					Assert.AreEqual(4, count);
				}
			}
		}

		[TestMethod]
		public void ExecuteScalarWithStringSql()
		{
			using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
			{
				using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
				{
					string command = @"select count(*) from TestTable";

					int count = Convert.ToInt32(database.ExecuteScalar(command));
					Assert.AreEqual(4, count);
				}
			}
		}


		[TestMethod]
		[ExpectedException(typeof (ArgumentNullException))]
		public void ExecuteScalarWithNullDbCommandThrows()
		{
			using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
			{
				using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
				{
					database.ExecuteScalar((DbCommand) null);
				}
			}
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentNullException))]
		public void ExecuteScalarWithNullStringDoesNotReturnNull()
		{
			using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
			{
				using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
				{
					database.ExecuteScalar((String) null);
				}
			}
		}

		[TestMethod]
		[ExpectedException(typeof (SqlCeException))]
		public void ExecuteScalarWithInvalidSqlStringThrows()
		{
			using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
			{
				using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
				{
					database.ExecuteScalar("junk");
				}
			}
		}

		[TestMethod]
		public void ExecuteScalarWithParametersReturnExpectedValue()
		{
			using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
			{
				using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
				{
					string command = @"select * from TestTable where TestColumn > @TestColumn";
					DbParameter parameter = database.CreateParameter("TestColumn", 2);

					int value = Convert.ToInt32(database.ExecuteScalar(command, parameter));

					Assert.AreEqual(3, value);
				}
			}
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentNullException))]
		public void ExecuteNonQueryWithNullDbCommandThrows()
		{
			using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
			{
				using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
				{
					database.ExecuteNonQuery((DbCommand) null);
				}
			}
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentNullException))]
		public void ExecuteNonQueryWithNullStringThrows()
		{
			using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
			{
				using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
				{
					database.ExecuteNonQuery((String) null);
				}
			}
		}

		[TestMethod]
		public void CanExecuteNonQueryWithDbCommand()
		{
			using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
			{
				using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
				{
					string insertionString = @"insert into TestTable values (5, 'Cinco')";
					DbCommand insertionCommand = new SqlCeCommand();
					insertionCommand.CommandText = insertionString;

					database.ExecuteNonQuery(insertionCommand);

					string countCommand = @"select count(*) from TestTable";
					int count = Convert.ToInt32(database.ExecuteScalar(countCommand));

					string cleanupString = "delete from TestTable where TestColumn = 5";
					DbCommand cleanupCommand = new SqlCeCommand();
					cleanupCommand.CommandText = cleanupString;

					int rowsAffected = database.ExecuteNonQuery(cleanupCommand);

					Assert.AreEqual(5, count);
					Assert.AreEqual(1, rowsAffected);
				}
			}
		}

		[TestMethod]
		public void CanExecuteNonQueryWithSqlString()
		{
			using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
			{
				using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
				{
					string insertionString = @"insert into TestTable values (5, 'Cinco')";
					database.ExecuteNonQuery(insertionString);

					string countCommand = @"select count(*) from TestTable";
					int count = Convert.ToInt32(database.ExecuteScalar(countCommand));

					string cleanupString = "delete from TestTable where TestColumn = 5";
					int rowsAffected = database.ExecuteNonQuery(cleanupString);

					Assert.AreEqual(5, count);
					Assert.AreEqual(1, rowsAffected);
				}
			}
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentNullException))]
		public void ExecuteReaderWithNullDbCommandThrows()
		{
			using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
			{
				using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
				{
					database.ExecuteReader((DbCommand) null);
				}
			}
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentNullException))]
		public void ExecuteReaderWithNullStringThrows()
		{
			using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
			{
				using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
				{
					database.ExecuteReader((String) null);
				}
			}
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentException))]
		public void ExecuteReaderWithEmptyStringThrows()
		{
			using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
			{
				using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
				{
					database.ExecuteReader(String.Empty);
				}
			}
		}

		[TestMethod]
		public void CanExecuteReaderWithCommandText()
		{
			using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
			{
				using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
				{
					string queryString = @"select * from TestTable";

					string accumulator = "";
					using (IDataReader reader = database.ExecuteReader(queryString))
					{
						while (reader.Read())
						{
							accumulator += ((string) reader["TestColumn2"]).Trim();
						}
					}

					Assert.AreEqual("UnoDosTresCuatro", accumulator);
				}
			}
		}

		[TestMethod]
		public void CanExecuteReaderFromDbCommand()
		{
			using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
			{
				using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
				{
					string queryString = @"select * from TestTable";
					DbCommand queryCommand = new SqlCeCommand();
					queryCommand.CommandText = queryString;

					string accumulator = "";
					using (IDataReader reader = database.ExecuteReader(queryCommand))
					{
						while (reader.Read())
						{
							accumulator += ((string) reader["TestColumn2"]).Trim();
						}
					}

					Assert.AreEqual("UnoDosTresCuatro", accumulator);
				}
			}
		}

		[TestMethod]
		public void WhatGetsReturnedWhenWeDoAnInsertThroughDbCommandExecute()
		{
			using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
			{
				int count = -1;
				IDataReader reader = null;

				using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
				{
					try
					{
						string insertString = @"insert into TestTable values (5, 'Cinco')";

						reader = database.ExecuteReader(insertString);
						count = reader.RecordsAffected;
					}
					finally
					{
						if (reader != null)
						{
							reader.Close();
						}

						string deleteString = "Delete from TestTable where TestColumn = 5";
						database.ExecuteNonQuery(deleteString);
					}

					Assert.AreEqual(1, count);
				}
			}
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentException))]
		public void RetriveResultSetFromEmptySqlStringThrows()
		{
			using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
			{
				using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
				{
					database.ExecuteResultSet("", ResultSetOptions.None);
				}
			}
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentNullException))]
		public void RetriveResultSetFromNullSqlCommandThrows()
		{
			using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
			{
				using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
				{
					database.ExecuteResultSet((SqlCeCommand) null, ResultSetOptions.None);
				}
			}
		}

		[TestMethod]
		public void ExecuteResultSetFromSqlStringSucceeds()
		{
			using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
			{
				using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
				{
					string queryString = @"select * from TestTable";
					using (SqlCeResultSet resultSet = database.ExecuteResultSet(queryString, ResultSetOptions.None))
					{
						Assert.IsNotNull(resultSet);
						Assert.IsFalse(resultSet.IsClosed);
					}
				}
			}
		}

		[TestMethod]
		public void ExecuteResultSetFromSqlCommandSucceeds()
		{
			using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
			{
				using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
				{
					SqlCeCommand queryCommand = new SqlCeCommand();

					string queryString = @"select * From TestTable";
					queryCommand.CommandText = queryString;

					using (SqlCeResultSet result = database.ExecuteResultSet(queryCommand, ResultSetOptions.None))
					{
						Assert.IsNotNull(result);
						Assert.IsFalse(result.IsClosed);
					}
				}
			}
		}

		[TestMethod]
		public void ExecuteResultSetCanBeScrollable()
		{
			using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
			{
				using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
				{
					string queryString = @"select * From TestTable";
					using (SqlCeResultSet result = database.ExecuteResultSet(queryString, ResultSetOptions.Scrollable))
					{
                        Assert.IsNotNull(result);
					}
				}
			}
		}

		[TestMethod]
		public void ExecuteResultSetReturnExpectedValues()
		{
			using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
			{
				using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
				{
					string queryString = @"select * From TestTable";
					using (SqlCeResultSet result = database.ExecuteResultSet(queryString, ResultSetOptions.Scrollable))
					{
						Assert.IsTrue(result.HasRows);
						Assert.AreEqual(2, result.FieldCount);
					}
				}
			}
		}

		[TestMethod]
		public void CanInsertNullStringParameter()
		{
			using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
			{
				using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
				{
					string sqlString = "insert into TestTable Values (@Param1, @Param2)";
					DbParameter[] parameters = new DbParameter[]
						{
							database.CreateParameter("@Param1", DbType.Int32, 0, 5),
							database.CreateParameter("@Param2", DbType.String, 50, null)
						};

					database.ExecuteNonQuery(sqlString, parameters);

					string sqlCount = "SELECT COUNT(*) FROM TestTable ";

					Assert.AreEqual(5, database.ExecuteScalar(sqlCount, null));
				}
			}
		}

		[TestMethod]
		public void ExecuteSqlStringCommandWithParameters()
		{
			using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
			{
				using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
				{
					string sql = "select * from TestTable where (TestColumn=@Param1) and TestColumn2=@Param2";

					DbParameter[] parameters = new DbParameter[]
						{
							database.CreateParameter("@Param1", DbType.Int32, 0, 1),
							database.CreateParameter("@Param2", DbType.String, 50, "Uno")
						};

					using (IDataReader reader = database.ExecuteReader(sql, parameters))
					{
						reader.Read();
						Assert.AreEqual(1, reader["TestColumn"]);
						Assert.AreEqual("Uno", reader["TestColumn2"]);
					}
				}
			}
		}

		[TestMethod]
        [ExpectedException(typeof(SqlCeException))]
		public void ExecuteSqlStringCommandWithNotEnoughParameterValues()
		{
            using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
            {
                using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
                {
                    string sql = "select * from TestTable where (TestColumn=@Param1) and TestColumn2=@Param2";

                    DbParameter parameter = database.CreateParameter("@Param1", DbType.Int32, 0, 1);
                    DbParameter paramete2 = database.CreateParameter("@Param2", DbType.Int32, 0, 1);

                    database.ExecuteScalar(sql, parameter, paramete2);
                }
            }
		}

		[TestMethod]
	    public void ExecuteSqlStringCommandWithTooManyParameterValues()
		{
			using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
			{
				using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
				{
					string sql = "select count(*) from TestTable where (TestColumn=@Param1) and TestColumn2=@Param2";

					DbParameter[] parameters = new DbParameter[]
						{
							database.CreateParameter("@Param1", DbType.Int32, 0, 1),
							database.CreateParameter("@Param2", DbType.String, 50, "Uno"),
							database.CreateParameter("@Param3", DbType.Int32, 0, 123)
						};

					object o = database.ExecuteScalar(sql, parameters);
                    Assert.AreEqual(1, (int)o);
				}
			}
		}


		[TestMethod]
		public void ExecuteSqlStringWithoutParametersButWithValues()
		{
			using (TestResourceFile dbFile = new TestResourceFile(this, "Datastore.sdf"))
			{
				using (SmartClientDatabase database = DatabaseFactory.CreateDatabase() as SmartClientDatabase)
				{
					string sql = "select * from TestTable";

					DbParameter[] parameters = new DbParameter[]
						{
							database.CreateParameter("@Param1", DbType.Int32, 0, 1),
							database.CreateParameter("@Param2", DbType.String, 50, "Uno")
						};

					using (IDataReader reader = database.ExecuteReader(sql, parameters))
					{
						reader.Read();
						Assert.AreEqual(1, reader["TestColumn"]);
						Assert.AreEqual("Uno", reader["TestColumn2"]);
						reader.Read();
						Assert.AreEqual(2, reader["TestColumn"]);
						Assert.AreEqual("Dos", reader["TestColumn2"]);
						reader.Read();
						Assert.AreEqual(3, reader["TestColumn"]);
						Assert.AreEqual("Tres", reader["TestColumn2"]);
						reader.Read();
						Assert.AreEqual(4, reader["TestColumn"]);
						Assert.AreEqual("Cuatro", reader["TestColumn2"]);
					}
				}
			}
		}
	}
}