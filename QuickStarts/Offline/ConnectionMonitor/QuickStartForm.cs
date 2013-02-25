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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.SmartClient.ConnectionMonitor;
using Microsoft.Practices.SmartClient.ConnectionMonitor.Implementations;
using System.Diagnostics;

namespace QuickStarts.ConnectionMonitor
{
	public partial class QuickStartForm : Form
	{
		public QuickStartForm()
		{
			InitializeComponent();
		}

        private void QuickStartForm_Load(object sender, EventArgs e)
        {
            ConnectionMonitorFactory.CreateFromConfiguration();
            LoadConnectionsAndNetworks();

			validateAddNetwork();
        }

        private void addConnectionButton_Click(object sender, EventArgs e)
        {
            string connectionName = connectionNameTextBox.Text;
            if (ConnectionMonitor.Connections.Contains(connectionName))
            {
                MessageBox.Show(Properties.Resources.ConnectionAlreadyExists);
                return;
            }

            int price;
            if (int.TryParse(priceTextBox.Text, out price))
            {
                Connection connection = QuickstartConnectionFactory.CreateConnection(connectionTypeDropDown.SelectedItem.ToString(), connectionName, price);
                ConnectionMonitor.Connections.Add(connection);
                LoadConnectionsAndNetworks();
            }
        }

        private ListViewGroup NetworksGroup
        {
            get { return connectionsListView.Groups["networksListViewGroup"]; }
        }

        private ListViewGroup ConnectionsGroup
        {
            get { return connectionsListView.Groups["connectionsListViewGroup"]; }
        }

        private Microsoft.Practices.SmartClient.ConnectionMonitor.ConnectionMonitor ConnectionMonitor
        {
            get { return Microsoft.Practices.SmartClient.ConnectionMonitor.ConnectionMonitorFactory.Instance; }
        }

        private void LoadConnectionsAndNetworks()
        {
            foreach (ListViewItem item in connectionsListView.Items)
            {
                item.Remove();
            }

            foreach (Connection connection in ConnectionMonitor.Connections)
            {
                ListViewItem item = new ConnectionListViewItem(connection, ConnectionsGroup);
                connectionsListView.Items.Add(item);
            }
            foreach (Network network in ConnectionMonitor.Networks)
            {
                ListViewItem item = new NetworkListViewItem(network, NetworksGroup);
                connectionsListView.Items.Add(item);
            }
        }

        private void networkAddButton_Click(object sender, EventArgs e)
        {
            string networkName = networkNameTextBox.Text;
            if (ConnectionMonitor.Networks.Contains(networkName))
            {
                MessageBox.Show(Properties.Resources.NetworkAlreadyExists);
                return;
            }

			string networkAddress = networkAddresstextBox.Text;

			try
			{
				Uri address = new Uri(networkAddress);
			}
			catch(UriFormatException ex)
			{
                Debug.Assert(ex != null);
				MessageBox.Show(Properties.Resources.BadAddress);
				return;
			}

			Network network = new Network(networkName, networkAddress);
			ConnectionMonitor.Networks.Add(network);
			LoadConnectionsAndNetworks();

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (connectionsListView.SelectedItems.Count == 0 || !(connectionsListView.SelectedItems[0] is ConnectionListViewItem))
            {
                e.Cancel = true;
                return;
            }

            ConnectionListViewItem item = (ConnectionListViewItem)connectionsListView.SelectedItems[0];
            bool isDesktopConnection = item.Connection is DesktopConnection;
            disconnectToolStripMenuItem.Enabled = isDesktopConnection;
            connectToolStripMenuItem.Enabled = isDesktopConnection;
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DesktopConnection connection = ((ConnectionListViewItem)connectionsListView.SelectedItems[0]).Connection as DesktopConnection;
            if (connection != null)
            {
                connection.Disconnect();
            }
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DesktopConnection connection = ((ConnectionListViewItem)connectionsListView.SelectedItems[0]).Connection as DesktopConnection;
            if (connection != null)
            {
                connection.Connect();
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            LoadConnectionsAndNetworks();
        }

		private void networkAddresstextBox_TextChanged(object sender, EventArgs e)
		{
			validateAddNetwork();
		}

		private void validateAddNetwork()
		{
			networkAddButton.Enabled = !string.IsNullOrEmpty(networkNameTextBox.Text) && !string.IsNullOrEmpty(networkAddresstextBox.Text);
		}

		private void networkNameTextBox_TextChanged(object sender, EventArgs e)
		{
			validateAddNetwork();
		}

        class ConnectionListViewItem : ListViewItem
        {
            Connection _connection;
            EventHandler<StateChangedEventArgs> _connectionStateChangedHandler;

            public ConnectionListViewItem(Connection connection, ListViewGroup group)
                : base(group)
            {
                _connection = connection;
                _connectionStateChangedHandler = _connection_StateChanged;
                _connection.StateChanged += _connectionStateChangedHandler;
                UpdateLook();
            }

            private void _connection_StateChanged(object sender, StateChangedEventArgs e)
            {
                if (ListView.InvokeRequired)
                {
                    ListView.Invoke(_connectionStateChangedHandler, sender, e);
                }
                else
                {
                    UpdateLook();
                }
            }

            private void UpdateLook()
            {
                string status = _connection.IsConnected ? "Connected" : "Disconnected";
                Text = String.Format(Properties.Resources.ConnectionDescription, _connection.ConnectionTypeName, status);
                ToolTipText = _connection.GetDetailedInfoString();
                ImageKey = String.Format("connection{0}", status);
            }

            public Connection Connection
            {
                get { return _connection; }
            }

            public override void Remove()
            {
                _connection.StateChanged -= _connectionStateChangedHandler;
                base.Remove();
            }
        }

        class NetworkListViewItem : ListViewItem
        {
            Network _network;
            EventHandler<StateChangedEventArgs> _networkStateChangedHandler;

            public NetworkListViewItem(Network network, ListViewGroup group)
                : base(group)
            {
                _network = network;
                _networkStateChangedHandler = new EventHandler<StateChangedEventArgs>(_network_StateChanged);
                _network.StateChanged += _networkStateChangedHandler;
                UpdateLook();
            }

            private void _network_StateChanged(object sender, StateChangedEventArgs e)
            {
                if (ListView.InvokeRequired)
                {
                    ListView.Invoke(_networkStateChangedHandler, sender, e);
                }
                else
                {
                    UpdateLook();
                }
            }

            private void UpdateLook()
            {
                string status = _network.Connected ? "Connected" : "Disconnected";
                Text = String.Format(Properties.Resources.NetworkDescription, _network.Name, status);
                ImageKey = String.Format("network{0}", status);
                ToolTipText = String.Format(Properties.Resources.NetworkDetails, _network.Address);
            }

            public Network Network
            {
                get { return _network; }
            }

            public override void Remove()
            {
                _network.StateChanged -= _networkStateChangedHandler;
                base.Remove();
            }
        }

	}
}