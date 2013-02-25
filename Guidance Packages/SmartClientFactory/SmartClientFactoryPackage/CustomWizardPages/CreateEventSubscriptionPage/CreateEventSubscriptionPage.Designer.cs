namespace Microsoft.Practices.SmartClientFactory.CustomWizardPages
{
    partial class CreateEventSubscriptionPage
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
            this.label1 = new System.Windows.Forms.Label();
            this._errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this._eventArgsTextBox = new System.Windows.Forms.TextBox();
            this._eventTopicComboBox = new System.Windows.Forms.ComboBox();
            this._threadingOptionLabel = new System.Windows.Forms.Label();
            this._eventArgsButton = new System.Windows.Forms.Button();
            this._threadingOptionComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.eventSubscriptionValidationProvider = new Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WinForms.ValidationProvider();
            this._showDocumentation = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // infoPanel
            // 
            this.infoPanel.AutoSize = false;
            this.infoPanel.Dock = System.Windows.Forms.DockStyle.None;
            this.infoPanel.Location = new System.Drawing.Point(552, 133);
            this.infoPanel.Size = new System.Drawing.Size(24, 21);
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
            this.eventSubscriptionValidationProvider.SetPerformValidation(this._eventArgsTextBox, true);
            this._eventArgsTextBox.Size = new System.Drawing.Size(633, 20);
            this.eventSubscriptionValidationProvider.SetSourcePropertyName(this._eventArgsTextBox, "EventArgs");
            this._eventArgsTextBox.TabIndex = 5;
            // 
            // _eventTopicComboBox
            // 
            this._eventTopicComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._eventTopicComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._eventTopicComboBox.FormattingEnabled = true;
            this._errorProvider.SetIconPadding(this._eventTopicComboBox, -37);
            this._eventTopicComboBox.Location = new System.Drawing.Point(17, 27);
            this._eventTopicComboBox.Name = "_eventTopicComboBox";
            this.eventSubscriptionValidationProvider.SetPerformValidation(this._eventTopicComboBox, true);
            this._eventTopicComboBox.Size = new System.Drawing.Size(666, 21);
            this.eventSubscriptionValidationProvider.SetSourcePropertyName(this._eventTopicComboBox, "EventTopic");
            this._eventTopicComboBox.TabIndex = 7;
            this.eventSubscriptionValidationProvider.SetValidatedProperty(this._eventTopicComboBox, "SelectedItem");
            // 
            // _threadingOptionLabel
            // 
            this._threadingOptionLabel.AutoSize = true;
            this._threadingOptionLabel.Location = new System.Drawing.Point(11, 51);
            this._threadingOptionLabel.Name = "_threadingOptionLabel";
            this._threadingOptionLabel.Size = new System.Drawing.Size(90, 13);
            this._threadingOptionLabel.TabIndex = 4;
            this._threadingOptionLabel.Text = "Threading option:";
            // 
            // _eventArgsButton
            // 
            this._eventArgsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._eventArgsButton.Location = new System.Drawing.Point(656, 106);
            this._eventArgsButton.Name = "_eventArgsButton";
            this._eventArgsButton.Size = new System.Drawing.Size(27, 20);
            this._eventArgsButton.TabIndex = 6;
            this._eventArgsButton.Text = "...";
            this._eventArgsButton.UseVisualStyleBackColor = true;
            this._eventArgsButton.Click += new System.EventHandler(this._eventArgsButton_Click);
            // 
            // _threadingOptionComboBox
            // 
            this._threadingOptionComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._threadingOptionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._threadingOptionComboBox.FormattingEnabled = true;
            this._threadingOptionComboBox.Location = new System.Drawing.Point(17, 67);
            this._threadingOptionComboBox.Name = "_threadingOptionComboBox";
            this._threadingOptionComboBox.Size = new System.Drawing.Size(666, 21);
            this._threadingOptionComboBox.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Event Argument Type:";
            // 
            // eventSubscriptionValidationProvider
            // 
            this.eventSubscriptionValidationProvider.ErrorProvider = this._errorProvider;
            this.eventSubscriptionValidationProvider.RulesetName = "";
            this.eventSubscriptionValidationProvider.SourceTypeName = "Microsoft.Practices.SmartClientFactory.CustomWizardPages.CreateEventSubscriptionP" +
                "ageModel, Microsoft.Practices.SmartClientFactory.GuidancePackage";
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
            // CreateEventSubscriptionPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.Controls.Add(this._showDocumentation);
            this.Controls.Add(this._threadingOptionComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._eventTopicComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._eventArgsButton);
            this.Controls.Add(this._eventArgsTextBox);
            this.Controls.Add(this._threadingOptionLabel);
            this.Name = "CreateEventSubscriptionPage";
            this.Size = new System.Drawing.Size(700, 179);
            this.Controls.SetChildIndex(this._threadingOptionLabel, 0);
            this.Controls.SetChildIndex(this._eventArgsTextBox, 0);
            this.Controls.SetChildIndex(this._eventArgsButton, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this._eventTopicComboBox, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.infoPanel, 0);
            this.Controls.SetChildIndex(this._threadingOptionComboBox, 0);
            this.Controls.SetChildIndex(this._showDocumentation, 0);
            ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider _errorProvider;
        private System.Windows.Forms.Button _eventArgsButton;
        private System.Windows.Forms.TextBox _eventArgsTextBox;
        private System.Windows.Forms.Label _threadingOptionLabel;
        private System.Windows.Forms.ComboBox _threadingOptionComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox _eventTopicComboBox;
        private Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WinForms.ValidationProvider eventSubscriptionValidationProvider;
        private System.Windows.Forms.CheckBox _showDocumentation;
    }
}
