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
using Microsoft.Practices.SmartClientFactory.Editors;

namespace Microsoft.Practices.SmartClientFactory.CustomWizardPages
{
    public partial class DefaultBehaviorPage : CustomWizardPage, IDefaultBehaviorPage
    {
        private DefaultBehaviorPagePresenter _presenter;
        
        public event EventHandler<EventArgs> ExpirationChanged;
        public event EventHandler<EventArgs> MaxRetriesChanged;
        public event EventHandler<EventArgs> StampsChanged;
        public event EventHandler<EventArgs> TagChanged;
        public event EventHandler<EventArgs> WizardActivated;
        public event EventHandler<EventArgs<bool>> RequestingValidation;

        public DefaultBehaviorPage()
        {
            InitializeComponent();
        }

        public DefaultBehaviorPage(WizardForm wizard)
            : base(wizard)
        {
            InitializeComponent();
            _stampsTextBox.TextChanged += new EventHandler(_stampsTextBox_TextChanged);
            _maxRetriesTextBox.TextChanged+=new EventHandler(_maxRetriesTextBox_TextChanged);
            _expirationTextBox.TextChanged += new EventHandler(_expirationTextBox_TextChanged);
            _tagTextBox.TextChanged += new EventHandler(_tagTextBox_TextChanged);
            wizard.Activated += new EventHandler(wizard_Activated);
        }

        void wizard_Activated(object sender, EventArgs e)
        {
            if (WizardActivated != null)
            {
                WizardActivated(sender, CreateValidationEventArgs());
            }
            Wizard.OnValidationStateChanged(this);
        }

        void _stampsTextBox_TextChanged(object sender, EventArgs e)
        {
            if (StampsChanged != null)
            {
                StampsChanged(sender, CreateValidationEventArgs());
            }
            Wizard.OnValidationStateChanged(this);
            this.ValidateChildren();
        }

        void _maxRetriesTextBox_TextChanged(object sender, EventArgs e)
        {
            if (MaxRetriesChanged != null)
            {
                MaxRetriesChanged(sender, CreateValidationEventArgs());
            }
            Wizard.OnValidationStateChanged(this);
            this.ValidateChildren();
        }

        void _expirationTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ExpirationChanged != null)
            {
                ExpirationChanged(sender, CreateValidationEventArgs());
            }
            Wizard.OnValidationStateChanged(this);
            this.ValidateChildren();
        }

        void _tagTextBox_TextChanged(object sender, EventArgs e)
        {
            if (TagChanged != null)
            {
                TagChanged(sender, CreateValidationEventArgs());
            }
            this.ValidateChildren();
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
            IDefaultBehaviorPageModel model = new DefaultBehaviorPageModel(dictionary, Site);
            _presenter = new DefaultBehaviorPagePresenter(this, model);
            _presenter.OnViewReady();                       
        }

        public string Expiration
        {
            get { return _expirationTextBox.Text; }
        }

        public string MaxRetries
        {
            get { return _maxRetriesTextBox.Text; }
        }

        public string Stamps
        {
            get { return _stampsTextBox.Text; }
        }

        public string TagValue
        {
            get { return _tagTextBox.Text; }
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

        public void ShowExpiration(string expiration)
        {
            _expirationTextBox.Text=expiration;
        }

        public void ShowMaxRetries(string maxRetries)
        {
            _maxRetriesTextBox.Text=maxRetries;
        }

        public void ShowStamps(string stamps)
        {
            _stampsTextBox.Text=stamps;
        }

        public void ShowTag(string tag)
        {
            _tagTextBox.Text=tag;
        }

        public void ShowProxyFactoryTypeFullName(string proxyFactoryTypeFullName)
        {
            if (proxyFactoryTypeFullName != null)
            {
                if (!_proxyFactoryTypeFullNameComboBox.Items.Contains(proxyFactoryTypeFullName))
                {
                    _proxyFactoryTypeFullNameComboBox.Items.Add(proxyFactoryTypeFullName);
                }
                _proxyFactoryTypeFullNameComboBox.Text = proxyFactoryTypeFullName;
            }
            else
            {
                _proxyFactoryTypeFullNameComboBox.Text = "";
            }
        }

        private void _proxyFactoryTypeFullNameComboBox_TextChanged(object sender, EventArgs e)
        {
            _toolTip.SetToolTip(_proxyFactoryTypeFullNameComboBox, _proxyFactoryTypeFullNameComboBox.Text);
		}
    }    
}
