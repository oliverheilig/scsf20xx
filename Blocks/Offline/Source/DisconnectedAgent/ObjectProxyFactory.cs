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

namespace Microsoft.Practices.SmartClient.DisconnectedAgent
{
	/// <summary>
	/// An <see cref="IProxyFactory"/> to create proxies to regular objects.
	/// It uses the OnlineProxyType property of the Request to know the expected type for the proxy object.
	/// </summary>
	public class ObjectProxyFactory : IProxyFactory
	{
		/// <summary>
		/// Gets the online proxy object for a <see cref="Request"/>.
		/// </summary>
		/// <param name="request">The <see cref="Request"/> to genereate a proxy object for.</param>
		/// <param name="networkName">Current network name.</param>
		/// <returns>The proxy object.</returns>
		public object GetOnlineProxy(Request request, string networkName)
		{
			Guard.ArgumentNotNull(request, "request");
			return Activator.CreateInstance(request.OnlineProxyType);
		}

		/// <summary>
        /// Calls the Dispose method of the proxy object if it implements <see cref="IDisposable"/>.
		/// </summary>
        /// <param name="onlineProxy">A reference to a proxy object created by the factory.</param>
		public virtual void ReleaseOnlineProxy(object onlineProxy)
		{
			Guard.ArgumentNotNull(onlineProxy, "onlineProxy");
			if (onlineProxy is IDisposable)
			{
				IDisposable proxy = onlineProxy as IDisposable;
				proxy.Dispose();
			}
		}

		/// <summary>
		/// Calls the specified method on the proxy object to fulfill the request.
		/// </summary>
		/// <param name="onlineProxy">A reference to a proxy object created by the factory.</param>
		/// <param name="request">The <see cref="Request"/> with the data to perform the method call.</param>
		/// <param name="ex">A reference to an <see cref="Exception"/> that should be handled by an exception callback.</param>
		/// <returns>The result of the method invocation.</returns>
		/// <exception cref="OnlineProxyException">Thrown to signal that an unhandled exception ocurred.</exception>
		public object CallOnlineProxyMethod(object onlineProxy, Request request, ref Exception ex)
		{
			Guard.ArgumentNotNull(onlineProxy, "onlineProxy");
			Guard.ArgumentNotNull(request, "request");

			MethodInfo method = onlineProxy.GetType().GetMethod(request.MethodName);
			return method.Invoke(onlineProxy, request.CallParameters);
		}
	}
}