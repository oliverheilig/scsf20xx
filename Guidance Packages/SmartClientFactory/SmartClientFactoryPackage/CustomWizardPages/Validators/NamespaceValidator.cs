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
using Microsoft.Practices.SmartClientFactory.Properties;

namespace Microsoft.Practices.EnterpriseLibrary.Validation.Validators
{
    public class NamespaceValidator : Validator<string>
    {
        private IdentifierValidator identifierValidator;

        public NamespaceValidator()
            : base(null, null)
        {
            identifierValidator = new IdentifierValidator();
        }

        protected override void DoValidate(string objectToValidate, object currentTarget, string key, ValidationResults validationResults)
        {
            if (objectToValidate == null)
            {
                this.LogValidationResult(validationResults, Resources.ValidNamespaceRequired, currentTarget, key);
                return;
            }
            foreach (string part in objectToValidate.Split('.'))
            {
                ValidationResults tempValidationResults = identifierValidator.Validate(part);
                if (!tempValidationResults.IsValid)
                {
                    this.LogValidationResult(validationResults, Resources.ValidNamespaceRequired, currentTarget, key);
                    break;
                }
            }
        }

        protected override string DefaultMessageTemplate
        {
            get { return "The value {0} is not a valid namespace"; }
        }
    }
}
