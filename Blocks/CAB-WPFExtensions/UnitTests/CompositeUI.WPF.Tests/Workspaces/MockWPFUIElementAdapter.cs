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
using Microsoft.Practices.CompositeUI.WPF;
using System.Windows.Forms.Integration;
using System.Windows;

namespace Microsoft.Practices.CompositeUI.WPF.Tests.Workspaces
{
    public class MockWPFUIElementAdapter : IWPFUIElementAdapter
    {
        public Dictionary<UIElement, ElementHost> Hosts = new Dictionary<UIElement, ElementHost>();
        
        #region IWPFUIElementAdapter Members

        public System.Windows.UIElement Unwrap(System.Windows.Forms.Control control)
        {
            if (control is ElementHost)
            {
                if (Hosts.ContainsValue((ElementHost)control))
                {
                    return ((ElementHost)control).Child;
                }
            }
            return null;
        }
        
        public System.Windows.Forms.Control Wrap(UIElement smartPart)
        {
            if (!Hosts.ContainsKey(smartPart))
            {
                ElementHost host = new ElementHost();
				if (smartPart is FrameworkElement)
				{
					FrameworkElement typedSmartPart = (FrameworkElement)smartPart;
					host.Size = new System.Drawing.Size((int)typedSmartPart.Width, (int)typedSmartPart.Height);
				}
                host.Child = smartPart;
                host.PropertyMap.Remove("Visible");
                host.PropertyMap["Visible"] += new PropertyTranslator(VisiblePropertyTranslator);
                Hosts.Add(smartPart, host);
            }
            return Hosts[smartPart];
        }

        void VisiblePropertyTranslator(object host, string propertyName, object value)
        {
            ElementHost eh = host as ElementHost;
            if (eh != null)
            {
                if (value is bool && eh.Child != null)
                {
                    Visibility visibility = ((bool)value) ? Visibility.Visible : Visibility.Hidden;
                    eh.Child.Visibility = visibility;
                }
            }
        }

 

 

        

        #endregion
    }
}
