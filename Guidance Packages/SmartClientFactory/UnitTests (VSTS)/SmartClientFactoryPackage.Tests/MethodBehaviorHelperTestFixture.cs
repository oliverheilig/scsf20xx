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
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Practices.SmartClient.DisconnectedAgent;
using Microsoft.Practices.SmartClientFactory.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SmartClientFactoryPackage.Tests
{
	[TestClass]
	public class MethodBehaviorHelperTestFixture
	{
		[TestMethod]
		public void ShouldReturnMethodsFromType()
		{
			Type mockProxyType = typeof (MockProxy);
			List<MethodInfo> methods = MethodBehaviorHelper.GetProxyMethods(mockProxyType);

			Assert.IsTrue(methods.Contains(mockProxyType.GetMethod("HelloWorld", new Type[] {})));
			Assert.IsTrue(methods.Contains(mockProxyType.GetMethod("HelloWorld", new Type[] {typeof (string)})));
			Assert.IsTrue(methods.Contains(mockProxyType.GetMethod("Foo", new Type[] {})));
		}

		[TestMethod]
		public void ShouldReturnMethodsFromAgent()
		{
			Type mockProxyAgentType = typeof (MockProxyAgent);
			Type mockProxyType = typeof (MockProxy);

			List<MethodInfo> methods = MethodBehaviorHelper.GetAgentMethods(mockProxyAgentType, mockProxyType);

			Assert.IsTrue(methods.Contains(mockProxyType.GetMethod("HelloWorld", new Type[] {})));
			Assert.IsTrue(methods.Contains(mockProxyType.GetMethod("HelloWorld", new Type[] {typeof (string)})));
			Assert.IsTrue(methods.Contains(mockProxyType.GetMethod("Foo", new Type[] {})));
		}

		[TestMethod]
		public void ShouldReturnAllMethodsForSameType()
		{
			Type mockProxyType = typeof (MockProxy);
			List<MethodInfo> methods1 = new List<MethodInfo>(mockProxyType.GetMethods());
			List<MethodInfo> methods2 = new List<MethodInfo>(mockProxyType.GetMethods());

			List<MethodInfo> filteredMethods = MethodBehaviorHelper.GetEqualMethods(methods1, methods2);

			Assert.AreNotEqual(0, methods1.Count);
			Assert.AreEqual(methods1.Count, methods2.Count);
			Assert.AreEqual(filteredMethods.Count, methods1.Count);
		}

		[TestMethod]
		public void ShouldReturnSameMethodsFromAgentThanProxy()
		{
			Type mockProxyAgentType = typeof (MockProxyAgent);
			Type mockProxyType = typeof (MockProxy);

			List<MethodInfo> agentMethods = MethodBehaviorHelper.GetAgentMethods(mockProxyAgentType, mockProxyType);

			Assert.AreEqual(3, agentMethods.Count);
			Assert.IsFalse(
				agentMethods.Contains(mockProxyAgentType.GetMethod("HelloWorld", new Type[] {typeof (OfflineBehavior)})));
			Assert.IsFalse(
				agentMethods.Contains(
					mockProxyAgentType.GetMethod("HelloWorld", new Type[] {typeof (string), typeof (OfflineBehavior)})));
			Assert.IsFalse(agentMethods.Contains(mockProxyAgentType.GetMethod("Foo", new Type[] {typeof (OfflineBehavior)})));
		}

		[TestMethod]
		public void SameMethodHasSameParameters()
		{
			Type mockProxyAgentType = typeof (MockProxyAgent);
			MethodInfo helloWorldMethod1 = mockProxyAgentType.GetMethod("HelloWorld", new Type[] {typeof (string)});
			MethodInfo helloWorldMethod2 = mockProxyAgentType.GetMethod("HelloWorld", new Type[] {typeof (string)});

			Assert.IsTrue(MethodBehaviorHelper.HasSameParameterTypes(helloWorldMethod1, helloWorldMethod2));
		}

		[TestMethod]
		public void TwoMethodHasDifferentParameters()
		{
			Type mockProxyAgentType = typeof (MockProxyAgent);
			MethodInfo helloWorldMethod1 = mockProxyAgentType.GetMethod("HelloWorld", new Type[] {});
			MethodInfo helloWorldMethod2 = mockProxyAgentType.GetMethod("HelloWorld", new Type[] {typeof (string)});

			Assert.IsFalse(MethodBehaviorHelper.HasSameParameterTypes(helloWorldMethod1, helloWorldMethod2));
		}

		[TestMethod]
		public void ShouldRemoveEqualNotSameMethod()
		{
			List<MethodInfo> agentMethods = new List<MethodInfo>(typeof (MockProxyAgent).GetMethods());
			MethodInfo proxyMethod = typeof (MockProxy).GetMethod("HelloWorld", new Type[] {typeof (string)});
			bool containsTargetMethod = agentMethods.Contains(proxyMethod);
			int methodsCount = agentMethods.Count;

			MethodBehaviorHelper.RemoveMethod(agentMethods, proxyMethod);

			Assert.IsNotNull(proxyMethod);
			Assert.AreNotEqual(0, methodsCount);
			Assert.IsFalse(containsTargetMethod);
			Assert.AreEqual(methodsCount - 1, agentMethods.Count);
		}

		[TestMethod]
		public void ShouldnotRemoveUnequalMethod()
		{
			List<MethodInfo> agentMethods = new List<MethodInfo>(typeof (MockProxyAgent).GetMethods());
			MethodInfo proxyMethod = typeof (MockProxy).GetMethod("NotAgentedMethod", new Type[] {});
			int methodsCount = agentMethods.Count;

			MethodBehaviorHelper.RemoveMethod(agentMethods, proxyMethod);

			Assert.IsNotNull(proxyMethod);
			Assert.AreNotEqual(0, methodsCount);
			Assert.AreEqual(methodsCount, agentMethods.Count);
		}

		[TestMethod]
		public void ShouldTranslateMockOfflineBehavior()
		{
			MockOfflineBehavior mockOffBehavior = new MockOfflineBehavior(DateTime.Now, 123, 456, "XXX");

			Microsoft.Practices.SmartClientFactory.DSACompatibleTypes.OfflineBehavior behavior =
				MethodBehaviorHelper.TranslateToOfflineBehavior(mockOffBehavior);

			Assert.AreEqual(mockOffBehavior.Expiration, behavior.Expiration);
			Assert.AreEqual(mockOffBehavior.MaxRetries, behavior.MaxRetries);
			Assert.AreEqual(mockOffBehavior.Stamps, behavior.Stamps);
			Assert.AreEqual(mockOffBehavior.Tag, behavior.Tag);
		}

		[TestMethod]
		public void ShouldReturnMethodNames()
		{
			List<MethodInfo> methods = new List<MethodInfo>();
			methods.AddRange(typeof (MockProxy).GetMethods());

			List<string> methodNames = MethodBehaviorHelper.GetMethodNames(methods);

			Assert.IsTrue(methodNames.Contains("HelloWorld"));
			Assert.IsTrue(methodNames.Contains("Foo"));
		}

		private class MockOfflineBehavior
		{
			public MockOfflineBehavior(DateTime expiration, int stamps, int maxRetries, string tag)
			{
				_expiration = expiration;
				_stamps = stamps;
				_maxRetries = maxRetries;
				_tag = tag;
			}

			private DateTime _expiration;

			public DateTime Expiration
			{
				get { return _expiration; }
			}

			private int _stamps;

			public int Stamps
			{
				get { return _stamps; }
			}

			private int _maxRetries;

			public int MaxRetries
			{
				get { return _maxRetries; }
			}

			private string _tag;

			public string Tag
			{
				get { return _tag; }
			}
		}

		private class MockProxy
		{
			public void HelloWorld()
			{
			}

			public int HelloWorld(string message)
			{
				return int.MaxValue;
			}

			public void Foo()
			{
			}

			public void NotAgentedMethod()
			{
			}
		}

		private class MockProxyAgent
		{
			public MockProxyAgent(IRequestQueue requestQueue)
			{
			}

			#region HelloWorld

			/// <summary>
			/// Enqueues a request to the <c>HelloWorld</c> web service method through the agent.
			/// </summary>
			/// <returns>The unique identifier associated with the request that was enqueued.</returns>
			public Guid HelloWorld()
			{
				return HelloWorld(GetHelloWorld1DefaultBehavior());
			}

			/// <summary>
			/// Enqueues a request to the <c>HelloWorld</c> web service method through the agent.
			/// </summary>
			/// <param name="behavior">The behavior associated with the offline request being enqueued.</param>
			/// <returns>The unique identifier associated with the request that was enqueued.</returns>
			public Guid HelloWorld(OfflineBehavior behavior)
			{
				return EnqueueRequest("HelloWorld", behavior);
			}

			public static OfflineBehavior GetHelloWorld1DefaultBehavior()
			{
				OfflineBehavior behavior = GetAgentDefaultBehavior();

				return behavior;
			}

			#endregion HelloWorld

			#region HelloWorld

			/// <summary>
			/// Enqueues a request to the <c>HelloWorld</c> web service method through the agent.
			/// </summary>
			/// <returns>The unique identifier associated with the request that was enqueued.</returns>
			public Guid HelloWorld(String message)
			{
				return HelloWorld(message, GetHelloWorld2DefaultBehavior());
			}

			/// <summary>
			/// Enqueues a request to the <c>HelloWorld</c> web service method through the agent.
			/// </summary>
			/// <param name="behavior">The behavior associated with the offline request being enqueued.</param>
			/// <returns>The unique identifier associated with the request that was enqueued.</returns>
			public Guid HelloWorld(String message, OfflineBehavior behavior)
			{
				return EnqueueRequest("HelloWorld", behavior, message);
			}

			public static OfflineBehavior GetHelloWorld2DefaultBehavior()
			{
				OfflineBehavior behavior = GetAgentDefaultBehavior();

				return behavior;
			}

			#endregion HelloWorld

			#region Foo

			/// <summary>
			/// Enqueues a request to the <c>Foo</c> web service method through the agent.
			/// </summary>
			/// <returns>The unique identifier associated with the request that was enqueued.</returns>
			public Guid Foo()
			{
				return Foo(GetFooDefaultBehavior());
			}

			/// <summary>
			/// Enqueues a request to the <c>Foo</c> web service method through the agent.
			/// </summary>
			/// <param name="behavior">The behavior associated with the offline request being enqueued.</param>
			/// <returns>The unique identifier associated with the request that was enqueued.</returns>
			public Guid Foo(OfflineBehavior behavior)
			{
				return EnqueueRequest("Foo", behavior);
			}

			public static OfflineBehavior GetFooDefaultBehavior()
			{
				OfflineBehavior behavior = GetAgentDefaultBehavior();

				return behavior;
			}

			#endregion Foo

			#region Common

			public static OfflineBehavior GetAgentDefaultBehavior()
			{
				OfflineBehavior behavior = new OfflineBehavior();
				behavior.MaxRetries = 9;
				behavior.Stamps = 9;
				behavior.Tag = "9999";
				behavior.Expiration = DateTime.Now + new TimeSpan(0, 0, 0, 30);
				behavior.ProxyFactoryType = typeof (ObjectProxyFactory);

				return behavior;
			}

			private Guid EnqueueRequest(string methodName, OfflineBehavior behavior, params object[] arguments)
			{
				Request request = CreateRequest(methodName, behavior, arguments);
				return request.RequestId;
			}

			private static Request CreateRequest(string methodName, OfflineBehavior behavior, params object[] arguments)
			{
				Request request = new Request();
				return request;
			}

			public static Type OnlineProxyType
			{
				get { return typeof (MockProxy); }
			}

			public static string Endpoint
			{
				get { return "FFTTYYUU"; }
			}

			#endregion
		}
	}
}