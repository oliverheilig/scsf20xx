namespace GlobalBank.Infrastructure.UI
{
	partial class LinkLabelPanel
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
			this._containerPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.SuspendLayout();
			// 
			// _containerPanel
			// 
			this._containerPanel.AutoScroll = true;
			this._containerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this._containerPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this._containerPanel.Location = new System.Drawing.Point(0, 0);
			this._containerPanel.Name = "_containerPanel";
			this._containerPanel.Size = new System.Drawing.Size(151, 137);
			this._containerPanel.TabIndex = 0;
			// 
			// LinkLabelPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._containerPanel);
			this.Name = "LinkLabelPanel";
			this.Size = new System.Drawing.Size(151, 137);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel _containerPanel;
	}
}
