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
using Microsoft.Practices.RecipeFramework;
using System.Collections.Specialized;
using Microsoft.Practices.ComponentModel;
using Microsoft.Practices.RecipeFramework.Extensions.CommonHelpers;
using System.ComponentModel.Design;
using EnvDTE;
using Microsoft.Practices.SmartClientFactory.Helpers;

namespace Microsoft.Practices.SmartClientFactory.ValueProviders
{
    [ServiceDependency(typeof(DTE))]
    public class CurrentDSAClassProxyTypeMethods : ValueProvider, IAttributesConfigurable
    {
        private const string CurrentDSAClassKey = "CurrentDSAClass";
        private const string ProxyTypeKey = "ProxyType";

        private string currentDSAClassExpression;
        private string proxyTypeExpression;

        public override bool OnBeginRecipe(object currentValue, out object newValue)
        {
            newValue = null;
            if (currentValue == null)
            {
                CodeClass currentDSAClass = ExpressionEvaluationHelper.EvaluateExpression((IDictionaryService)GetService(typeof(IDictionaryService)),
                                                currentDSAClassExpression) as CodeClass;
                Type proxyType = ExpressionEvaluationHelper.EvaluateExpression((IDictionaryService)GetService(typeof(IDictionaryService)),
                                                proxyTypeExpression) as Type;

                if (currentDSAClass != null && proxyType != null)
                {
                    Type dsaType = DteConverter.ToType(currentDSAClass);
                    if (dsaType != null)
                    {
                        newValue = MethodBehaviorHelper.GetMethodNames( MethodBehaviorHelper.GetAgentMethods(dsaType, proxyType));
                    }
                }

            }
            return currentValue != newValue;
        }

        public void Configure(StringDictionary attributes)
        {
            currentDSAClassExpression = attributes[CurrentDSAClassKey];
            proxyTypeExpression = attributes[ProxyTypeKey];
        }
    }
}
