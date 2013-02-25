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
using Microsoft.Practices.SmartClientFactory.CustomWizardPages;
using Microsoft.Practices.SmartClientFactory.Properties;
using System.Globalization;
using System.IO;

namespace Microsoft.Practices.EnterpriseLibrary.Validation.Validators
{
    public class ViewNameFileNotExistsValidator : Validator<string>
    {
        public const string DefaultFailureMessage = "View filename already exists";

        public ViewNameFileNotExistsValidator()
            :base(null,null)
        {
        }

        protected override void DoValidate(string objectToValidate, object currentTarget, string key, ValidationResults validationResults)
        {
            ICreateViewPageModel pageModel = currentTarget as ICreateViewPageModel;
            if (pageModel!=null && pageModel.ValidateExistingFileExtension!=null)
            {
                string physicalPath = string.Empty;
                if (pageModel.ProjectItem != null)
                {
                    physicalPath = pageModel.ProjectItem.ItemPath;
                }
                else
                {
                    physicalPath = pageModel.ModuleProject.ProjectPath;
                }
                string moduleViewsProjectPath = (pageModel.CreateViewFolder) ? Path.Combine(physicalPath, objectToValidate) : physicalPath;

                string viewFileName = String.Format(CultureInfo.CurrentCulture, "{0}.{1}", objectToValidate, pageModel.ValidateExistingFileExtension);
                string viewExistsMessage = (pageModel.IsWpfView) ? Resources.YouCantAddWpfViewIfFormsViewExists : Resources.YouCantAddFormsViewIfWpfViewExists;

                Validator<string> _viewViewFileValidator = new FileNotExistsValidator(
                                    moduleViewsProjectPath,
                                    String.Format(CultureInfo.CurrentCulture, viewExistsMessage, Path.Combine(moduleViewsProjectPath, viewFileName)));
                _viewViewFileValidator.Validate(viewFileName, validationResults);
            }
        }

        protected override string DefaultMessageTemplate
        {
            get { return DefaultFailureMessage; }
        }
    }
}
