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
using Microsoft.Practices.CompositeUI.Commands;

namespace Microsoft.Practices.CompositeUI.Tests.Commands
{
	[TestClass]
	public class EventTopicCommandFixture
	{
		private static WorkItem workItem;
		private static MockTopic topic;

		[TestInitialize]
		public void Setup()
		{
			workItem = new TestableRootWorkItem();
			topic = new MockTopic();
			workItem.EventTopics.Add(topic, topic.Name);
		}


		[TestMethod]
		public void CommandExecutionFiresEventTopic()
		{
            topic = workItem.EventTopics.AddNew<MockTopic>("topic://EventTopicCommand/Test");
            EventTopicCommand cmd = workItem.Commands.AddNew<EventTopicCommand>("Test");
            cmd.Execute();

            Assert.IsTrue(topic.FireCalled);
		}


		class MockTopic : EventTopic
		{
			public bool FireCalled = false;

            public MockTopic() : base()
            {
            }

			public override void Fire(object sender, EventArgs e, WorkItem workItem, PublicationScope scope)
			{
				FireCalled = true;
			}
		}
	}
}
