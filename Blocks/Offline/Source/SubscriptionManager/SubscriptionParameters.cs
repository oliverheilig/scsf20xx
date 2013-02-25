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

namespace Microsoft.Practices.SmartClient.Subscriptions
{
	/// <summary>
	///		This class provides a container for parameters that are required to create a
	///		new subscription.
	/// </summary>
	public class SubscriptionParameters
	{
		/// <summary>The name assigned on the server for the publication that has been setup for mobile devices.</summary>
		public string Publication;

		/// <summary>The name of the computer running the data store that contains the publication.</summary>
		public string Publisher;

		/// <summary>The name of the server-side database that contains the publication.</summary>
		public string PublisherDatabase;

		/// <summary>The URL of the server data store agent that provides access to the publication.</summary>
		public string InternetUrl;

		/// <summary>What to call the subscription that will be created in the local device database.</summary>
		public string Subscriber;

		/// <summary>A value sent to the server that allows filtering of the data.</summary>
		public string Filter;
	}
}
