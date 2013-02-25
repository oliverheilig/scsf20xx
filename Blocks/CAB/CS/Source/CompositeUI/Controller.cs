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

using Microsoft.Practices.ObjectBuilder;

namespace Microsoft.Practices.CompositeUI
{
	/// <summary>
	/// A base class for controllers and presenters that provides access to the <see cref="WorkItem"/>
	/// and <see cref="State"/>.
	/// </summary>
	public class Controller
	{
		private WorkItem workItem;

		/// <summary>
		/// Initializes a new instance of the <see cref="Controller"/> class.
		/// </summary>
		public Controller()
		{
		}

		/// <summary>
		/// Gets the current work item where the controller lives.
		/// </summary>
		[Dependency(NotPresentBehavior = NotPresentBehavior.ReturnNull)]
		public WorkItem WorkItem
		{
			get { return workItem; }
			set { workItem = value; }
		}

		/// <summary>
		/// Gets the state associated with the current <see cref="WorkItem"/>.
		/// </summary>
		public State State
		{
			get
			{
				return WorkItem == null ? null : WorkItem.State;
			}
		}
	}
}