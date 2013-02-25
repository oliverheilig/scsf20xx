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

namespace Microsoft.Practices.CompositeUI.EventBroker
{
	/// <summary>
	/// Declares an event as an <see cref="EventTopic"/> publication.
	/// </summary>
	[AttributeUsage(AttributeTargets.Event, AllowMultiple=true)]
	public sealed class EventPublicationAttribute : Attribute
	{
		private string topic;

		private PublicationScope scope;

		/// <summary>
		/// Declares an event publication for an <see cref="EventTopic"/> with the specified topic and 
		/// a <c>Global</c> <see cref="PublicationScope"/>.
		/// </summary>
		/// <param name="topic">The name of the <see cref="EventTopic"/> the decorated event will publish.</param>
		public EventPublicationAttribute(string topic)
			: this(topic, PublicationScope.Global)
		{
		}

		/// <summary>
		/// Declares an event publication for an <see cref="EventTopic"/> with the specified topic and
		/// <see cref="PublicationScope"/>.
		/// </summary>
		/// <param name="topic">The name of the <see cref="EventTopic"/> the event will publish.</param>
		/// <param name="scope">The scope for the publication.</param>
		public EventPublicationAttribute(string topic, PublicationScope scope)
		{
			this.topic = topic;
			this.scope = scope;
		}

		/// <summary>
		/// The name of the <see cref="EventTopic"/> the decorated event will publish.
		/// </summary>
		public string Topic
		{
			get { return topic; }
		}

		/// <summary>
		/// The <see cref="PublicationScope"/> for the publication.
		/// </summary>
		public PublicationScope Scope
		{
			get { return this.scope; }
		}
	}
}