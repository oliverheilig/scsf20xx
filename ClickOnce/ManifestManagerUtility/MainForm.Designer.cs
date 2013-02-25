namespace ManifestManagerUtility
{
   partial class MainForm
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

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.components = new System.ComponentModel.Container();
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
         System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
         System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
         System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
         this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
         this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
         this.fileExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.addFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.deleteSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
         this.helpAboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.fileOpenToolStripButton = new System.Windows.Forms.ToolStripButton();
         this.fileSaveToolStripButton = new System.Windows.Forms.ToolStripButton();
         this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
         this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
         this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
         this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
         this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
         this.appIdLabel = new System.Windows.Forms.Label();
         this.appIdTextBox = new System.Windows.Forms.TextBox();
         this.versionLabel = new System.Windows.Forms.Label();
         this.versionTextBox = new System.Windows.Forms.TextBox();
         this.dataGridView1 = new System.Windows.Forms.DataGridView();
         this.appFilesLabel = new System.Windows.Forms.Label();
         this.depProviderLabel = new System.Windows.Forms.Label();
         this.depProviderTextBox = new System.Windows.Forms.TextBox();
         this.appManifestPathTextBox = new System.Windows.Forms.TextBox();
         this.label1 = new System.Windows.Forms.Label();
         this.browseButton = new System.Windows.Forms.Button();
         this.mainMenu = new System.Windows.Forms.MenuStrip();
         this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
         this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSep = new System.Windows.Forms.ToolStripSeparator();
         this.saveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
         this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.editToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
         this.addFilesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.deleteSelectedFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.mainToolStrip = new System.Windows.Forms.ToolStrip();
         this.fileOpenToolStripItem = new System.Windows.Forms.ToolStripButton();
         this.fileSaveToolStripItem = new System.Windows.Forms.ToolStripButton();
         this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
         this.addFilesToolStripItem = new System.Windows.Forms.ToolStripButton();
         this.deleteFileToolStripItem = new System.Windows.Forms.ToolStripButton();
         this.dataFileCheckboxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
         this.fileNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.relPathColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.filesBindingSource = new System.Windows.Forms.BindingSource(this.components);
         ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
         this.mainMenu.SuspendLayout();
         this.mainToolStrip.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.filesBindingSource)).BeginInit();
         this.SuspendLayout();
         // 
         // fileToolStripMenuItem
         // 
         this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripSeparator,
            this.saveToolStripMenuItem,
            this.toolStripSeparator2,
            this.fileExitMenuItem});
         this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
         this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
         this.fileToolStripMenuItem.Text = "&File";
         // 
         // openToolStripMenuItem
         // 
         this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
         this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.openToolStripMenuItem.Name = "openToolStripMenuItem";
         this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
         this.openToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
         this.openToolStripMenuItem.Text = "&Open";
         this.openToolStripMenuItem.Click += new System.EventHandler(this.OnFileOpen);
         // 
         // toolStripSeparator
         // 
         this.toolStripSeparator.Name = "toolStripSeparator";
         this.toolStripSeparator.Size = new System.Drawing.Size(160, 6);
         // 
         // saveToolStripMenuItem
         // 
         this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
         this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
         this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
         this.saveToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
         this.saveToolStripMenuItem.Text = "&Save";
         this.saveToolStripMenuItem.Click += new System.EventHandler(this.OnFileSave);
         // 
         // toolStripSeparator2
         // 
         this.toolStripSeparator2.Name = "toolStripSeparator2";
         this.toolStripSeparator2.Size = new System.Drawing.Size(160, 6);
         // 
         // m_FileExitMenuItem
         // 
         this.fileExitMenuItem.Name = "m_FileExitMenuItem";
         this.fileExitMenuItem.Size = new System.Drawing.Size(163, 22);
         this.fileExitMenuItem.Text = "E&xit";
         this.fileExitMenuItem.Click += new System.EventHandler(this.OnFileExit);
         // 
         // editToolStripMenuItem
         // 
         this.editToolStripMenuItem.Name = "editToolStripMenuItem";
         this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
         this.editToolStripMenuItem.Text = "&Edit";
         // 
         // addFilesToolStripMenuItem
         // 
         this.addFilesToolStripMenuItem.Image = global::ManifestManagerUtility.Properties.Resources.NewDocument;
         this.addFilesToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Fuchsia;
         this.addFilesToolStripMenuItem.Name = "addFilesToolStripMenuItem";
         this.addFilesToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
         this.addFilesToolStripMenuItem.Text = "&Add Files";
         // 
         // deleteSToolStripMenuItem
         // 
         this.deleteSToolStripMenuItem.Image = global::ManifestManagerUtility.Properties.Resources.delete;
         this.deleteSToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Fuchsia;
         this.deleteSToolStripMenuItem.Name = "deleteSToolStripMenuItem";
         this.deleteSToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
         this.deleteSToolStripMenuItem.Text = "Delete Selected File";
         // 
         // m_HelpMenu
         // 
         this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpAboutMenuItem});
         this.helpMenu.Name = "m_HelpMenu";
         this.helpMenu.Size = new System.Drawing.Size(40, 20);
         this.helpMenu.Text = "&Help";
         // 
         // m_HelpAboutMenuItem
         // 
         this.helpAboutMenuItem.Name = "m_HelpAboutMenuItem";
         this.helpAboutMenuItem.Size = new System.Drawing.Size(132, 22);
         this.helpAboutMenuItem.Text = "&About...";
         this.helpAboutMenuItem.Click += new System.EventHandler(this.OnFileAbout);
         // 
         // m_FileOpenToolStripButton
         // 
         this.fileOpenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.fileOpenToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("m_FileOpenToolStripButton.Image")));
         this.fileOpenToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.fileOpenToolStripButton.Name = "m_FileOpenToolStripButton";
         this.fileOpenToolStripButton.Size = new System.Drawing.Size(23, 22);
         this.fileOpenToolStripButton.Text = "&Open";
         this.fileOpenToolStripButton.Click += new System.EventHandler(this.OnFileOpen);
         // 
         // m_FileSaveToolStripButton
         // 
         this.fileSaveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.fileSaveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("m_FileSaveToolStripButton.Image")));
         this.fileSaveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.fileSaveToolStripButton.Name = "m_FileSaveToolStripButton";
         this.fileSaveToolStripButton.Size = new System.Drawing.Size(23, 22);
         this.fileSaveToolStripButton.Text = "&Save";
         this.fileSaveToolStripButton.Click += new System.EventHandler(this.OnFileSave);
         // 
         // toolStripSeparator1
         // 
         this.toolStripSeparator1.Name = "toolStripSeparator1";
         this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
         // 
         // newToolStripButton
         // 
         this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.newToolStripButton.Image = global::ManifestManagerUtility.Properties.Resources.NewDocument;
         this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.newToolStripButton.Name = "newToolStripButton";
         this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
         this.newToolStripButton.Text = "&Add Files";
         this.newToolStripButton.Click += new System.EventHandler(this.OnAppFileAdd);
         // 
         // toolStripButton1
         // 
         this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.toolStripButton1.Image = global::ManifestManagerUtility.Properties.Resources.delete;
         this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.toolStripButton1.Name = "toolStripButton1";
         this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
         this.toolStripButton1.Text = "toolStripButton1";
         this.toolStripButton1.Click += new System.EventHandler(this.OnAppFileDelete);
         // 
         // toolStripSeparator3
         // 
         this.toolStripSeparator3.Name = "toolStripSeparator3";
         this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
         // 
         // toolStripButton2
         // 
         this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.toolStripButton2.Name = "toolStripButton2";
         this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
         this.toolStripButton2.Text = "toolStripButton2";
         // 
         // m_AppIdLabel
         // 
         this.appIdLabel.AutoSize = true;
         this.appIdLabel.Location = new System.Drawing.Point(13, 63);
         this.appIdLabel.Name = "m_AppIdLabel";
         this.appIdLabel.Size = new System.Drawing.Size(99, 13);
         this.appIdLabel.TabIndex = 2;
         this.appIdLabel.Text = "Application Identity:";
         // 
         // m_AppIdTextBox
         // 
         this.appIdTextBox.Location = new System.Drawing.Point(127, 60);
         this.appIdTextBox.Name = "m_AppIdTextBox";
         this.appIdTextBox.Size = new System.Drawing.Size(227, 20);
         this.appIdTextBox.TabIndex = 3;
         this.appIdTextBox.TextChanged += new System.EventHandler(this.OnFieldsEdited);
         // 
         // m_VersionLabel
         // 
         this.versionLabel.AutoSize = true;
         this.versionLabel.Location = new System.Drawing.Point(13, 88);
         this.versionLabel.Name = "m_VersionLabel";
         this.versionLabel.Size = new System.Drawing.Size(45, 13);
         this.versionLabel.TabIndex = 4;
         this.versionLabel.Text = "Version:";
         // 
         // m_VersionTextBox
         // 
         this.versionTextBox.Location = new System.Drawing.Point(127, 85);
         this.versionTextBox.Name = "m_VersionTextBox";
         this.versionTextBox.Size = new System.Drawing.Size(91, 20);
         this.versionTextBox.TabIndex = 5;
         this.versionTextBox.TextChanged += new System.EventHandler(this.OnFieldsEdited);
         // 
         // dataGridView1
         // 
         this.dataGridView1.AllowUserToAddRows = false;
         this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                     | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.dataGridView1.AutoGenerateColumns = false;
         dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
         dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
         dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
         dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
         dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
         dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
         this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
         this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataFileCheckboxColumn,
            this.fileNameColumn,
            this.relPathColumn});
         this.dataGridView1.DataSource = this.filesBindingSource;
         dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
         dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
         dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
         dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
         dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
         dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
         this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
         this.dataGridView1.Location = new System.Drawing.Point(12, 185);
         this.dataGridView1.Name = "dataGridView1";
         dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
         dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
         dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
         dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
         dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
         dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
         this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
         this.dataGridView1.Size = new System.Drawing.Size(748, 280);
         this.dataGridView1.TabIndex = 6;
         // 
         // m_AppFilesLabel
         // 
         this.appFilesLabel.AutoSize = true;
         this.appFilesLabel.Location = new System.Drawing.Point(9, 169);
         this.appFilesLabel.Name = "m_AppFilesLabel";
         this.appFilesLabel.Size = new System.Drawing.Size(86, 13);
         this.appFilesLabel.TabIndex = 7;
         this.appFilesLabel.Text = "Application Files:";
         // 
         // m_DepProviderLabel
         // 
         this.depProviderLabel.AutoSize = true;
         this.depProviderLabel.Location = new System.Drawing.Point(13, 113);
         this.depProviderLabel.Name = "m_DepProviderLabel";
         this.depProviderLabel.Size = new System.Drawing.Size(108, 13);
         this.depProviderLabel.TabIndex = 8;
         this.depProviderLabel.Text = "Deployment Provider:";
         // 
         // m_DepProviderTextBox
         // 
         this.depProviderTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.depProviderTextBox.Location = new System.Drawing.Point(127, 110);
         this.depProviderTextBox.Name = "m_DepProviderTextBox";
         this.depProviderTextBox.Size = new System.Drawing.Size(632, 20);
         this.depProviderTextBox.TabIndex = 9;
         this.depProviderTextBox.TextChanged += new System.EventHandler(this.OnFieldsEdited);
         // 
         // m_AppManifestPathTextBox
         // 
         this.appManifestPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.appManifestPathTextBox.Location = new System.Drawing.Point(127, 136);
         this.appManifestPathTextBox.Name = "m_AppManifestPathTextBox";
         this.appManifestPathTextBox.Size = new System.Drawing.Size(548, 20);
         this.appManifestPathTextBox.TabIndex = 11;
         this.appManifestPathTextBox.TextChanged += new System.EventHandler(this.OnFieldsEdited);
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(13, 139);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(105, 13);
         this.label1.TabIndex = 10;
         this.label1.Text = "Application Manifest:";
         // 
         // m_BrowseButton
         // 
         this.browseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.browseButton.Location = new System.Drawing.Point(681, 134);
         this.browseButton.Name = "m_BrowseButton";
         this.browseButton.Size = new System.Drawing.Size(75, 23);
         this.browseButton.TabIndex = 12;
         this.browseButton.Text = "Select...";
         this.browseButton.UseVisualStyleBackColor = true;
         this.browseButton.Click += new System.EventHandler(this.OnSelectAppManifest);
         // 
         // m_MainMenu
         // 
         this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.editToolStripMenuItem1,
            this.helpToolStripMenuItem});
         this.mainMenu.Location = new System.Drawing.Point(0, 0);
         this.mainMenu.Name = "m_MainMenu";
         this.mainMenu.Size = new System.Drawing.Size(772, 24);
         this.mainMenu.TabIndex = 13;
         this.mainMenu.Text = "menuStrip1";
         // 
         // m_FileMenu
         // 
         this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem1,
            this.toolStripSep,
            this.saveMenuItem,
            this.toolStripSeparator5,
            this.exitToolStripMenuItem});
         this.fileMenu.Name = "m_FileMenu";
         this.fileMenu.Size = new System.Drawing.Size(41, 20);
         this.fileMenu.Text = "&File";
         // 
         // openToolStripMenuItem1
         // 
         this.openToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem1.Image")));
         this.openToolStripMenuItem1.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
         this.openToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
         this.openToolStripMenuItem1.Text = "&Open";
         this.openToolStripMenuItem1.Click += new System.EventHandler(this.OnFileOpen);
         // 
         // m_ToolStripSep
         // 
         this.toolStripSep.Name = "m_ToolStripSep";
         this.toolStripSep.Size = new System.Drawing.Size(149, 6);
         // 
         // m_SaveMenuItem
         // 
         this.saveMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("m_SaveMenuItem.Image")));
         this.saveMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.saveMenuItem.Name = "m_SaveMenuItem";
         this.saveMenuItem.Size = new System.Drawing.Size(152, 22);
         this.saveMenuItem.Text = "&Save";
         this.saveMenuItem.Click += new System.EventHandler(this.OnFileSave);
         // 
         // toolStripSeparator5
         // 
         this.toolStripSeparator5.Name = "toolStripSeparator5";
         this.toolStripSeparator5.Size = new System.Drawing.Size(149, 6);
         // 
         // exitToolStripMenuItem
         // 
         this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
         this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
         this.exitToolStripMenuItem.Text = "E&xit";
         this.exitToolStripMenuItem.Click += new System.EventHandler(this.OnFileExit);
         // 
         // editToolStripMenuItem1
         // 
         this.editToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFilesMenuItem,
            this.deleteSelectedFileMenuItem});
         this.editToolStripMenuItem1.Name = "editToolStripMenuItem1";
         this.editToolStripMenuItem1.Size = new System.Drawing.Size(43, 20);
         this.editToolStripMenuItem1.Text = "&Edit";
         // 
         // m_AddFilesMenuItem
         // 
         this.addFilesMenuItem.Image = global::ManifestManagerUtility.Properties.Resources.NewDocument;
         this.addFilesMenuItem.ImageTransparentColor = System.Drawing.Color.Fuchsia;
         this.addFilesMenuItem.Name = "m_AddFilesMenuItem";
         this.addFilesMenuItem.Size = new System.Drawing.Size(203, 22);
         this.addFilesMenuItem.Text = "&Add Files";
         this.addFilesMenuItem.Click += new System.EventHandler(this.OnAppFileAdd);
         // 
         // m_DeleteSelectedFileMenuItem
         // 
         this.deleteSelectedFileMenuItem.Image = global::ManifestManagerUtility.Properties.Resources.delete;
         this.deleteSelectedFileMenuItem.ImageTransparentColor = System.Drawing.Color.Fuchsia;
         this.deleteSelectedFileMenuItem.Name = "m_DeleteSelectedFileMenuItem";
         this.deleteSelectedFileMenuItem.Size = new System.Drawing.Size(203, 22);
         this.deleteSelectedFileMenuItem.Text = "&Delete Selected File";
         this.deleteSelectedFileMenuItem.Click += new System.EventHandler(this.OnAppFileDelete);
         // 
         // helpToolStripMenuItem
         // 
         this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
         this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
         this.helpToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
         this.helpToolStripMenuItem.Text = "&Help";
         // 
         // aboutToolStripMenuItem
         // 
         this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
         this.aboutToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
         this.aboutToolStripMenuItem.Text = "&About...";
         this.aboutToolStripMenuItem.Click += new System.EventHandler(this.OnFileAbout);
         // 
         // m_MainToolStrip
         // 
         this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileOpenToolStripItem,
            this.fileSaveToolStripItem,
            this.toolStripSeparator10,
            this.addFilesToolStripItem,
            this.deleteFileToolStripItem});
         this.mainToolStrip.Location = new System.Drawing.Point(0, 24);
         this.mainToolStrip.Name = "m_MainToolStrip";
         this.mainToolStrip.Size = new System.Drawing.Size(772, 25);
         this.mainToolStrip.TabIndex = 14;
         this.mainToolStrip.Text = "toolStrip1";
         // 
         // m_FileOpenToolStripItem
         // 
         this.fileOpenToolStripItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.fileOpenToolStripItem.Image = ((System.Drawing.Image)(resources.GetObject("m_FileOpenToolStripItem.Image")));
         this.fileOpenToolStripItem.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.fileOpenToolStripItem.Name = "m_FileOpenToolStripItem";
         this.fileOpenToolStripItem.Size = new System.Drawing.Size(23, 22);
         this.fileOpenToolStripItem.Text = "&Open";
         this.fileOpenToolStripItem.Click += new System.EventHandler(this.OnFileOpen);
         // 
         // m_FileSaveToolStripItem
         // 
         this.fileSaveToolStripItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.fileSaveToolStripItem.Image = ((System.Drawing.Image)(resources.GetObject("m_FileSaveToolStripItem.Image")));
         this.fileSaveToolStripItem.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.fileSaveToolStripItem.Name = "m_FileSaveToolStripItem";
         this.fileSaveToolStripItem.Size = new System.Drawing.Size(23, 22);
         this.fileSaveToolStripItem.Text = "&Save";
         this.fileSaveToolStripItem.Click += new System.EventHandler(this.OnFileSave);
         // 
         // toolStripSeparator10
         // 
         this.toolStripSeparator10.Name = "toolStripSeparator10";
         this.toolStripSeparator10.Size = new System.Drawing.Size(6, 25);
         // 
         // m_AddFilesToolStripItem
         // 
         this.addFilesToolStripItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.addFilesToolStripItem.Image = global::ManifestManagerUtility.Properties.Resources.NewDocument;
         this.addFilesToolStripItem.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.addFilesToolStripItem.Name = "m_AddFilesToolStripItem";
         this.addFilesToolStripItem.Size = new System.Drawing.Size(23, 22);
         this.addFilesToolStripItem.Text = "&Add Files";
         this.addFilesToolStripItem.Click += new System.EventHandler(this.OnAppFileAdd);
         // 
         // m_DeleteFileToolStripItem
         // 
         this.deleteFileToolStripItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.deleteFileToolStripItem.Image = global::ManifestManagerUtility.Properties.Resources.delete;
         this.deleteFileToolStripItem.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.deleteFileToolStripItem.Name = "m_DeleteFileToolStripItem";
         this.deleteFileToolStripItem.Size = new System.Drawing.Size(23, 22);
         this.deleteFileToolStripItem.Text = "&Delete Selected File";
         this.deleteFileToolStripItem.Click += new System.EventHandler(this.OnAppFileDelete);
         // 
         // m_DataFileCheckboxColumn
         // 
         this.dataFileCheckboxColumn.DataPropertyName = "DataFile";
         this.dataFileCheckboxColumn.HeaderText = "DataFile";
         this.dataFileCheckboxColumn.Name = "m_DataFileCheckboxColumn";
         // 
         // m_FileNameColumn
         // 
         this.fileNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
         this.fileNameColumn.DataPropertyName = "FileName";
         this.fileNameColumn.HeaderText = "File Name";
         this.fileNameColumn.Name = "m_FileNameColumn";
         this.fileNameColumn.Width = 79;
         // 
         // m_RelPathColumn
         // 
         this.relPathColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
         this.relPathColumn.DataPropertyName = "RelativePath";
         this.relPathColumn.HeaderText = "Relative Path";
         this.relPathColumn.Name = "m_RelPathColumn";
         // 
         // m_FilesBindingSource
         // 
         this.filesBindingSource.DataSource = typeof(ClickOnceUtils.ApplicationFile);
         // 
         // MainForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(772, 483);
         this.Controls.Add(this.mainToolStrip);
         this.Controls.Add(this.browseButton);
         this.Controls.Add(this.appManifestPathTextBox);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.depProviderTextBox);
         this.Controls.Add(this.depProviderLabel);
         this.Controls.Add(this.appFilesLabel);
         this.Controls.Add(this.dataGridView1);
         this.Controls.Add(this.versionTextBox);
         this.Controls.Add(this.versionLabel);
         this.Controls.Add(this.appIdTextBox);
         this.Controls.Add(this.appIdLabel);
         this.Controls.Add(this.mainMenu);
         this.MainMenuStrip = this.mainMenu;
         this.Name = "MainForm";
         this.Text = "Manifest Manager";
         this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
         ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
         this.mainMenu.ResumeLayout(false);
         this.mainMenu.PerformLayout();
         this.mainToolStrip.ResumeLayout(false);
         this.mainToolStrip.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.filesBindingSource)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
      private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
      private System.Windows.Forms.ToolStripMenuItem fileExitMenuItem;
      private System.Windows.Forms.ToolStripMenuItem helpMenu;
      private System.Windows.Forms.ToolStripMenuItem helpAboutMenuItem;
      private System.Windows.Forms.ToolStripButton fileOpenToolStripButton;
      private System.Windows.Forms.ToolStripButton fileSaveToolStripButton;
      private System.Windows.Forms.Label appIdLabel;
      private System.Windows.Forms.TextBox appIdTextBox;
      private System.Windows.Forms.Label versionLabel;
      private System.Windows.Forms.TextBox versionTextBox;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
      private System.Windows.Forms.ToolStripButton toolStripButton1;
      private System.Windows.Forms.ToolStripButton newToolStripButton;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
      private System.Windows.Forms.DataGridView dataGridView1;
      private System.Windows.Forms.Label appFilesLabel;
      private System.Windows.Forms.BindingSource filesBindingSource;
      private System.Windows.Forms.Label depProviderLabel;
      private System.Windows.Forms.TextBox depProviderTextBox;
      private System.Windows.Forms.TextBox appManifestPathTextBox;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Button browseButton;
      private System.Windows.Forms.DataGridViewCheckBoxColumn dataFileCheckboxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn fileNameColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn relPathColumn;
      private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem addFilesToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem deleteSToolStripMenuItem;
      private System.Windows.Forms.ToolStripButton toolStripButton2;
      private System.Windows.Forms.MenuStrip mainMenu;
      private System.Windows.Forms.ToolStripMenuItem fileMenu;
      private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem1;
      private System.Windows.Forms.ToolStripSeparator toolStripSep;
      private System.Windows.Forms.ToolStripMenuItem saveMenuItem;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
      private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem1;
      private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
      private System.Windows.Forms.ToolStrip mainToolStrip;
      private System.Windows.Forms.ToolStripButton fileOpenToolStripItem;
      private System.Windows.Forms.ToolStripButton fileSaveToolStripItem;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
      private System.Windows.Forms.ToolStripButton addFilesToolStripItem;
      private System.Windows.Forms.ToolStripButton deleteFileToolStripItem;
      private System.Windows.Forms.ToolStripMenuItem addFilesMenuItem;
      private System.Windows.Forms.ToolStripMenuItem deleteSelectedFileMenuItem;
   }
}

