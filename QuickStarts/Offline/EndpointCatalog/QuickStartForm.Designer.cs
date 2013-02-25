namespace QuickStarts.EndpointCatalog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuickStartForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.endPointsListView = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.networkDropDown = new System.Windows.Forms.ComboBox();
            this.endpointsCountLabel = new System.Windows.Forms.Label();
            this.configurationGroupBox = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.domainTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.userTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.addressTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.configurationGroupBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::QuickStarts.EndpointCatalog.Properties.Resources.Logo;
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
            this.label2.Size = new System.Drawing.Size(336, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "This QuickStart demonstrates the Endpoint Catalog Application Block.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "Endpoint Catalog QuickStart";
            // 
            // endPointsListView
            // 
            this.endPointsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.endPointsListView.LargeImageList = this.imageList1;
            this.endPointsListView.Location = new System.Drawing.Point(13, 107);
            this.endPointsListView.MultiSelect = false;
            this.endPointsListView.Name = "endPointsListView";
            this.endPointsListView.ShowItemToolTips = true;
            this.endPointsListView.Size = new System.Drawing.Size(566, 193);
            this.endPointsListView.TabIndex = 4;
            this.endPointsListView.UseCompatibleStateImageBehavior = false;
            this.endPointsListView.View = System.Windows.Forms.View.Tile;
            this.endPointsListView.SelectedIndexChanged += new System.EventHandler(this.endPointsListView_SelectedIndexChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "endpoint");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 309);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(280, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "See the endpoint\'s configuration for the following network:";
            // 
            // networkDropDown
            // 
            this.networkDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.networkDropDown.FormattingEnabled = true;
            this.networkDropDown.Items.AddRange(new object[] {
            "Not Specified",
            "Work",
            "Internet"});
            this.networkDropDown.Location = new System.Drawing.Point(302, 306);
            this.networkDropDown.Name = "networkDropDown";
            this.networkDropDown.Size = new System.Drawing.Size(179, 21);
            this.networkDropDown.TabIndex = 6;
            this.networkDropDown.SelectedIndexChanged += new System.EventHandler(this.networkDropDown_SelectedIndexChanged);
            // 
            // endpointsCountLabel
            // 
            this.endpointsCountLabel.AutoSize = true;
            this.endpointsCountLabel.Location = new System.Drawing.Point(10, 89);
            this.endpointsCountLabel.Name = "endpointsCountLabel";
            this.endpointsCountLabel.Size = new System.Drawing.Size(0, 13);
            this.endpointsCountLabel.TabIndex = 7;
            // 
            // configurationGroupBox
            // 
            this.configurationGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.configurationGroupBox.Controls.Add(this.groupBox2);
            this.configurationGroupBox.Controls.Add(this.addressTextBox);
            this.configurationGroupBox.Controls.Add(this.label5);
            this.configurationGroupBox.Location = new System.Drawing.Point(16, 333);
            this.configurationGroupBox.Name = "configurationGroupBox";
            this.configurationGroupBox.Size = new System.Drawing.Size(566, 166);
            this.configurationGroupBox.TabIndex = 8;
            this.configurationGroupBox.TabStop = false;
            this.configurationGroupBox.Text = "Configuration";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.domainTextBox);
            this.groupBox2.Controls.Add(this.passwordTextBox);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.userTextBox);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(6, 51);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(270, 105);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Credential";
            // 
            // domainTextBox
            // 
            this.domainTextBox.Location = new System.Drawing.Point(75, 65);
            this.domainTextBox.Name = "domainTextBox";
            this.domainTextBox.ReadOnly = true;
            this.domainTextBox.Size = new System.Drawing.Size(189, 20);
            this.domainTextBox.TabIndex = 5;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(75, 43);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.ReadOnly = true;
            this.passwordTextBox.Size = new System.Drawing.Size(189, 20);
            this.passwordTextBox.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Domain:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Password:";
            // 
            // userTextBox
            // 
            this.userTextBox.Location = new System.Drawing.Point(75, 21);
            this.userTextBox.Name = "userTextBox";
            this.userTextBox.ReadOnly = true;
            this.userTextBox.Size = new System.Drawing.Size(189, 20);
            this.userTextBox.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "User name:";
            // 
            // addressTextBox
            // 
            this.addressTextBox.Location = new System.Drawing.Point(60, 22);
            this.addressTextBox.Name = "addressTextBox";
            this.addressTextBox.ReadOnly = true;
            this.addressTextBox.Size = new System.Drawing.Size(488, 20);
            this.addressTextBox.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Address:";
            // 
            // QuickStartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 501);
            this.Controls.Add(this.configurationGroupBox);
            this.Controls.Add(this.endpointsCountLabel);
            this.Controls.Add(this.networkDropDown);
            this.Controls.Add(this.endPointsListView);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Name = "QuickStartForm";
            this.Text = "Smart Client Software Factory QuickStart";
            this.Load += new System.EventHandler(this.QuickStartForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.configurationGroupBox.ResumeLayout(false);
            this.configurationGroupBox.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView endPointsListView;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox networkDropDown;
        private System.Windows.Forms.Label endpointsCountLabel;
        private System.Windows.Forms.GroupBox configurationGroupBox;
        private System.Windows.Forms.TextBox addressTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox domainTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox userTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ImageList imageList1;
	}
}

