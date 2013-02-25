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
	/// Event data associated to the <see cref="IRequestQueue.RequestEnqueued"/> event.
	/// </summary>
	public class RequestEnqueuedEventArgs : EventArgs
	{
		private Request request;

		/// <summary>
		/// Creates a RequestEnqueuedEventArgs object.
		/// </summary>
		/// <param name="request">The <see cref="Request"/> that was added to the queue.</param>
		public RequestEnqueuedEventArgs(Request request)
		{
			this.request = request;
		}

		/// <summary>
		/// The <see cref="Request"/> that was added to the queue.
		/// </summary>
		public Request Request
		{
			get { return request; }
		}
	}
}