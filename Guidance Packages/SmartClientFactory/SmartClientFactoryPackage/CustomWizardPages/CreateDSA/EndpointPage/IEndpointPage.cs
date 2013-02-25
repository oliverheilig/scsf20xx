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
using Microsoft.Practices.RecipeFramework.Extensions.DteWrapper;
using Microsoft.Practices.RecipeFramework.Extensions;
using System.Reflection;

namespace Microsoft.Practices.SmartClientFactory.CustomWizardPages
{
    public interface IEndpointPage
    {
        void ShowEndpoint(string endpoint);
        void ShowProxyType(Type proxyType);
        void ShowMethods(List<MethodInfo> Methods, List<MethodInfo> selectedMethods);
        void ShowShowDocumentation(bool showDocumentation);
        void SetLanguage(string language);
        void ShowNotBuildPanel(bool built, bool existsProxyClass, string proxyTypeName);
        void RefreshSolutionPreview(string disconnectedAgentsFolder, 
            string proxyFolder, 
            string agentFileName,
            string agentCallbackFileName,
            string agentCallbackBaseFileName,
            IProjectModel activeModuleProject);
        void ShowExpiration(string expiration);
        void ShowMaxRetries(string maxRetries);
        void ShowStamps(string stamps);
        void ShowTag(string tag);
        void ShowProxyFactoryTypeFullName(string proxyFactoryTypeFullName);

        event EventHandler<EventArgs> EndpointChanged;
        event EventHandler<EventArgs> ProxyTypeChanged;
        event EventHandler<EventArgs> MethodsChanged;
        event EventHandler<EventArgs> ExpirationChanged;
        event EventHandler<EventArgs> MaxRetriesChanged;
        event EventHandler<EventArgs> StampsChanged;
        event EventHandler<EventArgs> TagChanged;
        event EventHandler<EventArgs> ShowDocumentationChanged;
        event EventHandler<EventArgs<bool>> RequestingValidation;

        string Endpoint { get; }
        string ProxyTypeName { get; }
        List<MethodInfo> Methods { get;}
        string Language { get; }
        string Expiration { get; }
        string MaxRetries { get; }
        string Stamps { get; }
        string TagValue { get; }        bool ShowDocumentation { get; }
    }
}
