namespace Microsoft.Practices.SmartClientFactory.CustomWizardPages
{
    partial class DefaultBehaviorPage
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this._errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this._stampsTextBox = new System.Windows.Forms.TextBox();
			this._maxRetriesTextBox = new System.Windows.Forms.TextBox();
			this._tagTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this._expirationTextBox = new System.Windows.Forms.MaskedTextBox();
			this._toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.defaultBehaviorValidationProvider = new Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WinForms.ValidationProvider();
			this._proxyFactoryTypeFullNameLabel = new System.Windows.Forms.Label();
			this._proxyFactoryTypeFullNameComboBox = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this._errorProvider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// infoPanel
			// 
			this.infoPanel.AutoSize = false;
			this.infoPanel.Dock = System.Windows.Forms.DockStyle.None;
			this.infoPanel.Location = new System.Drawing.Point(13, 336);
			this.infoPanel.Margin = new System.Windows.Forms.Padding(4);
			this.infoPanel.Size = new System.Drawing.Size(38, 24);
			this.defaultBehaviorValidationProvider.SetSourcePropertyName(this.infoPanel, null);
			// 
			// _errorProvider
			// 
			this._errorProvider.ContainerControl = this;
			// 
			// _stampsTextBox
			// 
			this._stampsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._errorProvider.SetIconPadding(this._stampsTextBox, -20);
			this._stampsTextBox.Location = new System.Drawing.Point(10, 22);
			this._stampsTextBox.Name = "_stampsTextBox";
			this.defaultBehaviorValidationProvider.SetPerformValidation(this._stampsTextBox, true);
			this._stampsTextBox.Size = new System.Drawing.Size(678, 20);
			this.defaultBehaviorValidationProvider.SetSourcePropertyName(this._stampsTextBox, "Stamps");
			this._stampsTextBox.TabIndex = 2;
			this._stampsTextBox.Text = "1";
			this._toolTip.SetToolTip(this._stampsTextBox, "Sets the default number of stamps for the requests. \r\nStamps are used to determin" +
					"e under which connectivity conditions the request will get dispatched.");
			// 
			// _maxRetriesTextBox
			// 
			this._maxRetriesTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._errorProvider.SetIconPadding(this._maxRetriesTextBox, -20);
			this._maxRetriesTextBox.Location = new System.Drawing.Point(10, 61);
			this._maxRetriesTextBox.Name = "_maxRetriesTextBox";
			this.defaultBehaviorValidationProvider.SetPerformValidation(this._maxRetriesTextBox, true);
			this._maxRetriesTextBox.Size = new System.Drawing.Size(678, 20);
			this.defaultBehaviorValidationProvider.SetSourcePropertyName(this._maxRetriesTextBox, "MaxRetries");
			this._maxRetriesTextBox.TabIndex = 3;
			this._maxRetriesTextBox.Text = "0";
			this._toolTip.SetToolTip(this._maxRetriesTextBox, "Amount of times before firing the exception callback when the actual web service " +
					"call fails.");
			// 
			// _tagTextBox
			// 
			this._tagTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._errorProvider.SetIconPadding(this._tagTextBox, -20);
			this._tagTextBox.Location = new System.Drawing.Point(10, 139);
			this._tagTextBox.Name = "_tagTextBox";
			this.defaultBehaviorValidationProvider.SetPerformValidation(this._tagTextBox, true);
			this._tagTextBox.Size = new System.Drawing.Size(678, 20);
			this.defaultBehaviorValidationProvider.SetSourcePropertyName(this._tagTextBox, "Tag");
			this._tagTextBox.TabIndex = 5;
			this._toolTip.SetToolTip(this._tagTextBox, "Default Tag for the requests, used for filtering elements from the queue.");
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(42, 13);
			this.defaultBehaviorValidationProvider.SetSourcePropertyName(this.label1, null);
			this.label1.TabIndex = 6;
			this.label1.Text = "Stamps";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(10, 45);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 13);
			this.defaultBehaviorValidationProvider.SetSourcePropertyName(this.label2, null);
			this.label2.TabIndex = 6;
			this.label2.Text = "Max Retries";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(10, 84);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 13);
			this.defaultBehaviorValidationProvider.SetSourcePropertyName(this.label3, null);
			this.label3.TabIndex = 6;
			this.label3.Text = "Expiration";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(10, 123);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(26, 13);
			this.defaultBehaviorValidationProvider.SetSourcePropertyName(this.label4, null);
			this.label4.TabIndex = 6;
			this.label4.Text = "Tag";
			// 
			// _expirationTextBox
			// 
			this._expirationTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._expirationTextBox.Culture = new System.Globalization.CultureInfo("");
			this._errorProvider.SetIconPadding(this._expirationTextBox, -20);
			this._expirationTextBox.Location = new System.Drawing.Point(10, 100);
			this._expirationTextBox.Mask = "00\\.00:00:00";
			this._expirationTextBox.Name = "_expirationTextBox";
			this.defaultBehaviorValidationProvider.SetPerformValidation(this._expirationTextBox, true);
			this._expirationTextBox.Size = new System.Drawing.Size(678, 20);
			this.defaultBehaviorValidationProvider.SetSourcePropertyName(this._expirationTextBox, "Expiration");
			this._expirationTextBox.TabIndex = 4;
			this._expirationTextBox.Text = "00000030";
			this._toolTip.SetToolTip(this._expirationTextBox, "Expiration time for the request, in days.hours:minutes:seconds format.");
			// 
			// _toolTip
			// 
			this._toolTip.AutoPopDelay = 2000;
			this._toolTip.InitialDelay = 500;
			this._toolTip.ReshowDelay = 100;
			// 
			// defaultBehaviorValidationProvider
			// 
			this.defaultBehaviorValidationProvider.ErrorProvider = this._errorProvider;
			this.defaultBehaviorValidationProvider.RulesetName = "";
			this.defaultBehaviorValidationProvider.SourceTypeName = "Microsoft.Practices.SmartClientFactory.CustomWizardPages.DefaultBehaviorPageModel" +
				", Microsoft.Practices.SmartClientFactory.GuidancePackage";
			// 
			// _proxyFactoryTypeFullNameLabel
			// 
			this._proxyFactoryTypeFullNameLabel.AutoSize = true;
			this._proxyFactoryTypeFullNameLabel.Location = new System.Drawing.Point(10, 162);
			this._proxyFactoryTypeFullNameLabel.Name = "_proxyFactoryTypeFullNameLabel";
			this._proxyFactoryTypeFullNameLabel.Size = new System.Drawing.Size(101, 13);
			this.defaultBehaviorValidationProvider.SetSourcePropertyName(this._proxyFactoryTypeFullNameLabel, null);
			this._proxyFactoryTypeFullNameLabel.TabIndex = 23;
			this._proxyFactoryTypeFullNameLabel.Text = "Proxy Factory Type:";
			// 
			// _proxyFactoryTypeFullNameComboBox
			// 
			this._proxyFactoryTypeFullNameComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._proxyFactoryTypeFullNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._proxyFactoryTypeFullNameComboBox.FormattingEnabled = true;
			this._errorProvider.SetIconPadding(this._proxyFactoryTypeFullNameComboBox, -20);
			this._proxyFactoryTypeFullNameComboBox.Items.AddRange(new object[] {
            "Microsoft.Practices.SmartClient.DisconnectedAgent.WebServiceProxyFactory",
            "Microsoft.Practices.SmartClient.DisconnectedAgent.WCFProxyFactory",
            "Microsoft.Practices.SmartClient.DisconnectedAgent.ObjectProxyFactory"});
			this._proxyFactoryTypeFullNameComboBox.Location = new System.Drawing.Point(10, 179);
			this._proxyFactoryTypeFullNameComboBox.Name = "_proxyFactoryTypeFullNameComboBox";
			this.defaultBehaviorValidationProvider.SetPerformValidation(this._proxyFactoryTypeFullNameComboBox, true);
			this._proxyFactoryTypeFullNameComboBox.Size = new System.Drawing.Size(678, 21);
			this.defaultBehaviorValidationProvider.SetSourcePropertyName(this._proxyFactoryTypeFullNameComboBox, "ProxyFactoryTypeFullName");
			this._proxyFactoryTypeFullNameComboBox.TabIndex = 26;
			this._proxyFactoryTypeFullNameComboBox.TextChanged += new System.EventHandler(this._proxyFactoryTypeFullNameComboBox_TextChanged);
			// 
			// DefaultBehaviorPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
			this.Controls.Add(this._proxyFactoryTypeFullNameComboBox);
			this.Controls.Add(this._proxyFactoryTypeFullNameLabel);
			this.Controls.Add(this._stampsTextBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this._maxRetriesTextBox);
			this.Controls.Add(this._expirationTextBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this._tagTextBox);
			this.Controls.Add(this.label4);
			this.Name = "DefaultBehaviorPage";
			this.Size = new System.Drawing.Size(708, 364);
			this.defaultBehaviorValidationProvider.SetSourcePropertyName(this, null);
			this.Controls.SetChildIndex(this.label4, 0);
			this.Controls.SetChildIndex(this._tagTextBox, 0);
			this.Controls.SetChildIndex(this.label3, 0);
			this.Controls.SetChildIndex(this._expirationTextBox, 0);
			this.Controls.SetChildIndex(this._maxRetriesTextBox, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this._stampsTextBox, 0);
			this.Controls.SetChildIndex(this.infoPanel, 0);
			this.Controls.SetChildIndex(this._proxyFactoryTypeFullNameLabel, 0);
			this.Controls.SetChildIndex(this._proxyFactoryTypeFullNameComboBox, 0);
			((System.ComponentModel.ISupportInitialize)(this._errorProvider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ErrorProvider _errorProvider;
        private System.Windows.Forms.TextBox _tagTextBox;
        private System.Windows.Forms.TextBox _maxRetriesTextBox;
        private System.Windows.Forms.TextBox _stampsTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox _expirationTextBox;
        private System.Windows.Forms.ToolTip _toolTip;
        private Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WinForms.ValidationProvider defaultBehaviorValidationProvider;
        private System.Windows.Forms.Label _proxyFactoryTypeFullNameLabel;
        private System.Windows.Forms.ComboBox _proxyFactoryTypeFullNameComboBox;
    }
}
