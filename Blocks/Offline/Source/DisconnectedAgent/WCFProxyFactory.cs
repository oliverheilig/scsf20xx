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
using System.Net;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.Practices.SmartClient.EndpointCatalog;

namespace Microsoft.Practices.SmartClient.DisconnectedAgent
{
	/// <summary>
	/// An <see cref="IProxyFactory"/> to create proxies to WCF web services.
	/// It uses the OnlineProxyType property of the Request to know the expected type for the proxy object.
	/// Additionaly it sets the URL and Credentials properties in the proxy object. 
	/// To set these properties, the factory uses the EndpointCatalog to get the right address and credentials
	/// for the current network name and the Request's endpoint.
	/// </summary>
	public class WCFProxyFactory<TChannel> : IProxyFactory
		where TChannel : class
	{
		private IEndpointCatalog endpointCatalog;

		/// <summary>
		/// Creates a WCFProxyFactory object.
		/// </summary>
		public WCFProxyFactory()
			: this(RequestManager.Instance.EndpointCatalog)
		{
		}

		/// <summary>
		/// Creates a WCFProxyFactory object.
		/// </summary>
		/// <param name="endpointCatalog">The <see cref="IEndpointCatalog"/> that contains address and credentials for communicating to the WCF endpoint.</param>
		public WCFProxyFactory(IEndpointCatalog endpointCatalog)
		{
			this.endpointCatalog = endpointCatalog;
		}

		/// <summary>
		/// This method allows you to create the proxy object in concrete implementations.
		/// You can use the OnlineProxyType property of the Request to know the expected type for the proxy object.
		/// The concrete proxy factory should set specific properties in the proxy object like the URL and credentials. 
		/// To set the URL, the proxy factory can use the network name and the Request's endpoint.
		/// </summary>
		/// <param name="request">Request to be dispatched.</param>
		/// <param name="networkName">Current network name.</param>
		/// <returns>The proxy object.</returns>
		public virtual object GetOnlineProxy(Request request, string networkName)
		{
			Guard.ArgumentNotNull(request, "request");

			ClientBase<TChannel> proxy = (ClientBase<TChannel>) Activator.CreateInstance(request.OnlineProxyType);

			// Set the credentials
			ClientCredentials clientCredentials = proxy.ClientCredentials;

			if ((endpointCatalog != null) && (endpointCatalog.Count > 0) && (endpointCatalog.EndpointExists(request.Endpoint)))
			{
				NetworkCredential networkCredential = EndpointCatalog.GetCredentialForEndpoint(request.Endpoint, networkName);

				clientCredentials.UserName.UserName = networkCredential.UserName;
				clientCredentials.UserName.Password = networkCredential.Password;
				EndpointAddress address = new EndpointAddress(EndpointCatalog.GetAddressForEndpoint(request.Endpoint, networkName));
				proxy.Endpoint.Address = address;
			}

			return proxy;
		}

		/// <summary>
        /// Close the ClientBase proxy object if its State is Opened
        /// or Abort the ClientBase proxy object if its State is Faulted
		/// </summary>
        /// <param name="onlineProxy">A reference to a proxy object created by the factory.</param>
        /// <exception cref="InvalidCastException">If onlineProxy does not implements ICommunicationObject</exception>
		public virtual void ReleaseOnlineProxy(object onlineProxy)
		{
			Guard.ArgumentNotNull(onlineProxy, "onlineProxy");
			ICommunicationObject proxy = onlineProxy as ICommunicationObject;
			if (proxy == null)
				new InvalidCastException();

			if (proxy.State == CommunicationState.Faulted)
			{
				proxy.Abort();
            }
            else if (proxy.State == CommunicationState.Opened)
            {
                proxy.Close();
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
		public virtual object CallOnlineProxyMethod(object onlineProxy, Request request, ref Exception ex)
		{
			MethodInfo method = onlineProxy.GetType().GetMethod(request.MethodName);
			return method.Invoke(onlineProxy, request.CallParameters);
		}

		/// <summary>
		/// Gets the <see cref="IEndpointCatalog"/> that contains the address and credentials for communicating to the WCF endpoint.
		/// </summary>
		protected IEndpointCatalog EndpointCatalog
		{
			get { return endpointCatalog ?? RequestManager.Instance.EndpointCatalog; }
		}
	}
}