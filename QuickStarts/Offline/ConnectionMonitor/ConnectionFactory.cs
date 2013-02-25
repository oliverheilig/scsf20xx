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
using Microsoft.Practices.SmartClient.ConnectionMonitor;
using Microsoft.Practices.SmartClient.ConnectionMonitor.Implementations;

namespace QuickStarts.ConnectionMonitor
{
    public static class QuickstartConnectionFactory
    {
        public static Connection CreateConnection(string type, string name, int price)
        { 
            Connection connection;
            switch (type)
            { 
                case "WirelessConnection":
                    connection = new WirelessConnection(name, price);
                    break;
                case "WiredConnection":
                    connection = new WiredConnection(name, price);
                    break;
                case "DesktopConnection":
                    connection = new DesktopConnection(name, price);
                    break;
                case "NicConnection":
                    connection = new NicConnection(name, price);
                    break;
                default:
                    throw new ArgumentException("Unrecognized connection type.");
            }
            return connection;
        }
    }
}
