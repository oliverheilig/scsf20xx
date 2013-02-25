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
using EnvDTE;
using Microsoft.Practices.RecipeFramework;

namespace Microsoft.Practices.SmartClientFactory.Actions
{
    /// <summary>
    /// Action that can take an existing project
    /// and add the tweaks necessary to properly
    /// compile WPF projects.
    /// </summary>
    /// <remarks>
    /// This action will result in VS throwing up the
    /// "Your project file has changed outside the
    /// editor" dialog. Unfortunately, the things we
    /// need to do aren't available through the VS
    /// extensibility objects, so we've got to edit
    /// the file directly.
    /// </remarks>
    public class ConvertProjectToWpfAction : ConfigurableAction
    {
        private Project project;

        [Input(Required=true)]
        public Project Project
        {
            get { return project; }
            set { project = value; }
        }

        public override void Execute()
        {
            string projectFileName = project.FullName;
            if(!project.Saved)
            {
                project.Save(""); // Empty string means just save to current file
            }

            WpfProjectFileConverter converter = new WpfProjectFileConverter(projectFileName);
            converter.ConvertToWpfProject();
            converter.Save(projectFileName);
        }

        public override void Undo()
        {
            // No undo supported
        }
    }
}