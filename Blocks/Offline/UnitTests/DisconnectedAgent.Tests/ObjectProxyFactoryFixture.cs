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

namespace Microsoft.Practices.SmartClient.DisconnectedAgent.Tests
{
	/// <summary>
	/// Summary description for ObjectProxyFactoryFixture
	/// </summary>
	[TestClass]
	public class ObjectProxyFactoryFixture
	{
		[TestMethod]
		public void FactoryIsIProxyFactory()
		{
			ObjectProxyFactory factory = new ObjectProxyFactory();

			Assert.IsTrue(factory is IProxyFactory);
		}


		[TestMethod]
		public void FactoryCreatesAnObjectOfTheOnlineProxyType()
		{
			Request request = new Request();
			request.OnlineProxyType = typeof (MockProxy);

			ObjectProxyFactory factory = new ObjectProxyFactory();


			object proxy = factory.GetOnlineProxy(request, "Network");

			Assert.IsNotNull(proxy);
			Assert.AreEqual(typeof (MockProxy), proxy.GetType());
		}

		[TestMethod]
		public void FactoryCallTheSpecifiedMethod()
		{
			Request request = new Request();
			request.MethodName = "CalledMethod";
			request.OnlineProxyType = typeof (MockProxy);


			ObjectProxyFactory factory = new ObjectProxyFactory();

			MockProxy proxy = (MockProxy) factory.GetOnlineProxy(request, "Network");

			Exception exception = null;

			factory.CallOnlineProxyMethod(proxy, request, ref exception);

			Assert.IsTrue(proxy.MethodCalled);
		}

		private class MockProxy
		{
			public bool MethodCalled = false;

			public void CalledMethod()
			{
				MethodCalled = true;
			}
		}
	}
}