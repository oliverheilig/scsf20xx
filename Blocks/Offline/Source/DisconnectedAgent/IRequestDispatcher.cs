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
namespace Microsoft.Practices.SmartClient.DisconnectedAgent
{
	/// <summary>
	/// Interface describing the contract of a request dispatcher. 
	/// </summary>
	public interface IRequestDispatcher
	{
		/// <summary>
		/// This method's implementations should dispatch a request for the given network name.
		/// </summary>
		/// <param name="request">Request to be dispatched.</param>
		/// <param name="networkName">Current network name.</param>
		/// <returns>DispatchResult with the corresponding result of the dispatching process.</returns>
		DispatchResult Dispatch(Request request, string networkName);
	}
}