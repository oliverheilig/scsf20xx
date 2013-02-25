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

namespace Microsoft.Practices.SmartClient.DisconnectedAgent
{
	/// <summary>
	/// Interface describing the contract for a proxy factory object.
	/// </summary>
	public interface IProxyFactory
	{
		/// <summary>
		/// This method allows you to create the proxy object in concrete implementations.
		/// You can use the OnlineProxyType property of the Request to know the expected type for the proxy object.
		/// The concrete proxy factory should set specific properties in the proxy object like the URL and credentials. 
		/// To set the URL, the proxy factory can use the network name and the Request's endpoint.
		/// </summary>
		/// <param name="request">Request to be dispatched.</param>
		/// <param name="networkName">Current network name.</param>
		/// <returns>The proxy object.</returns>
		object GetOnlineProxy(Request request, string networkName);

		/// <summary>
		/// Calls the specified method on the proxy object to fulfill the request.
		/// </summary>
		/// <param name="onlineProxy">A reference to a proxy object created by the factory.</param>
		/// <param name="request">The <see cref="Request"/> with the data to perform the method call.</param>
		/// <param name="ex">A reference to an <see cref="Exception"/> that should be handled by an exception callback.</param>
		/// <returns>The result of the method invocation.</returns>
		/// <exception cref="OnlineProxyException">Thrown to signal that an unhandled exception ocurred.</exception>
		object CallOnlineProxyMethod(object onlineProxy, Request request, ref Exception ex);

        /// <summary>
        /// This method allows to release resources obtained by the proxy object in concrete implementations.
        /// The <see cref="RequestDispatcher"/> class calls this method after dispach a request using the proxy.
        /// </summary>
        /// <param name="onlineProxy">A reference to a proxy object created by the factory.</param>
        void ReleaseOnlineProxy(object onlineProxy);
    }
}