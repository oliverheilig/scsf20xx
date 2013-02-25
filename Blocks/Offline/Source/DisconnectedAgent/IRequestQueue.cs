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

namespace Microsoft.Practices.SmartClient.DisconnectedAgent
{
	/// <summary>
	/// Interfacace describing the contract a request queue must implement.
	/// </summary>
	public interface IRequestQueue
	{
		/// <summary>
		/// Event fired when a request is enqueued in the queue.
		/// </summary>
		event EventHandler<RequestEnqueuedEventArgs> RequestEnqueued;

		/// <summary>
		/// Enqueue a request.
		/// </summary>
		/// <param name="request">Request to be enqueued.</param>
		void Enqueue(Request request);

		/// <summary>
		/// Gets the total count of requests in the queue.
		/// </summary>
		/// <returns>Requests count in the queue.</returns>
		int GetCount();

		/// <summary>
		/// Gets the next request in the queue.
		/// </summary>
		/// <returns>The next request in the queue.</returns>
		Request GetNextRequest();

		/// <summary>
		/// Gets an iterator with the requests that contain the given string in the tag.
		/// </summary>
		/// <param name="tag">String to be searched.</param>
		/// <returns>IEnumerable with the matching requests.</returns>
		IEnumerable<Request> GetRequests(string tag);

		/// <summary>
		/// Gets an iterator with the requests that have equal or more stamps than the given value.
		/// </summary>
		/// <param name="stampsEqualOrMoreThan">Minimum stamps number for a request to be dispatched.</param>
		/// <returns>IEnumerable with the matching requests.</returns>
		IEnumerable<Request> GetRequests(int stampsEqualOrMoreThan);

		/// <summary>
		/// Gets an iterator with all the requests in the queue.
		/// </summary>
		/// <returns>IEnumerable with all the requests.</returns>
		IEnumerable<Request> GetRequests();

		/// <summary>
		/// Gets a specific request by RequestId.
		/// </summary>
		/// <param name="requestId">Request Id to be searched.</param>
		/// <returns>The matching request or null if it's not found.</returns>
		Request GetRequest(Guid requestId);

		/// <summary>
		/// Removes a request from the queue if it exists.
		/// </summary>
		/// <param name="request">Request to be removed.</param>
		void Remove(Request request);
	}
}