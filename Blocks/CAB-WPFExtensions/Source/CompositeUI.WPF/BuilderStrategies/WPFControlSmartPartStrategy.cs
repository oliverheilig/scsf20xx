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
using Microsoft.Practices.ObjectBuilder;
using System.Windows;
using System.Collections;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace Microsoft.Practices.CompositeUI.WPF.BuilderStrategies
{
    /// <summary>
    /// A <see cref="BuilderStrategy"/> that walks the control containment chain looking for child controls that are 
    /// either smart parts, placeholders or workspaces, so that they all get 
    /// added to the <see cref="WorkItem"/>.
    /// </summary>
    public class WPFControlSmartPartStrategy : BuilderStrategy
    {
        private WorkItem GetWorkItem(IReadableLocator locator)
        {
            return locator.Get<WorkItem>(new DependencyResolutionLocatorKey(typeof(WorkItem), null));
        }

        /// <summary>
        /// Walks the control hierarchy and adds the relevant elements to the <see cref="WorkItem"/>.
        /// </summary>
        public override object BuildUp(IBuilderContext context, Type t, object existing, string id)
        {
            if (existing is System.Windows.FrameworkElement)
                AddControlHierarchy(GetWorkItem(context.Locator), existing as System.Windows.FrameworkElement);

            return base.BuildUp(context, t, existing, id);
        }

        /// <summary>
        /// Walks the control hierarchy removing the relevant elements from the <see cref="WorkItem"/>.
        /// </summary>
        public override object TearDown(IBuilderContext context, object item)
        {
            if (item is FrameworkElement)
                RemoveControlHierarchy(GetWorkItem(context.Locator), item as FrameworkElement);

            return base.TearDown(context, item);
        }

        private void AddControlHierarchy(WorkItem workItem, FrameworkElement control)
        {
            ReplaceIfPlaceHolder(workItem, control);

            IEnumerator enumerator = LogicalTreeHelper.GetChildren(control).GetEnumerator();
            while (enumerator.MoveNext())
            {
                FrameworkElement child = enumerator.Current as FrameworkElement;
                if (child != null)
                {
                    if (AddControlToWorkItem(workItem, child) == false)
                        AddControlHierarchy(workItem, child);
                }
            }
        }

        private void RemoveControlHierarchy(WorkItem workItem, FrameworkElement control)
        {
            if (control != null)
                RemoveNestedControls(workItem, control);
        }

        private bool AddControlToWorkItem(WorkItem workItem, FrameworkElement control)
        {
            if (ShouldAddControlToWorkItem(workItem, control))
            {
                if (control.Name.Length != 0)
                    workItem.Items.Add(control, control.Name);
                else
                    workItem.Items.Add(control);

                return true;
            }

            return false;
        }

        private bool ShouldAddControlToWorkItem(WorkItem workItem, FrameworkElement control)
        {
            return !workItem.Items.ContainsObject(control) && (IsSmartPart(control) || IsWorkspace(control) || IsPlaceholder(control));
        }

        private bool IsPlaceholder(FrameworkElement control)
        {
            return (control is ISmartPartPlaceholder);
        }

        private bool IsSmartPart(FrameworkElement control)
        {
            return (control.GetType().GetCustomAttributes(typeof(SmartPartAttribute), true).Length > 0);
        }

        private bool IsWorkspace(FrameworkElement control)
        {
            return (control is IWorkspace);
        }

        private void RemoveNestedControls(WorkItem workItem, FrameworkElement control)
        {

            IEnumerator enumerator = LogicalTreeHelper.GetChildren(control).GetEnumerator();
            while (enumerator.MoveNext())
            {
                FrameworkElement child = enumerator.Current as FrameworkElement;
                if (child != null)
                {
                    workItem.Items.Remove(child);
                    RemoveNestedControls(workItem, child);
                }
            }
        }

        private void ReplaceIfPlaceHolder(WorkItem workItem, FrameworkElement control)
        {
            ISmartPartPlaceholder placeholder = control as ISmartPartPlaceholder;

            if (placeholder != null)
            {
                FrameworkElement replacement = workItem.Items.Get<FrameworkElement>(placeholder.SmartPartName);

                if (replacement != null)
                    placeholder.SmartPart = replacement;
            }
        }
    }
}