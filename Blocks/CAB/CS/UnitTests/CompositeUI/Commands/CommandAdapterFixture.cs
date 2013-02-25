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

namespace Microsoft.Practices.CompositeUI.Tests.Commands
{
	[TestClass]
	public class CommandAdapterFixture
	{
		private static MockAdapter adapter;

		[TestInitialize]
		public void SetUp()
		{
			adapter = new MockAdapter();
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ThrowsIfBindNullCommand()
		{
			adapter.BindCommand(null);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ThrowsIfBindTwice()
		{
			adapter.BindCommand(new Command());
			adapter.BindCommand(new Command());
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ThrowsIfUnbindNullCommand()
		{
			adapter.UnbindCommand(null);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ThrowsIfUnbindToNotBoundCommand()
		{
			adapter.UnbindCommand(new Command());
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ThrowsIfUnbindWithDifferentCommand()
		{
			adapter.BindCommand(new Command());
			adapter.UnbindCommand(new Command());
		}

		[TestMethod]
		public void CanBindAgainIfUnbound()
		{
			Command cmd = new Command();
			adapter.BindCommand(cmd);
			adapter.UnbindCommand(cmd);

			adapter.BindCommand(new Command());
		}

		class MockAdapter : CommandAdapter
		{
			public override void AddInvoker(object invoker, string eventName)
			{
				throw new Exception("The method or operation is not implemented.");
			}

			public override void RemoveInvoker(object invoker, string eventName)
			{
				throw new Exception("The method or operation is not implemented.");
			}

			public override int InvokerCount
			{
				get { throw new Exception("The method or operation is not implemented."); }
			}

			public override bool ContainsInvoker(object invoker)
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}
	}
}
