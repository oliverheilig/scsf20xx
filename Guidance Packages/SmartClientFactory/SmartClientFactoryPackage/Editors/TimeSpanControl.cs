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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Microsoft.Practices.SmartClientFactory.Editors
{
	internal partial class TimeSpanControl : UserControl
	{
		private IWindowsFormsEditorService formsService;
		private TimeSpan? timeSpan;
		private DialogResult result;

		public TimeSpanControl()
		{
			InitializeComponent();
		}

		public TimeSpanControl(IWindowsFormsEditorService formsService, TimeSpan? timeSpan) 
			: this()
		{
			this.timeSpan = timeSpan;
			this.formsService = formsService;

			if (timeSpan.HasValue)
			{
				days.Value = timeSpan.Value.Days;
				hours.Value = timeSpan.Value.Hours;
				minutes.Value = timeSpan.Value.Minutes;
				seconds.Value = timeSpan.Value.Seconds;
			}

			formsService.DropDownControl(this);
		}

		public TimeSpan? TimeSpan
		{
			get { return timeSpan; }
			set { timeSpan = value; }
		}

		public DialogResult DialogResult
		{
			get { return result; }
			set { result = value; }
		}

		private void OnChanged(object sender, EventArgs e)
		{
			timeSpan = new TimeSpan(
				(int)days.Value,
				(int)hours.Value,
				(int)minutes.Value,
				(int)seconds.Value);
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			result = DialogResult.OK;
			formsService.CloseDropDown();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			result = DialogResult.Cancel;
			formsService.CloseDropDown();
		}
	}
}
