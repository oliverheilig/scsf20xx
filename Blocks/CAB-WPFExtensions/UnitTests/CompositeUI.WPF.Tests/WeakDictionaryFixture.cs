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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.CompositeUI.WPF.Tests
{
    [TestClass]
    public class WeakDictionaryFixture
    {
        // Added a TestCleanup method to deal with the fact that the code was throwing an InvalidComObjectException
        // with the information "COM object that has been separated from its underlying RCW cannot be used."
        // Fix is based on this bug logged on Connect.Microsoft.Com:
        // http://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=318333
        [TestCleanup]
        public void CleanUp()
        {
            System.Windows.Threading.Dispatcher.CurrentDispatcher.InvokeShutdown();
        }
        
        [TestMethod]
        public void CanRegisterObjectAndFindItByID()
        {
            object o = new object();
            WeakDictionary<object, object> dict = new WeakDictionary<object, object>();

            dict.Add("foo", o);
            Assert.IsNotNull(dict["foo"]);
            Assert.AreSame(o, dict["foo"]);
        }

        [TestMethod]
        public void CanRegisterTwoObjectsAndGetThemBoth()
        {
            object o1 = new object();
            object o2 = new object();
            WeakDictionary<object, object> dict = new WeakDictionary<object, object>();

            dict.Add("foo1", o1);
            dict.Add("foo2", o2);

            Assert.AreSame(o1, dict["foo1"]);
            Assert.AreSame(o2, dict["foo2"]);
        }

        [TestMethod]
        public void KeyCanBeOfArbitraryType()
        {
            object oKey = new object();
            object oVal = new object();
            WeakDictionary<object, object> dict = new WeakDictionary<object, object>();

            dict.Add(oKey, oVal);

            Assert.AreSame(oVal, dict[oKey]);
        }

        [TestMethod]
        public void CanAddSameObjectTwiceWithDifferentKeys()
        {
            object o = new object();
            WeakDictionary<object, object> dict = new WeakDictionary<object, object>();

            dict.Add("foo1", o);
            dict.Add("foo2", o);

            Assert.AreSame(dict["foo1"], dict["foo2"]);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void AskingForAKeyThatDoesntExistThrows()
        {
            WeakDictionary<object, object> dict = new WeakDictionary<object, object>();
            object unused = dict["foo"];
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void CanRemoveAnObjectThatWasAlreadyAdded()
        {
            object o = new object();
            WeakDictionary<object, object> dict = new WeakDictionary<object, object>();

            dict.Add("foo", o);
            dict.Remove("foo");
            object unused = dict["foo"];
        }

        [TestMethod]
        public void RemovingAKeyOfOneObjectDoesNotAffectOtherKeysForSameObject()
        {
            object o = new object();
            WeakDictionary<object, object> dict = new WeakDictionary<object, object>();

            dict.Add("foo1", o);
            dict.Add("foo2", o);
            dict.Remove("foo1");

            Assert.AreSame(o, dict["foo2"]);
        }

        [TestMethod]
        public void RemovingAKeyDoesNotAffectOtherKeys()
        {
            object o1 = new object();
            object o2 = new object();
            WeakDictionary<object, object> dict = new WeakDictionary<object, object>();

            dict.Add("foo1", o1);
            dict.Add("foo2", o2);
            dict.Remove("foo1");

            Assert.AreSame(o2, dict["foo2"]);
        }

        [TestMethod]
        public void RemovingANonExistantKeyDoesntThrow()
        {
            WeakDictionary<object, object> dict = new WeakDictionary<object, object>();
            dict.Remove("foo1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddingToSameKeyTwiceAlwaysThrows()
        {
            object o = new object();
            WeakDictionary<object, object> dict = new WeakDictionary<object, object>();

            dict.Add("foo1", o);
            dict.Add("foo1", o);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void RegistrationDoesNotPreventGarbageCollection()
        {
            WeakDictionary<object, object> dict = new WeakDictionary<object, object>();
            dict.Add("foo", new object());
            GC.Collect();
            object unused = dict["foo"];
        }

        [TestMethod]
        public void KeyCanBeGarbageCollected()
        {
            WeakDictionary<object, object> dict = new WeakDictionary<object, object>();
            dict.Add(new object(), "foo");
            GC.Collect();
            Assert.IsTrue(dict.Count == 0);
        }

        [TestMethod]
        public void NullIsAValidValue()
        {
            WeakDictionary<object, object> dict = new WeakDictionary<object, object>();
            dict.Add("foo", null);
            Assert.IsNull(dict["foo"]);
        }

        [TestMethod]
        public void CanFindOutIfContainsAKey()
        {
            WeakDictionary<object, object> dict = new WeakDictionary<object, object>();

            dict.Add("foo", null);
            Assert.IsTrue(dict.ContainsKey("foo"));
            Assert.IsFalse(dict.ContainsKey("foo2"));
        }

        //[TestMethod]
        //[Ignore] // Enumeration not supported.
        //public void CanEnumerate()
        //{
        //    object o1 = new object();
        //    object o2 = new object();
        //    WeakDictionary<object, object> dict = new WeakDictionary<object, object>();

        //    dict.Add("foo1", o1);
        //    dict.Add("foo2", o2);

        //    foreach (KeyValuePair<object, object> kvp in dict)
        //    {
        //        Assert.IsNotNull(kvp);
        //        Assert.IsNotNull(kvp.Key);
        //        Assert.IsNotNull(kvp.Value);
        //    }
        //}

        [TestMethod]
        public void CountReturnsNumberOfKeysWithLiveValues()
        {
            object o = new object();
            WeakDictionary<object, object> dict = new WeakDictionary<object, object>();

            dict.Add("foo1", o);
            dict.Add("foo2", o);

            Assert.AreEqual(2, dict.Count);

            o = null;
            GC.Collect();

            Assert.AreEqual(0, dict.Count);
        }

        [TestMethod]
        public void CountReturnsNumberOfKeysWithLive()
        {
            object o = new object();
            WeakDictionary<object, object> dict = new WeakDictionary<object, object>();

            dict.Add(o, "foo");

            Assert.AreEqual(1, dict.Count);

            o = null;
            GC.Collect();

            Assert.AreEqual(0, dict.Count);
        }

        [TestMethod]
        public void CanAddItemAfterPreviousItemIsCollected()
        {
            WeakDictionary<object, object> dict = new WeakDictionary<object, object>();
            dict.Add("foo", new object());

            GC.Collect();

            dict.Add("foo", new object());
        }

        [TestMethod]
        public void CanInformCountainedValue()
        {
            WeakDictionary<object, object> dict = new WeakDictionary<object, object>();
            object o = new object();
            dict.Add("foo1", "bar");
            dict.Add("foo2", o);

            Assert.IsTrue(dict.ContainsValue("bar"));
            Assert.IsTrue(dict.ContainsValue(o));
        }
    }
}
