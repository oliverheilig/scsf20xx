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
using Microsoft.Practices.SmartClientFactory.Helpers;

namespace Microsoft.Practices.SmartClientFactory.ValueProviders
{
    [ServiceDependency(typeof(DTE))]
    public class CurrentDSAClassStaticMethodValueProvider : ValueProvider, IAttributesConfigurable
    {
        private const string CurrentDSAClassKey = "CurrentDSAClass";
        private const string MethodNameKey = "MethodName";
        private const string BuiltKey = "Built";

        private string currentDSAClassExpression;
        private string methodNameExpression;
        private string builtExpression;

        public override bool OnBeginRecipe(object currentValue, out object newValue)
        {
            newValue = null;
            if (currentValue == null)
            {
                CodeClass currentDSAClass = ExpressionEvaluationHelper.EvaluateExpression((IDictionaryService)GetService(typeof(IDictionaryService)),
                                                currentDSAClassExpression) as CodeClass;
                string methodName = ExpressionEvaluationHelper.EvaluateExpression((IDictionaryService)GetService(typeof(IDictionaryService)),
                                                methodNameExpression) as string;

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
                        MethodInfo method=dsaType.GetMethod(methodName);
                        if (method != null
                            && method.IsStatic
                            && method.GetParameters().Length==0)
                        {
                            newValue = method.Invoke(null, null);
                        	newValue = MethodBehaviorHelper.TranslateToOfflineBehavior(newValue);
                        }
                    }
                }

            }
            return currentValue!=newValue;
        }


        public void Configure(StringDictionary attributes)
        {
            currentDSAClassExpression = attributes[CurrentDSAClassKey];
            methodNameExpression = attributes[MethodNameKey];
            builtExpression = attributes[BuiltKey];
        }
    }
}
