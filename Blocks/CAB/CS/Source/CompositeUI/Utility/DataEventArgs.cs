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
//===============================================================================
// Microsoft patterns & practices
// CompositeUI Application Block
//===============================================================================
// Copyright � Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Practices.CompositeUI.Utility
{
    /// <summary>
    /// Generic arguments class to pass to event handlers that need to receive data.
    /// </summary>
    /// <typeparam name="TData">The type of data to pass.</typeparam>
    public class DataEventArgs<TData> : EventArgs
    {
        TData data;

        /// <summary>
        /// Initializes the DataEventArgs class.
        /// </summary>
        /// <param name="data">Information related to the event.</param>
        /// <exception cref="ArgumentNullException">The data is null.</exception>
        public DataEventArgs(TData data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            this.data = data;
        }

        /// <summary>
        /// Gets the information related to the event.
        /// </summary>
        public TData Data
        {
            get { return data; }
        }

        /// <summary>
        /// Provides a string representation of the argument data.
        /// </summary>
        public override string ToString()
        {
            return data.ToString();
        }
    }
}
