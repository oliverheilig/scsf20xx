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
using Microsoft.Practices.EnterpriseLibrary.Validation;
using EnvDTE;
using Microsoft.Practices.RecipeFramework.Extensions.CommonHelpers;
using Microsoft.Practices.SmartClientFactory.CustomWizardPages;

namespace Microsoft.Practices.EnterpriseLibrary.Validation.Validators
{
    class EventTopicPublicationValidator : Validator<string>
    {
        public const string DefaultFailureMessage = "Event topic is not valid.";

        public EventTopicPublicationValidator()
            : base(DefaultFailureMessage, null)
        {
        }

        protected override void DoValidate(string objectToValidate, object currentTarget, string key, ValidationResults validationResults)
        {
            if (objectToValidate == null)
            {
                this.LogValidationResult(validationResults, this.DefaultMessageTemplate, currentTarget, key);
            }
            else
            {
                IServiceProviderProviderPageModel pageModel = currentTarget as IServiceProviderProviderPageModel;
                if (pageModel != null)
                {
                    IServiceProvider _serviceProvider = pageModel.ServiceProvider;
                    if (_serviceProvider != null)
                    {
                        CodeClass cc = FindCodeClass(_serviceProvider);
                        if (cc != null)
                        {
                            if (FindCodeElement(cc, objectToValidate) != null)
                            {
                                this.LogValidationResult(validationResults, this.DefaultMessageTemplate, currentTarget, key);
                            }
                        }
                    }
                }
            }
        }

        internal static CodeClass FindCodeClass(IServiceProvider context)
        {
            DTE dte = context.GetService(typeof(DTE)) as DTE;

            if (dte.SelectedItems.Count == 1)
            {
                ProjectItem item = dte.SelectedItems.Item(1).ProjectItem;
                foreach (CodeElement element in item.FileCodeModel.CodeElements)
                {
                    if (element is CodeNamespace)
                    {
                        CodeNamespace cn = (CodeNamespace)element;
                        if (cn.Members.Count > 0 && cn.Members.Item(1) is CodeClass)
                            return (CodeClass)cn.Members.Item(1);
                    }
                    else if (element is CodeClass)
                    {
                        return (CodeClass)element;
                    }
                }
            }
            return null;
        }

        internal static CodeElement FindCodeElement(CodeClass codeClass, string name)
        {
            foreach (CodeElement ce in codeClass.Members)
            {
                if (ce.Name == name)
                    return ce;
            }
            return null;
        }

        protected override string DefaultMessageTemplate
        {
            get { return DefaultFailureMessage; }
        }
    }
}
