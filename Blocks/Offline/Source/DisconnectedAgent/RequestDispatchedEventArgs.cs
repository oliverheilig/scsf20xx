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
	/// Event arguments used when a <see cref="Request"/> is dispatched.
	/// </summary>
	public class RequestDispatchedEventArgs : EventArgs
	{
		private DispatchResult result;
		private Request request;

		/// <summary>
		/// It creates an RequestDispatchedEventArgs for the given request and result.
		/// It is used by the RequestDispatched event, fired during the dispatch process.
		/// </summary>
		/// <param name="request">Request tried to be dispatched.</param>
		/// <param name="result">Dispatch process result.</param>
		public RequestDispatchedEventArgs(Request request, DispatchResult result)
		{
			this.request = request;
			this.result = result;
		}

		/// <summary>
		/// Request tried to be dispatched.
		/// </summary>
		public Request Request
		{
			get { return request; }
		}

		/// <summary>
		/// Result of the dispatch process.
		/// </summary>
		public DispatchResult Result
		{
			get { return result; }
		}
	}
}