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
using Microsoft.Practices.Common;
using Microsoft.Practices.RecipeFramework.Extensions.CommonHelpers;
using System.ComponentModel.Design;
using EnvDTE;
using System.Collections.Specialized;
using Microsoft.Practices.ComponentModel;
using Microsoft.Practices.RecipeFramework;

namespace Microsoft.Practices.SmartClientFactory.ValueProviders
{
    [ServiceDependency(typeof(DTE))]
    public class ProjectItemByNameExpressionProvider : ValueProvider, IAttributesConfigurable
    {
        private string itemNameExpression;
        private string projectExpression;

        private bool Evaluate(out object newValue)
        {
            DTE dte = (DTE)GetService(typeof(DTE));
            IDictionaryService dictservice = (IDictionaryService)GetService(typeof(IDictionaryService));
            string itemName = ExpressionEvaluationHelper.EvaluateExpression(dictservice, this.itemNameExpression) as string;
            Project project = ExpressionEvaluationHelper.EvaluateExpression(dictservice, this.projectExpression) as Project;

            newValue = DteHelperEx.FindItemByName(project.ProjectItems, itemName, true);
            if (newValue != null)
            {
                return true;
            }
            return false;
        }
        public override bool OnBeginRecipe(object currentValue, out object newValue)
        {
            newValue = null;
            if (currentValue == null)
            {
                return Evaluate(out newValue);
            }
            return false;
        }

        public override bool OnBeforeActions(object currentValue, out object newValue)
        {
            return OnBeginRecipe(currentValue, out newValue);
        }

        void IAttributesConfigurable.Configure(StringDictionary attributes)
        {
            itemNameExpression = attributes["ItemName"];
            projectExpression = attributes["Project"];
        }

    }
}