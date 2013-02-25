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
using Microsoft.Practices.ObjectBuilder;
using System.Diagnostics;

namespace Microsoft.Practices.CompositeUI.Tests.Instrumentation
{
	[TestClass]
	public class TraceSourceAttributeFixture
	{
		[TestMethod]
		public void AttributeGetsTraceSourceFromCatalogIfAvailable()
		{
			TraceSourceCatalogService catalog = new TraceSourceCatalogService();
			Locator locator = new Locator();
			Builder builder = new Builder();
			locator.Add(new DependencyResolutionLocatorKey(typeof(ITraceSourceCatalogService), null), catalog);

			MockTracedClass mock = builder.BuildUp<MockTracedClass>(locator, null, null);

			Assert.AreEqual(1, catalog.TraceSources.Count);
			Assert.IsNotNull(mock.TraceSource);
		}

		[TestMethod]
		public void AttributeInjectsNullIfNoCatalogAvailable()
		{
			Locator locator = new Locator();
			Builder builder = new Builder();

			MockTracedClass mock = builder.BuildUp<MockTracedClass>(locator, null, null);

			Assert.IsNull(mock.TraceSource);
		}

		#region Helper classes

		class MockTracedClass
		{
			private TraceSource traceSource;

			[TraceSource("Foo")]
			public TraceSource TraceSource
			{
				get { return traceSource; }
				set { traceSource = value; }
			}
		}

		#endregion
	}
}
