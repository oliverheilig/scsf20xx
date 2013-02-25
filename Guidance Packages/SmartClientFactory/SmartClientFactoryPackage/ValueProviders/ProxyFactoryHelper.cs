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
using System.ServiceModel;
using System.Web.Services.Protocols;
using Microsoft.Practices.SmartClientFactory.CustomWizardPages;

namespace Microsoft.Practices.SmartClientFactory.ValueProviders
{
    public enum ProxyTechnology
    {
        Asmx, Wcf, Object
    }
    public class ProxyFactoryHelper
    {

        private const string genericFormatCS = "{0}<{1}>";
        private const string genericFormatVB = "{0}(Of {1})";
		public const string WebServiceProxyFactoryName = "Microsoft.Practices.SmartClient.DisconnectedAgent.WebServiceProxyFactory";
		public const string WCFProxyFactoryGenericName = "Microsoft.Practices.SmartClient.DisconnectedAgent.WCFProxyFactory`1";
		public const string ObjectProxyFactoryName = "Microsoft.Practices.SmartClient.DisconnectedAgent.ObjectProxyFactory";

        public static ProxyTechnology GetProxyTechnology(Type proxyType)
        {
            if (typeof(SoapHttpClientProtocol).IsAssignableFrom(proxyType))
            {
                return ProxyTechnology.Asmx;
            }
            else
            {
                Type genericImplementation = proxyType.BaseType;
                while (genericImplementation != typeof(Object))
                {
                    if (genericImplementation.IsGenericType
                        && typeof(ClientBase<>).IsAssignableFrom(genericImplementation.GetGenericTypeDefinition()))
                    {
                        return ProxyTechnology.Wcf;
                    }
                    genericImplementation = genericImplementation.BaseType;
                }
            }
            return ProxyTechnology.Object;
        }
        
        public static object GetProxyFactory(Type proxyType, string language)
        {
            object newValue=null;

            if (proxyType != null)
            {
                ProxyTechnology proxyTech=GetProxyTechnology(proxyType);
                if (proxyTech == ProxyTechnology.Asmx)
                {
					newValue = WebServiceProxyFactoryName;
                }
                else if (proxyTech == ProxyTechnology.Wcf)
                {
                    Type genericImplementation = proxyType.BaseType;
                    while (genericImplementation != typeof(Object))
                    {
                        if (genericImplementation.IsGenericType
                            && typeof(ClientBase<>).IsAssignableFrom(genericImplementation.GetGenericTypeDefinition()))
                        {
                            string wrappedServiceTypeName = string.Empty;
                            Type[] generigArguments = genericImplementation.GetGenericArguments();
                            if (generigArguments.Length == 1)
                            {
                                wrappedServiceTypeName = generigArguments[0].FullName;
                            }

							string clrNotationFactoryTypeName = WCFProxyFactoryGenericName;
                            string genericFactoryTypeName = NotationHelper.RemoveTrailingCLRChars(clrNotationFactoryTypeName);
                            newValue = string.Format(GetSelectedLanguageGenericFormat(language), genericFactoryTypeName, wrappedServiceTypeName);
                            break;
                        }
                        genericImplementation = genericImplementation.BaseType;
                    }
                }

                if (newValue == null)
                {
					newValue = ObjectProxyFactoryName;
                }
            }
            return newValue;
        }

        private static string GetSelectedLanguageGenericFormat(string language)
        {
            switch (language)
            {
                case "CS":
                case "C#":
                    return genericFormatCS;
                case "VB":
                    return genericFormatVB;
                default:
                    return genericFormatCS; ;
            }
        }

    }
}
