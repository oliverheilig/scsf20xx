namespace ManifestManagerUtility
{
   partial class ManifestSigningDialog
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
         this.m_PathPrompt = new System.Windows.Forms.Label();
         this.m_PathTextBox = new System.Windows.Forms.TextBox();
         this.m_BrowseButton = new System.Windows.Forms.Button();
         this.m_PasswordTextBox = new System.Windows.Forms.TextBox();
         this.m_PasswordPrompt = new System.Windows.Forms.Label();
         this.m_SaveButton = new System.Windows.Forms.Button();
         this.m_CancelButton = new System.Windows.Forms.Button();
         this.m_ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
         ((System.ComponentModel.ISupportInitialize)(this.m_ErrorProvider)).BeginInit();
         this.SuspendLayout();
         // 
         // m_PathPrompt
         // 
         this.m_PathPrompt.AutoSize = true;
         this.m_PathPrompt.Location = new System.Drawing.Point(2, 13);
         this.m_PathPrompt.Name = "m_PathPrompt";
         this.m_PathPrompt.Size = new System.Drawing.Size(32, 13);
         this.m_PathPrompt.TabIndex = 0;
         this.m_PathPrompt.Text = "Path:";
         // 
         // m_PathTextBox
         // 
         this.m_PathTextBox.Location = new System.Drawing.Point(64, 10);
         this.m_PathTextBox.Name = "m_PathTextBox";
         this.m_PathTextBox.Size = new System.Drawing.Size(370, 20);
         this.m_PathTextBox.TabIndex = 1;
         // 
         // m_BrowseButton
         // 
         this.m_BrowseButton.Location = new System.Drawing.Point(455, 8);
         this.m_BrowseButton.Name = "m_BrowseButton";
         this.m_BrowseButton.Size = new System.Drawing.Size(75, 23);
         this.m_BrowseButton.TabIndex = 2;
         this.m_BrowseButton.Text = "Browse...";
         this.m_BrowseButton.UseVisualStyleBackColor = true;
         this.m_BrowseButton.Click += new System.EventHandler(this.OnBrowse);
         // 
         // m_PasswordTextBox
         // 
         this.m_PasswordTextBox.Location = new System.Drawing.Point(64, 40);
         this.m_PasswordTextBox.Name = "m_PasswordTextBox";
         this.m_PasswordTextBox.Size = new System.Drawing.Size(160, 20);
         this.m_PasswordTextBox.TabIndex = 4;
         // 
         // m_PasswordPrompt
         // 
         this.m_PasswordPrompt.AutoSize = true;
         this.m_PasswordPrompt.Location = new System.Drawing.Point(2, 43);
         this.m_PasswordPrompt.Name = "m_PasswordPrompt";
         this.m_PasswordPrompt.Size = new System.Drawing.Size(56, 13);
         this.m_PasswordPrompt.TabIndex = 3;
         this.m_PasswordPrompt.Text = "Password:";
         // 
         // m_SaveButton
         // 
         this.m_SaveButton.Location = new System.Drawing.Point(282, 105);
         this.m_SaveButton.Name = "m_SaveButton";
         this.m_SaveButton.Size = new System.Drawing.Size(142, 23);
         this.m_SaveButton.TabIndex = 5;
         this.m_SaveButton.Text = "Save and Sign";
         this.m_SaveButton.UseVisualStyleBackColor = true;
         this.m_SaveButton.Click += new System.EventHandler(this.OnSignAndSave);
         // 
         // m_CancelButton
         // 
         this.m_CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.m_CancelButton.Location = new System.Drawing.Point(444, 105);
         this.m_CancelButton.Name = "m_CancelButton";
         this.m_CancelButton.Size = new System.Drawing.Size(75, 23);
         this.m_CancelButton.TabIndex = 6;
         this.m_CancelButton.Text = "Cancel";
         this.m_CancelButton.UseVisualStyleBackColor = true;
         this.m_CancelButton.Click += new System.EventHandler(this.OnCancel);
         // 
         // m_ErrorProvider
         // 
         this.m_ErrorProvider.ContainerControl = this;
         // 
         // ManifestSigningDialog
         // 
         this.AcceptButton = this.m_SaveButton;
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.CancelButton = this.m_CancelButton;
         this.ClientSize = new System.Drawing.Size(531, 131);
         this.Controls.Add(this.m_CancelButton);
         this.Controls.Add(this.m_SaveButton);
         this.Controls.Add(this.m_PasswordPrompt);
         this.Controls.Add(this.m_PasswordTextBox);
         this.Controls.Add(this.m_BrowseButton);
         this.Controls.Add(this.m_PathTextBox);
         this.Controls.Add(this.m_PathPrompt);
         this.Name = "ManifestSigningDialog";
         this.Text = "Select Publisher Certificate to Sign Manifest";
         ((System.ComponentModel.ISupportInitialize)(this.m_ErrorProvider)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label m_PathPrompt;
      private System.Windows.Forms.TextBox m_PathTextBox;
      private System.Windows.Forms.Button m_BrowseButton;
      private System.Windows.Forms.TextBox m_PasswordTextBox;
      private System.Windows.Forms.Label m_PasswordPrompt;
      private System.Windows.Forms.Button m_SaveButton;
      private System.Windows.Forms.Button m_CancelButton;
      private System.Windows.Forms.ErrorProvider m_ErrorProvider;
   }
}