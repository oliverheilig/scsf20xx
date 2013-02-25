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
// The EntityTranslatorService class is a service that provides a registry of
// translators and translation services between two classes. The user must implement
// the translators and register them with the service.
// 
// For more information see: 
// ms-help://MS.VSCC.v90/MS.VSIPCC.v90/ms.practices.scsf.2008apr/SCSF/html/03-01-090-How_to_Translate_Between_Business_Entities_and_Service_Entities.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using System;

namespace QuickStart.Infrastructure.Interface.Services
{
    public interface IEntityTranslatorService
    {
        bool CanTranslate(Type targetType, Type sourceType);
        bool CanTranslate<TTarget, TSource>();
        object Translate(Type targetType, object source);
        TTarget Translate<TTarget>(object source);
        void RegisterEntityTranslator(IEntityTranslator translator);
        void RemoveEntityTranslator(IEntityTranslator translator);
    }
}
