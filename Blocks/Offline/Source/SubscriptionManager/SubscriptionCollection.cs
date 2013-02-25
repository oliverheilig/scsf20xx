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
	///		Manages the list of subscriptions that are in the <see cref="SubscriptionManager"/>.
	/// </summary>
	public class SubscriptionCollection
	{
		private List<Subscription> subscriptions = new List<Subscription>();

		/// <summary>
		///		Provides access to the elements of this collection via the index.
		/// </summary>
		/// <param name="index">Index of the item to retreive.</param>
		/// <returns>The element located at the index.</returns>
		public Subscription this[int index]
		{
			get { return subscriptions[index]; }
		}

		/// <summary>
		///		Returns the <see cref="Subscription"/> instance with the Subscriber string.
		/// </summary>
		/// <param name="key">The Subscriber key to look for.</param>
		/// <returns>An instance of <see cref="Subscription"/> that has the key provided.</returns>
		/// <exception cref="KeyNotFoundException">
		///		If there is no <see cref="Subscription"/> instance with a matching Subscriber value.
		/// </exception>
		public Subscription this[string key]
		{
			get 
			{
				int index = Find(key);
				if (index < 0)
					throw new KeyNotFoundException();
				else
					return subscriptions[index];
			}
		}

		/// <summary>
		///		Tests to see if an exact instance of Subscription is in this collection.
		/// </summary>
		/// <param name="subscription">
		///		The instance we're testing
		/// </param>
		/// <returns>true only if this specific instance is in this collection.</returns>
		public bool Contains(Subscription subscription)
		{
			Guard.ArgumentNotNull(subscription, "subscription");
			return (subscriptions.Contains(subscription));
		}

		/// <summary>
		///		Looks to see if there is a <see cref="Subscription"/> in this collection with the
		///		Subscriber name.
		/// </summary>
		/// <param name="key">The Subscriber name of the <see cref="Subscription"/> instance to look for.</param>
		/// <returns>true if there is a <see cref="Subscription"/> with this Subscriber. Otherwise, false.</returns>
		public bool ContainsKey(string key)
		{
			Guard.ArgumentNotNull(key, "key");

			return Find(key) >= 0;
		}

		/// <summary>
		///		Number of <see cref="Subscription"/> elements in this collection.
		/// </summary>
		public int Count
		{
			get { return subscriptions.Count; }
		}

		/// <summary>
		///		Locates a <see cref="Subscription"/> in this collection that has the same Subscriber
		///		name as <paramref name="key"/>.
		/// </summary>
		/// <param name="key">The Subscriber to look for in this collection.</param>
		/// <returns>
		///		Index of the subscription with the provided Subscriber name, or -1 if no match found.
		///	</returns>
		public int Find(string key)
		{
			Guard.ArgumentNotNull(key, "key");

			for (int i = 0; i < subscriptions.Count; i++)
			{
				if (subscriptions[i].Subscriber == key)
					return i;
			}
			return -1;
		}

		/// <summary>
		///		Checks to see if a "matching" subscription exists in this list. Matching,
		///		in this case, doesn't mean the same exact subscription. Rather, we're
		///		using the <see cref="Subscription"/> class' IComparable interface to determine
		///		what is a match.
		/// </summary>
		/// <param name="example">An "example" subscription object. This method looks for a close match.</param>
		/// <returns>The index of the match we found, or -1 if we didn't find a match.</returns>
		public int Find(Subscription example)
		{
			int i = 0;
			foreach (IComparable<Subscription> subscription in subscriptions)
			{
				if (subscription.CompareTo(example) == 0)
					return i;

				i++;
			}
			return -1;
		}

		protected internal void Add(Subscription subscription)
		{
			subscriptions.Add(subscription);
		}

		protected internal void Remove(Subscription subscription)
		{
			subscriptions.Remove(subscription);
		}
	}
}
