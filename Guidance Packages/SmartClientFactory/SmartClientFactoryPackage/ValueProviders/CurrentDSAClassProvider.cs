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
using Microsoft.Practices.ComponentModel;
using EnvDTE;
using System.Collections.Specialized;
using Microsoft.Practices.RecipeFramework.Extensions.CommonHelpers;
using System.ComponentModel.Design;
using Microsoft.Practices.SmartClientFactory.Helpers;
using Microsoft.Practices.Common;

namespace Microsoft.Practices.SmartClientFactory.ValueProviders
{
    [ServiceDependency(typeof(DTE))]
    public class CurrentDSAClassProvider : ValueProvider, IAttributesConfigurable
    {
        private const string CurrentItemKey = "CurrentItem";
        private string currentItemExpression;

        public override bool OnBeginRecipe(object currentValue, out object newValue)
        {
            newValue = null;
            if (currentValue == null)
            {
                ProjectItem currentItem = ExpressionEvaluationHelper.EvaluateExpression((IDictionaryService)GetService(typeof(IDictionaryService)),
                                                currentItemExpression) as ProjectItem;
                if (currentItem != null)
                {
                    CodeClass codeClass;
                    if (CodeModelHelper.HaveAClassInProjectItems(currentItem, out codeClass, CodeModelHelper.IsDSAClass))
                    {
                        newValue = codeClass;
                    }
                    return true;
                }

            }
            return false;
        }


        public void Configure(StringDictionary attributes)
        {
            currentItemExpression = attributes[CurrentItemKey];
        }
    }
}
