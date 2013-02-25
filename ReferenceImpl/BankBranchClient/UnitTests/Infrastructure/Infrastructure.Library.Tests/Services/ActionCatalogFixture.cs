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
using GlobalBank.Infrastructure.Interface.Services;
using Microsoft.Practices.CompositeUI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GlobalBank.Infrastructure.Library.Services;

namespace GlobalBank.Infrastructure.Library.Tests.Services
{
    [TestClass]
    public class ActionCatalogFixture
    {
        private ActionCatalogService catalog;

        [TestInitialize]
        public void Initialize()
        {
            catalog = new ActionCatalogService();
        }

        [TestMethod]
        public void DefaultBehaviorIsAllowExecution()
        {
            Assert.IsTrue(catalog.CanExecute("Action1", null, null, null));
        }

        [TestMethod]
        public void RegisterTrueActionCondition()
        {
            catalog.RegisterSpecificCondition("Action1", new MockCondition1());

            Assert.IsTrue(catalog.CanExecute("Action1", null, null, null));
        }

        [TestMethod]
        public void RegisterGlobalActionCondition()
        {
            catalog.RegisterGeneralCondition(new MockCondition1());
            catalog.RegisterSpecificCondition("Action2", new MockCondition2());

            Assert.IsTrue(catalog.CanExecute("Action1", null, null, null));
            Assert.IsFalse(catalog.CanExecute("Action2", null, null, null));
        }

        [TestMethod]
        public void ComplexPipelineTest()
        {
            MockCondition1 ev1 = new MockCondition1(); // return true
            MockCondition2 ev2 = new MockCondition2(); // return false
            MockCondition3 ev3 = new MockCondition3(); // return true
            MockCondition4 ev4 = new MockCondition4(); // return true

            catalog.RegisterGeneralCondition(ev1);
            catalog.RegisterSpecificCondition("Action1", ev3);
            catalog.RegisterSpecificCondition("Action2", ev2);
            catalog.RegisterGeneralCondition(ev4);

            Assert.IsTrue(catalog.CanExecute("Action1", null, null, null)); // ev1, ev3
            Assert.IsFalse(catalog.CanExecute("Action2", null, null, null));  // ev1, ev2, ev4 
        }

        [TestMethod]
        public void RegisterFalseActionCondition()
        {
            catalog.RegisterSpecificCondition("Action1", new MockCondition2());

            Assert.IsFalse(catalog.CanExecute("Action1"));
        }

        [TestMethod]
        [ExpectedException(typeof(ActionCatalogException))]
        public void FailsWhenRegisteringDuplicateActionConditionsType()
        {
            catalog.RegisterSpecificCondition("Action1", new MockCondition1());
            catalog.RegisterSpecificCondition("Action1", new MockCondition1());
        }

        [TestMethod]
        public void CanExecuteMultipleActionConditionTypes()
        {
            MockCondition1 ev1 = new MockCondition1();
            MockCondition2 ev2 = new MockCondition2();

            catalog.RegisterSpecificCondition("Action1", ev1);
            catalog.RegisterSpecificCondition("Action1", ev2);

            Assert.IsFalse(catalog.CanExecute("Action1"));
            Assert.IsTrue(ev1.Executed);
            Assert.IsTrue(ev2.Executed);
        }

        [TestMethod]
        public void CanRemoveActionCondition()
        {
            MockCondition1 ev1 = new MockCondition1();
            MockCondition2 ev2 = new MockCondition2();

            catalog.RegisterSpecificCondition("Action1", ev1);
            catalog.RegisterSpecificCondition("Action1", ev2);
            catalog.RemoveSpecificCondition("Action1", ev2);

            Assert.IsTrue(catalog.CanExecute("Action1"));
            Assert.IsTrue(ev1.Executed);
            Assert.IsFalse(ev2.Executed);
        }

        [TestMethod]
        public void CanRemoveGlobalActionCondition()
        {
            MockCondition1 ev1 = new MockCondition1();
            MockCondition2 ev2 = new MockCondition2();

            catalog.RegisterGeneralCondition(ev1);
            catalog.RegisterGeneralCondition(ev2);
            catalog.RemoveGeneralCondition(ev2);

            Assert.IsTrue(catalog.CanExecute("Action1"));
            Assert.IsTrue(ev1.Executed);
            Assert.IsFalse(ev2.Executed);
        }

        [TestMethod]
        public void ExecuteActionImplementation()
        {
            object caller = null;
            object target = null;

            catalog.RegisterSpecificCondition("Action1", new MockCondition1());
            catalog.RegisterActionImplementation("Action1", delegate(object caller1, object target1)
            {
                caller = caller1;
                target = target1;
            });

            object caller2 = new object();
            object target2 = new object();
            catalog.Execute("Action1", new WorkItem(), caller2, target2);

            Assert.AreSame(caller2, caller);
            Assert.AreSame(target2, target);
        }

        [TestMethod]
        public void ActionImplementationIsNotExecutedWhenEvalatedToFalse()
        {
            bool called = false;
            catalog.RegisterSpecificCondition("Action1", new MockCondition2());
            catalog.RegisterActionImplementation("Action1", delegate(object caller1, object target1)
            {
                called = true;
            });

            catalog.Execute("Action1", new WorkItem(), new object(), new object());

            Assert.IsFalse(called);
        }

        [TestMethod]
        public void RegisterActionImplementation()
        {
            bool called = false;
            catalog.RegisterActionImplementation("Action1", delegate(object caller, object target)
            {
                called = true;
            });
            catalog.Execute("Action1", new WorkItem(), new object(), new object());
            Assert.IsTrue(called);
        }

        [TestMethod]
        public void RemoveActionImplementation()
        {
            bool called = false;
            ActionDelegate actionDelegate = delegate(object caller, object target)
            {
                called = true;
            };

            catalog.RegisterActionImplementation("Action1", actionDelegate);
            catalog.RemoveActionImplementation("Action1");

            catalog.Execute("Action1", new WorkItem(), new object(), new object());
            Assert.IsFalse(called);
        }

    }

    class MockCondition1 : IActionCondition
    {
        public bool Executed = false;
        public bool CanExecute(string action, WorkItem context, object caller, object target)
        {
            Executed = true;
            return true;
        }
    }

    class MockCondition2 : IActionCondition
    {
        public bool Executed = false;
        public bool CanExecute(string action, WorkItem context, object caller, object target)
        {
            Executed = true;
            return false;
        }
    }

    class MockCondition3 : IActionCondition
    {
        public bool Executed = false;
        public bool CanExecute(string action, WorkItem context, object caller, object target)
        {
            Executed = true;
            return true;
        }
    }

    class MockCondition4 : IActionCondition
    {
        public bool Executed = false;
        public bool CanExecute(string action, WorkItem context, object caller, object target)
        {
            Executed = true;
            return true;
        }
    }
}

