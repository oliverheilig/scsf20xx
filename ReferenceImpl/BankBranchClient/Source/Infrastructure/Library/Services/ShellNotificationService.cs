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
using System.Globalization;
using System.Windows.Forms;
using GlobalBank.Infrastructure.Interface.Services;
using Microsoft.Practices.CompositeUI.Utility;

namespace GlobalBank.Infrastructure.Library.Services
{
	public class ShellNotificationService : IMessageBoxService
	{
		private Control _owner;

		public ShellNotificationService() : this(null)
		{
		}

		public ShellNotificationService(Control owner)
		{
			_owner = owner;
		}

		private bool GetIsRtl(Control control)
		{
			if (control == null)
				return CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft;

			if (control.RightToLeft == RightToLeft.Yes)
			{
				return true;
			}
			else if (control.RightToLeft == RightToLeft.No)
			{
				return false;
			}
			else if (control.RightToLeft == RightToLeft.Inherit)
			{
				if (control.Parent == null)
				{
					throw new InvalidOperationException(string.Format(
					                                    	CultureInfo.CurrentCulture,
					                                    	Properties.Resources.TopLevelControlRightToLeftInherit));
				}
				return GetIsRtl(control.Parent);
			}
			return false;
		}

		private bool IsRtl
		{
			get { return GetIsRtl(_owner); }
		}

		public DialogResult Show(string text)
		{
			return Show(text, null, MessageBoxButtons.OK, MessageBoxIcon.None);
		}

		public DialogResult Show(string text, string caption)
		{
			return Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.None);
		}

		public DialogResult Show(string text, string caption, MessageBoxButtons buttons)
		{
			return Show(text, caption, buttons, MessageBoxIcon.None);
		}

		private delegate DialogResult ShowDelegate(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon
			);

		public DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			if (_owner != null && _owner.InvokeRequired)
			{
				return (DialogResult) _owner.Invoke(new ShowDelegate(Show), text, caption, buttons, icon);
			}
			else
			{
				MessageBoxOptions messageBoxOptions = (MessageBoxOptions) 0;
				if (IsRtl)
				{
					messageBoxOptions |= MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading;
				}
				return MessageBox.Show(_owner,
										   text, caption, buttons, icon,
										   MessageBoxDefaultButton.Button1,
										   messageBoxOptions);				

			}
		}
	}
}