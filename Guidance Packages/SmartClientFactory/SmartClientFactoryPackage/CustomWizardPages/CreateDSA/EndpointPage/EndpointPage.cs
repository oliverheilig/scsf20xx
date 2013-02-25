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
using System.Reflection;
using Microsoft.Practices.SmartClientFactory.Editors;
using Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WinForms;
using Microsoft.Practices.SmartClientFactory.Properties;

namespace Microsoft.Practices.SmartClientFactory.CustomWizardPages
{
    public partial class EndpointPage : CustomWizardPage, IEndpointPage
    {
        private EndpointPagePresenter _presenter;
        private string _language;
        private string _disconnectedAgentsFolder;
        private string _proxyFolder;
        private string _agentFileName;
        private string _agentCallbackFileName;
        private string _agentCallbackBaseFileName;
        private IProjectModel _activeModuleProject;
        private string _proxyFactoryTypeFullName;


        public event EventHandler<EventArgs> EndpointChanged;
        public event EventHandler<EventArgs> ProxyTypeChanged;
        public event EventHandler<EventArgs> MethodsChanged;
        public event EventHandler<EventArgs> ExpirationChanged;
        public event EventHandler<EventArgs> MaxRetriesChanged;
        public event EventHandler<EventArgs> StampsChanged;
        public event EventHandler<EventArgs> TagChanged;
        public event EventHandler<EventArgs> ShowDocumentationChanged;
        public event EventHandler<EventArgs<bool>> RequestingValidation;

		public EndpointPage()
		{
			InitializeComponent();
		}

        public EndpointPage(WizardForm wizard)
            : base(wizard)
        {
            InitializeComponent();
            _endpointTextBox.TextChanged += new System.EventHandler(_endpointTextBox_TextChanged);
			_methodsListView.ItemChecked += new ItemCheckedEventHandler(_methodsListView_ItemChecked);
            _proxyTypeNameTextBox.TextChanged += new EventHandler(_proxyTypeTextBox_TextChanged);
            _proxyTypeButton.Click += new EventHandler(_proxyTypeButton_Click);
            _stampsTextBox.TextChanged += new EventHandler(_stampsTextBox_TextChanged);
            _maxRetriesTextBox.TextChanged += new EventHandler(_maxRetriesTextBox_TextChanged);
            _expirationTextBox.TextChanged += new EventHandler(_expirationTextBox_TextChanged);
            _tagTextBox.TextChanged += new EventHandler(_tagTextBox_TextChanged);

            _showDocumentation.CheckedChanged += new EventHandler(_showDocumentation_CheckedChanged);
        }

        void _showDocumentation_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowDocumentationChanged != null)
            {
                ShowDocumentationChanged(sender, CreateValidationEventArgs());
            }
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
        
        void _proxyTypeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ProxyTypeChanged != null)
                ProxyTypeChanged(sender, CreateValidationEventArgs());

            Wizard.OnValidationStateChanged(this);
        }

        void _proxyTypeButton_Click(object sender, EventArgs e)
        {
            AnyPublicClassBrowserEditor edit = new AnyPublicClassBrowserEditor();
            object value = _proxyTypeNameTextBox.Text;
            string proxyTypeFullName = edit.EditValue(Site, value).ToString();
            proxyTypeFullName = NotationHelper.ParseClrNotationToGenericName(proxyTypeFullName);
            _proxyTypeNameTextBox.Text = proxyTypeFullName;
        }

        void _methodsListView_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			if (MethodsChanged != null)
				MethodsChanged(sender, CreateValidationEventArgs());

			Wizard.OnValidationStateChanged(this);
		}

        private void _endpointTextBox_TextChanged(object sender, EventArgs e)
        {
            if (EndpointChanged != null)
                EndpointChanged(sender, CreateValidationEventArgs());

            Wizard.OnValidationStateChanged(this);
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
            DTE dte = (DTE)GetService(typeof(DTE));
            EndpointPageModel model = new EndpointPageModel(dictionary,Site);
            _presenter = new EndpointPagePresenter(this, model);
            _presenter.OnViewReady();                       
        }

        public string Endpoint
        {
            get { return _endpointTextBox.Text; }
        }

        public bool ShowDocumentation
        {
            get { return _showDocumentation.Checked; }
            set { _showDocumentation.Checked = value; }
        }

        public string ProxyTypeName
        {
            get { return _proxyTypeNameTextBox.Text; }
        }

		public List<MethodInfo> Methods
		{
			get
			{
				List<MethodInfo> methods = new List<MethodInfo>();
				foreach (ListViewItem item in _methodsListView.CheckedItems)
				{
					methods.Add(item.Tag as MethodInfo);
				}

				return methods;
			}
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

        public void ShowShowDocumentation(bool showDocumentation)
        {
            _showDocumentation.Checked = showDocumentation;
        }

        public void ShowEndpoint(string endpoint)
        {
            _endpointTextBox.Text = endpoint;
        }
        public void ShowProxyType(Type proxyType)
        {
            if (proxyType != null)
            {
                _proxyTypeNameTextBox.Text = NotationHelper.ParseClrNotationToGenericName(proxyType.FullName);
            }
            else
            {
                _proxyTypeNameTextBox.Text = "";
            }
        }

		public void ShowMethods(List<MethodInfo> methods, List<MethodInfo> selectedMethods)
		{
			_methodsListView.Items.Clear();

			if (methods != null)
			{
				foreach (MethodInfo method in methods)
				{
					ListViewItem lvi = new ListViewItem(new string[]{method.ToString()});
					lvi.Tag = method;
					_methodsListView.Items.Add(lvi);

					if (selectedMethods != null)
					{
						lvi.Checked = selectedMethods.Contains(lvi.Tag as MethodInfo);
					}
				}
			}

			_methodsListView.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
		}

        public void ShowExpiration(string expiration)
        {
            _expirationTextBox.Text = expiration;
        }

        public void ShowMaxRetries(string maxRetries)
        {
            _maxRetriesTextBox.Text = maxRetries;
        }

        public void ShowStamps(string stamps)
        {
            _stampsTextBox.Text = stamps;
        }

        public void ShowTag(string tag)
        {
            _tagTextBox.Text = tag;
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
                _proxyFactoryTypeFullName = proxyFactoryTypeFullName;
            }
            else
            {
                _proxyFactoryTypeFullNameComboBox.Text = "";
            }
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

        public void SetLanguage(string language)
        {
            _language = language;
        }

        public string Language
        {
            get
            {
                return _language;
            }
        }

        public void RefreshSolutionPreview(string disconnectedAgentsFolder,
                                        string proxyFolder,
                                        string agentFileName,
                                        string agentCallbackFileName,
                                        string agentCallbackBaseFileName,
                                        IProjectModel activeModuleProject)
        {
            _disconnectedAgentsFolder = disconnectedAgentsFolder;
            _proxyFolder = proxyFolder;
            _agentFileName = agentFileName;
            _agentCallbackFileName = agentCallbackFileName;
            _agentCallbackBaseFileName = agentCallbackFileName;
            _activeModuleProject = activeModuleProject;
            GenerateAssembliesTree();
        }

        private void GenerateAssembliesTree()
        {
            SolutionPreviewHelper helper = new SolutionPreviewHelper(assembliesTreeView, Language);

            helper.GetDisconnectedServiceAgentPreview(_proxyFolder, _disconnectedAgentsFolder, _agentFileName, _agentCallbackFileName, _activeModuleProject);
        }

        public void ShowNotBuildPanel(bool built, bool existsProxyClass, string proxyTypeName)
        {
            _solutionNotBuiltSplitter.Panel1Collapsed = built;
            _solutionNotBuiltSplitter.Panel2Collapsed = !built;

            _endpointTextBox.Enabled = built;
            _proxyTypeNameTextBox.Enabled = built;
            _proxyTypeButton.Enabled = built;
            _methodsListGroupBox.Enabled = built;
            _solutionPreviewGroupBox.Enabled = built;

            _proxyClassNotFoundLabel.Visible = !existsProxyClass;
            _proxyClassNotFoundLabel.Text = string.Format(CultureInfo.CurrentCulture, Resources.ProxyClassnotFoundMessage, proxyTypeName);
        }

        private void _maxRetriesLabel_Click(object sender, EventArgs e)
        {

        }

        private void _maxRetriesTextBox_TextChanged_1(object sender, EventArgs e)
        {

        }
    }    
}
