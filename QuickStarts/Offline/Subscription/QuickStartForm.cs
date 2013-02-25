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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuickStart
{
	public partial class QuickStartForm : Form
	{

		public QuickStartForm()
		{
			InitializeComponent();
		}

        private void DisplayScenarioStart(string scenarioDescription)
        {
            this.resultsTextBox.Text = scenarioDescription + Environment.NewLine + Environment.NewLine;
            this.resultsTextBox.Update();
        }

		private void btnStart_Click(object sender, EventArgs e)
		{
            DisplayScenarioStart("QuickStart scenario description...");
		}

        private void QuitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

	}
}