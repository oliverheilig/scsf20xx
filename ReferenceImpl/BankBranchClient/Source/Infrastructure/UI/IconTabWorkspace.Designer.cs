namespace GlobalBank.Infrastructure.UI
{
	partial class IconTabWorkspace
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
			this._toolStrip = new System.Windows.Forms.ToolStrip();
			this._deckWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
			this.SuspendLayout();
			// 
			// _toolStrip
			// 
			this._toolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
			this._toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this._toolStrip.Location = new System.Drawing.Point(0, 295);
			this._toolStrip.Name = "_toolStrip";
			this._toolStrip.Size = new System.Drawing.Size(207, 25);
			this._toolStrip.TabIndex = 1;
			this._toolStrip.Text = "toolStrip";
			this._toolStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this._toolStrip_ItemClicked);
			// 
			// _deckWorkspace
			// 
			this._deckWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
			this._deckWorkspace.Location = new System.Drawing.Point(0, 0);
			this._deckWorkspace.Name = "_deckWorkspace";
			this._deckWorkspace.Size = new System.Drawing.Size(207, 295);
			this._deckWorkspace.TabIndex = 2;
			this._deckWorkspace.Text = "deckWorkspace1";
			this._deckWorkspace.SmartPartClosing += new System.EventHandler<Microsoft.Practices.CompositeUI.SmartParts.WorkspaceCancelEventArgs>(this._deckWorkspace_SmartPartClosing);
			// 
			// IconTabWorkspace
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._deckWorkspace);
			this.Controls.Add(this._toolStrip);
			this.Name = "IconTabWorkspace";
			this.Size = new System.Drawing.Size(207, 320);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip _toolStrip;
		private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace _deckWorkspace;
	}
}
