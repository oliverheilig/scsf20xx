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
using System.Threading;
using Microsoft.Practices.SmartClient.DisconnectedAgent;

namespace Microsoft.Practices.SmartClient.ApplicationBlocks.DisconnectedAgent.Tests.Mocks
{
	public class MockRequestDispatcher : RequestDispatcher
	{
		public List<Request> RequestsReceived = new List<Request>();
		public bool ForceFails;
		public int ThreadId;
		public static bool Block = false;
		public static bool SlowMode = false;
		public static bool UniqueDispatch;
		public static int DispatchRunning = 0;

		public static MockRequestDispatcher Instance;

		private int? sessionMaxDispatchAllowed = null;
		private int sessionDispatchCount = 0;

		public MockRequestDispatcher()
		{
			Instance = this;
		}

		//To lock the dispatch when 'value' requests has been dispatched
		//until something sets a new value.
		public int? SessionAllowDispatchs
		{
			set
			{
				sessionMaxDispatchAllowed = value;
				sessionDispatchCount = 0;
			}
		}

		public override DispatchResult Dispatch(Request request, string networkName)
		{
			if (DispatchRunning > 0)
				UniqueDispatch = false;

			DispatchRunning++;

			try
			{
				Console.Out.WriteLine(request.RequestId.ToString() + " >> " + request.Behavior.Stamps.ToString());
				sessionDispatchCount++;
				while (sessionMaxDispatchAllowed != null && sessionDispatchCount == sessionMaxDispatchAllowed)
					Thread.Sleep(100);

				while (Block)
					Thread.Sleep(10);

				if (SlowMode)
					Thread.Sleep(1000);
				ThreadId = Thread.CurrentThread.ManagedThreadId;
				if (ForceFails)
					return DispatchResult.Failed;
				else
				{
					RequestsReceived.Add(request);
					return DispatchResult.Succeeded;
				}
			}
			finally
			{
				DispatchRunning--;
			}
		}
	}
}