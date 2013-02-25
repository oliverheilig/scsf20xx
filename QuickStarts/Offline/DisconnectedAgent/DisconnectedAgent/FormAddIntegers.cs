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
using Microsoft.Practices.SmartClient.DisconnectedAgent;
using Quickstarts.DisconnectedAgent.IntegerCalculatorAgent;

namespace Quickstarts.DisconnectedAgent
{
    public partial class FormAddIntegers : Form
    {
        IntegerCalculatorServiceDisconnectedAgent calculator;

        public FormAddIntegers()
        {
            InitializeComponent();
            calculator = new IntegerCalculatorServiceDisconnectedAgent(RequestManager.Instance.RequestQueue);
            tagComboBox.SelectedIndex = 0;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            int number1 = 0;
            int number2 = 0;
            int.TryParse(textBoxFirstNumber.Text, out number1);
            int.TryParse(textBoxSecondNumber.Text, out number2);

            OfflineBehavior behavior = IntegerCalculatorServiceDisconnectedAgent.GetAddDefaultBehavior();
            if (!tagComboBox.SelectedItem.ToString().Equals("(none)"))
            {
                behavior.Tag = tagComboBox.SelectedItem.ToString();
            }

            calculator.Add(number1, number2, behavior);
        }
    }
}