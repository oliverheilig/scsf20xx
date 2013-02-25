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
using System.Windows.Forms;
using System.Globalization;
using Microsoft.Practices.RecipeFramework.Extensions.DteWrapper;
using System.IO;

namespace Microsoft.Practices.SmartClientFactory.CustomWizardPages
{
    public sealed class SolutionPreviewHelper
    {
        private string projectIcon = "CSharpProjectIcon";
        private string extension = "cs";
        private string xamlExtension = "xaml";
        private string itemIcon = "CSharpItemIcon";
        private string solutionFolderIcon = "SolutionFolderIcon";
        private string folderIcon = "FolderIcon";
        private string winFormsUserControlIcon = "UserControlIcon";
        private string wpfUserControlIcon = "WPFUserControlIcon";
        private string moduleSuffix = "";
        private TreeView assembliesTreeView;
        
        #region Create Solution Preview

        public void GetSolutionPreview(bool withShellLayout)
        {
            bool interfaceNodeExpanded=false;
            bool libraryNodeExpanded=false;

            TreeNode[] nodes;

            nodes = assembliesTreeView.Nodes.Find("Infrastructure.Interface", true);
            if (nodes.Length == 1)
            {
                interfaceNodeExpanded = nodes[0].IsExpanded;
            }
            nodes = assembliesTreeView.Nodes.Find("Infrastructure.Library", true);
            if (nodes.Length == 1)
            {
                libraryNodeExpanded = nodes[0].IsExpanded;
            }


            assembliesTreeView.BeginUpdate();
            assembliesTreeView.Nodes.Clear();

            TreeNode rootNode;

            rootNode = assembliesTreeView.Nodes.Add("Source");
            rootNode.ImageKey = solutionFolderIcon;
            rootNode.SelectedImageKey = solutionFolderIcon;

            TreeNode solutionInfrNode = GetNode(rootNode, "Infrastructure", solutionFolderIcon, solutionFolderIcon);

            TreeNode interfaceProjectNode = solutionInfrNode.Nodes.Add("Infrastructure.Interface","Infrastructure.Interface");
            interfaceProjectNode.ImageKey = projectIcon;
            interfaceProjectNode.SelectedImageKey = projectIcon;

            TreeNode businessNode = interfaceProjectNode.Nodes.Add("BusinessEntities");
            businessNode.ImageKey = folderIcon;
            businessNode.SelectedImageKey = folderIcon;

            TreeNode commandsNode = interfaceProjectNode.Nodes.Add("Commands");
            commandsNode.ImageKey = folderIcon;
            commandsNode.SelectedImageKey = folderIcon;

            TreeNode dotCommandsNode = commandsNode.Nodes.Add("...");
            dotCommandsNode.ImageKey = itemIcon;
            dotCommandsNode.SelectedImageKey = itemIcon;

            TreeNode constantsNode = interfaceProjectNode.Nodes.Add("Constants");
            constantsNode.ImageKey = folderIcon;
            constantsNode.SelectedImageKey = folderIcon;

            TreeNode dotConstantsNode = constantsNode.Nodes.Add("...");
            dotConstantsNode.ImageKey = itemIcon;
            dotConstantsNode.SelectedImageKey = itemIcon;

            TreeNode servicesNode = interfaceProjectNode.Nodes.Add("Services");
            servicesNode.ImageKey = folderIcon;
            servicesNode.SelectedImageKey = folderIcon;

            TreeNode dotInterfaceNode1 = interfaceProjectNode.Nodes.Add("...");
            dotInterfaceNode1.ImageKey = itemIcon;
            dotInterfaceNode1.SelectedImageKey = itemIcon;

            TreeNode presenterInterfaceNode = interfaceProjectNode.Nodes.Add(String.Format("Presenter.{0}", extension));
            presenterInterfaceNode.ImageKey = itemIcon;
            presenterInterfaceNode.SelectedImageKey = itemIcon;

            TreeNode dotInterfaceNode2 = interfaceProjectNode.Nodes.Add("...");
            dotInterfaceNode2.ImageKey = itemIcon;
            dotInterfaceNode2.SelectedImageKey = itemIcon;

            if (withShellLayout)
            {
                TreeNode moduleInterfaceProjectNode = solutionInfrNode.Nodes.Add("Infrastructure.Layout");
                moduleInterfaceProjectNode.ImageKey = projectIcon;
                moduleInterfaceProjectNode.SelectedImageKey = projectIcon;

                TreeNode shellLayoutNode = moduleInterfaceProjectNode.Nodes.Add(String.Format("ShellLayoutView.{0}", extension));
                shellLayoutNode.ImageKey = winFormsUserControlIcon;
                shellLayoutNode.SelectedImageKey = winFormsUserControlIcon;

                TreeNode shellPresenterNode = moduleInterfaceProjectNode.Nodes.Add(String.Format("ShellLayoutViewPresenter.{0}", extension));
                shellPresenterNode.ImageKey = itemIcon;
                shellPresenterNode.SelectedImageKey = itemIcon;

                TreeNode shellModuleNode = moduleInterfaceProjectNode.Nodes.Add(String.Format("Module{0}.{1}", moduleSuffix, extension));
                shellModuleNode.ImageKey = itemIcon;
                shellModuleNode.SelectedImageKey = itemIcon;

                moduleInterfaceProjectNode.Expand();
            }

            // Library Project

            TreeNode libraryProjectNode = solutionInfrNode.Nodes.Add("Infrastructure.Library","Infrastructure.Library");
            libraryProjectNode.ImageKey = projectIcon;
            libraryProjectNode.SelectedImageKey = projectIcon;

            TreeNode builderNode = libraryProjectNode.Nodes.Add("BuilderStrategies");
            builderNode.ImageKey = folderIcon;
            builderNode.SelectedImageKey = folderIcon;

            TreeNode dotBuilderNode = builderNode.Nodes.Add("...");
            dotBuilderNode.ImageKey = itemIcon;
            dotBuilderNode.SelectedImageKey = itemIcon;

            TreeNode entityTranslatorsNode = libraryProjectNode.Nodes.Add("EntityTranslators");
            entityTranslatorsNode.ImageKey = folderIcon;
            entityTranslatorsNode.SelectedImageKey = folderIcon;

            TreeNode dotTranslatorNode = entityTranslatorsNode.Nodes.Add("...");
            dotTranslatorNode.ImageKey = itemIcon;
            dotTranslatorNode.SelectedImageKey = itemIcon;

            TreeNode entLibNode = libraryProjectNode.Nodes.Add("EntLib");
            entLibNode.ImageKey = folderIcon;
            entLibNode.SelectedImageKey = folderIcon;

            TreeNode libraryServicesNode = libraryProjectNode.Nodes.Add("Services");
            libraryServicesNode.ImageKey = folderIcon;
            libraryServicesNode.SelectedImageKey = folderIcon;

            TreeNode dotLibraryServicesNode = GetNode(libraryServicesNode, "...", itemIcon, itemIcon);

            TreeNode libraryUINode = libraryProjectNode.Nodes.Add("UI");
            libraryUINode.ImageKey = folderIcon;
            libraryUINode.SelectedImageKey = folderIcon;

            TreeNode dotLibraryUINode = GetNode(libraryUINode, "...", itemIcon, itemIcon);

            TreeNode librarySmartAppNode = libraryProjectNode.Nodes.Add(String.Format("SmartClientApplication.{0}", extension));
            librarySmartAppNode.ImageKey = itemIcon;
            librarySmartAppNode.SelectedImageKey = itemIcon;

            TreeNode dotLibraryNode = libraryProjectNode.Nodes.Add("...");
            dotLibraryNode.ImageKey = itemIcon;
            dotLibraryNode.SelectedImageKey = itemIcon;

            // Module

            TreeNode moduleProjectNode = solutionInfrNode.Nodes.Add("Infrastructure.Module");
            moduleProjectNode.ImageKey = projectIcon;
            moduleProjectNode.SelectedImageKey = projectIcon;

            TreeNode dotModuleConstantsNode = moduleProjectNode.Nodes.Add(String.Format("Module{0}.{1}", moduleSuffix, extension));
            dotModuleConstantsNode.ImageKey = itemIcon;
            dotModuleConstantsNode.SelectedImageKey = itemIcon;

            TreeNode dotModuleServicesNode = moduleProjectNode.Nodes.Add(String.Format("ModuleController.{0}", extension));
            dotModuleServicesNode.ImageKey = itemIcon;
            dotModuleServicesNode.SelectedImageKey = itemIcon;

            // Shell

            TreeNode shellProjectNode = GetNode(solutionInfrNode, "Shell", projectIcon, projectIcon);

            TreeNode shellApplicationNode = GetNode(shellProjectNode, String.Format("ShellApplication.{0}", extension), itemIcon, itemIcon);

            TreeNode shellDotNode = GetNode(shellProjectNode, "...", itemIcon, itemIcon);

            rootNode.Expand();
            solutionInfrNode.Expand();
            if (interfaceNodeExpanded)
            {
                interfaceProjectNode.Expand();
            }
            if (libraryNodeExpanded)
            {
                libraryProjectNode.Expand();
            }
            moduleProjectNode.Expand();
            shellProjectNode.Expand();

            assembliesTreeView.EndUpdate();
        }
        
        #endregion

        #region Add Business Module Preview

        public void GetBusinessModulePreview(bool withInterface, bool withTestProject, string moduleName)
        {
            assembliesTreeView.BeginUpdate();
            assembliesTreeView.Nodes.Clear();

            TreeNode rootNode;

            rootNode = assembliesTreeView.Nodes.Add("Source");
            rootNode.ImageKey = solutionFolderIcon;
            rootNode.SelectedImageKey = solutionFolderIcon;

            TreeNode moduleProjectNode = rootNode.Nodes.Add(moduleName);
            moduleProjectNode.ImageKey = projectIcon;
            moduleProjectNode.SelectedImageKey = projectIcon;

            TreeNode constantsNode = moduleProjectNode.Nodes.Add("Constants");
            constantsNode.ImageKey = folderIcon;
            constantsNode.SelectedImageKey = folderIcon;

            TreeNode commandNode = constantsNode.Nodes.Add(String.Format("CommandNames.{0}", extension));
            commandNode.ImageKey = itemIcon;
            commandNode.SelectedImageKey = itemIcon;

            TreeNode eventTopicNode = constantsNode.Nodes.Add(String.Format("EventTopicNames.{0}", extension));
            eventTopicNode.ImageKey = itemIcon;
            eventTopicNode.SelectedImageKey = itemIcon;

            TreeNode uiExtensionNode = constantsNode.Nodes.Add(String.Format("UIExtensionSiteNames.{0}", extension));
            uiExtensionNode.ImageKey = itemIcon;
            uiExtensionNode.SelectedImageKey = itemIcon;

            TreeNode workspaceNode = constantsNode.Nodes.Add(String.Format("WorkspaceNames.{0}", extension));
            workspaceNode.ImageKey = itemIcon;
            workspaceNode.SelectedImageKey = itemIcon;

            TreeNode servicesNode = moduleProjectNode.Nodes.Add("Services");
            servicesNode.ImageKey = folderIcon;
            servicesNode.SelectedImageKey = folderIcon;

            TreeNode viewsNode = moduleProjectNode.Nodes.Add("Views");
            viewsNode.ImageKey = folderIcon;
            viewsNode.SelectedImageKey = folderIcon;

            TreeNode moduleInitializerNode = moduleProjectNode.Nodes.Add(String.Format("Module{0}.{1}", moduleSuffix, extension));
            moduleInitializerNode.ImageKey = itemIcon;
            moduleInitializerNode.SelectedImageKey = itemIcon;

            TreeNode moduleControllerNode = moduleProjectNode.Nodes.Add(String.Format("ModuleController.{0}", extension));
            moduleControllerNode.ImageKey = itemIcon;
            moduleControllerNode.SelectedImageKey = itemIcon;

            if (withInterface)
            {
                string moduleInterfaceName = String.Format(CultureInfo.InvariantCulture, "{0}.Interface", moduleName);
                TreeNode moduleInterfaceProjectNode = rootNode.Nodes.Add(moduleInterfaceName);
                moduleInterfaceProjectNode.ImageKey = projectIcon;
                moduleInterfaceProjectNode.SelectedImageKey = projectIcon;

                TreeNode constantsInterfaceNode = moduleInterfaceProjectNode.Nodes.Add("Constants");
                constantsInterfaceNode.ImageKey = folderIcon;
                constantsInterfaceNode.SelectedImageKey = folderIcon;

                TreeNode commandInterfaceNode = constantsInterfaceNode.Nodes.Add(String.Format("CommandNames.{0}", extension));
                commandInterfaceNode.ImageKey = itemIcon;
                commandInterfaceNode.SelectedImageKey = itemIcon;

                TreeNode eventTopicInterfaceNode = constantsInterfaceNode.Nodes.Add(String.Format("EventTopicNames.{0}", extension));
                eventTopicInterfaceNode.ImageKey = itemIcon;
                eventTopicInterfaceNode.SelectedImageKey = itemIcon;

                TreeNode uiExtensionInterfaceNode = constantsInterfaceNode.Nodes.Add(String.Format("UIExtensionSiteNames.{0}", extension));
                uiExtensionInterfaceNode.ImageKey = itemIcon;
                uiExtensionInterfaceNode.SelectedImageKey = itemIcon;

                TreeNode workspaceInterfaceNode = constantsInterfaceNode.Nodes.Add(String.Format("WorkspaceNames.{0}", extension));
                workspaceInterfaceNode.ImageKey = itemIcon;
                workspaceInterfaceNode.SelectedImageKey = itemIcon;

                TreeNode servicesInterfaceNode = moduleInterfaceProjectNode.Nodes.Add("Services");
                servicesInterfaceNode.ImageKey = folderIcon;
                servicesInterfaceNode.SelectedImageKey = folderIcon;

                moduleInterfaceProjectNode.Expand();
            }

            if (withTestProject)
            {
                TreeNode testProjectNode = GetNode(rootNode, String.Format(CultureInfo.InvariantCulture, "{0}.Tests", moduleName), projectIcon, projectIcon);

                TreeNode supportNode = GetNode(testProjectNode, "Support", folderIcon, folderIcon);

                TreeNode testableWorkItemNode = GetNode(supportNode, String.Format("TestableRootWorkItem.{0}", extension), itemIcon, itemIcon);

                TreeNode viewsTestNode = testProjectNode.Nodes.Add("Views");
                viewsTestNode.ImageKey = folderIcon;
                viewsTestNode.SelectedImageKey = folderIcon;
                
                TreeNode moduleControllerTestNode = GetNode(testProjectNode, String.Format("ModuleControllerTestFixture.{0}", extension), itemIcon, itemIcon);

                TreeNode moduleInitializerTestNode = GetNode(testProjectNode, String.Format("Module{0}TestFixture.{1}", moduleSuffix, extension), itemIcon, itemIcon);



                testProjectNode.Expand();
            }

            rootNode.Expand();
            moduleProjectNode.Expand();

            assembliesTreeView.EndUpdate();
        }

        #endregion

        #region Add Foundational Module Preview

        public void GetFoundationalModulePreview(bool withInterface, bool withLayout, bool withTestProject, string moduleName)
        {
            assembliesTreeView.BeginUpdate();
            assembliesTreeView.Nodes.Clear();

            TreeNode rootNode;

            rootNode = assembliesTreeView.Nodes.Add("Source");
            rootNode.ImageKey = solutionFolderIcon;
            rootNode.SelectedImageKey = solutionFolderIcon;

            TreeNode moduleProjectNode = rootNode.Nodes.Add(moduleName);
            moduleProjectNode.ImageKey = projectIcon;
            moduleProjectNode.SelectedImageKey = projectIcon;

            TreeNode constantsNode = moduleProjectNode.Nodes.Add("Constants");
            constantsNode.ImageKey = folderIcon;
            constantsNode.SelectedImageKey = folderIcon;

            TreeNode commandNode = constantsNode.Nodes.Add(String.Format("CommandNames.{0}", extension));
            commandNode.ImageKey = itemIcon;
            commandNode.SelectedImageKey = itemIcon;

            TreeNode eventTopicNode = constantsNode.Nodes.Add(String.Format("EventTopicNames.{0}", extension));
            eventTopicNode.ImageKey = itemIcon;
            eventTopicNode.SelectedImageKey = itemIcon;

            TreeNode uiExtensionNode = constantsNode.Nodes.Add(String.Format("UIExtensionSiteNames.{0}", extension));
            uiExtensionNode.ImageKey = itemIcon;
            uiExtensionNode.SelectedImageKey = itemIcon;

            TreeNode workspaceNode = constantsNode.Nodes.Add(String.Format("WorkspaceNames.{0}", extension));
            workspaceNode.ImageKey = itemIcon;
            workspaceNode.SelectedImageKey = itemIcon;

            TreeNode servicesNode = moduleProjectNode.Nodes.Add("Services");
            servicesNode.ImageKey = folderIcon;
            servicesNode.SelectedImageKey = folderIcon;


            if (withLayout)
            {
                TreeNode layoutViewInterfaceNode = moduleProjectNode.Nodes.Add(String.Format("ILayoutView.{0}", extension));
                layoutViewInterfaceNode.ImageKey = itemIcon;
                layoutViewInterfaceNode.SelectedImageKey = itemIcon;

                TreeNode layoutViewNode = moduleProjectNode.Nodes.Add(String.Format("LayoutView.{0}", extension));
                layoutViewNode.ImageKey = winFormsUserControlIcon;
                layoutViewNode.SelectedImageKey = winFormsUserControlIcon;

                TreeNode layoutViewPresenterNode = moduleProjectNode.Nodes.Add(String.Format("LayoutViewPresenter.{0}", extension));
                layoutViewPresenterNode.ImageKey = itemIcon;
                layoutViewPresenterNode.SelectedImageKey = itemIcon;
            }

            TreeNode moduleInitializerNode = moduleProjectNode.Nodes.Add(String.Format("Module{0}.{1}", moduleSuffix, extension));
            moduleInitializerNode.ImageKey = itemIcon;
            moduleInitializerNode.SelectedImageKey = itemIcon;

            if (withInterface)
            {
                string moduleInterfaceName = String.Format(CultureInfo.InvariantCulture, "{0}.Interface", moduleName);
                TreeNode moduleInterfaceProjectNode = rootNode.Nodes.Add(moduleInterfaceName);
                moduleInterfaceProjectNode.ImageKey = projectIcon;
                moduleInterfaceProjectNode.SelectedImageKey = projectIcon;

                TreeNode constantsInterfaceNode = moduleInterfaceProjectNode.Nodes.Add("Constants");
                constantsInterfaceNode.ImageKey = "FolderIcon";
                constantsInterfaceNode.SelectedImageKey = "FolderIcon";

                TreeNode commandInterfaceNode = constantsInterfaceNode.Nodes.Add(String.Format("CommandNames.{0}", extension));
                commandInterfaceNode.ImageKey = itemIcon;
                commandInterfaceNode.SelectedImageKey = itemIcon;

                TreeNode eventTopicInterfaceNode = constantsInterfaceNode.Nodes.Add(String.Format("EventTopicNames.{0}", extension));
                eventTopicInterfaceNode.ImageKey = itemIcon;
                eventTopicInterfaceNode.SelectedImageKey = itemIcon;

                TreeNode uiExtensionInterfaceNode = constantsInterfaceNode.Nodes.Add(String.Format("UIExtensionSiteNames.{0}", extension));
                uiExtensionInterfaceNode.ImageKey = itemIcon;
                uiExtensionInterfaceNode.SelectedImageKey = itemIcon;

                TreeNode workspaceInterfaceNode = constantsInterfaceNode.Nodes.Add(String.Format("WorkspaceNames.{0}", extension));
                workspaceInterfaceNode.ImageKey = itemIcon;
                workspaceInterfaceNode.SelectedImageKey = itemIcon;

                TreeNode servicesInterfaceNode = moduleInterfaceProjectNode.Nodes.Add("Services");
                servicesInterfaceNode.ImageKey = "FolderIcon";
                servicesInterfaceNode.SelectedImageKey = "FolderIcon";

                moduleInterfaceProjectNode.Expand();
            }

            if (withTestProject)
            {
                TreeNode testProjectNode = GetNode(rootNode, String.Format(CultureInfo.InvariantCulture, "{0}.Tests", moduleName), projectIcon, projectIcon);

                TreeNode supportNode = GetNode(testProjectNode, "Support", folderIcon, folderIcon);

                TreeNode testableWorkItemNode = GetNode(supportNode, String.Format("TestableRootWorkItem.{0}", extension), itemIcon, itemIcon);

                TreeNode moduleInitializerTestNode = GetNode(testProjectNode, String.Format("Module{0}TestFixture.{1}", moduleSuffix, extension), itemIcon, itemIcon);

                testProjectNode.Expand();
            }

            rootNode.Expand();
            moduleProjectNode.Expand();

            assembliesTreeView.EndUpdate();
        }

        #endregion

        #region Add View Preview

        public void GetViewPreview(bool createViewFolder, string viewName, bool testProjectExists, IProjectModel activeModuleProject, IProjectItemModel activeProjectItem, bool isWpfView)
        {
            assembliesTreeView.BeginUpdate();
            assembliesTreeView.Nodes.Clear();

            if (activeModuleProject != null)
            {
                TreeNode rootNode;

                rootNode = assembliesTreeView.Nodes.Add("Source");
                rootNode.ImageKey = solutionFolderIcon;
                rootNode.SelectedImageKey = solutionFolderIcon;

                TreeNode moduleProjectNode = rootNode.Nodes.Add(activeModuleProject.Name);
                moduleProjectNode.ImageKey = projectIcon;
                moduleProjectNode.SelectedImageKey = projectIcon;

                TreeNode viewsParentFolderNode = moduleProjectNode;
                TreeNode parentNode = moduleProjectNode;

                if (activeProjectItem != null)
                {
                    string projectPath = activeModuleProject.ProjectPath;
                    string folderPath = activeProjectItem.ItemPath;

                    if (folderPath.StartsWith(projectPath))
                    {
                        folderPath = folderPath.Replace(projectPath, String.Empty);
                        folderPath = Path.GetDirectoryName(folderPath);

                        string[] folders = folderPath.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string innerFolder in folders)
                        {
                            parentNode = parentNode.Nodes.Add(innerFolder);
                            parentNode.Parent.Expand();
                            parentNode.ImageKey = folderIcon;
                            parentNode.SelectedImageKey = folderIcon;
                        }

                        viewsParentFolderNode = parentNode.Nodes.Add(activeProjectItem.Name);
                        viewsParentFolderNode.ImageKey = folderIcon;
                        viewsParentFolderNode.SelectedImageKey = folderIcon;
                        parentNode.Expand();
                    }
                }

                TreeNode viewFolderNode;
                if (createViewFolder)
                {
                    viewFolderNode = viewsParentFolderNode.Nodes.Add(viewName);
                    viewFolderNode.ImageKey = folderIcon;
                    viewFolderNode.SelectedImageKey = folderIcon;
                }
                else
                {
                    viewFolderNode = viewsParentFolderNode;
                }


                TreeNode viewInterfaceNode = viewFolderNode.Nodes.Add(String.Format("I{0}.{1}", viewName, extension));
                viewInterfaceNode.ImageKey = itemIcon;
                viewInterfaceNode.SelectedImageKey = itemIcon;

                string userControlExtension = isWpfView ? xamlExtension : extension;
                string userControlIcon = isWpfView ? wpfUserControlIcon : winFormsUserControlIcon;
                TreeNode viewUserControlNode = viewFolderNode.Nodes.Add(String.Format("{0}.{1}", viewName, userControlExtension));
                viewUserControlNode.ImageKey = userControlIcon;
                viewUserControlNode.SelectedImageKey = userControlIcon;

                TreeNode viewPresenterNode = viewFolderNode.Nodes.Add(String.Format("{0}Presenter.{1}", viewName, extension));
                viewPresenterNode.ImageKey = itemIcon;
                viewPresenterNode.SelectedImageKey = itemIcon;

                if (testProjectExists)
                {
                    string testProjectName = string.Format(CultureInfo.InvariantCulture, "{0}.Tests", activeModuleProject.Name);
                    TreeNode testProjectNode = rootNode.Nodes.Add(testProjectName);
                    testProjectNode.ImageKey = projectIcon;
                    testProjectNode.SelectedImageKey = projectIcon;
                    testProjectNode.Expand();

                    TreeNode testDotNode1 = testProjectNode.Nodes.Add("...");
                    testDotNode1.ImageKey = folderIcon;
                    testDotNode1.SelectedImageKey = folderIcon;

                    TreeNode testViewsNode = testProjectNode.Nodes.Add("Views");
                    testViewsNode.ImageKey = folderIcon;
                    testViewsNode.SelectedImageKey = folderIcon;

                    TreeNode fixtureNode = testViewsNode.Nodes.Add(string.Format(CultureInfo.InvariantCulture, "{0}PresenterFixture.cs", viewName));
                    fixtureNode.ImageKey = itemIcon;
                    fixtureNode.SelectedImageKey = itemIcon;

                    TreeNode testDotNode2 = testProjectNode.Nodes.Add("...");
                    testDotNode2.ImageKey = folderIcon;
                    testDotNode2.SelectedImageKey = folderIcon;

                    testProjectNode.Expand();
                    testViewsNode.Expand();

                }

                rootNode.Expand();
                moduleProjectNode.Expand();
                viewsParentFolderNode.Expand();
                viewFolderNode.Expand();
            }

            assembliesTreeView.EndUpdate();
        }

        #endregion

        #region Add Disconnected Service Agent Preview

        public void GetDisconnectedServiceAgentPreview(string proxyFolder, string disconnectedAgentsFolder,string agentFileName, string agentCallbackFileName, IProjectModel activeModuleProject)
        {
            assembliesTreeView.BeginUpdate();
            assembliesTreeView.Nodes.Clear();

            if (proxyFolder != null && activeModuleProject != null)
            {
                TreeNode rootNode;

                rootNode = assembliesTreeView.Nodes.Add("Source");
                rootNode.ImageKey = solutionFolderIcon;
                rootNode.SelectedImageKey = solutionFolderIcon;

                TreeNode moduleProjectNode = rootNode.Nodes.Add(activeModuleProject.Name);
                moduleProjectNode.ImageKey = projectIcon;
                moduleProjectNode.SelectedImageKey = projectIcon;

                TreeNode agentsFolderNode;
                agentsFolderNode = moduleProjectNode.Nodes.Add(disconnectedAgentsFolder);
                agentsFolderNode.ImageKey = folderIcon;
                agentsFolderNode.SelectedImageKey = folderIcon;

                TreeNode proxyTypeFolderNode;
                proxyTypeFolderNode = agentsFolderNode.Nodes.Add(proxyFolder);
                proxyTypeFolderNode.ImageKey = folderIcon;
                proxyTypeFolderNode.SelectedImageKey = folderIcon;

                TreeNode agentFileNode;
                agentFileNode = proxyTypeFolderNode.Nodes.Add(string.Format("{0}.{1}", agentFileName, extension));
                agentFileNode.ImageKey = itemIcon;
                agentFileNode.SelectedImageKey = itemIcon;

                TreeNode agentCallbackFileNode;
                agentCallbackFileNode = proxyTypeFolderNode.Nodes.Add(string.Format("{0}.{1}", agentCallbackFileName, extension));
                agentCallbackFileNode.ImageKey = itemIcon;
                agentCallbackFileNode.SelectedImageKey = itemIcon;

                rootNode.Expand();
                moduleProjectNode.Expand();
                agentsFolderNode.Expand();
                proxyTypeFolderNode.Expand();
                agentFileNode.Expand();
            }

            assembliesTreeView.EndUpdate();
        }

        #endregion

        private TreeNode GetNode(TreeNode parent, string name, string imageKey, string selectedImageKey)
        {
            TreeNode node = parent.Nodes.Add(name);
            node.ImageKey = imageKey;
            node.SelectedImageKey = selectedImageKey;

            return node;
        }

        public SolutionPreviewHelper(TreeView assembliesTreeView, string language)
        {
            this.assembliesTreeView = assembliesTreeView;

            if (language == "VB")
            {
                projectIcon = "VBProjectIcon";
                extension = "vb";
                itemIcon = "VBItemIcon";
                moduleSuffix = "Initializer";
            }
        }
    }
}
