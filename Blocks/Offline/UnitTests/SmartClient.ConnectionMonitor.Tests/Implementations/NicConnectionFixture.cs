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
using System.Net.NetworkInformation;
using Microsoft.Practices.SmartClient.ConnectionMonitor.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.SmartClient.ConnectionMonitor.Tests.Implementations
{
	/// <summary>
	/// Summary description for NicConnectionFixture
	/// </summary>
	[TestClass]
	public class NicConnectionFixture
	{
		[TestMethod]
		public void GetNetworkNameReturnsNetworkName()
		{
			TestableConnection connection = new TestableConnection("Test", 1);

			Assert.AreEqual("Test", connection.ConnectionTypeName);
			Assert.AreEqual(1, connection.Price);
		}

		[TestMethod]
		public void TestDisposeOnNicConnection()
		{
			TestableConnection connection = new TestableConnection("Test", 100);

			connection.Dispose();
		}

		[TestMethod]
		public void TestDisposeOnWiredConnection()
		{
			WiredConnection connection = new WiredConnection("Test", 100);
			connection.Dispose();
		}

		[TestMethod]
		public void TestDisposeOnWirelessConnection()
		{
			WirelessConnection connection = new WirelessConnection("Test", 100);

			connection.Dispose();
		}
	}

	internal class TestableConnection : NicConnection
	{
		public TestableConnection(string name, int price)
			: base(name, price)
		{
		}

		protected override NetworkInterface DetectConnectedAdapter()
		{
			return null;
		}

		public override string GetDetailedInfoString()
		{
			return "Testable Connection";
		}
	}
}