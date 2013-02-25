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
using System.IO;
using Microsoft.Build.Tasks.Deployment.ManifestUtilities;
using ManifestManagerUtility.Properties;
using ClickOnceUtils;

namespace ManifestManagerUtility
{
	public partial class MainForm : Form
	{
		bool _dirty = false;
		ApplicationManifest m_AppManifest = null;
		DeployManifest m_DeployManifest = null;
		BindingList<ApplicationFile> m_Files = new BindingList<ApplicationFile>();
		List<AssemblyReference> m_Prerequisites = new List<AssemblyReference>();

		public MainForm()
		{
			InitializeComponent();

			// Listen for changes to bound list of files
			m_Files.ListChanged += OnListChanged;
			filesBindingSource.DataSource = m_Files;
			EnableToolStripItems(false);
		}

		// Set dirty flag for closing
		private void OnListChanged(object sender, ListChangedEventArgs e)
		{
			_dirty = true;
		}

		/// <summary>
		/// Handle manifest open command
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnFileOpen(object sender, EventArgs e)
		{
			// Prompt for location of deployment manifest 
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "Deployment Manifests (*.application)|*.application|All Files(*.*)|*.*";

			// Restore last manifest location if available
			if (!string.IsNullOrEmpty(Settings.Default.LastManifestPath) &&
			   Directory.Exists(Settings.Default.LastManifestPath))
			{
				ofd.InitialDirectory = Settings.Default.LastManifestPath;
			}

			// Path selected
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				string fileName = ofd.FileName;

				// Save for next time
				Settings.Default.LastManifestPath = Path.GetDirectoryName(fileName);
				Settings.Default.Save();
				bool appManifestAvail = false;

				// Load the details
				try
				{
					ManifestHelper.LoadDeploymentManifestData(fileName, out m_DeployManifest, out m_AppManifest);
					appManifestAvail = true;
				}
				catch (FileNotFoundException fex)
				{
					MessageBox.Show("Unable to locate referenced application manifest\n" + fex.FileName);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
					return;
				}

				// Update UI fields
				appIdTextBox.Text = m_DeployManifest.AssemblyIdentity.Name;
				versionTextBox.Text = m_DeployManifest.AssemblyIdentity.Version;
				depProviderTextBox.Text = m_DeployManifest.DeploymentUrl;

				// Update app manifest details
				if (appManifestAvail)
				{
					m_Prerequisites = ManifestHelper.GetPrerequisites(m_AppManifest);
					m_Files = ManifestHelper.GetFiles(m_AppManifest);
					filesBindingSource.DataSource = m_Files;
                    //If the user used mage at all, it will replace the space in "Application Files" with a %20. 
                    //Check for it and replace it if found. RobinDotNet 4/7/2010
                    appManifestPathTextBox.Text = m_DeployManifest.EntryPoint.TargetPath.Replace("%20", " ");
                    EnableToolStripItems(true);
				}
				else
				{
					EnableToolStripItems(false);
				}
				_dirty = false;
			}
		}

		private void EnableToolStripItems(bool enable)
		{
			fileSaveToolStripItem.Enabled = enable;
			addFilesToolStripItem.Enabled = enable;
			deleteFileToolStripItem.Enabled = enable;
			addFilesMenuItem.Enabled = enable;
			deleteSelectedFileMenuItem.Enabled = enable;
			saveMenuItem.Enabled = enable;
		}

		// Save changes back to manifests
		private void OnFileSave(object sender, EventArgs e)
		{
			// Data binding stuff
			Validate();
			filesBindingSource.EndEdit();
			ManifestHelper.SaveManifestValues(appIdTextBox.Text, versionTextBox.Text, depProviderTextBox.Text, m_DeployManifest, m_AppManifest, m_Files, m_Prerequisites);

			// Prompt with signing dialog
			ManifestSigningDialog dlg = new ManifestSigningDialog(m_DeployManifest, m_AppManifest);
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				_dirty = false;
			}
		}

		private void OnFileExit(object sender, EventArgs e)
		{
			if (Shutdown())
				Close();
		}

		private void OnFormClosing(object sender, FormClosingEventArgs e)
		{
			if (!Shutdown())
				e.Cancel = true;
		}

		// Dirty checking and prompting
		private bool Shutdown()
		{
			if (_dirty)
			{
				DialogResult result = MessageBox.Show("Manifest values have changed. Save?", "Manifest Manager", MessageBoxButtons.YesNoCancel);
				if (result == DialogResult.Cancel)
				{
					return false; // Do nothing
				}
				else if (result == DialogResult.Yes)
				{
					// Save and ready to close
					OnFileSave(null, null);
				}
				return true;
			}
			return true;
		}

		private void OnFileAbout(object sender, EventArgs e)
		{
			AboutForm about = new AboutForm();
			about.Show();
		}

		private void OnFieldsEdited(object sender, EventArgs e)
		{
			_dirty = true;
		}

		private void OnAppFileDelete(object sender, EventArgs e)
		{
			filesBindingSource.RemoveCurrent();
		}

		/// <summary>
		/// Event handler for Add Files button
		/// </summary>
		private void OnAppFileAdd(object sender, EventArgs e)
		{
			// Prompt to select files to move into application
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Multiselect = true;
			ofd.Title = "Add Application Files";
			ofd.Filter = "All Files (*.*)|*.*";

			// Remember last path used to select files
			if (!string.IsNullOrEmpty(Settings.Default.LastFileSelectPath) &&
				(Directory.Exists(Settings.Default.LastFileSelectPath)))
			{
				ofd.InitialDirectory = Settings.Default.LastFileSelectPath;
			}
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				// Save selected path
				Settings.Default.LastFileSelectPath = Path.GetDirectoryName(ofd.FileName);
				Settings.Default.Save();
				// Prompt for destination folder and add files
				string[] fileNames = ofd.FileNames;
				GetDestinationFolderAndAdd(fileNames);
			}

		}

		/// <summary>
		/// Prompts for destination folder and then starts processing additions
		/// </summary>
		/// <param name="fileNames">Collection of file paths to add</param>
		private void GetDestinationFolderAndAdd(string[] fileNames)
		{
			// Now prompt for destination folder
			FolderBrowserDialog folderDlg = new FolderBrowserDialog();
			folderDlg.Description = "Select destination folder for application files";

			// Restore last used target folder if there is one
			if (!string.IsNullOrEmpty(Settings.Default.LastTargetPath) &&
				(Directory.Exists(Settings.Default.LastTargetPath)))
			{
				folderDlg.SelectedPath = Settings.Default.LastTargetPath;
			}
			else if (!string.IsNullOrEmpty(Settings.Default.LastManifestPath) &&
				(Directory.Exists(Settings.Default.LastManifestPath)))
			{
				folderDlg.SelectedPath = Settings.Default.LastManifestPath;
			}

			// Prompt the user
			if (folderDlg.ShowDialog() == DialogResult.OK)
			{
				string targetPath = folderDlg.SelectedPath;

				// Save the select folder for next time
				Settings.Default.LastTargetPath = targetPath;
				Settings.Default.Save();

				// Add the files to the manifest 
				AddFiles(fileNames, targetPath);
			}
		}

		/// <summary>
		/// Checks to see if the files are moving to a new location and whether
		/// they should have the .deploy extension added, then calls the 
		/// appropriate helper method.
		/// </summary>
		/// <param name="fileNames">Collection of files to add</param>
		/// <param name="targetPath">Destination folder for the files</param>
		private void AddFiles(string[] fileNames, string targetPath)
		{
			// Get the source folder
			string filePath = Path.GetDirectoryName(fileNames[0]).ToLower();

			// See if it is same as targetPath
			if (filePath == targetPath.ToLower())
			{
				// Check whether to add .deploy extension
				DialogResult promptResult = MessageBox.Show("Add .deploy extension to those files that do not already have it?", "Add Extension?", MessageBoxButtons.YesNo);

				bool addDeployExtension = (promptResult == DialogResult.Yes);
				ManifestHelper.AddFilesInPlace(fileNames, addDeployExtension, m_Files, m_AppManifest);
			}
			else
			{
				// Copy, add extension if needed, and add to manifest 
				ManifestHelper.AddFilesToTargetFolder(fileNames, targetPath, m_Files, m_AppManifest);
			}
		}


		/// <summary>
		/// Allows user to point to a different app manifest 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnSelectAppManifest(object sender, EventArgs e)
		{
			// Prompt for path
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "Application Manifests (*.manifest)|*.manifest|All Files (*.*)|*.*";

			// Use last path if available
			if (!string.IsNullOrEmpty(Settings.Default.LastManifestPath) &&
				(Directory.Exists(Settings.Default.LastManifestPath)))
			{
				ofd.InitialDirectory = Settings.Default.LastManifestPath;
			}

			// Load file list if user selects manifest 
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				string fileName = ofd.FileName;
				try
				{
					m_AppManifest = ManifestHelper.LoadAppManifest(fileName);
					m_Prerequisites = ManifestHelper.GetPrerequisites(m_AppManifest);
					m_Files = ManifestHelper.GetFiles(m_AppManifest);
					filesBindingSource.DataSource = m_Files;
					ManifestHelper.UpdateDeployManifestAppReference(m_DeployManifest, m_AppManifest);
					appManifestPathTextBox.Text = m_DeployManifest.EntryPoint.TargetPath;
					EnableToolStripItems(true);
				}
				catch
				{
					MessageBox.Show("Invalid manifest");
				}
			}
		}
	}
}