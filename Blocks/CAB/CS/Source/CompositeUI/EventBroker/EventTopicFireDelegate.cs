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
using System.Collections.Generic;
using System.Diagnostics;
namespace Microsoft.Practices.CompositeUI.EventBroker
{
	/// <summary>
	/// Represents the signature for the subscription objects to get called from the <see cref="EventTopic"/> during
	/// a firing sequence.
	/// </summary>
	/// <param name="sender">The publisher object firing the topic.</param>
	/// <param name="e">The <see cref="EventArgs"/> data to be passed to the subscribers.</param>
	/// <param name="exceptions">An <see cref="Exception"/> list where a <see cref="Subscription"/> should 
	/// register the exceptions that might occur when executing the subcription code.</param>
	/// <param name="traceSource">The <see cref="TraceSource"/> to use for reporting information.</param>
	internal delegate void EventTopicFireDelegate(object sender, EventArgs e, 
		List<Exception> exceptions, TraceSource traceSource);
}