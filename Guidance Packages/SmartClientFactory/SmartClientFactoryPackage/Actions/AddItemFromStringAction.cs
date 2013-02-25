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
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.Practices.ComponentModel;
using Microsoft.Practices.RecipeFramework;
using Microsoft.Practices.RecipeFramework.Library;
using EnvDTE;
using Microsoft.Practices.RecipeFramework.Extensions.Actions.General;

//TODO SW: Replace this recipe by CreateFolder + AddItemFromStringAction
namespace Microsoft.Practices.SmartClientFactory.Actions
{
    /// <summary>
    /// The action creates a project item from a string passed to the action
    /// in the Content input property. The other two input properties of the
    /// action are (a) targetFileName - provides the name of the item file 
    /// and (b) ProjectItem - identifies the project item where the item is created. 
    /// </summary>
    [ServiceDependency(typeof(DTE))]
    public class AddItemFromStringAction : ConfigurableAction
    {
        private string content;
   
		/// <summary>
        /// The string with the content of the item. In most cases it is a class
        /// generated using the T4 engine.
        /// </summary>
        [Input(Required=true)]
        public string Content
        {
            get { return content; }
            set { content = value; }
        }

		/// <summary>
		/// Supported configuration attribute for specifying the path to ensure. 
		/// If the path does not exist in the file system, it will be created. 
		/// If it does not exist in the owner project, the project folder 
		/// structure will be created. Argument references can be specified, 
		/// as supported by the expression evaluation service.
		/// </summary>
		public const string PathAttribute = "Path";

		private string path;

		[Input(Required=true)]
		public string Path
		{
			get { return path; }
			set { path = value; }
		}

		private Project ownerProject;

		[Input(Required=false)]
		public Project OwnerProject
		{
			get { return ownerProject; }
			set { ownerProject = value; }
		}

		private ProjectItem ownerItem;

		[Input(Required = false)]
		public ProjectItem OwnerItem
		{
			get { return ownerItem; }
			set { ownerItem = value; }
		}

		private bool open = true;
        
		/// <summary>
        /// Wether the newly created item should be shown in a window.
        /// </summary>
        [Input]
        public bool Open
        {
            get { return open; }
            set { open = value; }
        }

		private bool overwrite;

		[Input(Required=true)]
		public bool Overwrite
		{
			get { return overwrite; }
			set { overwrite = value; }
		}

        private ProjectItem addedItem;

		/// <summary>
        /// A property that can be used to pass the creted item to
        /// a following action.
        /// </summary>
        [Output]
        public ProjectItem AddedItem
        {
            get { return addedItem; }
            set { addedItem = value; }
        }

        /// <summary>
        /// The method that creates a new item from the input string.
        /// </summary>
        public override void Execute()
        {
			if (ownerItem == null && ownerProject == null)
			{
				throw new ArgumentNullException(Properties.Resources.OwnerProjectOrItemRequired);
			}

			if (ownerItem != null)
			{
				ownerProject = ownerItem.ContainingProject;
				// If we got a parent item, build the virtual path to it 
				// for the real full path we will work againt.
				string itemPath = String.Empty;
				DteHelper.BuildPathFromCollection(ownerProject.ProjectItems, ownerItem, ref itemPath);
				path = System.IO.Path.Combine(itemPath, path);
			}

			DTE vs = GetService<DTE>();
			ProjectItem containingFolder;
			string targetPath;

			using (EnsureProjectFolderAction ensureFolder = new EnsureProjectFolderAction())
			{
				this.Site.Container.Add(ensureFolder);
				ensureFolder.Path = path;
				ensureFolder.OwnerProject = ownerProject;
				ensureFolder.Execute();
				containingFolder = ensureFolder.TargetFolder;
			}

			if (containingFolder.Object is Project) containingFolder = new ProjectProjectItemAdapter((Project)containingFolder.Object);

			using (EvaluateExpressionAction evaluateExpression = new EvaluateExpressionAction())
			{
				this.Site.Container.Add(evaluateExpression);
				evaluateExpression.Expression = path;
				evaluateExpression.Execute();
				targetPath = evaluateExpression.ReturnValue.ToString();
			}

			string targetFileName = System.IO.Path.GetFileName(targetPath);

			// This may be different than the containingFolder variable, 
			// as the EnsureProjectFolder only ensures folders, not 
			// hierarchy of dependent items. So we need to look for 
			// the actual parent which may be a project item, not a folder.
			ProjectItem parent = DteHelper.FindItemByPath(
				vs.Solution, 
				System.IO.Path.Combine(
					DteHelper.BuildPath(ownerProject), 
					/* This call just strips the last segment in the path. "Directory"  
					 * can still be a file name, as in the case of dependent items */
					System.IO.Path.GetDirectoryName(targetPath)
				)
			);

			if (parent == null) parent = containingFolder;

			Debug.Assert(parent != null, "Didn't find parent with path: " + targetPath);

			addedItem = DteHelper.FindItemByName(parent.ProjectItems, targetFileName, false);
			if (addedItem != null)
			{
				if (overwrite)
				{
					OverwriteFile(vs, addedItem.get_FileNames(1), content);
                    addedItem.Delete();
                }
			}

            addedItem = DteHelper.FindItemByName(parent.ProjectItems, targetFileName, false);
            if (addedItem == null)
            {
				// At the FS level, dependent files are always inside the folder we determined 
				// above using the EnsureProjectFolderAction.
				string folderPath = System.IO.Path.Combine(
					System.IO.Path.GetDirectoryName(ownerProject.FileName),
					DteHelper.BuildPath(containingFolder));
				string fullPath = System.IO.Path.Combine(folderPath, targetFileName);
				if (File.Exists(fullPath))
				{
					OverwriteFile(vs, fullPath, content);
					addedItem = parent.ProjectItems.AddFromFile(fullPath);
				}
				else
				{
					string tempfile = System.IO.Path.GetTempFileName();
					try
					{
						File.WriteAllText(tempfile, content);
						addedItem = parent.ProjectItems.AddFromTemplate(tempfile, targetFileName);
					}
					finally
					{
						File.Delete(tempfile);
					} 
				}
			}

            if (open)
            {
                Window wnd = addedItem.Open(Constants.vsViewKindPrimary);
                wnd.Visible = true;
                wnd.Activate();
            }
        }

		private void OverwriteFile(DTE vs, string fullPath, string content)
		{
			VsHelper.EnsureWriteable(vs, fullPath);
		}

        /// <summary>
        /// Undoes the creation of the item, then deletes the item
        /// </summary>
        public override void Undo()
        {
            if (addedItem != null)
            {
                addedItem.Delete();
            }
        }
	}
}
