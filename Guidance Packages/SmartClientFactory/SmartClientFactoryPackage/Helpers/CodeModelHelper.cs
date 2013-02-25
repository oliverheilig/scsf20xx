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
using EnvDTE;
using Microsoft.Practices.SmartClientFactory.Actions;
using System.Collections;

namespace Microsoft.Practices.SmartClientFactory.Helpers
{
    public sealed class CodeModelHelper
    {
        internal static ProjectItem GetFirstSelectedItem(object target)
        {
            ProjectItem projectItem = null;
            if (target is SelectedItems)
            {
                SelectedItems items = (SelectedItems)target;
                if ((items.Count > 1) && (items.Item(1).ProjectItem != null))
                {
                    projectItem = items.Item(1).ProjectItem;
                }
            }
            else if (target is ProjectItem)
            {
                projectItem = (ProjectItem)target;
            }

            return projectItem;
        }

        public static bool HaveAClass(object target, out CodeClass codeClass, Predicate<CodeClass> condition)
        {
            ProjectItem projectItem = GetFirstSelectedItem(target);

            codeClass = null;
            if ((projectItem != null) && (projectItem.FileCodeModel != null))
            {
                codeClass = FindFirstClass(projectItem.FileCodeModel.CodeElements, condition);
            }

            return (codeClass!=null);
        }

        public static bool HaveAClass(object target, out CodeClass codeClass)
        {
            return HaveAClass(target, out codeClass, delegate(CodeClass innerClass){return true;});
        }

        public static bool HaveAClassInProjectItems(object target, out CodeClass codeClass, Predicate<CodeClass> condition)
        {
            ProjectItem projectItem = GetFirstSelectedItem(target);

            codeClass = null;
            if (projectItem != null && projectItem.ProjectItems.Count > 0)
            {
                foreach (ProjectItem item in projectItem.ProjectItems)
                {
                    if (HaveAClass(item, out codeClass, condition))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        internal static CodeClass FindFirstClass(CodeElements elements, Predicate<CodeClass> condition)
        {
            foreach (CodeElement element in elements)
            {
                CodeNamespace ns = element as CodeNamespace;
                if (ns != null)
                {
                    return FindFirstClass(ns.Children, condition);
                }

                CodeClass cc = element as CodeClass;
                if (cc != null
                    && condition(cc))
                {
                    return cc;
                }
            }

            return null;
        }

        internal static CodeClass FindFirstClass(CodeElements elements)
        {
            return FindFirstClass(elements, delegate { return true; });
        }



        internal static bool IsDSAClass(CodeClass codeClass)
        {
            bool hasOnlineProxyType = false;
            bool hasEndpoint = false;
            foreach(CodeElement element in codeClass.Members)
            {
                CodeProperty property = element as CodeProperty;
                if (property != null
                    && property.Access==vsCMAccess.vsCMAccessPublic
                    && property.Name=="OnlineProxyType")
                {
                    hasOnlineProxyType= true;
                }
                if (property != null
                    && property.Access == vsCMAccess.vsCMAccessPublic
                    && property.Name == "Endpoint")
                {
                    hasEndpoint = true;
                }
                if (hasOnlineProxyType && hasEndpoint)
                {
                    return true;
                }
            }

            return false;
        }

        internal static bool IsProxyClass(CodeClass codeClass)
        {
            foreach (CodeElement codeBase in codeClass.Bases)
            {
                if (codeBase.FullName.StartsWith("System.ServiceModel.ClientBase")
                    || codeBase.FullName.StartsWith("System.Web.Services.Protocols.SoapHttpClientProtocol"))
                {
                    return true;
                }
            }
            return false;
        }

        internal static CodeProperty GetProperty(CodeClass codeClass, string propertyName, vsCMAccess access)
        {
            foreach (CodeElement element in codeClass.Members)
            {
                CodeProperty property = element as CodeProperty;
                if (property != null
                    && property.Access == access
                    && property.Name.Equals(propertyName, StringComparison.CurrentCultureIgnoreCase))
                {
                    return property;
                }
            }
            return null;
        }

        internal static bool IsWebReferenceOrServiceReference(ProjectItem item)
        {
            bool isWebReferenceProperty = false;
            bool isExtensionProperty = false;
            bool isServiceReferences = false;

            try
            {
                isWebReferenceProperty = !String.IsNullOrEmpty((string)item.Properties.Item("WebReference").Value);
            }
            catch
            {
                isWebReferenceProperty = false;
            }

            try
            {
                string extension = (string)item.Properties.Item("extension").Value;
                isExtensionProperty = (!String.IsNullOrEmpty(extension) && (extension.Equals(".map")));
            }
            catch
            {
                isExtensionProperty = false;
            }

            try
            {
                bool svcmapExtension = false;
                bool svcinfoExtension = false;

                IEnumerator enumerator = item.ProjectItems.GetEnumerator();

                while (enumerator.MoveNext() && (!isServiceReferences))
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

                    isServiceReferences = svcmapExtension && svcinfoExtension;
                }
            }
            catch
            {
                isServiceReferences = false;
            }

            if (isWebReferenceProperty)
            {
                return true;
            }
            else
            {
                if (isExtensionProperty)
                {
                    ProjectItem serviceReferencesFolder = item.Collection.Parent as ProjectItem;
                    if (serviceReferencesFolder != null && serviceReferencesFolder.Name.Equals("Service References"))
                    {
                        return true;
                    }
                }
                else
                {
                    if (isServiceReferences)
                    {
                        ProjectItem serviceReferencesFolder = item.Collection.Parent as ProjectItem;
                        if (serviceReferencesFolder != null && serviceReferencesFolder.Name.Equals("Service References"))
                        {
                            return true;
                        } 
                    }
                }
            }

            return false;
        }
    }
}
