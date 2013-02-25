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
using Microsoft.Practices.ComponentModel;
using EnvDTE;
using Microsoft.Practices.RecipeFramework.Extensions.CommonHelpers;
using System.ComponentModel.Design;

namespace Microsoft.Practices.SmartClientFactory.ValueProviders
{
    [ServiceDependency(typeof(DTE))]
    public class ExistsClassInProject : ValueProvider, IAttributesConfigurable
    {
        private const string TypeNameKey = "TypeName";
        private const string ProjectKey = "Project";

        private string TypeNameExpression;
        private string ProjectExpression;

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
            string typeName = ExpressionEvaluationHelper.EvaluateExpression((IDictionaryService)GetService(typeof(IDictionaryService)),
                                            TypeNameExpression) as string;
            Project project = ExpressionEvaluationHelper.EvaluateExpression((IDictionaryService)GetService(typeof(IDictionaryService)),
                                            ProjectExpression) as Project;

            if (typeName != null && project != null)
            {
                CodeClass targetClass = FileCodeModelHelper.FindCodeElementFromType(project, typeName, vsCMElement.vsCMElementClass) as CodeClass;
                newValue = (targetClass != null);
                return true;
            }

            return false;
        }

        public void Configure(System.Collections.Specialized.StringDictionary attributes)
        {
            TypeNameExpression = attributes[TypeNameKey];
            ProjectExpression = attributes[ProjectKey];
        }
    }
}
