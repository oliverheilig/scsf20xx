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
using Microsoft.Practices.RecipeFramework.Library;
using System.ComponentModel.Design;
using System.Collections.Specialized;
using Microsoft.Practices.ComponentModel;

namespace Microsoft.Practices.SmartClientFactory.ValueProviders
{
    public class ProxyFactoryValueProvider : ValueProvider, IAttributesConfigurable
    {
        private const string TypeExpressionKey = "TypeExpression";
        private const string LanguageExpressionKey = "LanguageExpression";

        private string _typeExpression;
        private string _languageExpression;

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

            ExpressionEvaluationService evaluator = new ExpressionEvaluationService();
            IDictionaryService dictservice = (IDictionaryService)GetService(typeof(IDictionaryService));

            Type proxyType = null;
            string language=string.Empty;

            try
            {
                proxyType = (Type)evaluator.Evaluate(
                                            this._typeExpression,
                                            new ServiceAdapterDictionary(dictservice));
                language = (string)evaluator.Evaluate(
                                            this._languageExpression,
                                            new ServiceAdapterDictionary(dictservice));
            }
            catch { }

            newValue = ProxyFactoryHelper.GetProxyFactory(proxyType,language);

            return newValue != null;
        }

        public void Configure(StringDictionary attributes)
        {
            _typeExpression = attributes[TypeExpressionKey];
            _languageExpression = attributes[LanguageExpressionKey];
        }
        public override bool OnArgumentChanged(string changedArgumentName, object changedArgumentValue, object currentValue, out object newValue)
        {
            Evaluate(out newValue);
            return true;
        }
    }
}
