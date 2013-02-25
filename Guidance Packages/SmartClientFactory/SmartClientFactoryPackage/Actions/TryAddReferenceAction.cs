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
using System.ComponentModel;
using Microsoft.Practices.ComponentModel;
using Microsoft.Practices.RecipeFramework;
using EnvDTE;
using VSLangProj;
using System.Globalization;

namespace Microsoft.Practices.SmartClientFactory.Actions
{
    [DesignerCategory("Code"), ServiceDependency(typeof(DTE))]
    public class TryAddReferenceAction : Microsoft.Practices.RecipeFramework.Action
    {
        // Fields
        private string referenceName;
        private Project referringProject;

        // Methods
        public TryAddReferenceAction()
        {
        }

        public override void Execute()
        {
            VSProject project = (VSProject)this.referringProject.Object;

            bool referenceExists = ReferenceExists(project);

            if (!referenceExists)
            {
                project.References.Add(this.ReferenceName);
            }
        }

        private bool ReferenceExists(VSProject project)
        {
            foreach (Reference reference in project.References)
            {
                if (string.Compare(System.IO.Path.GetFileName(reference.Path), System.IO.Path.GetFileName(this.ReferenceName), true, CultureInfo.CurrentCulture) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public override void Undo()
        {
        }

        [Input(Required = true)]
        public string ReferenceName
        {
            get
            {
                return this.referenceName;
            }
            set
            {
                this.referenceName = value;
            }
        }

        [Input(Required = true)]
        public Project ReferringProject
        {
            get
            {
                return this.referringProject;
            }
            set
            {
                this.referringProject = value;
            }
        }
    }
}
