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
using Microsoft.Practices.RecipeFramework.Library;
using Microsoft.Practices.SmartClientFactory.Properties;
using Microsoft.Practices.RecipeFramework.Extensions;

namespace Microsoft.Practices.SmartClientFactory.CustomWizardPages
{
    public class CreateFoundationalModulePagePresenter
    {
        private ICreateFoundationalModulePageModel _model;
        private ICreateFoundationalModulePage _view;

        public CreateFoundationalModulePagePresenter(ICreateFoundationalModulePage view, ICreateFoundationalModulePageModel model)
        {
            Guard.ArgumentNotNull(view, "view");
            Guard.ArgumentNotNull(view, "model");
            
            _view = view;
            _model = model;

            _view.CreateModuleInterfaceLibraryChanged += new EventHandler<EventArgs>(OnCreateModuleInterfaceLibraryChanged);
            _view.CreateLayoutModuleChanged += new EventHandler<EventArgs>(OnCreateLayoutModuleChanged);
            _view.ShowDocumentationChanged += new EventHandler<EventArgs>(OnShowDocumentationChanged);
            _view.RequestingValidation += new EventHandler<EventArgs<bool>>(OnValidating);
            _view.CreateTestProjectChanged += new EventHandler<EventArgs>(OnCreateTestProjectChanged);

        }

        void OnShowDocumentationChanged(object sender, EventArgs e)
        {
            _model.ShowDocumentation = _view.ShowDocumentation;
        }

        void OnCreateTestProjectChanged(object sender, EventArgs e)
        {
            _model.CreateTestProject = _view.CreateTestProject;
        }
                
        void OnCreateModuleInterfaceLibraryChanged(object sender, EventArgs e)
        {
            _model.CreateModuleInterfaceLibrary = _view.CreateModuleInterfaceLibrary;
        }

        void OnCreateLayoutModuleChanged(object sender, EventArgs e)
        {
            _model.CreateLayoutModule = _view.CreateLayoutModule;
        }

        public void OnViewReady()
        {
            _view.ShowModuleNamespace(_model.ModuleNamespace);
            _view.SetModuleName(_model.ModuleName);
            _view.SetLanguage(_model.Language);
            _view.RefreshSolutionPreview();
        }

        void OnValidating(object sender, EventArgs<bool> e)
        {
            e.Data = _model.IsValid;
        }

    }    
}
