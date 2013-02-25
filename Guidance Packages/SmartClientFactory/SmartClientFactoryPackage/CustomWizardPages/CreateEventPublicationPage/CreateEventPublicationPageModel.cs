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
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.SmartClientFactory.CABCompatibleTypes;

namespace Microsoft.Practices.SmartClientFactory.CustomWizardPages
{
    public interface ICreateEventPublicationPageModel
    {
        bool ShowDocumentation { get; set; }
        string EventTopic { get; set; }
        string EventArgs { get; set; }
        PublicationScope PublicationScope { get; set; }
        List<PublicationScope> PublicationScopes { get; }
        bool IsValid { get; }
    }

    public class CreateEventPublicationPageModel : IServiceProviderProviderPageModel, ICreateEventPublicationPageModel
    {
        private const string ShowDocumentationKey = "ShowDocumentation";
        private const string EventTopicKey = "EventTopic";
        private const string PublicationScopeKey = "PublicationScope";
        private const string EventArgsKey = "EventArgs";

        private IDictionaryService _dictionary;
        private Validator _validator;
        private IServiceProvider _serviceProvider;

        public CreateEventPublicationPageModel(IDictionaryService dictionary, IServiceProvider serviceProvider)
        {
            this._dictionary = dictionary;
            this._serviceProvider = serviceProvider;
        }

        public bool ShowDocumentation
        {
            get {
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
        IdentifierValidator,
        EventTopicPublicationValidatorAttribute]
        public string EventTopic
        {
            get { return (string)_dictionary.GetValue(EventTopicKey); }
            set { _dictionary.SetValue(EventTopicKey, value); }
        }

        public PublicationScope PublicationScope
        {
            get { return (PublicationScope)_dictionary.GetValue(PublicationScopeKey); }
            set { _dictionary.SetValue(PublicationScopeKey, value); }
        }

        [NotNullValidator,
        StringLengthValidator(1, 250),
        ExistingClassValidator]
        public String EventArgs
        {
            get { return ((string)_dictionary.GetValue(EventArgsKey)); }
            set { _dictionary.SetValue(EventArgsKey, value); }
        }

        public List<PublicationScope> PublicationScopes
        {
            get 
            {
                List<PublicationScope> scopes = new List<PublicationScope>();
                foreach (PublicationScope var in Enum.GetValues(typeof(PublicationScope)))
                {
                    scopes.Add(var);
                }
                return scopes; 
            }
        }
        
        public bool IsValid
        {
            get
            {
                if (_validator == null)
                {
                    _validator = ValidationFactory.CreateValidator<CreateEventPublicationPageModel>();
                }

                return _validator.Validate(this).IsValid;
            }
        }
        
        public IServiceProvider ServiceProvider
        {
            get { return _serviceProvider; }
        }
    }
}
