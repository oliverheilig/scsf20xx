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
using Microsoft.Practices.RecipeFramework.Extensions;
using System.ComponentModel.Design;
using System.Globalization;

namespace Microsoft.Practices.SmartClientFactory.CustomWizardPages
{
    public partial class CreateFoundationalModulePage : CustomWizardPage, ICreateFoundationalModulePage
    {
        private CreateFoundationalModulePagePresenter _presenter;
        private string _moduleName;
        private string _language;

        public CreateFoundationalModulePage()
        {
            InitializeComponent();
        }

        public CreateFoundationalModulePage(WizardForm wizard)
            : base(wizard)
        {
            InitializeComponent();

            _createModuleInterfaceLibrary.CheckedChanged += new EventHandler(_createModuleInterfaceLibrary_CheckedChanged);
            _createLayoutModule.CheckedChanged += new EventHandler(_createLayoutModule_CheckedChanged);
            _showDocumentation.CheckedChanged += new EventHandler(_showDocumentation_CheckedChanged);
            _createTestProject.CheckedChanged += new EventHandler(_createTestProject_CheckedChanged);
        }


        void _createLayoutModule_CheckedChanged(object sender, EventArgs e)
        {
            if (CreateLayoutModuleChanged != null)
            {
                CreateLayoutModuleChanged(sender, CreateValidationEventArgs());
            }

            GenerateAssembliesTree();
        }

        void _createTestProject_CheckedChanged(object sender, EventArgs e)
        {
            if (CreateTestProjectChanged != null)
            {
                CreateTestProjectChanged(sender, CreateValidationEventArgs());
            }

            GenerateAssembliesTree();
        }

        void _createModuleInterfaceLibrary_CheckedChanged(object sender, EventArgs e)
        {
            if (CreateModuleInterfaceLibraryChanged != null)
            {
                CreateModuleInterfaceLibraryChanged(sender, CreateValidationEventArgs());
            }

            GenerateAssembliesTree();
        }

        void _showDocumentation_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowDocumentationChanged != null)
            {
                ShowDocumentationChanged(sender, CreateValidationEventArgs());
            }
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
            CreateFoundationalModulePageModel model = new CreateFoundationalModulePageModel(dictionary);
            _presenter = new CreateFoundationalModulePagePresenter(this, model);
            _presenter.OnViewReady();
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

        #region ICreateFoundationalModulePage Members

        public event EventHandler<EventArgs> CreateModuleInterfaceLibraryChanged;
        public event EventHandler<EventArgs> CreateLayoutModuleChanged;
        public event EventHandler<EventArgs> ShowDocumentationChanged;
        public event EventHandler<EventArgs> CreateTestProjectChanged;
        public event EventHandler<EventArgs<bool>> RequestingValidation;

        public void ShowModuleNamespace(string moduleNamespace)
        {
            _moduleNamespaceTextBox.Text = moduleNamespace;
        }

        public bool CreateLayoutModule
        {
            get { return _createLayoutModule.Checked; }
        }

        public bool CreateModuleInterfaceLibrary
        {
            get { return _createModuleInterfaceLibrary.Checked; }
        }

        public bool ShowDocumentation
        {
            get { return _showDocumentation.Checked; }
        }

        public bool CreateTestProject
        {
            get { return _createTestProject.Checked; }
        }

        public void SetModuleName(string moduleName)
        {
            _moduleName = moduleName;
        }

        public void SetLanguage(string language)
        {
            _language = language;
        }

        public string ModuleName
        {
            get { return _moduleName; }
        }

        public string Language
        {
            get { return _language; }
        }

        public void RefreshSolutionPreview()
        {
            GenerateAssembliesTree();
        }

        #endregion

        private void GenerateAssembliesTree()
        {
            SolutionPreviewHelper helper = new SolutionPreviewHelper(assembliesTreeView, Language);

            helper.GetFoundationalModulePreview(_createModuleInterfaceLibrary.Checked, _createLayoutModule.Checked, _createTestProject.Checked, ModuleName);
        }
    }
}
