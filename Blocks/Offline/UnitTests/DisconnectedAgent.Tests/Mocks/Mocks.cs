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
using System.Web.Services;
using System.Web.Services.Protocols;
using Microsoft.Practices.SmartClient.ApplicationBlocks.DisconnectedAgent.Tests.Mocks;
using Microsoft.Practices.SmartClient.DisconnectedAgent;

namespace Microsoft.Practices.SmartClient.ApplicationBlocks.DisconnectedAgent.Tests
{
	public class SimpleOnlineProxyFactory : WebServiceProxyFactory
	{
		public Request LastRequest;

		public SimpleOnlineProxyFactory()
			: base(new MockEndpointCatalog())
		{
		}

		public override object GetOnlineProxy(Request request, string networkName)
		{
			LastRequest = request;
			return base.GetOnlineProxy(request, networkName);
		}
	}

	public class OfflineMockService
	{
		private MockRequestQueue requestQueue;

		public OfflineMockService(MockRequestQueue requestQueue)
		{
			this.requestQueue = requestQueue;
		}

		public void DoWithNoParams()
		{
			OfflineBehavior behavior = GetDefaultOfflineBehavior();
			behavior.ReturnCallback = new CommandCallback(typeof (OfflineMockServiceCallbacks), "DoWithNoParamsCallback");

			DoWithNoParams(behavior);
		}

		public void DoWithNoParams(OfflineBehavior behavior)
		{
			Request r = new Request();
			r.MethodName = "DoWithNoParams";
			r.Endpoint = "MockService";
			r.OnlineProxyType = typeof (MockService);

			r.Behavior = behavior;

			requestQueue.Enqueue(r);
		}

		public void DoWithParamsAndReturn(int p, MockServiceDataContract obj)
		{
			OfflineBehavior behavior = GetDefaultOfflineBehavior();
			behavior.ReturnCallback = new CommandCallback(typeof (OfflineMockServiceCallbacks), "OnDoWithParamsAndReturnReturn");

			DoWithParamsAndReturn(p, obj, behavior);
		}

		public void DoWithParamsAndReturn(int p, MockServiceDataContract obj, OfflineBehavior behavior)
		{
			Request r = new Request();
			r.MethodName = "DoWithParamsAndReturn";
			r.Endpoint = "MockService";
			r.OnlineProxyType = typeof (MockService);
			r.CallParameters = CallParameters.ToArray(p, obj);

			r.Behavior = behavior;

			requestQueue.Enqueue(r);
		}


		private OfflineBehavior GetDefaultOfflineBehavior()
		{
			OfflineBehavior behavior = new OfflineBehavior();

			//Set default offlineBehavior values
			behavior.ExceptionCallback = new CommandCallback(typeof (OfflineMockServiceCallbacks), "OnMockServiceException");
			behavior.Expiration = null;
			behavior.MaxRetries = 1;
			behavior.Stamps = 3;
			behavior.Tag = string.Empty;
			return behavior;
		}
	}

	public class OfflineMockServiceCallbacks
	{
		public static bool ReturnInvoked;
		public static bool ExceptionInvoked;
		public static Request LastRequest;
		public static object[] LastParams;
		public static Exception LastException;

		public void OnDoWithNoParamsReturn(Request request, object[] parameters)
		{
			LastRequest = request;
			ReturnInvoked = true;
		}

		public void OnDoWithNoParamSReturnThrowingException(Request request, object[] parameters)
		{
			LastRequest = request;
			ReturnInvoked = true;
			throw new MockException(); //Exception within the return callback.
		}

		public void OnDoWithParamsAndReturnReturn(Request request, object[] parameters, int result)
		{
			LastRequest = request;
			ReturnInvoked = true;
		}

		public void OnDoWithOutParamsAndReturnReturn(Request request, object[] parameters, int result)
		{
			LastRequest = request;
			ReturnInvoked = true;
			LastParams = parameters;
		}

		public OnExceptionAction OnMockServiceException(Request request, Exception exception)
		{
			LastRequest = request;
			LastException = exception;
			ExceptionInvoked = true;
			return OnExceptionAction.Retry;
		}

		public OnExceptionAction OnMockServiceExceptionRetriesTwice(Request request, Exception exception)
		{
			LastRequest = request;
			LastException = exception;
			ExceptionInvoked = true;
			if (MockService.InvokedTimes <= 2)
				return OnExceptionAction.Retry;
			else
				return OnExceptionAction.Dismiss;
		}

		public void OnDoWithNoParamsAndStringReturnReturn(Request request, object[] parameters, string result)
		{
			LastRequest = request;
			ReturnInvoked = true;
		}

		public OnExceptionAction OnMockServiceExceptionThrowsException(Request request, Exception exception)
		{
			throw exception;
		}
	}

	[WebServiceBinding(Name = "ServiceSoap", Namespace = "http://tempuri.org/")]
	public class MockService : SoapHttpClientProtocol
	{
		public static bool Invoked = false;
		public static int InvokedTimes = 0;
		public static Exception exception;
		public static EventHandler ForcedExceptionEvent;

		public void DoWithNoParams()
		{
			Invoked = true;
		}

		public int DoWithParamsAndReturn(int number, MockServiceDataContract obj)
		{
			Invoked = true;
			return obj.Age;
		}

		public int DoWithOutParamsAndReturn(int inNumber, out int outNumber, MockServiceDataContract inObj,
		                                    out MockServiceDataContract outObj)
		{
			Invoked = true;
			outNumber = inNumber;
			outObj = inObj;
			return inObj.Age;
		}

		public int DoThrowingAnException()
		{
			InvokedTimes++;
			throw new MockException();
		}

		public string DoWithNoParamsAndStringReturn()
		{
			return null;
		}

		public void ExceptionThrow()
		{
			if (exception != null)
			{
				if (ForcedExceptionEvent != null) ForcedExceptionEvent(this, EventArgs.Empty);
				throw exception;
			}
		}
	}

	public class MockException : Exception
	{
		public MockException() :
			base("Mock Exception")
		{
		}
	}


	public class MockServiceDataContract
	{
		public string Name;
		public int Age;

		public MockServiceDataContract(string name, int age)
		{
			Name = name;
			Age = age;
		}
	}
}