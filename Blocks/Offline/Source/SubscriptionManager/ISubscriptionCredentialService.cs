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
	///		Provides an abstract interface that you need to implement in order for <see cref="SubscriptionManager"/>
	///		to find credentials.
	/// </summary>
	public interface ISubscriptionCredentialService
	{
		/// <summary>
		///		Finds a set of credentials for a specific subscription that will be used to connect
		///		to the publication on the remote server.
		/// </summary>
		/// <param name="subscription">
		///		The subscription for which you need credentials.
		/// </param>
		/// <returns></returns>
		SubscriptionCredentials FindCredentials(Subscription subscription);
	}
}
