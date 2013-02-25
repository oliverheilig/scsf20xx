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
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Practices.RecipeFramework.Library;
using System.ComponentModel.Design;
using System.Globalization;
using Microsoft.Practices.Common;
using System.Collections.Specialized;
using System.Web.Services.Description;
using System.Reflection;

namespace Microsoft.Practices.SmartClientFactory.ValueProviders
{
    public class TypeMethodsValueProvider : ValueProvider, IAttributesConfigurable
	{
        private const string CurrentTypeKey = "CurrentType";

        private string CurrentTypeArgument;
                
		public override bool OnBeginRecipe(object currentValue, out object newValue)
		{
            if (currentValue == null)
            {
                return Evaluate(out newValue);
            }
            newValue = currentValue;
            return false;

		}

        private bool Evaluate(out object newValue)
        {
            newValue = null;

            IDictionaryService dict = GetService<IDictionaryService>();
            Type currentType = (Type)dict.GetValue(CurrentTypeArgument);

            if (currentType != null)
            {
                List<MethodInfo> methods = new List<MethodInfo>(currentType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly));
                methods.RemoveAll(delegate(MethodInfo innerMethod)
                                        {
                                            return innerMethod.IsSpecialName;
                                        });

                newValue = methods;
            }
            else
            {
                newValue = null;
            }

            return newValue != null;
        }

        public void Configure(StringDictionary attributes)
        {
            CurrentTypeArgument = attributes[CurrentTypeKey];
        }
        public override bool OnArgumentChanged(string changedArgumentName, object changedArgumentValue, object currentValue, out object newValue)
        {
            Evaluate(out newValue);
            return true;
        }
    }
}