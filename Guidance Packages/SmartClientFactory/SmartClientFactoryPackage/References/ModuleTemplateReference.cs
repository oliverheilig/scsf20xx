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
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;
using EnvDTE;
using Microsoft.Practices.RecipeFramework.VisualStudio.Templates;

namespace Microsoft.Practices.SmartClientFactory.References
{
    [Serializable]
    public class ModuleTemplateReference : UnboundTemplateReference
    {
        public ModuleTemplateReference(string recipe)
            : base(recipe)
        {
        }

        public ModuleTemplateReference(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override bool IsEnabledFor(object target)
        {
            return true;
        }

        public override string AppliesTo
        {
            get
            {
                return "Any solution folder or the solution root";
            }
        }
    }
}
