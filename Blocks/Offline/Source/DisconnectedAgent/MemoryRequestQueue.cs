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
using System.Globalization;
using Microsoft.Practices.SmartClient.DisconnectedAgent.Properties;

namespace Microsoft.Practices.SmartClient.DisconnectedAgent
{
	/// <summary>
	/// An in-memory implementation of an <see cref="IRequestQueue"/>.
	/// </summary>
	public class MemoryRequestQueue : IRequestQueue
	{
		private List<Request> _queue = new List<Request>();

		/// <summary>
		/// Event fired when a request is added to the queue.
		/// </summary>
		public event EventHandler<RequestEnqueuedEventArgs> RequestEnqueued;

		/// <summary>
		/// Adds a <see cref="Request"/> to the queue.
		/// </summary>
		/// <param name="request"></param>
		public void Enqueue(Request request)
		{
			Guard.ArgumentNotNull(request, "request");
			Guard.ArgumentNotNull(request.Behavior, "request.Behavior");
			Guard.ArgumentNotNull(request.MethodName, "request.MethodName");
			Guard.ArgumentNotNull(request.OnlineProxyType, "request.OnlineProxyType");
			Guard.ArgumentNotNull(request.Endpoint, "request.Endpoint");
			Guard.ArgumentNotNull(request.Behavior.ProxyFactoryType, "request.Behavior.ProxyFactoryType");

			if (GetRequest(request.RequestId) != null)
			{
				throw new RequestManagerException(String.Format(CultureInfo.CurrentCulture,
				                                                Resources.CannotEnqueueDuplicatedRequests,
				                                                request.RequestId.ToString()));
			}

			request.Behavior.QueuedDate = DateTime.Now;

			_queue.Add(request);

			if (RequestEnqueued != null)
			{
				RequestEnqueued(this, new RequestEnqueuedEventArgs(request));
			}
		}

		/// <summary>
		/// Gets the total count of requests in the queue.
		/// </summary>
		/// <returns>Requests count in the queue.</returns>
		public int GetCount()
		{
			return _queue.Count;
		}

		/// <summary>
		/// Gets the next request in the queue.
		/// </summary>
		/// <returns>The next request in the queue.</returns>
		public Request GetNextRequest()
		{
			if (_queue.Count > 0)
			{
				return _queue[0];
			}
			return null;
		}


		/// <summary>
		/// Gets an iterator with the requests that contain the given string in the tag.
		/// </summary>
		/// <param name="tag">String to be searched.</param>
		/// <returns>IEnumerable with the matching requests.</returns>
		public IEnumerable<Request> GetRequests(string tag)
		{
			List<Request> result = _queue.FindAll(delegate(Request match) { return tag.CompareTo(match.Behavior.Tag) == 0; });

			return result.AsReadOnly();
		}

		/// <summary>
		/// Gets an iterator with the requests that have equal or more stamps than the given value.
		/// </summary>
		/// <param name="stampsEqualOrMoreThan">Minimum stamps number for a request to be dispatched.</param>
		/// <returns>IEnumerable with the matching requests.</returns>
		public IEnumerable<Request> GetRequests(int stampsEqualOrMoreThan)
		{
			if (stampsEqualOrMoreThan < 0)
				throw new ArgumentOutOfRangeException("stampsEqualOrMoreThan");
				
			List<Request> result =
				_queue.FindAll(delegate(Request match) { return match.Behavior.Stamps >= stampsEqualOrMoreThan; });

			return result.AsReadOnly();
		}

		/// <summary>
		/// Gets an iterator with all the requests in the queue.
		/// </summary>
		/// <returns>IEnumerable with all the requests.</returns>
		public IEnumerable<Request> GetRequests()
		{
			return _queue.ToArray();
		}

		/// <summary>
		/// Gets a specific request by RequestId.
		/// </summary>
		/// <param name="requestId">Request Id to be searched.</param>
		/// <returns>The matching request or null if it's not found.</returns>
		public Request GetRequest(Guid requestId)
		{
			Request result = _queue.Find(delegate(Request match) { return requestId.CompareTo(match.RequestId) == 0; });

			return result;
		}

		/// <summary>
		/// Removes a request from the queue if it exists.
		/// </summary>
		/// <param name="request">Request to be removed.</param>
		public void Remove(Request request)
		{
			_queue.Remove(request);
		}
	}
}