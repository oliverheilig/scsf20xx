namespace Microsoft.Practices.SmartClientFactory.Editors
{
	partial class TimeSpanControl
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.minutes = new System.Windows.Forms.NumericUpDown();
			this.hours = new System.Windows.Forms.NumericUpDown();
			this.days = new System.Windows.Forms.NumericUpDown();
			this.seconds = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.minutes)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.hours)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.days)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.seconds)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(34, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Days:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 34);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(38, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Hours:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 60);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(47, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Minutes:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(4, 86);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(52, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "Seconds:";
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnOk.Location = new System.Drawing.Point(39, 124);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(55, 23);
			this.btnOk.TabIndex = 4;
			this.btnOk.Text = "&Ok";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCancel.Location = new System.Drawing.Point(100, 124);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(55, 23);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// minutes
			// 
			this.minutes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.minutes.Location = new System.Drawing.Point(62, 58);
			this.minutes.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
			this.minutes.Name = "minutes";
			this.minutes.Size = new System.Drawing.Size(93, 20);
			this.minutes.TabIndex = 2;
			this.minutes.ValueChanged += new System.EventHandler(this.OnChanged);
			// 
			// hours
			// 
			this.hours.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.hours.Location = new System.Drawing.Point(62, 32);
			this.hours.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
			this.hours.Name = "hours";
			this.hours.Size = new System.Drawing.Size(93, 20);
			this.hours.TabIndex = 1;
			this.hours.ValueChanged += new System.EventHandler(this.OnChanged);
			// 
			// days
			// 
			this.days.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.days.Location = new System.Drawing.Point(62, 6);
			this.days.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
			this.days.Name = "days";
			this.days.Size = new System.Drawing.Size(93, 20);
			this.days.TabIndex = 0;
			this.days.ValueChanged += new System.EventHandler(this.OnChanged);
			// 
			// seconds
			// 
			this.seconds.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.seconds.Location = new System.Drawing.Point(62, 84);
			this.seconds.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
			this.seconds.Name = "seconds";
			this.seconds.Size = new System.Drawing.Size(93, 20);
			this.seconds.TabIndex = 3;
			this.seconds.ValueChanged += new System.EventHandler(this.OnChanged);
			// 
			// TimeSpanControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.seconds);
			this.Controls.Add(this.minutes);
			this.Controls.Add(this.hours);
			this.Controls.Add(this.days);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "TimeSpanControl";
			this.Size = new System.Drawing.Size(163, 155);
			((System.ComponentModel.ISupportInitialize)(this.minutes)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.hours)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.days)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.seconds)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.NumericUpDown minutes;
		private System.Windows.Forms.NumericUpDown hours;
		private System.Windows.Forms.NumericUpDown days;
		private System.Windows.Forms.NumericUpDown seconds;
	}
}
