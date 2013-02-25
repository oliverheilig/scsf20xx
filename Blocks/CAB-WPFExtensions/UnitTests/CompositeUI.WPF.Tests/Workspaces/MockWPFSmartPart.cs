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
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.WPF;
using System.Windows.Controls;
using System.Windows.Forms.Integration;

namespace Microsoft.Practices.CompositeUI.WPF.Tests.Workspaces
{
    [SmartPart]
    public class MockWPFSmartPart : System.Windows.Controls.Control, IDisposable
    {
        public bool IsDisposed = false;

        private IWPFUIElementAdapter _WPFUIElementAdapter;

        [ServiceDependency]
        public IWPFUIElementAdapter WPFUIElementAdapter
        {
            get { return _WPFUIElementAdapter; }
            set { _WPFUIElementAdapter = value; }
        }
	
        public System.Windows.Forms.Control ElementHost
        {
            get { return _WPFUIElementAdapter.Wrap(this); }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (!IsDisposed)
            {
                IsDisposed = true;
                if (_WPFUIElementAdapter != null)
                {
                    System.Windows.Forms.Control wrapper = _WPFUIElementAdapter.Wrap(this);
                    wrapper.Dispose();
                }
            }
        }

        #endregion
    }
}
