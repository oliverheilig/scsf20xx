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
using System.Collections.Specialized;
using Microsoft.Practices.RecipeFramework;
using Microsoft.Practices.RecipeFramework.Extensions.CommonHelpers;
using System.ComponentModel.Design;
using System.Web.Services.Protocols;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Reflection;
using EnvDTE;
using System.Xml;

namespace Microsoft.Practices.SmartClientFactory.ValueProviders
{
    public class ProxyAddressValueProvider : ValueProvider, IAttributesConfigurable
    {
        private const string ProxyTypeKey = "ProxyType";
        private const string SelectedItemKey = "SelectedItem";

        private string _proxyType;
        private string _selectedItem;

        public override bool OnBeginRecipe(object currentValue, out object newValue)
        {
            newValue = null;
            if (currentValue == null)
            {
                Type proxyType = ExpressionEvaluationHelper.EvaluateExpression((IDictionaryService)GetService(typeof(IDictionaryService)),
                                                                _proxyType) as Type;

                if (proxyType != null)
                {
                    ProjectItem selectedItem = ExpressionEvaluationHelper.EvaluateExpression((IDictionaryService)GetService(typeof(IDictionaryService)),
                                                                _selectedItem) as ProjectItem;
                    XmlDocument mapFile = null;
                    if (selectedItem != null
                        && selectedItem.get_FileNames(0) !=null
                        && selectedItem.get_FileNames(0).ToLower().EndsWith(".map"))
                    {
                        mapFile = new XmlDocument();
                        mapFile.Load(selectedItem.get_FileNames(0));
                    }
                    newValue = GetProxyAddress(proxyType,mapFile);
                    return true;
                }
            }
            return false;
        }

        private string GetProxyAddress(Type proxyType, XmlDocument mapFile)
        {
            ProxyTechnology proxyTech=ProxyFactoryHelper.GetProxyTechnology(proxyType);
            if (proxyTech == ProxyTechnology.Asmx)
            {
                SoapHttpClientProtocol proxy = (SoapHttpClientProtocol)Activator.CreateInstance(proxyType);
                if (proxy != null)
                {
                    return proxy.Url;
                }
            }
            else if (proxyTech == ProxyTechnology.Wcf)
            {
                return GetAddresFromMapFile(mapFile);
            }
            return string.Empty;
        }

        private string GetAddresFromMapFile(XmlDocument mapFile)
        {
            if (mapFile != null)
            {
                XmlNode endpointNode = mapFile.SelectSingleNode("//EndPoints/EndPoint");
                if (endpointNode != null)
                {
                    XmlAttribute addressAtt = endpointNode.Attributes["Address"];
                    if (addressAtt != null)
                    {
                        return addressAtt.Value;
                    }
                }
            }
            return string.Empty;
        }

        public void Configure(StringDictionary attributes)
        {
            _proxyType = attributes[ProxyTypeKey];
            _selectedItem = attributes[SelectedItemKey];
        }
    }
}
