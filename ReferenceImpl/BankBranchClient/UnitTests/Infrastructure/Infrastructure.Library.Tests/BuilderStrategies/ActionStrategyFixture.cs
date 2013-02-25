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
using GlobalBank.Infrastructure.Interface;
using GlobalBank.Infrastructure.Interface.Services;
using GlobalBank.Infrastructure.Library.BuilderStrategies;
using GlobalBank.UnitTest.Library;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Utility;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GlobalBank.Infrastructure.Library.Tests.BuilderStrategies
{

    /// <summary>
    /// Summary description for ActionStrategyFixture
    /// </summary>
    [TestClass]
    public class ActionStrategyFixture
    {
        private ActionStrategy actionStrategy = null;
        private MockActionCatalog actionCatalog = null;
        private WorkItem workItem = null;
        MockBuilderContext builderContext = null;

        [TestInitialize]
        public void Initialize()
        {
            actionStrategy = new ActionStrategy();
            builderContext = new MockBuilderContext(actionStrategy);
            workItem = new TestableRootWorkItem();
            builderContext.Locator.Add(new DependencyResolutionLocatorKey(typeof(WorkItem), null), workItem);
            actionCatalog = workItem.Services.AddNew<MockActionCatalog, IActionCatalogService>();
        }

        [TestMethod]
        public void DiscoverActionAndRegisters()
        {
            MockActionClass actionClass = new MockActionClass();
            actionStrategy.BuildUp(builderContext, typeof(MockActionClass), actionClass, null);

            Assert.IsNotNull(actionCatalog.Action);
            Assert.AreEqual("Action1", actionCatalog.Action);
            Assert.IsNotNull(actionCatalog.ActionDelegate);
        }

        [TestMethod]
        public void DiscoverAndRemoveActionImplementation()
        {
            MockActionClass actionClass = new MockActionClass();
            actionStrategy.BuildUp(builderContext, typeof(MockActionClass), actionClass, null);

            actionCatalog.Action = null;
            actionCatalog.ActionDelegate = null;
            actionStrategy.TearDown(builderContext, actionClass);

            Assert.IsNotNull(actionCatalog.Action);
            Assert.AreEqual("Action1", actionCatalog.Action);
        }
    }

    class MockActionClass
    {
        [Action("Action1")]
        public void Action1Method(object caller, object target)
        {
        }
    }

    class MockActionCatalog : IActionCatalogService
    {
        public string Action;
        public ActionDelegate ActionDelegate;

        public bool CanExecute(string action, WorkItem context, object caller, object target)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public bool CanExecute(string action)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Execute(string action, WorkItem context, object caller, object target)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void RegisterSpecificCondition(string action, IActionCondition actionCondition)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void RegisterActionImplementation(string action, ActionDelegate actionDelegate)
        {
            Action = action;
            ActionDelegate = actionDelegate;
        }

        public void RemoveSpecificCondition(string action, IActionCondition actionCondition)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void RemoveActionImplementation(string action)
        {
            Action = action;
        }

        public void RegisterGeneralCondition(IActionCondition actionCondition)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void RemoveGeneralCondition(IActionCondition actionCondition)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    class MockBuilderContext : IBuilderContext
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
