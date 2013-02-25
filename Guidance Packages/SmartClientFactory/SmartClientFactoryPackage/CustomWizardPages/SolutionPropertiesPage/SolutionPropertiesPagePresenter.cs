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
using Microsoft.Practices.RecipeFramework.Extensions;
using Microsoft.Practices.RecipeFramework.Library;

namespace Microsoft.Practices.SmartClientFactory.CustomWizardPages
{
    public class SolutionPropertiesPagePresenter
    {
        private ISolutionPropertiesPage _view;
        private ISolutionPropertiesModel _model;

        public SolutionPropertiesPagePresenter(
            ISolutionPropertiesPage view, ISolutionPropertiesModel model)
        {
            Guard.ArgumentNotNull(view, "view");
            Guard.ArgumentNotNull(view, "model");

            _view = view;
            _model = model;

            _view.SupportLibrariesPathChanging +=
                new EventHandler<EventArgs>(OnSupportingLibrariesPathChanging);
            _view.RootNamespaceChanging += new EventHandler<EventArgs>(OnRootNamespaceChanging);
            _view.RequestingValidation += new EventHandler<EventArgs<bool>>(OnValidating);
            _view.ShowDocumentationChanging +=
                new EventHandler<EventArgs>(OnShowDocumentationChanging);
            _view.CreateShellLayoutModuleChanging +=
                new EventHandler<EventArgs>(OnCreateShellLayoutModuleChanging);
            _view.SupportWPFViewsChanging += new EventHandler<EventArgs>(OnSupportWPFViewsChanging);
        }


        private void OnShowDocumentationChanging(object sender, EventArgs e)
        {
            _model.ShowDocumentation = _view.ShowDocumentation;
        }

        private void OnCreateShellLayoutModuleChanging(object sender, EventArgs e)
        {
            _model.CreateShellLayoutModule = _view.CreateShellLayoutModule;
        }

        private void OnSupportWPFViewsChanging(object sender, EventArgs e)
        {
            _model.SupportWPFViews = _view.SupportWPFViews;
        }

        public void OnViewReady()
        {
            _view.ShowSupportLibraries(_model.GetSupportingLibraries(), _model.GetMissingLibraries());
            _view.ShowSupportLibrariesPath(_model.SupportLibrariesPath);
            _view.ShowRootNamespace(_model.RootNamespace);
            _view.ShowShowDocumentation(_model.ShowDocumentation);
            _view.SetLanguage(_model.Language);
            _view.RefreshSolutionPreview();
        }

        private void OnValidating(object sender, EventArgs<bool> e)
        {
            e.Data = _model.IsValid;
        }

        private void OnSupportingLibrariesPathChanging(object sender, EventArgs e)
        {
            _model.SupportLibrariesPath = _view.SupportLibrariesPath;
            _view.ShowSupportLibraries(_model.GetSupportingLibraries(), _model.GetMissingLibraries());
        }

        private void OnRootNamespaceChanging(object sender, EventArgs e)
        {
            _model.RootNamespace = _view.RootNamespace;
        }
    }
}