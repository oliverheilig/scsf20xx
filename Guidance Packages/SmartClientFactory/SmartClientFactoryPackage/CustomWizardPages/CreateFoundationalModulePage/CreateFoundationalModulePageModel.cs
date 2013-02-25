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
using System.ComponentModel.Design;
using Microsoft.Practices.RecipeFramework.Extensions.DteWrapper;
using EnvDTE;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace Microsoft.Practices.SmartClientFactory.CustomWizardPages
{
    public interface ICreateFoundationalModulePageModel
    {
        string ModuleNamespace { get; }
        bool CreateModuleInterfaceLibrary { get; set; }
        bool CreateLayoutModule { get; set; }
        bool ShowDocumentation { get; set; }
        bool CreateTestProject { get; set; }
        bool IsValid { get; }
        string ModuleName { get; }
        string Language { get; }
    }

    public class CreateFoundationalModulePageModel : ILanguageDependentPageModel, ICreateFoundationalModulePageModel
    {
        private const string ModuleNamespaceKey = "ModuleNamespace";
        private const string CreateModuleInterfaceLibraryKey = "CreateModuleInterfaceLibrary";
        private const string CreateLayoutModuleKey = "CreateLayoutModule";
        private const string ShowDocumentationKey = "ShowDocumentation";
        private const string CreateTestProjectKey = "CreateTestProject";
        private const string ModuleNameKey = "ModuleName";
        private const string RecipeLanguageKey = "RecipeLanguage";

        private IDictionaryService _dictionary;
        private Validator _validator;

        public CreateFoundationalModulePageModel(IDictionaryService dictionary)
        {
            this._dictionary = dictionary;
        }

        public string ModuleNamespace
        {
            get { return GetDictString(ModuleNamespaceKey); }
        }

        public bool CreateModuleInterfaceLibrary
        {
            get { return (bool)_dictionary.GetValue(CreateModuleInterfaceLibraryKey); }
            set { _dictionary.SetValue(CreateModuleInterfaceLibraryKey, value); }
        }

        public bool CreateLayoutModule
        {
            get { return (bool)_dictionary.GetValue(CreateLayoutModuleKey); }
            set { _dictionary.SetValue(CreateLayoutModuleKey, value); }
        }

        public bool ShowDocumentation
        {
            get { return (bool)_dictionary.GetValue(ShowDocumentationKey); }
            set { _dictionary.SetValue(ShowDocumentationKey, value); }
        }

        public bool CreateTestProject
        {
            get { return (bool)_dictionary.GetValue(CreateTestProjectKey); }
            set { _dictionary.SetValue(CreateTestProjectKey, value); }
        }
        
        public string ModuleName
        {
            get { return GetDictString(ModuleNameKey); }
        }

        public string Language
        {
            get { return GetDictString(RecipeLanguageKey); }
        }

        private string GetDictString(string key)
        {
            return (string)(_dictionary.GetValue(key));
        }

        public bool IsValid
        {
            get
            {
                if (_validator == null)
                {
                    _validator = ValidationFactory.CreateValidator<CreateFoundationalModulePageModel>();
                }

                return _validator.Validate(this).IsValid;
            }
        }
    }
}
