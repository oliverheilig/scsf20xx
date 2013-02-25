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
using Microsoft.Practices.Common;
using Microsoft.VisualStudio.Shell.Interop;
using System.ComponentModel.Design;
using System.Collections.Specialized;
using Microsoft.Practices.SmartClientFactory.Actions;

namespace Microsoft.Practices.SmartClientFactory.ValueProviders
{
	public class GetProjectFromGuidProvider : ValueProvider, IAttributesConfigurable
	{
		private string _fromArgument;

		public override bool OnBeforeActions(object currentValue, out object newValue)
		{
			return OnBeginRecipe(currentValue, out newValue);
		}

		public override bool OnBeginRecipe(object currentValue, out object newValue)
		{
			newValue = currentValue;
			if (newValue == null)
			{
				IDictionaryService dict = GetService<IDictionaryService>();
				object value = dict.GetValue(_fromArgument);
				Guid guid = Guid.Empty;
				if ( value is string )
				{
					guid = new Guid(value as String);
				}
				else
				{
					guid = (Guid)value;
				}
				newValue = Utility.GetProjectFromGuid(GetService<DTE>(), GetService<IServiceProvider>(), guid);
			}
			return newValue  != null;
		}

		public void Configure(StringDictionary attributes)
		{
			_fromArgument = attributes["FromArgument"];
		}
	}
}
