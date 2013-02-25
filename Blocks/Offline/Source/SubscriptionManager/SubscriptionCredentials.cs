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

namespace Microsoft.Practices.SmartClient.Subscriptions
{
	/// <summary>
	///		Provides credential information that the subscription manager needs in order
	///		to synchronize a subscription with a remote SQL Server publication.
	/// </summary>
	/// <remarks>
	/// <para>
	///		In the simple case where the only authentication is database authentication on
	///		the publisher end, you can create an instance of this class an set the PublisherLogin
	///		and PublisherPassword and everything will just work.
	/// </para>
	/// <para>
	///		If you need to perform authentication in proxies or gateways, you'll need to create
	///		a subclass and override the <see cref="ApplyCredentials"/> method.
	/// </para>
	/// </remarks>
	public class SubscriptionCredentials
	{
		/// <summary>
		///		The SQL Server user name to use if you're using SQL authentication.
		/// </summary>
		public string PublisherLogin;

		/// <summary>
		///		The SQL Server password to use if the subscription will be connecting with SQL authentication.
		/// </summary>
		public string PublisherPassword;

		/// <summary>
		///		How SQL Server Mobile will attempt to authenticate with the remote server, which is either
		///		Windows Authentication, or Sql Server authentication (username and password).
		/// </summary>
		public SecurityType PublisherSecurityMode = SecurityType.DBAuthentication;

		/// <summary>
		///		Override this method if you need to apply more credentials than the basic set, or if you
		///		need to use NTAuthentication instead of SQL Server authentication.
		/// </summary>
		/// <remarks>
		///		This method is called by the <see cref="SubscriptionManager"/> class just before it starts as
		///		synchronization (either synchronous or asynchronous). The default implementaion sets the
		///		SQL server authentication to use <see cref="SecurityType.DBAuthentication"/> and it sets
		///		the PublisherLogin and PublisherPassword values using the values stored in this instance.
		/// </remarks>
		/// <param name="repl">
		///		The SqlCeReplication instance that the <see cref="SubscriptionManager"/> class has prepared and will use
		///		after this method call returns to perform a synchronization.
		///	</param>
		public virtual void ApplyCredentials(SqlCeReplication replication)
		{
			Guard.ArgumentNotNull(replication, "replication");

			replication.PublisherSecurityMode = PublisherSecurityMode;
			replication.PublisherLogin = PublisherLogin;
			replication.PublisherPassword = PublisherPassword;
		}
	}
}
