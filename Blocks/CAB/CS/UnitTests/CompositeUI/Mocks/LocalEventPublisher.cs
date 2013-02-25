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
using System.ComponentModel;
using Microsoft.Practices.CompositeUI.EventBroker;

namespace Microsoft.Practices.CompositeUI.Tests.Mocks
{
	[System.ComponentModel.DesignerCategory("Code")]
	public class LocalEventPublisher : Component
	{
		[EventPublication("LocalEvent", PublicationScope.WorkItem)]
		public event EventHandler Event;

		public void FireTheEventHandler()
		{
			if (Event != null)
			{
				Event(this, EventArgs.Empty);
			}
		}

		public bool EventIsNull
		{
			get { return Event == null; }
		}
	}


	public class LocalObjectEventPublisher
	{
		[EventPublicationAttribute("LocalEvent", PublicationScope.WorkItem)]
		public event EventHandler Event;

		public void FireTheEventHandler()
		{
			if (Event != null)
			{
				Event(this, EventArgs.Empty);
			}
		}

		public bool EventIsNull
		{
			get { return Event == null; }
		}
	}
}