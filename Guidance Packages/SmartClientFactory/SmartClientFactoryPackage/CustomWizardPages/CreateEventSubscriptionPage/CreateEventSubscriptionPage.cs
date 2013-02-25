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
using Microsoft.Practices.SmartClientFactory.CABCompatibleTypes;
using Microsoft.Practices.WizardFramework;
using Microsoft.Practices.RecipeFramework.Extensions;
using System.ComponentModel.Design;
using Microsoft.Practices.SmartClientFactory.Editors;

namespace Microsoft.Practices.SmartClientFactory.CustomWizardPages
{
    public partial class CreateEventSubscriptionPage : CustomWizardPage, ICreateEventSubscriptionPage
    {
        private CreateEventSubscriptionPagePresenter _presenter;

        public CreateEventSubscriptionPage()
        {
            InitializeComponent();
        }

        public CreateEventSubscriptionPage(WizardForm wizard)
            : base(wizard)
        {
            InitializeComponent();
            _eventTopicComboBox.SelectedValueChanged += new EventHandler(_eventTopicComboBox_SelectedValueChanged);
            _threadingOptionComboBox.SelectedValueChanged += new EventHandler(_threadingOptionComboBox_SelectedValueChanged);
            _eventArgsTextBox.TextChanged += new EventHandler(_eventArgsTextBox_TextChanged);
            _showDocumentation.CheckedChanged += new EventHandler(_showDocumentation_CheckedChanged);
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
            CreateEventSubscriptionPageModel model = new CreateEventSubscriptionPageModel(dictionary, Site);
            _presenter = new CreateEventSubscriptionPagePresenter(this, model,Site);
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

        private void _eventArgsTextBox_TextChanged(object sender, EventArgs e)
        {
            if (EventArgsChanged != null)
            {
                EventArgsChanged(sender, CreateValidationEventArgs());
                Wizard.OnValidationStateChanged(this);
            }
        }

        private void _eventTopicComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (EventTopicChanged != null)
            {
                EventTopicChanged(sender, CreateValidationEventArgs());
                Wizard.OnValidationStateChanged(this);
            }
        }

        private void _threadingOptionComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (ThreadingOptionChanged != null)
            {
                ThreadingOptionChanged(sender, CreateValidationEventArgs());
                Wizard.OnValidationStateChanged(this);
            }
        }

        private void _eventArgsButton_Click(object sender, EventArgs e)
        {
            EventArgsClassBrowserEditor edit = new EventArgsClassBrowserEditor();
            object value = _eventArgsTextBox.Text;
            string eventArgs = edit.EditValue(Site, value).ToString();
            eventArgs = ParseClrNotationToGenericName(eventArgs);
            _eventArgsTextBox.Text = eventArgs;
        }

        private static string ParseClrNotationToGenericName(string eventArgs)
        {
            Type eventArgsType = Type.GetType(eventArgs);
            if (eventArgsType.IsGenericType)
            {
                Type[] genericParameterTypes = eventArgsType.GetGenericArguments();

                StringBuilder parametersBuilder = new StringBuilder("<");
                foreach (Type genericParameterType in genericParameterTypes)
                {
                    parametersBuilder.AppendFormat("{0}, ", genericParameterType.Name);

                }
                parametersBuilder.Length -= 2;
                parametersBuilder.Append(">");
                eventArgs = eventArgs.Substring(0, eventArgs.Length - 2) + parametersBuilder.ToString();
            }
            return eventArgs;
        }

        #region ICreateEventSubscriptionPage Members

        public event EventHandler<EventArgs<bool>> RequestingValidation;
        public event EventHandler<EventArgs> EventTopicChanged;
        public event EventHandler<EventArgs> ThreadingOptionChanged;
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
            get { return _eventTopicComboBox.SelectedItem.ToString(); }
        }

        public ThreadOption ThreadingOption
        {
            get { return (ThreadOption)_threadingOptionComboBox.SelectedItem; }
        }

        public string EventArgs
        {
            get { return _eventArgsTextBox.Text; }
        }

        public void ShowEventArgs(string eventArgs)
        {
            _eventArgsTextBox.Text = eventArgs;
        }

        public void ShowThreadingOption(List<ThreadOption> threadingOptions, ThreadOption selected)
        {
            _threadingOptionComboBox.DataSource = threadingOptions;
            _threadingOptionComboBox.SelectedItem = selected;
        }

        public void ShowEventTopics(List<string> eventTopics)
        {
            _eventTopicComboBox.DataSource = eventTopics;
        }

        #endregion

        
    }
}
