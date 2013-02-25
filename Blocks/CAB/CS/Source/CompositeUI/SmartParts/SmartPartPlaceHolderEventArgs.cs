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
using Microsoft.Practices.CompositeUI.Utility;

namespace Microsoft.Practices.CompositeUI.SmartParts
{
	/// <summary>
	/// Used by the the smartpart placeholder.
	/// </summary>
	public class SmartPartPlaceHolderEventArgs : EventArgs
	{
		private object smartPart;

		/// <summary>
		/// Initializes a new instance of the <see cref="SmartPartPlaceHolderEventArgs"/> using
		/// the specified SmartPart.
		/// </summary>
		/// <param name="smartPart"></param>
		public SmartPartPlaceHolderEventArgs(object smartPart)
		{
			Guard.ArgumentNotNull(smartPart, "smartPart");
			this.smartPart = smartPart;
		}

		/// <summary>
		/// Gets the SmartPart associated with this event.
		/// </summary>
		public object SmartPart
		{
			get { return smartPart; }
		}
	}
}