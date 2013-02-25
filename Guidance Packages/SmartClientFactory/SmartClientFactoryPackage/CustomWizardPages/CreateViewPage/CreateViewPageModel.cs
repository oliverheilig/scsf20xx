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
using Microsoft.Practices.RecipeFramework.Extensions.DteWrapper;
using System.ComponentModel.Design;
using EnvDTE;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System.IO;
using System.Globalization;
using Microsoft.Practices.SmartClientFactory.Properties;

namespace Microsoft.Practices.SmartClientFactory.CustomWizardPages
{
    public interface ICreateViewPageModel
    {
        string ViewName { get; set; }
        string ValidateExistingFileExtension { get;}
        bool CreateViewFolder { get; set; }
        bool ShowDocumentation { get; set; }
        bool IsWpfView { get; }
        bool IsValid { get; }
        IProjectModel ModuleProject { get; }
        IProjectItemModel ProjectItem { get; set;}
        string Language { get; }
        bool TestProjectExists { get; }
        string ValidationErrorMessage { get; }

    }

    public class CreateViewPageModel : ILanguageDependentPageModel, ICreateViewPageModel
    {
        private IDictionaryService _dictionary;
        private IServiceProvider _serviceProvider;
        private Validator _validator;

        private IProjectModel _moduleProject;
        private IProjectItemModel _projectItem;

        private const string ViewNameKey = "ViewName";
        private const string ValidateExistingFileExtensionKey = "ValidateExistingFileExtension";
        private const string ShowDocumentationKey = "ShowDocumentation";
        private const string CreateViewFolderKey = "CreateViewFolder";
        private const string ModuleProjectKey = "ModuleProject";
        private const string ProjectItemKey = "SelectedProjectItem";
        private const string RecipeLanguageKey = "RecipeLanguage";
        private const string TestProjectExistsKey = "TestProjectExists";
        private const string IsWpfViewKey = "IsWpfView";
        
        public CreateViewPageModel(IDictionaryService dictionary, IServiceProvider serviceProvider)
        {
            _dictionary = dictionary;
            _serviceProvider = serviceProvider;
        }

        [NotNullValidator,
        StringLengthValidator(1, 250),
        ViewNameFileNameValidator,
        IdentifierValidator ,
        ViewNameFileNotExistsValidator]
        public string ViewName
        {
            get { return _dictionary.GetValue(ViewNameKey) as string; }
            set { _dictionary.SetValue(ViewNameKey, value); }
        }

        public string ValidateExistingFileExtension
        {
            get { return _dictionary.GetValue(ValidateExistingFileExtensionKey) as string; }
        }
        public bool ShowDocumentation
        {
            get { return (bool)_dictionary.GetValue(ShowDocumentationKey); }
            set { _dictionary.SetValue(ShowDocumentationKey, value); }
        }

        public bool CreateViewFolder
        {
            get { return (bool)(_dictionary.GetValue(CreateViewFolderKey)??false); }
            set { _dictionary.SetValue(CreateViewFolderKey, value); }
        }

        public string Language
        {
            get { return (string)_dictionary.GetValue(RecipeLanguageKey); }
        }

        public bool TestProjectExists
        {
            get { return (bool)_dictionary.GetValue(TestProjectExistsKey); }
        }

        public IProjectModel ModuleProject
        {
            get
            {
                if (_moduleProject == null)
                {
                    if (_dictionary.GetValue(ModuleProjectKey) != null)
                    {
                        _moduleProject = new DteProjectModel(_dictionary.GetValue(ModuleProjectKey) as Project, _serviceProvider);
                    }
                }
                return _moduleProject;
            }
        }

        public IProjectItemModel ProjectItem
        {
            get
            {
                if (_projectItem == null)
                {
                    if (_dictionary.GetValue(ProjectItemKey) != null)
                    {
                        _projectItem = new DteProjectItemModel(_dictionary.GetValue(ProjectItemKey) as ProjectItem);
                    }
                }
                return _projectItem;
            }
            set
            {
                _projectItem = value;
            }
        }
        public bool IsValid
        {
            get
            {
                if (_validator == null)
                {
                    _validator = ValidationFactory.CreateValidator<CreateViewPageModel>();
                }
                return _validator.Validate(this).IsValid;
            }
        }

        public string ValidationErrorMessage
        {
            get 
            {
                if (_validator == null)
                {
                    _validator = ValidationFactory.CreateValidator<CreateViewPageModel>();
                }
                return FormatErrorMessage(_validator.Validate(this));
            }
        }

        private string FormatErrorMessage(ValidationResults validationResults)
        {
            StringBuilder builder = new StringBuilder();
            IEnumerator<ValidationResult> enumerator = ((IEnumerable<ValidationResult>)validationResults).GetEnumerator();
            if (enumerator.MoveNext())
            {
                while (true)
                {
                    builder.AppendFormat(CultureInfo.CurrentUICulture, enumerator.Current.Message);
                    if (!enumerator.MoveNext())
                    {
                        break;
                    }
                    builder.AppendLine();
                }
            }
            return builder.ToString();
        }

        public bool IsWpfView
        {
            get { return (bool)_dictionary.GetValue(IsWpfViewKey); }
        }
    }
 }
