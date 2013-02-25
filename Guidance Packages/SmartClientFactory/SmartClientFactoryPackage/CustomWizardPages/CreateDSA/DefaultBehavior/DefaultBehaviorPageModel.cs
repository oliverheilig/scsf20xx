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
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.SmartClientFactory.Properties;
using System.Globalization;

namespace Microsoft.Practices.SmartClientFactory.CustomWizardPages
{
    public interface IDefaultBehaviorPageModel
    {
        string Stamps { get; set; }
        string MaxRetries { get; set; }
        string Expiration { get; set; }
        string Tag { get; set; }
        string ProxyFactoryTypeFullName { get;}

        bool IsValid { get; }
    }

    public class DefaultBehaviorPageModel : IServiceProviderProviderPageModel, IDefaultBehaviorPageModel
    {
        private IDictionaryService _dictionary;
        private IServiceProvider _serviceProvider;
        private Validator _validator;

        private const string StampsKey = "Stamps";
        private const string MaxRetriesKey = "MaxRetries";
        private const string ExpirationKey = "Expiration";
        private const string TagKey = "Tag";
        private const string ProxyFactoryTypeFullNameKey = "ProxyFactoryTypeFullName";

        public DefaultBehaviorPageModel(IDictionaryService dictionary, IServiceProvider serviceProvider)
        {
            _dictionary = dictionary;
            _serviceProvider = serviceProvider;
        }

        public IServiceProvider ServiceProvider
        {
            get { return _serviceProvider; }
        }

        [NotNullValidator,
        TypeConversionValidator(typeof(int))]
        public string Stamps
        {
            get { return ((int?)_dictionary.GetValue(StampsKey)).ToString(); }
            set
            {
                int stamps;
                if (int.TryParse(value, NumberStyles.Integer, CultureInfo.CurrentCulture, out stamps))
                {
                    _dictionary.SetValue(StampsKey, stamps);
                }
                else
                {
                    _dictionary.SetValue(StampsKey, null);
                }
            }
        }

        [NotNullValidator,
        TypeConversionValidator(typeof(int))]
        public string MaxRetries
        {
            get { return ((int?)_dictionary.GetValue(MaxRetriesKey)).ToString(); }
            set {
                int maxRetries;
                if (int.TryParse(value,NumberStyles.Integer,CultureInfo.CurrentCulture, out maxRetries))
                {
                    _dictionary.SetValue(MaxRetriesKey, maxRetries);
                }
                else
                {
                    _dictionary.SetValue(MaxRetriesKey, null);
                }
            }
        }

        [NotNullValidator,
        TimeSpanValidator]
        public string Expiration
        {
            get {
                TimeSpan? expiration=(TimeSpan?)_dictionary.GetValue(ExpirationKey);
                if (expiration.HasValue)
                {
                    return string.Format(CultureInfo.InvariantCulture, "{0:D2}.{1:D2}:{2:D2}:{3:D2}", expiration.Value.Days, expiration.Value.Hours, expiration.Value.Minutes, expiration.Value.Seconds);
                }
                return null; 
            }
            set {
                TimeSpan expiration;
                if (TimeSpan.TryParse(value, out expiration))
                {
                    _dictionary.SetValue(ExpirationKey, expiration);
                }
                else
                {
                    _dictionary.SetValue(ExpirationKey, null);
                }
            }
        }

        public string Tag
        {
            get { return _dictionary.GetValue(TagKey) as string; }
            set { _dictionary.SetValue(TagKey, value); }
        }

        [NotNullValidator]
        public string ProxyFactoryTypeFullName
        {
            get { return _dictionary.GetValue(ProxyFactoryTypeFullNameKey) as string; }
        }

        public bool IsValid
        {
            get
            {
                if (_validator == null)
                {
                    _validator = ValidationFactory.CreateValidator<DefaultBehaviorPageModel>();
                }

                return _validator.Validate(this).IsValid;
            }
        }
    }
}
