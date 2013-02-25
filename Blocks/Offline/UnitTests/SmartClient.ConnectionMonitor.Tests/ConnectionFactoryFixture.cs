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
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.SmartClient.ConnectionMonitor.Implementations;

namespace Microsoft.Practices.SmartClient.ConnectionMonitor.Tests
{
	/// <summary>
	/// Summary description for ConnectionFactoryFixture
	/// </summary>
	[TestClass]
	public class ConnectionFactoryFixture
	{
		public ConnectionFactoryFixture()
		{
		}

		[TestMethod]
		public void CanCreateDesktopConnection()
		{
			Connection connection = ConnectionFactory.CreateConnection("DesktopConnection", 1);
			Assert.IsNotNull(connection);
			Assert.IsTrue(connection is DesktopConnection);
		}

		[TestMethod]
		public void CanCreateNicConnection()
		{
			Connection connection = ConnectionFactory.CreateConnection("NicConnection", 1);
			Assert.IsNotNull(connection);
			Assert.IsTrue(connection is NicConnection);
		}


		[TestMethod]
		public void CanCreateWirelessConnection()
		{
			Connection connection = ConnectionFactory.CreateConnection("WirelessConnection", 1);
			Assert.IsNotNull(connection);
			Assert.IsTrue(connection is WirelessConnection);
		}


		[TestMethod]
		public void CanCreateWiredConnection()
		{
			Connection connection = ConnectionFactory.CreateConnection("WiredConnection", 1);
			Assert.IsNotNull(connection);
			Assert.IsTrue(connection is WiredConnection);
		}

		[TestMethod]
		public void CanCreateMyCustomConnection()
		{
			Connection connection = ConnectionFactory.CreateConnection("Microsoft.Practices.SmartClient.ConnectionMonitor.Tests.MyCustomConnection,SmartClient.ConnectionMonitor.Tests", 1);
			Assert.IsNotNull(connection);
			Assert.IsTrue(connection is MyCustomConnection);
		}
		
		[TestMethod]
		[ExpectedException(typeof(ConnectionMonitorException))]
		public void CreateConnectionThrowsWhenPassedBadType()
		{
			Connection connection = ConnectionFactory.CreateConnection("BadTypeName", 1);
		}
		
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void CreateConnectionThrowsWhenPassedNullType()
		{
			Connection connection = ConnectionFactory.CreateConnection(null, 1);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void CreateConnectionThrowsWhenPassedEmptyType()
		{
			Connection connection = ConnectionFactory.CreateConnection(String.Empty, 1);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void CreateConnectionThrowsWhenPassedNegativePrice()
		{
			Connection connection = ConnectionFactory.CreateConnection("WiredConnection", -1);
		}

	}
}
