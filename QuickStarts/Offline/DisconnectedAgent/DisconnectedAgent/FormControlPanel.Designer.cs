namespace Quickstarts.DisconnectedAgent
{
	partial class FormControlPanel
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
			this.label1 = new System.Windows.Forms.Label();
			this.manualDispatchButton = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.labelConnectionStatus = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.requestQueueContextStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.dispatchSelectedRequestsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.requestQueueListView = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader14 = new System.Windows.Forms.ColumnHeader();
			this.failedQueueContextStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.enqueueSelectedRequestsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.removeSelectedRequestsFromQueueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.servedRequestsListView = new System.Windows.Forms.ListView();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader16 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
			this.servedRequestsQueue = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.clearListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.failedQueueListView = new System.Windows.Forms.ListView();
			this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader15 = new System.Windows.Forms.ColumnHeader();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.dispatchRequestsLabel = new System.Windows.Forms.Label();
			this.tagComboBox = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.webServiceEnabledCheckBox = new System.Windows.Forms.CheckBox();
			this.disconnectedRadioButton = new System.Windows.Forms.RadioButton();
			this.connectedRadioButton = new System.Windows.Forms.RadioButton();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label4 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label5 = new System.Windows.Forms.Label();
			this.requestQueueContextStrip.SuspendLayout();
			this.failedQueueContextStrip.SuspendLayout();
			this.servedRequestsQueue.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 225);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(93, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Queued Requests";
			// 
			// manualDispatchButton
			// 
			this.manualDispatchButton.Location = new System.Drawing.Point(293, 91);
			this.manualDispatchButton.Name = "manualDispatchButton";
			this.manualDispatchButton.Size = new System.Drawing.Size(96, 23);
			this.manualDispatchButton.TabIndex = 3;
			this.manualDispatchButton.Text = "Dispatch";
			this.manualDispatchButton.UseVisualStyleBackColor = true;
			this.manualDispatchButton.Click += new System.EventHandler(this.buttonManualDispatch_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(18, 341);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(89, 13);
			this.label3.TabIndex = 1;
			this.label3.Text = "Served Requests";
			// 
			// labelConnectionStatus
			// 
			this.labelConnectionStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.labelConnectionStatus.AutoSize = true;
			this.labelConnectionStatus.Location = new System.Drawing.Point(677, 581);
			this.labelConnectionStatus.Name = "labelConnectionStatus";
			this.labelConnectionStatus.Size = new System.Drawing.Size(113, 13);
			this.labelConnectionStatus.TabIndex = 4;
			this.labelConnectionStatus.Text = "labelConnectionStatus";
			this.labelConnectionStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 454);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(83, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Failed Requests";
			// 
			// requestQueueContextStrip
			// 
			this.requestQueueContextStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dispatchSelectedRequestsToolStripMenuItem});
			this.requestQueueContextStrip.Name = "requestQueueContextStrip";
			this.requestQueueContextStrip.Size = new System.Drawing.Size(218, 26);
			this.requestQueueContextStrip.Opening += new System.ComponentModel.CancelEventHandler(this.requestQueueContextStrip_Opening);
			// 
			// dispatchSelectedRequestsToolStripMenuItem
			// 
			this.dispatchSelectedRequestsToolStripMenuItem.Name = "dispatchSelectedRequestsToolStripMenuItem";
			this.dispatchSelectedRequestsToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
			this.dispatchSelectedRequestsToolStripMenuItem.Text = "Dispatch Selected Requests";
			this.dispatchSelectedRequestsToolStripMenuItem.Click += new System.EventHandler(this.dispatchSelectedRequestsToolStripMenuItem_Click);
			// 
			// requestQueueListView
			// 
			this.requestQueueListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader4,
            this.columnHeader14});
			this.requestQueueListView.ContextMenuStrip = this.requestQueueContextStrip;
			this.requestQueueListView.FullRowSelect = true;
			this.requestQueueListView.Location = new System.Drawing.Point(16, 242);
			this.requestQueueListView.Name = "requestQueueListView";
			this.requestQueueListView.Size = new System.Drawing.Size(774, 90);
			this.requestQueueListView.TabIndex = 8;
			this.requestQueueListView.UseCompatibleStateImageBehavior = false;
			this.requestQueueListView.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "GUID";
			this.columnHeader1.Width = 270;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Parameters";
			this.columnHeader4.Width = 150;
			// 
			// columnHeader14
			// 
			this.columnHeader14.Text = "Tag";
			this.columnHeader14.Width = 100;
			// 
			// failedQueueContextStrip
			// 
			this.failedQueueContextStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enqueueSelectedRequestsToolStripMenuItem,
            this.removeSelectedRequestsFromQueueToolStripMenuItem});
			this.failedQueueContextStrip.Name = "failedQueueContextStrip";
			this.failedQueueContextStrip.Size = new System.Drawing.Size(282, 48);
			this.failedQueueContextStrip.Opening += new System.ComponentModel.CancelEventHandler(this.failedQueueContextStrip_Opening);
			// 
			// enqueueSelectedRequestsToolStripMenuItem
			// 
			this.enqueueSelectedRequestsToolStripMenuItem.Name = "enqueueSelectedRequestsToolStripMenuItem";
			this.enqueueSelectedRequestsToolStripMenuItem.Size = new System.Drawing.Size(281, 22);
			this.enqueueSelectedRequestsToolStripMenuItem.Text = "Enqueue Selected Requests";
			this.enqueueSelectedRequestsToolStripMenuItem.Click += new System.EventHandler(this.enqueueSelectedRequestsToolStripMenuItem_Click);
			// 
			// removeSelectedRequestsFromQueueToolStripMenuItem
			// 
			this.removeSelectedRequestsFromQueueToolStripMenuItem.Name = "removeSelectedRequestsFromQueueToolStripMenuItem";
			this.removeSelectedRequestsFromQueueToolStripMenuItem.Size = new System.Drawing.Size(281, 22);
			this.removeSelectedRequestsFromQueueToolStripMenuItem.Text = "Remove Selected Requests from Queue";
			this.removeSelectedRequestsFromQueueToolStripMenuItem.Click += new System.EventHandler(this.removeSelectedRequestsFromQueueToolStripMenuItem_Click);
			// 
			// servedRequestsListView
			// 
			this.servedRequestsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader8,
            this.columnHeader16,
            this.columnHeader9});
			this.servedRequestsListView.ContextMenuStrip = this.servedRequestsQueue;
			this.servedRequestsListView.FullRowSelect = true;
			this.servedRequestsListView.Location = new System.Drawing.Point(15, 357);
			this.servedRequestsListView.Name = "servedRequestsListView";
			this.servedRequestsListView.Size = new System.Drawing.Size(775, 90);
			this.servedRequestsListView.TabIndex = 11;
			this.servedRequestsListView.UseCompatibleStateImageBehavior = false;
			this.servedRequestsListView.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "GUID";
			this.columnHeader5.Width = 270;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "Parameters";
			this.columnHeader8.Width = 150;
			// 
			// columnHeader16
			// 
			this.columnHeader16.Text = "Tag";
			this.columnHeader16.Width = 100;
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "Result";
			this.columnHeader9.Width = 100;
			// 
			// servedRequestsQueue
			// 
			this.servedRequestsQueue.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearListToolStripMenuItem});
			this.servedRequestsQueue.Name = "servedRequestsQueue";
			this.servedRequestsQueue.Size = new System.Drawing.Size(123, 26);
			this.servedRequestsQueue.Opening += new System.ComponentModel.CancelEventHandler(this.servedRequestsQueue_Opening);
			// 
			// clearListToolStripMenuItem
			// 
			this.clearListToolStripMenuItem.Name = "clearListToolStripMenuItem";
			this.clearListToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.clearListToolStripMenuItem.Text = "Clear List";
			this.clearListToolStripMenuItem.Click += new System.EventHandler(this.clearListToolStripMenuItem_Click);
			// 
			// failedQueueListView
			// 
			this.failedQueueListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader10,
            this.columnHeader13,
            this.columnHeader15});
			this.failedQueueListView.ContextMenuStrip = this.failedQueueContextStrip;
			this.failedQueueListView.FullRowSelect = true;
			this.failedQueueListView.Location = new System.Drawing.Point(15, 473);
			this.failedQueueListView.Name = "failedQueueListView";
			this.failedQueueListView.Size = new System.Drawing.Size(775, 90);
			this.failedQueueListView.TabIndex = 12;
			this.failedQueueListView.UseCompatibleStateImageBehavior = false;
			this.failedQueueListView.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "GUID";
			this.columnHeader10.Width = 270;
			// 
			// columnHeader13
			// 
			this.columnHeader13.Text = "Parameters";
			this.columnHeader13.Width = 150;
			// 
			// columnHeader15
			// 
			this.columnHeader15.Text = "Tag";
			this.columnHeader15.Width = 100;
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
			this.statusStrip.Location = new System.Drawing.Point(0, 581);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(802, 22);
			this.statusStrip.TabIndex = 13;
			this.statusStrip.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
			// 
			// dispatchRequestsLabel
			// 
			this.dispatchRequestsLabel.AutoSize = true;
			this.dispatchRequestsLabel.Location = new System.Drawing.Point(31, 67);
			this.dispatchRequestsLabel.Name = "dispatchRequestsLabel";
			this.dispatchRequestsLabel.Size = new System.Drawing.Size(153, 13);
			this.dispatchRequestsLabel.TabIndex = 14;
			this.dispatchRequestsLabel.Text = "Dispatch requests with the tag:";
			// 
			// tagComboBox
			// 
			this.tagComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tagComboBox.FormattingEnabled = true;
			this.tagComboBox.Items.AddRange(new object[] {
            "All",
            "Tag1",
            "Tag2"});
			this.tagComboBox.Location = new System.Drawing.Point(198, 64);
			this.tagComboBox.Name = "tagComboBox";
			this.tagComboBox.Size = new System.Drawing.Size(191, 21);
			this.tagComboBox.TabIndex = 15;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioButton2);
			this.groupBox1.Controls.Add(this.tagComboBox);
			this.groupBox1.Controls.Add(this.dispatchRequestsLabel);
			this.groupBox1.Controls.Add(this.radioButton1);
			this.groupBox1.Controls.Add(this.manualDispatchButton);
			this.groupBox1.Location = new System.Drawing.Point(12, 88);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(397, 125);
			this.groupBox1.TabIndex = 16;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Requests Dispatching";
			// 
			// radioButton2
			// 
			this.radioButton2.AutoSize = true;
			this.radioButton2.Checked = true;
			this.radioButton2.Location = new System.Drawing.Point(15, 43);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(119, 17);
			this.radioButton2.TabIndex = 1;
			this.radioButton2.TabStop = true;
			this.radioButton2.Text = "Manual Dispatching";
			this.radioButton2.UseVisualStyleBackColor = true;
			this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
			// 
			// radioButton1
			// 
			this.radioButton1.AutoSize = true;
			this.radioButton1.Location = new System.Drawing.Point(15, 19);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(131, 17);
			this.radioButton1.TabIndex = 0;
			this.radioButton1.Text = "Automatic Dispatching";
			this.radioButton1.UseVisualStyleBackColor = true;
			this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.webServiceEnabledCheckBox);
			this.groupBox2.Controls.Add(this.disconnectedRadioButton);
			this.groupBox2.Controls.Add(this.connectedRadioButton);
			this.groupBox2.Location = new System.Drawing.Point(415, 88);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(200, 125);
			this.groupBox2.TabIndex = 17;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Connection Status";
			// 
			// webServiceEnabledCheckBox
			// 
			this.webServiceEnabledCheckBox.AutoSize = true;
			this.webServiceEnabledCheckBox.Checked = true;
			this.webServiceEnabledCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.webServiceEnabledCheckBox.Location = new System.Drawing.Point(27, 43);
			this.webServiceEnabledCheckBox.Name = "webServiceEnabledCheckBox";
			this.webServiceEnabledCheckBox.Size = new System.Drawing.Size(124, 17);
			this.webServiceEnabledCheckBox.TabIndex = 2;
			this.webServiceEnabledCheckBox.Text = "Enable Web Service";
			this.webServiceEnabledCheckBox.UseVisualStyleBackColor = true;
			this.webServiceEnabledCheckBox.CheckedChanged += new System.EventHandler(this.webServiceEnabledCheckBox_CheckedChanged);
			// 
			// disconnectedRadioButton
			// 
			this.disconnectedRadioButton.AutoSize = true;
			this.disconnectedRadioButton.Location = new System.Drawing.Point(7, 67);
			this.disconnectedRadioButton.Name = "disconnectedRadioButton";
			this.disconnectedRadioButton.Size = new System.Drawing.Size(91, 17);
			this.disconnectedRadioButton.TabIndex = 1;
			this.disconnectedRadioButton.Text = "Disconnected";
			this.disconnectedRadioButton.UseVisualStyleBackColor = true;
			this.disconnectedRadioButton.CheckedChanged += new System.EventHandler(this.disconnectedRadioButton_CheckedChanged);
			// 
			// connectedRadioButton
			// 
			this.connectedRadioButton.AutoSize = true;
			this.connectedRadioButton.Checked = true;
			this.connectedRadioButton.Location = new System.Drawing.Point(7, 20);
			this.connectedRadioButton.Name = "connectedRadioButton";
			this.connectedRadioButton.Size = new System.Drawing.Size(77, 17);
			this.connectedRadioButton.TabIndex = 0;
			this.connectedRadioButton.TabStop = true;
			this.connectedRadioButton.Text = "Connected";
			this.connectedRadioButton.UseVisualStyleBackColor = true;
			this.connectedRadioButton.CheckedChanged += new System.EventHandler(this.connectedRadioButton_CheckedChanged);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox1.Image = global::Quickstarts.DisconnectedAgent.Properties.Resources.Logo;
			this.pictureBox1.Location = new System.Drawing.Point(714, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(70, 50);
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(15, 22);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(307, 19);
			this.label4.TabIndex = 3;
			this.label4.Text = "Disconnected Service Agent Quickstart";
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(802, 82);
			this.panel1.TabIndex = 18;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(16, 41);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(391, 13);
			this.label5.TabIndex = 5;
			this.label5.Text = "This QuickStart demonstrates the Disconnected Service Agent Application Block.";
			// 
			// FormControlPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(802, 603);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.labelConnectionStatus);
			this.Controls.Add(this.requestQueueListView);
			this.Controls.Add(this.failedQueueListView);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.servedRequestsListView);
			this.Controls.Add(this.label3);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Name = "FormControlPanel";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Request Manager Control Panel";
			this.Load += new System.EventHandler(this.FormControlPanel_Load);
			this.requestQueueContextStrip.ResumeLayout(false);
			this.failedQueueContextStrip.ResumeLayout(false);
			this.servedRequestsQueue.ResumeLayout(false);
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button manualDispatchButton;
		private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelConnectionStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip requestQueueContextStrip;
        private System.Windows.Forms.ToolStripMenuItem dispatchSelectedRequestsToolStripMenuItem;
        private System.Windows.Forms.ListView requestQueueListView;
        private System.Windows.Forms.ContextMenuStrip failedQueueContextStrip;
        private System.Windows.Forms.ToolStripMenuItem enqueueSelectedRequestsToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ListView servedRequestsListView;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ListView failedQueueListView;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label dispatchRequestsLabel;
        private System.Windows.Forms.ComboBox tagComboBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton disconnectedRadioButton;
        private System.Windows.Forms.RadioButton connectedRadioButton;
        private System.Windows.Forms.CheckBox webServiceEnabledCheckBox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ContextMenuStrip servedRequestsQueue;
        private System.Windows.Forms.ToolStripMenuItem clearListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeSelectedRequestsFromQueueToolStripMenuItem;
	}
}