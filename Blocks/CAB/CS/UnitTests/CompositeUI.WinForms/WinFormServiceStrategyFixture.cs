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

namespace Microsoft.Practices.CompositeUI.WinForms.Tests
{
	[TestClass]
	public class WinFormServiceStrategyFixture
	{
		[TestMethod]
		public void ControlActivationServiceCalledWhenControlAdded()
		{
			WorkItem workItem = new TestableRootWorkItem();
			WorkItem wi = workItem.WorkItems.AddNew<WorkItem>();

			Assert.IsTrue(wi.Services.ContainsLocal(typeof(IControlActivationService)));
		}
	}
}
