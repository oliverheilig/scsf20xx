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
using System.Collections.Generic;
using System.Text;
using Maui.Core.WinControls;
using Maui.Core;
using BankBranchWorkBench.MAUI.Common;
using Maui.Core.Utilities;

namespace BankBranchWorkBench.MAUI
{
	public class AuthenAdapter
	{
		#region Screen Controls
		private Window authenWindow;
		#endregion

		#region Lifetime
		public AuthenAdapter()
		{
			//BankWorkBench.Kill();
			authenWindow = MAUIUtilities.GetWindow(BankWorkBench.Application, ResourceNames.Authen.WindowTitle);
		}

		public void CheckPresenceOfControls()
		{
			//Label
            
			MAUIUtilities.GetControl<StaticControl>(authenWindow,
						ResourceNames.Authen.UserNameLabel, StringMatchSyntax.ExactMatch,
						"*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(authenWindow,
                        ResourceNames.Authen.PasswordLabel, StringMatchSyntax.ExactMatch,
                        "*", StringMatchSyntax.WildCard);
            ////Combo
            //MAUIUtilities.GetControl<ComboBox>(authenWindow,
            //            ResourceNames.Authen.RoleIdComboBoxName);

			//Button
			MAUIUtilities.GetControl<Button>(authenWindow,
						ResourceNames.Authen.OkButtonName);
			MAUIUtilities.GetControl<Button>(authenWindow,
						ResourceNames.Authen.CancelButtonName);
            //TextBox
            MAUIUtilities.GetControl<TextBox>(authenWindow,
                     ResourceNames.Authen.UserNameTextBox);
            MAUIUtilities.GetControl<TextBox>(authenWindow,
                     ResourceNames.Authen.PasswordTextBox);

		}

		public void Close()
		{
			authenWindow.Extended.CloseWindow();
            BankWorkBench.Close();
			authenWindow = null;
		}

		public bool Closed
		{
			get
			{
				return authenWindow == null;
			}
		}

		#endregion

		#region Screen Value Properties

		public string User
		{
			get
			{
				return MAUIUtilities.GetControl<ComboBox>(authenWindow,
							ResourceNames.Authen.RoleIdComboBoxName).Text;
			}

			set
			{
				MAUIUtilities.GetControl<ComboBox>(authenWindow,
							ResourceNames.Authen.RoleIdComboBoxName).SelectByText(value, true);
			}
		}

		private Button OkButton
		{
			get
			{
				return MAUIUtilities.GetControl<Button>(authenWindow,
									ResourceNames.Authen.OkButtonName);
			}
		}

		private Button CancelButton
		{
			get
			{
				return MAUIUtilities.GetControl<Button>(authenWindow,
									 ResourceNames.Authen.CancelButtonName);
			}
		}

		public bool IsModal
		{
			get
			{
				return MAUIUtilities.IsModal(BankWorkBench.Application, authenWindow);
			}
		}

		public bool IsResizable
		{
			get
			{
				return authenWindow.Extended.IsResizable;
			}
		}

        public bool IsOkButtonEnabled
        {
            get
            {
                return OkButton.IsEnabled;
            }
        }

        public bool IsCancelButtonEnabled
        {
            get
            {
                return CancelButton.IsEnabled;
            }
        }

        public string UserName
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(authenWindow,
                    ResourceNames.Authen.UserNameTextBox).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(authenWindow,
                    ResourceNames.Authen.UserNameTextBox).Text = value;
            }
        }

        public string Password
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(authenWindow,
                    ResourceNames.Authen.PasswordTextBox).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(authenWindow,
                    ResourceNames.Authen.PasswordTextBox).Text = value;
            }
        }

        public void SetUserNameAndPassword(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
		#endregion

		#region Screen Operations

		public ShellAdapter OkClick()
		{           
            OkButton.Click();
            Sleeper.Delay(500);
			ResetAuthenWindow();
			return new ShellAdapter();
		}       

		public void Cancel()
		{
			CancelButton.Click();
            Sleeper.Delay(500);
            ResetAuthenWindow();
		}

        public void EscPressed()
        {
            authenWindow.SendKeys("{ESC}");
            Sleeper.Delay(500);
            ResetAuthenWindow();
        }

		private void ResetAuthenWindow()
		{
			authenWindow = MAUIUtilities.GetWindow(BankWorkBench.Application,
					ResourceNames.Authen.WindowTitle, false);
		}
		#endregion
	}
}
