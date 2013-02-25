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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Practices.SmartClient.DisconnectedAgent;
using Quickstarts.DisconnectedAgent.IntegerCalculatorAgent;
using Microsoft.Practices.SmartClient.ConnectionMonitor.Implementations;
using Microsoft.Practices.SmartClient.EndpointCatalog;
using Microsoft.Practices.SmartClient.ConnectionMonitor;
using System.Configuration;
using Microsoft.Practices.SmartClient.EnterpriseLibrary;
using Microsoft.Practices.SmartClient.EndpointCatalog.Configuration;

namespace Quickstarts.DisconnectedAgent
{
    public partial class FormControlPanel : Form
    {
        RequestManager manager;

        public FormControlPanel()
        {
            InitializeComponent();
            tagComboBox.SelectedIndex = 0;

            manager = DatabaseRequestManagerIntializer.Initialize("QueueDatabase");
			manager.RequestQueue.RequestEnqueued += new EventHandler<RequestEnqueuedEventArgs>(OnRequestEnqueued);
            manager.RequestDispatched += new EventHandler<RequestDispatchedEventArgs>(OnRequestDispatched);
            IntegerCalculatorServiceDisconnectedAgentCallback.AddReturn += new EventHandler<AddReturnEventArgs>(OnAddReturn);
            
            UpdateRequestQueue();
            UpdateFailedQueue();
        }

        private delegate void UpdateServedRequestsQueueDelegate(AddReturnEventArgs e);
        private void UpdateServedRequestsQueue(AddReturnEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateServedRequestsQueueDelegate(UpdateServedRequestsQueue), e);
            }
            else
            {
                ListViewItem item = GetListViewItem(e.Request);
                item.SubItems.Add(e.ReturnValue.ToString());
                servedRequestsListView.Items.Add(item);
            }
        }

        private delegate void UpdateRequestQueueDelegate();
        private void UpdateRequestQueue()
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateRequestQueueDelegate(UpdateRequestQueue));
            }
            else
            {
                requestQueueListView.Items.Clear();
                foreach (Request request in manager.RequestQueue.GetRequests())
                {
                    requestQueueListView.Items.Add(GetListViewItem(request));
                }
            }
        }

        private delegate void UpdateFailedQueueDelegate();
        private void UpdateFailedQueue()
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateFailedQueueDelegate(UpdateFailedQueue));
            }
            else
            {
                failedQueueListView.Items.Clear();
                foreach (Request request in manager.DeadLetterQueue.GetRequests())
                {
                    failedQueueListView.Items.Add(GetListViewItem(request));
                }
            }
        }

        private static ListViewItem GetListViewItem(Request request)
        {
            ListViewItem item = new ListViewItem(request.RequestId.ToString());
            item.SubItems.Add(String.Format(Properties.Resources.RequestListViewItemDescription, request.CallParameters[0], request.CallParameters[1]));
            item.SubItems.Add(request.Behavior.Tag);
            item.Tag = request;
            return item;
        }

        private void OnAddReturn(object sender, AddReturnEventArgs e)
        {
            UpdateServedRequestsQueue(e);
        }

        private void OnRequestEnqueued(object sender, RequestEnqueuedEventArgs e)
        {
            UpdateRequestQueue();
            Status = String.Format(Properties.Resources.RequestEnqueued, e.Request.RequestId);
        }

        private void OnRequestDispatched(object sender, RequestDispatchedEventArgs e)
        {
            UpdateRequestQueue();
            UpdateFailedQueue();
            switch (e.Result)
            { 
                case DispatchResult.Expired:
                    Status = String.Format(Properties.Resources.RequestExpired, e.Request.RequestId.ToString());
                    break;
                case DispatchResult.Failed:
                    Status = String.Format(Properties.Resources.ErrorDispatchingRequest, e.Request.RequestId.ToString());
                    break;
                case DispatchResult.Succeeded:
                    Status = String.Format(Properties.Resources.RequestSuccessfullyDispatched, e.Request.RequestId.ToString());
                        break;
            }
        }

        private string Status
        {
            set { toolStripStatusLabel1.Text = value; }
        }

        private void FormControlPanel_Load(object sender, EventArgs e)
        {
            FormAddIntegers frmCalculator = new FormAddIntegers();
            frmCalculator.StartPosition = FormStartPosition.Manual;
            frmCalculator.Location = new Point(this.DesktopLocation.X + this.DesktopBounds.Width, this.DesktopLocation.Y);
            frmCalculator.Show(this);
        }
       
        private void buttonManualDispatch_Click(object sender, EventArgs e)
        {
            string tag = tagComboBox.SelectedItem.ToString();
            if (tag == "All")
            {
                manager.DispatchAllPendingRequests();
            }
            else
            {
                manager.DispatchPendingRequestsByTag(tag);
            }
        }

        private void dispatchSelectedRequestsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in requestQueueListView.SelectedItems)
            {
                manager.DispatchRequest((Request)item.Tag);
            }
        }

        private void requestQueueContextStrip_Opening(object sender, CancelEventArgs e)
        {
            dispatchSelectedRequestsToolStripMenuItem.Enabled = (requestQueueListView.SelectedItems.Count > 0);
        }

        private void failedQueueContextStrip_Opening(object sender, CancelEventArgs e)
        {
            enqueueSelectedRequestsToolStripMenuItem.Enabled = (failedQueueListView.SelectedItems.Count > 0);
            removeSelectedRequestsFromQueueToolStripMenuItem.Enabled = (failedQueueListView.SelectedItems.Count > 0);
        }

        private void enqueueSelectedRequestsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in failedQueueListView.SelectedItems)
            {
                Request request = (Request)item.Tag;
                manager.RequestQueue.Enqueue(request);
                manager.DeadLetterQueue.Remove(request);
            }
            UpdateFailedQueue();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                manualDispatchButton.Enabled = false;
                tagComboBox.Enabled = false;
                dispatchRequestsLabel.Enabled = false;

                manager.StartAutomaticDispatch();
            }
            else
            {
                manager.StopAutomaticDispatch();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                manualDispatchButton.Enabled = true;
                tagComboBox.Enabled = true;
                dispatchRequestsLabel.Enabled = true;
            }
            if (manager.AutomaticDispatcherRunning)
            {
                manager.StopAutomaticDispatch();
            }
        }

        private void connectedRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                ((DesktopConnection)ConnectionMonitorFactory.Instance.Connections["DesktopConnection"]).Connect();
                webServiceEnabledCheckBox.Enabled = true;
            }
        }

        private void disconnectedRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                ((DesktopConnection)ConnectionMonitorFactory.Instance.Connections["DesktopConnection"]).Disconnect();
                webServiceEnabledCheckBox.Enabled = false;
            }
        }

        private void webServiceEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                Endpoint endpoint = new Endpoint("IntegerCalculatorEndpoint");
                EndpointSection endpointSection = ConfigurationManager.GetSection("Endpoints") as EndpointSection;
                EndpointItemElement endpointConfig = endpointSection.EndpointItemCollection.GetEndpoint("IntegerCalculatorEndpoint");
                endpoint.Default = new EndpointConfig(endpointConfig.Address);
                manager.EndpointCatalog.SetEndpoint(endpoint);
            }
            else
            {
                Endpoint endpoint = new Endpoint("IntegerCalculatorEndpoint");
                endpoint.Default = new EndpointConfig(ConfigurationManager.AppSettings["NonExistingURL"]);
                manager.EndpointCatalog.SetEndpoint(endpoint);
            }
        }

        private void clearListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            servedRequestsListView.Items.Clear();
        }

        private void servedRequestsQueue_Opening(object sender, CancelEventArgs e)
        {
            clearListToolStripMenuItem.Enabled = servedRequestsListView.Items.Count > 0;
        }

        private void removeSelectedRequestsFromQueueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in failedQueueListView.SelectedItems)
            {
                Request request = (Request)item.Tag;
                manager.DeadLetterQueue.Remove(request);
            }
            UpdateFailedQueue();
        }
    }
}