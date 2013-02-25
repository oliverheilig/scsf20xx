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
using Microsoft.Practices.CompositeUI.Services;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Tests.Mocks;
using System.ComponentModel;

namespace Microsoft.Practices.CompositeUI.Tests.EventBroker
{
	[TestClass]
	public class EventInspectorFixture
	{
		//static EventCatalogServiceMock catalog;
		static WorkItem workItem;

		[TestInitialize]
		public void SetUp()
		{
			workItem = new TestableRootWorkItem();
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void CannotRegisterNullObject()
		{
			EventInspector.Register(null, workItem);
		}

		[TestMethod]
		public void CanRegisterObject()
		{
			EventInspector.Register(new object(), workItem);

			Assert.AreEqual(0, workItem.EventTopics.Count);
		}

		[TestMethod]
		public void CanRegisterLocalEventPublisher()
		{
			EventInspector.Register(new Mocks.LocalEventPublisher(), workItem);

			Assert.AreEqual(1, workItem.EventTopics.Count);
			Assert.AreEqual("LocalEvent", workItem.EventTopics.Get("LocalEvent").Name);
			Assert.AreEqual(1, workItem.EventTopics.Get("LocalEvent").PublicationCount);
		}

		[TestMethod]
		public void CanRegisterGlobalEventPublisher()
		{
			EventTopic handle = new EventTopic();

			EventInspector.Register(new Mocks.GlobalEventPublisher(), workItem);

			Assert.AreEqual(1, workItem.EventTopics.Count);
			Assert.AreEqual("GlobalEvent", workItem.EventTopics.Get("GlobalEvent").Name);
			Assert.AreEqual(1, workItem.EventTopics.Get("GlobalEvent").PublicationCount);
		}

		[TestMethod]
		public void EmptyTopicsGetsUnregistered()
		{
			EventTopic topic = new EventTopic();
			workItem.EventTopics.Add(topic, "GlobalEvent");
			Mocks.GlobalEventPublisher publisher = new Mocks.GlobalEventPublisher();
			Mocks.GlobalEventHandler subscriber = new Mocks.GlobalEventHandler();

			EventInspector.Register(publisher, workItem);
			Assert.AreEqual(1, topic.PublicationCount);

			EventInspector.Register(subscriber, workItem);
			Assert.AreEqual(1, topic.SubscriptionCount);

			EventInspector.Unregister(publisher, workItem);
			Assert.AreEqual(0, topic.PublicationCount);
			Assert.AreEqual(1, topic.SubscriptionCount);

			EventInspector.Unregister(subscriber, workItem);
			Assert.AreEqual(0, topic.PublicationCount);
			Assert.AreEqual(0, topic.SubscriptionCount);
		}

		[TestMethod]
		public void CanRegisterObjectWithOverloadedMethodes()
		{
			EventInspector.Register(new subA(), workItem);

			Assert.AreEqual(1, workItem.EventTopics.Count);
		}

		public class subA
		{
			[EventSubscription("testA")]
			public void HandlerA(object sender, EventArgs e)
			{
			}

			public void HandlerA()
			{
			}
		}


		class MockInspectorTarget
		{
			[EventPublication("LocalEvent", PublicationScope.WorkItem)]
			public event EventHandler LocalEvent;

			[EventPublication("GlobalEvent", PublicationScope.Global)]
			public event EventHandler GlobalEvent;

			[EventSubscription("TestTopic")]
			public void TestTopicHandler(object sender, EventArgs e)
			{
			}

			private void ResolveCompilerWarnings()
			{
				LocalEvent(null, null);
				GlobalEvent(null, null);
			}

		}
	}
}
