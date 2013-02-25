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

namespace GlobalBank.Infrastructure.Interface.Services
{
    public interface ICacheService
    {
        int Count { get; }

        object this[string key] { get; }

        void Add(string key, object value);

        void Add(string key, object value, DateTime absolute);

        void Add(string key, object value, TimeSpan relative);       

        bool Contains(string key);

        void Dispose();

        void Flush();

        object GetData(string key);

        void Remove(string key);
    }
}
