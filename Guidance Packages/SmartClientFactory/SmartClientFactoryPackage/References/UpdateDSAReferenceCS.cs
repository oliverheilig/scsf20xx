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
using System.Runtime.Serialization;
using Microsoft.Practices.RecipeFramework;
using Microsoft.Practices.RecipeFramework.VisualStudio.Templates;
using EnvDTE;
using VSLangProj;
using Microsoft.Practices.SmartClientFactory.Actions;
using Microsoft.Practices.SmartClientFactory.Helpers;
using System.Reflection;

namespace Microsoft.Practices.SmartClientFactory.References
{
	[Serializable]
	public class UpdateDSAReferenceCS : UnboundRecipeReference
	{
		public UpdateDSAReferenceCS(string recipe)
			: base(recipe)
		{
		}

        public UpdateDSAReferenceCS(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

        public override bool IsEnabledFor(object target)
        {
            CodeClass codeClass=null;
            bool haveADSAClass;

            // We avoid the "Class not yet compiled" exception. Because this is a Reference class.
            try
            {
                haveADSAClass = CodeModelHelper.HaveAClassInProjectItems(target, out codeClass, CodeModelHelper.IsDSAClass);
            }
            catch
            {
                haveADSAClass = false;
            }
            
            if (target is ProjectItem
                && haveADSAClass)
            {
                return (codeClass.Language == CodeModelLanguageConstants.vsCMLanguageCSharp);
            }
            return false;
        }

        public override string AppliesTo
		{
			get { return "Any folder containing a C# Disconnected Service Agent class."; }
		}
	}
}
