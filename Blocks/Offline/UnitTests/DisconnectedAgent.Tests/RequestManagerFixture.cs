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
	public class RequestManagerFixture
	{
		[TestMethod]
		public void RequestManagerPassesConnectionPriceToQueue()
		{
			MockRequestQueue queue = new MockRequestQueue();
			MockRequestQueue deadletterQueue = new MockRequestQueue();
			MockEndpointCatalog endCatalog = CreateEndpointCatalog();
			MockConnectionMonitor connectionMonitor = CreateConnectionManager();
			connectionMonitor.MockChangeConnectionStatus(true);

			MockEndpoint ep = new MockEndpoint("A");
			ep.Default = new MockEndpointConfig("address");

			endCatalog.Endpoints.Add("A", ep);

			RequestManager mgr = RequestManager.Instance;
			mgr.Initialize<MockRequestDispatcher>(queue, deadletterQueue, connectionMonitor, endCatalog);

			Request request = new Request();
			request.Endpoint = "A";
			request.Behavior.Stamps = 9;
			queue.Enqueue(request);

			connectionMonitor.CurrentConnectionPrice = 5;
			mgr.DispatchAllPendingRequestsForConnection();
			mgr.Join(1000);

			Assert.AreEqual(5, queue.CriteriaReceivedPrice);
			Assert.AreEqual(0, queue.requests.Count);
		}


		[TestMethod]
		public void DispatchOneRequestForciblyOnlyWorksIfItsConnected()
		{
			MockRequestQueue deadletterQueue = new MockRequestQueue();
			MockEndpointCatalog endCatalog = CreateEndpointCatalog();
			MockConnectionMonitor connectionMonitor = CreateConnectionManager();
			MockRequestQueue requestQueue = new MockRequestQueue();

			RequestManager mgr = RequestManager.Instance;
			mgr.Initialize<MockRequestDispatcher>(requestQueue, deadletterQueue, connectionMonitor, endCatalog);
			MockRequestDispatcher disp = MockRequestDispatcher.Instance;

			Request r = new Request();
			r.Endpoint = "Endpoint";
			mgr.RequestQueue.Enqueue(r);

			connectionMonitor.MockChangeConnectionStatus(false);
			mgr.DispatchRequest(r); //
			mgr.DispatchRequest(r); // r should be dispatched once 
			mgr.DispatchRequest(r); //
			mgr.Join(1000);
			Assert.AreEqual(0, disp.RequestsReceived.Count);

			connectionMonitor.MockChangeConnectionStatus(true);
			mgr.DispatchRequest(r);
			mgr.Join(1000);

			Assert.AreEqual(1, disp.RequestsReceived.Count); // r should be dispatched once 
			Assert.AreSame(r, disp.RequestsReceived[0]);
		}

		[TestMethod]
		public void DispatchDoesNotDispatchRequestsOutOfTheQueue()
		{
			MockRequestDispatcher disp = new MockRequestDispatcher();
			MockRequestQueue deadletterQueue = new MockRequestQueue();
			MockEndpointCatalog endCatalog = CreateEndpointCatalog();
			MockConnectionMonitor connectionMonitor = CreateConnectionManager();
			MockRequestQueue requestQueue = new MockRequestQueue();

			RequestManager mgr = RequestManager.Instance;
			mgr.Initialize<MockRequestDispatcher>(requestQueue, deadletterQueue, connectionMonitor, endCatalog);

			Request r = new Request();
			r.Endpoint = "Endpoint";

			connectionMonitor.MockChangeConnectionStatus(true);
			mgr.DispatchRequest(r);
			mgr.Join(1000);
			Assert.AreEqual(0, disp.RequestsReceived.Count);
		}

		[TestMethod]
		public void DispatchOneRequestForciblyRemoveItFromTheQueue()
		{
			MockRequestQueue deadletterQueue = new MockRequestQueue();
			MockEndpointCatalog endCatalog = CreateEndpointCatalog();
			MockConnectionMonitor connectionMonitor = CreateConnectionManager();
			MockRequestQueue requestQueue = new MockRequestQueue();
			connectionMonitor.MockChangeConnectionStatus(true);
			RequestManager mgr = RequestManager.Instance;
			mgr.Initialize<MockRequestDispatcher>(requestQueue, deadletterQueue, connectionMonitor, endCatalog);
			MockRequestDispatcher disp = MockRequestDispatcher.Instance;

			Request r = new Request();
			r.Endpoint = "Endpoint";

			mgr.RequestQueue.Enqueue(r);
			Assert.AreEqual(1, mgr.RequestQueue.GetCount());
			mgr.DispatchRequest(r);
			mgr.Join(1000);
			Assert.AreEqual(0, mgr.RequestQueue.GetCount());

			Assert.AreSame(r, disp.RequestsReceived[0]);
			Assert.IsFalse(requestQueue.requests.Contains(r));
		}

		[TestMethod]
		public void DispatchAllRequestByTagBypassingCost()
		{
			MockRequestQueue queue = new MockRequestQueue();
			MockRequestQueue deadletterQueue = new MockRequestQueue();
			MockEndpointCatalog endCatalog = CreateEndpointCatalog();

			MockConnectionMonitor connectionMonitor = CreateConnectionManager();
			connectionMonitor.MockChangeConnectionStatus(true);

			RequestManager mgr = RequestManager.Instance;
			mgr.Initialize<MockRequestDispatcher>(queue, deadletterQueue, connectionMonitor, endCatalog);
			MockRequestDispatcher disp = MockRequestDispatcher.Instance;
			OfflineBehavior behavior = new OfflineBehavior();
			behavior.Tag = "A";

			Request r1 = new Request();
			r1.Endpoint = "Endpoint";
			Request r2 = new Request();
			r2.Endpoint = "Endpoint";
			Request r3 = new Request();
			r3.Endpoint = "Endpoint";

			r1.Behavior = behavior;
			r3.Behavior = behavior;

			queue.Enqueue(r1);
			queue.Enqueue(r2);
			queue.Enqueue(r3);

			mgr.DispatchPendingRequestsByTag("A");
			mgr.Join(1000);

			Assert.IsTrue(disp.RequestsReceived.Contains(r1));
			Assert.IsTrue(disp.RequestsReceived.Contains(r3));
			Assert.IsFalse(disp.RequestsReceived.Contains(r2));

			Assert.AreEqual(2, disp.RequestsReceived.Count);
			Assert.AreEqual(1, queue.GetCount());
		}


		[TestMethod]
		public void DispatchAllPendingRequestsBypassingCosts()
		{
			MockRequestQueue queue = new MockRequestQueue();
			MockRequestQueue deadletterQueue = new MockRequestQueue();
			MockEndpointCatalog endCatalog = CreateEndpointCatalog();
			MockConnectionMonitor connectionMonitor = CreateConnectionManager();
			connectionMonitor.MockChangeConnectionStatus(true);

			RequestManager mgr = RequestManager.Instance;
			mgr.Initialize<MockRequestDispatcher>(queue, deadletterQueue, connectionMonitor, endCatalog);
			MockRequestDispatcher disp = MockRequestDispatcher.Instance;

			Request r1 = new Request();
			r1.Endpoint = "Endpoint";
			Request r2 = new Request();
			r2.Endpoint = "Endpoint";
			Request r3 = new Request();
			r3.Endpoint = "Endpoint";

			queue.Enqueue(r1);
			queue.Enqueue(r2);
			queue.Enqueue(r3);

			mgr.DispatchAllPendingRequests();
			mgr.Join(1000);

			Assert.IsTrue(disp.RequestsReceived.Contains(r1));
			Assert.IsTrue(disp.RequestsReceived.Contains(r3));
			Assert.IsTrue(disp.RequestsReceived.Contains(r2));

			Assert.AreEqual(3, disp.RequestsReceived.Count);

			Assert.AreEqual(0, queue.GetCount());
		}

		[TestMethod]
		public void DispatchAllPendingRequestsDoesntDispatchRequestsForUnavailableEndpoints()
		{
			MockRequestQueue queue = new MockRequestQueue();
			MockRequestQueue deadletterQueue = new MockRequestQueue();
			MockEndpointCatalog endCatalog = CreateEndpointCatalog();
			MockConnectionMonitor connectionMonitor = CreateConnectionManager();
			connectionMonitor.MockChangeConnectionStatus(true);

			RequestManager mgr = RequestManager.Instance;
			mgr.Initialize<MockRequestDispatcher>(queue, deadletterQueue, connectionMonitor, endCatalog);
			MockRequestDispatcher disp = MockRequestDispatcher.Instance;

			Request r1 = new Request();
			r1.Endpoint = "Endpoint";
			Request r2 = new Request();
			r2.Endpoint = "Unavailable";
			Request r3 = new Request();
			r3.Endpoint = "Endpoint";

			queue.Enqueue(r1);
			queue.Enqueue(r2);
			queue.Enqueue(r3);

			mgr.DispatchAllPendingRequests();
			mgr.Join(1000);

			Assert.IsTrue(disp.RequestsReceived.Contains(r1));
			Assert.IsTrue(disp.RequestsReceived.Contains(r3));
			Assert.IsFalse(disp.RequestsReceived.Contains(r2));

			Assert.AreEqual(2, disp.RequestsReceived.Count);
			Assert.AreEqual(1, queue.GetCount());
		}

		[TestMethod]
		public void DispatchRequestsForConnectionOnMessageEnqueuedAndConnected()
		{
			MockRequestQueue queue = new MockRequestQueue();
			MockRequestQueue deadletterQueue = new MockRequestQueue();
			MockEndpointCatalog endCatalog = new MockEndpointCatalog();

			endCatalog.Endpoints.Add("E1", new MockEndpoint("E1"));
			endCatalog.Endpoints.Add("E2", new MockEndpoint("E2"));
			endCatalog.Endpoints.Add("E3", new MockEndpoint("E3"));
			endCatalog.Endpoints["E1"].Default = new MockEndpointConfig("DefaultURI1");
			endCatalog.Endpoints["E2"].Default = new MockEndpointConfig("DefaultURI2");

			MockConnectionMonitor cm = new MockConnectionMonitor();
			cm.CurrentNetwork = "net";
			cm.CurrentConnectionPrice = 5;
			cm.MockChangeConnectionStatus(true);

			RequestManager mgr = RequestManager.Instance;
			mgr.Initialize<MockRequestDispatcher>(queue, deadletterQueue, cm, endCatalog);
			MockRequestDispatcher disp = MockRequestDispatcher.Instance;

			mgr.StartAutomaticDispatch();

			//ok
			Request r1 = new Request();
			r1.Endpoint = "E1";
			r1.Behavior.Stamps = 9;

			//not enough stamps
			Request r2 = new Request();
			r2.Endpoint = "E2";
			r2.Behavior.Stamps = 1;

			//no endpoint
			Request r3 = new Request();
			r3.Endpoint = "E3";
			r3.Behavior.Stamps = 9;

			queue.Enqueue(r1);
			queue.Enqueue(r2);
			queue.Enqueue(r3);

			mgr.Join(1000);

			Assert.IsTrue(disp.RequestsReceived.Contains(r1));
			Assert.IsFalse(disp.RequestsReceived.Contains(r3));
			Assert.IsFalse(disp.RequestsReceived.Contains(r2));

			Assert.AreEqual(1, disp.RequestsReceived.Count);
			Assert.AreEqual(2, queue.GetCount());
		}

		[TestMethod]
		public void DispatchRequestsForConnectionStatusChangeAccordingToStamps()
		{
			MockRequestQueue queue = new MockRequestQueue();
			MockRequestQueue deadletterQueue = new MockRequestQueue();
			MockEndpointCatalog endCatalog = new MockEndpointCatalog();

			endCatalog.Endpoints.Add("E1", new MockEndpoint("E1"));
			endCatalog.Endpoints.Add("E2", new MockEndpoint("E2"));
			endCatalog.Endpoints.Add("E3", new MockEndpoint("E3"));
			endCatalog.Endpoints["E1"].Default = new MockEndpointConfig("DefaultURI1");
			endCatalog.Endpoints["E2"].Default = new MockEndpointConfig("DefaultURI2");

			MockConnectionMonitor cm = CreateConnectionManager();

			RequestManager mgr = RequestManager.Instance;
			mgr.Initialize<MockRequestDispatcher>(queue, deadletterQueue, cm, endCatalog);
			MockRequestDispatcher disp = MockRequestDispatcher.Instance;

			//ok
			Request r1 = new Request();
			r1.Endpoint = "E1";
			r1.Behavior.Stamps = 9;

			//not enough stamps
			Request r2 = new Request();
			r2.Endpoint = "E2";
			r2.Behavior.Stamps = 1;

			//no endpoint
			Request r3 = new Request();
			r3.Endpoint = "E3";
			r3.Behavior.Stamps = 9;

			queue.Enqueue(r1);
			queue.Enqueue(r2);
			queue.Enqueue(r3);

			mgr.StartAutomaticDispatch();

			Assert.AreEqual(0, disp.RequestsReceived.Count);

			cm.MockChangeConnectionStatus(true);

			mgr.Join(1000);

			Assert.IsTrue(disp.RequestsReceived.Contains(r1));
			Assert.IsFalse(disp.RequestsReceived.Contains(r3));
			Assert.IsFalse(disp.RequestsReceived.Contains(r2));

			Assert.AreEqual(1, disp.RequestsReceived.Count);
			Assert.AreEqual(2, queue.GetCount());
		}


		[TestMethod]
		public void DispatchRequestsForConnectionStatusChangeToConnected()
		{
			MockRequestQueue queue = new MockRequestQueue();
			MockRequestQueue deadletterQueue = new MockRequestQueue();
			MockEndpointCatalog endCatalog = new MockEndpointCatalog();

			endCatalog.Endpoints.Add("E1", new MockEndpoint("E1"));
			endCatalog.Endpoints.Add("E2", new MockEndpoint("E2"));
			endCatalog.Endpoints.Add("E3", new MockEndpoint("E3"));
			endCatalog.Endpoints["E1"].Default = new MockEndpointConfig("DefaultURI1");
			endCatalog.Endpoints["E2"].Default = new MockEndpointConfig("DefaultURI2");

			MockConnectionMonitor cm = CreateConnectionManager();

			RequestManager mgr = RequestManager.Instance;
			mgr.Initialize<MockRequestDispatcher>(queue, deadletterQueue, cm, endCatalog);
			MockRequestDispatcher disp = MockRequestDispatcher.Instance;

			//All these request have enough stamps to be dispatched
			Request r1 = new Request();
			r1.Endpoint = "E1";
			r1.Behavior.Stamps = 9;

			Request r2 = new Request();
			r2.Endpoint = "E2";
			r2.Behavior.Stamps = 9;

			//no endpoint
			Request r3 = new Request();
			r3.Endpoint = "E3";
			r3.Behavior.Stamps = 9;

			queue.Enqueue(r1);
			queue.Enqueue(r2);
			queue.Enqueue(r3);

			disp.SessionAllowDispatchs = 2; //Just allow the first two request dispatch
			cm.MockChangeConnectionStatus(false); //Is not connected
			mgr.StartAutomaticDispatch();
			mgr.Join(1000);
			Assert.AreEqual(0, disp.RequestsReceived.Count); //should hold on until connection
			cm.MockChangeConnectionStatus(true); //Should launch 1 request and stay locked in the second dispatch
			mgr.Join(1000);
			Assert.AreEqual(1, disp.RequestsReceived.Count);
			cm.MockChangeConnectionStatus(false);
			disp.SessionAllowDispatchs = null; //Unlock the locked dispatch.
			mgr.Join(1000);

			//Only the two first request should been dispatched
			Assert.IsTrue(disp.RequestsReceived.Contains(r1));
			Assert.IsTrue(disp.RequestsReceived.Contains(r2));
			Assert.IsFalse(disp.RequestsReceived.Contains(r3));

			Assert.AreEqual(2, disp.RequestsReceived.Count);
			Assert.AreEqual(1, queue.GetCount());
		}


		private static MockConnectionMonitor CreateConnectionManager()
		{
			MockConnectionMonitor cm = new MockConnectionMonitor();
			cm.CurrentNetwork = "net";
			cm.CurrentConnectionPrice = 5;
			return cm;
		}

		[TestMethod]
		public void EnqueDoesntDispatchIfManagerIsStopped()
		{
			MockRequestQueue queue = new MockRequestQueue();
			MockRequestQueue deadletterQueue = new MockRequestQueue();
			MockRequestDispatcher disp = new MockRequestDispatcher();
			MockEndpointCatalog endCatalog = new MockEndpointCatalog();
			endCatalog.Endpoints.Add("E1", new MockEndpoint("E1"));
			endCatalog.Endpoints["E1"].Default = new MockEndpointConfig("DefaultURI2");

			MockConnectionMonitor cm = new MockConnectionMonitor();
			cm.CurrentNetwork = "net";
			cm.CurrentConnectionPrice = 5;

			RequestManager mgr = RequestManager.Instance;
			mgr.Initialize<MockRequestDispatcher>(queue, deadletterQueue, cm, endCatalog);

			Request r1 = new Request();
			r1.Endpoint = "E1";
			r1.Behavior.Stamps = 9;

			mgr.StartAutomaticDispatch();
			mgr.StopAutomaticDispatch();

			queue.Enqueue(r1);
			mgr.Join(1000);
			Assert.AreEqual(0, disp.RequestsReceived.Count);
		}

		[TestMethod]
		public void StartDispatchPendingRequestIfDeviceIsConnected()
		{
			MockRequestQueue queue = new MockRequestQueue();
			MockRequestQueue deadletterQueue = new MockRequestQueue();
			MockEndpointCatalog endCatalog = new MockEndpointCatalog();
			endCatalog.Endpoints.Add("E1", new MockEndpoint("E1"));
			endCatalog.Endpoints["E1"].Default = new MockEndpointConfig("DefaultURI2");

			MockConnectionMonitor cm = new MockConnectionMonitor();
			cm.CurrentNetwork = "net";
			cm.CurrentConnectionPrice = 5;
			cm.MockChangeConnectionStatus(true);

			RequestManager mgr = RequestManager.Instance;
			mgr.Initialize<MockRequestDispatcher>(queue, deadletterQueue, cm, endCatalog);
			MockRequestDispatcher disp = MockRequestDispatcher.Instance;

			Request r1 = new Request();
			r1.Endpoint = "E1";
			r1.Behavior.Stamps = 9;

			mgr.StartAutomaticDispatch();
			mgr.StopAutomaticDispatch();

			queue.Enqueue(r1);
			Assert.AreEqual(0, disp.RequestsReceived.Count);

			mgr.StartAutomaticDispatch();
			mgr.Join(1000);
			Assert.IsTrue(disp.RequestsReceived.Contains(r1));
			Assert.AreEqual(1, disp.RequestsReceived.Count);
			Assert.AreEqual(0, queue.requests.Count);
		}

		[TestMethod]
		public void IfRequestSucceedsRemoveFromQueue()
		{
			MockRequestQueue queue = new MockRequestQueue();
			MockEndpointCatalog endCatalog = new MockEndpointCatalog();
			MockRequestQueue deadletterQueue = new MockRequestQueue();
			endCatalog.Endpoints.Add("E1", new MockEndpoint("E1"));
			endCatalog.Endpoints["E1"].Default = new MockEndpointConfig("DefaultURI2");

			MockConnectionMonitor cm = new MockConnectionMonitor();
			cm.CurrentNetwork = "net";
			cm.CurrentConnectionPrice = 5;
			cm.MockChangeConnectionStatus(true);

			RequestManager mgr = RequestManager.Instance;
			mgr.Initialize<MockRequestDispatcher>(queue, deadletterQueue, cm, endCatalog);
			MockRequestDispatcher disp = MockRequestDispatcher.Instance;

			Request r1 = new Request();
			r1.Endpoint = "E1";
			r1.Behavior.Stamps = 9;

			disp.ForceFails = false;
			mgr.StartAutomaticDispatch();
			queue.Enqueue(r1);
			mgr.Join(1000);

			Assert.AreEqual(1, disp.RequestsReceived.Count);
			Assert.IsTrue(disp.RequestsReceived.Contains(r1));
			Assert.AreEqual(0, queue.GetCount());
		}

		[TestMethod]
		public void IfMessageFailsRemoveFromQueueAndPlaceInDLQ()
		{
			MockRequestQueue queue = new MockRequestQueue();
			MockRequestQueue dlq = new MockRequestQueue();
			MockEndpointCatalog endCatalog = new MockEndpointCatalog();
			endCatalog.Endpoints.Add("E1", new MockEndpoint("E1"));
			endCatalog.Endpoints["E1"].Default = new MockEndpointConfig("DefaultURI2");

			MockConnectionMonitor cm = new MockConnectionMonitor();
			cm.CurrentNetwork = "net";
			cm.CurrentConnectionPrice = 5;
			cm.MockChangeConnectionStatus(true);

			RequestManager mgr = RequestManager.Instance;
			mgr.Initialize<MockRequestDispatcher>(queue, dlq, cm, endCatalog);
			MockRequestDispatcher disp = MockRequestDispatcher.Instance;

			Request r1 = new Request();
			r1.Endpoint = "E1";
			r1.Behavior.Stamps = 9;

			disp.ForceFails = true; //Dispatch fails
			mgr.StartAutomaticDispatch();
			queue.Enqueue(r1);
			mgr.Join(1000);

			Assert.AreEqual(0, disp.RequestsReceived.Count);
			Assert.IsFalse(disp.RequestsReceived.Contains(r1));
			Assert.AreEqual(0, queue.GetCount());
			Assert.AreEqual(1, dlq.GetCount());
			Assert.IsTrue(dlq.requests.Contains(r1));
		}

		[TestMethod]
		public void ExpiredMessagesDontGetDispatched()
		{
			MockRequestQueue queue = new MockRequestQueue();
			MockRequestQueue dlq = new MockRequestQueue();
			MockRequestDispatcher disp = new MockRequestDispatcher();
			MockEndpointCatalog endCatalog = new MockEndpointCatalog();
			endCatalog.Endpoints.Add("E1", new MockEndpoint("E1"));
			endCatalog.Endpoints["E1"].Default = new MockEndpointConfig("DefaultURI2");

			MockConnectionMonitor cm = new MockConnectionMonitor();
			cm.CurrentNetwork = "net";
			cm.CurrentConnectionPrice = 5;
			cm.MockChangeConnectionStatus(true);

			RequestManager mgr = RequestManager.Instance;
			mgr.Initialize<MockRequestDispatcher>(queue, dlq, cm, endCatalog);

			Request r1 = new Request();
			r1.Endpoint = "E1";
			r1.Behavior.Stamps = 9;
			r1.Behavior.Expiration = DateTime.Now - TimeSpan.FromHours(1); //Already expired

			mgr.StartAutomaticDispatch();
			queue.Enqueue(r1);
			mgr.Join(1000);

			Assert.AreEqual(0, disp.RequestsReceived.Count);
			Assert.IsFalse(disp.RequestsReceived.Contains(r1));
			Assert.AreEqual(0, queue.GetCount());
			Assert.AreEqual(0, dlq.GetCount());
			Assert.IsFalse(dlq.requests.Contains(r1));
		}

		///what happens if we call start twice? stop twice?
		[TestMethod]
		public void CallingStartTwiceIsSameAsOnce()
		{
			MockRequestQueue queue = new MockRequestQueue();
			MockEndpointCatalog endCatalog = new MockEndpointCatalog();
			MockRequestQueue deadletterQueue = new MockRequestQueue();
			endCatalog.Endpoints.Add("E1", new MockEndpoint("E1"));
			endCatalog.Endpoints["E1"].Default = new MockEndpointConfig("DefaultURI2");

			MockConnectionMonitor cm = new MockConnectionMonitor();
			cm.CurrentNetwork = "net";
			cm.CurrentConnectionPrice = 5;
			cm.MockChangeConnectionStatus(true);

			RequestManager mgr = RequestManager.Instance;
			mgr.Initialize<MockRequestDispatcher>(queue, deadletterQueue, cm, endCatalog);
			MockRequestDispatcher disp = MockRequestDispatcher.Instance;

			Request r1 = new Request();
			r1.Endpoint = "E1";
			r1.Behavior.Stamps = 9;

			mgr.StartAutomaticDispatch();
			mgr.StartAutomaticDispatch();
			mgr.StopAutomaticDispatch();

			queue.Enqueue(r1);
			Assert.AreEqual(0, disp.RequestsReceived.Count);

			mgr.StartAutomaticDispatch();
			mgr.Join(1000);
			Assert.IsTrue(disp.RequestsReceived.Contains(r1));
			Assert.AreEqual(1, disp.RequestsReceived.Count);
			Assert.AreEqual(0, queue.requests.Count);
		}

		[TestMethod]
		public void RequestManagerIsCreatedStopped()
		{
			MockRequestQueue queue = new MockRequestQueue();
			MockEndpointCatalog endCatalog = new MockEndpointCatalog();
			MockRequestQueue deadletterQueue = new MockRequestQueue();
			endCatalog.Endpoints.Add("E1", new MockEndpoint("E1"));
			endCatalog.Endpoints["E1"].Default = new MockEndpointConfig("DefaultURI2");

			MockConnectionMonitor cm = CreateConnectionManager();
			RequestManager mgr = RequestManager.Instance;
			mgr.Initialize<MockRequestDispatcher>(queue, deadletterQueue, cm, endCatalog);

			Assert.IsFalse(mgr.AutomaticDispatcherRunning);
		}

		[TestMethod]
		public void ManangerSendsSentEventAfterRequestSent()
		{
			MockEndpointCatalog catalog = CreateEndpointCatalog();
			MockConnectionMonitor connectionMonitor = new MockConnectionMonitor();
			IRequestQueue queue = new MockRequestQueue();
			connectionMonitor.MockChangeConnectionStatus(true);
			MockRequestQueue deadletterQueue = new MockRequestQueue();

			RequestManager mgr = RequestManager.Instance;
			mgr.Initialize<MockRequestDispatcher>(queue, deadletterQueue, connectionMonitor, catalog);

			dispatchedRequest = null;
			mgr.RequestDispatched += new EventHandler<RequestDispatchedEventArgs>(OnRequestDispatched);

			Request request = new Request();
			request.Endpoint = "Endpoint";
			request.Behavior.MessageId = Guid.NewGuid();
			request.Behavior.Stamps = 9;
			request.OnlineProxyType = typeof (MockService);
			request.MethodName = "DoWithNoParams";
			mgr.RequestQueue.Enqueue(request);
			mgr.DispatchAllPendingRequests();
			mgr.Join(1000);

			Assert.IsNotNull(dispatchedRequest);
			Assert.AreEqual(DispatchResult.Succeeded, dispatchResult);
		}

		[TestMethod]
		public void EnqueuedRequestsWithAutomaticDispatchStartedAreNotDispatchedIfThereIsNotConnection()
		{
			MockEndpointCatalog catalog = CreateEndpointCatalog();
			MockConnectionMonitor connectionMonitor = CreateConnectionManager();
			IRequestQueue queue = new MockRequestQueue();
			MockRequestQueue deadletterQueue = new MockRequestQueue();

			RequestManager mgr = RequestManager.Instance;
			mgr.Initialize<MockRequestDispatcher>(queue, deadletterQueue, connectionMonitor, catalog);

			connectionMonitor.MockChangeConnectionStatus(false);

			mgr.StartAutomaticDispatch();

			Request request = new Request();
			request.Endpoint = "Endpoint";
			request.Behavior.MessageId = Guid.NewGuid();
			request.Behavior.Stamps = 9;
			request.OnlineProxyType = typeof (MockService);
			request.MethodName = "DoWithNoParams";

			Assert.AreEqual(0, queue.GetCount());
			queue.Enqueue(request);
			mgr.Join(1000);
			mgr.StopAutomaticDispatch();
			Assert.AreEqual(1, queue.GetCount(), "The request should not be dispatched until connection is available.");
		}

		[TestMethod]
		public void DispatchingRequestWhenConnectionDownRetriesRequestWhenConnectionReturns()
		{
			MockEndpointCatalog catalog = CreateEndpointCatalog();
			MockConnectionMonitor connectionMonitor = CreateConnectionManager();
			IRequestQueue queue = new MockRequestQueue();
			IRequestQueue dlq = new MockRequestQueue();

			RequestManager mgr = RequestManager.Instance;
			mgr.Initialize<MockRequestDispatcher>(queue, dlq, connectionMonitor, catalog);
			MockRequestDispatcher dispatcher = MockRequestDispatcher.Instance;

			Request request = new Request();
			request.Endpoint = "Endpoint";
			request.Behavior.MessageId = Guid.NewGuid();
			request.Behavior.Stamps = 9;
			request.OnlineProxyType = typeof (MockService);
			request.MethodName = "DoWithNoParams";

			queue.Enqueue(request);
			dispatcher.ForceFails = true;

			connectionMonitor.MockChangeConnectionStatus(true);
			MockRequestDispatcher.Block = true;
			mgr.StartAutomaticDispatch();
			Thread.Sleep(30);

			connectionMonitor.MockChangeConnectionStatus(false);
			MockRequestDispatcher.Block = false;
			Thread.Sleep(30);

			Assert.AreEqual(0, dispatcher.RequestsReceived.Count); //request tried and failed
			Assert.AreEqual(0, mgr.DeadLetterQueue.GetCount()); //request not put in DLQ

			dispatcher.ForceFails = false;
			connectionMonitor.MockChangeConnectionStatus(true);
			Thread.Sleep(30);
			mgr.Join(1000);

			Assert.AreEqual(1, dispatcher.RequestsReceived.Count); //request succeeded
			Assert.AreSame(request, dispatcher.RequestsReceived[0]);
		}

		[TestMethod]
		public void DispatchRequestForCurrentConnectionShouldCheckForActiveConnection()
		{
			MockEndpointCatalog catalog = CreateEndpointCatalog();
			MockConnectionMonitor connectionMonitor = CreateConnectionManager();
			IRequestQueue queue = new MockRequestQueue();
			IRequestQueue dlq = new MockRequestQueue();

			RequestManager mgr = RequestManager.Instance;
			mgr.Initialize<MockRequestDispatcher>(queue, dlq, connectionMonitor, catalog);
			MockRequestDispatcher dispatcher = MockRequestDispatcher.Instance;

			Request request1 = new Request();
			request1.Endpoint = "Endpoint";
			request1.Behavior.MessageId = Guid.NewGuid();
			request1.Behavior.Stamps = 9;
			request1.OnlineProxyType = typeof (MockService);
			request1.MethodName = "DoWithNoParams";

			queue.Enqueue(request1);

			Request request2 = new Request();
			request2.Endpoint = "Endpoint";
			request2.Behavior.MessageId = Guid.NewGuid();
			request2.Behavior.Stamps = 9;
			request2.OnlineProxyType = typeof (MockService);
			request2.MethodName = "DoWithNoParams";

			queue.Enqueue(request2);

			connectionMonitor.MockChangeConnectionStatus(true);
			MockRequestDispatcher.Block = true;
			mgr.StartAutomaticDispatch();
			Thread.Sleep(30);

			connectionMonitor.MockChangeConnectionStatus(false);
			MockRequestDispatcher.Block = false;
			Thread.Sleep(30);

			mgr.DispatchAllPendingRequests();

			Thread.Sleep(30);
			mgr.Join(1000);

			Assert.AreEqual(1, dispatcher.RequestsReceived.Count); //request succeeded
		}

		[TestMethod]
		public void RequestManagerIsSingleton()
		{
			RequestManager requestManager1 = RequestManager.Instance;
			RequestManager requestManager2 = RequestManager.Instance;

			Assert.AreSame(requestManager1, requestManager2);
		}

		[TestMethod]
		public void RequestsWithMissingEndpointAreMovedToTheDeadLetterQueue()
		{
			MockRequestDispatcher disp = new MockRequestDispatcher();
			MockEndpointCatalog endCatalog = CreateEndpointCatalog();
			MockConnectionMonitor connectionMonitor = CreateConnectionManager();
			MockRequestQueue requestQueue = new MockRequestQueue();
			MockRequestQueue deadLetterQueue = new MockRequestQueue();

			RequestManager mgr = RequestManager.Instance;
			mgr.Initialize<MockRequestDispatcher>(requestQueue, deadLetterQueue, connectionMonitor, endCatalog);

			Request r = new Request();
			r.Endpoint = "MissingEndpoint";
			mgr.RequestQueue.Enqueue(r);

			connectionMonitor.MockChangeConnectionStatus(true);
			mgr.DispatchRequest(r);
			mgr.Join(1000);
			Assert.AreEqual(0, disp.RequestsReceived.Count);
			Assert.AreEqual(1, deadLetterQueue.GetCount());
			Assert.AreEqual(0, requestQueue.GetCount());
			Assert.AreSame(r, deadLetterQueue.requests[0]);
		}
		
		[TestMethod]
		public void ExpiredMessagesFiresRequestDispatchedAsExpired()
		{
			MockRequestDispatcher disp = new MockRequestDispatcher();
			MockEndpointCatalog endCatalog = CreateEndpointCatalog();
			MockConnectionMonitor connectionMonitor = CreateConnectionManager();
			MockRequestQueue requestQueue = new MockRequestQueue();
			MockRequestQueue deadLetterQueue = new MockRequestQueue();
			DispatchResult? result = null;
			connectionMonitor.CurrentConnectionPrice = 1;

			RequestManager mgr = RequestManager.Instance;
			mgr.Initialize<MockRequestDispatcher>(requestQueue, deadLetterQueue, connectionMonitor, endCatalog);
			mgr.RequestDispatched += delegate(object sender, RequestDispatchedEventArgs args) { result = args.Result; };

			Request request = new Request();
			request.Endpoint = "Endpoint";
			request.Behavior.MessageId = Guid.NewGuid();
			request.Behavior.Stamps = 9;
			request.OnlineProxyType = typeof (MockService);
			request.MethodName = "DoWithNoParams";
			request.Behavior.Expiration = DateTime.Now - TimeSpan.FromDays(1);
			mgr.RequestQueue.Enqueue(request);

			connectionMonitor.MockChangeConnectionStatus(true);

			mgr.DispatchRequest(request);
			//mgr.StartAutomaticDispatch();
			mgr.Join(1000);

			Assert.AreEqual(DispatchResult.Expired, result);
			Assert.AreEqual(0, disp.RequestsReceived.Count);
			Assert.AreEqual(0, deadLetterQueue.GetCount());
		}

		[TestMethod]
		public void RequestEndpointAsEmptyStringIsDispatchedWhenEndpointCatalogIsConfigured()
		{
			MockRequestQueue queue = new MockRequestQueue();
			MockEndpointCatalog endCatalog = CreateEndpointCatalog();
			MockEndpoint ep = new MockEndpoint("A");
			ep.Default = new MockEndpointConfig("address");

			endCatalog.Endpoints.Add("A", ep);

			MockRequestQueue deadletterQueue = new MockRequestQueue();

			MockConnectionMonitor cm = new MockConnectionMonitor();
			cm.CurrentNetwork = "net";
			cm.CurrentConnectionPrice = 5;
			cm.MockChangeConnectionStatus(true);

			RequestManager mgr = RequestManager.Instance;
			mgr.Initialize<MockRequestDispatcher>(queue, deadletterQueue, cm, endCatalog);
			MockRequestDispatcher disp = MockRequestDispatcher.Instance;

			Request r1 = new Request();
			r1.Endpoint = "";
			r1.Behavior.Stamps = 9;

			disp.ForceFails = false;
			queue.Enqueue(r1);
			mgr.StartAutomaticDispatch();
			mgr.Join(1000);

			Assert.AreEqual(1, disp.RequestsReceived.Count);
			Assert.IsTrue(disp.RequestsReceived.Contains(r1));
			Assert.AreEqual(0, queue.GetCount());

		}

		private static MockEndpointCatalog CreateEndpointCatalog()
		{
			MockEndpointCatalog endCatalog = new MockEndpointCatalog();
			endCatalog.Endpoints.Add("Endpoint", new MockEndpoint("Endpoint"));
			endCatalog.Endpoints["Endpoint"].Default = new MockEndpointConfig("DefaultURI");
			return endCatalog;
		}


		private Request dispatchedRequest;
		private DispatchResult dispatchResult;

		private void OnRequestDispatched(object sender, RequestDispatchedEventArgs e)
		{
			dispatchedRequest = e.Request;
			dispatchResult = e.Result;
		}
	}
}