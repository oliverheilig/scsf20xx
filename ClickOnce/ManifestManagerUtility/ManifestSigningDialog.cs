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
using Microsoft.Build.Tasks.Deployment.ManifestUtilities;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using ManifestManagerUtility.Properties;
using ClickOnceUtils;

namespace ManifestManagerUtility
{
	public partial class ManifestSigningDialog : Form
	{
		private ApplicationManifest m_AppManifest;
		private DeployManifest m_DeployManifest;

		public ManifestSigningDialog(DeployManifest deployManifest, ApplicationManifest appManifest)
		{
			m_DeployManifest = deployManifest;
			m_AppManifest = appManifest;
			InitializeComponent();
			if (!string.IsNullOrEmpty(Settings.Default.LastSelectedCertificate) &&
				File.Exists(Settings.Default.LastSelectedCertificate))
			{
				m_PathTextBox.Text = Settings.Default.LastSelectedCertificate;
			}
		}

		private void OnBrowse(object sender, EventArgs e)
		{
			// Prompt for cert file
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "Pfx Files (*.pfx)|*.pfx";
		
			// Restore last location if available
			if (!string.IsNullOrEmpty(Settings.Default.LastCertPath) &&
			   Directory.Exists(Settings.Default.LastCertPath))
			{
				ofd.InitialDirectory = Settings.Default.LastCertPath;
			}
			
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				m_PathTextBox.Text = ofd.FileName;
			
				// Save selected location and path for next time
				Settings.Default.LastCertPath = Path.GetDirectoryName(ofd.FileName);
				Settings.Default.LastSelectedCertificate = ofd.FileName;
				Settings.Default.Save();
			}
		}

		private void OnSignAndSave(object sender, EventArgs e)
		{
			// Make sure the entered cert file exists
			if (File.Exists(m_PathTextBox.Text))
			{
				// Update hashes and size info for files
				m_AppManifest.ResolveFiles();
				m_AppManifest.UpdateFileInfo();
				
				// Write app manifest 
				ManifestWriter.WriteManifest(m_AppManifest);
				
				// Sign app manifest 
				ManifestHelper.SignManifest(m_AppManifest, m_PathTextBox.Text, m_PasswordTextBox.Text);
				ManifestHelper.UpdateDeployManifestAppReference(m_DeployManifest, m_AppManifest);

				// Write deploy manifest 
				ManifestWriter.WriteManifest(m_DeployManifest);
				
				// sign deploy manifest 
				ManifestHelper.SignManifest(m_DeployManifest, m_PathTextBox.Text, m_PasswordTextBox.Text);
				DialogResult = DialogResult.OK;
				Close();
			}
			else
			{
				m_ErrorProvider.SetError(m_PathTextBox, "Invalid Path");
			}
		}

		private void OnCancel(object sender, EventArgs e)
		{
			Close();
		}
	}
}