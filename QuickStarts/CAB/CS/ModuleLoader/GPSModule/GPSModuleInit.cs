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
using Microsoft.Practices.CompositeUI;
using System.Windows.Forms;
using GPSModule;
using Microsoft.Practices.CompositeUI.Services;

namespace SampleModule1
{
	// The module initialization class
	public class GPSModuleInit : ModuleInit
	{
		private WorkItem rootWorkItem;

		[ServiceDependency]
		public WorkItem RootWorkItem
		{
			set { rootWorkItem = value; }
		}

		public override void Load()
		{
			// This is just to show when the module initialization happens
			MessageBox.Show("SampleModule Started");

			SampleWorkItem wi = rootWorkItem.WorkItems.AddNew<SampleWorkItem>();
			wi.Run();
		}
	}
}
