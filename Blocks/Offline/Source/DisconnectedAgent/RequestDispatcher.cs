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
using System.Reflection;
using Microsoft.Practices.SmartClient.DisconnectedAgent.Properties;

namespace Microsoft.Practices.SmartClient.DisconnectedAgent
{
	/// <summary>
	/// An object that can dispatch <see cref="Request"/>s.
	/// </summary>
	public class RequestDispatcher : IRequestDispatcher
	{
		/// <summary>
		/// Dispatches a <see cref="Request"/> on the given network.
		/// </summary>
		/// <param name="request">Request to be dispatched.</param>
		/// <param name="networkName">Current network name.</param>
		/// <returns>DispatchResult with the corresponding result of the dispatching process.</returns>
		public virtual DispatchResult Dispatch(Request request, string networkName)
		{
			IProxyFactory factory = null;

			if (request.Behavior.ProxyFactoryType == null)
			{
				throw new ArgumentNullException("request.Behavior.ProxyFactoryType");
			}
			try
			{
				factory = (IProxyFactory)Activator.CreateInstance(request.Behavior.ProxyFactoryType);
			}
			catch
			{
				return DispatchResult.Failed;
			}

			object onlineProxy = factory.GetOnlineProxy(request, networkName);

			if (onlineProxy == null)
			{
				return DispatchResult.Failed;
			}

			DispatchResult? dispatchResult = null;
			int retries = 0;

			try
			{
				while (dispatchResult == null)
				{
					Exception exception = null;
					try
					{
						retries++;
						object result = factory.CallOnlineProxyMethod(onlineProxy, request, ref exception);

						try
						{
							// Proxy method call went ok, call the return command
							InvokeReturnCommand(onlineProxy, request, result);
						}
						catch (Exception ex)
						{
							if (ex is TargetInvocationException)
							{
								throw new ReturnCallbackException(Resources.ExceptionOnReturnCallback + ex.Message, ex.InnerException);
							}
							else
							{
								throw new ReturnCallbackException(Resources.ExceptionOnReturnCallback + ex.Message, ex);
							}
						}

						dispatchResult = DispatchResult.Succeeded;
						break;
					}

					catch (OnlineProxyException ex)
					{
						throw ex.InnerException;
					}

					catch (TargetInvocationException ex)
					{
						exception = ex.InnerException;
					}

					catch (Exception ex)
					{
						exception = ex;
					}

					if (exception != null)
					{
						// Call the exception callback
						try
						{
							switch (InvokeExceptionCommand(request, exception))
							{
								case OnExceptionAction.Dismiss:
									dispatchResult = DispatchResult.Failed;
									break;
								case OnExceptionAction.Retry:
									if (retries >= request.Behavior.MaxRetries)
									{
										dispatchResult = DispatchResult.Failed;
									}
									break;
							}
						}
						catch
						{
							dispatchResult = DispatchResult.Failed;
						}
					}
				}
			}
			finally
			{
				factory.ReleaseOnlineProxy(onlineProxy);
			}

			return (DispatchResult)dispatchResult;

		}

		/// <summary>
		/// Invokes the callback specified as the callback to call when an error occurs.
		/// </summary>
		/// <param name="request">The <see cref="Request"/> that failed.</param>
		/// <param name="realException">An <see cref="Exception"/> object describing the failure.</param>
		/// <returns>An <see cref="OnExceptionAction"/>.</returns>
		protected virtual OnExceptionAction InvokeExceptionCommand(Request request, Exception realException)
		{
			if (request.Behavior.ExceptionCallback != null)
			{
				return (OnExceptionAction) request.Behavior.ExceptionCallback.Invoke(request, realException);
			}
			return OnExceptionAction.Dismiss;
		}

		/// <summary>
		/// Invokes the callback specified as the return callback when a call completes.
		/// </summary>
		/// <param name="onlineProxy">The proxy object used to make the call.</param>
		/// <param name="request">The <see cref="Request"/>.</param>
		/// <param name="result">The result of the call.</param>
		protected virtual void InvokeReturnCommand(object onlineProxy, Request request, object result)
		{
			if (request.Behavior.ReturnCallback != null)
			{
				MethodInfo method = onlineProxy.GetType().GetMethod(request.MethodName);
				if (method.ReturnType != typeof (void))
					request.Behavior.ReturnCallback.Invoke(request, request.CallParameters, result);
				else
					request.Behavior.ReturnCallback.Invoke(request, request.CallParameters);
			}
		}
	}
}