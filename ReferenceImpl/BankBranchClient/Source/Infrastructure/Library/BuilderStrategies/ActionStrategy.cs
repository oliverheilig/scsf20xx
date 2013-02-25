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
using System.Reflection;
using GlobalBank.Infrastructure.Interface;
using GlobalBank.Infrastructure.Interface.Services;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;

namespace GlobalBank.Infrastructure.Library.BuilderStrategies
{
    public class ActionStrategy : BuilderStrategy
    {
        public override object BuildUp(IBuilderContext context, Type typeToBuild, object existing, string idToBuild)
        {
            WorkItem workItem = GetWorkItem(context, existing);

            if (workItem != null)
            {
                IActionCatalogService actionCatalog = workItem.Services.Get<IActionCatalogService>();
                if (actionCatalog != null)
                {
                    Type targetType = existing.GetType();

                    foreach (MethodInfo methodInfo in targetType.GetMethods())
                        RegisterActionImplementation(context, actionCatalog, existing, idToBuild, methodInfo);
                }
            }

            return base.BuildUp(context, typeToBuild, existing, idToBuild);
        }

        public override object TearDown(IBuilderContext context, object item)
        {
            WorkItem workItem = GetWorkItem(context, item);

            if (workItem != null)
            {
                IActionCatalogService actionCatalog = workItem.Services.Get<IActionCatalogService>();
                if (actionCatalog != null)
                {
                    Type targetType = item.GetType();

                    foreach (MethodInfo methodInfo in targetType.GetMethods())
                        RemoveActionImplementation(context, actionCatalog, item, methodInfo);
                }
            }
            return base.TearDown(context, item);
        }

        private void RemoveActionImplementation(IBuilderContext context, IActionCatalogService catalog, object existing, MethodInfo methodInfo)
        {
            foreach (ActionAttribute attr in methodInfo.GetCustomAttributes(typeof(ActionAttribute), true))
            {
                catalog.RemoveActionImplementation(attr.ActionName);

                TraceTearDown(context, existing, "Action implementation removed for action {0}, for the method {1} on the type {2}.", attr.ActionName, methodInfo.Name, existing.GetType().Name);
            }
        }

        private void RegisterActionImplementation(IBuilderContext context, IActionCatalogService catalog, object existing, string idToBuild, MethodInfo methodInfo)
        {
            foreach (ActionAttribute attr in methodInfo.GetCustomAttributes(typeof(ActionAttribute), true))
            {
                ActionDelegate actionDelegate = (ActionDelegate)Delegate.CreateDelegate(typeof(ActionDelegate), existing, methodInfo);
                catalog.RegisterActionImplementation(attr.ActionName, actionDelegate);

                // TODO: Add to resources
                TraceBuildUp(context, existing.GetType(), idToBuild, "Action implementation built for action {0}, for the method {1} on the type {2}.", attr.ActionName, methodInfo.Name, existing.GetType().Name);
            }
        }

        private WorkItem GetWorkItem(IBuilderContext context, object item)
        {
            if (item is WorkItem)
                return item as WorkItem;

            return context.Locator.Get<WorkItem>(new DependencyResolutionLocatorKey(typeof(WorkItem), null));
        }
    }
}
