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
using System.Collections.Generic;
using Microsoft.Practices.SmartClient.DisconnectedAgent;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.SmartClient.ApplicationBlocks.DisconnectedAgent.Tests
{
	[TestClass]
	public class CommandFixture
	{
		private bool called = false;
		private string param1SetInside = string.Empty;
		private int param2SetInside = 0;
		private IEnumerable<Request> iterator = new Request[] {new Request()};

		[TestMethod]
		public void CreateCommandSetsPropertiesCorrectly()
		{
			Command dispatchCommand = new Command(this, "MockCommandMethod", "string", 8);

			dispatchCommand.Execute();

			Assert.IsTrue(called);
			Assert.AreEqual("string", param1SetInside);
			Assert.AreEqual(8, param2SetInside);
		}

		[TestMethod]
		public void ExecuteReturnsRightResult()
		{
			Command dispatchCommand = new Command(this, "MockCommandMethod", "string", 8);

			object result = dispatchCommand.Execute();
			Assert.AreEqual(iterator, result);
		}

		[TestMethod]
		public void ExecuteCommandWithoutParamsCallsMethod()
		{
			Command dispatchCommand = new Command(this, "MockMethodNoParams");

			object result = dispatchCommand.Execute();
			Assert.AreEqual(1, result);
		}

		[TestMethod]
		public void ExecuteVoidCommandCallsMethod()
		{
			Command dispatchCommand = new Command(this, "MockVoidMethod");
			called = false;
			object result = dispatchCommand.Execute();
			Assert.IsTrue(called);
		}


		public IEnumerable<Request> MockCommandMethod(string par1, int par2)
		{
			called = true;
			param1SetInside = par1;
			param2SetInside = par2;

			return iterator;
		}

		public int MockMethodNoParams()
		{
			return 1;
		}

		public void MockVoidMethod()
		{
			called = true;
		}
	}
}