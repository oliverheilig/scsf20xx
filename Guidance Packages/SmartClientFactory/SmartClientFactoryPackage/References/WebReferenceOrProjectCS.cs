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
using Microsoft.Practices.RecipeFramework;
using EnvDTE;
using System.Runtime.Serialization;
using Microsoft.Practices.Common;
using System.Globalization;
using Microsoft.Practices.SmartClientFactory.Helpers;

namespace Microsoft.Practices.SmartClientFactory.References
{
	[Serializable]
	public class WebReferenceOrProjectCS : UnboundRecipeReference
	{
		public WebReferenceOrProjectCS(string recipe)
			: base(recipe)
		{
		}

		public override bool IsEnabledFor(object target)
		{
			ProjectItem item = target as ProjectItem;

			if (item != null)
			{
				try
				{
                    if (CodeModelHelper.IsWebReferenceOrServiceReference(item))
                    {
                        return (item.ContainingProject.CodeModel.Language == CodeModelLanguageConstants.vsCMLanguageCSharp);
                    }
                }
				catch (Exception)
				{
				}
			}

            Project project = target as Project;
            if (project != null)
            {
                return (project.CodeModel.Language == CodeModelLanguageConstants.vsCMLanguageCSharp);
            }

			return false;
		}

		public override string AppliesTo
		{
			get { return Properties.Resources.AllWebReferencesOrProject; }
		}

		#region ISerializable Members

		protected WebReferenceOrProjectCS(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		#endregion
	}
}
