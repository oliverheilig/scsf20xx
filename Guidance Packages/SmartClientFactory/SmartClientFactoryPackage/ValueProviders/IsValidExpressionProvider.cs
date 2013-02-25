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
using Microsoft.Practices.RecipeFramework.Library;
using System.ComponentModel.Design;
using Microsoft.Practices.Common;
using Microsoft.Practices.ComponentModel;

namespace Microsoft.Practices.SmartClientFactory.ValueProviders
{
    public class IsValidExpressionProvider : ValueProvider, IAttributesConfigurable
    {
        public const string ValueExpressionAttribute = "ValueExpression";

        private string _valueExpression;

        public override bool OnBeginRecipe(object currentValue, out object newValue)
        {
            if (string.IsNullOrEmpty(currentValue as string))
            {
                bool expressionIsValid = Evaluate();
                newValue = expressionIsValid;
                return true;                
            }
            newValue = null;
            return false;
        }

        private bool Evaluate()
        {
            ExpressionEvaluationService evaluator = new ExpressionEvaluationService();
            IDictionaryService dictservice = (IDictionaryService)GetService(typeof(IDictionaryService));

            object value = null;

            try
            {
                value = evaluator.Evaluate(
                                            this._valueExpression,
                                            new ServiceAdapterDictionary(dictservice)).ToString();
            }
            catch { }

            bool expressionIsValid = (value != null);
            return expressionIsValid;
        }

        public override bool OnArgumentChanged(string changedArgumentName, object changedArgumentValue, object currentValue, out object newValue)
        {
            bool expressionIsValid = Evaluate();
            newValue = expressionIsValid;
            return true;
        }

        public void Configure(System.Collections.Specialized.StringDictionary attributes)
        {
            if (attributes.ContainsKey(ValueExpressionAttribute))
            {
                _valueExpression = attributes[ValueExpressionAttribute];
            }
        }
    }
}
