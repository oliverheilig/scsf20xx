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
using Microsoft.Practices.RecipeFramework;
using Microsoft.Practices.RecipeFramework.Library;
using EnvDTE;
using System.IO;
using System.CodeDom.Compiler;
using Microsoft.Practices.ComponentModel;
using System.ComponentModel.Design;
using Microsoft.Practices.RecipeFramework.Extensions.Actions.General;

//TODO SW: Replace this recipe by CreateFolder + AddItemFromStringAction
namespace Microsoft.Practices.SmartClientFactory.Actions
{
    public class EnsureProjectFolderAction : ConfigurableAction
    {
        /// <summary>
        /// Supported configuration attribute for specifying the path to ensure. 
        /// If the path does not exist in the file system, it will be created. 
        /// If it does not exist in the owner project, the project folder 
        /// structure will be created. Argument references can be specified, 
        /// as supported by the expression evaluation service.
        /// </summary>
        public const string PathAttribute = "Path";

        private string path;

        [Input(Required = true)]
        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        private Project ownerProject;

        [Input(Required = true)]
        public Project OwnerProject
        {
            get { return ownerProject; }
            set { ownerProject = value; }
        }

        private ProjectItem folder;

        [Output]
        public ProjectItem TargetFolder
        {
            get { return folder; }
            set { folder = value; }
        }

        public override void Execute()
		{
			DTE vs = GetService<DTE>(true);

			string location;

            using (EvaluateExpressionAction evaluateExpression = new EvaluateExpressionAction())
            {
                this.Site.Container.Add(evaluateExpression);
                evaluateExpression.Expression = path;
                evaluateExpression.Execute();
                location = evaluateExpression.ReturnValue.ToString();
            }

            if (System.IO.Path.HasExtension(location))
            {
				location = System.IO.Path.GetDirectoryName(location);
            }

			// A directory-like path could actually be a parent project item, so 
			// we need to keep iterating until we find a path with no extension.
			while (System.IO.Path.HasExtension(location) && FileExists(ownerProject, location))
			{
				location = System.IO.Path.GetDirectoryName(location);
			}

			folder = GetOrCreateFolder(ownerProject, location);
		}

        private bool FileExists(Project ownerProject, string location)
        {
            string fileName = System.IO.Path.Combine(
                            System.IO.Path.GetDirectoryName(ownerProject.FileName), location);
            return File.Exists(fileName);
        }

        private ProjectItem GetOrCreateFolder(Project project, string virtualPath)
        {
            string[] paths = virtualPath.Split(new char[] { 
				System.IO.Path.DirectorySeparatorChar, 
				System.IO.Path.AltDirectorySeparatorChar },
                StringSplitOptions.RemoveEmptyEntries);

            if (paths.Length == 0)
            {
                ProjectItem parent = project.ParentProjectItem;
                if (parent == null) parent = new ProjectProjectItemAdapter(project);
                return parent;
            }
            else
            {
                return GetOrCreateRecursive(project.ProjectItems, paths, 0);
            }
        }

        private ProjectItem GetOrCreateRecursive(ProjectItems items, string[] paths, int index)
        {
            if (items != null)
            {
                foreach (ProjectItem item in items)
                {
                    if (item.Name == paths[index])
                    {
                        if (index == (paths.Length - 1))
                        {
                            return item;
                        }
                        else
                        {
                            return GetOrCreateRecursive(item.ProjectItems, paths, ++index);
                        }
                    }
                }
            }

            ProjectItem parent = items.Parent as ProjectItem;
            if (parent == null)
            {
                // Root folder is treated specially.
                Project project = (Project)items.Parent;
                ProjectItem folder = project.ProjectItems.AddFolder(paths[index], Constants.vsProjectItemKindPhysicalFolder);
                return CreateFoldersRecursive(folder, paths, ++index);
            }
            else
            {
                return CreateFoldersRecursive(items.Parent as ProjectItem, paths, index);
            }
        }

        private ProjectItem CreateFoldersRecursive(ProjectItem parent, string[] paths, int index)
        {
            if (index >= paths.Length) return parent;

            ProjectItem folder = parent.ProjectItems.AddFolder(paths[index], Constants.vsProjectItemKindPhysicalFolder);
            return CreateFoldersRecursive(folder, paths, ++index);
        }

        public override void Undo()
        {
        }
    }
}
