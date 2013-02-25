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
using System.Threading;
using Microsoft.Practices.SmartClient.ApplicationBlocks.DisconnectedAgent.Tests.Mocks;
using Microsoft.Practices.SmartClient.DisconnectedAgent;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.SmartClient.ApplicationBlocks.DisconnectedAgent.Tests
{
	[TestClass]
	public class RequestManagerThreadFixture
	{
		[TestMethod]
		public void DispatchSpecificRequestUsesDifferentThread()
		{
			MockEndpointCatalog endCatalog = CreateEndpointCatalog();
			MockConnectionMonitor connectionMonitor = CreateConnectionMonitor();
			MockRequestQueue requestQueue = new MockRequestQueue();
			MockRequestQueue deadletterQueue = new MockRequestQueue();

			RequestManager mgr = RequestManager.Instance;
			mgr.Initialize<MockRequestDispatcher>(requestQueue, deadletterQueue, connectionMonitor, endCatalog);
			MockRequestDispatcher disp = MockRequestDispatcher.Instance;
			Request r = CreateRequest(9, string.Empty, "E1");
			mgr.RequestQueue.Enqueue(r);

			disp.ThreadId = 0;

			connectionMonitor.MockChangeConnectionStatus(true);
			mgr.DispatchRequest(r);

			mgr.Join(1000);

			int threadId = Thread.CurrentThread.ManagedThreadId;
			Assert.AreNotEqual(threadId, disp.ThreadId, "Call to dispatcher should have been on a different thread");
			Assert.AreNotEqual(0, disp.ThreadId);
			Assert.AreSame(r, disp.RequestsReceived[0]);
			Assert.AreEqual(1, disp.RequestsReceived.Count);
		}

		[TestMethod]
		public void DispatchAllRequestsUsesDifferentThread()
		{
			MockEndpointCatalog endCatalog = CreateEndpointCatalog();
			MockConnectionMonitor connectionMonitor = CreateConnectionMonitor();
			MockRequestQueue requestQueue = new MockRequestQueue();
			MockRequestQueue deadletterQueue = new MockRequestQueue();

			RequestManager mgr = RequestManager.Instance;
			mgr.Initialize<MockRequestDispatcher>(requestQueue, deadletterQueue, connectionMonitor, endCatalog);
			MockRequestDispatcher disp = MockRequestDispatcher.Instance;

			Request r = new Request();
			r.Endpoint = "E1";
			mgr.RequestQueue.Enqueue(r);

			disp.ThreadId = 0;
			requestQueue.ThreadId = 0;

			connectionMonitor.MockChangeConnectionStatus(true);
			mgr.DispatchAllPendingRequests();

			mgr.Join(1000);

			int threadId = Thread.CurrentThread.ManagedThreadId;
			Assert.AreNotEqual(threadId, requestQueue.ThreadId, "Call to queue should have been on a different thread");
			Assert.AreNotEqual(threadId, disp.ThreadId, "Call to dispatcher shoul dhave been on a different thread");
			Assert.AreEqual(disp.ThreadId, requestQueue.ThreadId);
			Assert.AreNotEqual(0, disp.ThreadId);
			Assert.AreSame(r, disp.RequestsReceived[0]);
			Assert.AreEqual(1, disp.RequestsReceived.Count);
		}

		[TestMethod]
		public void DispatchRequestsByTagUsesDifferentThread()
		{
			MockEndpointCatalog endCatalog = CreateEndpointCatalog();
			MockConnectionMonitor connectionMonitor = CreateConnectionMonitor();
			MockRequestQueue requestQueue = new MockRequestQueue();
			MockRequestQueue deadletterQueue = new MockRequestQueue();

			RequestManager mgr = RequestManager.Instance;
			mgr.Initialize<MockRequestDispatcher>(requestQueue, deadletterQueue, connectionMonitor, endCatalog);
			MockRequestDispatcher disp = MockRequestDispatcher.Instance;

			Request r = new Request();
			r.Behavior.Tag = "Tag";
			r.Endpoint = "E1";
			mgr.RequestQueue.Enqueue(r);

			disp.ThreadId = 0;
			requestQueue.ThreadId = 0;

			connectionMonitor.MockChangeConnectionStatus(true);
			mgr.DispatchPendingRequestsByTag("Tag");

			mgr.Join(1000);

			int threadId = Thread.CurrentThread.ManagedThreadId;
			Assert.AreNotEqual(threadId, requestQueue.ThreadId, "Call to queue should have been on a different thread");
			Assert.AreNotEqual(threadId, disp.ThreadId, "Call to dispatcher shoul dhave been on a different thread");
			Assert.AreEqual(disp.ThreadId, requestQueue.ThreadId);
			Assert.AreNotEqual(0, disp.ThreadId);
			Assert.AreSame(r, disp.RequestsReceived[0]);
			Assert.AreEqual(1, disp.RequestsReceived.Count);
		}

		[TestMethod]
		public void DispatchingMethodsUseOnlyOneDispatchAtTheSameTime()
		{
			MockEndpointCatalog endCatalog = CreateEndpointCatalog();
			MockConnectionMonitor connectionMonitor = CreateConnectionMonitor();
			MockRequestQueue requestQueue = new MockRequestQueue();
			MockRequestQueue Dlq = new MockRequestQueue();

			RequestManager mgr = RequestManager.Instance;
			mgr.Initialize<MockRequestDispatcher>(requestQueue, Dlq, connectionMonitor, endCatalog);
			MockRequestDispatcher disp = MockRequestDispatcher.Instance;

			EnqueueNewRequest(mgr.RequestQueue, "E1", "Tag1", 9);
			EnqueueNewRequest(mgr.RequestQueue, "E1", "Tag2", 9);
			EnqueueNewRequest(mgr.RequestQueue, "E1", "Tag1", 3);
			EnqueueNewRequest(mgr.RequestQueue, "E1", "Tag1", 3);
			EnqueueNewRequest(mgr.RequestQueue, "E1", "Tag2", 9);
			EnqueueNewRequest(mgr.RequestQueue, "E1", "Tag1", 9);

			MockRequestDispatcher.Block = false;
			MockRequestDispatcher.SlowMode = true;
			MockRequestDispatcher.UniqueDispatch = true;
			MockRequestDispatcher.DispatchRunning = 0;
			connectionMonitor.CurrentConnectionPrice = 3;
			mgr.StartAutomaticDispatch();
			mgr.DispatchPendingRequestsByTag("Tag1");
			connectionMonitor.MockChangeConnectionStatus(true);
			mgr.DispatchRequest(mgr.RequestQueue.GetNextRequest());
			connectionMonitor.CurrentConnectionPrice = 8;
			mgr.DispatchAllPendingRequestsForConnection();
			connectionMonitor.MockChangeConnectionStatus(false);
			mgr.DispatchAllPendingRequests();
			connectionMonitor.MockChangeConnectionStatus(true);
			mgr.Join(10000);
			mgr.StopAutomaticDispatch();
			MockRequestDispatcher.SlowMode = false;

			Assert.IsTrue(MockRequestDispatcher.UniqueDispatch);
			Assert.AreEqual(0, MockRequestDispatcher.DispatchRunning);
			Assert.AreEqual(6, disp.RequestsReceived.Count);
		}

		private void EnqueueNewRequest(IRequestQueue requestQueue, string endpoint, string tag, int stamps)
		{
			Request request = new Request();
			request.Endpoint = endpoint;
			request.Behavior.Tag = tag;
			request.Behavior.Stamps = stamps;
			requestQueue.Enqueue(request);
		}


		[TestMethod]
		public void DispatchRequestOnConnectionChangesRestartsActiveCommand()
		{
			MockEndpointCatalog endCatalog = CreateEndpointCatalog();
			MockConnectionMonitor connectionMonitor = CreateConnectionMonitor();
			MockRequestQueue requestQueue = new MockRequestQueue();
			MockRequestQueue deadletterQueue = new MockRequestQueue();

			RequestManager mgr = RequestManager.Instance;
			mgr.Initialize<MockRequestDispatcher>(requestQueue, deadletterQueue, connectionMonitor, endCatalog);
			MockRequestDispatcher disp = MockRequestDispatcher.Instance;

			Request r1 = CreateRequest(9, String.Empty, "E1");
			mgr.RequestQueue.Enqueue(r1);
			Request r2 = CreateRequest(3, String.Empty, "E1");
			mgr.RequestQueue.Enqueue(r2);
			Request r3 = CreateRequest(9, String.Empty, "E1");
			mgr.RequestQueue.Enqueue(r3);

			MockRequestDispatcher.Block = true;
			connectionMonitor.MockChangeConnectionStatus(false);

			mgr.StartAutomaticDispatch();

			connectionMonitor.CurrentNetwork = "net";
			connectionMonitor.CurrentConnectionPrice = 9;
			connectionMonitor.MockChangeConnectionStatus(true); //Should starts OnAutomaticDispatch of { r1, r3 }
			Thread.Sleep(30); //Starts r1 dispatching which is blocked

			//Change to a price 3 connection
			connectionMonitor.MockChangeConnectionStatus(false); //Disconnect
			MockRequestDispatcher.Block = false; //Unblock and finish the r1 dispatching

			connectionMonitor.CurrentConnectionPrice = 3;
			Thread.Sleep(30);
			connectionMonitor.MockChangeConnectionStatus(true); //Should restart active command {r2, r3}
			//Should starts OnAutomaticDispatch
			Thread.Sleep(30);

			MockRequestDispatcher.Block = false;

			mgr.StopAutomaticDispatch();

			mgr.Join(1000);

			Assert.AreEqual(3, disp.RequestsReceived.Count);
			Assert.AreSame(r1, disp.RequestsReceived[0]);
			Assert.AreEqual(r2, disp.RequestsReceived[1]);
			Assert.AreEqual(r3, disp.RequestsReceived[2]);
		}

		[TestMethod]
		public void DispatchStopsDispatchingWhenConnectionGoesAway()
		{
			MockEndpointCatalog endCatalog = CreateEndpointCatalog();
			MockConnectionMonitor connectionMonitor = CreateConnectionMonitor();
			MockRequestQueue requestQueue = new MockRequestQueue();
			MockRequestQueue deadletterQueue = new MockRequestQueue();

			RequestManager mgr = RequestManager.Instance;
			mgr.Initialize<MockRequestDispatcher>(requestQueue, deadletterQueue, connectionMonitor, endCatalog);
			MockRequestDispatcher disp = MockRequestDispatcher.Instance;

			Request r1 = new Request();
			r1.Behavior.Stamps = 9;
			r1.Endpoint = "E1";
			mgr.RequestQueue.Enqueue(r1);
			Request r2 = new Request();
			r2.Behavior.Stamps = 3;
			r2.Endpoint = "E1";
			mgr.RequestQueue.Enqueue(r2);
			Request r3 = new Request();
			r3.Behavior.Stamps = 9;
			r3.Endpoint = "E1";
			mgr.RequestQueue.Enqueue(r3);

			MockRequestDispatcher.Block = true;

			connectionMonitor.CurrentNetwork = "net";
			connectionMonitor.CurrentConnectionPrice = 3;
			connectionMonitor.MockChangeConnectionStatus(true);
			mgr.DispatchAllPendingRequests();
			Thread.Sleep(30);

			connectionMonitor.MockChangeConnectionStatus(false);
			Thread.Sleep(30);

			MockRequestDispatcher.Block = false;

			mgr.Join(1000);

			Assert.AreEqual(1, disp.RequestsReceived.Count);
			Assert.AreSame(r1, disp.RequestsReceived[0]);
		}

		[TestMethod]
		public void DispatchWithAutomaticDispatchStopsDispatchingWhenConnectionGoesAway()
		{
			MockEndpointCatalog endCatalog = CreateEndpointCatalog();
			MockConnectionMonitor connectionMonitor = CreateConnectionMonitor();
			MockRequestQueue requestQueue = new MockRequestQueue();
			MockRequestQueue deadletterQueue = new MockRequestQueue();

			RequestManager mgr = RequestManager.Instance;
			mgr.Initialize<MockRequestDispatcher>(requestQueue, deadletterQueue, connectionMonitor, endCatalog);
			MockRequestDispatcher disp = MockRequestDispatcher.Instance;

			Request r1 = new Request();
			r1.Behavior.Stamps = 9;
			r1.Endpoint = "E1";
			mgr.RequestQueue.Enqueue(r1);
			Request r2 = new Request();
			r2.Behavior.Stamps = 3;
			r2.Endpoint = "E1";
			mgr.RequestQueue.Enqueue(r2);
			Request r3 = new Request();
			r3.Behavior.Stamps = 9;
			r3.Endpoint = "E1";
			mgr.RequestQueue.Enqueue(r3);

			MockRequestDispatcher.Block = true;

			connectionMonitor.CurrentNetwork = "net";
			connectionMonitor.CurrentConnectionPrice = 3;
			connectionMonitor.MockChangeConnectionStatus(true);
			mgr.StartAutomaticDispatch();
			Thread.Sleep(30);

			connectionMonitor.MockChangeConnectionStatus(false);
			Thread.Sleep(30);

			MockRequestDispatcher.Block = false;

			mgr.Join(1000);
			mgr.StartAutomaticDispatch();

			Assert.AreEqual(1, disp.RequestsReceived.Count);
			Assert.AreSame(r1, disp.RequestsReceived[0]);
		}

		[TestMethod]
		public void DispatchingCanBeStoppedExplicitly()
		{
			MockEndpointCatalog endCatalog = CreateEndpointCatalog();
			MockConnectionMonitor connectionMonitor = CreateConnectionMonitor();
			MockRequestQueue requestQueue = new MockRequestQueue();
			MockRequestQueue deadletterQueue = new MockRequestQueue();

			RequestManager mgr = RequestManager.Instance;
			mgr.Initialize<MockRequestDispatcher>(requestQueue, deadletterQueue, connectionMonitor, endCatalog);
			MockRequestDispatcher disp = MockRequestDispatcher.Instance;

			Request r1 = new Request();
			r1.Behavior.Stamps = 9;
			r1.Endpoint = "E1";
			mgr.RequestQueue.Enqueue(r1);
			Request r2 = new Request();
			r2.Behavior.Stamps = 3;
			r2.Endpoint = "E1";
			mgr.RequestQueue.Enqueue(r2);
			Request r3 = new Request();
			r3.Behavior.Stamps = 9;
			r3.Endpoint = "E1";
			mgr.RequestQueue.Enqueue(r3);

			MockRequestDispatcher.Block = true;

			connectionMonitor.CurrentNetwork = "net";
			connectionMonitor.CurrentConnectionPrice = 3;
			connectionMonitor.MockChangeConnectionStatus(true);
			mgr.DispatchAllPendingRequests();
			Thread.Sleep(30);

			mgr.StopAutomaticDispatching();
			Thread.Sleep(30);

			MockRequestDispatcher.Block = false;

			mgr.Join(1000);

			Assert.AreEqual(1, disp.RequestsReceived.Count);
			Assert.AreSame(r1, disp.RequestsReceived[0]);
		}

		[TestMethod]
		public void DispatchRequestInSameOrderAsCommandsWereEntered()
		{
			MockEndpointCatalog endCatalog = CreateEndpointCatalog();
			MockConnectionMonitor connectionMonitor = CreateConnectionMonitor();
			MockRequestQueue requestQueue = new MockRequestQueue();
			MockRequestQueue deadletterQueue = new MockRequestQueue();

			RequestManager mgr = RequestManager.Instance;
			mgr.Initialize<MockRequestDispatcher>(requestQueue, deadletterQueue, connectionMonitor, endCatalog);
			MockRequestDispatcher disp = MockRequestDispatcher.Instance;

			Request r1 = CreateRequest(9, "Tag", "E1");
			mgr.RequestQueue.Enqueue(r1);
			Request r2 = CreateRequest(3, String.Empty, "E1");
			mgr.RequestQueue.Enqueue(r2);
			Request r3 = CreateRequest(9, "Tag", "E1");
			mgr.RequestQueue.Enqueue(r3);

			connectionMonitor.MockChangeConnectionStatus(false);

			mgr.StartAutomaticDispatch();

			mgr.DispatchPendingRequestsByTag("Tag"); //Should dispatch r1 and r3
			Thread.Sleep(30);
			connectionMonitor.CurrentNetwork = "net";
			connectionMonitor.CurrentConnectionPrice = 3;
			connectionMonitor.MockChangeConnectionStatus(true); //Should starts OnAutomaticDispatch to dispatch r2
			Thread.Sleep(30);
			mgr.Join(1000);

			Assert.AreEqual(3, disp.RequestsReceived.Count);
			Assert.AreEqual(r1, disp.RequestsReceived[0]);
			Assert.AreEqual(r3, disp.RequestsReceived[1]);
			Assert.AreEqual(r2, disp.RequestsReceived[2]);
		}

		[TestMethod]
		public void DispatchRequestDoesNotDispatchPreviouslyDispatchedRequests()
		{
			MockEndpointCatalog endCatalog = CreateEndpointCatalog();
			MockConnectionMonitor connectionMonitor = CreateConnectionMonitor();
			MockRequestQueue requestQueue = new MockRequestQueue();
			MockRequestQueue deadletterQueue = new MockRequestQueue();

			RequestManager mgr = RequestManager.Instance;
			mgr.Initialize<MockRequestDispatcher>(requestQueue, deadletterQueue, connectionMonitor, endCatalog);
			MockRequestDispatcher disp = MockRequestDispatcher.Instance;

			Request r1 = CreateRequest(9, "Tag", "E1");
			mgr.RequestQueue.Enqueue(r1);
			Request r2 = CreateRequest(9, "Dispatched", "E1");
			mgr.RequestQueue.Enqueue(r2);
			Request r3 = CreateRequest(9, "Tag", "E1");
			mgr.RequestQueue.Enqueue(r3);

			connectionMonitor.MockChangeConnectionStatus(false);

			mgr.DispatchAllPendingRequests(); //When connection comes up it will try to dispatch r1, r2 and r3
			Thread.Sleep(30);
			connectionMonitor.CurrentNetwork = "net";
			connectionMonitor.MockChangeConnectionStatus(true);
			mgr.DispatchRequest(r2); //Explicitly dispatch for r2 ('cause there is an active connection should start
			//the dispatching thread and process all pending dispatch commands).
			Thread.Sleep(30);
			mgr.Join(1000);

			Assert.AreEqual(3, disp.RequestsReceived.Count);
			Assert.AreEqual(r1, disp.RequestsReceived[0]);
			Assert.AreEqual(r2, disp.RequestsReceived[1]);
			Assert.AreEqual(r3, disp.RequestsReceived[2]);
		}

		private Request CreateRequest(int stamps, string tag, string endpoint)
		{
			Request request = new Request();
			request.Behavior.Stamps = stamps;
			request.Behavior.Tag = tag;
			request.Endpoint = endpoint;
			return request;
		}

		private MockConnectionMonitor CreateConnectionMonitor()
		{
			MockConnectionMonitor cm = new MockConnectionMonitor();
			cm.CurrentNetwork = "net";
			cm.CurrentConnectionPrice = 5;
			return cm;
		}

		private MockEndpointCatalog CreateEndpointCatalog()
		{
			MockEndpointCatalog endCatalog = new MockEndpointCatalog();
			endCatalog.Endpoints.Add("E1", new MockEndpoint("E1"));
			endCatalog.Endpoints["E1"].Default = new MockEndpointConfig("DefaultURI1");
			return endCatalog;
		}
	}
}