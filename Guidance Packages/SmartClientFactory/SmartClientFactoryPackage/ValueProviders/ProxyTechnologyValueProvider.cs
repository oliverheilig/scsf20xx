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
using System.Collections.Specialized;
using Microsoft.Practices.RecipeFramework;
using Microsoft.Practices.RecipeFramework.Extensions.CommonHelpers;
using System.ComponentModel.Design;
using System.Web.Services.Protocols;

namespace Microsoft.Practices.SmartClientFactory.ValueProviders
{

    public class ProxyTechnologyValueProvider : ValueProvider, IAttributesConfigurable
    {
        private const string ProxyTypeKey = "ProxyType";
        private string _proxyType;

        public override bool OnBeginRecipe(object currentValue, out object newValue)
        {
            newValue = null;
            if (currentValue == null)
            {
                return Evaluate(out newValue);
            }
            return false;
        }

        private bool Evaluate(out object newValue)
        {
            newValue = null;
            Type proxyType = ExpressionEvaluationHelper.EvaluateExpression((IDictionaryService)GetService(typeof(IDictionaryService)),
                                                _proxyType) as Type;
            if (proxyType != null)
            {
                newValue = ProxyFactoryHelper.GetProxyTechnology(proxyType);
                return true;
            }
            return false;
        }
        public void Configure(StringDictionary attributes)
        {
            _proxyType = attributes[ProxyTypeKey];
        }

        public override bool OnArgumentChanged(string changedArgumentName, object changedArgumentValue, object currentValue, out object newValue)
        {
            Evaluate(out newValue);
            return true;
        }
    }
}
