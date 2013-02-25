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

namespace Microsoft.Practices.SmartClientFactory.ValueProviders
{
    [ServiceDependency(typeof(DTE))]
    public class CurrentClassProvider : ValueProvider
    {
        internal static CodeClass GetCurrentClass(ProjectItem prItem)
        {
            foreach (CodeElement element in prItem.FileCodeModel.CodeElements)
            {
                if (element is CodeNamespace)
                {
                    CodeNamespace namespace2 = (CodeNamespace)element;
                    if ((namespace2.Members.Count > 0) && (namespace2.Members.Item(1) is CodeClass))
                    {
                        return (CodeClass)namespace2.Members.Item(1);
                    }
                }
                else if (element is CodeClass)                                                                                               
                {
                    return (CodeClass)element;
                }
            }
            return null;
        }

        public override bool OnBeginRecipe(object currentValue, out object newValue)
        {
            if (currentValue == null)
            {
                DTE service = (DTE)this.GetService(typeof(DTE));
                if (service.SelectedItems.Count == 1)
                {
                    ProjectItem prItem = service.SelectedItems.Item(1).ProjectItem;
                    if (prItem != null)
                    {
                        newValue = GetCurrentClass(prItem);
                        if (newValue != null)
                        {
                            return true;
                        }
                    }
                }
            }
            newValue = currentValue;
            return false;
        }
    }
}
