//===============================================================================
// Microsoft patterns & practices
// Smart Client Software Factory 2010
//===============================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===============================================================================
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace GlobalBank.Infrastructure.Library.Services
{
	public class UserSelectionForm : Form
	{
		private Label label1;
		private Button _cancelButton;
		private Button _okButton;
		private Label _messageLabel;
		private IContainer components = null;

		private UserData[] _users;
		private TextBox _userNameTextBox;
		private TextBox _passwordTextBox;
		private Label label3;
        private Panel panel1;
        private Label label2;
		private UserData _matchUser;

		public UserSelectionForm(UserData[] users)
		{
			_users = users;
			InitializeComponent();
		}

		public UserData SelectUser()
		{
			if (DialogResult.OK == ShowDialog())
			{
				return _matchUser;
			}

			return null;
		}


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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserSelectionForm));
			this.label1 = new System.Windows.Forms.Label();
			this._cancelButton = new System.Windows.Forms.Button();
			this._okButton = new System.Windows.Forms.Button();
			this._messageLabel = new System.Windows.Forms.Label();
			this._userNameTextBox = new System.Windows.Forms.TextBox();
			this._passwordTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// _cancelButton
			// 
			this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			resources.ApplyResources(this._cancelButton, "_cancelButton");
			this._cancelButton.Name = "_cancelButton";
			this._cancelButton.UseVisualStyleBackColor = true;
			// 
			// _okButton
			// 
			resources.ApplyResources(this._okButton, "_okButton");
			this._okButton.Name = "_okButton";
			this._okButton.UseVisualStyleBackColor = true;
			this._okButton.Click += new System.EventHandler(this._okButton_Click);
			// 
			// _messageLabel
			// 
			resources.ApplyResources(this._messageLabel, "_messageLabel");
			this._messageLabel.Name = "_messageLabel";
			// 
			// _userNameTextBox
			// 
			resources.ApplyResources(this._userNameTextBox, "_userNameTextBox");
			this._userNameTextBox.Name = "_userNameTextBox";
			// 
			// _passwordTextBox
			// 
			resources.ApplyResources(this._passwordTextBox, "_passwordTextBox");
			this._passwordTextBox.Name = "_passwordTextBox";
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			// 
			// panel1
			// 
			resources.ApplyResources(this.panel1, "panel1");
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.label2);
			this.panel1.Name = "panel1";
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// UserSelectionForm
			// 
			this.AcceptButton = this._okButton;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this._cancelButton;
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this._passwordTextBox);
			this.Controls.Add(this._userNameTextBox);
			this.Controls.Add(this._messageLabel);
			this.Controls.Add(this._okButton);
			this.Controls.Add(this._cancelButton);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "UserSelectionForm";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private void _okButton_Click(object sender, EventArgs e)
		{
			UserData match = Array.Find<UserData>(_users, delegate(UserData test)
			{
				return String.Compare(test.Name, _userNameTextBox.Text, StringComparison.CurrentCulture) == 0 &&
					String.Compare(test.Password, _passwordTextBox.Text, StringComparison.CurrentCulture) == 0;
			});

			if (match == null)
			{
				_messageLabel.Text = Properties.Resources.UserNotFoundMessage;
			}
			else
			{
				_matchUser = match;
				this.DialogResult = DialogResult.OK;
				Close();
			}
		}
	}
}
