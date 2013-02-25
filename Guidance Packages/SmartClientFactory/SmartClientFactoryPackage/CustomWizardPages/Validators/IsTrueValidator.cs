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
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.SmartClientFactory.CustomWizardPages;
using System.Globalization;

namespace Microsoft.Practices.EnterpriseLibrary.Validation.Validators
{
    public class IsTrueValidator : Validator<bool>
    {
        public IsTrueValidator()
            : base(null, null)
        {
        }

        protected override void DoValidate(bool objectToValidate, object currentTarget, string key, ValidationResults validationResults)
        {
            if (!objectToValidate)
            {
                LogValidationResult(validationResults, MessageTemplate, currentTarget, key);
            }
        }
        protected override string DefaultMessageTemplate
        {
            get { return "The property is not true"; }
        }

    }
}
