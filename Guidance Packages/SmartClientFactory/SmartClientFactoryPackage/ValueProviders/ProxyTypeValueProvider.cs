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
using System.Diagnostics;
//using Microsoft.VisualStudio.Shell.Design;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Practices.RecipeFramework.Library;
using System.ComponentModel.Design;
using System.Globalization;
using Microsoft.Practices.SmartClientFactory.Helpers;
using Microsoft.Practices.Common;
using System.Collections.Specialized;
using Microsoft.Practices.RecipeFramework.Extensions.CommonHelpers;

namespace Microsoft.Practices.SmartClientFactory.ValueProviders
{
    public class ProxyTypeValueProvider : ValueProvider, IAttributesConfigurable
	{
        private const string BuiltKey = "Built";

        private string builtExpression;

		public override bool OnBeginRecipe(object currentValue, out object newValue)
		{
			DTE vs = GetService<DTE>(true);

            newValue = null;
            if (vs.SelectedItems.Item(1).Project == null)
            {
                ProjectItem webRef = vs.SelectedItems.Item(1).ProjectItem;
                if (webRef == null) throw new InvalidOperationException(Properties.Resources.NoProxyFound);

                ProjectItem proxyItem = FindProxyClass(webRef.ProjectItems);
                if (proxyItem == null) throw new InvalidOperationException(Properties.Resources.NoProxyFound);
                if (proxyItem.FileCodeModel == null) throw new InvalidOperationException(Properties.Resources.NoProxyFound);

                bool built = true;
                object builtValue = ExpressionEvaluationHelper.EvaluateExpression((IDictionaryService)GetService(typeof(IDictionaryService)),
                                                builtExpression);
                if (builtValue != null)
                { built = (bool)builtValue; }

                if (built)
                {
                    CodeClass proxyTypeCodeClass = CodeModelHelper.FindFirstClass(proxyItem.FileCodeModel.CodeElements, CodeModelHelper.IsProxyClass);
                    newValue = DteConverter.ToType(proxyTypeCodeClass);
                }
            }
			return true;
		}

		private ProjectItem FindProxyClass(ProjectItems projectItems)
		{
			if (projectItems == null) return null;

			foreach (ProjectItem item in projectItems)
			{
				if (item.FileCodeModel != null)
				{
					return item;
				}
				else
				{
					ProjectItem proxyClass = FindProxyClass(item.ProjectItems);
					if (proxyClass != null)
					{
						return proxyClass;
					}
				}
			}

			return null;
		}

        public void Configure(StringDictionary attributes)
        {
            builtExpression = attributes[BuiltKey];
        }
    }
}
