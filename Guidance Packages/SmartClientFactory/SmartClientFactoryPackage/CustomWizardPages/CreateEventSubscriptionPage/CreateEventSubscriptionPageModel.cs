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
using System.Text.RegularExpressions;
using EnvDTE;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.SmartClientFactory.CABCompatibleTypes;

namespace Microsoft.Practices.SmartClientFactory.CustomWizardPages
{
    public interface ICreateEventSubscriptionPageModel
    {
        bool ShowDocumentation { get; set; }
        string EventTopic { get; set; }
        string EventArgs { get; set; }
        ThreadOption ThreadingOption { get; set; }
        List<ThreadOption> ThreadingOptions { get; }
        List<string> EventTopics { get;}
        bool IsValid { get; }
    }

    public class CreateEventSubscriptionPageModel : IServiceProviderProviderPageModel, ICreateEventSubscriptionPageModel
    {
        private const string ShowDocumentationKey = "ShowDocumentation";
        private const string EventTopicKey = "EventTopic";
        private const string ThreadOptionKey = "ThreadOption";
        private const string EventArgsKey = "EventArgs";
        private const string EventTopicNamesCodeClassKey = "EventTopicNamesCodeClass";

        private IDictionaryService _dictionary;
        private Validator _validator;
        private IServiceProvider _serviceProvider;

        public CreateEventSubscriptionPageModel(IDictionaryService dictionary, IServiceProvider serviceProvider)
        {
            this._dictionary = dictionary;
            this._serviceProvider = serviceProvider;
        }

        public bool ShowDocumentation
        {
            get
            {
                if (_dictionary.GetValue(ShowDocumentationKey) != null)
                {
                    return (bool)_dictionary.GetValue(ShowDocumentationKey);
                }
                return false;
            }
            set { _dictionary.SetValue(ShowDocumentationKey, value); }
        }

        [NotNullValidator,
        StringLengthValidator(1, 250),
        EventTopicSubscriptionValidator]
        public string EventTopic
        {
            get { return (string)_dictionary.GetValue(EventTopicKey); }
            set { _dictionary.SetValue(EventTopicKey, value); }
        }

        public ThreadOption ThreadingOption
        {
            get { return (ThreadOption)_dictionary.GetValue(ThreadOptionKey); }
            set { _dictionary.SetValue(ThreadOptionKey, value); }
        }

        [NotNullValidator,
        StringLengthValidator(1, 250),
        ExistingClassValidator]
        public String EventArgs
        {
            get { return ((string)_dictionary.GetValue(EventArgsKey)); }
            set { _dictionary.SetValue(EventArgsKey, value); }
        }

        public List<ThreadOption> ThreadingOptions
        {
            get 
            {
                List<ThreadOption> scopes = new List<ThreadOption>();
                foreach (ThreadOption var in Enum.GetValues(typeof(ThreadOption)))
                {
                    scopes.Add(var);
                }
                return scopes; 
            }
        }

        public List<string> EventTopics
        {
            get
            {
                return GetEventTopicValues();
            }
        }

        public bool IsValid
        {
            get
            {
                if (_validator == null)
                {
                    _validator = ValidationFactory.CreateValidator<CreateEventSubscriptionPageModel>();
                }

                return _validator.Validate(this).IsValid;
            }
        }

        private List<string> GetEventTopicValues()
        {
            List<string> members = new List<string>();

            if (_dictionary != null)
            {
                CodeClass target = (CodeClass)_dictionary.GetValue(EventTopicNamesCodeClassKey);
                if (target != null)
                {
                    members.AddRange(GetDefinedConstants(target));
                }
            }
            return members;
        }

        private List<string> GetDefinedConstants(CodeClass target)
        {
            List<string> result = new List<string>();
            EditPoint startPoint = target.StartPoint.CreateEditPoint();
            string text = startPoint.GetText(target.EndPoint);

            string pattern = @"const\s*\w*\s*(\w*)";

            if (target.Language == CodeModelLanguageConstants.vsCMLanguageVB)
                pattern = @"Const\s*(\w*)";

            Regex exp = new Regex(pattern, RegexOptions.Multiline);

            foreach (Match match in exp.Matches(text))
            {
                result.Add(match.Groups[1].Value);
            }

            return result;
        }

        public IServiceProvider ServiceProvider
        {
            get { return _serviceProvider; }
        }
    }
}
