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
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.Tests.Mocks;
using Microsoft.Practices.CompositeUI.BuilderStrategies;
using Microsoft.Practices.ObjectBuilder;

namespace Microsoft.Practices.CompositeUI.Tests.BuilderStrategies
{
	[TestClass]
	public class CommandStrategyFixture
	{
		[TestMethod]
		public void AddingObjectWithCommandHandlerRegisterTheCommand()
		{
			CommandStrategy strategy = new CommandStrategy();
			MockBuilderContext context = new MockBuilderContext(strategy);
			WorkItem wi = new TestableRootWorkItem();
			context.Locator.Add(new DependencyResolutionLocatorKey(typeof(WorkItem), null), wi);

			SampleClass sample = new SampleClass();

			strategy.BuildUp(context, typeof(SampleClass), sample, null);

			Assert.IsTrue(wi.Items.Contains("TestCommand"));
		}

		[TestMethod]
		[ExpectedException(typeof(CommandException))]
		public void StaticHandlerThrows()
		{
			CommandStrategy strategy = new CommandStrategy();
			MockBuilderContext context = new MockBuilderContext(strategy);
			WorkItem wi = new TestableRootWorkItem();
			context.Locator.Add(new DependencyResolutionLocatorKey(typeof(WorkItem), null), wi);

			SampleStaticClass sample = new SampleStaticClass();

			strategy.BuildUp(context, typeof(SampleStaticClass), sample, null);
		}


		class SampleClass
		{
			[CommandHandler("TestCommand")]
			public void TestCommandHandler(object sender, EventArgs e)
			{
			}
		}

		class SampleStaticClass
		{
			[CommandHandler("TestCommand")]
			public static void TestCommandHandler(object sender, EventArgs e)
			{
			}
		}
	}
}
