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
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace Microsoft.Practices.EnterpriseLibrary.Validation.Validators
{
    public class PathExistsValidator : Validator<string>
    {
        public const string DefaultFailureMessage = "Path is not valid";
        
        public PathExistsValidator() : base(DefaultFailureMessage, null)
        {
        }

        public PathExistsValidator(string errorMessage)
            : base(errorMessage, null)
        {
        }

        protected override void DoValidate(string objectToValidate, object currentTarget, string key, ValidationResults validationResults)
        {
            if (!File.Exists(objectToValidate) && !Directory.Exists(objectToValidate))
            {
                this.LogValidationResult(validationResults, this.MessageTemplate, currentTarget, key);
            }
        }

        protected override string DefaultMessageTemplate
        {
            get { return DefaultFailureMessage; }
        }
    }
}
