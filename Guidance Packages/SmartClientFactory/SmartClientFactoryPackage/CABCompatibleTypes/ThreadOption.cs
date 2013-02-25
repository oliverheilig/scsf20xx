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
using System.Threading;

namespace Microsoft.Practices.SmartClientFactory.CABCompatibleTypes
{
	/// <summary>
	/// Specifies on which <see cref="Thread"/> an EventTopic subscriber will be called.
	/// </summary>
	public enum ThreadOption
	{
		/// <summary>
		/// The call is done on the same <see cref="Thread"/> on which the EventTopic was fired.
		/// </summary>
		Publisher,
		/// <summary>
		/// The call is done asynchronously on a background <see cref="Thread"/>.
		/// </summary>
		Background,
		/// <summary>
		/// The call is done is done on the UI <see cref="Thread"/>.
		/// </summary>
		UserInterface,
	}
}
