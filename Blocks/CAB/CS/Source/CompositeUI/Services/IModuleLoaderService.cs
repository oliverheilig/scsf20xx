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
using System.Reflection;
using Microsoft.Practices.CompositeUI.Configuration;
using Microsoft.Practices.CompositeUI.Utility;

namespace Microsoft.Practices.CompositeUI.Services
{
	/// <summary>
	/// Service that performs module loading.
	/// </summary>
	public interface IModuleLoaderService
	{
		/// <summary>
		/// Returns a list of the loaded modules.
		/// </summary>
		IList<LoadedModuleInfo> LoadedModules { get; }

		/// <summary>
		/// Loads the specified list of modules.
		/// </summary>
		/// <param name="workItem">The <see cref="WorkItem"/> that will host the modules.</param>
		/// <param name="modules">The list of modules to load.</param>
		void Load(WorkItem workItem, params IModuleInfo[] modules);

		/// <summary>
		/// Loads assemblies as modules.
		/// </summary>
		/// <param name="workItem">The <see cref="WorkItem"/> that will host the modules.</param>
		/// <param name="assemblies">The list of assemblies to load as modules.</param>
		void Load(WorkItem workItem, params Assembly[] assemblies);

		/// <summary>
		/// The event that is fired when a module has been loaded by the service.
		/// </summary>
		event EventHandler<DataEventArgs<LoadedModuleInfo>> ModuleLoaded;
	}
}