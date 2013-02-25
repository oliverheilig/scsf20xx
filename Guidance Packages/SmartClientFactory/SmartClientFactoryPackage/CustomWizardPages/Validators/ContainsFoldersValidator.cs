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
using System.Globalization;

namespace Microsoft.Practices.EnterpriseLibrary.Validation.Validators
{
    public class ContainsFoldersValidator : Validator<string>
    {
        public const string DefaultFailureMessage = "Folder {0} does not exists in path {1}";

        private string[] _foldersList;

        public ContainsFoldersValidator(string foldersList)
            : base(DefaultFailureMessage, null)
        {
            if (!string.IsNullOrEmpty(foldersList))
            {
                _foldersList = foldersList.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        protected override void DoValidate(string objectToValidate, object currentTarget, string key, ValidationResults validationResults)
        {
            if (objectToValidate != null)
            {
                foreach (string subFolder in _foldersList)
                {
                    string fullPath = Path.Combine(objectToValidate, subFolder);
                    if (!Directory.Exists(fullPath))
                    {
                        string message = string.Format(CultureInfo.CurrentCulture,
                                                    this.MessageTemplate,
                                                    subFolder,
                                                    objectToValidate);
                        this.LogValidationResult(validationResults, message, currentTarget, key);
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
