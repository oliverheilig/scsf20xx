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
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.WinForms;
using Microsoft.Practices.CompositeUI.WPF.BuilderStrategies;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI.WPF;

namespace Microsoft.Practices.CompositeUI.WPF
{
    /// <summary>
    /// Extends <see cref="FormShellApplication{TWorkItem,TShell}"/> to support an application which
    /// uses a Windows Forms <see cref="Form"/> as its shell and WPF <see cref="System.Windows.Controls"/> views.
    /// </summary>
    /// <remarks>
    /// Adds the <see cref="WPFUIElementAdapter"/> service to the RootWorkItem, and includes the
    /// <see cref="WPFControlSmartPartStrategy"/> builder strategy, which are needed 
    /// by the <see cref="ElementHostWorkspace{TSmartPart, TSmartPartInfo}"/> class.
    /// </remarks>
    /// <typeparam name="TWorkItem">The type of the root application work item.</typeparam>
    /// <typeparam name="TShell">The type for the shell to use.</typeparam>
    public abstract class WPFFormShellApplication<TWorkItem, TShell> : FormShellApplication<TWorkItem, TShell>
        where TWorkItem : WorkItem, new()
        where TShell : Form
    {
        /// <summary>
        /// Adds the <see cref="WPFControlSmartPartStrategy"/> Builder Strategy.
        /// </summary>
        protected override void AddBuilderStrategies(Microsoft.Practices.ObjectBuilder.Builder builder)
        {
            base.AddBuilderStrategies(builder);
            builder.Strategies.AddNew<WPFControlSmartPartStrategy>(BuilderStage.Initialization);
        }

        /// <summary>
        /// Adds the <see cref="WPFUIElementAdapter"/> service to the RootWorkItem.
        /// </summary>
        protected override void AddServices()
        {
            base.AddServices();
            RootWorkItem.Services.AddNew<WPFUIElementAdapter, IWPFUIElementAdapter>();
        }
    }
}
