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
using Microsoft.Practices.RecipeFramework.Extensions;
using Microsoft.Practices.RecipeFramework.Extensions.DteWrapper;
using EnvDTE;

namespace Microsoft.Practices.SmartClientFactory.CustomWizardPages
{
    public interface ICreateFoundationalModulePage
    {
        event EventHandler<EventArgs> CreateModuleInterfaceLibraryChanged;
        event EventHandler<EventArgs> ShowDocumentationChanged;
        event EventHandler<EventArgs> CreateTestProjectChanged;
        event EventHandler<EventArgs<bool>> RequestingValidation;
        event EventHandler<EventArgs> CreateLayoutModuleChanged;

        void ShowModuleNamespace(string moduleNamespace);
        void SetModuleName(string moduleName);
        void SetLanguage(string language);
        bool CreateModuleInterfaceLibrary { get; }
        bool CreateLayoutModule { get; }
        string ModuleName { get; }
        string Language { get; }
        void RefreshSolutionPreview();
        bool ShowDocumentation { get; }
        bool CreateTestProject { get; }

    }
}
