namespace Microsoft.Practices.SmartClientFactory.CustomWizardPages
{
    partial class CreateBusinessModulePage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateBusinessModulePage));
            this._moduleNamespaceTextBox = new System.Windows.Forms.TextBox();
            this._showDocumentation = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._createTestProject = new System.Windows.Forms.CheckBox();
            this._createModuleInterfaceLibrary = new System.Windows.Forms.CheckBox();
            this._errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.assembliesTreeView = new System.Windows.Forms.TreeView();
            this._iconList = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // infoPanel
            // 
            this.infoPanel.AutoSize = false;
            this.infoPanel.Dock = System.Windows.Forms.DockStyle.None;
            this.infoPanel.Location = new System.Drawing.Point(23, 191);
            this.infoPanel.Size = new System.Drawing.Size(24, 21);
            this.infoPanel.TabIndex = 4;
            // 
            // _moduleNamespaceTextBox
            // 
            this._moduleNamespaceTextBox.Location = new System.Drawing.Point(17, 28);
            this._moduleNamespaceTextBox.Name = "_moduleNamespaceTextBox";
            this._moduleNamespaceTextBox.ReadOnly = true;
            this._moduleNamespaceTextBox.Size = new System.Drawing.Size(262, 20);
            this._moduleNamespaceTextBox.TabIndex = 1;
            // 
            // _showDocumentation
            // 
            this._showDocumentation.AutoSize = true;
            this._showDocumentation.Location = new System.Drawing.Point(6, 64);
            this._showDocumentation.Name = "_showDocumentation";
            this._showDocumentation.Size = new System.Drawing.Size(233, 17);
            this._showDocumentation.TabIndex = 2;
            this._showDocumentation.Text = "Show documentation after recipe completes";
            this._showDocumentation.UseVisualStyleBackColor = true;
            this._showDocumentation.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Module namespace:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._createTestProject);
            this.groupBox1.Controls.Add(this._createModuleInterfaceLibrary);
            this.groupBox1.Controls.Add(this._showDocumentation);
            this.groupBox1.Location = new System.Drawing.Point(17, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(262, 65);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // _createTestProject
            // 
            this._createTestProject.AutoSize = true;
            this._createTestProject.Location = new System.Drawing.Point(6, 41);
            this._createTestProject.Name = "_createTestProject";
            this._createTestProject.Size = new System.Drawing.Size(212, 17);
            this._createTestProject.TabIndex = 1;
            this._createTestProject.Text = "Create a unit test project for this module";
            this._createTestProject.UseVisualStyleBackColor = true;
            // 
            // _createModuleInterfaceLibrary
            // 
            this._createModuleInterfaceLibrary.AutoSize = true;
            this._createModuleInterfaceLibrary.Checked = true;
            this._createModuleInterfaceLibrary.CheckState = System.Windows.Forms.CheckState.Checked;
            this._createModuleInterfaceLibrary.Location = new System.Drawing.Point(6, 19);
            this._createModuleInterfaceLibrary.Name = "_createModuleInterfaceLibrary";
            this._createModuleInterfaceLibrary.Size = new System.Drawing.Size(217, 17);
            this._createModuleInterfaceLibrary.TabIndex = 0;
            this._createModuleInterfaceLibrary.Text = "Create an interface library for this module";
            this._createModuleInterfaceLibrary.UseVisualStyleBackColor = true;
            // 
            // _errorProvider
            // 
            this._errorProvider.ContainerControl = this;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.assembliesTreeView);
            this.groupBox2.Location = new System.Drawing.Point(285, 11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(208, 227);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Solution Preview";
            // 
            // assembliesTreeView
            // 
            this.assembliesTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.assembliesTreeView.ImageIndex = 0;
            this.assembliesTreeView.ImageList = this._iconList;
            this.assembliesTreeView.Location = new System.Drawing.Point(3, 16);
            this.assembliesTreeView.Name = "assembliesTreeView";
            this.assembliesTreeView.SelectedImageIndex = 0;
            this.assembliesTreeView.Size = new System.Drawing.Size(202, 208);
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
            // 
            // CreateBusinessModulePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this._moduleNamespaceTextBox);
            this.Controls.Add(this.label1);
            this.Name = "CreateBusinessModulePage";
            this.Size = new System.Drawing.Size(501, 245);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this._moduleNamespaceTextBox, 0);
            this.Controls.SetChildIndex(this.infoPanel, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _moduleNamespaceTextBox;
        private System.Windows.Forms.CheckBox _showDocumentation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox _createModuleInterfaceLibrary;
        private System.Windows.Forms.ErrorProvider _errorProvider;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TreeView assembliesTreeView;
        private System.Windows.Forms.ImageList _iconList;
        private System.Windows.Forms.CheckBox _createTestProject;
    }
}
