namespace Microsoft.Practices.SmartClientFactory.CustomWizardPages
{
    partial class SolutionPropertiesPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SolutionPropertiesPage));
            this._folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this._errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this._supportingLibrariesTextBox = new System.Windows.Forms.TextBox();
            this._rootNamespaceTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._browseSupportingLibraryButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this._supportingLibrariesList = new System.Windows.Forms.ListView();
            this.label5 = new System.Windows.Forms.Label();
            this._showDocumentation = new System.Windows.Forms.CheckBox();
            this._createShellLayoutModule = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.assembliesTreeView = new System.Windows.Forms.TreeView();
            this._iconList = new System.Windows.Forms.ImageList(this.components);
            this.solutionValidationProvider = new Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WinForms.ValidationProvider();
            this._supportWPFViews = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // infoPanel
            // 
            this.infoPanel.AutoSize = false;
            this.infoPanel.Dock = System.Windows.Forms.DockStyle.None;
            this.infoPanel.Location = new System.Drawing.Point(29, 326);
            this.infoPanel.Margin = new System.Windows.Forms.Padding(4);
            this.infoPanel.Size = new System.Drawing.Size(24, 21);
            this.infoPanel.TabIndex = 11;
            // 
            // _errorProvider
            // 
            this._errorProvider.ContainerControl = this;
            // 
            // _supportingLibrariesTextBox
            // 
            this._supportingLibrariesTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._errorProvider.SetIconPadding(this._supportingLibrariesTextBox, -20);
            this._supportingLibrariesTextBox.Location = new System.Drawing.Point(6, 18);
            this._supportingLibrariesTextBox.Name = "_supportingLibrariesTextBox";
            this.solutionValidationProvider.SetPerformValidation(this._supportingLibrariesTextBox, true);
            this._supportingLibrariesTextBox.Size = new System.Drawing.Size(427, 20);
            this.solutionValidationProvider.SetSourcePropertyName(this._supportingLibrariesTextBox, "SupportLibrariesPath");
            this._supportingLibrariesTextBox.TabIndex = 1;
            // 
            // _rootNamespaceTextbox
            // 
            this._rootNamespaceTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._errorProvider.SetIconPadding(this._rootNamespaceTextbox, -20);
            this._rootNamespaceTextbox.Location = new System.Drawing.Point(6, 57);
            this._rootNamespaceTextbox.Name = "_rootNamespaceTextbox";
            this.solutionValidationProvider.SetPerformValidation(this._rootNamespaceTextbox, true);
            this._rootNamespaceTextbox.Size = new System.Drawing.Size(459, 20);
            this.solutionValidationProvider.SetSourcePropertyName(this._rootNamespaceTextbox, "RootNamespace");
            this._rootNamespaceTextbox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(310, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Location of required application block assemblies (see list below)";
            // 
            // _browseSupportingLibraryButton
            // 
            this._browseSupportingLibraryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._browseSupportingLibraryButton.Location = new System.Drawing.Point(439, 16);
            this._browseSupportingLibraryButton.Name = "_browseSupportingLibraryButton";
            this._browseSupportingLibraryButton.Size = new System.Drawing.Size(26, 21);
            this._browseSupportingLibraryButton.TabIndex = 2;
            this._browseSupportingLibraryButton.Text = "...";
            this._browseSupportingLibraryButton.UseVisualStyleBackColor = true;
            this._browseSupportingLibraryButton.Click += new System.EventHandler(this.OnBrowseSupportingLibraryClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Root &namespace:";
            // 
            // _supportingLibrariesList
            // 
            this._supportingLibrariesList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._supportingLibrariesList.ForeColor = System.Drawing.SystemColors.GrayText;
            this._supportingLibrariesList.Location = new System.Drawing.Point(6, 96);
            this._supportingLibrariesList.Name = "_supportingLibrariesList";
            this._supportingLibrariesList.Size = new System.Drawing.Size(459, 128);
            this._supportingLibrariesList.TabIndex = 6;
            this._supportingLibrariesList.TileSize = new System.Drawing.Size(370, 18);
            this._supportingLibrariesList.UseCompatibleStateImageBehavior = false;
            this._supportingLibrariesList.View = System.Windows.Forms.View.Tile;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(265, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Required application block &assemblies";
            // 
            // _showDocumentation
            // 
            this._showDocumentation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._showDocumentation.AutoSize = true;
            this._showDocumentation.Location = new System.Drawing.Point(6, 276);
            this._showDocumentation.Name = "_showDocumentation";
            this._showDocumentation.Size = new System.Drawing.Size(233, 17);
            this._showDocumentation.TabIndex = 9;
            this._showDocumentation.Text = "Show &documentation after recipe completes";
            this._showDocumentation.UseVisualStyleBackColor = true;
            this._showDocumentation.Visible = false;
            // 
            // _createShellLayoutModule
            // 
            this._createShellLayoutModule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._createShellLayoutModule.AutoSize = true;
            this._createShellLayoutModule.Checked = true;
            this._createShellLayoutModule.CheckState = System.Windows.Forms.CheckState.Checked;
            this._createShellLayoutModule.Location = new System.Drawing.Point(6, 230);
            this._createShellLayoutModule.Name = "_createShellLayoutModule";
            this._createShellLayoutModule.Size = new System.Drawing.Size(297, 17);
            this._createShellLayoutModule.TabIndex = 7;
            this._createShellLayoutModule.Text = "Create a separate &module to define the layout for the shell";
            this._createShellLayoutModule.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.assembliesTreeView);
            this.groupBox1.Location = new System.Drawing.Point(471, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(217, 331);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Solution &Preview";
            // 
            // assembliesTreeView
            // 
            this.assembliesTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.assembliesTreeView.ImageIndex = 0;
            this.assembliesTreeView.ImageList = this._iconList;
            this.assembliesTreeView.Location = new System.Drawing.Point(3, 16);
            this.assembliesTreeView.Name = "assembliesTreeView";
            this.assembliesTreeView.SelectedImageIndex = 0;
            this.assembliesTreeView.Size = new System.Drawing.Size(211, 312);
            this.assembliesTreeView.TabIndex = 0;
            // 
            // _iconList
            // 
            this._iconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_iconList.ImageStream")));
            this._iconList.TransparentColor = System.Drawing.Color.Transparent;
            this._iconList.Images.SetKeyName(0, "FolderIcon");
            this._iconList.Images.SetKeyName(1, "CSharpItemIcon");
            this._iconList.Images.SetKeyName(2, "VBProjectIcon");
            this._iconList.Images.SetKeyName(3, "CSharpProjectIcon");
            this._iconList.Images.SetKeyName(4, "SolutionFolderIcon");
            this._iconList.Images.SetKeyName(5, "VBItemIcon");
            this._iconList.Images.SetKeyName(6, "UserControlIcon");
            // 
            // solutionValidationProvider
            // 
            this.solutionValidationProvider.ErrorProvider = this._errorProvider;
            this.solutionValidationProvider.RulesetName = "";
            this.solutionValidationProvider.SourceTypeName = "Microsoft.Practices.SmartClientFactory.CustomWizardPages.SolutionPropertiesModel," +
                " Microsoft.Practices.SmartClientFactory.GuidancePackage";
            // 
            // _supportWPFViews
            // 
            this._supportWPFViews.AutoSize = true;
            this._supportWPFViews.Location = new System.Drawing.Point(6, 253);
            this._supportWPFViews.Name = "_supportWPFViews";
            this._supportWPFViews.Size = new System.Drawing.Size(206, 17);
            this._supportWPFViews.TabIndex = 8;
            this._supportWPFViews.Text = "Allow solution to host &WPF SmartParts";
            this._supportWPFViews.UseVisualStyleBackColor = true;
            // 
            // SolutionPropertiesPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.Controls.Add(this._supportWPFViews);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this._createShellLayoutModule);
            this.Controls.Add(this._showDocumentation);
            this.Controls.Add(this._supportingLibrariesList);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._supportingLibrariesTextBox);
            this.Controls.Add(this._browseSupportingLibraryButton);
            this.Controls.Add(this._rootNamespaceTextbox);
            this.Controls.Add(this.label3);
            this.Name = "SolutionPropertiesPage";
            this.Size = new System.Drawing.Size(700, 351);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this._rootNamespaceTextbox, 0);
            this.Controls.SetChildIndex(this._browseSupportingLibraryButton, 0);
            this.Controls.SetChildIndex(this.infoPanel, 0);
            this.Controls.SetChildIndex(this._supportingLibrariesTextBox, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this._supportingLibrariesList, 0);
            this.Controls.SetChildIndex(this._showDocumentation, 0);
            this.Controls.SetChildIndex(this._createShellLayoutModule, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this._supportWPFViews, 0);
            ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog _folderBrowserDialog;
        private System.Windows.Forms.ErrorProvider _errorProvider;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _supportingLibrariesTextBox;
        private System.Windows.Forms.Button _browseSupportingLibraryButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox _rootNamespaceTextbox;
        private System.Windows.Forms.ListView _supportingLibrariesList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox _showDocumentation;
        private System.Windows.Forms.CheckBox _createShellLayoutModule;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView assembliesTreeView;
        private System.Windows.Forms.ImageList _iconList;
        private Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WinForms.ValidationProvider solutionValidationProvider;
        private System.Windows.Forms.CheckBox _supportWPFViews;
    }
}
