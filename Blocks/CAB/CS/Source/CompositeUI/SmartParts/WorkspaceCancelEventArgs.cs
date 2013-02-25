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

namespace Microsoft.Practices.CompositeUI.SmartParts
{
	/// <summary>
	/// Provides data for the cancellable events exposed by 
	/// <see cref="IWorkspace"/>.
	/// </summary>
	public class WorkspaceCancelEventArgs : WorkspaceEventArgs
	{
		private bool cancel;

		/// <summary>
		/// Initializes a new <see cref="WorkspaceCancelEventArgs"/> using
		/// the specified SmartPart.
		/// </summary>
		/// <param name="smartPart"></param>
		public WorkspaceCancelEventArgs(object smartPart)
			: base(smartPart)
		{
		}

		/// <summary>
		/// Gets or sets a value indicating whether the event should be canceled.
		/// </summary>
		public bool Cancel
		{
			get { return cancel; }
			set { cancel = value; }
		}
	}
}