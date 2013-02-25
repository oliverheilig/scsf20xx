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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Practices.SmartClient.Subscriptions.Tests
{
	[TestClass]
	public class SubscriptionCollectionFixture
	{
		[TestMethod]
		public void NewSubscriptionCollectHasNoElements()
		{
			MockSubscriptionCollection subscriptions = new MockSubscriptionCollection();
			MockSubscription subscription = new MockSubscription("test", "testPub");
			Assert.AreEqual(0, subscriptions.Count);
			Assert.AreEqual(-1, subscriptions.Find("test"));
			Assert.AreEqual(-1, subscriptions.Find(subscription));
			Assert.IsFalse(subscriptions.ContainsKey("test"));
			Assert.IsFalse(subscriptions.Contains(subscription));
		}

		[TestMethod]
		public void AddingSubscriptionsReturnsCorrectNumberOfElements()
		{
			MockSubscriptionCollection subscriptions = new MockSubscriptionCollection();
			MockSubscription subscription = new MockSubscription("test", "testPub");
			subscriptions.Add(subscription);

			subscription = new MockSubscription("another", "anotherPub");
			subscriptions.Add(subscription);

			Assert.AreEqual(2, subscriptions.Count);
		}

		[TestMethod]
		public void ContainsKeyPerformsCorrectly()
		{
			MockSubscriptionCollection subscriptions = new MockSubscriptionCollection();

			MockSubscription subscription = new MockSubscription("test", "testPub");
			subscriptions.Add(subscription);

			subscription = new MockSubscription("another", "anotherPub");
			subscriptions.Add(subscription);

			Assert.IsTrue(subscriptions.ContainsKey("test"));
			Assert.IsTrue(subscriptions.ContainsKey("another"));
			Assert.IsFalse(subscriptions.ContainsKey("not"));
		}

		[TestMethod]
		public void ContainsReturnsTrueOnlyForExactSameInstance()
		{
			MockSubscriptionCollection subscriptions = new MockSubscriptionCollection();

			MockSubscription subscription1 = new MockSubscription("test", "testPub");
			subscriptions.Add(subscription1);

			MockSubscription subscription2 = new MockSubscription("another", "anotherPub");
			subscriptions.Add(subscription2);

			MockSubscription subscription3 = new MockSubscription("test", "testPub");

			Assert.IsTrue(subscriptions.Contains(subscription1));
			Assert.IsFalse(subscriptions.Contains(subscription3));
		}

		[TestMethod]
		public void FindViaSubscriberReturnsCorrectResults()
		{
			MockSubscriptionCollection subscriptions = new MockSubscriptionCollection();

			MockSubscription subscription = new MockSubscription("test", "testPub");
			subscriptions.Add(subscription);

			subscription = new MockSubscription("another", "anotherPub");
			subscriptions.Add(subscription);

			Assert.AreEqual(0, subscriptions.Find("test"));
			Assert.AreEqual(1, subscriptions.Find("another"));
			Assert.AreEqual(-1, subscriptions.Find("not"));
		}

		[TestMethod]
		public void FindViaSubscriptionReturnsCorrectResults()
		{
			MockSubscriptionCollection subscriptions = new MockSubscriptionCollection();

			MockSubscription subscription = new MockSubscription("test", "testPub");
			subscriptions.Add(subscription);

			subscription = new MockSubscription("another", "anotherPub");
			subscriptions.Add(subscription);

			subscription = new MockSubscription("test", "testPub");
			Assert.AreEqual(0, subscriptions.Find(subscription));

			subscription = new MockSubscription("another", "anotherPub");
			Assert.AreEqual(1, subscriptions.Find(subscription));

			subscription = new MockSubscription("not", "notPub");
			Assert.AreEqual(-1, subscriptions.Find(subscription));
		}

		[TestMethod]
		public void IndexFindsSubscriptionUsingSubscriber()
		{
			MockSubscriptionCollection subscriptions = new MockSubscriptionCollection();

			MockSubscription subscription = new MockSubscription("test", "testPub");
			subscriptions.Add(subscription);

			subscription = new MockSubscription("another", "anotherPub");
			subscriptions.Add(subscription);

			Subscription test = subscriptions["another"];
			Assert.IsNotNull(test);
			Assert.AreEqual("anotherPub", test.Publication);
		}

		[TestMethod]
		[ExpectedException(typeof(KeyNotFoundException))]
		public void IndexerThrowsWhenNotMatch()
		{
			MockSubscriptionCollection subscriptions = new MockSubscriptionCollection();

			MockSubscription subscription = new MockSubscription("test", "testPub");
			subscriptions.Add(subscription);

			subscription = new MockSubscription("another", "anotherPub");
			subscriptions.Add(subscription);

			Subscription test = subscriptions["not"];
		}

		[TestMethod]
		public void IntegerIndexerGetsCorrectSubscription()
		{
			MockSubscriptionCollection subscriptions = new MockSubscriptionCollection();

			MockSubscription subscription = new MockSubscription("test", "testPub");
			subscriptions.Add(subscription);

			subscription = new MockSubscription("another", "anotherPub");
			subscriptions.Add(subscription);

			Subscription test = subscriptions[1];
			Assert.IsNotNull(test);
			Assert.AreEqual("anotherPub", test.Publication);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void IndexThrowsForPastEndOfList()
		{
			MockSubscriptionCollection subscriptions = new MockSubscriptionCollection();

			MockSubscription subscription = new MockSubscription("test", "testPub");
			subscriptions.Add(subscription);

			subscription = new MockSubscription("another", "anotherPub");
			subscriptions.Add(subscription);

			Subscription test = subscriptions[2];
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void IndexThrowsForNegativeIndex()
		{
			MockSubscriptionCollection subscriptions = new MockSubscriptionCollection();

			MockSubscription subscription = new MockSubscription("test", "testPub");
			subscriptions.Add(subscription);

			subscription = new MockSubscription("another", "anotherPub");
			subscriptions.Add(subscription);

			Subscription test = subscriptions[-1];
		}

		private class MockSubscription : Subscription
		{
			public MockSubscription(string subscriber, string publicaction)
			{
				Subscriber = subscriber;
				Publication = publicaction;
			}
		}

		private class MockSubscriptionCollection : SubscriptionCollection
		{
			public new void Add(Subscription subscription)
			{
				base.Add(subscription);
			}
		}
	}
}
