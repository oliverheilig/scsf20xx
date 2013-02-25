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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.SmartParts;
using BankTellerModule;
using Microsoft.Practices.CompositeUI;
using System.Web;
using BankTellerCommon;

namespace CustomerMapExtensionModule
{
	[SmartPart]
	public partial class CustomerMap : UserControl
	{
		private Customer customer;
		private bool mapLoaded = false;

		const string mapUrlFormat = "http://maps.msn.com/home.aspx?strt1={0}&city1={2}&stnm1={3}&zipc1={4}";

		[State(StateConstants.CUSTOMER)]
		public Customer Customer
		{
			set { customer = value; }
		}

		public CustomerMap()
		{
			InitializeComponent();
		}

		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			
			if (this.Visible == true && mapLoaded == false)
			{
				LoadMap();
				mapLoaded = true;
			}
		}
		
		private void LoadMap()
		{
			if (customer != null)
			{
				string url = String.Format(mapUrlFormat, customer.Address1, customer.Address2, customer.City, customer.State, customer.ZipCode);				
				browser.Navigate(Uri.EscapeUriString(url));
			}
		}
	}
}
