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
using EnvDTE;
using Microsoft.Practices.RecipeFramework;
using System.IO;
using EnvDTE80;
using VSLangProj;
using Microsoft.Practices.Common.Services;
using System.ComponentModel.Design;
using Microsoft.Practices.RecipeFramework.Extensions.CommonHelpers;

namespace Microsoft.Practices.SmartClientFactory.Actions
{
    class UnfoldModuleProjectsAction : ConfigurableAction
    {

        private const string CABProfileNamespaceV1 = "http://schemas.microsoft.com/pag/cab-profile";
        private const string CABProfileNamespaceV2 = "http://schemas.microsoft.com/pag/cab-profile/2.0";

        private string _layoutTemplate;
        private string _interfaceTemplate;
        private string _testProjectTemplate;
        private bool _createInterface;
        private bool _createTestProject;
        private bool _createLayout;
        private Project _shellProject;
        private Project _interfaceProject;
        private Project _moduleProject;
        private string _baseTemplate;
        private Project _moduleInterfaceProject;
        private Project _testProject;

        [Input]
        public string BaseTemplate
        {
            get { return _baseTemplate; }
            set { _baseTemplate = value; }
        }

        [Input]
        [Output]
        public Project ModuleProject
        {
            get { return _moduleProject; }
            set { _moduleProject = value; }
        }

        [Output]
        public Project ModuleInterfaceProject
        {
            get { return _moduleInterfaceProject; }
            set { _moduleInterfaceProject = value; }
        }

        [Output]
        public Project TestProject
        {
            get { return _testProject; }
            set { _testProject = value; }
        }
        
        [Input]
        public Project InterfaceProject
        {
            get { return _interfaceProject; }
            set { _interfaceProject = value; }
        }


        [Input]
        public Project ShellProject
        {
            get { return _shellProject; }
            set { _shellProject = value; }
        }

        [Input]
        public bool CreateLayout
        {
            get { return _createLayout; }
            set { _createLayout = value; }
        }

        [Input]
        public bool CreateInterface
        {
            get { return _createInterface; }
            set { _createInterface = value; }
        }

        [Input]
        public bool CreateTestProject
        {
            get { return _createTestProject; }
            set { _createTestProject = value; }
        }
        
        [Input]
        public string InterfaceTemplate
        {
            get { return _interfaceTemplate; }
            set { _interfaceTemplate = value; }
        }

        [Input]
        public string LayoutTemplate
        {
            get { return _layoutTemplate; }
            set { _layoutTemplate = value; }
        }

        [Input]
        public string TestProjectTemplate
        {
            get { return _testProjectTemplate; }
            set { _testProjectTemplate = value; }
        }

        public override void Execute()
        {
            DTE dte = GetService<DTE>();

            string projectName = ModuleProject.Name;
            object target = ModuleProject.ParentProjectItem == null ? ModuleProject.DTE.Solution : ModuleProject.ParentProjectItem.ContainingProject.Object;
            string targetDirectory = Path.GetDirectoryName(ModuleProject.FileName);

            // NOTE: Visual Studio always creates:
            // - a project file 
            // - a obj folder
            // - a Properties folder (VB only)
            // before running the wizard. We need to remove those
            // because we want to store the project in a subfolder instead.

            // Remove original (handle and file) project
            string obsoleteProjectPath = ModuleProject.FullName;
            dte.Solution.Remove(ModuleProject);

            if (!string.IsNullOrEmpty(obsoleteProjectPath))
            {
                if (File.Exists(obsoleteProjectPath))
                {
                    File.Delete(obsoleteProjectPath);
                }
                string objFolder = Path.Combine(Path.GetDirectoryName(obsoleteProjectPath), "obj");
                if (Directory.Exists(objFolder))
                {
                    // Visual basic project not always can delete de obj folder
                    try
                    {
                        Directory.Delete(objFolder, true);
                    }
                    catch { }
                }
                string propertiesFolder = Path.Combine(Path.GetDirectoryName(obsoleteProjectPath), "Properties");
                if (Directory.Exists(propertiesFolder))
                {
                    try
                    {
                        Directory.Delete(propertiesFolder, true);
                    }
                    catch { }
                }
            }

            if (CreateLayout)
            {
                UnfoldTemplateOnTarget(target, LayoutTemplate, targetDirectory, projectName);
            }
            else
            {
                UnfoldTemplateOnTarget(target, BaseTemplate, targetDirectory, projectName);
            }
            //if (target is SolutionFolder)
            //{
            //    ModuleProject = (Project)DteHelper.FindItemByName(((SolutionFolder)target).Parent.ProjectItems, projectName, true).Object;
            //}
            //else
            //{
                ModuleProject = FindProjectByNameEx(dte, projectName);
            //}

            // Unfold required projects
            if (CreateInterface)
            {
                string interfaceProjectName = String.Format("{0}.Interface", ModuleProject.Name);
                UnfoldTemplateOnTarget(target, InterfaceTemplate, targetDirectory, interfaceProjectName);
                ModuleInterfaceProject = DteHelperEx.FindProjectByName(dte, interfaceProjectName,false);
            }


            // Update projects references
            if (CreateInterface)
            {
                ((VSProject)ModuleInterfaceProject.Object).References.AddProject(InterfaceProject);
                ((VSProject)ModuleProject.Object).References.AddProject(ModuleInterfaceProject);
            }
            else
            {
                ((VSProject)ModuleProject.Object).References.AddProject(InterfaceProject);
            }

            // create test project
            if (CreateTestProject)
            {
                string testProjectName = String.Format("{0}.Tests", ModuleProject.Name);
                UnfoldTemplateOnTarget(target, TestProjectTemplate, targetDirectory, testProjectName);
                TestProject = DteHelperEx.FindProjectByName(dte, testProjectName, false);

                // Update projects references
                ((VSProject)TestProject.Object).References.AddProject(ModuleProject);
                ((VSProject)TestProject.Object).References.AddProject(InterfaceProject);
                if (CreateInterface)
                {
                    ((VSProject)TestProject.Object).References.AddProject(ModuleInterfaceProject);
                }
            }
        }

        // This method fix the problem with GAx that founds a SolutionFolder as a project
        private Project FindProjectByNameEx(DTE dte, string projectName)
        {
            return DteHelperEx.FindProject(dte, new Predicate<Project>(delegate(Project internalProject)
                                        {
                                            return internalProject.Name == projectName
                                                && !(internalProject.Object is SolutionFolder);
                                        }));
        }

        public override void Undo()
        {
        }


        private void UnfoldTemplateOnTarget(object target, string template, string destination, string name)
        {
            Project module;
            if (target is SolutionFolder)
            {
                module=((SolutionFolder)target).AddFromTemplate(ResolveTemplateDirectory(template), Path.Combine(destination, name), name);
            }
            else if (target is Solution)
            {
                module=((Solution)target).AddFromTemplate(ResolveTemplateDirectory(template), Path.Combine(destination, name), name, false);
            }
        }

        private string ResolveTemplateDirectory(string template)
        {
            if (!File.Exists(template))
            {
                TypeResolutionService typeResService = (TypeResolutionService)GetService(typeof(ITypeResolutionService));
                if (typeResService != null)
                {
                    template = new FileInfo(Path.Combine(
                        typeResService.BasePath + @"\Templates\", template)).FullName;
                }
            }
            return template;
        }

    }
}
