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
using Microsoft.Practices.Common;
using System.ComponentModel.Design;
using EnvDTE;

namespace Microsoft.Practices.SmartClientFactory.ValueProviders
{
	public class ProjectCodeClassFromTypeNameProvider : ValueProvider, IAttributesConfigurable
	{
		private string _typeName;
		private string _projectArgument;

		public override bool OnBeginRecipe(object currentValue, out object newValue)
		{
			newValue = currentValue;

			IDictionaryService dict = GetService<IDictionaryService>();
			Project project = dict.GetValue(_projectArgument) as Project;

			if (project != null)
				newValue = (CodeClass)project.CodeModel.CodeTypeFromFullName(String.Format("{0}.{1}", project.Properties.Item("DefaultNamespace").Value, _typeName));

			return newValue != null;
		}

		public override bool OnBeforeActions(object currentValue, out object newValue)
		{
			return OnBeginRecipe(currentValue, out newValue);
		}

		public void Configure(System.Collections.Specialized.StringDictionary attributes)
		{
			_typeName = attributes["TypeName"];
			_projectArgument = attributes["Project"];
		}
	}
}
