namespace Microsoft.Practices.SmartClientFactory.CustomWizardPages
{
    partial class CreateEventPublicationPage
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
            this._eventTopicTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this._eventArgsTextBox = new System.Windows.Forms.TextBox();
            this._publicationScopeLabel = new System.Windows.Forms.Label();
            this._eventArgsButton = new System.Windows.Forms.Button();
            this._publicationScopeComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.eventPublicationValidationProvider = new Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WinForms.ValidationProvider();
            this._showDocumentation = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // infoPanel
            // 
            this.infoPanel.AutoSize = false;
            this.infoPanel.Dock = System.Windows.Forms.DockStyle.None;
            this.infoPanel.Location = new System.Drawing.Point(489, 133);
            this.infoPanel.Size = new System.Drawing.Size(24, 21);
            // 
            // _eventTopicTextBox
            // 
            this._eventTopicTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._errorProvider.SetIconPadding(this._eventTopicTextBox, -20);
            this._eventTopicTextBox.Location = new System.Drawing.Point(17, 28);
            this._eventTopicTextBox.Name = "_eventTopicTextBox";
            this.eventPublicationValidationProvider.SetPerformValidation(this._eventTopicTextBox, true);
            this._eventTopicTextBox.Size = new System.Drawing.Size(666, 20);
            this.eventPublicationValidationProvider.SetSourcePropertyName(this._eventTopicTextBox, "EventTopic");
            this._eventTopicTextBox.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Event Topic Name:";
            // 
            // _errorProvider
            // 
            this._errorProvider.ContainerControl = this;
            // 
            // _eventArgsTextBox
            // 
            this._eventArgsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._errorProvider.SetIconPadding(this._eventArgsTextBox, -20);
            this._eventArgsTextBox.Location = new System.Drawing.Point(17, 107);
            this._eventArgsTextBox.Name = "_eventArgsTextBox";
            this.eventPublicationValidationProvider.SetPerformValidation(this._eventArgsTextBox, true);
            this._eventArgsTextBox.Size = new System.Drawing.Size(633, 20);
            this.eventPublicationValidationProvider.SetSourcePropertyName(this._eventArgsTextBox, "EventArgs");
            this._eventArgsTextBox.TabIndex = 5;
            // 
            // _publicationScopeLabel
            // 
            this._publicationScopeLabel.AutoSize = true;
            this._publicationScopeLabel.Location = new System.Drawing.Point(14, 51);
            this._publicationScopeLabel.Name = "_publicationScopeLabel";
            this._publicationScopeLabel.Size = new System.Drawing.Size(96, 13);
            this._publicationScopeLabel.TabIndex = 4;
            this._publicationScopeLabel.Text = "Publication Scope:";
            // 
            // _eventArgsButton
            // 
            this._eventArgsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._eventArgsButton.Location = new System.Drawing.Point(656, 107);
            this._eventArgsButton.Name = "_eventArgsButton";
            this._eventArgsButton.Size = new System.Drawing.Size(27, 20);
            this._eventArgsButton.TabIndex = 6;
            this._eventArgsButton.Text = "...";
            this._eventArgsButton.UseVisualStyleBackColor = true;
            this._eventArgsButton.Click += new System.EventHandler(this._eventArgsButton_Click);
            // 
            // _publicationScopeComboBox
            // 
            this._publicationScopeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._publicationScopeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._publicationScopeComboBox.FormattingEnabled = true;
            this._publicationScopeComboBox.Location = new System.Drawing.Point(17, 67);
            this._publicationScopeComboBox.Name = "_publicationScopeComboBox";
            this._publicationScopeComboBox.Size = new System.Drawing.Size(666, 21);
            this._publicationScopeComboBox.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Event Argument Type:";
            // 
            // eventPublicationValidationProvider
            // 
            this.eventPublicationValidationProvider.ErrorProvider = this._errorProvider;
            this.eventPublicationValidationProvider.RulesetName = "";
            this.eventPublicationValidationProvider.SourceTypeName = "Microsoft.Practices.SmartClientFactory.CustomWizardPages.CreateEventPublicationPa" +
                "geModel, Microsoft.Practices.SmartClientFactory.GuidancePackage";
            // 
            // _showDocumentation
            // 
            this._showDocumentation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._showDocumentation.AutoSize = true;
            this._showDocumentation.Location = new System.Drawing.Point(17, 133);
            this._showDocumentation.Name = "_showDocumentation";
            this._showDocumentation.Size = new System.Drawing.Size(233, 17);
            this._showDocumentation.TabIndex = 8;
            this._showDocumentation.Text = "Show documentation after recipe completes";
            this._showDocumentation.UseVisualStyleBackColor = true;
            this._showDocumentation.Visible = false;
            // 
            // CreateEventPublicationPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.Controls.Add(this._showDocumentation);
            this.Controls.Add(this._eventTopicTextBox);
            this.Controls.Add(this._publicationScopeComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._eventArgsButton);
            this.Controls.Add(this._eventArgsTextBox);
            this.Controls.Add(this._publicationScopeLabel);
            this.Name = "CreateEventPublicationPage";
            this.Size = new System.Drawing.Size(700, 179);
            this.Controls.SetChildIndex(this._publicationScopeLabel, 0);
            this.Controls.SetChildIndex(this._eventArgsTextBox, 0);
            this.Controls.SetChildIndex(this._eventArgsButton, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this._publicationScopeComboBox, 0);
            this.Controls.SetChildIndex(this.infoPanel, 0);
            this.Controls.SetChildIndex(this._eventTopicTextBox, 0);
            this.Controls.SetChildIndex(this._showDocumentation, 0);
            ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _eventTopicTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider _errorProvider;
        private System.Windows.Forms.Button _eventArgsButton;
        private System.Windows.Forms.TextBox _eventArgsTextBox;
        private System.Windows.Forms.Label _publicationScopeLabel;
        private System.Windows.Forms.ComboBox _publicationScopeComboBox;
        private System.Windows.Forms.Label label2;
        private Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WinForms.ValidationProvider eventPublicationValidationProvider;
		private System.Windows.Forms.CheckBox _showDocumentation;
    }
}
