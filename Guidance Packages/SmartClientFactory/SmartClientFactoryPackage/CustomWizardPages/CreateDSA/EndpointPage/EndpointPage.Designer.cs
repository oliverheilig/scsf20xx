namespace Microsoft.Practices.SmartClientFactory.CustomWizardPages
{
    partial class EndpointPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EndpointPage));
            this._errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this._expirationTextBox = new System.Windows.Forms.MaskedTextBox();
            this._endpointTextBox = new System.Windows.Forms.TextBox();
            this._stampsTextBox = new System.Windows.Forms.TextBox();
            this._maxRetriesTextBox = new System.Windows.Forms.TextBox();
            this._proxyTypeLabel = new System.Windows.Forms.Label();
            this._proxyTypeNameTextBox = new System.Windows.Forms.TextBox();
            this._iconList = new System.Windows.Forms.ImageList(this.components);
            this._solutionPreviewGroupBox = new System.Windows.Forms.GroupBox();
            this.assembliesTreeView = new System.Windows.Forms.TreeView();
            this._methodsListGroupBox = new System.Windows.Forms.GroupBox();
            this._methodsListView = new System.Windows.Forms.ListView();
            this.methodColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._proxyFactoryTypeFullNameComboBox = new System.Windows.Forms.ComboBox();
            this._proxyFactoryTypeFullNameLabel = new System.Windows.Forms.Label();
            this._tagLabel = new System.Windows.Forms.Label();
            this._tagTextBox = new System.Windows.Forms.TextBox();
            this._expirationLabel = new System.Windows.Forms.Label();
            this._methodsPreviewSplitter = new System.Windows.Forms.SplitContainer();
            this.endpointValidationProvider = new Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WinForms.ValidationProvider();
            this._solutionNotBuiltSplitter = new System.Windows.Forms.SplitContainer();
            this._proxyClassNotFoundLabel = new System.Windows.Forms.Label();
            this._solutionNotBuiltLabel = new System.Windows.Forms.Label();
            this._ShowDocumentationSplitter = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.PropertiesTabPage = new System.Windows.Forms.TabPage();
            this._proxyTypeButton = new System.Windows.Forms.Button();
            this.AdvancedTabPage = new System.Windows.Forms.TabPage();
            this._DefaultOfflineBehaviorGroupBox = new System.Windows.Forms.GroupBox();
            this._endpointLabel = new System.Windows.Forms.Label();
            this._maxRetriesLabel = new System.Windows.Forms.Label();
            this._stampsLabel = new System.Windows.Forms.Label();
            this._showDocumentation = new System.Windows.Forms.CheckBox();
            this._endpointTooltip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).BeginInit();
            this._solutionPreviewGroupBox.SuspendLayout();
            this._methodsListGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._methodsPreviewSplitter)).BeginInit();
            this._methodsPreviewSplitter.Panel1.SuspendLayout();
            this._methodsPreviewSplitter.Panel2.SuspendLayout();
            this._methodsPreviewSplitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._solutionNotBuiltSplitter)).BeginInit();
            this._solutionNotBuiltSplitter.Panel1.SuspendLayout();
            this._solutionNotBuiltSplitter.Panel2.SuspendLayout();
            this._solutionNotBuiltSplitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._ShowDocumentationSplitter)).BeginInit();
            this._ShowDocumentationSplitter.Panel1.SuspendLayout();
            this._ShowDocumentationSplitter.Panel2.SuspendLayout();
            this._ShowDocumentationSplitter.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.PropertiesTabPage.SuspendLayout();
            this.AdvancedTabPage.SuspendLayout();
            this._DefaultOfflineBehaviorGroupBox.SuspendLayout();
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
            // _expirationTextBox
            // 
            this._expirationTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._expirationTextBox.Culture = new System.Globalization.CultureInfo("");
            this._errorProvider.SetIconPadding(this._expirationTextBox, -20);
            this._expirationTextBox.Location = new System.Drawing.Point(7, 110);
            this._expirationTextBox.Mask = "00\\.00:00:00";
            this._expirationTextBox.Name = "_expirationTextBox";
            this.endpointValidationProvider.SetPerformValidation(this._expirationTextBox, true);
            this._expirationTextBox.Size = new System.Drawing.Size(659, 20);
            this.endpointValidationProvider.SetSourcePropertyName(this._expirationTextBox, "Expiration");
            this._expirationTextBox.TabIndex = 29;
            this._expirationTextBox.Text = "24000000";
            // 
            // _endpointTextBox
            // 
            this._endpointTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._errorProvider.SetIconPadding(this._endpointTextBox, -20);
            this._endpointTextBox.Location = new System.Drawing.Point(7, 32);
            this._endpointTextBox.Name = "_endpointTextBox";
            this.endpointValidationProvider.SetPerformValidation(this._endpointTextBox, true);
            this._endpointTextBox.Size = new System.Drawing.Size(659, 20);
            this.endpointValidationProvider.SetSourcePropertyName(this._endpointTextBox, "Endpoint");
            this._endpointTextBox.TabIndex = 39;
            this._endpointTooltip.SetToolTip(this._endpointTextBox, resources.GetString("_endpointTextBox.ToolTip"));
            // 
            // _stampsTextBox
            // 
            this._errorProvider.SetIconPadding(this._stampsTextBox, -20);
            this._stampsTextBox.Location = new System.Drawing.Point(7, 71);
            this._stampsTextBox.Name = "_stampsTextBox";
            this.endpointValidationProvider.SetPerformValidation(this._stampsTextBox, true);
            this._stampsTextBox.Size = new System.Drawing.Size(175, 20);
            this.endpointValidationProvider.SetSourcePropertyName(this._stampsTextBox, "Stamps");
            this._stampsTextBox.TabIndex = 37;
            this._stampsTextBox.Text = "1";
            // 
            // _maxRetriesTextBox
            // 
            this._maxRetriesTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._errorProvider.SetIconPadding(this._maxRetriesTextBox, -20);
            this._maxRetriesTextBox.Location = new System.Drawing.Point(187, 71);
            this._maxRetriesTextBox.Name = "_maxRetriesTextBox";
            this.endpointValidationProvider.SetPerformValidation(this._maxRetriesTextBox, true);
            this._maxRetriesTextBox.Size = new System.Drawing.Size(479, 20);
            this.endpointValidationProvider.SetSourcePropertyName(this._maxRetriesTextBox, "MaxRetries");
            this._maxRetriesTextBox.TabIndex = 39;
            this._maxRetriesTextBox.Text = "3";
            // 
            // _proxyTypeLabel
            // 
            this._proxyTypeLabel.AutoSize = true;
            this._proxyTypeLabel.Location = new System.Drawing.Point(3, 3);
            this._proxyTypeLabel.Name = "_proxyTypeLabel";
            this._proxyTypeLabel.Size = new System.Drawing.Size(63, 13);
            this._proxyTypeLabel.TabIndex = 20;
            this._proxyTypeLabel.Text = "Proxy Type:";
            // 
            // _proxyTypeNameTextBox
            // 
            this._proxyTypeNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._proxyTypeNameTextBox.Location = new System.Drawing.Point(6, 19);
            this._proxyTypeNameTextBox.Name = "_proxyTypeNameTextBox";
            this.endpointValidationProvider.SetPerformValidation(this._proxyTypeNameTextBox, true);
            this._proxyTypeNameTextBox.Size = new System.Drawing.Size(652, 20);
            this.endpointValidationProvider.SetSourcePropertyName(this._proxyTypeNameTextBox, "ProxyTypeName");
            this._proxyTypeNameTextBox.TabIndex = 21;
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
            this._iconList.Images.SetKeyName(6, "UserControl");
            // 
            // _solutionPreviewGroupBox
            // 
            this._solutionPreviewGroupBox.Controls.Add(this.assembliesTreeView);
            this._solutionPreviewGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._solutionPreviewGroupBox.Location = new System.Drawing.Point(0, 0);
            this._solutionPreviewGroupBox.Name = "_solutionPreviewGroupBox";
            this._solutionPreviewGroupBox.Size = new System.Drawing.Size(467, 290);
            this._solutionPreviewGroupBox.TabIndex = 34;
            this._solutionPreviewGroupBox.TabStop = false;
            this._solutionPreviewGroupBox.Text = "Solution Preview";
            // 
            // assembliesTreeView
            // 
            this.assembliesTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.assembliesTreeView.ImageIndex = 0;
            this.assembliesTreeView.ImageList = this._iconList;
            this.assembliesTreeView.Location = new System.Drawing.Point(3, 16);
            this.assembliesTreeView.Name = "assembliesTreeView";
            this.assembliesTreeView.SelectedImageIndex = 0;
            this.assembliesTreeView.Size = new System.Drawing.Size(461, 271);
            this.assembliesTreeView.TabIndex = 0;
            // 
            // _methodsListGroupBox
            // 
            this._methodsListGroupBox.Controls.Add(this._methodsListView);
            this._methodsListGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._methodsListGroupBox.Location = new System.Drawing.Point(0, 0);
            this._methodsListGroupBox.Name = "_methodsListGroupBox";
            this._methodsListGroupBox.Size = new System.Drawing.Size(215, 290);
            this._methodsListGroupBox.TabIndex = 35;
            this._methodsListGroupBox.TabStop = false;
            this._methodsListGroupBox.Text = "Type methods";
            // 
            // _methodsListView
            // 
            this._methodsListView.CheckBoxes = true;
            this._methodsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.methodColumnHeader});
            this._methodsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._methodsListView.FullRowSelect = true;
            this._methodsListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this._methodsListView.Location = new System.Drawing.Point(3, 16);
            this._methodsListView.Name = "_methodsListView";
            this._methodsListView.ShowItemToolTips = true;
            this._methodsListView.Size = new System.Drawing.Size(209, 271);
            this._methodsListView.TabIndex = 26;
            this._methodsListView.UseCompatibleStateImageBehavior = false;
            this._methodsListView.View = System.Windows.Forms.View.Details;
            // 
            // methodColumnHeader
            // 
            this.methodColumnHeader.Text = "Method";
            this.methodColumnHeader.Width = 193;
            // 
            // _proxyFactoryTypeFullNameComboBox
            // 
            this._proxyFactoryTypeFullNameComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._proxyFactoryTypeFullNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._proxyFactoryTypeFullNameComboBox.Enabled = false;
            this._proxyFactoryTypeFullNameComboBox.FormattingEnabled = true;
            this._proxyFactoryTypeFullNameComboBox.Items.AddRange(new object[] {
            "Microsoft.Practices.SmartClient.DisconnectedAgent.WebServiceProxyFactory",
            "Microsoft.Practices.SmartClient.DisconnectedAgent.WCFProxyFactory",
            "Microsoft.Practices.SmartClient.DisconnectedAgent.ObjectProxyFactory"});
            this._proxyFactoryTypeFullNameComboBox.Location = new System.Drawing.Point(7, 189);
            this._proxyFactoryTypeFullNameComboBox.Name = "_proxyFactoryTypeFullNameComboBox";
            this._proxyFactoryTypeFullNameComboBox.Size = new System.Drawing.Size(659, 21);
            this._proxyFactoryTypeFullNameComboBox.TabIndex = 36;
            // 
            // _proxyFactoryTypeFullNameLabel
            // 
            this._proxyFactoryTypeFullNameLabel.AutoSize = true;
            this._proxyFactoryTypeFullNameLabel.Location = new System.Drawing.Point(6, 172);
            this._proxyFactoryTypeFullNameLabel.Name = "_proxyFactoryTypeFullNameLabel";
            this._proxyFactoryTypeFullNameLabel.Size = new System.Drawing.Size(101, 13);
            this._proxyFactoryTypeFullNameLabel.TabIndex = 35;
            this._proxyFactoryTypeFullNameLabel.Text = "Proxy Factory Type:";
            // 
            // _tagLabel
            // 
            this._tagLabel.AutoSize = true;
            this._tagLabel.Location = new System.Drawing.Point(6, 133);
            this._tagLabel.Name = "_tagLabel";
            this._tagLabel.Size = new System.Drawing.Size(26, 13);
            this._tagLabel.TabIndex = 31;
            this._tagLabel.Text = "Tag";
            // 
            // _tagTextBox
            // 
            this._tagTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._tagTextBox.Location = new System.Drawing.Point(7, 149);
            this._tagTextBox.Name = "_tagTextBox";
            this._tagTextBox.Size = new System.Drawing.Size(659, 20);
            this._tagTextBox.TabIndex = 30;
            this._tagTextBox.TextChanged += new System.EventHandler(this._tagTextBox_TextChanged);
            // 
            // _expirationLabel
            // 
            this._expirationLabel.AutoSize = true;
            this._expirationLabel.Location = new System.Drawing.Point(6, 94);
            this._expirationLabel.Name = "_expirationLabel";
            this._expirationLabel.Size = new System.Drawing.Size(53, 13);
            this._expirationLabel.TabIndex = 32;
            this._expirationLabel.Text = "Expiration";
            // 
            // _methodsPreviewSplitter
            // 
            this._methodsPreviewSplitter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._methodsPreviewSplitter.Location = new System.Drawing.Point(6, 45);
            this._methodsPreviewSplitter.Name = "_methodsPreviewSplitter";
            // 
            // _methodsPreviewSplitter.Panel1
            // 
            this._methodsPreviewSplitter.Panel1.Controls.Add(this._methodsListGroupBox);
            // 
            // _methodsPreviewSplitter.Panel2
            // 
            this._methodsPreviewSplitter.Panel2.Controls.Add(this._solutionPreviewGroupBox);
            this._methodsPreviewSplitter.Size = new System.Drawing.Size(686, 290);
            this._methodsPreviewSplitter.SplitterDistance = 215;
            this._methodsPreviewSplitter.TabIndex = 25;
            // 
            // endpointValidationProvider
            // 
            this.endpointValidationProvider.ErrorProvider = this._errorProvider;
            this.endpointValidationProvider.RulesetName = "";
            this.endpointValidationProvider.SourceTypeName = "Microsoft.Practices.SmartClientFactory.CustomWizardPages.EndpointPageModel, Micro" +
                "soft.Practices.SmartClientFactory.GuidancePackage";
            // 
            // _solutionNotBuiltSplitter
            // 
            this._solutionNotBuiltSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this._solutionNotBuiltSplitter.IsSplitterFixed = true;
            this._solutionNotBuiltSplitter.Location = new System.Drawing.Point(0, 0);
            this._solutionNotBuiltSplitter.Name = "_solutionNotBuiltSplitter";
            this._solutionNotBuiltSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _solutionNotBuiltSplitter.Panel1
            // 
            this._solutionNotBuiltSplitter.Panel1.Controls.Add(this._proxyClassNotFoundLabel);
            this._solutionNotBuiltSplitter.Panel1.Controls.Add(this._solutionNotBuiltLabel);
            // 
            // _solutionNotBuiltSplitter.Panel2
            // 
            this._solutionNotBuiltSplitter.Panel2.Controls.Add(this._ShowDocumentationSplitter);
            this._solutionNotBuiltSplitter.Size = new System.Drawing.Size(703, 472);
            this._solutionNotBuiltSplitter.SplitterDistance = 83;
            this._solutionNotBuiltSplitter.TabIndex = 25;
            // 
            // _proxyClassNotFoundLabel
            // 
            this._proxyClassNotFoundLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._proxyClassNotFoundLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._proxyClassNotFoundLabel.Location = new System.Drawing.Point(0, 43);
            this._proxyClassNotFoundLabel.Name = "_proxyClassNotFoundLabel";
            this._proxyClassNotFoundLabel.Size = new System.Drawing.Size(662, 131);
            this._proxyClassNotFoundLabel.TabIndex = 2;
            this._proxyClassNotFoundLabel.Text = "Proxy class not found message will be here.";
            // 
            // _solutionNotBuiltLabel
            // 
            this._solutionNotBuiltLabel.AutoSize = true;
            this._solutionNotBuiltLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._solutionNotBuiltLabel.Location = new System.Drawing.Point(0, 0);
            this._solutionNotBuiltLabel.Name = "_solutionNotBuiltLabel";
            this._solutionNotBuiltLabel.Size = new System.Drawing.Size(256, 26);
            this._solutionNotBuiltLabel.TabIndex = 1;
            this._solutionNotBuiltLabel.Text = "In order to run this recipe, your solution must compile.\r\nPlease build your solut" +
                "ion and try again.";
            // 
            // _ShowDocumentationSplitter
            // 
            this._ShowDocumentationSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ShowDocumentationSplitter.IsSplitterFixed = true;
            this._ShowDocumentationSplitter.Location = new System.Drawing.Point(0, 0);
            this._ShowDocumentationSplitter.Name = "_ShowDocumentationSplitter";
            this._ShowDocumentationSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _ShowDocumentationSplitter.Panel1
            // 
            this._ShowDocumentationSplitter.Panel1.Controls.Add(this.tabControl1);
            this._ShowDocumentationSplitter.Panel1MinSize = 20;
            // 
            // _ShowDocumentationSplitter.Panel2
            // 
            this._ShowDocumentationSplitter.Panel2.Controls.Add(this._showDocumentation);
            this._ShowDocumentationSplitter.Panel2MinSize = 20;
            this._ShowDocumentationSplitter.Size = new System.Drawing.Size(703, 385);
            this._ShowDocumentationSplitter.SplitterDistance = 361;
            this._ShowDocumentationSplitter.SplitterWidth = 2;
            this._ShowDocumentationSplitter.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.PropertiesTabPage);
            this.tabControl1.Controls.Add(this.AdvancedTabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(703, 361);
            this.tabControl1.TabIndex = 5;
            // 
            // PropertiesTabPage
            // 
            this.PropertiesTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.PropertiesTabPage.Controls.Add(this._proxyTypeButton);
            this.PropertiesTabPage.Controls.Add(this._methodsPreviewSplitter);
            this.PropertiesTabPage.Controls.Add(this._proxyTypeLabel);
            this.PropertiesTabPage.Controls.Add(this._proxyTypeNameTextBox);
            this.PropertiesTabPage.Location = new System.Drawing.Point(4, 22);
            this.PropertiesTabPage.Name = "PropertiesTabPage";
            this.PropertiesTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.PropertiesTabPage.Size = new System.Drawing.Size(695, 335);
            this.PropertiesTabPage.TabIndex = 0;
            this.PropertiesTabPage.Text = "Properties";
            // 
            // _proxyTypeButton
            // 
            this._proxyTypeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._proxyTypeButton.Location = new System.Drawing.Point(664, 19);
            this._proxyTypeButton.Name = "_proxyTypeButton";
            this._proxyTypeButton.Size = new System.Drawing.Size(25, 20);
            this._proxyTypeButton.TabIndex = 26;
            this._proxyTypeButton.Text = "...";
            this._proxyTypeButton.UseVisualStyleBackColor = true;
            // 
            // AdvancedTabPage
            // 
            this.AdvancedTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.AdvancedTabPage.Controls.Add(this._DefaultOfflineBehaviorGroupBox);
            this.AdvancedTabPage.Location = new System.Drawing.Point(4, 22);
            this.AdvancedTabPage.Name = "AdvancedTabPage";
            this.AdvancedTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.AdvancedTabPage.Size = new System.Drawing.Size(695, 335);
            this.AdvancedTabPage.TabIndex = 1;
            this.AdvancedTabPage.Text = "Advanced";
            // 
            // _DefaultOfflineBehaviorGroupBox
            // 
            this._DefaultOfflineBehaviorGroupBox.Controls.Add(this._endpointLabel);
            this._DefaultOfflineBehaviorGroupBox.Controls.Add(this._endpointTextBox);
            this._DefaultOfflineBehaviorGroupBox.Controls.Add(this._maxRetriesTextBox);
            this._DefaultOfflineBehaviorGroupBox.Controls.Add(this._maxRetriesLabel);
            this._DefaultOfflineBehaviorGroupBox.Controls.Add(this._stampsLabel);
            this._DefaultOfflineBehaviorGroupBox.Controls.Add(this._stampsTextBox);
            this._DefaultOfflineBehaviorGroupBox.Controls.Add(this._proxyFactoryTypeFullNameComboBox);
            this._DefaultOfflineBehaviorGroupBox.Controls.Add(this._expirationTextBox);
            this._DefaultOfflineBehaviorGroupBox.Controls.Add(this._proxyFactoryTypeFullNameLabel);
            this._DefaultOfflineBehaviorGroupBox.Controls.Add(this._expirationLabel);
            this._DefaultOfflineBehaviorGroupBox.Controls.Add(this._tagLabel);
            this._DefaultOfflineBehaviorGroupBox.Controls.Add(this._tagTextBox);
            this._DefaultOfflineBehaviorGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._DefaultOfflineBehaviorGroupBox.Location = new System.Drawing.Point(3, 3);
            this._DefaultOfflineBehaviorGroupBox.Name = "_DefaultOfflineBehaviorGroupBox";
            this._DefaultOfflineBehaviorGroupBox.Size = new System.Drawing.Size(689, 329);
            this._DefaultOfflineBehaviorGroupBox.TabIndex = 37;
            this._DefaultOfflineBehaviorGroupBox.TabStop = false;
            this._DefaultOfflineBehaviorGroupBox.Text = "Default Offline Behavior settings";
            // 
            // _endpointLabel
            // 
            this._endpointLabel.AutoSize = true;
            this._endpointLabel.Location = new System.Drawing.Point(6, 16);
            this._endpointLabel.Name = "_endpointLabel";
            this._endpointLabel.Size = new System.Drawing.Size(52, 13);
            this._endpointLabel.TabIndex = 38;
            this._endpointLabel.Text = "Endpoint:";
            // 
            // _maxRetriesLabel
            // 
            this._maxRetriesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this._maxRetriesLabel.AutoSize = true;
            this._maxRetriesLabel.Location = new System.Drawing.Point(184, 55);
            this._maxRetriesLabel.Name = "_maxRetriesLabel";
            this._maxRetriesLabel.Size = new System.Drawing.Size(63, 13);
            this._maxRetriesLabel.TabIndex = 40;
            this._maxRetriesLabel.Text = "Max Retries";
            // 
            // _stampsLabel
            // 
            this._stampsLabel.AutoSize = true;
            this._stampsLabel.Location = new System.Drawing.Point(6, 55);
            this._stampsLabel.Name = "_stampsLabel";
            this._stampsLabel.Size = new System.Drawing.Size(42, 13);
            this._stampsLabel.TabIndex = 38;
            this._stampsLabel.Text = "Stamps";
            // 
            // _showDocumentation
            // 
            this._showDocumentation.AutoSize = true;
            this._showDocumentation.Dock = System.Windows.Forms.DockStyle.Left;
            this._showDocumentation.Location = new System.Drawing.Point(0, 0);
            this._showDocumentation.Name = "_showDocumentation";
            this._showDocumentation.Size = new System.Drawing.Size(233, 22);
            this._showDocumentation.TabIndex = 3;
            this._showDocumentation.Text = "Show documentation after recipe completes";
            this._showDocumentation.UseVisualStyleBackColor = true;
            this._showDocumentation.Visible = false;
            // 
            // _endpointTooltip
            // 
            this._endpointTooltip.AutoPopDelay = 15000;
            this._endpointTooltip.InitialDelay = 500;
            this._endpointTooltip.ReshowDelay = 100;
            this._endpointTooltip.ShowAlways = true;
            // 
            // EndpointPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.Controls.Add(this._solutionNotBuiltSplitter);
            this.Name = "EndpointPage";
            this.Size = new System.Drawing.Size(703, 472);
            this.Controls.SetChildIndex(this.infoPanel, 0);
            this.Controls.SetChildIndex(this._solutionNotBuiltSplitter, 0);
            ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).EndInit();
            this._solutionPreviewGroupBox.ResumeLayout(false);
            this._methodsListGroupBox.ResumeLayout(false);
            this._methodsPreviewSplitter.Panel1.ResumeLayout(false);
            this._methodsPreviewSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._methodsPreviewSplitter)).EndInit();
            this._methodsPreviewSplitter.ResumeLayout(false);
            this._solutionNotBuiltSplitter.Panel1.ResumeLayout(false);
            this._solutionNotBuiltSplitter.Panel1.PerformLayout();
            this._solutionNotBuiltSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._solutionNotBuiltSplitter)).EndInit();
            this._solutionNotBuiltSplitter.ResumeLayout(false);
            this._ShowDocumentationSplitter.Panel1.ResumeLayout(false);
            this._ShowDocumentationSplitter.Panel2.ResumeLayout(false);
            this._ShowDocumentationSplitter.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._ShowDocumentationSplitter)).EndInit();
            this._ShowDocumentationSplitter.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.PropertiesTabPage.ResumeLayout(false);
            this.PropertiesTabPage.PerformLayout();
            this.AdvancedTabPage.ResumeLayout(false);
            this._DefaultOfflineBehaviorGroupBox.ResumeLayout(false);
            this._DefaultOfflineBehaviorGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ErrorProvider _errorProvider;
        private System.Windows.Forms.Label _proxyTypeLabel;
		private System.Windows.Forms.TextBox _proxyTypeNameTextBox;
        private System.Windows.Forms.ImageList _iconList;
        private System.Windows.Forms.GroupBox _solutionPreviewGroupBox;
        private System.Windows.Forms.TreeView assembliesTreeView;
        private System.Windows.Forms.GroupBox _methodsListGroupBox;
        private System.Windows.Forms.SplitContainer _methodsPreviewSplitter;
        private Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WinForms.ValidationProvider endpointValidationProvider;
        private System.Windows.Forms.SplitContainer _solutionNotBuiltSplitter;
        private System.Windows.Forms.SplitContainer _ShowDocumentationSplitter;
        private System.Windows.Forms.CheckBox _showDocumentation;
        private System.Windows.Forms.Label _solutionNotBuiltLabel;
        private System.Windows.Forms.ComboBox _proxyFactoryTypeFullNameComboBox;
        private System.Windows.Forms.Label _proxyFactoryTypeFullNameLabel;
        private System.Windows.Forms.Label _tagLabel;
        private System.Windows.Forms.TextBox _tagTextBox;
        private System.Windows.Forms.Label _expirationLabel;
        private System.Windows.Forms.MaskedTextBox _expirationTextBox;
        private System.Windows.Forms.GroupBox _DefaultOfflineBehaviorGroupBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage PropertiesTabPage;
        private System.Windows.Forms.TabPage AdvancedTabPage;
		private System.Windows.Forms.Button _proxyTypeButton;
		private System.Windows.Forms.ListView _methodsListView;
        private System.Windows.Forms.ColumnHeader methodColumnHeader;
        private System.Windows.Forms.Label _endpointLabel;
        private System.Windows.Forms.TextBox _endpointTextBox;
        private System.Windows.Forms.TextBox _maxRetriesTextBox;
        private System.Windows.Forms.Label _maxRetriesLabel;
        private System.Windows.Forms.Label _stampsLabel;
        private System.Windows.Forms.TextBox _stampsTextBox;
        private System.Windows.Forms.ToolTip _endpointTooltip;
        private System.Windows.Forms.Label _proxyClassNotFoundLabel;
    }
}
