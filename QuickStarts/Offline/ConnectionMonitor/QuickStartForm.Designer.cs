namespace QuickStarts.ConnectionMonitor
{
	partial class QuickStartForm
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
			System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Connections", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Networks", System.Windows.Forms.HorizontalAlignment.Left);
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuickStartForm));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.connectionsListView = new System.Windows.Forms.ListView();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.addConnectionButton = new System.Windows.Forms.Button();
			this.priceTextBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.connectionTypeDropDown = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.connectionNameTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.networkAddresstextBox = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.networkAddButton = new System.Windows.Forms.Button();
			this.networkNameTextBox = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.refreshButton = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.panel1.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox1.Image = global::QuickStarts.ConnectionMonitor.Properties.Resources.Logo;
			this.pictureBox1.Location = new System.Drawing.Point(515, 25);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(70, 50);
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Location = new System.Drawing.Point(-3, -3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(607, 89);
			this.panel1.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(19, 42);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(347, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "This QuickStart demonstrates the Connection Monitor Application Block.";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(15, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(244, 19);
			this.label1.TabIndex = 3;
			this.label1.Text = "Connection Monitor QuickStart";
			// 
			// connectionsListView
			// 
			this.connectionsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.connectionsListView.ContextMenuStrip = this.contextMenuStrip1;
			this.connectionsListView.GridLines = true;
			listViewGroup3.Header = "Connections";
			listViewGroup3.Name = "connectionsListViewGroup";
			listViewGroup4.Header = "Networks";
			listViewGroup4.Name = "networksListViewGroup";
			this.connectionsListView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup3,
            listViewGroup4});
			this.connectionsListView.LargeImageList = this.imageList1;
			this.connectionsListView.Location = new System.Drawing.Point(12, 121);
			this.connectionsListView.MultiSelect = false;
			this.connectionsListView.Name = "connectionsListView";
			this.connectionsListView.ShowItemToolTips = true;
			this.connectionsListView.Size = new System.Drawing.Size(579, 187);
			this.connectionsListView.TabIndex = 5;
			this.connectionsListView.UseCompatibleStateImageBehavior = false;
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.disconnectToolStripMenuItem,
            this.connectToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(138, 48);
			this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
			// 
			// disconnectToolStripMenuItem
			// 
			this.disconnectToolStripMenuItem.Enabled = false;
			this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
			this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
			this.disconnectToolStripMenuItem.Text = "Disconnect";
			this.disconnectToolStripMenuItem.Click += new System.EventHandler(this.disconnectToolStripMenuItem_Click);
			// 
			// connectToolStripMenuItem
			// 
			this.connectToolStripMenuItem.Enabled = false;
			this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
			this.connectToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
			this.connectToolStripMenuItem.Text = "Connect";
			this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "connectionConnected");
			this.imageList1.Images.SetKeyName(1, "connectionDisconnected");
			this.imageList1.Images.SetKeyName(2, "networkConnected");
			this.imageList1.Images.SetKeyName(3, "networkDisconnected");
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox1.Controls.Add(this.addConnectionButton);
			this.groupBox1.Controls.Add(this.priceTextBox);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.connectionTypeDropDown);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.connectionNameTextBox);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Location = new System.Drawing.Point(12, 314);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(299, 131);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Add new connection";
			// 
			// addConnectionButton
			// 
			this.addConnectionButton.Location = new System.Drawing.Point(213, 98);
			this.addConnectionButton.Name = "addConnectionButton";
			this.addConnectionButton.Size = new System.Drawing.Size(75, 23);
			this.addConnectionButton.TabIndex = 4;
			this.addConnectionButton.Text = "Add";
			this.addConnectionButton.UseVisualStyleBackColor = true;
			this.addConnectionButton.Click += new System.EventHandler(this.addConnectionButton_Click);
			// 
			// priceTextBox
			// 
			this.priceTextBox.Location = new System.Drawing.Point(129, 72);
			this.priceTextBox.Name = "priceTextBox";
			this.priceTextBox.Size = new System.Drawing.Size(159, 20);
			this.priceTextBox.TabIndex = 3;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(7, 72);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(31, 13);
			this.label5.TabIndex = 4;
			this.label5.Text = "Price";
			// 
			// connectionTypeDropDown
			// 
			this.connectionTypeDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.connectionTypeDropDown.FormattingEnabled = true;
			this.connectionTypeDropDown.Items.AddRange(new object[] {
            "DesktopConnection",
            "NicConnection",
            "WiredConnection",
            "WirelessConnection"});
			this.connectionTypeDropDown.Location = new System.Drawing.Point(129, 43);
			this.connectionTypeDropDown.Name = "connectionTypeDropDown";
			this.connectionTypeDropDown.Size = new System.Drawing.Size(159, 21);
			this.connectionTypeDropDown.TabIndex = 2;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(7, 46);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(88, 13);
			this.label4.TabIndex = 2;
			this.label4.Text = "Connection Type";
			// 
			// connectionNameTextBox
			// 
			this.connectionNameTextBox.Location = new System.Drawing.Point(129, 17);
			this.connectionNameTextBox.Name = "connectionNameTextBox";
			this.connectionNameTextBox.Size = new System.Drawing.Size(159, 20);
			this.connectionNameTextBox.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(7, 20);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(92, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Connection Name";
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox2.Controls.Add(this.networkAddresstextBox);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.networkAddButton);
			this.groupBox2.Controls.Add(this.networkNameTextBox);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Location = new System.Drawing.Point(320, 314);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(271, 131);
			this.groupBox2.TabIndex = 7;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Add new network";
			// 
			// networkAddresstextBox
			// 
			this.networkAddresstextBox.Location = new System.Drawing.Point(102, 44);
			this.networkAddresstextBox.Name = "networkAddresstextBox";
			this.networkAddresstextBox.Size = new System.Drawing.Size(156, 20);
			this.networkAddresstextBox.TabIndex = 6;
			this.networkAddresstextBox.TextChanged += new System.EventHandler(this.networkAddresstextBox_TextChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(7, 46);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(88, 13);
			this.label6.TabIndex = 3;
			this.label6.Text = "Network Address";
			// 
			// networkAddButton
			// 
			this.networkAddButton.Location = new System.Drawing.Point(183, 98);
			this.networkAddButton.Name = "networkAddButton";
			this.networkAddButton.Size = new System.Drawing.Size(75, 23);
			this.networkAddButton.TabIndex = 7;
			this.networkAddButton.Text = "Add";
			this.networkAddButton.UseVisualStyleBackColor = true;
			this.networkAddButton.Click += new System.EventHandler(this.networkAddButton_Click);
			// 
			// networkNameTextBox
			// 
			this.networkNameTextBox.Location = new System.Drawing.Point(102, 20);
			this.networkNameTextBox.Name = "networkNameTextBox";
			this.networkNameTextBox.Size = new System.Drawing.Size(156, 20);
			this.networkNameTextBox.TabIndex = 5;
			this.networkNameTextBox.TextChanged += new System.EventHandler(this.networkNameTextBox_TextChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(7, 20);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(78, 13);
			this.label7.TabIndex = 0;
			this.label7.Text = "Network Name";
			// 
			// refreshButton
			// 
			this.refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.refreshButton.Location = new System.Drawing.Point(515, 91);
			this.refreshButton.Name = "refreshButton";
			this.refreshButton.Size = new System.Drawing.Size(75, 23);
			this.refreshButton.TabIndex = 8;
			this.refreshButton.Text = "Refresh";
			this.refreshButton.UseVisualStyleBackColor = true;
			this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(12, 97);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(181, 13);
			this.label8.TabIndex = 9;
			this.label8.Text = "Available Connections and Networks";
			// 
			// QuickStartForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(603, 457);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.refreshButton);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.connectionsListView);
			this.Controls.Add(this.panel1);
			this.Name = "QuickStartForm";
			this.Text = "Smart Client Software Factory QuickStart";
			this.Load += new System.EventHandler(this.QuickStartForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.contextMenuStrip1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView connectionsListView;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox priceTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox connectionTypeDropDown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox connectionNameTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button addConnectionButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox networkNameTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button networkAddButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox networkAddresstextBox;
		private System.Windows.Forms.Label label6;
	}
}

