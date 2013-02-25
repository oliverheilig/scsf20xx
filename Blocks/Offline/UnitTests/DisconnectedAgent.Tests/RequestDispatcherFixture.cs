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
	/// Summary description for RequestDispatcherFixture
	/// </summary>
	[TestClass]
	public class RequestDispatcherFixture
	{
		[TestInitialize]
		public void Initialize()
		{
			MockProxyFactory.Instance = null;
			MockProxyFactory.OnlineProxy = null;
			MockCallbacks.TimesCalled = 0;
		}

		[TestMethod]
		public void UsesTheProxyFactorySpecifiedInRequest()
		{
			RequestDispatcher dispatcher = new RequestDispatcher();
			Request request = new Request();
			request.Behavior = new OfflineBehavior();
			request.Behavior.ProxyFactoryType = typeof (MockProxyFactory);

			dispatcher.Dispatch(request, "Network");

			Assert.IsNotNull(MockProxyFactory.Instance);
		}

		[TestMethod]
		public void ReturnsFailedIfCannotCreateFactory()
		{
			RequestDispatcher dispatcher = new RequestDispatcher();
			Request request = new Request();
			request.Behavior = new OfflineBehavior();
			request.Behavior.ProxyFactoryType = typeof (ImpossibleProxyFactory);

			DispatchResult result = dispatcher.Dispatch(request, "Network");

			Assert.AreEqual(DispatchResult.Failed, result);
		}

		[TestMethod]
		public void ReturnsFailedIfCannotGetProxyInstance()
		{
			RequestDispatcher dispatcher = new RequestDispatcher();
			Request request = new Request();
			request.Behavior = new OfflineBehavior();
			request.Behavior.ProxyFactoryType = typeof (MockProxyFactory);
			DispatchResult result = dispatcher.Dispatch(request, "Network");

			Assert.AreEqual(DispatchResult.Failed, result);
		}

		[TestMethod]
		public void UsesFactoryToGetProxy()
		{
			RequestDispatcher dispatcher = new RequestDispatcher();
			Request request = new Request();
			request.Behavior = new OfflineBehavior();
			request.Behavior.ProxyFactoryType = typeof (MockProxyFactory);

			dispatcher.Dispatch(request, "Network");

			Assert.AreEqual(request, MockProxyFactory.Instance.GetOnlineProxyRequest);
			Assert.AreEqual("Network", MockProxyFactory.Instance.GetOnlineProxyNetworkName);
		}

		[TestMethod]
		public void FailsAfterRetryingTheSpecifiedNumberOfTimes()
		{
			RequestDispatcher dispatcher = new RequestDispatcher();
			Request request = new Request();
			request.Behavior = new OfflineBehavior();
			request.Behavior.ProxyFactoryType = typeof (MockProxyFactory);
			request.Behavior.MaxRetries = 5;
			request.Behavior.ExceptionCallback = new CommandCallback(typeof (MockCallbacks), "OnException");
			request.MethodName = "TheMethod";

			MockProxy proxy = new MockProxy();
			MockProxyFactory.OnlineProxy = proxy;

			DispatchResult result = dispatcher.Dispatch(request, "Network");


			Assert.AreEqual(DispatchResult.Failed, result);
			Assert.AreEqual(5, MockCallbacks.TimesCalled);
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentNullException))]
		public void FailsWhenNoFactoryTypeIsNull()
		{
			Request r = new Request();

			RequestDispatcher dispatcher = new RequestDispatcher();

			DispatchResult result = dispatcher.Dispatch(r, "Network");
		}

		[TestMethod]
		public void ProxyObjectMustBeDisposedIfCreatedForDispatch()
		{
			RequestDispatcher dispatcher = new RequestDispatcher();
			Request request = new Request();

			request.Behavior = new OfflineBehavior();
			request.Behavior.ProxyFactoryType = typeof(MockDisposableProxyFactory);

			DispatchResult result = dispatcher.Dispatch(request, "Network");

			Assert.IsNotNull(MockDisposableProxyFactory.LastProxy);
			Assert.IsTrue(MockDisposableProxyFactory.LastProxy.Disposed);
		}

		private class MockProxyFactory : IProxyFactory
		{
			public static MockProxyFactory Instance;
			public static object OnlineProxy;

			public MockProxyFactory()
			{
				Instance = this;
			}

			public Request GetOnlineProxyRequest = null;
			public string GetOnlineProxyNetworkName = null;

			public object GetOnlineProxy(Request request, string networkName)
			{
				GetOnlineProxyRequest = request;
				GetOnlineProxyNetworkName = networkName;
				return OnlineProxy;
			}


			public object CallOnlineProxyMethod(object onlineProxy, Request request, ref Exception ex)
			{
				throw new Exception("The method or operation is not implemented.");
			}

			public void ReleaseOnlineProxy(object onlineProxy)
			{
				
			}
		}

		private class ImpossibleProxyFactory
		{
			private ImpossibleProxyFactory()
			{
			}
		}

		public class MockProxy
		{
		}

		private class MockCallbacks
		{
			public static int TimesCalled;

			public OnExceptionAction OnException(Request request, Exception ex)
			{
				TimesCalled++;
				return OnExceptionAction.Retry;
			}
		}

		private class MockDisposableProxyFactory : IProxyFactory
		{
			public static MockDisposableProxy LastProxy = null;

			#region IProxyFactory Members

			public object GetOnlineProxy(Request request, string networkName)
			{
				MockDisposableProxy newProxy = new MockDisposableProxy();
				LastProxy = newProxy;
				return newProxy;
			}

			public object CallOnlineProxyMethod(object onlineProxy, Request request, ref Exception ex)
			{
				return null;
			}

			#endregion

			#region IProxyFactory Members

			public void ReleaseOnlineProxy(object onlineProxy)
			{
				if (onlineProxy == null)
					throw new ArgumentException();
				MockDisposableProxy proxy = onlineProxy as MockDisposableProxy;
				if (proxy == null)
					throw new ArgumentException();
				proxy.Dispose();
			}

			#endregion
		}

		private class MockDisposableProxy : IDisposable
		{
			public bool Disposed;
			public MockDisposableProxy()
			{
				Disposed = false;
			}

			public void Dispose()
			{
				Disposed = true;
			}
		}
	}
}