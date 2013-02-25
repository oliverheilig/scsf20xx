namespace GlobalBank.BranchSystems.Layout
{
	partial class LayoutView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LayoutView));
			this._mainMenuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this._launchBarWorkspace = new GlobalBank.Infrastructure.UI.IconTabWorkspace();
			this._statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this._branchSystemsWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
			this._mainStatusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this._userLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this._rolesLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this._mainMenuStrip.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this._mainStatusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// _mainMenuStrip
			// 
			this._mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
			resources.ApplyResources(this._mainMenuStrip, "_mainMenuStrip");
			this._mainMenuStrip.Name = "_mainMenuStrip";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.OnFileExit);
			// 
			// _launchBarWorkspace
			// 
			resources.ApplyResources(this._launchBarWorkspace, "_launchBarWorkspace");
			this._launchBarWorkspace.Name = "_launchBarWorkspace";
			// 
			// _statusLabel
			// 
			this._statusLabel.Name = "_statusLabel";
			resources.ApplyResources(this._statusLabel, "_statusLabel");
			// 
			// splitContainer1
			// 
			this.splitContainer1.BackColor = System.Drawing.SystemColors.Control;
			resources.ApplyResources(this.splitContainer1, "splitContainer1");
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this._launchBarWorkspace);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this._branchSystemsWorkspace);
			// 
			// _branchSystemsWorkspace
			// 
			resources.ApplyResources(this._branchSystemsWorkspace, "_branchSystemsWorkspace");
			this._branchSystemsWorkspace.Name = "_branchSystemsWorkspace";
			// 
			// _mainStatusStrip
			// 
			this._mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._statusLabel,
            this.toolStripStatusLabel1,
            this._userLabel,
            this._rolesLabel});
			resources.ApplyResources(this._mainStatusStrip, "_mainStatusStrip");
			this._mainStatusStrip.Name = "_mainStatusStrip";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
			this.toolStripStatusLabel1.Spring = true;
			// 
			// _userLabel
			// 
			this._userLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
						| System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
						| System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this._userLabel.Name = "_userLabel";
			resources.ApplyResources(this._userLabel, "_userLabel");
			// 
			// _rolesLabel
			// 
			this._rolesLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
						| System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
						| System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this._rolesLabel.Name = "_rolesLabel";
			resources.ApplyResources(this._rolesLabel, "_rolesLabel");
			// 
			// LayoutView
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this._mainStatusStrip);
			this.Controls.Add(this._mainMenuStrip);
			this.Name = "LayoutView";
			this._mainMenuStrip.ResumeLayout(false);
			this._mainMenuStrip.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this._mainStatusStrip.ResumeLayout(false);
			this._mainStatusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip _mainMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private GlobalBank.Infrastructure.UI.IconTabWorkspace _launchBarWorkspace;
		private System.Windows.Forms.ToolStripStatusLabel _statusLabel;
		private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.StatusStrip _mainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel _userLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel _rolesLabel;
		private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace _branchSystemsWorkspace;
	}
}
