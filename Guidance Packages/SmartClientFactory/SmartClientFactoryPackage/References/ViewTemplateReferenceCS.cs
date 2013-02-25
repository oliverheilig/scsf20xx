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
using System.Collections;

namespace Microsoft.Practices.SmartClientFactory.References
{
	[Serializable]
	public class ViewTemplateReferenceCS : UnboundRecipeReference
	{
		public ViewTemplateReferenceCS(string recipe)
			: base(recipe)
		{
		}

		public ViewTemplateReferenceCS(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

        public override bool IsEnabledFor(object target)
        {
            string expectedLanguage = CodeModelLanguageConstants.vsCMLanguageCSharp;
            if (target is Project)
            {
                Project project = (Project)target;
                return (project.CodeModel.Language == expectedLanguage) && ContainsRequiredReferences((Project)target);
            }
            if (target is ProjectItem)
            {
                ProjectItem item = (ProjectItem)target;
                bool haveADSAClass;
                CodeClass codeClass;

                // We avoid the "Class not yet compiled" exception. Because this is a Reference class.
                try
                {
                    haveADSAClass = CodeModelHelper.HaveAClassInProjectItems(target, out codeClass, CodeModelHelper.IsDSAClass);
                }
                catch
                {
                    haveADSAClass = false;
                }
                if (item.Kind == "{6BB5F8EF-4483-11D3-8BCF-00C04F8EC28C}" 
                    && IsNotWebReference(item) 
                    && (item.ContainingProject.CodeModel.Language == expectedLanguage)
                    && !haveADSAClass)
                {
                    return ContainsRequiredReferences(item.ContainingProject);
                }
                return false;
            }
            return false;
        }

        private static bool IsNotWebReference(ProjectItem item)
        {
            bool serviceReferences = false;
            bool webReference = false;

            try
            {
                bool svcmapExtension = false;
                bool svcinfoExtension = false;

                IEnumerator enumerator = item.ProjectItems.GetEnumerator();

                while (enumerator.MoveNext() && (!serviceReferences))
                {
                    ProjectItem it = (ProjectItem)enumerator.Current;

                    if (!svcmapExtension)
                    {
                        svcmapExtension = it.Properties.Item("extension").Value.Equals(".svcmap");
                    }

                    if (!svcinfoExtension)
                    {
                        svcinfoExtension = it.Properties.Item("extension").Value.Equals(".svcinfo");
                    }

                    serviceReferences = svcmapExtension && svcinfoExtension;
                }
            }
            catch
            {
                serviceReferences = false;
            }

            try
            {
                webReference = !String.IsNullOrEmpty((string)item.Properties.Item("WebReference").Value);
            }
            catch
            {
                webReference = false;
            }

            return ((!serviceReferences) && (!webReference));
        }

		private bool ContainsRequiredReferences(Project project)
		{
			DTE dte = GetService<DTE>();
			if (!dte.Solution.Globals.get_VariableExists("CommonProjectGuid"))
				return false;

            Guid commonProjectGuid;
            if (dte.Solution.Globals["CommonProjectGuid"] is Guid)
            {
                commonProjectGuid = (Guid)dte.Solution.Globals["CommonProjectGuid"];
            }
            else
            {
                commonProjectGuid = new Guid((string)dte.Solution.Globals["CommonProjectGuid"]);
            }
			Project prjCommon = Utility.GetProjectFromGuid(dte, GetService<IServiceProvider>(), commonProjectGuid);
			
			return ContainsReference(project, "Microsoft.Practices.CompositeUI") &&
				ContainsReference(project, "Microsoft.Practices.ObjectBuilder") &&
				ContainsReference(project, prjCommon.Name);
		}

		private bool ContainsReference(Project project, string referenceIdentity)
		{
			if (project.Name == referenceIdentity) return true;
			VSProject vsProject = (VSProject)project.Object;
			foreach (Reference reference in vsProject.References)
			{
				if (reference.Name == referenceIdentity) return true;
			}
			return false;
		}

		public override string AppliesTo
		{
			get { return "Any C# project referencing CAB Libraries and the Infrastructure.Interface library."; }
		}
	}
}
