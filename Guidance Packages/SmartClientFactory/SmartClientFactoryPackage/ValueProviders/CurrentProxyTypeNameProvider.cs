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
using System.Text.RegularExpressions;

namespace Microsoft.Practices.SmartClientFactory.ValueProviders
{
    [ServiceDependency(typeof(DTE))]
    public class CurrentProxyTypeNameProvider : ValueProvider, IAttributesConfigurable
    {
        private const string CurrentDSAClassKey = "CurrentDSAClass";

        private string currentDSAClassExpression;

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
            CodeClass currentDSAClass = ExpressionEvaluationHelper.EvaluateExpression((IDictionaryService)GetService(typeof(IDictionaryService)),
                                            currentDSAClassExpression) as CodeClass;
            if (currentDSAClass != null)
            {
                CodeProperty onlineProxyTypeProperty = CodeModelHelper.GetProperty(currentDSAClass, "OnlineProxyType", vsCMAccess.vsCMAccessPublic);
                if (onlineProxyTypeProperty != null)
                {
                    CodeFunction getMethod = onlineProxyTypeProperty.Getter;
                    EditPoint edit = getMethod.StartPoint.CreateEditPoint();
                    EditPoint endpoint=null;
                    TextRanges tags=null;
                    if (edit.FindPattern(@"(typeof|GetType)\((.+)\)", (int)vsFindOptions.vsFindOptionsRegularExpression, ref endpoint, ref tags))
                    {
                        EditPoint begin= edit.CreateEditPoint();
                        string proxyTypeName = begin.GetText(endpoint);
                        Regex extractRegex = new Regex(@"(typeof|GetType)\((.+)\)");
                        if (extractRegex.IsMatch(proxyTypeName) && extractRegex.Match(proxyTypeName).Groups.Count == 3)
                        {
                            proxyTypeName = extractRegex.Match(proxyTypeName).Groups[2].Value;
                            newValue = proxyTypeName;
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public void Configure(StringDictionary attributes)
        {
            currentDSAClassExpression = attributes[CurrentDSAClassKey];
        }
    }
}
