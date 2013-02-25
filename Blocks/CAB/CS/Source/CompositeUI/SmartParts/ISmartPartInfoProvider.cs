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
//===============================================================================
// Microsoft patterns & practices
// CompositeUI Application Block
//===============================================================================
// Copyright � Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;

namespace Microsoft.Practices.CompositeUI.SmartParts
{
	/// <summary>
	/// Interface to be implemented by smart parts that contain their own 
	/// <see cref="ISmartPartInfo"/> components.
	/// </summary>
	public interface ISmartPartInfoProvider
	{
		/// <summary>
		/// Tries to retrieve smart part information compatible with type 
		/// smartPartInfoType.
		/// </summary>
		/// <param name="smartPartInfoType">Type of information to retrieve.</param>
		/// <returns>The <see cref="ISmartPartInfo"/> instance or null if none exists in the smart part.</returns>
		ISmartPartInfo GetSmartPartInfo(Type smartPartInfoType);
	}
}
