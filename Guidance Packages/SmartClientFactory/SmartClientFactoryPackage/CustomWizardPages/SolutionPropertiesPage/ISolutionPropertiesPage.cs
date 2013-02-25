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
using Microsoft.Practices.RecipeFramework.Extensions;

namespace Microsoft.Practices.SmartClientFactory.CustomWizardPages
{
    public interface ISolutionPropertiesPage
    {
        event EventHandler<EventArgs> SupportLibrariesPathChanging;
        event EventHandler<EventArgs> RootNamespaceChanging;
        event EventHandler<EventArgs<bool>> RequestingValidation;
        event EventHandler<EventArgs> ShowDocumentationChanging;
        event EventHandler<EventArgs> CreateShellLayoutModuleChanging;
        event EventHandler<EventArgs> SupportWPFViewsChanging;

        string SupportLibrariesPath { get; }
        string RootNamespace { get; }
        void ShowSupportLibraries(string[] libraries, bool[] missing);
        void ShowSupportLibrariesPath(string path);
        void ShowRootNamespace(string rootNamespace);
        void ShowShowDocumentation(bool showDocumentation);

        bool ShowDocumentation { get; }
        bool CreateShellLayoutModule { get; }
        bool SupportWPFViews { get; }
        void SetLanguage(string language);
        string Language { get; }
        void RefreshSolutionPreview();
    }
}