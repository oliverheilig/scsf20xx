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
using EnvDTE;
using System.Windows.Forms.Design;
using Microsoft.Practices.SmartClientFactory.Properties;
using Microsoft.Practices.Common;
using System.Globalization;
using System.Collections.Specialized;
using Microsoft.Practices.RecipeFramework.Extensions.CommonHelpers;
using System.ComponentModel.Design;

namespace Microsoft.Practices.SmartClientFactory.ValueProviders
{
    public class BuiltSolutionValueProvider : ValueProvider, IAttributesConfigurable
    {
        private string _throwExceptionExpression;
        private string _showErrorMessageExpression;

        public override bool OnBeginRecipe(object currentValue, out object newValue)
        {
            DTE vs = GetService<DTE>(true);
            vs.Solution.SolutionBuild.Build(true);

            if (vs.Solution.SolutionBuild.LastBuildInfo == 0)
            {
                newValue = true;
            }
            else
            {
                bool throwException = false;
                bool showErrorMessage = false;

                object throwExceptionValue = ExpressionEvaluationHelper.EvaluateExpression((IDictionaryService)GetService(typeof(IDictionaryService)),
                                _throwExceptionExpression);
                object showErrorMessageValue = ExpressionEvaluationHelper.EvaluateExpression((IDictionaryService)GetService(typeof(IDictionaryService)),
                                _showErrorMessageExpression);
                
                if (throwExceptionValue != null)
                {
                    throwException = (bool)throwExceptionValue;
                }
                if (showErrorMessageValue != null)
                {
                    showErrorMessage = (bool)showErrorMessageValue;
                }

                if (showErrorMessage)
                {
                    DisplayErrorMessage(Resources.SolutionBuildFailed);
                }

                if (throwException)
                {
                    throw new InvalidOperationException(String.Format(
                        CultureInfo.CurrentCulture,
                        Resources.SolutionMustBeBuildToRunTheRecipe));
                }

                newValue = false;
            }

            return newValue != currentValue;
        }

        private void DisplayErrorMessage(string message)
        {
            IUIService UIService = GetService<IUIService>();
            if (UIService != null)
            {
                UIService.ShowError(message);
            }
        }

        public void Configure(StringDictionary attributes)
        {
            _throwExceptionExpression = attributes["ThrowException"];
            _showErrorMessageExpression = attributes["ShowErrorMessage"];
        }
    }
}
