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
using System.Data.SqlServerCe;
using System.Data;

namespace Microsoft.Practices.SmartClient.Subscriptions
{
	/// <summary>
	///		This class contains information about a subscription that is currently
	///		in the database.
	/// </summary>
	/// <remarks>
	///		This class contains a number of properties that mirror properties from
	///		the SqlCeReplication class.
	/// <para>
	///		 See teh SQL Server 2005 Mobile Edition Books Online for more information.
	/// </para>
	/// </remarks>
	public class Subscription : IComparable<Subscription>
	{
		private SqlCeReplication replication;
		private IAsyncResult asyncResult;
		private DateTime? lastSyncTime;

		private string internetUrl;
		private short compressionLevel;
		private int connectionRetryTimeout;
		private string distributor;
		private string distributorAddress;
		private NetworkType distributorNetwork;
		private ExchangeType exchangeType;
		private string filter;
		private string internetProxyServer;
		private short loginTimeout;
		private string profileName;
		private string publisher;
		private string publisherAddress;
		private string publisherDatabase;
		private NetworkType publisherNetwork;
		private string publication;
		private short queryTimeout;
		private SnapshotTransferType snapshotTransferType;
		private string subscriber;
		private ValidateType validate;
		private int subscriptionInfoId;

		/// <summary>
		///		This method is marked as Internal so we can mock a subscription object.
		/// </summary>
		protected internal Subscription()
		{
		}

		/// <summary>
		///		Fired when SQL Mobile finishes synchronizing this subscription asynchronously.
		/// </summary>
		public event EventHandler SyncCompleted;

		/// <summary>
		///		Fired periodically during synchronization of this subscription. It passes the
		///		percentage complete as an int. Only for asynchronous.
		/// </summary>
		public event EventHandler<ProgressEventArgs> SyncProgress;

		/// <summary>
		///		Fired when SQL Mobile begins sending local changes in a table back to the server.
		///		Passes the name of the table. Only for asynchronous.
		/// </summary>
		public event EventHandler<TableEventArgs> TableUploadStarted;

		/// <summary>
		///		Fired when SQL Mobile begins to download data for a table on the server. It
		///		passes the name of the table. Only for asynchronous.
		/// </summary>
		public event EventHandler<TableEventArgs> TableDownloadStarted;

		/// <summary>
		///		The URL that SQL Mobile will use to connect to the SQL Server Mobile Server Agent
		///		that provides access to the publication on the server. Only for asynchronous.
		/// </summary>
		public string InternetUrl
		{
			get { return internetUrl; }
			set { internetUrl = value; }
		}

		/// <summary>
		///		Provides a way for you to check the status of an asynchronous synchronization started
		///		by a call to <see cref="SubscriptionManager.BeginSync"/>.
		/// </summary>
		public IAsyncResult AsyncResult
		{
			get { return asyncResult; }
		}

		internal void BeginSync(SqlCeReplication replication)
		{
			this.replication = replication;
			asyncResult = replication.BeginSynchronize(OnSyncCompletion, OnStartTableUpload, OnStartTableDownload, OnSynchronization, null);
		}

		internal void CancelSync()
		{
			replication.CancelSynchronize();
			replication.Dispose();
			replication = null;
		}

		internal void EndSync()
		{
			try
			{
				replication.EndSynchronize(asyncResult);
			}
			finally
			{
				asyncResult = null;
				replication.Dispose();
				replication = null;
			}
		}

		private void OnSyncCompletion(IAsyncResult ar)
		{
			if (SyncCompleted != null)
				SyncCompleted(this, EventArgs.Empty);
		}

		private void OnStartTableUpload(IAsyncResult ar, string tableName)
		{
			if (TableUploadStarted != null)
				TableUploadStarted(this, new TableEventArgs(tableName));
		}

		private void OnStartTableDownload(IAsyncResult ar, string tableName)
		{
			if (TableDownloadStarted != null)
				TableDownloadStarted(this, new TableEventArgs(tableName));
		}

		private void OnSynchronization(IAsyncResult ar, int percentComplete)
		{
			if (SyncProgress != null)
				SyncProgress(this, new ProgressEventArgs(percentComplete));
		}

		/// <summary>
		///		Gets or sets the compression level used when compressing data between the
		///		server and this device. Valid values are 0 to 6, with 0 being no compression.
		///		The default value is 0.
		/// </summary>
		public short CompressionLevel
		{
			get { return compressionLevel; }
			internal set { compressionLevel = value; }
		}

		/// <summary>
		///		Gets or sets the number of seconds that SQL Server Mobile will keep trying to process
		///		a request before it fails. Valid values are 0 to 900 seconds. The default is 120 seconds.
		/// </summary>
		public int ConnectionRetryTimeout
		{
			get { return connectionRetryTimeout; }
			internal set { connectionRetryTimeout = value; }
		}

		/// <summary>
		///		Gets or sets the name of the Distributor that provides access to the remote SQL Server
		///		database.
		/// </summary>
		public string Distributor
		{
			get { return distributor; }
			internal set { distributor = value; }
		}

		/// <summary>
		///		Gets or sets the network address for the distributor. See teh SQL Server 2005 Mobile Edition
		///		Books Online for more information.
		/// </summary>
		public string DistributorAddress
		{
			get { return distributorAddress; }
			internal set { distributorAddress = value; }
		}

		/// <summary>
		///		The network to use between the SQL Server Reconciler and the Distributor.
		/// </summary>
		public NetworkType DistributorNetwork
		{
			get { return (NetworkType) distributorNetwork; }
			internal set { distributorNetwork = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public ExchangeType ExchangeType
		{
			get { return (ExchangeType) exchangeType; }
			internal set { exchangeType = value; }
		}

		/// <summary>
		///		Provides a single parameter you can send to the server to filter the data returned
		///		by the publication. This is a read-only value. To change the filter, you have to
		///		drop and re-create the subscription.
		/// </summary>
		/// <remarks>
		///		For SQL Server Mobile replication, this is placed into the HostName property of the
		///		replication object.
		/// </remarks>
		public string Filter
		{
			get { return filter; }
			internal set { filter = value; }
		}

		/// <summary>
		///		The proxy server that will be used.
		/// </summary>
		public string InternetProxyServer
		{
			get { return internetProxyServer; }
			internal set { internetProxyServer = value; }
		}

		/// <summary>
		///		Returns the date/time when this subscription was last synchronized, or null if it hasn't
		///		been synchronized yet.
		/// </summary>
		public DateTime? LastSyncTime
		{
			get { return lastSyncTime; }
			internal set { lastSyncTime = value; }
		}

		/// <summary>
		///		The timeout for the login attempt, measured in seconds.
		/// </summary>
		public short LoginTimeout
		{
			get { return loginTimeout; }
			internal set { loginTimeout = value; }
		}

		/// <summary>
		///		Gets the name of the profile that will be used.
		/// </summary>
		public string ProfileName
		{
			get { return profileName; }
			internal set { profileName = value; }
		}

		/// <summary>
		///		Gets the name of the publisher.
		/// </summary>
		public string Publisher
		{
			get { return publisher; }
			internal set { publisher = value; }
		}

		/// <summary>
		///		Gets the address of the publisher
		/// </summary>
		public string PublisherAddress
		{
			get { return publisherAddress; }
			internal set { publisherAddress = value; }
		}

		/// <summary>
		///		Gets the name of the database on the publisher that is being used.
		/// </summary>
		public string PublisherDatabase
		{
			get { return publisherDatabase; }
			internal set { publisherDatabase = value; }
		}

		/// <summary>
		///		Gets the network protocol used between the SQL Server Replication provider and the SQL Server database.
		/// </summary>
		public NetworkType PublisherNetwork
		{
			get { return (NetworkType) publisherNetwork; }
			internal set { publisherNetwork = value; }
		}

		/// <summary>
		///		Gets the name of the publication to which this subscription is attached.
		/// </summary>
		public string Publication
		{
			get { return publication; }
			protected internal set { publication = value; }
		}

		/// <summary>
		///		Reports whether or not this subscription has been synchornized yet. When you add
		///		a subscription, the information is saved in the database without attempting to
		///		synchronize with the server. This property allows you to find out if a synchornization
		///		has been performed on this subsciption.
		/// </summary>
		/// <value>
		///		True if this subscription has been synchronized with the server at least once. False if it 
		///		has never been synchronized with the server.
		/// </value>
		public bool HasBeenSynchronized
		{
			get { return lastSyncTime != null; }
		}

		/// <summary>
		///		Gets the timeout, in seconds, for each query to run before it fails. The default is 300 seconds.
		/// </summary>
		public short QueryTimeout
		{
			get { return queryTimeout; }
			internal set { queryTimeout = value; }
		}

		/// <summary>
		///		Gets the type of transfer that will be used to transfer the snapshot file from the distributor
		///		to the gateway running IIS.
		/// </summary>
		public SnapshotTransferType SnapshotTransferType
		{
			get { return (SnapshotTransferType) snapshotTransferType; }
			internal set { snapshotTransferType = value; }
		}

		/// <summary>
		///		Gets the name of this subscriber.
		/// </summary>
		public string Subscriber
		{
			get { return subscriber; }
			protected internal set { subscriber = value; }
		}

		/// <summary>
		///		Gets the row ID for the local storage of this subscription.
		/// </summary>
		public int SubscriptionInfoId
		{
			get { return subscriptionInfoId; }
			internal set { subscriptionInfoId = value; }
		}

		/// <summary>
		///		Gets the validation setting that will be used during synchronization.
		/// </summary>
		public ValidateType Validate
		{
			get { return (ValidateType) validate; }
			internal set { validate = value; }
		}

		#region IComparable<Subscription> Members

		/// <summary>
		///		Compares another <see cref="Subscription"/> with this one to see if they refer to the
		///		same publication. This method uses the <see cref="Publication"/>, <see cref="Publisher"/>,
		///		and <see cref="PublisherDatabase"/> properties for comparison.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public int CompareTo(Subscription other)
		{
			Guard.ArgumentNotNull(other, "other");

			int result = String.Compare(Publication, other.Publication);
			if (result != 0)
				return result;
			result = String.Compare(this.Publisher, other.Publisher);
			if (result != 0)
				return result;
			else
				return String.Compare(this.PublisherDatabase, other.PublisherDatabase);
		}

		#endregion
	}
}
