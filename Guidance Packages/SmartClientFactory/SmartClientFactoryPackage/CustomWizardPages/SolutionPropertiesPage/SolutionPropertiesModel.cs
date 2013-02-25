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
using System.ComponentModel.Design;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Microsoft.Practices.SmartClientFactory.CustomWizardPages
{
    public interface ISolutionPropertiesModel
    {
        string SupportLibrariesPath { get; set; }
        string RootNamespace { get; set; }
        bool IsValid { get; }
        bool ShowDocumentation { get; set; }
        string Language { get; }
        bool CreateShellLayoutModule { get; set; }
        bool SupportWPFViews { get; set; }
        string[] GetSupportingLibraries();
        bool[] GetMissingLibraries();
    }

    [HasSelfValidation()]
    public class SolutionPropertiesModel : ISolutionPropertiesModel
    {
        private const string SupportLibrariesPathKey = "SupportLibrariesPath";
        private const string CompositeUIDllsKey = "CompositeUIDlls";
        private const string EnterpriseLibraryDllsKey = "EnterpriseLibraryDlls";
        private const string RootNamespaceKey = "RootNamespace";
        private const string ShowDocumentationKey = "ShowDocumentation";
        private const string CreateShellLayoutModuleKey = "CreateShellLayoutModule";
        private const string UseSimpleShellKey = "UseSimpleShell";
        private const string SupportWPFViewsKey = "SupportWPFViews";
        private const string RecipeLanguageKey = "RecipeLanguage";
        private const string WorkspaceTechnologyKey = "WorkspaceTechnology";
        private const string ApplicationBaseClassKey = "ApplicationBaseClass";
        private const string ApplicationBaseClassNamespaceKey = "ApplicationBaseClassNamespace";

        private const string WinFormsApplicationBaseClass = "FormShellApplication";
        private const string WinFormsApplicationBaseClassNamespace =
            "Microsoft.Practices.CompositeUI.WinForms";

        private const string WpfApplicationBaseClass = "WPFFormShellApplication";
        private const string WpfApplicationBaseClassNamespace = "Microsoft.Practices.CompositeUI.WPF";

        private IDictionaryService _dictionary;
        private Validator _validator;

        public SolutionPropertiesModel(IDictionaryService dictionary)
        {
            _dictionary = dictionary;
        }

        [NotNullValidator,
         StringLengthValidator(1, 250),
         PathExistsValidator]
        public string SupportLibrariesPath
        {
            get { return GetDictString(SupportLibrariesPathKey); }
            set { _dictionary.SetValue(SupportLibrariesPathKey, value); }
        }

        [NotNullValidator()]
        public string CompositeUIDlls
        {
            get { return GetDictString(CompositeUIDllsKey); }
        }

        [NotNullValidator()]
        public string EnterpriseLibraryDlls
        {
            get { return GetDictString(EnterpriseLibraryDllsKey); }
        }

        public string[] GetSupportingLibraries()
        {
            string libraries = String.Join(";", new string[]
                {
                    CompositeUIDlls,
                    EnterpriseLibraryDlls
                });
            return libraries.Split(';');
        }

        public bool[] GetMissingLibraries()
        {
            string[] libraries = GetSupportingLibraries();
            List<bool> missing = new List<bool>(libraries.Length);
            foreach(string file in libraries)
            {
                if(string.IsNullOrEmpty(SupportLibrariesPath))
                {
                    missing.Add(true);
                }
                else
                {
                    FileExistsValidator fileValidator =
                        new FileExistsValidator(SupportLibrariesPath);
                    bool fileExists = fileValidator.Validate(file).IsValid;
                    missing.Add(!fileExists);
                }
            }
            return missing.ToArray();
        }

        [NotNullValidator(),
         StringLengthValidator(1, 250),
         NamespaceValidator()]
        public string RootNamespace
        {
            get { return GetDictString(RootNamespaceKey); }
            set { _dictionary.SetValue(RootNamespaceKey, value); }
        }

        public bool ShowDocumentation
        {
            get { return (bool)( _dictionary.GetValue(ShowDocumentationKey) ?? false ); }
            set { _dictionary.SetValue(ShowDocumentationKey, value); }
        }

        public bool CreateShellLayoutModule
        {
            get { return (bool)_dictionary.GetValue(CreateShellLayoutModuleKey); }
            set
            {
                _dictionary.SetValue(CreateShellLayoutModuleKey, value);
                _dictionary.SetValue(UseSimpleShellKey, !value);
            }
        }

        public bool SupportWPFViews
        {
            get { return (bool)_dictionary.GetValue(SupportWPFViewsKey); }
            set
            {
                if( value )
                {
                    _dictionary.SetValue(WorkspaceTechnologyKey, "WPF");
                    _dictionary.SetValue(ApplicationBaseClassKey, WpfApplicationBaseClass);
                    _dictionary.SetValue(ApplicationBaseClassNamespaceKey,
                        WpfApplicationBaseClassNamespace);
                }
                else
                {
                    _dictionary.SetValue(WorkspaceTechnologyKey, "WinForms");
                    _dictionary.SetValue(ApplicationBaseClassKey, WinFormsApplicationBaseClass);
                    _dictionary.SetValue(ApplicationBaseClassNamespaceKey,
                        WinFormsApplicationBaseClassNamespace);
                }
                _dictionary.SetValue(SupportWPFViewsKey, value);
            }
        }

        public string Language
        {
            get { return GetDictString(RecipeLanguageKey); }
        }

        private string GetDictString(string key)
        {
            return (string)( _dictionary.GetValue(key) );
        }

        public bool IsValid
        {
            get
            {
                if(_validator == null)
                {
                    _validator = ValidationFactory.CreateValidator<SolutionPropertiesModel>();
                }

                return _validator.Validate(this).IsValid;
            }
        }

        [SelfValidation()]
        public void Validate(ValidationResults validationResults)
        {
            Validator _supportLibrariesValidator =
                new FileArrayExistsValidator(SupportLibrariesPath);

            _supportLibrariesValidator.Validate(GetSupportingLibraries(), validationResults);
        }
    }
}