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
using System.Text.RegularExpressions;

namespace Microsoft.Practices.EnterpriseLibrary.Validation.Validators
{
    /// <summary>
    /// Validator for file names
    /// </summary>
    public class FileNameValidator : AndCompositeValidator
    {
        /// <summary>
        /// Creates a new instance of <see cref="FileNameValidator"/>
        /// </summary>
        public FileNameValidator()
            :base(new Validator[]{new FileNameLengthValidator(),new ReservedSystemWordsFileNameValidator()})
        {
        }

        private class FileNameLengthValidator : Validator<string>
        {
            private const int maxFileNameLength = 110;

            public FileNameLengthValidator() : base("The file name is too long",null)
            {
            }
            protected override void DoValidate(string objectToValidate, object currentTarget, string key, ValidationResults validationResults)
            {
                if (objectToValidate==null || objectToValidate.Length > maxFileNameLength)
                    this.LogValidationResult(validationResults, this.MessageTemplate, currentTarget, key);
            }

            protected override string DefaultMessageTemplate
            {
                get { return "The file name is too long"; }
            }
        }

        private class ReservedSystemWordsFileNameValidator : Validator<string>
        {
            private Regex validNames = new Regex(@"^(?!^(PRN|AUX|CLOCK\$|NUL|CON|COM\d|LPT\d|\..*)(\..+)?$)[^\x00-\x1f\\?*:""><|/]+$",
                                             RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

            public ReservedSystemWordsFileNameValidator()
                : base("File name not allowed",null)
            {
            }

            protected override void DoValidate(string objectToValidate, object currentTarget, string key, ValidationResults validationResults)
            {
                if (objectToValidate==null || !validNames.IsMatch(objectToValidate))
                    this.LogValidationResult(validationResults, DefaultMessageTemplate, currentTarget, key);
            }

            protected override string DefaultMessageTemplate
            {
                get { return "File name not allowed"; }
            }
        }
    }
}
