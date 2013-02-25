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

namespace Microsoft.Practices.SmartClient.DisconnectedAgent
{
	/// <summary>
	/// Offline Behavior describes how the Dosconnected Agent should behave when offline.
	/// </summary>
	public class OfflineBehavior
	{
		private int maxRetries;
		private CommandCallback returnCallback;
		private DateTime? expiration;
		private int stamps;
		private string tag;
		private CommandCallback exceptionCallback;
		private Guid messageId;
		private DateTime? queuedDate;
		private Type proxyFactoryType;

		/// <summary>
		/// Default constructor
		/// </summary>
		public OfflineBehavior()
		{
			maxRetries = 1;
		}

		/// <summary>
		/// Callback to be invoked if an Exception is thrown during the dispatching process.
		/// </summary>
		public CommandCallback ExceptionCallback
		{
			get { return exceptionCallback; }
			set { exceptionCallback = value; }
		}

		/// <summary>
		/// Maximum datetime to dispatch a request. If the request is expired, it will not be dispatched and
		/// a RequestDispatched event will be fired with the DispatchResult.Expired result in the argument.
		/// </summary>
		public DateTime? Expiration
		{
			get { return expiration; }
			set { expiration = value; }
		}

		/// <summary>
		/// Datetime when the request has been enqueued.
		/// </summary>
		public DateTime? QueuedDate
		{
			get { return queuedDate; }
			set { queuedDate = value; }
		}

		/// <summary>
		/// Maximum number of dispatching retries, before to move a failing dispatch request in the dead letter queue.
		/// </summary>
		public int MaxRetries
		{
			get { return maxRetries; }
			set
			{
				if (value < 0)
					throw new ArgumentOutOfRangeException("value");
				
				maxRetries = value;
			}
		}

		/// <summary>
		/// Callback to be invoked when a request is dispatched succesfully. 
		/// </summary>
		public CommandCallback ReturnCallback
		{
			get { return returnCallback; }
			set { returnCallback = value; }
		}

		/// <summary>
		/// Maximum connectivity price to be dispatched.
		/// The request will be dispatched automatically if the connectivity price
		/// is equal or lower than this value.
		/// </summary>
		public int Stamps
		{
			get { return stamps; }
			set
			{
				if (value < 0) 
					throw new ArgumentOutOfRangeException("value");
				
				stamps = value;
			}
		}

		/// <summary>
		/// String to provide a customizable clasification for the request.
		/// </summary>
		public string Tag
		{
			get { return tag; }
			set { tag = value; }
		}

		/// <summary>
		/// Message Id
		/// </summary>
		public Guid MessageId
		{
			get { return messageId; }
			set { messageId = value; }
		}

		/// <summary>
		/// Gets or sets the type of the factory to use to create the proxies
		/// </summary>
		public Type ProxyFactoryType
		{
			get { return proxyFactoryType; }
			set { proxyFactoryType = value; }
		}
	}
}