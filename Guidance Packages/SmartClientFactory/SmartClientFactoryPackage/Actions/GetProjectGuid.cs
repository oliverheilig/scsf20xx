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
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.RecipeFramework;
using EnvDTE;

namespace Microsoft.Practices.SmartClientFactory.Actions
{
	public class GetProjectGuid : ConfigurableAction
	{
		private Project _project;
		private Guid _projectGuid;

		[Input]
		public Project Project
		{
			get { return _project; }
			set { _project = value; }
		}

		[Output]
		public Guid Guid
		{
			get { return _projectGuid; }
			set { _projectGuid = value; }
		}

		public override void Execute()
		{
			Guid = Utility.GetProjectGuid(GetService<IServiceProvider>(), Project);
		}

		public override void Undo()
		{
			throw new Exception("The method or operation is not implemented.");
		}
	}
}
