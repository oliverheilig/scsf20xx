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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.WizardFramework;
using System.ComponentModel.Design;
using Microsoft.Practices.RecipeFramework.Extensions;
using Microsoft.Practices.RecipeFramework.Extensions.DteWrapper;
using EnvDTE;
using System.Globalization;
using System.IO;

namespace Microsoft.Practices.SmartClientFactory.CustomWizardPages
{
    public partial class CreateViewPage : CustomWizardPage, ICreateViewPage
    {
        private CreateViewPagePresenter _presenter;
        private string _language;
        private IProjectModel _activeModuleProject;
        private IProjectItemModel _activeProjectItem;
        private bool _testProjectExists;
        private bool _isWpfView;

        public event EventHandler<EventArgs> ViewNameChanged;
        public event EventHandler<EventArgs<bool>> RequestingValidation;
        public event EventHandler<EventArgs> ShowDocumentationChanged;
        public event EventHandler<EventArgs> CreateViewFolderChanged;

        public CreateViewPage()
        {
            InitializeComponent();
        }

        public CreateViewPage(WizardForm wizard)
            : base(wizard)
        {
            InitializeComponent();
            _showDocumentation.CheckedChanged += new EventHandler(_showDocumentation_CheckedChanged);
            _createViewFolder.CheckedChanged += new EventHandler(_createViewFolder_CheckedChanged);
            _viewNameTextBox.TextChanged += new EventHandler(_viewNameTextBox_TextChanged);
        }

        void _showDocumentation_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowDocumentationChanged != null)
            {
                ShowDocumentationChanged(sender, CreateValidationEventArgs());
            }
        }

        void _createViewFolder_CheckedChanged(object sender, EventArgs e)
        {
            EventArgs _validationEventArgs = CreateValidationEventArgs();
            if (CreateViewFolderChanged != null)
            {
                CreateViewFolderChanged(sender, _validationEventArgs);
            }
            if (ViewNameChanged != null)
            {
                ViewNameChanged(_viewNameTextBox, _validationEventArgs);
            }

            this.ValidateChildren();
            Wizard.OnValidationStateChanged(this);

            GenerateAssembliesTree();
        }

        private void _viewNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ViewNameChanged != null)
                ViewNameChanged(sender, CreateValidationEventArgs());

            this.ValidateChildren();
            Wizard.OnValidationStateChanged(this);

            GenerateAssembliesTree();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitializePresenterAndModel();

            Wizard.OnValidationStateChanged(this);
        }

        void InitializePresenterAndModel()
        {
            IDictionaryService dictionary = (IDictionaryService)GetService(typeof(IDictionaryService));
            DTE dte = (DTE)GetService(typeof(DTE));
            CreateViewPageModel model = new CreateViewPageModel(dictionary, Site);
            _presenter = new CreateViewPagePresenter(this, model);
            _presenter.OnViewReady();
        }

        public string ViewName
        {
            get { return _viewNameTextBox.Text; }
        }

        public bool TestProjectExists
        {
            set { _testProjectExists = value; }
        }

        public bool ShowDocumentation
        {
            get { return _showDocumentation.Checked; }
            set { _showDocumentation.Checked = value; }
        }

        public bool CreateViewFolder
        {
            get { return _createViewFolder.Checked; }
            set { _createViewFolder.Checked = value; }
        }


        public void ShowViewName(string viewName)
        {
            _viewNameTextBox.Text = viewName;
        }

        public void SetLanguage(string language)
        {
            _language = language;
        }

        public string Language
        {
            get { return _language; }
        }

        private EventArgs CreateValidationEventArgs()
        {
            return new EventArgs();
        }

        public override bool IsDataValid
        {
            get
            {
                EventArgs<bool> args = new EventArgs<bool>();
                if (RequestingValidation != null)
                {
                    RequestingValidation(this, args);
                    return args.Data;
                }
                return false;
            }
        }

        public void RefreshSolutionPreview(IProjectModel activeModuleProject, IProjectItemModel activeProjectItem, bool isWpfView)
        {
            _activeModuleProject = activeModuleProject;
            _activeProjectItem = activeProjectItem;
            _isWpfView = isWpfView;

            GenerateAssembliesTree();
        }

        private void GenerateAssembliesTree()
        {
            SolutionPreviewHelper helper = new SolutionPreviewHelper(assembliesTreeView, Language);

            helper.GetViewPreview(CreateViewFolder, ViewName, _testProjectExists, _activeModuleProject, _activeProjectItem, _isWpfView);
        }

        public void ShowValidationErrorMessage(string errorMessage)
        {
            _errorProvider.SetError(_viewNameTextBox, errorMessage);
        }
    }
}
