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

using System.Configuration;

namespace Microsoft.Practices.CompositeUI.Configuration
{
	/// <summary>
	/// Definition of the configuration section for the block.
	/// </summary>
	public class SettingsSection : ConfigurationSection
	{
		/// <summary>
		/// The configuration section name for this section.
		/// </summary>
		public const string SectionName = "CompositeUI";

		/// <summary>
		/// List of startup services that will be initialized on the host.
		/// </summary>
		[ConfigurationProperty("services", IsRequired = true)]
		public ServiceElementCollection Services
		{
			get { return (ServiceElementCollection) this["services"]; }
		}

		/// <summary>
		/// Optional visualizer.
		/// </summary>
		[ConfigurationProperty("visualizer", IsRequired = false)]
		public VisualizationElementCollection Visualizer
		{
			get { return (VisualizationElementCollection) this["visualizer"]; }
		}
	}
}