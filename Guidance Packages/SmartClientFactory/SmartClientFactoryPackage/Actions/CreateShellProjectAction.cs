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
using System.ComponentModel.Design;
using System.IO;
using EnvDTE;
using EnvDTE80;
using Microsoft.Practices.Common.Services;
using Microsoft.Practices.RecipeFramework;
using Microsoft.Practices.RecipeFramework.Extensions.CommonHelpers;
using VSLangProj;

namespace Microsoft.Practices.SmartClientFactory.Actions
{
    public class CreateShellProjectAction : ConfigurableAction
    {
        private object _root;
        private string _shellProjectName;
        private string _layoutProjectName;
        private string _libraryProjectName;
        private bool _useExtendedShell;
        private bool _supportWPFViews;
        private string _basicShellTemplate;
        private string _extendedShellTemplate;
        private string _layoutTemplate;
        private string _destinationDir;
        private string _libPath;
        private string _cabWPFAssembly;

        private object _shellProject;
        private object _layoutProject;
        private object _libraryProject;

        [Input(Required = true)]
        public string LayoutProjectName
        {
            get { return _layoutProjectName; }
            set { _layoutProjectName = value; }
        }

        [Input]
        public string DestinationDirectory
        {
            get { return _destinationDir; }
            set { _destinationDir = value; }
        }

        [Input]
        public string LayoutTemplate
        {
            get { return _layoutTemplate; }
            set { _layoutTemplate = value; }
        }

        [Input(Required = true)]
        public string BasicShellTemplate
        {
            get { return _basicShellTemplate; }
            set { _basicShellTemplate = value; }
        }

        [Input]
        public string ExtendedShellTemplate
        {
            get { return _extendedShellTemplate; }
            set { _extendedShellTemplate = value; }
        }


        [Input(Required = true)]
        public bool UseExtendedShell
        {
            get { return _useExtendedShell; }
            set { _useExtendedShell = value; }
        }

        [Input]
        public bool SupportWPFViews
        {
            get { return _supportWPFViews; }
            set { _supportWPFViews = value; }
        }

        [Input(Required = true)]
        public string ShellProjectName
        {
            get { return _shellProjectName; }
            set { _shellProjectName = value; }
        }

        [Input(Required=true)]
        public string LibraryProjectName
        {
            get { return _libraryProjectName; }
            set { _libraryProjectName = value; }
        }

        [Input(Required = true)]
        public object Root
        {
            get { return _root; }
            set { _root = value; }
        }

        [Input]
        public string LibPath
        {
            get { return _libPath; }
            set { _libPath = value; }
        }

        [Input]
        public string CABWPFAssembly
        {
            get { return _cabWPFAssembly; }
            set { _cabWPFAssembly = value; }
        }

        [Output]
        public object ShellProject
        {
            get { return _shellProject; }
            set { _shellProject = value; }
        }

        [Output]
        public object LayoutProject
        {
            get { return _layoutProject; }
            set { _layoutProject = value; }
        }

        [Output]
        public object LibraryProject
        {
            get { return _libraryProject; }
            set { _libraryProject = value; }
        }

        public override void Execute()
        {
            DTE dte = GetService<DTE>();
            string solutionDirectory =
                Path.GetDirectoryName((string)dte.Solution.Properties.Item("Path").Value);
            string targetDirectory = Path.Combine(solutionDirectory, DestinationDirectory);

            if(Root == null)
            {
                if(UseExtendedShell)
                {
                    AddTemplateToSolution(dte.Solution,
                        ResolveTemplateDirectory(ExtendedShellTemplate), targetDirectory,
                        LayoutProjectName);
                    AddTemplateToSolution(dte.Solution, ResolveTemplateDirectory(LayoutTemplate),
                        targetDirectory, LayoutProjectName);
                }
                else
                {
                    AddTemplateToSolution(dte.Solution, ResolveTemplateDirectory(BasicShellTemplate),
                        targetDirectory, ShellProjectName);
                }
            }
            if(Root is Project && ( (Project)Root ).Object is SolutionFolder)
            {
                SolutionFolder slnFolder = ( (Project)Root ).Object as SolutionFolder;
                if(UseExtendedShell)
                {
                    AddTemplateToSolutionFolder(slnFolder,
                        ResolveTemplateDirectory(ExtendedShellTemplate), targetDirectory,
                        ShellProjectName);
                    AddTemplateToSolutionFolder(slnFolder, ResolveTemplateDirectory(LayoutTemplate),
                        targetDirectory, LayoutProjectName);
                }
                else
                {
                    AddTemplateToSolutionFolder(slnFolder,
                        ResolveTemplateDirectory(BasicShellTemplate), targetDirectory,
                        ShellProjectName);
                }
            }

            ShellProject = DteHelperEx.FindProjectByName(dte, ShellProjectName, false);
            LayoutProject = DteHelperEx.FindProjectByName(dte, LayoutProjectName, false);
            LibraryProject = DteHelperEx.FindProjectByName(dte, LibraryProjectName, false);
            if (_supportWPFViews)
            {
                if(UseExtendedShell)
                {
                    AddWPFReferenceToProject((Project)LayoutProject);
                }
                AddWPFReferenceToProject((Project)ShellProject);
                AddWPFReferenceToProject((Project)LibraryProject);
            }
        }

        private void AddTemplateToSolutionFolder(
            SolutionFolder solutionFolder, string template, string destination, string name)
        {
            solutionFolder.AddFromTemplate(template, Path.Combine(destination, name), name);
        }

        private void AddTemplateToSolution(
            Solution solution, string template, string destination, string name)
        {
            solution.AddFromTemplate(template, Path.Combine(destination, name), name, false);
        }

        private string ResolveTemplateDirectory(string template)
        {
            if(!File.Exists(template))
            {
                TypeResolutionService typeResService =
                    (TypeResolutionService)GetService(typeof(ITypeResolutionService));
                if(typeResService != null)
                {
                    template = new FileInfo(Path.Combine(
                        typeResService.BasePath + @"\Templates\", template)).FullName;
                }
            }
            return template;
        }

        private void AddWPFReferenceToProject(Project project)
        {
            VSProject vsProject = (VSProject)project.Object;
            vsProject.References.Add(Path.Combine(_libPath, _cabWPFAssembly));
        }

        public override void Undo()
        {
        }
    }
}