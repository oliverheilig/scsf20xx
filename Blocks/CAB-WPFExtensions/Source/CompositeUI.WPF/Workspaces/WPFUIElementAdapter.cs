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
using System.Windows;
using System.Windows.Forms.Integration;
using System.Windows.Forms;

namespace Microsoft.Practices.CompositeUI.WPF
{
    /// <summary>
    /// Default implementation of the <see cref="IWPFUIElementAdapter"/>
    /// </summary>
    public class WPFUIElementAdapter : IWPFUIElementAdapter
    {

        private WeakDictionary<UIElement, ElementHost> _hosts = new WeakDictionary<UIElement, ElementHost>();

        #region IWPFUIElementAdapter Members

        /// <summary>
        /// See <see cref="IWPFUIElementAdapter.Unwrap"/> for more information.
        /// </summary>
        public System.Windows.UIElement Unwrap(System.Windows.Forms.Control control)
        {
            if (control is ElementHost)
            {
                if (_hosts.ContainsValue((ElementHost)control))
                {
                    return ((ElementHost)control).Child;
                }
            }
            return null;
        }

        /// <summary>
        /// See <see cref="IWPFUIElementAdapter.Wrap"/> for more information.
        /// </summary>
        public System.Windows.Forms.Control Wrap(UIElement smartPart)
        {
            if (smartPart == null)
                throw new ArgumentNullException("smartPart cannot be null");

            if (!_hosts.ContainsKey(smartPart))
            {
                ElementHost host = new ElementHost();
                if (smartPart is FrameworkElement)
                {
                    FrameworkElement typedSmartPart = (FrameworkElement)smartPart;
                    host.Size = new System.Drawing.Size((int)typedSmartPart.Width, (int)typedSmartPart.Height);
                }
                host.Child = smartPart;
                
                host.ParentChanged += new EventHandler(HostParentChanged);
                host.VisibleChanged += new EventHandler(HostVisibleChanged);
                host.Disposed += new EventHandler(HostDisposed);

                _hosts.Add(smartPart, host);
            }

            return _hosts[smartPart];
        }

        void HostDisposed(object sender, EventArgs e)
        {
            ElementHost host = sender as ElementHost;
            if (host != null)
            {
                _hosts.Remove(host.Child);
            }
        }

        void HostParentChanged(object sender, EventArgs e)
        {
            ElementHost host = sender as ElementHost;
            if (host != null && host.Parent != null)
            {
                host.Parent.VisibleChanged += new EventHandler(ParentVisibleChanged);
            }
        }

        void ParentVisibleChanged(object sender, EventArgs e)
        {
            Control parent = sender as Control;
            if (parent != null)
            {
                ElementHost host = GetElementHostChild(parent);
                if (host != null)
                {
                    host.Visible = parent.Visible;
                    // This action is not called when Hide method in host is called.
                    //Patching
                    HostVisibleChanged(host, e);

                }
            }
        }

        void HostVisibleChanged(object sender, EventArgs e)
        {
            ElementHost host = sender as ElementHost;
            if (host != null)
            {
                host.Child.Visibility = host.Visible ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
            }
        }

        ElementHost GetElementHostChild(Control c)
        {
            for (int i = 0; i < c.Controls.Count; i++)
            {
                if (c.Controls[i] is ElementHost)
                    return c.Controls[i] as ElementHost;
            }
            return null;
        }

        #endregion
    }
}
