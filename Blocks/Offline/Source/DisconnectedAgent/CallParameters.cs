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
	/// Helper class to build call parameters array.
	/// </summary>
	public static class CallParameters
	{
		/// <summary>
		/// Helper method which builds an object array with a list of arguments.
		/// </summary>
		/// <param name="arguments">List of arguments.</param>
		/// <returns>object array with the list of arguments.</returns>
		public static object[] ToArray(params object[] arguments)
		{
			return arguments;
		}
	}
}