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
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;
using System.Data.SqlClient;
using System;
using System.Windows.Forms;

namespace GlobalBank.Support.Module
{
	public class Module : ModuleInit
	{
		public override void Load()
		{
			using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.ConnectionString))
			{
				try
				{
					sqlConnection.Open();
					sqlConnection.Close();
				}
				catch(Exception ex)
				{
					FormError frm = new FormError();
					frm.ErrorMessage = ex.Message;
					frm.ShowDialog();
					Environment.Exit(1);
				}
			}
		}
	}
}