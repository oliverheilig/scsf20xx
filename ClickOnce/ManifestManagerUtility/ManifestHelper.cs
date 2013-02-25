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
using System.Text;
using System.IO;
using Microsoft.Build.Tasks.Deployment.ManifestUtilities;
using System.Security.Cryptography.X509Certificates;
using ManifestManagerUtility;
using System.ComponentModel;

namespace ClickOnceUtils
{
	public static class ManifestHelper
	{
		/// <summary>
		/// Loads the deployment manifest and referenced app manifest data into the manifest variables passed in
		/// </summary>
		/// <param name="fileName">Deployment manifest to load</param>
		/// <param name="deployManifest">Deployment manifest to populate</param>
		/// <param name="appManifest">Application manifest to populate</param>
		public static void LoadDeploymentManifestData(string fileName, out DeployManifest deployManifest, out ApplicationManifest appManifest)
		{
			Manifest manifest = ManifestReader.ReadManifest(fileName, false);
			deployManifest = manifest as DeployManifest;
            //This is the part that doesn't work if the deployment manifest is for a project targeting .NET 4.0.
            //If you put a breakpoint here and check the contents of the deployManifest, you will find that 
            //  the CompatibleFrameworks collection has a count of 0, but that section does exist in the manifest.
            //  This has been filed as a Connect bug. When they fix it, you can run this utility for a .NET 4.0 application.
            //  Until then, it will only work for applications targeting .NET 2.0, .NET 3.0, and .NET 3.5.
			if (deployManifest == null)
			{
				throw new ArgumentException("Not a valid deployment manifest");
			}

            //If the user used mage at all, it will replace the space in "Application Files" with a %20. 
            //Check for it and replace it if found. RobinDotNet 4/7/2010
            string assemManifestPath = deployManifest.EntryPoint.TargetPath.Replace("%20", " ");
			assemManifestPath = Path.Combine(Path.GetDirectoryName(deployManifest.SourcePath), assemManifestPath);
			appManifest = LoadAppManifest(assemManifestPath);
		}

		/// <summary>
		/// Loads specified app manifest and populates the manifest variable
		/// </summary>
		/// <param name="assemManifestPath">Path to the app manifest to load</param>
		/// <returns>Application manifest reference</returns>
		public static ApplicationManifest LoadAppManifest(string assemManifestPath)
		{
			Manifest manifest = ManifestReader.ReadManifest(assemManifestPath, false);
			ApplicationManifest appManifest = manifest as ApplicationManifest;
			if (appManifest == null)
			{
				throw new FileNotFoundException("Unable to open referenced application manifest", assemManifestPath);
			}

			return appManifest;
		}

		/// <summary>
		/// Populates a list of prerequisites from the app manifest
		/// </summary>
		/// <param name="appManifest">app manifest to inspect</param>
		/// <returns>List of assembly references that contains only prerequisites.</returns>
		public static List<AssemblyReference> GetPrerequisites(ApplicationManifest appManifest)
		{
			List<AssemblyReference> prereqs = new List<AssemblyReference>();
			foreach (AssemblyReference assemRef in appManifest.AssemblyReferences)
			{
				if (assemRef.IsPrerequisite)
					prereqs.Add(assemRef);
			}

			return prereqs;
		}

		/// <summary>
		/// Returns a collection of application files from the assembly and file references in the provided app manifest
		/// </summary>
		/// <param name="appManifest">App manifest to inspect</param>
		/// <returns>List of ApplicationFile instances with the info from the manifest file list</returns>
		public static BindingList<ApplicationFile> GetFiles(ApplicationManifest appManifest)
		{
			BindingList<ApplicationFile> files = new BindingList<ApplicationFile>();

			// Populate collection with assembly references
			foreach (AssemblyReference assemRef in appManifest.AssemblyReferences)
			{
				if (assemRef.IsPrerequisite)
					continue;
				ApplicationFile appFile = new ApplicationFile();
				appFile.FileName = Path.GetFileName(assemRef.TargetPath);
				appFile.RelativePath = Path.GetDirectoryName(assemRef.TargetPath);
				appFile.DataFile = false;
				if (appManifest.EntryPoint == assemRef)
				{
					appFile.EntryPoint = true;
				}
				files.Add(appFile);
			}

			// Populate collection with file references
			foreach (FileReference fileRef in appManifest.FileReferences)
			{
				ApplicationFile appFile = new ApplicationFile();
				appFile.FileName = Path.GetFileName(fileRef.TargetPath);
				appFile.RelativePath = Path.GetDirectoryName(fileRef.TargetPath);
				appFile.DataFile = fileRef.IsDataFile;
				files.Add(appFile);
			}
			return files;
		}

		/// <summary>
		/// Saves manifest values into the respective manifest
		/// </summary>
		/// <param name="name">Application deployment name</param>
		/// <param name="version">Version number</param>
		/// <param name="deploymentProvider">Deployment provider URL</param>
		/// <param name="deployManifest">Deployment manifest reference</param>
		/// <param name="appManifest">Application manifest reference</param>
		/// <param name="appFiles">List of files to populate app manifest</param>
		/// <param name="preReqs">List of prerequisites to add</param>
		public static void SaveManifestValues(string name, string version, string deploymentProvider, DeployManifest deployManifest, ApplicationManifest appManifest, IList<ApplicationFile> appFiles, IList<AssemblyReference> preReqs)
		{
			// Populate discrete values
			deployManifest.AssemblyIdentity.Name = name;
			deployManifest.AssemblyIdentity.Version = version;
			deployManifest.DeploymentUrl = deploymentProvider;
			appManifest.AssemblyIdentity.Version = version;

			// Refresh app manifest file lists
			appManifest.AssemblyReferences.Clear();
			appManifest.FileReferences.Clear();

			// Populate prerequisites
			foreach (AssemblyReference assemRef in preReqs)
			{
				appManifest.AssemblyReferences.Add(assemRef);
			}

			// Populate assembly and file references
			foreach (ApplicationFile appFile in appFiles)
			{
				string appFilePath = Path.Combine(appFile.RelativePath, appFile.FileName);
				string appManifestFolder = Path.GetDirectoryName(appManifest.SourcePath);
				string appFileFullPath = Path.Combine(appManifestFolder, appFilePath) + ".deploy";
				AssemblyIdentity assemId = AssemblyIdentity.FromFile(appFileFullPath);
				if (assemId != null) // valid assembly
				{
					AssemblyReference assemRef = appManifest.AssemblyReferences.Add(appFileFullPath);
					assemRef.TargetPath = appFilePath;
					if (appFile.EntryPoint)
						appManifest.EntryPoint = assemRef;
				}
				else
				{
					FileReference fref = appManifest.FileReferences.Add(appFileFullPath);
					fref.TargetPath = appFilePath;
					if (appFile.DataFile)
						fref.IsDataFile = true;
				}
			}
		}

		/// <summary>
		/// Sign the manifest with provided publisher certificate
		/// </summary>
		/// <param name="manifest">manifest to sign</param>
		/// <param name="certFilePath">Path to cert file</param>
		/// <param name="password">Password for cert file</param>
		public static void SignManifest(Manifest manifest, string certFilePath, string password)
		{
			// Make sure the entered cert file exists
			if (File.Exists(certFilePath))
			{
				// Construct cert object for cert
				X509Certificate2 cert;
				if (string.IsNullOrEmpty(password))
				{
					cert = new X509Certificate2(certFilePath);
				}
				else
				{
					cert = new X509Certificate2(certFilePath, password);
				}

				SecurityUtilities.SignFile(cert, null, manifest.SourcePath);
			}
			else
			{
				throw new ArgumentException("Invalid certificate file path");
			}

		}

		/// <summary>
		/// Updates the application reference in a deployment manifest to point to a specific application manifest 
		/// </summary>
		/// <param name="depManifest">The deployment manifest to update</param>
		/// <param name="appManifest">The app manifest to refer to </param>
		public static void UpdateDeployManifestAppReference(DeployManifest depManifest, ApplicationManifest appManifest)
		{
			string deployManifestPath = Path.GetDirectoryName(depManifest.SourcePath);
			string relPath = ManifestHelper.GetRelativeFolderPath(appManifest.SourcePath, deployManifestPath);
			depManifest.AssemblyReferences.Clear();
			AssemblyReference assemRef = depManifest.AssemblyReferences.Add(appManifest.SourcePath);
			assemRef.TargetPath = Path.Combine(relPath, assemRef.TargetPath);
			depManifest.EntryPoint = assemRef;
			depManifest.EntryPoint.ReferenceType = AssemblyReferenceType.ClickOnceManifest;
			depManifest.ResolveFiles();
			depManifest.UpdateFileInfo();
		}

		/// <summary>
		/// Adds files to the manifest from their current location, adding
		/// .deploy file extension if needed
		/// </summary>
		/// <param name="fileNames">Collection of files to add</param>
		/// <param name="addDeployExtension">True if deploy extension should be added if needed</param>
		/// <param name="appfiles">The application files collection to add to</param>
		/// <param name="appManifest">The app manifest the file is associated with</param>
		public static void AddFilesInPlace(string[] fileNames, bool addDeployExtension, IList<ApplicationFile> appfiles, ApplicationManifest appManifest)
		{
			for (int i = 0; i < fileNames.Length; i++)
			{
				// Add .deploy extension if appropriate
				if (addDeployExtension &&
				   Path.GetExtension(fileNames[i]).ToLower() != ".deploy")
				{
					string newName = fileNames[i] + ".deploy";
					File.Move(fileNames[i], newName);
					fileNames[i] = newName;
				}

				// Add to manifest 
				AddFile(fileNames[i], appfiles, appManifest);
			}
		}

		/// <summary>
		/// Copies files to be added to a target folder, adds .deploy extension
		/// if needed, and adds them to the manifest 
		/// </summary>
		/// <param name="fileNames">Collection of files to add</param>
		/// <param name="targetFolder">Destination folder</param>
		/// <param name="appfiles">The application files collection to add to</param>
		/// <param name="appManifest">The app manifest the file is associated with</param>
		public static void AddFilesToTargetFolder(string[] fileNames, string targetFolder, IList<ApplicationFile> appfiles, ApplicationManifest appManifest)
		{
			foreach (string fileName in fileNames)
			{
				string newName = Path.Combine(targetFolder, Path.GetFileName(fileName));
				if (Path.GetExtension(newName).ToLower() != ".deploy")
				{
					newName += ".deploy";
				}

				File.Copy(fileName, newName, true);
				AddFile(newName, appfiles, appManifest);
			}
		}

		/// <summary>
		/// Adds an individual file to the manifest 
		/// </summary>
		/// <param name="fileName">Full file path of file to add</param>
		/// <param name="appfiles">The application files collection to add to</param>
		/// <param name="appManifest">The app manifest the file is associated with</param>
		public static void AddFile(string fileName, IList<ApplicationFile> appfiles, ApplicationManifest appManifest)
		{
			BaseReference fileRef;

			// Check to see if it is an assembly
			AssemblyIdentity assemId = AssemblyIdentity.FromFile(fileName);
			if (assemId != null) // valid assembly
			{
				// Add as assembly
				fileRef = appManifest.AssemblyReferences.Add(fileName);
			}
			else
			{
				// Add as a file 
				fileRef = appManifest.FileReferences.Add(fileName);
			}

			// Add it to bound list of file information
			ApplicationFile appFile = new ApplicationFile();
			appFile.FileName = Path.GetFileName(fileRef.TargetPath);
			string appManifestFolder = Path.GetDirectoryName(appManifest.SourcePath);
			appFile.RelativePath = ManifestHelper.GetRelativeFolderPath(fileName, appManifestFolder);
			appfiles.Add(appFile);
		}


		/// <summary>
		/// Calculates a relative folder path between a file and a reference folder
		/// </summary>
		/// <param name="fileName">The full path to the file</param>
		/// <param name="folder">The folder to calculate the relative path from</param>
		/// <returns>The relative path</returns>
		public static string GetRelativeFolderPath(string fileName, string folder)
		{
			folder = folder.Trim();

			// Special case, drive letter only
			if (Path.GetPathRoot(folder) == folder) // Drive letter, root folder
			{
				if (!folder.EndsWith("\\"))
				{
					folder += "\\"; // Add slash for drive letter comparison
				}
			}

			// Different drive, must specify full path
			if (Path.GetPathRoot(fileName).ToLower() != Path.GetPathRoot(folder).ToLower())
			{
				return Path.GetDirectoryName(fileName);
			}

			// Strip trailing slash
			if (folder.EndsWith("\\"))
			{
				folder = folder.Remove(folder.Length - 1);
			}

			// Chop the paths into parts
			string[] fileParts = Path.GetDirectoryName(fileName).Split('\\');
			string[] folderParts = folder.Split('\\');

			// Same folder, empty relative folder path
			if (Path.GetDirectoryName(fileName).ToLower() == folder.ToLower())
			{
				return string.Empty;
			}

			// Find where the paths no longer match
			int mismatchCount = folderParts.Length; // assume the full folder path matches
			for (int i = 1; i < folderParts.Length; i++)
			{
				if ((i >= fileParts.Length - 1) ||
					(fileParts[i].ToLower() != folderParts[i].ToLower()))
				{
					mismatchCount = i;
					break;
				}
			}

			// Build relative path
			string relPath = string.Empty;
			for (int i = mismatchCount; i < folderParts.Length; i++)
			{
				relPath += "..\\";
			}
			for (int i = mismatchCount; i < fileParts.Length; i++)
			{
				relPath += fileParts[i] + "\\";
			}

			if (relPath.EndsWith("\\"))
			{
				relPath = relPath.Remove(relPath.Length - 1);
			}

			return relPath;
		}
	}
}
