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
using Microsoft.Practices.SmartClient.ConnectionMonitor.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.SmartClient.ConnectionMonitor.Tests.Implementations
{
	/// <summary>
	/// Summary description for DesktopConnectionFixture
	/// </summary>
	[TestClass]
	public class DesktopConnectionFixture
	{
		[TestMethod]
		public void DesktopConnectionIsConnection()
		{
			DesktopConnection connection = new DesktopConnection("Desktop", 0);

			Assert.IsTrue(connection is Connection);
		}

		[TestMethod]
		public void TestInitialization()
		{
			DesktopConnection connection = new DesktopConnection("Desktop", 10);

			Assert.AreEqual("Desktop", connection.ConnectionTypeName);
			Assert.AreEqual(10, connection.Price);
			Assert.IsFalse(String.IsNullOrEmpty(connection.GetDetailedInfoString()));
		}


		[TestMethod]
		public void CanConnectAndDisconnect()
		{
			DesktopConnection connection = new DesktopConnection("Desktop", 10);

			connection.Connect();
			Assert.IsTrue(connection.IsConnected);

			connection.Disconnect();
			Assert.IsFalse(connection.IsConnected);
		}

		[TestMethod]
		public void ConnectRaisesStateChangeEvent()
		{
			DesktopConnection connection = new DesktopConnection("Desktop", 10);

			bool isConnected = false;
			string networkName = string.Empty;
			EventHandler<StateChangedEventArgs> handler =
				new EventHandler<StateChangedEventArgs>(
					delegate(object sender, StateChangedEventArgs args) { isConnected = args.IsConnected; });
			connection.StateChanged += handler;
			connection.Connect();

			Assert.IsTrue(isConnected);
		}

		[TestMethod]
		public void DisconnectRaisesStateChangeEvent()
		{
			DesktopConnection connection = new DesktopConnection("Desktop", 10);

			bool isConnected = true;
			string networkName = string.Empty;
			EventHandler<StateChangedEventArgs> handler =
				new EventHandler<StateChangedEventArgs>(
					delegate(object sender, StateChangedEventArgs args) { isConnected = args.IsConnected; });
			connection.StateChanged += handler;
			connection.Disconnect();

			Assert.IsFalse(isConnected);
		}
	}
}