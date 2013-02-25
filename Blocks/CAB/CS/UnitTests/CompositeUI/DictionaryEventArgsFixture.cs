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

namespace Microsoft.Practices.CompositeUI.Tests
{
	[TestClass]
	public class DictionaryEventArgsFixture
	{
		private DictionaryEventArgs eventArgs;

		[TestInitialize]
		public void TestSetup()
		{
			eventArgs = new DictionaryEventArgs();
		}

		[TestMethod]
		public void ToStringShowsContents()
		{
			eventArgs.Data.Add("one", "1");
			eventArgs.Data.Add("two", "2");

			Assert.AreEqual("one: 1, two: 2", eventArgs.ToString());
		}
	}
}
