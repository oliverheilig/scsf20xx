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
using Microsoft.Practices.CompositeUI.Utility;

namespace Microsoft.Practices.CompositeUI.WPF.Tests
{
    public class MockBuilderContext : IBuilderContext
    {
        IReadWriteLocator locator = new Locator();
        BuilderStrategyChain chain = new BuilderStrategyChain();
        PolicyList policies = new PolicyList();

        public MockBuilderContext(params IBuilderStrategy[] strategies)
        {
            Guard.ArgumentNotNull(strategies, "strategies");

            foreach (IBuilderStrategy strategy in strategies)
                chain.Add(strategy);
        }

        public IBuilderStrategy HeadOfChain
        {
            get { return chain.Head; }
        }

        public IBuilderStrategy GetNextInChain(IBuilderStrategy currentStrategy)
        {
            return chain.GetNext(currentStrategy);
        }

        public IReadWriteLocator Locator
        {
            get { return locator; }
        }

        public PolicyList Policies
        {
            get { return policies; }
        }
    }
}
