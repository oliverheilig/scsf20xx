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
using System.Runtime.InteropServices;
using Microsoft.Build.BuildEngine;
using Microsoft.Practices.SmartClientFactory.Properties;

namespace Microsoft.Practices.SmartClientFactory.Actions
{
    /// <summary>
    /// Given a path to a valid MSBuild project file,
    /// add the appropriate entries to make Visual Studio
    /// support WPF development in that project.
    /// </summary>
    public class WpfProjectFileConverter
    {
        private const string TypeGuidsProperty = "ProjectTypeGuids";

        private const string WpfProjectTypeGuid = "{60dc8134-eba5-43b8-bcc9-bb4bc16c2548}";
        private const string csharpProjectTypeGuid = "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}";
        private const string vbProjectTypeGuid = "{F184B08F-C81C-45F6-A57F-5ABD9991F28F}";
        private const string csharpImportPath = "Microsoft.CSharp.targets";
        private const string vbImportPath = "Microsoft.VisualBasic.targets";

        private Project project;

        public WpfProjectFileConverter(string filename)
        {
            Engine engine = new Engine();
            project = new Project(engine);
            project.Load(filename);
        }

        public bool IsWpfProject
        {
            get { return ContainsWpfTypeGuidProperty(); }
        }

        public bool IsDirty
        {
            get { return project.IsDirty; }
        }

        public bool CanConvertToWpfProject
        {
            get { return !IsWpfProject && !ContainsNonWpfTypeGuidProperty(); }
        }

        public void ConvertToWpfProject()
        {
            if(!IsWpfProject && !CanConvertToWpfProject)
            {
                throw new InvalidOperationException(Resources.CannotConvertProject);
            }

            if(CanConvertToWpfProject)
            {
                if(!ContainsWpfTypeGuidProperty())
                {
                    AddTypeGuidProperty();
                }
            }
        }

        public void Save(string filename)
        {
            if(project.IsDirty)
            {
                project.Save(filename);
            }
        }

        private void AddTypeGuidProperty()
        {
            // BuildPropertyGroup only supports iteration, not indexing. So we just grab
            // the first property group and place the property in there
            foreach(BuildPropertyGroup group in project.PropertyGroups)
            {
                if(string.IsNullOrEmpty(group.Condition))
                {
                    AddTypeGuidToGroup(group);
                    return;
                }
            }

            // If we got here, this is a very strange project file that doesn't 
            // have any global property groups. If that't the case, add one and
            // put our properties in.
            BuildPropertyGroup globalGroup = project.AddNewPropertyGroup(false);
            AddTypeGuidToGroup(globalGroup);
        }

        private void AddTypeGuidToGroup(BuildPropertyGroup group)
        {
            if(DoesImportTarget(csharpImportPath))
            {
                group.AddNewProperty(TypeGuidsProperty,
                    string.Format("{0};{1}", WpfProjectTypeGuid, csharpProjectTypeGuid));
            }
            else if(DoesImportTarget(vbImportPath))
            {
                group.AddNewProperty(TypeGuidsProperty,
                    string.Format("{0};{1}", WpfProjectTypeGuid, vbProjectTypeGuid));
            }
        }

        private bool ContainsWpfTypeGuidProperty()
        {
            foreach(BuildPropertyGroup group in project.PropertyGroups)
            {
                foreach(BuildProperty property in group)
                {
                    if(property.Name == TypeGuidsProperty &&
                        property.Value.Contains(WpfProjectTypeGuid))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool ContainsNonWpfTypeGuidProperty()
        {
            foreach(BuildPropertyGroup group in project.PropertyGroups)
            {
                foreach(BuildProperty property in group)
                {
                    if(property.Name == TypeGuidsProperty &&
                        !property.Value.Contains(WpfProjectTypeGuid))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static string GetClrPath()
        {
            return RuntimeEnvironment.GetRuntimeDirectory();
        }

        private bool DoesImportTarget(string target)
        {
            foreach(Import import in project.Imports)
            {
                if(import.ProjectPath.EndsWith(target))
                {
                    return true;
                }
            }
            return false;
        }
    }
}