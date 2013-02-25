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

namespace Microsoft.Practices.SmartClientFactory.CustomWizardPages
{
    public class CreateViewPagePresenter
    {
        private ICreateViewPage _view;
        private ICreateViewPageModel _model;

        public CreateViewPagePresenter(ICreateViewPage view, ICreateViewPageModel model)
        {
            _view = view;
            _model = model;

            _view.ViewNameChanged += new EventHandler<EventArgs>(OnViewNameChanged);
            _view.RequestingValidation += new EventHandler<EventArgs<bool>>(OnRequestingValidation);
            _view.ShowDocumentationChanged += new EventHandler<EventArgs>(OnShowDocumentationChanged);
            _view.CreateViewFolderChanged += new EventHandler<EventArgs>(OnCreateViewFolderChanged);
        }

        void OnShowDocumentationChanged(object sender, EventArgs e)
        {
            _model.ShowDocumentation = _view.ShowDocumentation;
        }

        void OnCreateViewFolderChanged(object sender, EventArgs e)
        {
            _model.CreateViewFolder = _view.CreateViewFolder;
        }

        public void OnViewReady()
        {
            _view.ShowViewName(_model.ViewName);
            _view.SetLanguage(_model.Language);
            _view.TestProjectExists = _model.TestProjectExists;
            _view.RefreshSolutionPreview(_model.ModuleProject,_model.ProjectItem, _model.IsWpfView);
        }

        void OnViewNameChanged(object sender, EventArgs e)
        {
             _model.ViewName = _view.ViewName;
        }
       
        void OnRequestingValidation(object sender, EventArgs<bool> e)
        {
            bool validModel = _model.IsValid;
            if (!validModel)
            {
                _view.ShowValidationErrorMessage(_model.ValidationErrorMessage);
            }
            e.Data = validModel;
        }        
    }
}
