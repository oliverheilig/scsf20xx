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
//===============================================================================
// Microsoft patterns & practices
// CompositeUI Application Block
//===============================================================================
// Copyright � Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI.EventBroker;

namespace Microsoft.Practices.CompositeUI.Tests.EventBroker
{
	[TestClass]
	public class WorkItemEventTopicCollectionFixture
	{
		private static WorkItem root;
		
		[TestInitialize]
		public void Setup()
		{
			root = new TestableRootWorkItem();
		}


		[TestMethod]
		public void CollectionIsEmpty()
		{
			Assert.AreEqual(0, root.EventTopics.Count);
		}


		[TestMethod]
		public void CanAddEventTopic()
		{
			EventTopic topic = root.EventTopics.AddNew<EventTopic>("test");

			Assert.AreEqual(1, root.EventTopics.Count);
			Assert.AreSame(topic, root.EventTopics.Get("test"));
		}

		[TestMethod]
		public void TopicAddedToChildIsAccessibleInParent()
		{
			WorkItem child = root.WorkItems.AddNew<WorkItem>();

            EventTopic topic = child.EventTopics.AddNew<EventTopic>("test");

			Assert.AreEqual(1, root.EventTopics.Count);
			Assert.AreSame(topic, root.EventTopics.Get("test"));			
		}

	}
}
