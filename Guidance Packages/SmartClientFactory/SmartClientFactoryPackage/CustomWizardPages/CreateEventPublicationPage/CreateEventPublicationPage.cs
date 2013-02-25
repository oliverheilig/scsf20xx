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
using Microsoft.Practices.SmartClientFactory.CABCompatibleTypes;
using Microsoft.Practices.WizardFramework;
using Microsoft.Practices.RecipeFramework.Extensions;
using System.ComponentModel.Design;
using Microsoft.Practices.SmartClientFactory.Editors;

namespace Microsoft.Practices.SmartClientFactory.CustomWizardPages
{
    public partial class CreateEventPublicationPage : CustomWizardPage, ICreateEventPublicationPage
    {
        private CreateEventPublicationPagePresenter _presenter;

        public CreateEventPublicationPage()
        {
            InitializeComponent();
        }

        public CreateEventPublicationPage(WizardForm wizard)
            : base(wizard)
        {
            InitializeComponent();
            _eventTopicTextBox.TextChanged += new EventHandler(_eventTopicTextBox_TextChanged);
            _publicationScopeComboBox.SelectedValueChanged += new EventHandler(_publicationScopeComboBox_SelectedValueChanged);
            _eventArgsTextBox.TextChanged += new EventHandler(_argumentTypeTextBox_TextChanged);
            _showDocumentation.CheckedChanged += new EventHandler(_showDocumentation_CheckedChanged);
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
            CreateEventPublicationPageModel model = new CreateEventPublicationPageModel(dictionary, Site);
            _presenter = new CreateEventPublicationPagePresenter(this, model, Site);
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

        void _showDocumentation_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowDocumentationChanged != null)
            {
                ShowDocumentationChanged(sender, CreateValidationEventArgs());
            }
        }

        private void _argumentTypeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (EventArgsChanged != null)
            {
                EventArgsChanged(sender, CreateValidationEventArgs());
                Wizard.OnValidationStateChanged(this);
            }
        }

        private void _eventTopicTextBox_TextChanged(object sender, EventArgs e)
        {
            if (EventTopicChanged != null)
            {
                EventTopicChanged(sender, CreateValidationEventArgs());
                Wizard.OnValidationStateChanged(this);
            }
        }

        private void _publicationScopeComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (PublicationScopeChanged != null)
            {
                PublicationScopeChanged(sender, CreateValidationEventArgs());
                Wizard.OnValidationStateChanged(this);
            }
        }

        private void _eventArgsButton_Click(object sender, EventArgs e)
        {
            EventArgsClassBrowserEditor edit = new EventArgsClassBrowserEditor();
            object value = _eventArgsTextBox.Text;
            string eventArgs = edit.EditValue(Site, value).ToString();
            eventArgs = NotationHelper.ParseClrNotationToGenericName(eventArgs);
            _eventArgsTextBox.Text = eventArgs;
        }

        #region ICreateEventPublicationPage Members

        public event EventHandler<EventArgs<bool>> RequestingValidation;
        public event EventHandler<EventArgs> EventTopicChanged;
        public event EventHandler<EventArgs> PublicationScopeChanged;
        public event EventHandler<EventArgs> EventArgsChanged;
        public event EventHandler<EventArgs> ShowDocumentationChanged;

        public void ShowShowDocumentation(bool showDocumentation)
        {
            _showDocumentation.Checked = showDocumentation;
        }

        public bool ShowDocumentation
        {
            get { return _showDocumentation.Checked; }
            set { _showDocumentation.Checked = value; }
        }

        public string EventTopic
        {
            get { return _eventTopicTextBox.Text; }
        }

        public PublicationScope PublicationScope
        {
            get { return (PublicationScope)_publicationScopeComboBox.SelectedItem; }
        }

        public string EventArgs
        {
            get { return _eventArgsTextBox.Text; }
        }

        public void ShowEventArgs(string eventArgs)
        {
            _eventArgsTextBox.Text = eventArgs;
        }

        public void ShowPublicationScope(List<PublicationScope> publicationScopes, PublicationScope selected)
        {
            _publicationScopeComboBox.DataSource = publicationScopes;
            _publicationScopeComboBox.SelectedItem = selected;
        }

        #endregion

        
    }
}
