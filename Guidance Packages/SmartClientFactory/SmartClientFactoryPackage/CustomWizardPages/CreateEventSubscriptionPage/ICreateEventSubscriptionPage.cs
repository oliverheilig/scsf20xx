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
using Microsoft.Practices.RecipeFramework.Extensions;
using Microsoft.Practices.SmartClientFactory.CABCompatibleTypes;

namespace Microsoft.Practices.SmartClientFactory.CustomWizardPages
{
    public interface ICreateEventSubscriptionPage
    {
        event EventHandler<EventArgs<bool>> RequestingValidation;
        event EventHandler<EventArgs> EventTopicChanged;
        event EventHandler<EventArgs> ThreadingOptionChanged;
        event EventHandler<EventArgs> EventArgsChanged;
        event EventHandler<EventArgs> ShowDocumentationChanged;

        string EventTopic { get; }
        ThreadOption ThreadingOption { get; }
        string EventArgs { get; }
        bool ShowDocumentation { get; }

        void ShowShowDocumentation(bool showDocumentation);
        void ShowEventArgs(string eventArgs);
        void ShowThreadingOption(List<ThreadOption> threadingOptions, ThreadOption selected);
        void ShowEventTopics(List<string> eventTopics);
    }
}
