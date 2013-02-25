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
using Microsoft.Practices.SmartClient.DisconnectedAgent;

namespace Microsoft.Practices.SmartClient.ApplicationBlocks.DisconnectedAgent.Tests.Mocks
{
	public class MockConnectionMonitor : IConnectionMonitor
	{
		private string currentNetwork;
		private int currentConnectionPrice;
		public event EventHandler ConnectionStatusChanged;
		private bool isConnected;

		public int CurrentConnectionPrice
		{
			get
			{
				if (IsConnected)
					return currentConnectionPrice;
				else
					throw new InvalidOperationException();
			}
			set { currentConnectionPrice = value; }
		}

		public string CurrentNetwork
		{
			get { return currentNetwork; }
			set { currentNetwork = value; }
		}

		public bool IsConnected
		{
			get { return isConnected; }
		}

		public void MockChangeConnectionStatus(bool isConnected)
		{
			this.isConnected = isConnected;
			if (ConnectionStatusChanged != null) ConnectionStatusChanged(this, EventArgs.Empty);
		}


		public List<string> ConnectedNetworks
		{
			get
			{
				List<string> result = new List<string>();
				result.Add(currentNetwork);
				return result;
			}
		}
	}
}