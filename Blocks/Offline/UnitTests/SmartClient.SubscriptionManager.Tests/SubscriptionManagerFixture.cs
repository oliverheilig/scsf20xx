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
using System.Text;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Threading;
using Microsoft.Practices.SmartClient.DataAccess;
using Microsoft.Practices.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.SmartClient.Subscriptions.Tests
{
	//
	// Goals:
	//
	// The subscription manager is designed to handle the details of working with SqlServerCE
	// subscriptions. Here is a list of requirements:
	//
	//	*	List of Subscriptions: Be able to obtain a list of the current subscriptions.
	//	*	Add a new subscription
	//	*	Adding a new subscription that matches an existing subscription (with publisher data)
	//		throws an exception.
	//	*	Remove an existing subscription
	//	*	Last Sync Time: Be able to get the last time a subscription was synced.
	//	*	Sync a subscription synchronously.
	//	*	Sync a subscription asynchronously and report status during sync and status when
	//		sync is finished or aborts.
	//	*	Be able to set a filter (HostName) for a subscription. This cannont be changed to an
	//		existing subscription--you must drop then add the subscription again to change it.
	//	*	Provide credentials that will be used for a subscription, and be able to update them.
	//	*	Choose the type of authentication (NT or SQL).
	//
	//
	//	Note:	These tests won't run properly on a multi-processor machine. The problem is that
	//			the test runner will allocate a thread to each processor and run multiple tests
	//			at the same time, and each test copyies the database file and then opens it,
	//			resulting in a lock. When tests run on a single-processor machine, this works
	//			fine, but on a multi-processor machine, threads run into lock errors when trying
	//			to copy the database file when another thread is already using that file.
	[TestClass]
	public class SubscriptionManagerFixture
	{
		private const int asyncTimeout = 50000;
		private string connectionStringPattern = @"Data Source=""{0}""";
		private string connectionString;
		private static TestResourceFile dbFile;
		private SqlDatabase subscriptionDatabase;

		[TestInitialize]
		public void CopyDatabaseFile()
		{
			dbFile = new TestResourceFile(this, "TestSubscription.sdf");
			connectionString = String.Format(connectionStringPattern, dbFile.Filename);
			subscriptionDatabase = new SqlDatabase(connectionString);
		}

		[TestCleanup]
		public void RemoveDatabaseFile()
		{
			subscriptionDatabase.Dispose();
			//subscriptionDatabase = null;

			dbFile.Dispose();
			dbFile = null;
		}

		[TestMethod]
		public void CanCreateSubscriptionmanager()
		{
			SqlSubscriptionManager sub = CreateSubscriptionManager();
			Assert.IsNotNull(sub);
		}

		[TestMethod]
		public void NumberOfSubscriptionsIsInitiallyZero()
		{
			SqlSubscriptionManager sub = CreateSubscriptionManager();
			Assert.AreEqual(0, sub.Subscriptions.Count);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void ThrowsArgumentOutOfRangeIfIndexOutOfRange()
		{
			SqlSubscriptionManager subMgr = CreateSubscriptionManager();
			Subscription sub = subMgr.Subscriptions[99];
		}

		[TestMethod]
		[ExpectedException(typeof(KeyNotFoundException))]
		public void ThrowsKeyNotFoundExceptionWhenNoSuchSubscription()
		{
			SqlSubscriptionManager subMgr = CreateSubscriptionManager();
			Subscription sub = subMgr.Subscriptions["Junk"];
		}

		[TestMethod]
		public void CanAddSubscription()
		{
			SqlSubscriptionManager subMgr = CreateSubscriptionManager();
			SubscriptionParameters subParams = CreateSubscriptionParams("PubCustomers", "CustomersTest");
			subMgr.Add(subParams);

			Assert.AreEqual(1, subMgr.Subscriptions.Count);
			Assert.AreEqual("CustomersTest", subMgr.Subscriptions[0].Subscriber);
		}

		[TestMethod]
		public void CanAddSubscriptionWithReplicationInstance()
		{
			SqlSubscriptionManager subMgr = CreateSubscriptionManager();
			SqlCeReplication repl = new SqlCeReplication();
			repl.Publication = "PubCustomers";
			repl.Publisher = "MOBGUISQL01";
			repl.PublisherDatabase = "AdventureWorksMobileStaging";
			repl.InternetUrl = "http://mobguisql01/PublicationCustomers/sqlcesa30.dll";
			repl.Subscriber = "Test";
			repl.HostName = "DE";
			subMgr.Add(repl);

			Assert.AreEqual(1, subMgr.Subscriptions.Count);
			Assert.AreEqual("Test", subMgr.Subscriptions[0].Subscriber);
		}

		[TestMethod]
		public void CanAddSubscriptionWithReplicationInstanceExtendedProps()
		{
			SqlSubscriptionManager subMgr = CreateSubscriptionManager();
			SqlCeReplication repl = new SqlCeReplication();
			repl.Publication = "PubCustomers";
			repl.Publisher = "MOBGUISQL01";
			repl.PublisherDatabase = "AdventureWorksMobileStaging";
			repl.InternetUrl = "http://mobguisql01/PublicationCustomers/sqlcesa30.dll";
			repl.Subscriber = "Test";
			repl.HostName = "DE";
			repl.Distributor = "junk";

			subMgr.Add(repl);

			subMgr = CreateSubscriptionManager();

			Assert.AreEqual(1, subMgr.Subscriptions.Count);
			Assert.AreEqual("junk", subMgr.Subscriptions[0].Distributor);
		}

		[TestMethod]
		public void ExtendedPropsArePutIntoReplicationObject()
		{
			SqlSubscriptionManager subMgr = CreateSubscriptionManager();
			SqlCeReplication repl = new SqlCeReplication();
			repl.Publication = "PubCustomers";
			repl.Publisher = "MOBGUISQL01";
			repl.PublisherDatabase = "AdventureWorksMobileStaging";
			repl.InternetUrl = "http://mobguisql01/PublicationCustomers/sqlcesa30.dll";
			repl.Subscriber = "Test";
			repl.HostName = "DE";
			repl.Distributor = "junk";

			SubscriptionCredentialsMock.FoundJunkDistributor = false;
			subMgr.Add(repl);
			try
			{
				subMgr.Synchronize(subMgr.Subscriptions[0]);
			}
			catch {}
			Assert.IsTrue(SubscriptionCredentialsMock.FoundJunkDistributor);
		}

		[TestMethod]
		public void SubscriptionsIsNotNull()
		{
			SqlSubscriptionManager subMgr = CreateSubscriptionManager();
			Assert.IsNotNull(subMgr.Subscriptions);
		}

		[TestMethod]
		public void CanGetSubscriptionByName()
		{
			SqlSubscriptionManager subMgr = CreateSubscriptionManager();
			SubscriptionParameters subParams = CreateSubscriptionParams("PubCustomers", "CustomersTest");
			subMgr.Add(subParams);

			subParams = CreateSubscriptionParams("PubTest", "AnotherTest");
			subMgr.Add(subParams);

			Subscription sub = subMgr.Subscriptions["CustomersTest"];
			Assert.AreEqual("CustomersTest", sub.Subscriber);

			sub = subMgr.Subscriptions["AnotherTest"];
			Assert.AreEqual("AnotherTest", sub.Subscriber);
		}

		[TestMethod]
		public void ContainsKeyReturnsTrueWhenSubscriptoinExists()
		{
			SqlSubscriptionManager subMgr = CreateSubscriptionManager();
			SubscriptionParameters subParams = CreateSubscriptionParams("PubCustomers", "CustomersTest");
			subMgr.Add(subParams);

			Assert.IsTrue(subMgr.Subscriptions.ContainsKey("CustomersTest"));
		}

		[TestMethod]
		public void ContainsKeyReturnsFalseWhenSubscriptionNotPresent()
		{
			SqlSubscriptionManager subMgr = CreateSubscriptionManager();
			SubscriptionParameters subParams = CreateSubscriptionParams("PubCustomers", "CustomersTest");
			Assert.IsFalse(subMgr.Subscriptions.ContainsKey("junk"));
			subMgr.Add(subParams);
			Assert.IsFalse(subMgr.Subscriptions.ContainsKey("junk"));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ContainsKeyThrowsForNullKey()
		{
			SqlSubscriptionManager subMgr = CreateSubscriptionManager();
			subMgr.Subscriptions.ContainsKey(null);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void AddingSubscriptionWithDuplicatePublisherFails()
		{
			SqlSubscriptionManager subMgr = CreateSubscriptionManager();
			SubscriptionParameters sub = CreateSubscriptionParams("PubCustomers", "CustomersTest");

			subMgr.Add(sub);
			sub = CreateSubscriptionParams("PubCustomers", "Customers2");
			subMgr.Add(sub);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void AddingDuplicateSubscriptionFails()
		{
			SqlSubscriptionManager subMgr = CreateSubscriptionManager();
			SubscriptionParameters sub = CreateSubscriptionParams("PubCustomers", "CustomersTest");

			subMgr.Add(sub);
			subMgr.Add(sub);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void AddingDuplicateSubscriptionFailsAfterDeviceRestart()
		{
			SqlSubscriptionManager subMgr = CreateSubscriptionManager();
			SubscriptionParameters sub = CreateSubscriptionParams("PubCustomers", "CustomersTest");
			subMgr.Add(sub);

			subMgr = CreateSubscriptionManager();
			sub = CreateSubscriptionParams("PubCustomers", "CustomersTest");
			subMgr.Add(sub);
		}

		[TestMethod]
		[Ignore]		// Requires setting up IIS fro subscription
		public void NewSubscriptionCanReturnData()
		{
			SubscriptionParameters subParams = CreateSubscriptionParams("PubCustomers", "CustomersTest");
			SqlSubscriptionManager subMgr = CreateSubscriptionManager();
			subMgr.Add(subParams);

			using (Database database = new SqlDatabase(connectionString))
			{
				Assert.IsFalse(database.TableExists("Customer"));

				subMgr.Synchronize(subMgr.Subscriptions[0]);

				Assert.IsTrue(database.TableExists("Customer"));
				Assert.IsTrue(CountCustomers(connectionString) > 0, "Didn't return any records");
			}
		}

		[TestMethod]
		[Ignore]		// Requires setting up IIS fro subscription
		public void CanSyncBySubscriberName()
		{
			SubscriptionParameters subParams = CreateSubscriptionParams("PubCustomers", "CustomersTest");
			SqlSubscriptionManager subMgr = CreateSubscriptionManager();
			subMgr.Add(subParams);

			using (Database database = new SqlDatabase(connectionString))
			{
				Assert.IsFalse(database.TableExists("Customer"));

				subMgr.Synchronize("CustomersTest");

				Assert.IsTrue(database.TableExists("Customer"), "Customer table doesn't exist");
				Assert.IsTrue(CountCustomers(connectionString) > 0, "Didn't return any records");
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void SyncOfOldSubscriptionFails()
		{
			SubscriptionParameters subParams = CreateSubscriptionParams("PubCustomers", "CustomersTest");
			SqlSubscriptionManager subMgr = CreateSubscriptionManager();
			subMgr.Add(subParams);

			Subscription sub = subMgr.Subscriptions[0];

			subMgr = CreateSubscriptionManager();
			subMgr.Synchronize(sub);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void AsyncSyncOfOldSubscriptionFails()
		{
			SubscriptionParameters subParams = CreateSubscriptionParams("PubCustomers", "CustomersTest");
			SqlSubscriptionManager subMgr = CreateSubscriptionManager();
			subMgr.Add(subParams);

			Subscription sub = subMgr.Subscriptions[0];

			subMgr = CreateSubscriptionManager();
			subMgr.BeginSynchronize(sub);
		}

		[TestMethod]
		[Ignore]		// Requires setting up IIS fro subscription
		public void LastSyncTimeBecomesNonNullAfterSync()
		{
			SubscriptionParameters subParams = CreateSubscriptionParams("PubCustomers", "CustomersTest");
			SqlSubscriptionManager subMgr = CreateSubscriptionManager();
			subMgr.Add(subParams);
			Subscription sub = subMgr.Subscriptions[0];
			Assert.IsNull(sub.LastSyncTime);
			subMgr.Synchronize(sub);

			subMgr = CreateSubscriptionManager();
			Assert.IsNotNull(sub.LastSyncTime);
		}

		[TestMethod]
		[Ignore]		// Requires setting up IIS fro subscription
		public void LastSyncTimeUpdatedAfterSync()
		{
			SqlSubscriptionManager subMgr = CreateSubscriptionManager();
			SubscriptionParameters subParams = CreateSubscriptionParams("PubCustomers", "CustomersTest");
			subMgr.Add(subParams);
			Assert.IsNull(subMgr.Subscriptions[0].LastSyncTime);

			subMgr.Synchronize(subMgr.Subscriptions[0]);
			Assert.IsNotNull(subMgr.Subscriptions[0].LastSyncTime);
			DateTime oldSyncTime = (DateTime)subMgr.Subscriptions[0].LastSyncTime;
			Thread.Sleep(1000);
			subMgr.Synchronize(subMgr.Subscriptions[0]);
			Assert.IsTrue(subMgr.Subscriptions[0].LastSyncTime > oldSyncTime);
		}

		[TestMethod]
		[Ignore]		// Requires setting up IIS fro subscription
		public void PublicationNameIsCorrectAfterSync()
		{
			SubscriptionParameters subParams = CreateSubscriptionParams("PubCustomers", "CustomersTest");
			SqlSubscriptionManager subMgr = CreateSubscriptionManager();
			subMgr.Add(subParams);
			subMgr.Synchronize(subMgr.Subscriptions[0]);

			subMgr = CreateSubscriptionManager();
			Assert.AreEqual("PubCustomers", subMgr.Subscriptions[0].Publication);
		}

		[TestMethod]
		[Ignore]		// Requires setting up IIS fro subscription
		public void CanDropSubscription()
		{
			SubscriptionParameters subParams = CreateSubscriptionParams("PubCustomers", "CustomersTest");
			SqlSubscriptionManager subMgr = CreateSubscriptionManager();
			subMgr.Add(subParams);
			subMgr.Synchronize(subMgr.Subscriptions[0]);

			subMgr = CreateSubscriptionManager();
			subMgr.Drop(subMgr.Subscriptions[0]);
			Assert.AreEqual(0, subMgr.Subscriptions.Count);

			subMgr = CreateSubscriptionManager();
			Assert.AreEqual(0, subMgr.Subscriptions.Count);
		}

		[TestMethod]
		public void CanDropSubscriptionAfterAppRestart()
		{
			SubscriptionParameters subParams = CreateSubscriptionParams("PubCustomers", "CustomersTest");
			SqlSubscriptionManager subMgr = CreateSubscriptionManager();
			subMgr.Add(subParams);

			subMgr = CreateSubscriptionManager();
			subMgr.Drop(subMgr.Subscriptions[0]);
			Assert.AreEqual(0, subMgr.Subscriptions.Count);
		}

		[TestMethod]
		[Ignore]		// Requires setting up IIS fro subscription
		public void CanDropSubscriptionByName()
		{
			SubscriptionParameters subParams = CreateSubscriptionParams("PubCustomers", "CustomersTest");
			SqlSubscriptionManager subMgr = CreateSubscriptionManager();
			subMgr.Add(subParams);
			subMgr.Synchronize("CustomersTest");

			subMgr = CreateSubscriptionManager();
			subMgr.Drop("CustomersTest");
			Assert.AreEqual(0, subMgr.Subscriptions.Count);

			subMgr = CreateSubscriptionManager();
			Assert.AreEqual(0, subMgr.Subscriptions.Count);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		[Ignore]		// Requires setting up IIS fro subscription
		public void DropSubscriptionTwiceThrowsException()
		{
			SubscriptionParameters subParams = CreateSubscriptionParams("PubCustomers", "CustomersTest");
			SqlSubscriptionManager subMgr = CreateSubscriptionManager();
			subMgr.Add(subParams);
			subMgr.Synchronize(subMgr.Subscriptions[0]);

			subMgr = CreateSubscriptionManager();
			Subscription sub = subMgr.Subscriptions[0];
			subMgr.Drop(sub);
			subMgr.Drop(sub);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		[Ignore]		// Requires setting up IIS fro subscription
		public void DropInvalidSubscriptionThrowsException()
		{
			SubscriptionParameters subParams = CreateSubscriptionParams("PubCustomers", "CustomersTest");
			SqlSubscriptionManager subMgr = CreateSubscriptionManager();
			subMgr.Add(subParams);
			subMgr.Synchronize(subMgr.Subscriptions[0]);

			Subscription sub = subMgr.Subscriptions[0];

			subMgr = CreateSubscriptionManager();
			subMgr.Drop(sub);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void SyncingDroppedSubscriptionThrowsException()
		{
			SubscriptionParameters subParams = CreateSubscriptionParams("PubCustomers", "CustomersTest");
			SqlSubscriptionManager subMgr = CreateSubscriptionManager();
			subMgr.Add(subParams);

			Subscription sub = subMgr.Subscriptions[0];

			subMgr.Drop(sub);
			subMgr.Synchronize(sub);
		}

		[TestMethod]
		[Ignore]		// Requires setting up IIS fro subscription
		public void DifferentFilterReturnsDifferentData()
		{
			SubscriptionParameters subParams = CreateSubscriptionParams("PubCustomers", "CustomersTest");
			subParams.Filter = "DE";
			SqlSubscriptionManager subMgr = CreateSubscriptionManager();
			subMgr.Add(subParams);

			Subscription sub = subMgr.Subscriptions[0];
			subMgr.Synchronize(sub);
			int count = CountCustomers(connectionString);

			subMgr.Drop(sub);
			subParams.Filter = "DK";
			subMgr.Add(subParams);
			sub = subMgr.Subscriptions[0];
			subMgr.Synchronize(sub);
			int count2 = CountCustomers(connectionString);
			Assert.AreNotEqual(count, count2);
		}

		[TestMethod]
		[Ignore]		// Requires review
		public void AsynchronousSyncFiresEvents()
		{
			SqlSubscriptionManager subMgr = CreateSubscriptionManager();
			SubscriptionParameters subParams = CreateSubscriptionParams("PubCustomers", "CustomersTest");
			subMgr.Add(subParams);

			bool didStartTableDownload = false;
			bool didSyncCompletion = false;
			bool didSynchronization = false;

			Subscription sub = subMgr.Subscriptions[0];
			sub.SyncCompleted += delegate { didSyncCompletion = true; };
			sub.SyncProgress += delegate { didSynchronization = true; };
			sub.TableDownloadStarted += delegate { didStartTableDownload = true; };

			subMgr.BeginSynchronize(sub);
			sub.AsyncResult.AsyncWaitHandle.WaitOne(asyncTimeout, false);
			Assert.IsTrue(didStartTableDownload);
			Assert.IsTrue(didSyncCompletion);
			Assert.IsTrue(didSynchronization);

			subMgr.EndSynchronize(sub);
		}

		[TestMethod]
		public void CanCancelAsynchronousSync()
		{
			SubscriptionParameters subParams = CreateSubscriptionParams("PubCustomers", "CustomersTest");
			SqlSubscriptionManager subMgr = CreateSubscriptionManager();
			subMgr.Add(subParams);

			Subscription sub = subMgr.Subscriptions[0];

			subMgr.BeginSynchronize(sub);
			sub.AsyncResult.AsyncWaitHandle.WaitOne(500, false);
			subMgr.CancelSynchronize(sub);
			sub.AsyncResult.AsyncWaitHandle.WaitOne(asyncTimeout, false);

			using (Database database = new SqlDatabase(connectionString))
			{
				Assert.IsFalse(database.TableExists("Customer"));
			}
		}

		[TestMethod]
		[Ignore]		// Requires review
		public void AsynchronousSyncUpdatesLastSyncTime()
		{
			SubscriptionParameters subParams = CreateSubscriptionParams("PubCustomers", "CustomersTest");
			SqlSubscriptionManager subMgr = CreateSubscriptionManager();
			subMgr.Add(subParams);

			Subscription sub = subMgr.Subscriptions[0];

			subMgr.BeginSynchronize(sub);
			sub.AsyncResult.AsyncWaitHandle.WaitOne(asyncTimeout, false);
			Assert.IsTrue(sub.AsyncResult.IsCompleted);
			Assert.IsNotNull(sub.LastSyncTime);

			subMgr.EndSynchronize(sub);

			Assert.IsNotNull(sub.LastSyncTime);
		}

		[TestMethod]
		public void AsynEndSyncThrowsExceptionWhenNoSuchPublication()
		{
			SubscriptionParameters subParams = CreateSubscriptionParams("NoSuchPublication", "Test");
			SqlSubscriptionManager subMgr = CreateSubscriptionManager();
			subMgr.Add(subParams);

			Subscription sub = subMgr.Subscriptions["Test"];
			subMgr.BeginSynchronize(sub);
			sub.AsyncResult.AsyncWaitHandle.WaitOne(asyncTimeout, false);

			SqlCeException endException = null;
			try
			{
				subMgr.EndSynchronize(sub);
			}
			catch (SqlCeException ex)
			{
				endException = ex;
			}
			Assert.IsNotNull(endException, "Did not raise excepected exception");
		}

		[TestMethod]
		public void AsyncEndSyncReloadsSubscriptionsEvenWhenThrowingException()
		{
			SubscriptionParameters subParams = CreateSubscriptionParams("NoSuchPublication", "Test");
			SqlSubscriptionManager subMgr = CreateSubscriptionManager();
			subMgr.Add(subParams);

			Subscription sub = subMgr.Subscriptions["Test"];
			subMgr.BeginSynchronize(sub);
			sub.AsyncResult.AsyncWaitHandle.WaitOne(asyncTimeout, false);

			subMgr.ClearCache();
			try
			{
				subMgr.EndSynchronize(sub);
			}
			catch
			{
			}
			Assert.IsTrue(subMgr.SubscriptionsAreCurrent);
		}


		[TestMethod]
		[ExpectedException(typeof(SqlCeException))]
		public void BeginSynchronizeThrowsProperExceptionIfCredentialsNotFound()
		{
			SqlSubscriptionManager manager = new SqlSubscriptionManager(subscriptionDatabase, new SubscriptionNullCredentialServiceMock());
			SubscriptionParameters parameters = CreateSubscriptionParams("PubCustomers", "NoCredentials");

			manager.Add(parameters);

			Subscription sub = manager.Subscriptions[0];

			//Throws System.NullReferenceException: 
			manager.BeginSynchronize(sub);

			sub.AsyncResult.AsyncWaitHandle.WaitOne(10000, false);

			manager.EndSynchronize(sub);

			Assert.IsNotNull(sub.LastSyncTime);
		}

		[TestMethod]
		[ExpectedException(typeof(SqlCeException))]
		public void SynchronizeThrowsProperExceptionIfCredentialsNotFound()
		{
			SqlSubscriptionManager manager = new SqlSubscriptionManager(subscriptionDatabase, new SubscriptionNullCredentialServiceMock());
			SubscriptionParameters parameters = CreateSubscriptionParams("PubCustomers", "NoCredentials");
			Assert.AreEqual(0, manager.Subscriptions.Count);
			manager.Add(parameters);

			Subscription subs = manager.Subscriptions[0];

			//Throws System.NullReferenceException: 
			manager.Synchronize(subs);

			Assert.AreEqual(1, manager.Subscriptions.Count);
		}

		private SqlSubscriptionManager CreateSubscriptionManager()
		{
			SqlSubscriptionManager sub = new SqlSubscriptionManager(subscriptionDatabase, new SubscriptionCredentialServiceMock());
			return sub;
		}

		private static SubscriptionParameters CreateSubscriptionParams(string publication, string subscriber)
		{
			SubscriptionParameters subscription = new SubscriptionParameters();
			subscription.Publication = publication;
			subscription.Publisher = "MOBGUISQL01";
			subscription.PublisherDatabase = "AdventureWorksMobileStaging";
			subscription.InternetUrl = "http://mobguisql01/PublicationCustomers/sqlcesa30.dll";
			subscription.Subscriber = subscriber;
			subscription.Filter = "DE";
			return subscription;
		}

		private int CountCustomers(string connectString)
		{
			using (SqlCeConnection connection = new SqlCeConnection(connectString))
			{
				connection.Open();

				string sql = @"
						SELECT	COUNT(*)
						FROM	Customer";

				using (SqlCeCommand command = new SqlCeCommand(sql, connection))
				{
					return (int)command.ExecuteScalar();
				}
			}
		}
	}
}
