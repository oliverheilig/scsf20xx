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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Practices.CompositeUI.Configuration;

namespace GlobalBank.Infrastructure.Library.Services
{
	public class DependentModuleInfo : ModuleInfo, IDependentModuleInfo
	{
		string _name = null;
		List<string> _dependencies = new List<string>();

		public DependentModuleInfo()
		{
		}

		public DependentModuleInfo(string assemblyFilename) : base(assemblyFilename)
		{
		}

		public List<string> Dependencies
		{
			get { return _dependencies; }
		}

		ReadOnlyCollection<string> IDependentModuleInfo.Dependencies
		{
			get { return Dependencies.AsReadOnly(); }
		}

		public string Name
		{
			get { return _name; }
		}

		public void SetName(string name)
		{
			_name = name;
		}
	}
}