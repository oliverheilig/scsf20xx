namespace Microsoft.Practices.SmartClientFactory.CustomWizardPages
{
    partial class CreateViewPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateViewPage));
            this._errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this._viewNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._showDocumentation = new System.Windows.Forms.CheckBox();
            this._createViewFolder = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.assembliesTreeView = new System.Windows.Forms.TreeView();
            this._iconList = new System.Windows.Forms.ImageList(this.components);
            this.viewValidationProvider = new Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WinForms.ValidationProvider();
            ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            // 
            // _errorProvider
            // 
            this._errorProvider.ContainerControl = this;
            // 
            // _viewNameTextBox
            // 
            this._viewNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._errorProvider.SetIconPadding(this._viewNameTextBox, -20);
            this._viewNameTextBox.Location = new System.Drawing.Point(6, 19);
            this._viewNameTextBox.Name = "_viewNameTextBox";
            this.viewValidationProvider.SetPerformValidation(this._viewNameTextBox, true);
            this._viewNameTextBox.Size = new System.Drawing.Size(491, 20);
            this.viewValidationProvider.SetSourcePropertyName(this._viewNameTextBox, "ViewName");
            this._viewNameTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "View name:";
            // 
            // _showDocumentation
            // 
            this._showDocumentation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._showDocumentation.AutoSize = true;
            this._showDocumentation.Location = new System.Drawing.Point(6, 68);
            this._showDocumentation.Name = "_showDocumentation";
            this._showDocumentation.Size = new System.Drawing.Size(233, 17);
            this._showDocumentation.TabIndex = 3;
            this._showDocumentation.Text = "Show documentation after recipe completes";
            this._showDocumentation.UseVisualStyleBackColor = true;
            this._showDocumentation.Visible = false;
            // 
            // _createViewFolder
            // 
            this._createViewFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._createViewFolder.AutoSize = true;
            this._createViewFolder.Location = new System.Drawing.Point(6, 45);
            this._createViewFolder.Name = "_createViewFolder";
            this._createViewFolder.Size = new System.Drawing.Size(153, 17);
            this._createViewFolder.TabIndex = 2;
            this._createViewFolder.Text = "Create a folder for the view";
            this._createViewFolder.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.assembliesTreeView);
            this.groupBox1.Location = new System.Drawing.Point(295, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(202, 250);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Solution Preview";
            // 
            // assembliesTreeView
            // 
            this.assembliesTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.assembliesTreeView.ImageIndex = 0;
            this.assembliesTreeView.ImageList = this._iconList;
            this.assembliesTreeView.Location = new System.Drawing.Point(3, 16);
            this.assembliesTreeView.Name = "assembliesTreeView";
            this.assembliesTreeView.SelectedImageIndex = 0;
            this.assembliesTreeView.Size = new System.Drawing.Size(196, 231);
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
            this._iconList.Images.SetKeyName(7, "WPFUserControlIcon");
            // 
            // viewValidationProvider
            // 
            this.viewValidationProvider.ErrorProvider = this._errorProvider;
            this.viewValidationProvider.RulesetName = "";
            this.viewValidationProvider.SourceTypeName = "Microsoft.Practices.SmartClientFactory.CustomWizardPages.CreateViewPageModel, Mic" +
                "rosoft.Practices.SmartClientFactory.GuidancePackage";
            // 
            // CreateViewPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = false;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this._createViewFolder);
            this.Controls.Add(this._showDocumentation);
            this.Controls.Add(this._viewNameTextBox);
            this.Controls.Add(this.label1);
            this.Name = "CreateViewPage";
            this.Size = new System.Drawing.Size(500, 316);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this._viewNameTextBox, 0);
            this.Controls.SetChildIndex(this.infoPanel, 0);
            this.Controls.SetChildIndex(this._showDocumentation, 0);
            this.Controls.SetChildIndex(this._createViewFolder, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ErrorProvider _errorProvider;
        private System.Windows.Forms.TextBox _viewNameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox _showDocumentation;
        private System.Windows.Forms.CheckBox _createViewFolder;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView assembliesTreeView;
        private System.Windows.Forms.ImageList _iconList;
        private Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WinForms.ValidationProvider viewValidationProvider;
    }
}
