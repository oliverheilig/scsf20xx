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
using System.Reflection;
using Microsoft.Practices.SmartClientFactory.ValueProviders;
using System.Collections.Specialized;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.SmartClientFactory.Properties;
using Microsoft.VisualStudio.Shell.Design;
using Microsoft.VisualStudio.Shell.Interop;
using System.Globalization;

namespace Microsoft.Practices.SmartClientFactory.CustomWizardPages
{
    public interface IEndpointPageModel
    {
        bool Built { get;}
        bool ExistsProxyClass { get;}
        bool ShowDocumentation { get; set; }
        string Endpoint { get; set; }
        Type ProxyType { get; }
        string ProxyTypeName { get; set;}
        string CurrentProxyTypeName { get;}
        List<MethodInfo> OriginalTypeMethods { get;}
        List<MethodInfo> ServiceAgentMethods { get; set;}
        List<string> Operations { get;}
        bool IsValid { get; }
        string Language { get; }
        string DisconnectedAgentsFolder { get; }
        string ProxyFolder { get; }
        string AgentFileName { get; }
        string AgentCallbackFileName { get; }
        string AgentCallbackBaseFileName { get; }
        IProjectModel CurrentProject { get; }

        string Stamps { get; set; }
        string MaxRetries { get; set; }
        string Expiration { get; set; }
        string Tag { get; set; }
        string ProxyFactoryTypeFullName { get;}
    }

    [HasSelfValidation]
    public class EndpointPageModel : ILanguageDependentPageModel, IServiceProviderProviderPageModel, IEndpointPageModel
    {
        private IDictionaryService _dictionary;
        private IServiceProvider _serviceProvider;
        private Validator _validator;


        private IProjectModel _currentProject;

        private const string BuiltKey = "Built";
        private const string ExistsProxyClassKey = "ExistsProxyClass";
        private const string EndpointKey = "Endpoint";
        private const string ProxyTypeKey = "ProxyType";
        private const string OriginalTypeMethodsKey = "OriginalTypeMethods";
        private const string ServiceAgentMethodsKey = "ServiceAgentMethods";
        private const string OperationsKey = "Operations";
        private const string RecipeLanguageKey = "RecipeLanguage";

        private const string ShowDocumentationKey = "ShowDocumentation";
        private const string DisconnectedAgentsFolderKey = "DisconnectedAgentsFolder";
        private const string ProxyFolderKey = "ProxyFolder";
        private const string CurrentProjectKey = "CurrentProject";

        private const string StampsKey = "Stamps";
        private const string MaxRetriesKey = "MaxRetries";
        private const string ExpirationKey = "Expiration";
        private const string TagKey = "Tag";
        private const string ProxyFactoryTypeFullNameKey = "ProxyFactoryTypeFullName";
        private const string CurrentProxyTypeNameKey = "CurrentProxyTypeName";

        public EndpointPageModel(IDictionaryService dictionary, IServiceProvider serviceProvider)
        {
            _dictionary = dictionary;
            _serviceProvider = serviceProvider;
        }

        [IsTrueValidator]
        public bool Built
        {
            get
            {
                if (_dictionary.GetValue(BuiltKey) != null)
                {
                    return (bool)_dictionary.GetValue(BuiltKey);
                }
                return false;
            }
        }

        public bool ExistsProxyClass
        {
            get
            {
                //Create DSA does not have this Argument, so default value is true.
                if (_dictionary.GetValue(ExistsProxyClassKey) != null)
                {
                    return (bool)_dictionary.GetValue(ExistsProxyClassKey);
                }
                return true;
            }
        }

        public string Language
        {
            get { return (string)_dictionary.GetValue(RecipeLanguageKey); }
        }

        public string DisconnectedAgentsFolder
        {
            get { return (string)_dictionary.GetValue(DisconnectedAgentsFolderKey); }
        }

        public string ProxyFolder
        {
            get { return (string)_dictionary.GetValue(ProxyFolderKey); }
        }

        public string AgentFileName
        {
            get {
                if (ProxyType != null)
                {
                    return "Agent";
                }
                else
                { return null; }
            }
        }

        public string AgentCallbackFileName
        {
            get
            {
                if (ProxyType != null)
                {
                    return "Callback";
                }
                else
                { return null; }
            }
        }

        public string AgentCallbackBaseFileName
        {
            get
            {
                if (ProxyType != null)
                {
                    return "Callbackbase";
                }
                else
                { return null; }
            } 
        }

        public IProjectModel CurrentProject
        {
            get
            {
                if (_currentProject == null)
                {
                    if (_dictionary.GetValue(CurrentProjectKey) != null)
                    {
                        _currentProject = new DteProjectModel(_dictionary.GetValue(CurrentProjectKey) as Project, _serviceProvider);
                    }
                }
                return _currentProject;
            }
        }

        //[NotNullValidator,
        //StringLengthValidator(1,250)]
        public string Endpoint
        {
            get { return _dictionary.GetValue(EndpointKey) as string; }
            set { _dictionary.SetValue(EndpointKey, value); }
        }

        [NotNullValidator]
        public Type ProxyType
        {
            get { return _dictionary.GetValue(ProxyTypeKey) as Type; }
            set 
            {
                _dictionary.SetValue(ProxyTypeKey, value);
            }
        }

        [ExistingClassValidator]
        public string ProxyTypeName
        {
            get
            {
                return (ProxyType != null) ? ProxyType.FullName : null;
            }
            set
            {
                ITypeResolutionService typeResolution=null;
                Project project=null;

                if (CurrentProject != null)
                {
                    project = CurrentProject.Project as Project;
                }
                if (_serviceProvider != null && project != null)
                {
                    DynamicTypeService typeService = VsHelper.GetService<DynamicTypeService>(_serviceProvider, true);
                    IVsHierarchy hier = DteConverter.ToHierarchy(project);
                    typeResolution = typeService.GetTypeResolutionService(hier);
                }

                if (typeResolution == null)
                {
                    ProxyType = Type.GetType(NotationHelper.ParseGenericNameToCLRNotation(value));
                }
                else
                {
                    ProxyType = typeResolution.GetType(NotationHelper.ParseGenericNameToCLRNotation(value));
                }
            }
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
            set
            {
                int maxRetries;
                if (int.TryParse(value, NumberStyles.Integer, CultureInfo.CurrentCulture, out maxRetries))
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
            get
            {
                TimeSpan? expiration = (TimeSpan?)_dictionary.GetValue(ExpirationKey);
                if (expiration.HasValue)
                {
                    return string.Format(CultureInfo.InvariantCulture, "{0:D2}.{1:D2}:{2:D2}:{3:D2}", expiration.Value.Days, expiration.Value.Hours, expiration.Value.Minutes, expiration.Value.Seconds);
                }
                return null;
            }
            set
            {
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

        public string CurrentProxyTypeName
        {
            get { return _dictionary.GetValue(CurrentProxyTypeNameKey) as string; }
        }

        public bool ShowDocumentation
        {
            get { return (bool)_dictionary.GetValue(ShowDocumentationKey); }
            set { _dictionary.SetValue(ShowDocumentationKey, value); }
        }

        public List<string> Operations
        {
            get { return _dictionary.GetValue(OperationsKey) as List<string>; }
            set { _dictionary.SetValue(OperationsKey, value); }
        }

        public List<MethodInfo> ServiceAgentMethods
        {
            get { return _dictionary.GetValue(ServiceAgentMethodsKey) as List<MethodInfo>; }
            set { _dictionary.SetValue(ServiceAgentMethodsKey, value); }
        }

        public List<MethodInfo> OriginalTypeMethods
        {
            get { return _dictionary.GetValue(OriginalTypeMethodsKey) as List<MethodInfo>; }
            set { _dictionary.SetValue(OriginalTypeMethodsKey, value); }
        }
        
        public bool IsValid
        {
            get
            {
                if (_validator == null)
                {
                    _validator = ValidationFactory.CreateValidator<EndpointPageModel>();
                }

                return _validator.Validate(this).IsValid;
            }
        }

        public IServiceProvider ServiceProvider
        {
            get { return _serviceProvider; }
        }

        [SelfValidation]
        public void ValidateServiceAgentMethods(ValidationResults validationResults)
        {
            if (ServiceAgentMethods.Count == 0)
            {
                validationResults.AddResult(new ValidationResult(Resources.OneSelectedMethodNeeded, this, "ServiceAgentMethods", null, null));
            }
        }
    }
}
