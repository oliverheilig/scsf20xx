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
using System.CodeDom.Compiler;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace Microsoft.Practices.SmartClientFactory.CustomWizardPages
{
    public interface ICreateBusinessModulePageModel
    {
        string ModuleNamespace { get; }
        bool CreateModuleInterfaceLibrary { get; set; }
        bool ShowDocumentation { get; set; }
        bool CreateTestProject { get; set; }
        string ModuleName { get; }
        string Language { get; }
        bool IsValid { get; }
    }

    public class CreateBusinessModulePageModel : ICreateBusinessModulePageModel
    {
        private const string ModuleNamespaceKey = "ModuleNamespace";
        private const string CreateModuleInterfaceLibraryKey = "CreateModuleInterfaceLibrary";
        private const string ShowDocumentationKey = "ShowDocumentation";
        private const string CreateTestProjectKey = "CreateTestProject";
        private const string ModuleNameKey = "ModuleName";
        private const string RecipeLanguageKey = "RecipeLanguage";

        private IDictionaryService dictionary;
        private Validator _validator;

        public CreateBusinessModulePageModel(IDictionaryService dictionary)
        {
            this.dictionary = dictionary;
        }

        public string ModuleNamespace
        {
            get { return GetDictString(ModuleNamespaceKey); }
        }

        public bool CreateModuleInterfaceLibrary
        {
            get { return (bool)dictionary.GetValue(CreateModuleInterfaceLibraryKey); }
            set { dictionary.SetValue(CreateModuleInterfaceLibraryKey, value); }
        }

        public bool ShowDocumentation
        {
            get { return (bool)dictionary.GetValue(ShowDocumentationKey); }
            set { dictionary.SetValue(ShowDocumentationKey, value); }
        }

        public bool CreateTestProject
        {
            get { return (bool)dictionary.GetValue(CreateTestProjectKey); }
            set { dictionary.SetValue(CreateTestProjectKey, value); }
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
            return (string)(dictionary.GetValue(key));
        }

        public bool IsValid
        {
            get
            {
                if (_validator == null)
                {
                    _validator = ValidationFactory.CreateValidator<CreateBusinessModulePageModel>();
                }

                return _validator.Validate(this).IsValid;
            }
        }
    }
}
