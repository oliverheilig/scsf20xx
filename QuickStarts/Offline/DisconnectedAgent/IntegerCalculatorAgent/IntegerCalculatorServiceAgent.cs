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
using Microsoft.Practices.SmartClient.DisconnectedAgent;
using System;

namespace Quickstarts.DisconnectedAgent.IntegerCalculatorAgent
{
	// Use this proxy to make requests to the service when working in an application that is occasionally connected
	public partial class IntegerCalculatorServiceDisconnectedAgent
	{
		IRequestQueue requestQueue;

		public IntegerCalculatorServiceDisconnectedAgent(IRequestQueue requestQueue)
		{
			this.requestQueue = requestQueue;
		}

		#region Add

		/// <summary>
		/// Enqueues a request to the <c>Add</c> web service method through the agent.
		/// </summary>
		/// <returns>The unique identifier associated with the request that was enqueued.</returns>
		public Guid Add(Int32 a, Int32 b)
		{
			return Add(a, b, GetAddDefaultBehavior());
		}

		/// <summary>
		/// Enqueues a request to the <c>Add</c> web service method through the agent.
		/// </summary>
		/// <param name="behavior">The behavior associated with the offline request being enqueued.</param>
		/// <returns>The unique identifier associated with the request that was enqueued.</returns>
		public Guid Add(Int32 a, Int32 b, OfflineBehavior behavior)
		{
			behavior.ReturnCallback = new CommandCallback(typeof(IntegerCalculatorServiceDisconnectedAgentCallback), "OnAddReturn");
			behavior.ExceptionCallback = new CommandCallback(typeof(IntegerCalculatorServiceDisconnectedAgentCallback), "OnAddException");

			return EnqueueRequest("Add", behavior, a, b);
		}

		public static OfflineBehavior GetAddDefaultBehavior()
		{
			OfflineBehavior behavior = GetAgentDefaultBehavior();
			behavior.ReturnCallback = new CommandCallback(typeof(IntegerCalculatorServiceDisconnectedAgentCallback), "OnAddReturn");
			behavior.ExceptionCallback = new CommandCallback(typeof(IntegerCalculatorServiceDisconnectedAgentCallback), "OnAddException");
			return behavior;
		}

		#endregion Add

		#region Common

		public static OfflineBehavior GetAgentDefaultBehavior()
		{
			OfflineBehavior behavior = new OfflineBehavior();
			behavior.ProxyFactoryType = typeof(WebServiceProxyFactory);
			behavior.MaxRetries = 1;
			behavior.Stamps = 1;
            behavior.Expiration = DateTime.Now + new TimeSpan(0, 0, 30);
			return behavior;
		}

		private Guid EnqueueRequest(string methodName, OfflineBehavior behavior, params object[] arguments)
		{
			Request request = CreateRequest(methodName, behavior, arguments);

			requestQueue.Enqueue(request);

			return request.RequestId;
		}

		public static Request CreateRequest(string methodName, OfflineBehavior behavior, params object[] arguments)
		{
			Request request = new Request();
			request.MethodName = methodName;
			request.Behavior = behavior;
			request.CallParameters = arguments;

			request.OnlineProxyType = OnlineProxyType;
			request.Endpoint = Endpoint;

			return request;
		}

        public static string Endpoint
        {
            get { return "IntegerCalculatorEndpoint"; }
        }

        public static Type OnlineProxyType
        {
            get { return typeof(Quickstarts.DisconnectedAgent.IntegerCalculatorAgent.IntegerCalculatorService.IntegerCalculatorService); }
        }

		#endregion
	}
}
