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
	/// Summary description for OfflineBehaviorFixture
	/// </summary>
	[TestClass]
	public class OfflineBehaviorFixture
	{
		[TestMethod]
		public void CanSpecifyTheProxyFactoryType()
		{
			OfflineBehavior behavior = new OfflineBehavior();
			behavior.ProxyFactoryType = typeof (MockProxyFactory);


			Assert.AreEqual(typeof (MockProxyFactory), behavior.ProxyFactoryType);
		}


		private class MockProxyFactory : IProxyFactory
		{
			public object GetOnlineProxy(Request request, string networkName)
			{
				throw new Exception("The method or operation is not implemented.");
			}

			public object CallOnlineProxyMethod(object onlineProxy, Request request, ref Exception ex)
			{
				throw new Exception("The method or operation is not implemented.");
			}

			public void ReleaseOnlineProxy(object onlineProxy)
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}
	}
}