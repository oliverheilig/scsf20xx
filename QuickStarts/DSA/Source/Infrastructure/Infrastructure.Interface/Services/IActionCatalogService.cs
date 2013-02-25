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
//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// The IActionCatalogService defines the ability to conditionally execute code based upon 
// aspects of a program that can change at run time 
//
// For more information see: 
// ms-help://MS.VSCC.v90/MS.VSIPCC.v90/ms.practices.scsf.2008apr/SCSF/html/03-01-140-How_to_Use_the_Action_Catalog.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using Microsoft.Practices.CompositeUI;

namespace QuickStart.Infrastructure.Interface.Services
{
    public delegate void ActionDelegate(object caller, object target);

    public interface IActionCatalogService
    {
        bool CanExecute(string action, WorkItem context, object caller, object target);
        bool CanExecute(string action);
        void Execute(string action, WorkItem context, object caller, object target);

        void RegisterSpecificCondition(string action, IActionCondition actionCondition);
        void RegisterGeneralCondition(IActionCondition actionCondition);
        void RemoveSpecificCondition(string action, IActionCondition actionCondition);
        void RemoveGeneralCondition(IActionCondition actionCondition);

        void RemoveActionImplementation(string action);
        void RegisterActionImplementation(string action, ActionDelegate actionDelegate);
    }
}
