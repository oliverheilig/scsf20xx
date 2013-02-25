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
    public class ProjectByNameExpressionProvider : ValueProvider, IAttributesConfigurable
    {
        private string nameExpression;

        #region Overrides

        /// <summary>
        /// Uses <see cref="DteHelper.FindProjectByPath"/> to search for the project specified by the "Path" attributte
        /// </summary>
        /// <param name="currentValue"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        /// <seealso cref="ValueProvider.OnBeginRecipe"/>
        /// <seealso cref="DteHelper.FindProjectByPath"/>
        public override bool OnBeginRecipe(object currentValue, out object newValue)
        {
            DTE dte = (DTE)GetService(typeof(DTE));
            IDictionaryService dictservice = (IDictionaryService)GetService(typeof(IDictionaryService));

            if (currentValue == null)
            {
                string projectName = ExpressionEvaluationHelper.EvaluateExpression(dictservice, this.nameExpression) as string;

                newValue = DteHelperEx.FindProjectByName(dte,  projectName, false);
                if (newValue != null)
                {
                    return true;
                }
            }
            newValue = currentValue;
            return false;
        }

        /// <param name="currentValue"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        /// <seealso cref="ValueProvider.OnBeforeActions"/>
        /// <seealso cref="OnBeginRecipe"/>
        public override bool OnBeforeActions(object currentValue, out object newValue)
        {
            return OnBeginRecipe(currentValue, out newValue);
        }

        #endregion

        #region IAttributesConfigurable Members

        void IAttributesConfigurable.Configure(StringDictionary attributes)
        {
            nameExpression = attributes["NameExpression"];
        }

    #endregion
    }
}