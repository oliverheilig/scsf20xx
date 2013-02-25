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
using Microsoft.Practices.RecipeFramework.Extensions.DteWrapper;
using System.Xml.Serialization;
using Microsoft.Practices.RecipeFramework.Extensions.CommonHelpers;
using Microsoft.Practices.RecipeFramework.Extensions;
using Microsoft.Practices.SmartClientFactory.Properties;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Microsoft.Practices.SmartClientFactory.CustomWizardPages
{
    public class EndpointPagePresenter
    {
        private IEndpointPage _view;
        private IEndpointPageModel _model;
        private RangeValidator _MethodsCountValidator;

        public EndpointPagePresenter(IEndpointPage view, IEndpointPageModel model)
        {
            _view = view;
            _model = model;

            _view.EndpointChanged += new EventHandler<EventArgs>(OnEndpointChanged);
            _view.ProxyTypeChanged += new EventHandler<EventArgs>(OnProxyTypeChanged);
            _view.MethodsChanged += new EventHandler<EventArgs>(OnMethodsChanged);
            _view.StampsChanged += new EventHandler<EventArgs>(OnStampsChanged);
            _view.MaxRetriesChanged += new EventHandler<EventArgs>(OnMaxRetriesChanged);
            _view.ExpirationChanged += new EventHandler<EventArgs>(OnExpirationChanged);
            _view.TagChanged += new EventHandler<EventArgs>(OnTagChanged);
            _view.ShowDocumentationChanged += new EventHandler<EventArgs>(OnShowDocumentationChanged);

            _MethodsCountValidator = new RangeValidator(1,RangeBoundaryType.Inclusive,
                                                int.MaxValue,RangeBoundaryType.Inclusive, Resources.OneSelectedMethodNeeded);
            _view.RequestingValidation += new EventHandler<EventArgs<bool>>(OnRequestingValidation);
        }

        void OnShowDocumentationChanged(object sender, EventArgs e)
        {
            _model.ShowDocumentation = _view.ShowDocumentation;
        }

        void OnMethodsChanged(object sender, EventArgs e)
        {
            _model.ServiceAgentMethods = _view.Methods;
        }

        private void OnMaxRetriesChanged(object sender, EventArgs e)
        {
            _model.MaxRetries = _view.MaxRetries;
        }

        private void OnStampsChanged(object sender, EventArgs e)
        {
            _model.Stamps = _view.Stamps;
        }

        private void OnExpirationChanged(object sender, EventArgs e)
        {
            _model.Expiration = _view.Expiration;
        }

        private void OnTagChanged(object sender, EventArgs e)
        {
            _model.Tag = _view.TagValue;
        }

        public void OnViewReady()
        {
            _view.ShowShowDocumentation(_model.ShowDocumentation);
            _view.SetLanguage(_model.Language);
            _view.ShowEndpoint(_model.Endpoint);
            _view.ShowProxyType(_model.ProxyType);

            SelectOperationsByDefault(_model.ServiceAgentMethods, _model.Operations);
            _view.ShowMethods(_model.OriginalTypeMethods, _model.ServiceAgentMethods);
            _view.RefreshSolutionPreview(_model.DisconnectedAgentsFolder, 
                                        _model.ProxyFolder, 
                                        _model.AgentFileName, 
                                        _model.AgentCallbackFileName, 
                                        _model.AgentCallbackBaseFileName,
                                        _model.CurrentProject);

            _view.ShowExpiration(_model.Expiration);
            _view.ShowMaxRetries(_model.MaxRetries);
            _view.ShowStamps(_model.Stamps);
            _view.ShowTag(_model.Tag);
            _view.ShowProxyFactoryTypeFullName(_model.ProxyFactoryTypeFullName);
            
            _view.ShowNotBuildPanel(_model.Built, _model.ExistsProxyClass, _model.CurrentProxyTypeName);
        }

        private void SelectOperationsByDefault(List<MethodInfo> selectedMethods, List<string> operations)
        {
            if (operations != null && selectedMethods!=null)
            {
                selectedMethods.RemoveAll(delegate(MethodInfo innerMethod)
                                                {
                                                    return !operations.Contains(innerMethod.Name);
                                                });
            }
        }

        void OnEndpointChanged(object sender, EventArgs e)
        {
            _model.Endpoint = _view.Endpoint;
        }

        void OnProxyTypeChanged(object sender, EventArgs e)
        {
            _model.ProxyTypeName = _view.ProxyTypeName;
            _view.ShowMethods(_model.OriginalTypeMethods, _model.ServiceAgentMethods);
            _view.RefreshSolutionPreview(_model.DisconnectedAgentsFolder,
                        _model.ProxyFolder,
                        _model.AgentFileName,
                        _model.AgentCallbackFileName,
                        _model.AgentCallbackBaseFileName,
                        _model.CurrentProject);
        }
        void OnRequestingValidation(object sender, EventArgs<bool> e)
        {
            e.Data = _model.IsValid;
        }        
    }
}
