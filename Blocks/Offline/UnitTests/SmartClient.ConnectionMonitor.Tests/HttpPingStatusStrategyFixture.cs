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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.SmartClient.ConnectionMonitor.Tests
{
	[TestClass]
	public class HttpPingStatusStrategyFixture
	{
		[TestMethod]
		public void IsAliveReturnsFalseForBadIPAddress()
		{
			INetworkStatusStrategy strategy = new HttpPingStatusStrategy();
			bool isAlive = strategy.IsAlive("http://123.456.789.098/");
			Assert.IsFalse(isAlive);
		}

		[TestMethod]
		public void IsAliveReturnsTrueForMyAddress()
		{
			INetworkStatusStrategy strategy = new HttpPingStatusStrategy();
			bool isAlive = false;
			// This test case will randomly fail for no reason.  Try 3 times just to be sure.
			for (int i = 0; i < 3; i++ )
			{
				if (strategy.IsAlive("http://127.0.0.1/"))
				{
					isAlive = true;
				}
			}

			Assert.IsTrue(isAlive);
		}

		[TestMethod]
		public void IsAliveReturnsTrueForMissingPageAtGoodAddress()
		{
			INetworkStatusStrategy strategy = new HttpPingStatusStrategy();
			bool isAlive = false;
			// This test case will randomly fail for no reason.  Try 3 times just to be sure.
            for (int i = 0; i < 3; i++ )
			{
				if (strategy.IsAlive("http://127.0.0.1/myPage.aspx"))
				{
					isAlive = true;
				}
			}

			Assert.IsTrue(isAlive);
		}

		[TestMethod]
		public void IsAliveReturnsFalseForBogusAddress()
		{
			INetworkStatusStrategy strategy = new HttpPingStatusStrategy();
			bool isAlive = strategy.IsAlive("http://thereisnosuchcomputer/");
			Assert.IsFalse(isAlive);
		}

		[TestMethod]
		[ExpectedException(typeof (UriFormatException))]
		public void IsAliveReturnsFalseForBadlyFormedAddress()
		{
			INetworkStatusStrategy strategy = new HttpPingStatusStrategy();
			bool isAlive = strategy.IsAlive("123789.098");
		}

		[TestMethod]
		[ExpectedException(typeof (UriFormatException))]
		public void IsAliveReturnsFalseForAddressWithoutHttpPrefix()
		{
			INetworkStatusStrategy strategy = new HttpPingStatusStrategy();
			bool isAlive = strategy.IsAlive("productsweb");
		}
	}
}