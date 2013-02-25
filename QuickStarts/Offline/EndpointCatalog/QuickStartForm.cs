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
using System.Configuration;
using Microsoft.Practices.SmartClient.EndpointCatalog.Configuration;
using Microsoft.Practices.SmartClient.EndpointCatalog;
using System.Net;

namespace QuickStarts.EndpointCatalog
{
	public partial class QuickStartForm : Form
	{
        private const string EndpointsConfigurationSectionName = "Endpoints";
        IEndpointCatalog endpointCatalog;

		public QuickStartForm()
		{
			InitializeComponent();
		}

        private void QuickStartForm_Load(object sender, EventArgs e)
        {
            endpointCatalog = new EndpointCatalogFactory(EndpointsConfigurationSectionName).CreateCatalog();
            networkDropDown.SelectedIndex = 0;
            LoadEndpoints();
        }

        private void LoadEndpoints()
        {
            EndpointSection endpointSection = ConfigurationManager.GetSection(EndpointsConfigurationSectionName) as EndpointSection;
            foreach (EndpointItemElement endpointItemElement in endpointSection.EndpointItemCollection)
            {
                ListViewItem item = new ListViewItem(endpointItemElement.Name, "endpoint");
                item.ToolTipText = Properties.Resources.ResourceManager.GetString(endpointItemElement.Name);
                item.Tag = endpointItemElement.Name;
                endPointsListView.Items.Add(item);
            }
            endpointsCountLabel.Text = String.Format(Properties.Resources.EndpointsFound, endpointCatalog.Count);
        }

        private void ShowConfiguration()
        {
            if (endPointsListView.SelectedItems.Count == 0)
                return;
            
            string endpointName = endPointsListView.SelectedItems[0].Tag.ToString();
            string networkName = networkDropDown.SelectedItem.ToString() == "Not Specified" ? null : networkDropDown.SelectedItem.ToString();
            configurationGroupBox.Text = String.Format(Properties.Resources.ConfigurationForEndpoint, endpointName);

            ClearConfigurationFields();
            if (endpointCatalog.AddressExistsForEndpoint(endpointName, networkName))
            {
                addressTextBox.Text = endpointCatalog.GetAddressForEndpoint(endpointName, networkName);
                NetworkCredential credential = endpointCatalog.GetCredentialForEndpoint(endpointName, networkName);
                userTextBox.Text = credential.UserName;
                passwordTextBox.Text = credential.Password;
                domainTextBox.Text = credential.Domain;
            }
            else
            {
                addressTextBox.Text = Properties.Resources.NoConfigurationFoundForSpecifiedNetwork;
            }
        }

        private void ClearConfigurationFields()
        {
            addressTextBox.Clear();
            userTextBox.Clear();
            passwordTextBox.Clear();
            domainTextBox.Clear();
        }

        private void networkDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowConfiguration();
        }

        private void endPointsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowConfiguration();
        }
	}
}