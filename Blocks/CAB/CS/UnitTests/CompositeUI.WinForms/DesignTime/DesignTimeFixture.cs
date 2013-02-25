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

 
using Microsoft.VisualStudio.TestTools.UnitTesting;
 
 
 
 
 
 
 

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace Microsoft.Practices.CompositeUI.WinForms.Tests.DesignTime
{
	[TestClass]
	public class DesignTimeFixture
	{
		//[TestMethod]
		//[Ignore("Design time code generation needs to change... See CustomWorkItem.cs!!!")]
		public void DraggedComponentsAreSitedWithNameWhenWorkItemSited()
		{
			WorkItem workItem = new TestableRootWorkItem();
			CustomWorkItem wi = workItem.WorkItems.AddNew<CustomWorkItem>();

			Assert.IsTrue(wi.Items.ContainsObject(wi.customerInformation));
			Assert.IsTrue(wi.Items.ContainsObject(wi.anyComponent));
			Assert.IsTrue(wi.Items.ContainsObject("customerInformation"));
			Assert.IsTrue(wi.Items.ContainsObject("anyComponent"));
		}

		[TestMethod]
		public void DraggedWorkspaceIsAddedToCollection()
		{
			WorkItem workItem = new TestableRootWorkItem();
			CustomWorkItem wi = workItem.WorkItems.AddNew<CustomWorkItem>();

			Assert.IsTrue(wi.Workspaces.Contains("window"));
		}
	}
}
