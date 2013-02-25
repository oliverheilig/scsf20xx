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
using Microsoft.Practices.RecipeFramework;
using EnvDTE;
using System.Runtime.Serialization;
using Microsoft.Practices.RecipeFramework.Library.Solution;
using Microsoft.Practices.SmartClientFactory.Actions;
using Microsoft.Practices.SmartClientFactory.Helpers;

namespace Microsoft.Practices.SmartClientFactory.References
{
	[Serializable]
	public class AddEventSubscriptionRecipeReferenceVB : UnboundRecipeReference
	{
		public AddEventSubscriptionRecipeReferenceVB(string recipe)
			: base(recipe) { }

        public AddEventSubscriptionRecipeReferenceVB(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

        public override bool IsEnabledFor(object target)
        {
            CodeClass codeClass = null;
            if (CodeModelHelper.HaveAClass(target, out codeClass))
            {
                if (!Utility.IsSealedOrStatic(codeClass))
                {
                    return (codeClass.Language == CodeModelLanguageConstants.vsCMLanguageVB);
                }
            }

            return false;
        }

		public override string AppliesTo
		{
			get { return "Any non inheritable VB Class"; }
		}
	}
}
