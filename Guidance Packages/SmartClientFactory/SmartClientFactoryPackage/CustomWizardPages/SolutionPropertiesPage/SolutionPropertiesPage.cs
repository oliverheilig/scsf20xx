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
using System.Globalization;

namespace Microsoft.Practices.SmartClientFactory.CustomWizardPages
{
    public partial class SolutionPropertiesPage : CustomWizardPage, ISolutionPropertiesPage
    {
        private SolutionPropertiesPagePresenter _presenter;
        private string _language;

        public SolutionPropertiesPage()
        {
            InitializeComponent();
        }

        public SolutionPropertiesPage(WizardForm wizard)
            : base(wizard)
        {
            InitializeComponent();

            _rootNamespaceTextbox.TextChanged += new EventHandler(_rootNamespaceTextbox_TextChanged);
            _supportingLibrariesTextBox.TextChanged += new EventHandler(OnSupportingLibrariesTextChanged);
            _showDocumentation.CheckedChanged += new EventHandler(_showDocumentation_CheckedChanged);
            _createShellLayoutModule.CheckedChanged += new EventHandler(_createShellLayoutModule_CheckedChanged);
            _supportWPFViews.CheckedChanged += new EventHandler(OnSupportWPFViewsChanging);
            wizard.Activated += new EventHandler(wizard_Activated);
        }

        void _showDocumentation_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowDocumentationChanging != null)
            {
                ShowDocumentationChanging(sender, CreateValidationEventArgs());
            }
        }
        void _createShellLayoutModule_CheckedChanged(object sender, EventArgs e)
        {
            if (CreateShellLayoutModuleChanging != null)
            {
                CreateShellLayoutModuleChanging(sender, CreateValidationEventArgs());
            }

            RefreshSolutionPreview();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitializePresenterAndModel();

            Wizard.OnValidationStateChanged(this);
        }

        void wizard_Activated(object sender, EventArgs e)
        {
            RefreshSupportLibraries();
        }

        void RefreshSupportLibraries()
        {
            OnSupportingLibrariesTextChanged(_supportingLibrariesTextBox, EventArgs.Empty);
        }

        void InitializePresenterAndModel()
        {
            IDictionaryService dictionary = (IDictionaryService)GetService(typeof(IDictionaryService));
            SolutionPropertiesModel model = new SolutionPropertiesModel(dictionary);
            _presenter = new SolutionPropertiesPagePresenter(this, model);
            _presenter.OnViewReady();
        }

        public event EventHandler<EventArgs> SupportLibrariesPathChanging;
        public event EventHandler<EventArgs> RootNamespaceChanging;
        public event EventHandler<EventArgs<bool>> RequestingValidation;
        public event EventHandler<EventArgs> ShowDocumentationChanging;
        public event EventHandler<EventArgs> CreateShellLayoutModuleChanging;
        public event EventHandler<EventArgs> SupportWPFViewsChanging;

        void OnSupportingLibrariesTextChanged(object sender, EventArgs e)
        {
            if (SupportLibrariesPathChanging != null)
            {
                SupportLibrariesPathChanging(sender, CreateValidationEventArgs());
            }
            Wizard.OnValidationStateChanged(this);
            this.ValidateChildren();
        }

        void _rootNamespaceTextbox_TextChanged(object sender, EventArgs e)
        {
            if (RootNamespaceChanging != null)
            {
                RootNamespaceChanging(sender, CreateValidationEventArgs());
            }

            Wizard.OnValidationStateChanged(this);
            this.ValidateChildren();
        }

        protected virtual void OnSupportWPFViewsChanging(object sender, EventArgs e)
        {
            if(SupportWPFViewsChanging != null)
            {
                SupportWPFViewsChanging(sender, e);
            }
        }

        private EventArgs CreateValidationEventArgs()
        {
            return new EventArgs();
        }

        public string SupportLibrariesPath
        {
            get
            {
                return _supportingLibrariesTextBox.Text;
            }
        }

        public bool ShowDocumentation
        {
            get
            {
                return _showDocumentation.Checked;
            }
        }

        public bool CreateShellLayoutModule
        {
            get
            {
                return _createShellLayoutModule.Checked;
            }
        }


        public bool SupportWPFViews
        {
            get 
            {
                return _supportWPFViews.Checked;
            }
        }

        public string RootNamespace
        {
            get
            {
                return _rootNamespaceTextbox.Text;
            }
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

        private void OnBrowseSupportingLibraryClick(object sender, EventArgs e)
        {
			_folderBrowserDialog.SelectedPath = _supportingLibrariesTextBox.Text;
            DialogResult result = _folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                _supportingLibrariesTextBox.Text = _folderBrowserDialog.SelectedPath;
            }
        }


        public void ShowSupportLibraries(string[] libraries, bool[] missing)
        {
            _supportingLibrariesList.Items.Clear();
            for (int i = 0; i < libraries.Length; i++)
            {
                ListViewItem lvi = new ListViewItem(libraries[i]);
                lvi.ForeColor = missing[i] ? Color.Red : Color.FromKnownColor(KnownColor.GrayText);
                _supportingLibrariesList.Items.Add(lvi);
            }
        }

        public void ShowSupportLibrariesPath(string path)
        {
            _supportingLibrariesTextBox.Text = path;
        }

        public void ShowRootNamespace(string rootNamespace)
        {
            _rootNamespaceTextbox.Text = rootNamespace;
        }

        public void SetLanguage(string language)
        {
            _language = language;
        }

        public void ShowShowDocumentation(bool showDocumentation)
        {
            _showDocumentation.Checked = showDocumentation;
        }

        public string Language
        {
            get { return _language; }
        }

        public void RefreshSolutionPreview()
        {
            GenerateAssembliesTree();
        }

        private void GenerateAssembliesTree()
        {
            SolutionPreviewHelper helper = new SolutionPreviewHelper(assembliesTreeView, Language);

            helper.GetSolutionPreview(_createShellLayoutModule.Checked);

            assembliesTreeView.EndUpdate();
        }

    }
}
