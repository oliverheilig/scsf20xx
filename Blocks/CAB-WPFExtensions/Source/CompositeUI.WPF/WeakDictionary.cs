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
using Microsoft.Practices.CompositeUI.WPF.Properties;

namespace Microsoft.Practices.CompositeUI.WPF
{
    /// <summary>
    /// Represents a dictionary which stores the keys and values as weak references instead of strong
    /// references. Null values are supported.
    /// </summary>
    /// <typeparam name="TKey">The key type</typeparam>
    /// <typeparam name="TValue">The value type</typeparam>
    public class WeakDictionary<TKey, TValue>
    {
        /// <summary>
        /// Compares strong references with WeakKeyReference targets.
        /// </summary>
        /// <remarks>
        /// Although the inner dictionary key holds WeakReferences, object is used as the key type.
        /// This allows to access the inner dictionary using the TKey strong reference.
        /// <example>
        /// _inner.Add(new WeakKeyReference(strongKey), weakValue));
        /// _inner[strongKey] == weakValue;
        /// </example>
        /// The WeakReferenceComparer is used by the inner dictionary to compare keys and 
        /// recognize weak references keys from its strong references.
        /// </remarks>
        /// <typeparam name="T">The type of the key.</typeparam>
        private class WeakReferenceComparer<T> : IEqualityComparer<object>
        {
            public new bool Equals(object x, object y)
            {
                bool xIsDead, yIsDead;
                T first = GetTarget(x, out xIsDead);
                T second = GetTarget(y, out yIsDead);

                if (xIsDead)
                    return yIsDead ? x == y : false;

                if (yIsDead)
                    return false;

                return first.Equals(second);
            }

            public int GetHashCode(object obj)
            {
                WeakKeyReference weakKey = obj as WeakKeyReference;
                if (weakKey != null)
                {
                    return weakKey.HashCode;
                }
                return obj.GetHashCode();
            }

            private static T GetTarget(object obj, out bool isDead)
            {
                WeakKeyReference wref = obj as WeakKeyReference;
                T target;
                if (wref != null)
                {
                    target = (T)wref.Target;
                    isDead = !wref.IsAlive;
                }
                else
                {
                    target = (T)obj;
                    isDead = false;
                }
                return target;
            }
        }

        /// <summary>
        /// Mantains the HashCode to be used when the key reference is no longer alive.
        /// </summary>
        private class WeakKeyReference : WeakReference
        {
            public WeakKeyReference(object key)
                : base(key)
            {
                this._hashCode = key.GetHashCode();
            }

            private int _hashCode;

            public int HashCode
            {
                get { return _hashCode; }
                set { _hashCode = value; }
            }
        }

        private class NullObject
        {
            public NullObject()
            {

            }
        }

        private Dictionary<object, WeakReference> _inner;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakDictionary{TKey, TValue}"/> class.
        /// </summary>
        public WeakDictionary()
        {
            WeakReferenceComparer<TKey> comparer = new WeakReferenceComparer<TKey>();
            _inner = new Dictionary<object, WeakReference>(comparer);
        }

        /// <summary>
        /// Adds a new item to the dictionary.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Add(TKey key, TValue value)
        {
            if (key == null) throw new ArgumentNullException("key"); 

            TValue local;
            if (this.TryGet(key, out local))
            {
                throw new ArgumentException(Resources.KeyAlreadyPresentInDictionary);
            }

            WeakKeyReference weakKey = new WeakKeyReference(key);

            WeakReference weakValue = new WeakReference(EncodeNullObject(value));

            _inner.Add(weakKey, weakValue);
        }

        private object EncodeNullObject(object value)
        {
            if (value == null)
            {
                return typeof(NullObject);
            }
            return value;
        }

        private TObject DecodeNullObject<TObject>(object innerValue)
        {
            if ((Type)innerValue == typeof(NullObject))
            {
                return default(TObject);
            }
            return (TObject)innerValue;
        }

        /// <summary>
        /// Determines if the dictionary contains the value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool ContainsValue(TValue value)
        {
            foreach (WeakReference weakValue in _inner.Values)
            {
                if (weakValue.IsAlive && weakValue.Target == (object)value)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns a count of the number of items in the dictionary.
        /// </summary>
        public int Count
        {
            get
            {
                this.CleanAbandonedItems();
                return this._inner.Count;
            }
        }

        private void CleanAbandonedItems()
        {
            List<object> list = new List<object>();
            foreach (KeyValuePair<object, WeakReference> pair in this._inner)
            {
                WeakReference key = (WeakReference)pair.Key;
                if (!key.IsAlive || !pair.Value.IsAlive)
                {
                    list.Add(key);
                }
            }
            foreach (TKey local in list)
            {
                this._inner.Remove(local);
            }
        }

        /// <summary>
        /// Determines if the dictionary contains a value for the key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            TValue local;
            return this.TryGet(key, out local);
        }

        /// <summary>
        /// Removes an item from the dictionary.
        /// </summary>
        /// <param name="key">The key of the item to be removed.</param>
        /// <returns>Returns true if the key was in the dictionary; return false otherwise.</returns>
        public bool Remove(TKey key)
        {
            return this._inner.Remove(key);
        }

        /// <summary>
        /// Retrieves a value from the dictionary.
        /// </summary>
        /// <param name="key">The key to look for.</param>
        /// <returns>The value in the dictionary.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the key does exist in the dictionary.
        /// Since the dictionary contains weak references, the key may have been removed by the
        /// garbage collection of the object.</exception>
        public TValue this[TKey key]
        {
            get
            {
                TValue local;
                if (!this.TryGet(key, out local))
                {
                    throw new KeyNotFoundException();
                }
                return local;
            }
        }

        /// <summary>
        /// Attempts to get a value from the dictionary.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        /// <returns>Returns true if the value was present; false otherwise.</returns>
        public bool TryGet(TKey key, out TValue value)
        {
            WeakReference reference;
            value = default(TValue);
            if (!this._inner.TryGetValue(key, out reference))
            {
                return false;
            }
            object innerValue = reference.Target;
            if (innerValue == null)
            {
                this._inner.Remove(key);
                return false;
            }
            value = this.DecodeNullObject<TValue>(innerValue);
            return true;
        }
    }
}
