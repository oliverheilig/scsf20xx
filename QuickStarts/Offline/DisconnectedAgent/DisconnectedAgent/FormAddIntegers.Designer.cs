namespace Quickstarts.DisconnectedAgent
{
	partial class FormAddIntegers
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxFirstNumber = new System.Windows.Forms.TextBox();
			this.textBoxSecondNumber = new System.Windows.Forms.TextBox();
			this.buttonAdd = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.tagComboBox = new System.Windows.Forms.ComboBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(73, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "First Operand:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 41);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(91, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Second Operand:";
			// 
			// textBoxFirstNumber
			// 
			this.textBoxFirstNumber.Location = new System.Drawing.Point(109, 11);
			this.textBoxFirstNumber.Name = "textBoxFirstNumber";
			this.textBoxFirstNumber.Size = new System.Drawing.Size(115, 20);
			this.textBoxFirstNumber.TabIndex = 3;
			this.textBoxFirstNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textBoxSecondNumber
			// 
			this.textBoxSecondNumber.Location = new System.Drawing.Point(109, 37);
			this.textBoxSecondNumber.Name = "textBoxSecondNumber";
			this.textBoxSecondNumber.Size = new System.Drawing.Size(115, 20);
			this.textBoxSecondNumber.TabIndex = 4;
			this.textBoxSecondNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// buttonAdd
			// 
			this.buttonAdd.Location = new System.Drawing.Point(149, 123);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(75, 23);
			this.buttonAdd.TabIndex = 6;
			this.buttonAdd.Text = "Add";
			this.buttonAdd.UseVisualStyleBackColor = true;
			this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.tagComboBox);
			this.groupBox1.Location = new System.Drawing.Point(15, 63);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(209, 54);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Request Options";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 17);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(29, 13);
			this.label3.TabIndex = 1;
			this.label3.Text = "Tag:";
			// 
			// tagComboBox
			// 
			this.tagComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tagComboBox.FormattingEnabled = true;
			this.tagComboBox.Items.AddRange(new object[] {
            "(none)",
            "Tag1",
            "Tag2"});
			this.tagComboBox.Location = new System.Drawing.Point(94, 14);
			this.tagComboBox.Name = "tagComboBox";
			this.tagComboBox.Size = new System.Drawing.Size(100, 21);
			this.tagComboBox.TabIndex = 0;
			// 
			// FormAddIntegers
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(239, 156);
			this.ControlBox = false;
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.buttonAdd);
			this.Controls.Add(this.textBoxSecondNumber);
			this.Controls.Add(this.textBoxFirstNumber);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximumSize = new System.Drawing.Size(249, 186);
			this.MinimumSize = new System.Drawing.Size(249, 186);
			this.Name = "FormAddIntegers";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Create Add Request";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxFirstNumber;
        private System.Windows.Forms.TextBox textBoxSecondNumber;
		private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox tagComboBox;
	}
}