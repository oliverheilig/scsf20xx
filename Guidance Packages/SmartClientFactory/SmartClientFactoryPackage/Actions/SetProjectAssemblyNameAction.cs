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
using System;
using System.Collections.Generic;
using Microsoft.Practices.RecipeFramework;
using Microsoft.Practices.RecipeFramework.Library;
using Microsoft.Practices.RecipeFramework.Library.CodeModel;
using System.Globalization;

namespace Microsoft.Practices.SmartClientFactory.Actions
{
    public class SetProjectAssemblyNameAction : ConfigurableAction
    {
        [Input(Required = true)]
        public Project Project
        { get; set; }

        [Input(Required = true)]
        public string RootNamespace
        { get; set; }

        public override void Execute()
        {
            Project.Properties.Item("AssemblyName").Value = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", RootNamespace, Project.Name);
        }

        public override void Undo()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
