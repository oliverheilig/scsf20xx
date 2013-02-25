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
using Microsoft.Practices.SmartClient.ConnectionMonitor.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.SmartClient.ConnectionMonitor.Tests
{
	[TestClass]
	public class ConnectionManagerFixture
	{
		[TestMethod]
		public void AddConnectionFiresEvent()
		{
			ConnectionCollection connections = new ConnectionCollection();

			bool eventFired = false;
			Connection reported = null;
			connections.ConnectionAdded += delegate(object sender, ConnectionEventArgs args)
			                               	{
			                               		eventFired = true;
			                               		reported = args.Connection;
			                               	};

			Connection expected = new MockConnection("Test", 0);
			connections.Add(expected);

			Assert.IsTrue(eventFired);
			Assert.AreSame(expected, reported);
		}

		[TestMethod]
		public void RemoveConnectionFiresEvent()
		{
			MockConnection expected = new MockConnection("Test", 0);
			ConnectionCollection connections = new ConnectionCollection();
			connections.Add(expected);

			bool eventFired = false;
			Connection reported = null;
			connections.ConnectionRemoved += delegate(object sender, ConnectionEventArgs args)
			                                 	{
			                                 		eventFired = true;
			                                 		reported = args.Connection;
			                                 	};

			connections.Remove("Test");

			Assert.IsTrue(eventFired);
			Assert.AreSame(expected, reported);
		}

		[TestMethod]
		public void ConnectionStatusChangedEventIsFired()
		{
			ConnectionCollection manager = new ConnectionCollection();
			MockConnection connection = new MockConnection("Connection1", 10);
			manager.Add(connection);

			bool handlerCalled = false;
			Connection reportedConnection = null;
			manager.ConnectionStatusChanged += delegate(object sender, ConnectionEventArgs args)
			                                   	{
			                                   		handlerCalled = true;
			                                   		reportedConnection = args.Connection;
			                                   	};

			connection.Disconnect();

			Assert.IsTrue(handlerCalled);
			Assert.AreSame(connection, reportedConnection);
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentException))]
		public void CannotAddDuplicatedConnectionNames()
		{
			ConnectionCollection manager = new ConnectionCollection();
			manager.Add(new MockConnection("Connection", 10));
			manager.Add(new MockConnection("Connection", 11));
		}

		[TestMethod]
		public void ActiveConnectionsGetsConnectedConnections()
		{
			ConnectionCollection manager = new ConnectionCollection();
			MockConnection connection1 = new MockConnection("Connection1", 10);
			MockConnection connection2 = new MockConnection("Connection2", 10);
			MockConnection connection3 = new MockConnection("Connection3", 10);
			connection2.Disconnect();

			manager.Add(connection1);
			manager.Add(connection2);
			manager.Add(connection3);

			Assert.AreEqual(2, manager.ActiveConnections.Count);
			Assert.IsTrue(manager.ActiveConnections.Contains(connection1));
			Assert.IsTrue(manager.ActiveConnections.Contains(connection3));
		}

		[TestMethod]
		public void RemoveActiveConnectionDeletesFromActiveConnectionList()
		{
			ConnectionCollection manager = new ConnectionCollection();
			MockConnection connection1 = new MockConnection("Connection1", 10);
			MockConnection connection2 = new MockConnection("Connection2", 10);
			MockConnection connection3 = new MockConnection("Connection3", 10);
			manager.Add(connection1);
			manager.Add(connection2);
			manager.Add(connection3);

			manager.Remove(connection1);

			Assert.IsFalse(manager.ActiveConnections.Contains(connection1));
		}

		[TestMethod]
		public void AddUnactiveConnectionDoesNotAddToActiveConnectionList()
		{
			ConnectionCollection manager = new ConnectionCollection();
			MockConnection connection1 = new MockConnection("Connection1", 10);
			MockConnection connection2 = new MockConnection("Connection2", 10);
			MockConnection connection3 = new MockConnection("Connection3", 10);
			connection1.Disconnect();
			manager.Add(connection1);
			manager.Add(connection2);
			manager.Add(connection3);

			Assert.IsFalse(manager.ActiveConnections.Contains(connection1));
		}

        [TestMethod]
        public void ConnectionManagerDoesNotHoldDuplicatesWhenConnectedStateIsRaised()
        {
            ConnectionCollection manager = new ConnectionCollection();
            MockConnection connection1 = new MockConnection("Connection1", 10);
            manager.Add(connection1);
            connection1.Connect();

            Assert.AreEqual(1, manager.ActiveConnections.Count);
        }
	}
}