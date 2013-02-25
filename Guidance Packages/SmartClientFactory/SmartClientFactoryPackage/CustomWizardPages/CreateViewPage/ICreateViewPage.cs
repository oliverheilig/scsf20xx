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

namespace Microsoft.Practices.SmartClientFactory.CustomWizardPages
{
    public interface ICreateViewPage
    {
        void ShowViewName(string viewName);

        event EventHandler<EventArgs> ViewNameChanged;
        event EventHandler<EventArgs<bool>> RequestingValidation;
        event EventHandler<EventArgs> CreateViewFolderChanged;
        event EventHandler<EventArgs> ShowDocumentationChanged;
        
        string ViewName { get; }
        bool CreateViewFolder { get; }
        bool ShowDocumentation { get; }
        string Language { get; }
        void SetLanguage(string language);
        void RefreshSolutionPreview(IProjectModel activeModuleProject, IProjectItemModel activeProjectItem, bool isWpfView);
        bool TestProjectExists { set; }

        void ShowValidationErrorMessage(string errorMessage);
    }
}
