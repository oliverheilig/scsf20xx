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
using System.Web.Services.Protocols;

namespace Microsoft.Practices.SmartClientFactory.CustomWizardPages
{
    public class DefaultBehaviorPagePresenter
    {
        private IDefaultBehaviorPage _view;
        private IDefaultBehaviorPageModel _model;

        public DefaultBehaviorPagePresenter(IDefaultBehaviorPage view, IDefaultBehaviorPageModel model)
        {
            _view = view;
            _model = model;

            _view.StampsChanged += new EventHandler<EventArgs>(OnStampsChanged);
            _view.MaxRetriesChanged += new EventHandler<EventArgs>(OnMaxRetriesChanged);
            _view.ExpirationChanged += new EventHandler<EventArgs>(OnExpirationChanged);
            _view.TagChanged += new EventHandler<EventArgs>(OnTagChanged);
            _view.WizardActivated += new EventHandler<EventArgs>(OnViewActivated);
            _view.RequestingValidation += new EventHandler<EventArgs<bool>>(OnRequestingValidation);
        }

        void OnViewActivated(object sender, EventArgs e)
        {
            _view.ShowProxyFactoryTypeFullName(_model.ProxyFactoryTypeFullName);
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

        void OnRequestingValidation(object sender, EventArgs<bool> e)
        {
            e.Data = _model.IsValid;
        }        

        public void OnViewReady()
        {
            _view.ShowExpiration(_model.Expiration);
            _view.ShowMaxRetries(_model.MaxRetries);
            _view.ShowStamps(_model.Stamps);
            _view.ShowTag(_model.Tag);
            _view.ShowProxyFactoryTypeFullName(_model.ProxyFactoryTypeFullName);
        }
    }
}
