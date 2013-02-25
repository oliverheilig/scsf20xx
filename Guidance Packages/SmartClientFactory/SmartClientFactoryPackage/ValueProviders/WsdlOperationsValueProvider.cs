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
using Microsoft.Practices.Common;
using Microsoft.Practices.RecipeFramework;
using Microsoft.Practices.RecipeFramework.Extensions.CommonHelpers;
using System.ComponentModel.Design;
using EnvDTE;
using System.Web.Services.Description;
using System.Collections.Specialized;
using System.Reflection;

namespace Microsoft.Practices.SmartClientFactory.ValueProviders
{
    public class WsdlOperationsValueProvider : ValueProvider, IAttributesConfigurable
    {
        private const string CurrentItemKey = "CurrentItem";
        private const string OriginalTypeMethodsKey = "OriginalTypeMethods";
        private const string ProxyTechnologyKey = "ProxyTechnology";
        private string currentItemExpression;
        private string originalTypeMethodsExpression;
        private string proxyTechnologyExpression;

        public override bool OnBeginRecipe(object currentValue, out object newValue)
        {
            newValue = null;
            if (currentValue == null)
            {
                ProjectItem currentItem = ExpressionEvaluationHelper.EvaluateExpression((IDictionaryService)GetService(typeof(IDictionaryService)),
                                                                currentItemExpression) as ProjectItem;
                if (currentItem != null)
                {
                    object proxyTechnology = ExpressionEvaluationHelper.EvaluateExpression((IDictionaryService)GetService(typeof(IDictionaryService)),
                                                                proxyTechnologyExpression);
                    if (proxyTechnology == null || (ProxyTechnology)proxyTechnology == ProxyTechnology.Asmx)
                    {
                        newValue = GetWsdlOperations(currentItem);
                    }
                    else
                    {
                        List<MethodInfo> originalTypeMethods = ExpressionEvaluationHelper.EvaluateExpression((IDictionaryService)GetService(typeof(IDictionaryService)),
                                                                originalTypeMethodsExpression) as List<MethodInfo>;
                        if (originalTypeMethods != null)
                        {
                            newValue = GetOperationsFromTypeMethods(originalTypeMethods);
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        private List<string> GetOperationsFromTypeMethods(List<MethodInfo> originalTypeMethods)
        {
            List<string> operations = new List<string>();
            foreach (MethodInfo method in originalTypeMethods)
            {
                operations.Add(method.Name);
            }
            return operations;
        }

        private static List<string> GetWsdlOperations(ProjectItem WebReferenceProjectItem)
        {
            List<string> wsdlOperations=new List<string>();
            ProjectItem wsdlItem = GetWsdlItem(WebReferenceProjectItem);

            if (wsdlItem != null)
            {
                ServiceDescription wsdl = ReadWsdlFile(wsdlItem);
                wsdlOperations = GetWsdlOperations(wsdl);
            }
            return wsdlOperations;
        }

        private static ProjectItem GetWsdlItem(ProjectItem webReferenceItem)
        {
            foreach (ProjectItem item in webReferenceItem.ProjectItems)
                if (item.Name.ToLowerInvariant().EndsWith(".wsdl"))
                    return item;

            return null;
        }

        private static ServiceDescription ReadWsdlFile(ProjectItem wsdlItem)
        {
            return ServiceDescription.Read(wsdlItem.get_FileNames(0));
        }

        private static List<string> GetWsdlOperations(ServiceDescription wsdl)
        {
            List<string> operationNames = new List<string>();

            foreach (PortType type in wsdl.PortTypes)
                foreach (Operation oper in type.Operations)
                    operationNames.Add(oper.Name);

            return operationNames;
        }

        public void Configure(StringDictionary attributes)
        {
            currentItemExpression = attributes[CurrentItemKey];
            originalTypeMethodsExpression = attributes[OriginalTypeMethodsKey];
            proxyTechnologyExpression = attributes[ProxyTechnologyKey];
        }
    }
}
