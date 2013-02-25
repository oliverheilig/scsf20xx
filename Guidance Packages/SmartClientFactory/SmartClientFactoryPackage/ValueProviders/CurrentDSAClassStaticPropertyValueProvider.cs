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
using System.Reflection;
using Microsoft.Practices.Common;

namespace Microsoft.Practices.SmartClientFactory.ValueProviders
{
    [ServiceDependency(typeof(DTE))]
    public class CurrentDSAClassStaticPropertyValueProvider : ValueProvider, IAttributesConfigurable
    {
        private const string CurrentDSAClassKey = "CurrentDSAClass";
        private const string PropertyNameKey = "PropertyName";
        private const string BuiltKey = "Built";

        private string currentDSAClassExpression;
        private string propertyNameExpression;
        private string builtExpression;

        public override bool OnBeginRecipe(object currentValue, out object newValue)
        {
            newValue = null;
            if (currentValue == null)
            {
                CodeClass currentDSAClass = ExpressionEvaluationHelper.EvaluateExpression((IDictionaryService)GetService(typeof(IDictionaryService)),
                                                currentDSAClassExpression) as CodeClass;
                string propertyName = ExpressionEvaluationHelper.EvaluateExpression((IDictionaryService)GetService(typeof(IDictionaryService)),
                                                propertyNameExpression) as string;

                bool built = true;
                object builtValue = ExpressionEvaluationHelper.EvaluateExpression((IDictionaryService)GetService(typeof(IDictionaryService)),
                                                builtExpression);
                if (builtValue != null)
                { built = (bool)builtValue; }

                if (currentDSAClass != null && built)
                {
                    Type dsaType = DteConverter.ToType(currentDSAClass);
                    if (dsaType != null)
                    {
                        newValue = dsaType.InvokeMember(propertyName, BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty, null, null, null);
                        if (newValue != null)
                        {
                            return true;
                        }
                    }
                }

            }
            return false;
        }

        public override bool OnArgumentChanged(string changedArgumentName, object changedArgumentValue, object currentValue, out object newValue)
        {
            return base.OnArgumentChanged(changedArgumentName, changedArgumentValue, currentValue, out newValue);
        }

        public void Configure(StringDictionary attributes)
        {
            currentDSAClassExpression = attributes[CurrentDSAClassKey];
            propertyNameExpression = attributes[PropertyNameKey];
            builtExpression = attributes[BuiltKey];
        }
    }
}
