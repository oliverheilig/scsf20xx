namespace GlobalBank.Support
{
	partial class FormError
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
			this.textboxExceptionMessage = new System.Windows.Forms.TextBox();
			this.buttonOK = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(401, 42);
			this.label1.TabIndex = 0;
			this.label1.Text = "The support module found that the RI cannot connect to the \'GlobalBank\' database." +
				" The database can be installed by running the SetUpDatabase.cmd batch under the " +
				"scripts folder.\r\n";
			// 
			// textboxExceptionMessage
			// 
			this.textboxExceptionMessage.Location = new System.Drawing.Point(15, 103);
			this.textboxExceptionMessage.Multiline = true;
			this.textboxExceptionMessage.Name = "textboxExceptionMessage";
			this.textboxExceptionMessage.Size = new System.Drawing.Size(398, 127);
			this.textboxExceptionMessage.TabIndex = 1;
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(338, 236);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 2;
			this.buttonOK.Text = "Ok";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(15, 84);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(73, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Detailed error:";
			// 
			// FormError
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(425, 271);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.textboxExceptionMessage);
			this.Controls.Add(this.label1);
			this.Name = "FormError";
			this.Text = "Support Form";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textboxExceptionMessage;
	}
}