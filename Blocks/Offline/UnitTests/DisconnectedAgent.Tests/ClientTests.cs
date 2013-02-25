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
using Microsoft.Practices.SmartClient.DisconnectedAgent;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.SmartClient.ApplicationBlocks.DisconnectedAgent.Tests
{
	[TestClass]
	public class ClientTests
	{
		[TestMethod]
		public void CreateOnlineProxyAndInvokeResultsInRequestInQueue()
		{
			MockRequestQueue queue = new MockRequestQueue();

			OfflineMockService proxy = new OfflineMockService(queue);
			proxy.DoWithNoParams();

			Request r = queue.GetNextRequest();

			Assert.AreEqual("DoWithNoParams", r.MethodName);
			Assert.AreEqual("MockService", r.Endpoint);
			Assert.AreSame(typeof (MockService), r.OnlineProxyType);

			OfflineBehavior defaultBehavior = new OfflineBehavior();
			defaultBehavior.Expiration = null;
			defaultBehavior.MaxRetries = 1;
			defaultBehavior.ReturnCallback = new CommandCallback(typeof (OfflineMockServiceCallbacks), "DoWithNoParamsCallback");
			defaultBehavior.ExceptionCallback = new CommandCallback(typeof (OfflineMockServiceCallbacks), "OfflineCallFailed");
			defaultBehavior.Stamps = 3;
			defaultBehavior.Tag = "";

			Assert.AreEqual(defaultBehavior.Expiration, r.Behavior.Expiration);
			Assert.AreEqual(defaultBehavior.MaxRetries, r.Behavior.MaxRetries);
			Assert.AreEqual(defaultBehavior.ReturnCallback.TargetMethodName, r.Behavior.ReturnCallback.TargetMethodName);
			Assert.AreEqual(defaultBehavior.ReturnCallback.TargetType, r.Behavior.ReturnCallback.TargetType);
			Assert.AreEqual(defaultBehavior.Stamps, r.Behavior.Stamps);
			Assert.AreEqual(defaultBehavior.Tag, r.Behavior.Tag);
		}


		[TestMethod]
		public void CanSpecifyBehaviorsForTheRequest()
		{
			MockRequestQueue queue = new MockRequestQueue();

			OfflineMockService proxy = new OfflineMockService(queue);

			OfflineBehavior beh = new OfflineBehavior();
			beh.Tag = "tag";
			beh.Stamps = 3;
			beh.MaxRetries = 5;
			beh.ReturnCallback = new CommandCallback(typeof (OfflineMockServiceCallbacks), "SomeOtherMethod");
			beh.Expiration = DateTime.MinValue;

			proxy.DoWithNoParams(beh);

			Request r = queue.GetNextRequest();

			Assert.AreEqual(beh, r.Behavior);
			Assert.AreEqual(DateTime.MinValue, r.Behavior.Expiration);
			Assert.AreEqual(5, r.Behavior.MaxRetries);
			Assert.AreEqual("SomeOtherMethod", r.Behavior.ReturnCallback.TargetMethodName);
			Assert.AreEqual(typeof (OfflineMockServiceCallbacks), r.Behavior.ReturnCallback.TargetType);
			Assert.AreEqual(3, r.Behavior.Stamps);
			Assert.AreEqual("tag", r.Behavior.Tag);
		}

		[TestMethod]
		public void NewRequestObjectHasNonNullOfflineBehavior()
		{
			Request r = new Request();
			Assert.IsNotNull(r.Behavior);
		}

		[TestMethod]
		public void CreatingRequestWithReturnValueAndParams()
		{
			MockRequestQueue queue = new MockRequestQueue();

			OfflineMockService proxy = new OfflineMockService(queue);

			MockServiceDataContract obj = new MockServiceDataContract("Jose", 31);

			proxy.DoWithParamsAndReturn(1, obj);


			Request r = queue.GetNextRequest();

			Assert.AreSame(obj, r.CallParameters[1]);
			Assert.AreEqual("MockService", r.Endpoint);
			Assert.AreEqual("DoWithParamsAndReturn", r.MethodName);
			Assert.AreEqual(typeof (MockService), r.OnlineProxyType);
			Assert.AreEqual("OnDoWithParamsAndReturnReturn", r.Behavior.ReturnCallback.TargetMethodName);
			Assert.AreEqual(typeof (OfflineMockServiceCallbacks), r.Behavior.ReturnCallback.TargetType);

			Assert.AreEqual("OnMockServiceException", r.Behavior.ExceptionCallback.TargetMethodName);
			Assert.AreEqual(typeof (OfflineMockServiceCallbacks), r.Behavior.ExceptionCallback.TargetType);

			//these test the default behavior; which should have been set by the recipe when creating the OfflineSvcAgent
			Assert.AreEqual(null, r.Behavior.Expiration);
			Assert.AreEqual(1, r.Behavior.MaxRetries);
			Assert.AreEqual(3, r.Behavior.Stamps);
			Assert.AreEqual("", r.Behavior.Tag);
			Assert.IsNotNull(r.Behavior.MessageId);
		}


		[TestMethod]
		public void CreatingRequestWithReturnValueAndSpecificBehavior()
		{
			MockRequestQueue queue = new MockRequestQueue();
			OfflineMockService proxy = new OfflineMockService(queue);
			MockServiceDataContract obj = new MockServiceDataContract("Jose", 31);

			OfflineBehavior behavior = new OfflineBehavior();
			behavior.MaxRetries = 7;
			behavior.Stamps = 5;
			behavior.Tag = "Test";
			behavior.ReturnCallback =
				new CommandCallback(typeof (OfflineMockServiceCallbacks), "OnDoWithParamsAndReturnAlternativeReturn");
			behavior.ExceptionCallback = new CommandCallback(typeof (OfflineMockServiceCallbacks), "OnMockServiceOtherException");
			DateTime expDate = DateTime.Now + TimeSpan.FromHours(2);
			behavior.Expiration = expDate;
			Guid behId = Guid.NewGuid();
			behavior.MessageId = behId;

			proxy.DoWithParamsAndReturn(1, obj, behavior);

			Request r = queue.GetNextRequest();

			Assert.AreEqual(behId, r.Behavior.MessageId);
			Assert.AreSame(obj, r.CallParameters[1]);
			Assert.AreEqual("MockService", r.Endpoint);
			Assert.AreEqual("DoWithParamsAndReturn", r.MethodName);
			Assert.AreEqual(typeof (MockService), r.OnlineProxyType);

			Assert.AreEqual("OnDoWithParamsAndReturnAlternativeReturn", r.Behavior.ReturnCallback.TargetMethodName);
			Assert.AreEqual(typeof (OfflineMockServiceCallbacks), r.Behavior.ReturnCallback.TargetType);
			Assert.AreEqual("OnMockServiceOtherException", r.Behavior.ExceptionCallback.TargetMethodName);
			Assert.AreEqual(typeof (OfflineMockServiceCallbacks), r.Behavior.ExceptionCallback.TargetType);

			//these test the default behavior; which should have been set by the recipe when creating the OfflineSvcAgent
			Assert.AreEqual(expDate, r.Behavior.Expiration);
			Assert.AreEqual(7, r.Behavior.MaxRetries);
			Assert.AreEqual(5, r.Behavior.Stamps);
			Assert.AreEqual("Test", r.Behavior.Tag);
		}
	}
}