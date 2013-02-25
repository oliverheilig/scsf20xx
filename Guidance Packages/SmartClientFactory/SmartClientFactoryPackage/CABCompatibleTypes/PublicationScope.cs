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
namespace Microsoft.Practices.SmartClientFactory.CABCompatibleTypes
{
	/// <summary>
	/// Defines the scope for a publication of an EventTopic.
	/// </summary>
	public enum PublicationScope
	{
		/// <summary>
		/// Indicates that the topic should be fired on all the <see cref="WorkItem"/> instances,
		/// regarding where the publication firing occurred.
		/// </summary>
		Global,
		/// <summary>
		/// Indicates that the topic should be fired only in the <see cref="WorkItem"/> instance where 
		/// the publication firing occurred.
		/// </summary>
		WorkItem,
		/// <summary>
		/// Indicates that the topic should be fired in the <see cref="WorkItem"/> instance where 
		/// the publication firing occurred, and in all the <see cref="WorkItem"/> descendants.
		/// </summary>
		Descendants,
	}
}
