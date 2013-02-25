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
    class EventTopicSubscriptionValidator : Validator<string>
    {
        public const string DefaultFailureMessage = "Event topic is not valid.";

        public EventTopicSubscriptionValidator()
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
                        CodeClass cc = EventTopicPublicationValidator.FindCodeClass(_serviceProvider);
                        if (cc != null)
                        {
                            string eventHandlerName = string.Format("On{0}", objectToValidate);
                            if (EventTopicPublicationValidator.FindCodeElement(cc, eventHandlerName) != null)
                            {
                                this.LogValidationResult(validationResults, this.DefaultMessageTemplate, currentTarget, key);
                            }
                        }
                    }
                }
            }
        }

        protected override string DefaultMessageTemplate
        {
            get { return DefaultFailureMessage; }
        }
    }
}
