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
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Web.Services.Protocols;
using Microsoft.Practices.SmartClient.DisconnectedAgent.Properties;
using Microsoft.Practices.SmartClient.EndpointCatalog;

namespace Microsoft.Practices.SmartClient.DisconnectedAgent
{
	/// <summary>
	/// An <see cref="IProxyFactory"/> to create proxies to ASMX web services.
	/// It uses the OnlineProxyType property of the Request to know the expected type for the proxy object.
	/// Additionaly it sets the URL and Credentials properties in the proxy object. 
	/// To set these properties, the factory uses the EndpointCatalog to get the right address and credentials
	/// for the current network name and the Request's endpoint.
	/// </summary>
	public class WebServiceProxyFactory : IProxyFactory
	{
		private IEndpointCatalog catalog;

		/// <summary>
		/// Creates a WebServiceProxyFactory object.
		/// </summary>
		public WebServiceProxyFactory()
			: this(RequestManager.Instance.EndpointCatalog)
		{
		}

		/// <summary>
		/// Constructor which sets the IEndpointCatalog to get credentials and address for endpoints.
		/// </summary>
		/// <param name="catalog">
		///		Endpoint catalog containing addresses and credentials for each endpoint and network names.
		/// </param>
		public WebServiceProxyFactory(IEndpointCatalog catalog)
		{
			this.catalog = catalog;
		}

		/// <summary>
		/// This method creates the proxy object and set the URL and Credentials properties.
		/// </summary>
		/// <param name="request">Request to be dispatched.</param>
		/// <param name="networkName">Current network name.</param>
		/// <returns>The proxy object which has been created.</returns>
		public virtual object GetOnlineProxy(Request request, string networkName)
		{
			Guard.ArgumentNotNull(request, "request");
			SoapHttpClientProtocol proxy = (SoapHttpClientProtocol) Activator.CreateInstance(request.OnlineProxyType);
			if ((catalog != null) && (catalog.Count > 0) && (catalog.EndpointExists(request.Endpoint)))
			{
				proxy.Credentials = catalog.GetCredentialForEndpoint(request.Endpoint, networkName);
				proxy.Url = catalog.GetAddressForEndpoint(request.Endpoint, networkName);
			}

			return proxy;
		}

		/// <summary>
        /// Calls the Dispose method of the <see cref="SoapHttpClientProtocol"/> proxy object.
		/// </summary>
        /// <param name="onlineProxy">A reference to a proxy object created by the factory.</param>
        /// <exception cref="InvalidCastException">If onlineProxy can't be casted to SoapHttpClientProtocol</exception>
        public virtual void ReleaseOnlineProxy(object onlineProxy)
		{
			Guard.ArgumentNotNull(onlineProxy, "onlineProxy");
			SoapHttpClientProtocol proxy = onlineProxy as SoapHttpClientProtocol;
			if (proxy == null)
				new InvalidCastException();
			proxy.Dispose();
		}

		/// <summary>
		/// Calls the specified method on the proxy object to fulfill the request.
		/// </summary>
		/// <param name="onlineProxy">A reference to a proxy object created by the factory.</param>
		/// <param name="request">The <see cref="Request"/> with the data to perform the method call.</param>
		/// <param name="ex">A reference to an <see cref="Exception"/> that should be handled by an exception callback.</param>
		/// <returns>The result of the method invocation.</returns>
		public virtual object CallOnlineProxyMethod(object onlineProxy, Request request, ref Exception ex)
		{
			Guard.ArgumentNotNull(onlineProxy, "onlineProxy");
			Guard.ArgumentNotNull(request, "request");

			object result = null;

			try
			{
				MethodInfo method = onlineProxy.GetType().GetMethod(request.MethodName);

				//Set the MessageID soapheader for Idempotency conforming WSAddressing
				PropertyInfo property = onlineProxy.GetType().GetProperty("MessageIDValue");
				if (property != null)
				{
					object reqId = Activator.CreateInstance(property.PropertyType);
					PropertyInfo textProperty = reqId.GetType().GetProperty("Text");
					textProperty.SetValue(reqId, new string[] {"uuid:" + request.Behavior.MessageId.ToString()}, null);
					property.SetValue(onlineProxy, reqId, null);
				}

				result = method.Invoke(onlineProxy, request.CallParameters);
			}
			catch (WebException webException)
			{
				switch (webException.Status)
				{
						//At server side should be the same as a failure
					case WebExceptionStatus.NameResolutionFailure:
					case WebExceptionStatus.ProxyNameResolutionFailure:
					case WebExceptionStatus.ProtocolError:
					case WebExceptionStatus.ServerProtocolViolation:
					case WebExceptionStatus.TrustFailure:
						ex = webException;
						break;

						//Otherwise should be handled out of the dispatcher.
					default:
						throw new OnlineProxyException(
							String.Format(
								CultureInfo.CurrentCulture,
								Resources.ExceptionWebServiceProxyFactory,
								request.MethodName),
							webException);
				}
			}
			return result;
		}
	}
}