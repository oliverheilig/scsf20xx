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
using System.Text;
using System.Collections.ObjectModel;
using System.Data.SqlServerCe;
using Microsoft.Practices.SmartClient.DataAccess;
using System.Data.Common;
using System.Data;

namespace Microsoft.Practices.SmartClient.Subscriptions
{
	/// <summary>
	///		This class manages a set of subscriptions in the database. For this class to work
	///		correctly, all subscriptions must be added and removed via this manager.
	/// </summary>
	/// <remarks>
	///		The synchronization supported by this class is one-way, with data from the
	///		server being downloaded to the local database as reference data.
	/// </remarks>
	//
	// This class relies on a table in the local database called SubscriptionInfo that contains
	// all the data we need to persist so we can register and synchronize the subscription with
	// SqlServerCe once we're connected to the network.
	//
	// We require this table to work around some behavior with SqlServerCe replication that
	// doesn't match the requirements for this class. In particular, we need to be able to
	// "register" subscriptions when the device is off-line. But SqlServerCe's replication
	// doesn't save any of the replication data in the database until you synchronize, which
	// could happen quite some time in the future. Additionally, the SaveProperties method
	// of the replication object fails if you attempt to call it after making changes, but
	// before synchronizing.
	//
	// Here is a list of assumptions that we've made:
	// 
	//	*	Any time you start a synchronization operation, we set the flag to attempt
	//		connecting using Windows CE's Connection Manager.
	//	*	We assume the local data we're synchornizing is read-only reference data that is
	//		copied from server to local store only.
	//
	public class SqlSubscriptionManager
	{
		private SubscriptionCollection subscriptions = new SubscriptionCollection();
		private ISubscriptionCredentialService subscriptionCredentials;
		private IDatabase infoDatabase;
		private IDatabase refDatabase;
		private bool reloadSubscriptions = true;


		private SqlSubscriptionManager() { }

		/// <summary>
		///		Constructor for the <see cref="SubscriptionManager"/> class.
		/// </summary>
		/// <param name="refDatabase">
		///		Database that will contain the actual subscriptions and data retrieved by these
		///		subscriptions. Can be the same database as the <paramref name="infoDatabase"/> parameter.
		/// </param>
		/// <param name="infoDatabase">
		///		Database that will be used by <see cref="SubscriptionManager"/> to store data about
		///		registered subscriptions.
		/// </param>
		/// <param name="subCredentials">
		///		The object that can provide credentials for subscriptions.
		/// </param>
		public SqlSubscriptionManager(IDatabase referenceDatabase, IDatabase infoDatabase, ISubscriptionCredentialService subscriptionCredentials)
		{
			Guard.ArgumentNotNull(referenceDatabase, "referenceDatabase");
			Guard.ArgumentNotNull(infoDatabase, "infoDatabase");
			Guard.ArgumentNotNull(subscriptionCredentials, "subscriptionCredentials");

			this.infoDatabase = infoDatabase;
			this.refDatabase = referenceDatabase;
			this.subscriptionCredentials = subscriptionCredentials;

			if (!infoDatabase.TableExists("SubscriptionInfo"))
				CreateDatabaseTables();
		}

		/// <summary>
		///		Creates a new instance of this class and provides it with the required services.
		/// </summary>
		/// <param name="databaseService">
		///		Provides the connection to the local database where subscriptions will be managed
		///		by this class.
		/// </param>
		/// <param name="subCredentials">
		///		Provides the credentials needed for a subscription to connect to the server
		///		that has the publication used by the subscription.
		/// </param>
		public SqlSubscriptionManager(IDatabase databaseService, ISubscriptionCredentialService subCredentials)
			: this(databaseService, databaseService, subCredentials)
		{
		}

		/// <summary>
		///		Reports if the <see cref="Subscriptions"/> collection is current. If this property
		///		is false, it means that the list of subscriptions need to be reloaded into memory.
		/// </summary>
		/// <remarks>
		///		We added this property to help with writing the unit tests, and it doesn't really
		///		provide much useful information since other methods will automatically reload
		///		the subscription list if it needs to be updated.
		/// </remarks>
		public bool SubscriptionsAreCurrent
		{
			get { return !reloadSubscriptions; }
		}


		/// <summary>
		///		A read-only list of subscriptions that have been registered with this subscription
		///		manager in the local database described by the <seealso cref="DatabaseService"/> property.
		/// </summary>
		/// <remarks>
		///		This read-only list will change if you add or drop a subscription.
		/// </remarks>
		public SubscriptionCollection Subscriptions
		{
			get
			{
				if (reloadSubscriptions)
					ReloadSubscriptions();

				return subscriptions;
			}
		}

		/// <summary>
		///		Adds a new subscription to the database. Call <see cref="BeginSync"/> to
		///		download the subscription from the server.
		/// </summary>
		/// <remarks>
		///		The subscription information is initially stored in the SubscriptionInfo
		///		table only in the local database. It will also appear in system tables
		///		managed by SQL Server Mobile after the first synchronization. Use
		///		<see cref="BeginSync"/> or <see cref="Synchronize"/> to synchronize
		///		the data.
		/// </remarks>
		/// <param name="sub"></param>
		/// <exception cref="ArgumentException">
		///		A subscription with the same publisher information already exists.
		/// </exception>
		public void Add(SubscriptionParameters subscription)
		{
			Guard.ArgumentNotNull(subscription, "subscription");
			SqlCeReplication replication = new SqlCeReplication();

			replication.Publication = subscription.Publication;
			replication.Publisher = subscription.Publisher;
			replication.PublisherDatabase = subscription.PublisherDatabase;
			replication.InternetUrl = subscription.InternetUrl;
			replication.Subscriber = subscription.Subscriber;
			replication.HostName = subscription.Filter;

			Add(replication);

			replication.Dispose();
		}

		/// <summary>
		///		Adds a new subscription to the database. Call <see cref="BeginSync"/> to
		///		download the subscription from the server.
		/// </summary>
		/// <remarks>
		/// <para>
		///		The subscription information is initially stored in the SubscriptionInfo
		///		table only in the local database. It will also appear in system tables
		///		managed by SQL Server Mobile after the first synchronization. Use
		///		<see cref="BeginSync"/> or <see cref="Synchronize"/> to synchronize
		///		the data.
		/// </para>
		/// <para>
		///		Note: You should use the <paramref name="sub"/> parameter only as a carrier of
		///		data. In other words, do not call any methods on this instance--let this class
		///		handle all the method calls.
		/// </para>
		/// </remarks>
		/// <param name="sub"></param>
		/// <exception cref="ArgumentException">
		///		A subscription with the same publisher information already exists.
		/// </exception>
		public void Add(SqlCeReplication subscription)
		{
			Guard.ArgumentNotNull(subscription, "subscription");
			Subscription target = new Subscription();
			target.Publication = subscription.Publication;
			target.Publisher = subscription.Publisher;
			target.PublisherDatabase = subscription.PublisherDatabase;

			if (Subscriptions.Find(target) >= 0)
				throw new ArgumentException(Properties.Resources.SubscriptionExists);

			string sql = @"INSERT SubscriptionInfo
			(
				Publication,
				Publisher,
				PublisherDatabase,
				InternetUrl,
				Subscriber,
				HostName,
				CompressionLevel,
				ConnectionRetryTimeout,
				Distributor,
				DistributorAddress,
				DistributorNetwork,
				ExchangeType,
				InternetProxyServer,
				LoginTimeout,
				ProfileName,
				PublisherAddress,
				PublisherNetwork,
				QueryTimeout,
				SnapshotTransferType,
				Validate)
			VALUES(
				@Publication,
				@Publisher,
				@PublisherDatabase,
				@InternetUrl,
				@Subscriber,
				@HostName,
				@CompressionLevel,
				@ConnectionRetryTimeout,
				@Distributor,
				@DistributorAddress,
				@DistributorNetwork,
				@ExchangeType,
				@InternetProxyServer,
				@LoginTimeout,
				@ProfileName,
				@PublisherAddress,
				@PublisherNetwork,
				@QueryTimeout,
				@SnapshotTransferType,
				@Validate)";

			DbParameter[] parameters = new DbParameter[] {
				infoDatabase.CreateParameter("@Publication", DbType.String, 4000, subscription.Publication),
				infoDatabase.CreateParameter("@Publisher", DbType.String, 4000, subscription.Publisher),
				infoDatabase.CreateParameter("@PublisherDatabase", DbType.String, 4000, subscription.PublisherDatabase),
				infoDatabase.CreateParameter("@InternetUrl", DbType.String, 4000, subscription.InternetUrl),
				infoDatabase.CreateParameter("@Subscriber", DbType.String, 4000, subscription.Subscriber),
				infoDatabase.CreateParameter("@HostName", DbType.String, 4000, subscription.HostName),
				infoDatabase.CreateParameter("@CompressionLevel", DbType.Int16, 4000, subscription.CompressionLevel),
				infoDatabase.CreateParameter("@ConnectionRetryTimeout", DbType.Int16, 0, subscription.ConnectionRetryTimeout),
				infoDatabase.CreateParameter("@Distributor", DbType.String, 4000, subscription.Distributor ?? String.Empty),
				infoDatabase.CreateParameter("@DistributorAddress", DbType.String, 4000, subscription.DistributorAddress ?? String.Empty),
				infoDatabase.CreateParameter("@DistributorNetwork", DbType.Int32, 0, (int) subscription.DistributorNetwork),
				infoDatabase.CreateParameter("@ExchangeType", DbType.Int32, 0, subscription.ExchangeType),
				infoDatabase.CreateParameter("@InternetProxyServer", DbType.String, 4000, subscription.InternetProxyServer ?? String.Empty),
				infoDatabase.CreateParameter("@LoginTimeout", DbType.Int16, 0, subscription.LoginTimeout),
				infoDatabase.CreateParameter("@ProfileName", DbType.String, 4000, subscription.ProfileName ?? String.Empty),
				infoDatabase.CreateParameter("@PublisherAddress", DbType.String, 4000, subscription.PublisherAddress ?? String.Empty),
				infoDatabase.CreateParameter("@PublisherNetwork", DbType.Int32, 0, (int) subscription.PublisherNetwork),
				infoDatabase.CreateParameter("@QueryTimeout", DbType.Int16, 0, subscription.QueryTimeout),
				infoDatabase.CreateParameter("@SnapshotTransferType", DbType.Int32, 0, subscription.SnapshotTransferType),
				infoDatabase.CreateParameter("@Validate", DbType.Int32, 0, subscription.Validate)
			};

			infoDatabase.ExecuteNonQuery(sql, parameters);

			ReloadSubscriptions();
		}

		private void CreateDatabaseTables()
		{
			string sql = "CREATE TABLE SubscriptionInfo(" +
						 "SubscriptionInfoID int IDENTITY PRIMARY KEY NOT NULL," +
						 "Publication NVARCHAR(4000) NOT NULL," +
						 "Publisher NVARCHAR(4000) NOT NULL," +
						 "PublisherDatabase NVARCHAR(4000) NOT NULL," +
						 "InternetUrl NVARCHAR(4000) NOT NULL," +
						 "Subscriber NVARCHAR(4000) NOT NULL," +
						 "HostName NVARCHAR(4000)," +
						 "CompressionLevel smallint NOT NULL," +
						 "ConnectionRetryTimeout smallint NOT NULL," +
						 "Distributor nvarchar(4000) NOT NULL," +
						 "DistributorAddress nvarchar(4000) NOT NULL," +
						 "DistributorNetwork int NOT NULL," +
						 "ExchangeType int NOT NULL," +
						 "InternetProxyServer nvarchar(4000) NOT NULL," +
						 "LoginTimeout smallint NOT NULL," +
						 "ProfileName nvarchar(4000) NOT NULL," +
						 "PublisherAddress nvarchar(4000) NOT NULL," +
						 "PublisherNetwork int NOT NULL," +
						 "QueryTimeout smallint NOT NULL," +
						 "SnapshotTransferType int NOT NULL," +
						 "Validate int NOT NULL" +
						 ")";
			infoDatabase.ExecuteNonQuery(sql);
		}

		/// <summary>
		///		Re-reads the subscription information from the database and reloads it
		///		into the Subscriptions collection.
		/// </summary>
		/// <remarks>
		///		Existing Subscription instances will remain valid after a reload as long as the
		///		subscription wasn't dropped from this subscription manager.
		/// </remarks>
		public void ReloadSubscriptions()
		{
			ReadSyncSubscriptions();
			ReadLastSyncTimes();
			reloadSubscriptions = false;
		}

		//
		// This method attempts to read subscription information from both the SubscriptionInfo
		// and __sysMergeSubscriptions tables. However, since the __sysMergeSubscriptions table
		// only exists if we've actually synchronized a subscription (as opposed to simply adding
		// it), this table may not exist.
		//
		private void ReadSyncSubscriptions()
		{
			string sql = @"
				SELECT	SubscriptionInfoID,
						Publication,
						Publisher,
						PublisherDatabase,
						InternetUrl,
						Subscriber,
						HostName,
						CompressionLevel,
						ConnectionRetryTimeout,
						Distributor,
						DistributorAddress,
						DistributorNetwork,
						ExchangeType,
						InternetProxyServer,
						LoginTimeout,
						ProfileName,
						PublisherAddress,
						PublisherNetwork,
						QueryTimeout,
						SnapshotTransferType,
						Validate
					  FROM	SubscriptionInfo";

			using (DbDataReader reader = infoDatabase.ExecuteReader(sql))
			{
				while (reader.Read())
				{
					Subscription subscription = new Subscription();
					subscription.SubscriptionInfoId = (int)reader["SubscriptionInfoId"];
					subscription.Publication = (string)reader["Publication"];
					subscription.Publisher = (string)reader["Publisher"];
					subscription.PublisherDatabase = (string)reader["PublisherDatabase"];

					int index = subscriptions.Find(subscription);
					if (index >= 0)					// Did we find an existing instance?
					{
						subscription = subscriptions[index];	// Yes, use existing one
					}
					else
					{
						lock (subscriptions)
						{
							subscription.SyncCompleted += OnSyncComplete;
							subscriptions.Add(subscription); // No, use new one and add it to the list
						}
					}

					subscription.CompressionLevel = (short)reader["CompressionLevel"];
					subscription.ConnectionRetryTimeout = (short)reader["ConnectionRetryTimeout"];
					subscription.Distributor = (string)reader["Distributor"];
					subscription.DistributorAddress = (string)reader["DistributorAddress"];
					subscription.DistributorNetwork = (NetworkType)(int)reader["DistributorNetwork"];
					subscription.ExchangeType = (ExchangeType)(int)reader["ExchangeType"];
					subscription.Filter = (string)Database.GetNullable(reader["HostName"]);
					subscription.InternetProxyServer = (string)reader["InternetProxyServer"];
					subscription.InternetUrl = (string)reader["InternetUrl"];
					subscription.LoginTimeout = (short)reader["LoginTimeout"];
					subscription.ProfileName = (string)reader["ProfileName"];
					subscription.PublisherAddress = (string)reader["PublisherAddress"];
					subscription.PublisherNetwork = (NetworkType)(int)reader["PublisherNetwork"];
					subscription.QueryTimeout = (short)reader["QueryTimeout"];
					subscription.SnapshotTransferType = (SnapshotTransferType)(int)reader["SnapshotTransferType"];
					subscription.Subscriber = (string)reader["Subscriber"];
					subscription.Validate = (ValidateType)(int)reader["Validate"];
				}
			}
		}

		private void ReadLastSyncTimes()
		{
			if (!refDatabase.TableExists("__sysMergeSubscriptions"))
				return;

			string sql = @"
				SELECT	Publication,
						Publisher,
						PublisherDatabase,
						LastSuccessfulSync
				  FROM	__sysMergeSubscriptions
			";

			using (DbDataReader reader = refDatabase.ExecuteReader(sql))
			{
				while (reader.Read())
				{
					int readerIndex = 0;
					Subscription subscription = new Subscription();
					subscription.Publication = reader.GetString(readerIndex++);
					subscription.Publisher = reader.GetString(readerIndex++);
					subscription.PublisherDatabase = reader.GetString(readerIndex++);

					int index = subscriptions.Find(subscription);
					if (index >= 0)					// Did we find an existing instance?
					{
						subscription = subscriptions[index];	// Yes, use existing one
						if (!reader.IsDBNull(readerIndex))
							subscription.LastSyncTime = reader.GetDateTime(readerIndex);
					}
				}
			}
		}

		private void OnSyncComplete(object sender, EventArgs args)
		{
			lock (subscriptions)
			{
				ReloadSubscriptions();
			}
		}

		/// <summary>
		///		Removes a subscription from the local database.
		/// </summary>
		/// <param name="subscription"></param>
		public void Drop(Subscription subscription)
		{
			Guard.ArgumentNotNull(subscription, "subscription");

			lock (subscriptions)
			{
				if (!Subscriptions.Contains(subscription))
					throw new ArgumentException(Properties.Resources.NoSuchSubscription);

				//
				// The subscription information isn't saved in the replication tables until the
				// subscription is synchronized.
				//
				if (subscription.HasBeenSynchronized)
				{
					SqlCeReplication replication = new SqlCeReplication();
					replication.SubscriberConnectionString = refDatabase.ConnectionString;
					replication.Publication = subscription.Publication;
					replication.Publisher = subscription.Publisher;
					replication.PublisherDatabase = subscription.PublisherDatabase;

					replication.DropSubscription(DropOption.LeaveDatabase);
				}

				string sql = @"DELETE FROM SubscriptionInfo WHERE SubscriptionInfoID=" + infoDatabase.BuildParameterName("ID");
				DbParameter idParameter = infoDatabase.CreateParameter("@ID", subscription.SubscriptionInfoId);
				infoDatabase.ExecuteNonQuery(sql, idParameter);

				subscriptions.Remove(subscription);
			}
		}

		/// <summary>
		///		Removes a subscription from both the <see cref="SubscriptionManager"/> database
		///		and from the Sql Server Mobile subscription list (if it's been synchronized).
		/// </summary>
		/// <param name="subscriber"></param>
		public void Drop(string subscriber)
		{
			Guard.ArgumentNotNull(subscriber, "subscriber");

			Subscription subscription = Subscriptions[subscriber];
			Drop(subscription);
		}

		/// <summary>
		///		Download data for the subscription from the server, and don't return until the download
		///		finishes.
		/// </summary>
		/// <param name="subscription">
		///		The subscription for which you want to download data from the server.
		/// </param>
		public void Synchronize(Subscription subscription)
		{
			Guard.ArgumentNotNull(subscription, "subscription");

			if (!Subscriptions.Contains(subscription))
				throw new ArgumentException(Properties.Resources.NoSuchSubscription);

			SubscriptionCredentials credentials = subscriptionCredentials.FindCredentials(subscription);
			SqlCeReplication replication = GetSyncReplication(subscription);

			if (credentials != null)
				credentials.ApplyCredentials(replication);

			try
			{
				replication.Synchronize();
			}
			finally
			{
				replication.Dispose();

				ReloadSubscriptions();
			}
		}

		/// <summary>
		///		Download data for the subscription from the server, and don't return until the download
		///		finishes.
		/// </summary>
		/// <param name="subscriber">
		///		The subscriber name that you provided when you registered the subscription with the
		///		<see cref="Add"/> method.
		/// </param>
		public void Synchronize(string subscriber)
		{
			Guard.ArgumentNotNull(subscriber, "subscriber");

			Subscription subscription = Subscriptions[subscriber];
			Synchronize(subscription);
		}

		private SqlCeReplication GetSyncReplication(Subscription subscription)
		{
			SqlCeReplication replication = new SqlCeReplication();

			replication.ConnectionManager = true;
			replication.SubscriberConnectionString = refDatabase.ConnectionString;
			replication.Publication = subscription.Publication;
			replication.Publisher = subscription.Publisher;
			replication.PublisherDatabase = subscription.PublisherDatabase;
			replication.Subscriber = subscription.Subscriber ?? String.Empty;
			replication.InternetUrl = subscription.InternetUrl ?? String.Empty;
			replication.HostName = subscription.Filter ?? String.Empty;

			//
			// Simply setting some of these values, even if we're not changing the
			// value, causes other values in the same group to become required. As
			// a result, we have to make sure we don't set null, etc.
			//
			replication.CompressionLevel = subscription.CompressionLevel;
			if (subscription.Distributor != null)
				replication.Distributor = subscription.Distributor;
			if (subscription.DistributorAddress != null)
				replication.DistributorAddress = subscription.DistributorAddress;
			if (replication.DistributorNetwork != subscription.DistributorNetwork)
				replication.DistributorNetwork = subscription.DistributorNetwork;
			replication.ExchangeType = subscription.ExchangeType;
			if (subscription.InternetProxyServer != null)
				replication.InternetProxyServer = subscription.InternetProxyServer;
			replication.LoginTimeout = subscription.LoginTimeout;
			if (subscription.ProfileName != null)
				replication.ProfileName = subscription.ProfileName;
			if (subscription.PublisherAddress != null)
				replication.PublisherAddress = subscription.PublisherAddress;
			replication.PublisherNetwork = subscription.PublisherNetwork;
			replication.QueryTimeout = subscription.QueryTimeout;
			replication.SnapshotTransferType = (SnapshotTransferType) subscription.SnapshotTransferType;
			replication.Validate = subscription.Validate;

			if (!subscription.HasBeenSynchronized)
				replication.AddSubscription(AddOption.ExistingDatabase);

			return replication;
		}

		/// <summary>
		///		Begin downloading data for the subscription and return control immediately.
		/// </summary>
		/// <remarks>
		/// <para>
		///		Before you call this method, you can register to receive events from the <paramref name="subscription"/>
		///		object. This will allow you to monitor the progress of the synchronization.
		/// </para>
		/// <para>
		///		Once the synchronization completes, you must call <see cref="EndSync"/>.
		/// </para>
		/// </remarks>
		/// <param name="subscription">
		///		The subscription you want to synchronize.
		/// </param>
		public void BeginSynchronize(Subscription subscription)
		{
			Guard.ArgumentNotNull(subscription, "subscription");

			if (!Subscriptions.Contains(subscription))
				throw new ArgumentException(Properties.Resources.NoSuchSubscription);

			SubscriptionCredentials credentials = subscriptionCredentials.FindCredentials(subscription);
			SqlCeReplication replication = GetSyncReplication(subscription);
			if (credentials != null)
				credentials.ApplyCredentials(replication);
			subscription.BeginSync(replication);
		}

		/// <summary>
		///		Call this method to cancel the synchronize from the client.
		/// </summary>
		/// <param name="subscription">
		///		The subscription that you want to cancel.
		/// </param>
		public void CancelSynchronize(Subscription subscription)
		{
			Guard.ArgumentNotNull(subscription, "subscription");

			subscription.CancelSync();
		}

		/// <summary>
		///		Call this method once a synchronization completes when you start the synchronization
		///		by calling <see cref="BeginSync"/>.
		/// </summary>
		/// <param name="subscription">
		///		The subscription that finished synchronizing.
		/// </param>
		/// <exception cref="SqlCeException">
		///		If there was an error during synchronization, this type of exception will
		///		be thrown.
		/// </exception>
		public void EndSynchronize(Subscription subscription)
		{
			Guard.ArgumentNotNull(subscription, "subscription");

			try
			{
				subscription.EndSync();
			}
			catch (SqlCeException)
			{
				ReloadSubscriptions();
				throw;
			}
		}

		/// <summary>
		///		Clears the <see cref="Subscriptions"/> list so it will be reloaded from the database
		///		the next time you access the list.
		/// </summary>
		/// <remarks>
		///		We added this method to aid writing unit tests.
		/// </remarks>
		public void ClearCache()
		{
			subscriptions = new SubscriptionCollection();
			reloadSubscriptions = true;
		}
	}
}
