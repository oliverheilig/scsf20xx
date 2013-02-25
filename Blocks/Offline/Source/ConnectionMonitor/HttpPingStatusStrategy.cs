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

namespace Microsoft.Practices.SmartClient.ConnectionMonitor
{
	/// <summary>
	/// A network detection strategy the sends a HTTP GET verb to the specified host.
	/// </summary>
	public class HttpPingStatusStrategy : INetworkStatusStrategy
	{
		/// <summary>
		/// Informs if the specified host or address is reachable from the client.
		/// </summary>
		/// <param name="hostnameOrAddress">A string that identifies the computer that is the destination to test.</param>
		/// <returns><see langword="true"/> if the destination can be reach; otherwise, <see langword="false"/>.</returns>
		public bool IsAlive(string hostnameOrAddress)
		{
			bool success = false;
			try
			{
				Uri address = new Uri(hostnameOrAddress);
				HttpWebRequest request = (HttpWebRequest) HttpWebRequest.Create(address);
				request.Timeout = 5000;
				using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
				{
					success = DoesResponseStatusCodeIndicateOnlineStatus(response);
				}
			}
			catch (WebException wex)
			{
				success = DoesWebExceptionStatusIndicateOnlineStatus(wex);
			}

			return success;
		}

		private static bool DoesWebExceptionStatusIndicateOnlineStatus(WebException wex)
		{
			bool success;
			switch (wex.Status)
			{
				case WebExceptionStatus.ProtocolError:
					//  The response received from the server was complete but indicated a protocol-level error. For example, an HTTP protocol error such as 401 Access Denied would use this status.  
					{
						HttpWebResponse response = wex.Response as HttpWebResponse;
						success = DoesResponseStatusCodeExplicitlyIndicateOfflineStatus(response);
						break;
					}
				case WebExceptionStatus.CacheEntryNotFound: // The specified cache entry was not found.  
				case WebExceptionStatus.KeepAliveFailure:
					//  The connection for a request that specifies the Keep-alive header was closed unexpectedly.  
				case WebExceptionStatus.MessageLengthLimitExceeded:
					//  A message was received that exceeded the specified limit when sending a request or receiving a response from the server.  
				case WebExceptionStatus.Pending: //  An internal asynchronous request is pending.  
				case WebExceptionStatus.PipelineFailure:
					//  The request was a piplined request and the connection was closed before the response was received.  
				case WebExceptionStatus.ReceiveFailure: //  A complete response was not received from the remote server.  
				case WebExceptionStatus.RequestProhibitedByCachePolicy:
					//  The request was not permitted by the cache policy. In general, this occurs when a request is not cacheable and the effective policy prohibits sending the request to the server. You might receive this status if a request method implies the presence of a request body, a request method requires direct interaction with the server, or a request contains a conditional header.  
				case WebExceptionStatus.ServerProtocolViolation: //  The server response was not a valid HTTP response.  
				case WebExceptionStatus.Success: //  No error was encountered.  
				case WebExceptionStatus.TrustFailure: //  A server certificate could not be validated.  
					success = true;
					break;
				case WebExceptionStatus.ConnectFailure: // The remote service point could not be contacted at the transport level.  
				case WebExceptionStatus.ConnectionClosed: // The connection was prematurely closed.  
				case WebExceptionStatus.NameResolutionFailure: // The name resolver service could not resolve the host name.  
				case WebExceptionStatus.ProxyNameResolutionFailure:
					// The name resolver service could not resolve the proxy host name.  
				case WebExceptionStatus.RequestCanceled:
					// The request was canceled, the WebRequest.Abort method was called, or an unclassifiable error occurred. This is the default value for Status.  
				case WebExceptionStatus.RequestProhibitedByProxy: // This request was not permitted by the proxy.  
				case WebExceptionStatus.SecureChannelFailure: // An error occurred while establishing a connection using SSL.  
				case WebExceptionStatus.SendFailure: // A complete request could not be sent to the remote server.  
				case WebExceptionStatus.Timeout: // No response was received during the time-out period for a request.  
				case WebExceptionStatus.UnknownError: // An exception of unknown type has occurred.  
				default:
					success = false;
					break;
			}
			return success;
		}

		private static bool DoesResponseStatusCodeExplicitlyIndicateOfflineStatus(HttpWebResponse response)
		{
			bool success;
			switch (response.StatusCode)
			{
				case HttpStatusCode.BadGateway:
					// Equivalent to HTTP status 502. BadGateway indicates that an intermediate proxy server received a bad response from another proxy or the origin server. 
				case HttpStatusCode.BadRequest:
					// Equivalent to HTTP status 400. BadRequest indicates that the request could not be understood by the server. BadRequest is sent when no other error is applicable, or if the exact error is unknown or does not have its own error code. 
				case HttpStatusCode.GatewayTimeout:
					// Equivalent to HTTP status 504. GatewayTimeout indicates that an intermediate proxy server timed out while waiting for a response from another proxy or the origin server. 
				case HttpStatusCode.ProxyAuthenticationRequired:
					// Equivalent to HTTP status 407. ProxyAuthenticationRequired indicates that the requested proxy requires authentication. The Proxy-authenticate header contains the details of how to perform the authentication. 
				case HttpStatusCode.ServiceUnavailable:
					// Equivalent to HTTP status 503. ServiceUnavailable indicates that the server is temporarily unavailable, usually due to high load or maintenance. 
					success = false;
					break;
				default:
					success = true;
					break;
			}
			return success;
		}

		private static bool DoesResponseStatusCodeIndicateOnlineStatus(HttpWebResponse response)
		{
			bool success;
			switch (response.StatusCode)
			{
				case HttpStatusCode.Accepted:
				case HttpStatusCode.Ambiguous:
				case HttpStatusCode.Continue:
				case HttpStatusCode.Created:
				case HttpStatusCode.Forbidden:
				case HttpStatusCode.Found:
				case HttpStatusCode.LengthRequired:
				case HttpStatusCode.MethodNotAllowed:
				case HttpStatusCode.NoContent:
				case HttpStatusCode.NotModified:
				case HttpStatusCode.OK:
				case HttpStatusCode.PartialContent:
				case HttpStatusCode.RedirectKeepVerb:
				case HttpStatusCode.RedirectMethod:
				case HttpStatusCode.ResetContent:
				case HttpStatusCode.Unauthorized:
					{
						success = true;
						break;
					}
				default:
					{
						success = false;
						break;
					}
			}
			return success;
		}
	}
}